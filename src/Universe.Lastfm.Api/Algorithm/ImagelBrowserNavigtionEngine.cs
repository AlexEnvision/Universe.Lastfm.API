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
using System.Threading;
using OpenQA.Selenium;
using Universe.Diagnostic.Logger;
using Universe.Helpers.Extensions;
using Universe.Lastfm.Api.Algorithm.Searchers;
using Universe.Lastfm.Api.Infrastracture;
using Universe.Lastfm.Api.Models.Base;
using Universe.Types.Collection;

namespace Universe.Lastfm.Api.Algorithm
{
    /// <summary>
    /// <author>Alex Universe</author>
    /// <author>Alex Envision</author>
    /// </summary>
    internal class ImageBrowserNavigationEngine : OpenBrowserEngine
    {
        private readonly IUniverseLastApiSettings _settings;

        private IWebDriver _driver;

        private readonly ISeleniumLinksSearcher _seleniumLinksSearcher;

        public const string ImagePathInSite = "+images";

        public const string SecondImageDomain = "lastfm.freetls.fastly.net";

        public const string ImageSizeMarker = "i/u/770x0";

        public ImageBrowserNavigationEngine(IUniverseLastApiSettings settings, IUniverseLogger log) : base(settings)
        {
            MinWaitPause = 250;

            _settings = settings;
            _seleniumLinksSearcher = new SeleniumLinksSearcher(log);
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
        public ImageNavigateResult Run(ImageNavigateParameters parameters)
        {
            if (parameters.SiteNav.IsNullOrEmpty())
                return new ImageNavigateResult
                {
                    IsSuccessful = false
                };

            using (_driver = GetWebDriver(parameters))
            {
                var images = StringExtension.CombineUrl(parameters.SiteNav, ImagePathInSite);
                _driver.Navigate().GoToUrl(images);

                var sessionId = Guid.NewGuid().ToString();
                var links = _seleniumLinksSearcher.GetPageSearchLinksSafe(_driver, sessionId);

                var smallSizeImageLinks = links
                    .Where(x => x.Href.Contains(ImagePathInSite)).ToList();

                var largeSizeImageLinks = new MatList<LinkImageInformation>();
                for (var index = 0; index < smallSizeImageLinks.Count; index++)
                {
                    var smallSizeImageLink = smallSizeImageLinks[index];
                    if (parameters.Limit > 0 && index + 1 > parameters.Limit)
                        break;

                    var href = smallSizeImageLink.Href;
                    _driver.Navigate().GoToUrl(href);
                    var largeImages = _seleniumLinksSearcher.GetPageSearchImgLinksSafe(_driver, sessionId);
                    largeSizeImageLinks += largeImages
                        .Where(x => x.Href.Contains(SecondImageDomain) && x.Href.Contains(ImageSizeMarker)).ToList();
                }

                return new ImageNavigateResult
                {
                    SmallSizeImageLinks = smallSizeImageLinks,
                    LargeSizeImageLinks = largeSizeImageLinks,
                    IsSuccessful = true
                };
            }
        }
    }

    internal class ImageNavigateParameters : OpenBrowserParameters
    {
        public int Limit { get; set; }
    }

    internal class ImageNavigateResult : BaseResponce
    {
        public List<LinkInformation> SmallSizeImageLinks { get; set; }

        public MatList<LinkImageInformation> LargeSizeImageLinks { get; set; }

        public ImageNavigateResult()
        {
            SmallSizeImageLinks = new List<LinkInformation>();
            LargeSizeImageLinks = new MatList<LinkImageInformation>();
        }
    }
}