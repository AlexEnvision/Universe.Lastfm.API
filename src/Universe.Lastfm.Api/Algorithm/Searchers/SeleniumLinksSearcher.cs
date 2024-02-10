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
using Universe.Diagnostic.Logger;
using Universe.Helpers.Extensions;
using Universe.Lastfm.Api.Algorithm.Comparers;
using Universe.Lastfm.Api.Algorithm.Helpers;

namespace Universe.Lastfm.Api.Algorithm.Searchers
{
    /// <inheritdoc/>
    public class SeleniumLinksSearcher : ISeleniumLinksSearcher
    {
        private readonly IUniverseLogger _log;
        private readonly LinkInformationComparer linkInformationComparer = new ();

        public SeleniumLinksSearcher(IUniverseLogger log)
        {
            _log = log;
        }

        /// <inheritdoc/>
        public List<LinkInformation> GetPageSearchLinksSafe(IWebDriver driver, string sessionId)
        {
            var url = driver.GetUrlSafe(_log, sessionId);
            var originalAtuhority = !url.IsNullOrEmpty() ? new Uri(url).GetLeftPart(UriPartial.Authority) : string.Empty;

            var allPageLinks = GetLinkInformationsSafe(sessionId, driver.FindElementsByXPathSafe(_log, sessionId, "//a"), true)
                .Concat(GetLinkInformationsSafe(sessionId, driver.FindElementsByXPathSafe(_log, sessionId, "//a[not(ancestor::nav)]"), false))
                .Where(li => li.IsValidPageLink(originalAtuhority))
                .Distinct(new LinkInformationUriComparer<LinkInformation>())
                .OrderByDescending(l => l, linkInformationComparer).ToList();

            return allPageLinks;
        }

        public List<LinkImageInformation> GetPageSearchImgLinksSafe(IWebDriver driver, string sessionId)
        {
            var url = driver.GetUrlSafe(_log, sessionId);
            var originalAtuhority = !url.IsNullOrEmpty() ? new Uri(url).GetLeftPart(UriPartial.Authority) : string.Empty;

            var allPageImageLinks = GetImgLinkInformationsSafe(sessionId, driver.FindElementsByXPathSafe(_log, sessionId, "//img[@class='js-gallery-image']"), true)
                .Concat(GetImgLinkInformationsSafe(sessionId, driver.FindElementsByXPathSafe(_log, sessionId, "//img[not(ancestor::nav)]"), false))
                 //.Where(li => li.IsValidPageLink(originalAtuhority))
                .Distinct(new LinkInformationUriComparer<LinkImageInformation>())
                .OrderByDescending(l => l, linkInformationComparer).ToList();

            return allPageImageLinks;
        }

        private List<LinkInformation> GetLinkInformations(
            string sessionId,
            List<IWebElement> elements,
            bool isNavLink = false)
        {
            return elements
                .Select(a => new
                {
                    Href = a.GetAttribute("href"), 
                    Text = a.GetText(_log),
                    InnerHtml = a.GetText(_log),
                })
                .Where(a => !string.IsNullOrWhiteSpace(a.Text) && !string.IsNullOrWhiteSpace(a.Href))
                .Select(a => new LinkInformation
                {
                    Href = a.Href,
                    Title = a.Text,
                    InnerHtml = a.InnerHtml,
                    Url = new Uri(a.Href),
                    IsNavLink = isNavLink
                }).ToList();
        }
        private IEnumerable<LinkInformation> GetLinkInformationsSafe(
            string sessionId,
            List<IWebElement> elements,
            bool isNavLink = false)
        {
            try
            {
                return GetLinkInformations(sessionId, elements, isNavLink);
            }
            catch (StaleElementReferenceException srex)
            {
                _log.Warning(srex, $"{sessionId} {srex.Message}");
                return new List<LinkInformation>();
            }
            catch (Exception ex)
            {
                _log.Warning(ex, $"{sessionId} {ex.Message}");
                return new List<LinkInformation>();
            }
        }

        private List<LinkImageInformation> GetImgLinkInformations(
            string sessionId,
            List<IWebElement> elements,
            bool isNavLink = false)
        {
            return elements
                .Select(a => new
                {
                    //Href = a.GetAttribute("img"),
                    Src = a.GetAttribute("src"),
                    Alt = a.GetAttribute("alt"),
                    Text = a.GetText(_log),
                    InnerHtml = a.GetText(_log),
                })
                .Where(a => !string.IsNullOrWhiteSpace(a.Src) && !string.IsNullOrWhiteSpace(a.Alt))
                .Select(a => new LinkImageInformation
                {
                    Href = a.Src,
                    Title = a.Alt,
                    Src = a.Src,
                    Alt = a.Alt,
                    InnerHtml = a.InnerHtml,
                    Url = new Uri(a.Src),
                    IsNavLink = isNavLink
                }).ToList();
        }
        private List<LinkImageInformation> GetImgLinkInformationsSafe(
            string sessionId,
            List<IWebElement> elements,
            bool isNavLink = false)
        {
            try
            {
                var links = GetImgLinkInformations(sessionId, elements, isNavLink).ToList();
                return links;
            }
            catch (StaleElementReferenceException srex)
            {
                _log.Warning(srex, $"{sessionId} {srex.Message}");
                return new List<LinkImageInformation>();
            }
            catch (Exception ex)
            {
                _log.Warning(ex, $"{sessionId} {ex.Message}");
                return new List<LinkImageInformation>();
            }
        }
    }
}