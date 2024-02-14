//  ╔═════════════════════════════════════════════════════════════════════════════════╗
//  ║                                                                                 ║
//  ║   Copyright 2024 Universe.Lastfm.Api                                            ║
//  ║                                                                                 ║
//  ║   Licensed under the Apache License, Version 2.0 (the "License");               ║
//  ║   you may not use this file except in compliance with the License.              ║
//  ║   You may obtain a copy of the License at                                       ║
//  ║                                                                                 ║
//  ║       http://www.apache.org/licenses/LICENSE-2.0                                ║
//  ║                                                                                 ║
//  ║   Unless required by applicable law or agreed to in writing, software           ║
//  ║   distributed under the License is distributed on an "AS IS" BASIS,             ║
//  ║   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.      ║
//  ║   See the License for the specific language governing permissions and           ║
//  ║   limitations under the License.                                                ║
//  ║                                                                                 ║
//  ║                                                                                 ║
//  ║   Copyright 2024 Universe.Lastfm.Api                                            ║
//  ║                                                                                 ║
//  ║   Лицензировано согласно Лицензии Apache, Версия 2.0 ("Лицензия");              ║
//  ║   вы можете использовать этот файл только в соответствии с Лицензией.           ║
//  ║   Вы можете найти копию Лицензии по адресу                                      ║
//  ║                                                                                 ║
//  ║       http://www.apache.org/licenses/LICENSE-2.0.                               ║
//  ║                                                                                 ║
//  ║   За исключением случаев, когда это регламентировано существующим               ║
//  ║   законодательством или если это не оговорено в письменном соглашении,          ║
//  ║   программное обеспечение распространяемое на условиях данной Лицензии,         ║
//  ║   предоставляется "КАК ЕСТЬ" и любые явные или неявные ГАРАНТИИ ОТВЕРГАЮТСЯ.    ║
//  ║   Информацию об основных правах и ограничениях,                                 ║
//  ║   применяемых к определенному языку согласно Лицензии,                          ║
//  ║   вы можете найти в данной Лицензии.                                            ║
//  ║                                                                                 ║
//  ╚═════════════════════════════════════════════════════════════════════════════════╝

using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Universe.Diagnostic.Logger;
using Universe.Helpers.Extensions;
using Universe.Lastfm.Api.Algorithm.Comparers;
using Universe.Lastfm.Api.Algorithm.Helpers;
using Universe.Lastfm.Api.Algorithm.Searchers;
using Universe.Lastfm.Api.Infrastracture;
using Universe.Lastfm.Api.Models.Base;

namespace Universe.Lastfm.Api.Algorithm
{
    /// <summary>
    /// <author>Alex Universe</author>
    /// <author>Alex Envision</author>
    /// </summary>
    internal class DescriptionBrowserNavigationEngine : OpenBrowserEngine
    {
        private readonly IUniverseLastApiSettings _settings;

        private readonly IUniverseLogger _log;

        private IWebDriver _driver;

        public const string WikiPathInSite = "+wiki";

        public const string DivClassDescription = "wiki-content";

        public DescriptionBrowserNavigationEngine(IUniverseLastApiSettings settings, IUniverseLogger log) : base(settings)
        {
            MinWaitPause = 250;

            _settings = settings;
            _log = log;
        }

        public IWebDriver GetWebNavDriver(OpenBrowserParameters searchParameters)
        {
            return base.GetWebDriver(searchParameters);
        }

        /// <summary>
        ///     Запуск браузера
        /// </summary>
        /// <param name="parameters">Параметры</param>
        /// <returns></returns>
        public DescriptionNavigateResult Run(DescriptionNavigateParameters parameters)
        {
            if (parameters.SiteNav.IsNullOrEmpty())
                return new DescriptionNavigateResult
                {
                    IsSuccessful = false
                };

            using (_driver = GetWebDriver(parameters))
            {
                var images = StringExtension.CombineUrl(parameters.SiteNav, WikiPathInSite);
                _driver.Navigate().GoToUrl(images);

                var sessionId = Guid.NewGuid().ToString();

                // $"//div[@class='{DivClassDescription}']"
                var descriptions = 
                    GetInformationsSafe(sessionId, 
                            _driver.FindElementsByXPathSafe(
                                _log, 
                                sessionId, 
                                $"//div[@class='{DivClassDescription}']"))
                    .ToList();

                return new DescriptionNavigateResult()
                {
                    Description = descriptions,
                    IsSuccessful = true
                };
            }
        }

        private List<DescriptionInformation> GetInformations(
            string sessionId,
            List<IWebElement> elements)
        {
            return elements
                .Select(a => new
                {
                    Text = WebElementExtensions.GetText(a, _log),
                    InnerHtml = WebElementExtensions.GetText(a, _log),
                })
                .Where(a => !string.IsNullOrWhiteSpace(a.Text) && !string.IsNullOrWhiteSpace(a.InnerHtml))
                .Select(a => new DescriptionInformation
                {
                    Title = a.Text,
                    InnerHtml = a.InnerHtml,
                }).ToList();
        }

        private IEnumerable<DescriptionInformation> GetInformationsSafe(
            string sessionId,
            List<IWebElement> elements)
        {
            try
            {
                var infos = GetInformations(sessionId, elements);
                return infos;
            }
            catch (StaleElementReferenceException srex)
            {
                _log.Warning(srex, $"{sessionId} {srex.Message}");
                return new List<DescriptionInformation>();
            }
            catch (Exception ex)
            {
                _log.Warning(ex, $"{sessionId} {ex.Message}");
                return new List<DescriptionInformation>();
            }
        }
    }

    internal class DescriptionNavigateParameters : OpenBrowserParameters
    {
        public int Limit { get; set; }
    }

    internal class DescriptionNavigateResult : BaseResponce
    {
        public List<DescriptionInformation> Description { get; set; }
    }
}