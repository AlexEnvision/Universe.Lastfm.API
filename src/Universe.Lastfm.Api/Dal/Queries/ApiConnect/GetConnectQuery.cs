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

using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Universe.Helpers.Extensions;
using Universe.Lastfm.Api.Browser;
using Universe.Lastfm.Api.Dal.Queries.Hash;
using Universe.Lastfm.Api.Helpers;

namespace Universe.Lastfm.Api.Dal.Queries.ApiConnect
{
    /// <summary>
    /// <author>Alex Universe</author>
    /// <author>Alex Envision</author>
    /// </summary>
    public class GetConnectQuery : LastQuery
    {
        private string _token;
        private string _sessionKey;

        public int MinWaitPause { get; private set; }

        public int WaitingMultuplicator => 15;

        public void Connect(bool needApprovement)
        {
            SetToken(Settings.ApiKey);
            if (MinWaitPause == 0)
                SetMinPause(1000);

            //WebRequest request = WebRequest.Create("http://ws.audioscrobbler.com/2.0/?method=auth.gettoken&api_key=" + Settings.ApiKey);
            //Process s = Process.Start("http://www.last.fm/api/auth/?api_key=" + _api_key + "&token=" + _token);

            if (needApprovement)
            {
                //WebRequest request = WebRequest.Create("http://ws.audioscrobbler.com/2.0/?method=auth.gettoken&api_key=" + Settings.ApiKey);

                var url = "http://www.last.fm/api/auth/?api_key=" + Settings.ApiKey + "&token=" + _token;

                if (!Settings.WebDriverExecutableFilePath.IsNullOrEmpty() && 
                    !Settings.Login.IsNullOrEmpty() && 
                    !Settings.Password.IsNullOrEmpty())
                {
                    var engine = new OpenBrowserEngine(Settings);
                    engine.MinWaitPause = MinWaitPause;
                    engine.Run(new OpenBrowserParameters { SiteNav = url });
                }
                else
                {
                    url.OpenUrl();

                    //После этого пользователь должен потвердить согласие
                    //На это есть 15 секунд

                    Thread.Sleep(WaitingMultuplicator * MinWaitPause);
                }
            }
        }

        public void SetMinPause(int timeout)
        {
            MinWaitPause = timeout;
        }

        private void SetToken(string apiKey)
        {
            WebRequest request = WebRequest.Create("http://ws.audioscrobbler.com/2.0/?method=auth.gettoken&api_key=" + apiKey);
            WebResponse response = request.GetResponse();

            string tokenResult = new StreamReader(response.GetResponseStream(), Encoding.UTF8).ReadToEnd();
            string token = string.Empty;
            for (int i = tokenResult.IndexOf("<token>") + 7; i < tokenResult.IndexOf("</token"); i++)
            {
                token += tokenResult[i];
            }
            _token = token;
        }

        /// <summary>
        ///     Getting of session
        /// </summary>
        /// <param name="apiKey"></param>
        /// <param name="secretKey"></param>
        /// <param name="token"></param>
        public void SetSessionKey(string apiKey, string secretKey, string token)
        {
            string sessionKey = "";
            string tmp = "api_key" + apiKey + "methodauth.getsessiontoken" + token + secretKey;
            byte[] bytes = Encoding.Default.GetBytes(tmp);
            tmp = Encoding.UTF8.GetString(bytes);

            bytes = Encoding.Default.GetBytes(apiKey);
            apiKey = Encoding.UTF8.GetString(bytes);

            bytes = Encoding.Default.GetBytes(token);
            token = Encoding.UTF8.GetString(bytes);

            MD5 md5Hash = MD5.Create();
            Md5HashQuery getMd5Hash = new Md5HashQuery();
            string sig = getMd5Hash.Execute(md5Hash, tmp);

            WebRequest sessionRequest = WebRequest.Create("http://ws.audioscrobbler.com/2.0/?method=auth.getsession&api_key=" + apiKey+ "&token=" + token + "&api_sig=" + sig);  //&format=json"
            WebResponse sessionResponse = sessionRequest.GetResponse();
            string sessionResult = new StreamReader(sessionResponse.GetResponseStream(), Encoding.UTF8).ReadToEnd();

            for (int i = sessionResult.IndexOf("<key>") + 5; i < sessionResult.IndexOf("</key>"); i++)
            {
                sessionKey += sessionResult[i];
            }
            _sessionKey = sessionKey;
        }

        /// <summary>
        ///     Getting of session
        /// </summary>
        /// <param name="apiKey"></param>
        /// <param name="secretKey"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public void SetMobileSessionKey(
            string apiKey, 
            string secretKey,
            string username,
            string password)
        {
            string sessionKey = "";

            MD5 md5HashPass = MD5.Create();
            Md5HashQuery getMd5HashPass = new Md5HashQuery();
            string sigPassword = getMd5HashPass.Execute(md5HashPass, password);

            string tmp = "api_key" + apiKey + "methodauth.getMobileSession"  + username + sigPassword + secretKey;
            MD5 md5Hash = MD5.Create();
            Md5HashQuery getMd5Hash = new Md5HashQuery();
            string sig = getMd5Hash.Execute(md5Hash, tmp);

            WebRequest sessionRequest = WebRequest.Create("http://ws.audioscrobbler.com/2.0/?method=methodauth.getMobileSession&username=" + username + "&password=" + password + "&api_key=" + apiKey + "&api_sig=" + sig);
            sessionRequest.Headers["User-Agent"] = "Mozilla/5.0 (Linux; Android 10; K) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/118.0.0.0 Mobile Safari/537.36";

            WebResponse sessionResponse = sessionRequest.GetResponse();
            string sessionResult = new StreamReader(sessionResponse.GetResponseStream(), Encoding.UTF8).ReadToEnd();

            for (int i = sessionResult.IndexOf("<key>") + 5; i < sessionResult.IndexOf("</key>"); i++)
            {
                sessionKey += sessionResult[i];
            }
            _sessionKey = sessionKey;
        }

        public string GetApi()
        {
            return Settings.ApiKey;
        }

        public string GetSecretKey()
        {
            return Settings.SecretKey;
        }

        public string GetToken()
        {
            return _token;
        }

        public string GetSessionKey()
        {
            return _sessionKey;
        }
    }
}