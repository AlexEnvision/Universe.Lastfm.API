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
using Universe.Lastfm.Api.Helpers;
using Universe.Lastfm.Api.IO.Validators;
using Universe.Lastfm.Api.Models;
using Universe.Lastfm.Api.Models.Base;
using Universe.Lastfm.Api.Models.Res;

namespace Universe.Lastfm.Api.Adapters
{
    public class LastAdapter : Adapter
    {   
        public LastfmSessionContext LastfmSessionContext { get; protected set; }

        private readonly string _mySecret;

        public LastAdapter(string apiKey, string mySecret) : base("http://ws.audioscrobbler.com/2.0/", apiKey)
        {
            _mySecret = mySecret;
            LastfmSessionContext = new LastfmSessionContext{
                ApiKey = apiKey
            };
        }

        protected AllowAccessResponce CreateAllowAccessRequest(bool isTrustedApp = false)
        {
            var responce = CreateGetRequest("auth.gettoken");
            string tokenResult = responce.ServiceAnswer;

            // извлекаем то, что нам нужно. Можно сделать и через парсинг XML (видимо, я о нём ещё не знал в тот момент, когда писал этот код).
            string token = String.Empty;
            for (int i = tokenResult.IndexOf("<token>", StringComparison.Ordinal) + 7; i < tokenResult.IndexOf("</token", StringComparison.Ordinal); i++)
            {
                token += tokenResult[i];
            }

            if (!isTrustedApp)
            {
                // запускаем в браузере по умолчанию страницу http://www.last.fm/api/auth/ c параметрами API Key и только что полученным токеном)
                //Process.Start("http://www.last.fm/api/auth/?api_key=" + ApiKey + "&token=" + token);
                var authurl = "http://www.last.fm/api/auth/?api_key=" + ApiKey + "&token=" + token;
                authurl.OpenUrl();
            }

            var allowAccessResponce = ResponceExt.CreateFrom<BaseResponce, AllowAccessResponce>(responce);
            allowAccessResponce.Token = token;
            LastfmSessionContext.Token = token;

            return allowAccessResponce;
        }

        protected AuthResponce CreateAuthRequest(AllowAccessResponce accessResponce)
        {
            string token = accessResponce.Token;
         
            // создаём сигнатуру для получения сессии (указываем API Key, метод, токен и наш секретный ключ, всё это без символов '&' и '='
            string tmp = "api_key" + ApiKey + "methodauth.getsessiontoken" + token + _mySecret;

            // хешируем это алгоритмом MD5
            var md5Validator = new Md5Validator();
            var sig = md5Validator.Hash(tmp);

            // получаем сессию похожим способом
            var sessionResponce = CreateGetRequest("auth.getsession", true, Argument.Create("token", token), Argument.Create("api_key", ApiKey), Argument.Create("api_sig", sig));
            // получаем ответ
            var sessionResult = sessionResponce.ServiceAnswer;

            var sessionKey = string.Empty;
            // извлечение сессии (опять же проще использовать XML парсер)
            for (int i = sessionResult.IndexOf("<key>", StringComparison.Ordinal) + 5; i < sessionResult.IndexOf("</key>", StringComparison.Ordinal); i++)
            {
                sessionKey += sessionResult[i];
            }

            var authResponce = ResponceExt.CreateFrom<BaseResponce, AuthResponce>(sessionResponce);
            authResponce.SessionKey = sessionKey;
            LastfmSessionContext.SessionKey = sessionKey;

            return authResponce;
        }

        public void FixCallback(BaseResponce sessionResponce)
        {
            var recrutchCallBackResponce = sessionResponce.ServiceAnswer;
            if (!string.IsNullOrEmpty(recrutchCallBackResponce))
            {
                recrutchCallBackResponce = recrutchCallBackResponce.Remove(0, 2);
                recrutchCallBackResponce = recrutchCallBackResponce.Remove(recrutchCallBackResponce.Length - 2, 2);
                sessionResponce.ServiceAnswer = recrutchCallBackResponce;
            }
        }

        public AllowAccessResponce RequestAccess(bool isTrustedApp = false)
        {
            return CreateAllowAccessRequest(isTrustedApp);
        }

        public AuthResponce Auth(AllowAccessResponce accessResponce)
        {
            return CreateAuthRequest(accessResponce);
        }

        public BaseResponce GetRequest(string method, params Argument[] arguments)
        {
            return CreateGetRequest(method, arguments);
        }

        public BaseResponce PostRequest(string method, string submissionReqString)
        {
            return CreatePostRequest(method, submissionReqString);
        }
    }
}