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

using Universe.Lastfm.Api.Infrastracture;
using Universe.Lastfm.Api.Models;
using Universe.Windows.Forms.Controls.Settings;

namespace Universe.Lastfm.Api.FormsApp.Settings
{
    /// <summary>
    ///     Настройки приложения
    /// <author>Alex Universe</author>
    /// <author>Alex Envision</author>
    /// </summary>
    public class UniverseLastApiAppSettings : FormAppSettings, IUniverseLastApiSettings
    {
        public bool NeedApprovement { get; set; }

        public string ApiKey { get; set; }

        public string SecretKey { get; set; }

        public bool IsMaximized { get; set; }

        public bool IsTrustedApiApp { get; set; }

        public bool IsFullRubberUI { get; set; }

        public bool IsSpaceMode { get; set; }

        public string WebDriverExecutableFilePath { get; set; }

        public ReqContext ReqCtx { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public UniverseLastApiAppSettings()
        {
            ReqCtx = new ReqContext();
        }

        public string GetUniverseDbConnectionString()
        {
            return string.Empty;
        }
    }
}