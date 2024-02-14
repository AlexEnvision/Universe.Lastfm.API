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
using System.Diagnostics;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Universe.Helpers.Extensions;
using Universe.Lastfm.Api.Infrastracture;

namespace Universe.Lastfm.Api.Algorithm
{
    /// <summary>
    /// <author>Alex Universe</author>
    /// <author>Alex Envision</author>
    /// </summary>
    internal class OpenBrowserEngine
    {
        private readonly IUniverseLastApiSettings _settings;

        protected string SessionId;

        private IWebDriver _driver;

        public IntPtr CurrentBrowserHwnd { get; set; }

        public int CurrentBrowserPid { get; set; }

        public int ChromeDriverPid { get; set; }

        public int MinWaitPause { get; set; }

        public OpenBrowserEngine(IUniverseLastApiSettings settings)
        {
            _settings = settings;
            SessionId = Guid.NewGuid().ToString();

            MinWaitPause = 250;
        }

        protected virtual IWebDriver GetWebDriver(OpenBrowserParameters searchParameters)
        {
            var driverId = @"""" + SessionId.Replace(" ", "-") + @"""";

            var chromeOptions = new ChromeOptions
            {
                AcceptInsecureCertificates = true
            };
            //chromeOptions.AddArguments("--window-size=1028,655");
            chromeOptions.AddArguments("--window-size=1152,720");
            chromeOptions.AddArgument("--allow-file-access-from-files");
            chromeOptions.AddArgument("--enable-file-cookies");
            chromeOptions.AddArgument("--ignore-certificate-errors");
            chromeOptions.AddArgument("--ignore-ssl-errors");
            chromeOptions.AddArgument("--disable-popup-blocking");
            chromeOptions.AddArguments("--disable-notifications"); // to disable notification

            chromeOptions.AddArguments("--headless");

            // Разрешить режим инкогнито
            //chromeOptions.AddArgument("--incognito");

            chromeOptions.AddArgument("scriptpid-" + driverId);

            // Отключаем использование GPU
            //chromeOptions.AddArguments("--disable-gpu");

            // Отключаем всякие расширения браузера
            //chromeOptions.AddArguments("--disable-extensions");

            // Дополнительные арргументы - призваны ускорить работу радикально
            // Но использовать осторожно!
            //chromeOptions.AddArguments("--no-sandbox");
            //chromeOptions.AddArguments("--disable-dev-shm-usage");
            //chromeOptions.AddArguments("--aggressive-cache-discard");
            //chromeOptions.AddArguments("--disable-cache");
            //chromeOptions.AddArguments("--disable-application-cache");
            //chromeOptions.AddArguments("--disable-offline-load-stale-cache");
            //chromeOptions.AddArguments("--disk-cache-size=0");
            //chromeOptions.AddArguments("--headless");
            //chromeOptions.AddArguments("--dns-prefetch-disable");
            //chromeOptions.AddArguments("--no-proxy-server");
            //chromeOptions.AddArguments("--log-level=3"); 
            //chromeOptions.AddArguments("--silent"); 
            //chromeOptions.AddArguments("--disable-browser-side-navigation");

            // Отключаем кэши
            //chromeOptions.AddArguments("--disable-cache");
            //chromeOptions.AddArguments("--disable-application-cache");
            //chromeOptions.AddArguments("--disable-offline-load-stale-cache");
            //chromeOptions.AddArguments("--disk-cache-size=1");

            //if (!_settings.GoogleChromeCachePath.IsNullOrWhiteSpace())
            //    chromeOptions.AddArguments($@"--user-data-dir=""{_settings.GoogleChromeCachePath}""");

            var webDriverExecutableFilePath = _settings.WebDriverExecutableFilePath;
            if (webDriverExecutableFilePath.IsNullOrWhiteSpace())
                throw new Exception($"{SessionId} Не указан путь к драйверу браузера!");

            var service = ChromeDriverService.CreateDefaultService(webDriverExecutableFilePath);
            service.HideCommandPromptWindow = true;
            ChromeDriverPid = service.ProcessId;

            var webDriver = new ChromeDriver(
                service,
                chromeOptions,
                new TimeSpan(0, 30, 0));

            CompleteBrowserPid(driverId, searchParameters, ChromeDriverPid);

            return webDriver;
        }

        /// <summary>
        ///     Заполняет PID связанного с драйвером браузера
        /// </summary>
        /// <param name="driverId">Идентификатор драйвера, основанный на идентификаторе сессии</param>
        /// /// <param name="searchParameters">Параметры</param>
        /// <param name="chromeDriverPid"></param>
        private void CompleteBrowserPid(string driverId, OpenBrowserParameters searchParameters, int chromeDriverPid)
        {
            var processes = Process.GetProcessesByName("chrome");
            foreach (var process in processes)
            {
                //var commandLineSearcher =
                //    new ManagementObjectSearcher("SELECT CommandLine FROM Win32_Process WHERE ProcessId = " +
                //                                 process.Id);
                //var commandLine = "";
                //foreach (var commandLineObject in commandLineSearcher.Get())
                //    commandLine += commandLineObject["CommandLine"] as string;

                //var scriptPidStr = new Regex("--scriptpid-(.+?) ").Match(commandLine).Groups[1].Value;
                //if (!scriptPidStr.IsNullOrEmpty() && scriptPidStr.PrepareToCompare() == driverId.PrepareToCompare())
                //{
                //    CurrentBrowserPid = process.Id;
                //    CurrentBrowserHwnd = process.MainWindowHandle;
                //    break;
                //}
            }
        }

        /// <summary>
        ///     Запуск браузера
        /// </summary>
        /// <param name="parameters">Параметры</param>
        /// <returns></returns>
        public void Run(OpenBrowserParameters parameters)
        {
            using (_driver = GetWebDriver(parameters))
            {
                _driver.Navigate().GoToUrl(parameters.SiteNav);

                IWebElement inputUser = _driver.FindElement(By.Name("username_or_email"));
                Thread.Sleep(MinWaitPause);
                inputUser.SendKeys(_settings.Login);

                IWebElement inputPass = _driver.FindElement(By.Name("password"));
                Thread.Sleep(MinWaitPause);
                inputPass.SendKeys(_settings.Password);

                IWebElement inputSubmit = _driver.FindElement(By.Name("submit"));
                inputSubmit.Click();
                Thread.Sleep(MinWaitPause);

                Thread.Sleep(3 * MinWaitPause);

                IWebElement inputConfirm = _driver.FindElement(By.Name("confirm"));
                inputConfirm.Click();
                Thread.Sleep(MinWaitPause);

                Thread.Sleep(4 * MinWaitPause);
            }
        }
    }

    public class OpenBrowserParameters
    {
        public string SiteNav { get; set;}
    }
}