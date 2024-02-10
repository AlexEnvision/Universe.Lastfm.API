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
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using Universe.Diagnostic.Logger;

namespace Universe.Lastfm.Api.Algorithm.Helpers
{
    /// <summary>
    ///     Расширения WebDriver'а
    /// </summary>
    public static class WebDriverExtensions
    {
        /// <summary>
        ///     Перемотка страницы вниз
        /// </summary>
        /// <param name="driver">Драйвер</param>
        /// <param name="position">Позиция в пикселях</param>
        /// <param name="step">Шаг в пикселях (Размер страницы)</param>
        /// <returns></returns>
        public static int ScrollPageDown(this IWebDriver driver, int position, int step = 655)
        {
            var js = (IJavaScriptExecutor)driver;
            try
            {
                var currentPosition = position + step;
                js.ExecuteScript($"window.scrollTo({0}, {currentPosition})");
                return position;
            }
            catch (Exception)
            {
                return position;
            }
        }

        /// <summary>
        ///     Перемотка страницы вверх
        /// </summary>
        /// <param name="driver">Драйвер</param>
        /// <param name="position">Позиция в пикселях</param>
        /// <param name="step">Шаг в пикселях (Размер страницы)</param>
        /// <returns></returns>
        public static int ScrollPageUp(this IWebDriver driver, int position, int step = 655)
        {
            var js = (IJavaScriptExecutor)driver;
            try
            {
                var currentPosition = position - step;

                if (currentPosition < 0)
                    currentPosition = 0;

                js.ExecuteScript($"window.scrollTo({0}, {currentPosition})");
                return position;
            }
            catch (Exception)
            {
                return position;
            }
        }

        /// <summary>
        ///     Поиск HTML елемента по тексту
        /// </summary>
        /// <param name="driver">Драйвер</param>
        /// <param name="text">Текст</param>
        /// <returns></returns>
        public static IWebElement FindElementByText(this IWebDriver driver, string text)
        {
            try
            {
                var el = driver.FindElement(By.XPath($"//*[contains(text(), '{text}')]"));
                return el;
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }

        /// <summary>
        ///     Скролл страницы к HTML елементу
        /// </summary>
        /// <param name="driver">Драйвер</param>
        /// <param name="element">HTML елемент</param>
        public static void MoveToElement(this IWebDriver driver, IWebElement element)
        {
            var actions = new OpenQA.Selenium.Interactions.Actions(driver);
            actions.MoveToElement(element);
            actions.Perform();
        }

        /// <summary>
        ///     Получение текста (без разметки) HTML страницы
        /// </summary>
        /// <param name="driver">Драйвер</param>
        /// <param name="log">Лог</param>
        /// <returns></returns>
        public static string GetPageText(this IWebDriver driver, IUniverseLogger log = null)
        {
            try
            {
                var el = driver.FindElement(By.CssSelector("body"));
                return el.GetText(log);
            }
            catch (NoSuchElementException)
            {
                return null;
            }            
        }

        /// <summary>
        ///     Функция с жаргонным названием "Семь футов под заголовком сайта"
        ///     Задача функции: пролистывание сайта вниз.
        /// </summary>
        /// <param name="driver">Драйвер</param>
        /// <param name="steps">Число шагов вниз от заголовка страницы</param>
        /// <returns></returns>
        public static void SevenFeetDownUnderSiteHeader(this IWebDriver driver, int steps = 7)
        {
            var stepSize = 1080;
            var location = 0;

            for (int i = 0; i < steps; i++)
            {
                driver.ScrollPageDown(location, stepSize);
                location += stepSize;
            }
        }

        /// <summary>
        ///     Безопасный поиск элементов с помощью веб-драйвера
        /// </summary>
        /// <param name="driver">Веб-драйвер</param>
        /// <param name="log">Логгер</param>
        /// <param name="sessionId">Идентификатор сеанса связи</param>
        /// <param name="xPathPattern">Паттерн</param>
        /// <returns></returns>
        public static List<IWebElement> FindElementsByXPathSafe(this IWebDriver driver, IUniverseLogger log, string sessionId, string xPathPattern)
        {
            try
            {
                return driver.FindElements(By.XPath(xPathPattern)).ToList();
            }
            catch (Exception ex)
            {
                log.Warning(ex, $"{sessionId} {ex.Message}");
                return new List<IWebElement>();
            }
        }

        /// <summary>
        ///     Безопасный поиск элементов с помощью веб-драйвера
        /// </summary>
        /// <param name="driver">Веб-драйвер</param>
        /// <param name="log">Логгер</param>
        /// <param name="xPathPattern">Паттерн</param>
        /// <returns></returns>
        public static List<IWebElement> FindElementsByXPathSafe(this IWebDriver driver, IUniverseLogger log, string xPathPattern)
        {
            try
            {
                return driver.FindElements(By.XPath(xPathPattern)).ToList();
            }
            catch (Exception ex)
            {
                log.Warning(ex, $"{ex.Message}");
                return new List<IWebElement>();
            }
        }

        /// <summary>
        ///     Безопасный поиск элементов с помощью веб-драйвера
        /// </summary>
        /// <param name="driver">Веб-драйвер</param>
        /// <param name="log">Логгер</param>
        /// <param name="sessionId">Идентификатор сеанса связи</param>
        /// <param name="linkText">Текст</param>
        /// <returns></returns>
        public static List<IWebElement> FindElementsByLinkTextSafe(this IWebDriver driver, IUniverseLogger log, string sessionId, string linkText)
        {
            try
            {
                return driver.FindElements(By.LinkText(linkText)).ToList();
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
        /// <param name="driver">Веб-драйвер</param>
        /// <param name="log">Логгер</param>
        /// <param name="sessionId">Идентификатор сеанса связи</param>
        /// <param name="cssPattern">Паттерн</param>
        /// <returns></returns>
        public static IWebElement FindElementByCssSafe(this IWebDriver driver, IUniverseLogger log, string sessionId, string cssPattern)
        {
            try
            {
                return driver.FindElement(By.CssSelector(cssPattern));
            }
            catch (Exception ex)
            {
                log.Warning(ex, $"{sessionId} {ex.Message}");
                return null;
            }
        }

        /// <summary>
        ///     Безопасный поиск элементов в веб-элементе
        /// </summary>
        /// <param name="driver">Веб-драйвер</param>
        /// <param name="log">Логгер</param>
        /// <param name="cssPattern">Паттерн</param>
        /// <returns></returns>
        public static IWebElement FindElementByCssSafe(this IWebDriver driver, IUniverseLogger log, string cssPattern)
        {
            try
            {
                return driver.FindElement(By.CssSelector(cssPattern));
            }
            catch (Exception ex)
            {
                log.Warning(ex, $"{ex.Message}");
                return null;
            }
        }

        /// <summary>
        ///     Безопасный поиск элементов в веб-элементе
        /// </summary>
        /// <param name="driver">Веб-драйвер</param>
        /// <param name="log">Логгер</param>
        /// <param name="sessionId">Идентификатор сеанса связи</param>
        /// <param name="cssPattern">Паттерн</param>
        /// <returns></returns>
        public static List<IWebElement> FindElementsByCssSafe(this IWebDriver driver, IUniverseLogger log, string sessionId, string cssPattern)
        {
            try
            {
                return driver.FindElements(By.CssSelector(cssPattern)).ToList();
            }
            catch (Exception ex)
            {
                log.Warning(ex, $"{sessionId} {ex.Message}");
                return new List<IWebElement>();
            }
        }

        /// <summary>
        ///     Безопасный получение URL веб-драйвера
        /// </summary>
        /// <param name="driver">Веб-драйвер</param>
        /// <param name="log">Логгер</param>
        /// <param name="sessionId">Идентификатор сеанса связи</param>
        /// <returns></returns>
        public static string GetUrlSafe(this IWebDriver driver, IUniverseLogger log, string sessionId)
        {
            try
            {
                return driver.Url;
            }
            catch (Exception ex)
            {
                log.Warning(ex, $"{sessionId} {ex.Message}");
                return string.Empty;
            }
        }

        /// <summary>
        ///     Получение веб-элементов посредством регулярных выражений
        /// </summary>
        /// <param name="driver">Веб-драйвер</param>
        /// <param name="attrName">Название атрибута</param>
        /// <param name="attrRegex">Регулярное выражение</param>
        /// <returns></returns>
        public static List<IWebElement> SearchByAttributeWithRegex(IWebDriver driver, string attrName, string attrRegex)
        {
            List<IWebElement> elements = new List<IWebElement>();

            //Allows spaces around equal sign. Ex: id = 55
            string searchString = attrName + "\\s*=\\s*\"" + attrRegex + "\"";
            //Search page source
            var pageSource = driver.PageSource;
            MatchCollection matches = Regex.Matches(pageSource, searchString, RegexOptions.IgnoreCase);
            //iterate over matches
            foreach (Match match in matches)
            {
                //Get exact attribute value
                Match innerMatch = Regex.Match(match.Value, attrRegex);
                var cssSelector = "[" + attrName + "=" + attrRegex + "]";
                //Find element by exact attribute value
                elements.Add(driver.FindElement(By.CssSelector(cssSelector)));
            }

            return elements;
        }

        /// <summary>
        ///     Получение веб-элементов посредством регулярных выражений
        /// </summary>
        /// <param name="driver">Веб-драйвер</param>
        /// <param name="log">Логгер</param>
        /// <param name="sessionId">Идентификатор сеанса связи</param>
        /// <param name="attrName">Название атрибута</param>
        /// <param name="attrRegex">Регулярное выражение</param>
        /// <returns></returns>
        public static List<IWebElement> SearchByAttributeWithRegexSafe(this IWebDriver driver, IUniverseLogger log, string sessionId, string attrName, string attrRegex)
        {
            try
            {
                return SearchByAttributeWithRegex(driver, attrName, attrRegex);
            }
            catch (Exception ex)
            {
                log.Warning(ex, $"{sessionId} {ex.Message}");
                return new List<IWebElement>();
            }
        }

        /// <summary>
        ///     Получение веб-элементов посредством регулярных выражений
        /// </summary>
        /// <param name="driver">Веб-драйвер</param>
        /// <param name="log">Логгер</param>
        /// <param name="attrName">Название атрибута</param>
        /// <param name="attrRegex">Регулярное выражение</param>
        /// <returns></returns>
        public static List<IWebElement> SearchByAttributeWithRegexSafe(this IWebDriver driver, IUniverseLogger log, string attrName, string attrRegex)
        {
            try
            {
                return SearchByAttributeWithRegex(driver, attrName, attrRegex);
            }
            catch (Exception ex)
            {
                log.Warning(ex, $"{ex.Message}");
                return new List<IWebElement>();
            }
        }
    }
}