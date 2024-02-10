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
using System.Drawing;
using System.Linq;
using OpenQA.Selenium;
using Universe.Diagnostic.Logger;
using Universe.Helpers.Extensions;

namespace Universe.Lastfm.Api.Algorithm.Helpers
{
    /// <summary>
    ///     Расширения для Веб-элементов
    /// </summary>
    public static class WebElementExtensions
    {
        /// <summary>
        ///     Безопасный поиск элементов в веб-элементе
        /// </summary>
        /// <param name="element">Веб-элемент</param>
        /// <param name="log">Логгер</param>
        /// <param name="sessionId">Идентификатор сеанса связи</param>
        /// <param name="xPathPattern">Паттерн</param>
        /// <returns></returns>
        public static List<IWebElement> FindElementsByXPathSafe(this IWebElement element, IUniverseLogger log, string sessionId, string xPathPattern)
        {
            if (element == null)
                return new List<IWebElement>();

            try
            {
                return element.FindElements(By.XPath(xPathPattern)).ToList();
            }
            catch (Exception ex)
            {
                log.Warning(ex, $"{sessionId} {ex.Message}");
                return new List<IWebElement>();
            }
        }

        /// <summary>
        ///     Безопасный поиск элементов в веб-элементе
        /// </summary>
        /// <param name="element">Веб-элемент</param>
        /// <param name="log">Логгер</param>
        /// <param name="xPathPattern">Паттерн</param>
        /// <returns></returns>
        public static List<IWebElement> FindElementsByXPathSafe(this IWebElement element, string xPathPattern, IUniverseLogger log = null)
        {
            if (element == null)
                return new List<IWebElement>();

            try
            {
                return element.FindElements(By.XPath(xPathPattern)).ToList();
            }
            catch (Exception ex)
            {
                log?.Warning(ex, $"{ex.Message}");
                return new List<IWebElement>();
            }
        }

        /// <summary>
        ///     Безопасный поиск элементов в веб-элементе
        /// </summary>
        /// <param name="element">Веб-элемент</param>
        /// <param name="log">Логгер</param>
        /// <param name="sessionId">Идентификатор сеанса связи</param>
        /// <param name="cssPattern">Паттерн</param>
        /// <returns></returns>
        public static List<IWebElement> FindElementsByCssSafe(this IWebElement element, IUniverseLogger log, string sessionId, string cssPattern)
        {
            if (element == null)
                return new List<IWebElement>();

            try
            {
                return element.FindElements(By.CssSelector(cssPattern)).ToList();
            }
            catch (Exception ex)
            {
                log.Warning(ex, $"{sessionId} {ex.Message}");
                return new List<IWebElement>();
            }
        }

        /// <summary>
        ///     Безопасный поиск элементов в веб-элементе
        /// </summary>
        /// <param name="element">Веб-элемент</param>
        /// <param name="log">Логгер</param>
        /// <param name="cssPattern">Паттерн</param>
        /// <returns></returns>
        public static IWebElement FindElementByCssSafe(this IWebElement element, string cssPattern, IUniverseLogger log = null)
        {
            try
            {
                return element.FindElement(By.CssSelector(cssPattern));
            }
            catch (Exception ex)
            {
                log?.Warning(ex, $"{ex.Message}");
                return null;
            }
        }

        ///// <summary>
        /////     Безопасное получение элементов на странице
        ///// </summary>
        ///// <param name="el">Элемент</param>
        ///// <param name="log">Лог</param>
        ///// <param name="sessionId">Идентификатор сеанса</param>
        ///// <returns></returns>
        //public static IList<WebElementNode> GetChildrenElementSafe(this WebElementNode el, IUniverseLogger log,
        //    string sessionId)
        //{
        //    if (el == null)
        //        return new List<WebElementNode>();

        //    try
        //    {
        //        return el.Children;
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Warning(ex, $"{sessionId} {ex.Message}");
        //        return new List<WebElementNode>();
        //    }
        //}

        /// <summary>
        ///     Получение текста элемента
        /// </summary>
        /// <param name="element">Элемент</param>
        /// <param name="log">Лог</param>
        /// <returns></returns>
        public static Point GetLocation(this IWebElement element, IUniverseLogger log = null)
        {
            var elPoint = new Point();
            try
            {
                elPoint = element.Location;
            }
            catch (StaleElementReferenceException)
            {
                log?.Warning($"{nameof(StaleElementReferenceException)} catched");
            }

            return elPoint;
        }

        /// <summary>
        ///     Получение текста элемента
        /// </summary>
        /// <param name="element">Элемент</param>
        /// <param name="log">Лог</param>
        /// <returns></returns>
        public static string GetText(this IWebElement element, IUniverseLogger log = null)
        {
            var elText = "";
            try
            {
                elText = element.Text;
            }

            catch (StaleElementReferenceException)
            {
                log?.Warning($"{nameof(StaleElementReferenceException)} catched");
            }

            try
            {
                if (elText.IsNullOrWhiteSpace())
                    elText = element.GetAttribute("innerText");
            }
            catch (StaleElementReferenceException)
            {
                log?.Warning($"{nameof(StaleElementReferenceException)} catched");
            }

            try
            {
                if (elText.IsNullOrWhiteSpace())
                {
                    elText = element.GetAttribute("innerHTML");

                    //// Очищаем от JSON в тексте
                    //elText = elText.CleanFromJson();

                    //// Очищаем от тегов
                    //elText = elText.CleanFromHtml();

                    //// И оставляем только русский язык
                    //elText = elText.FetchTextRus();
                }
            }
            catch (StaleElementReferenceException)
            {
                log?.Warning($"{nameof(StaleElementReferenceException)} catched");
            }

            return elText;
        }

      

        /// <summary>
        ///     Получение гипертекста элемента
        /// </summary>
        /// <param name="element">Элемент</param>
        /// <param name="log">Лог</param>
        /// <returns></returns>
        public static string GetHtml(this IWebElement element, IUniverseLogger log = null)
        {
            var elText = "";
            try
            {
                if (elText.IsNullOrEmpty())
                    elText = element.GetAttribute("innerHTML");
            }
            catch (StaleElementReferenceException)
            {
                log?.Warning($"{nameof(StaleElementReferenceException)} catched");
            }

            return elText;
        }

        /// <summary>
        ///     Получение текста элемента.
        ///     Небезопасный, использовать если точно уверены в обработке <see cref="StaleElementReferenceException"/>/
        /// </summary>
        /// <param name="element">Элемент</param>
        /// <returns></returns>
        public static string GetTextUnsafe(this IWebElement element)
        {
            string elText = element.Text;

            if (elText.IsNullOrEmpty())
                elText = element.GetAttribute("innerText");

            return elText;
        }
    }
}