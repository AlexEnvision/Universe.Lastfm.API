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
using Universe.Lastfm.Api.Dal.Queries.Hash;
using Universe.Lastfm.Api.Helpers;

namespace Universe.Lastfm.Api.Dal.Queries.ApiConnect
{
    public class GetConnectQuery : LastQuery
    {
        private string _token;
        private string _sessionKey;

        public void Connect(bool needApprovement)
        {
            SetToken(Settings.ApiKey);

            WebRequest request = WebRequest.Create("http://ws.audioscrobbler.com/2.0/?method=auth.gettoken&api_key=" + Settings.ApiKey);
            //Process s = Process.Start("http://www.last.fm/api/auth/?api_key=" + _api_key + "&token=" + _token);

            if (needApprovement)
            {
                var url = "http://www.last.fm/api/auth/?api_key=" + Settings.ApiKey + "&token=" + _token;
                url.OpenUrl();
                //После этого пользователь должен потвердить согласие
            }
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

        //На всякий случай
        private void SetSessionKey(string apiKey, string secretKey, string token)
        {
            string sessionKey = "";
            string tmp = "api_key" + apiKey + "methodauth.getsessiontoken" + token + secretKey;
            MD5 md5Hash = MD5.Create();
            Md5HashQuery getMd5Hash = new Md5HashQuery();
            string sig = getMd5Hash.Execute(md5Hash, tmp);

            WebRequest sessionRequest = WebRequest.Create("http://ws.audioscrobbler.com/2.0/?method=auth.getsession&token=" + token + "&api_key=" + apiKey + "&api_sig=" + sig);
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