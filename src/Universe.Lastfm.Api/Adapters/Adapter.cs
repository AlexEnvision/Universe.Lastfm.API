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
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Universe.Lastfm.Api.Helpers;
using Universe.Lastfm.Api.Models;
using Universe.Lastfm.Api.Models.Base;

namespace Universe.Lastfm.Api.Adapters
{
    public class Adapter
    {
        protected readonly string BaseAdress;

        protected readonly string ApiKey;

        protected string ResponseFormat { get; set; }

        protected Adapter(string baseAdress, string apiKey)
        {
            ApiKey = apiKey;
            BaseAdress = baseAdress;

            ResponseFormat = "json";
        }

        protected BaseResponce CreateGetRequest(string method)
        {
            // создаём объект HttpWebRequest через статический метод Create класса WebRequest, явно приводим результат к HttpWebRequest. В параметрах указываем страницу, которая указана в API, в качестве параметров - method=auth.gettoken и наш API Key
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($@"{BaseAdress}?method={method}&api_key={ApiKey}");

            // получаем ответ сервера
            var response = (HttpWebResponse)request.GetResponse();

            // и полностью считываем его в строку
            string result = new StreamReader(response.GetResponseStream(), Encoding.UTF8).ReadToEnd();
         
            return new BaseResponce {
                ServiceAnswer = result,
                IsSuccessful = true
            };
        }

        protected BaseResponce CreateGetRequest(string method, params Argument[] arguments)
        {
            return CreateGetRequest(method, false, arguments);
        }

        protected BaseResponce CreateGetRequest(string method, bool allowAutoRedirect, params Argument[] arguments)
        {
            // создаём объект HttpWebRequest через статический метод Create класса WebRequest, явно приводим результат к HttpWebRequest. В параметрах указываем страницу, которая указана в API, в качестве параметров - method=auth.gettoken и наш API Key
            var reqUrl = $@"{BaseAdress}?method={method}";
            StringBuilder sb = new StringBuilder();
            sb.Append(reqUrl);
            foreach (var argument in arguments)
            {
                if (argument != null)
                    sb.Append($"&{argument.ArgumentName}={argument.ArgumentValue}");
            }
            reqUrl = sb.ToString();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(reqUrl);

            if (allowAutoRedirect)
               request.AllowAutoRedirect = true;

            // получаем ответ сервера
            var response = (HttpWebResponse)request.GetResponse();

            // и полностью считываем его в строку
            string result = new StreamReader(response.GetResponseStream(), Encoding.UTF8).ReadToEnd();

            return new BaseResponce{
                ServiceAnswer = result,
                IsSuccessful = true
            };
        }

        protected BaseResponce CreatePostRequest(string submissionReqString)
        {
            HttpWebRequest submissionRequest = (HttpWebRequest)WebRequest.Create(BaseAdress); // адрес запроса без параметров

            // очень важная строка. Долго я мучался, пока не выяснил, что она обязательно должна быть
            submissionRequest.ServicePoint.Expect100Continue = false;

            // Настраиваем параметры запроса
            submissionRequest.UserAgent = "Mozilla/5.0";
            // Указываем метод отправки данных скрипту, в случае с POST обязательно
            submissionRequest.Method = "POST";
            // В случае с POST обязательная строка
            submissionRequest.ContentType = "application/x-www-form-urlencoded";

            // ставим таймаут, чтобы программа не повисла при неудаче обращения к серверу, а выкинула Exception
            submissionRequest.Timeout = 60000;

            // Преобразуем данные в соответствующую кодировку, получаем массив байтов из строки с параметрами (UTF8 обязательно)
            byte[] encodedPostParams = Encoding.UTF8.GetBytes(submissionReqString);
            submissionRequest.ContentLength = encodedPostParams.Length;

            // Записываем данные в поток запроса (массив байтов, откуда начинаем, сколько записываем)
            submissionRequest.GetRequestStream().Write(encodedPostParams, 0, encodedPostParams.Length);
            // закрываем поток
            submissionRequest.GetRequestStream().Close();

            // получаем ответ сервера
            HttpWebResponse submissionResponse = (HttpWebResponse)submissionRequest.GetResponse();

            // считываем поток ответа
            string submissionResult = new StreamReader(submissionResponse.GetResponseStream(), Encoding.UTF8).ReadToEnd();

            return new BaseResponce {
                ServiceAnswer = submissionResult,
                IsSuccessful = true
            };
        }

        protected BaseResponce CreatePostRequest(
            string method,
            string secretKey,
            params Argument[] arguments)
        {
            var kvps = arguments.Select(x => new KeyValuePair<string, string>(x.ArgumentName, x.ArgumentValue)).ToArray();
            return CreatePostRequestAsync(method, secretKey, kvps).Result;
        }

        protected async Task<BaseResponce> CreatePostRequestAsync(
            string method,
            string secretKey,
            params KeyValuePair<string, string>[] arguments)
        {
            HttpClient httpClient = new HttpClient();

            // очень важная строка. Долго я мучался, пока не выяснил, что она обязательно должна быть
            httpClient.DefaultRequestHeaders.ExpectContinue = false;

            var apikey = ApiKey;
            string apisig = GenerateMethodSignature(method,
                secretKey, 
                arguments.GroupBy(x => x.Key)
                    .ToDictionary(x => x.Key, x => x.ToList().FirstOrDefault().Value));
            var postContent = CreatePostBody(method, apikey, apisig, arguments);

            LastResponse result;
            using (var response = await httpClient.PostAsync(BaseAdress, postContent))
            {
                result = await LastResponse.HandleResponse(response);
            }

            return new BaseResponce
            {
                Message = result.Message,
                ServiceAnswer = result.Status.ToString(),
                IsSuccessful = result.IsSuccessful
            };
        }

        protected FormUrlEncodedContent CreatePostBody(string method, string apikey, string apisig,
            IEnumerable<KeyValuePair<string, string>> parameters)
        {
            var init = new Dictionary<string, string>
            {
                {"method", method},
                {"api_key", apikey},
                {"api_sig", apisig},
                {"format", ResponseFormat}
            };

            var requestParameters = init.Concat(parameters);

            return new FormUrlEncodedContent(requestParameters);
        }

        public string GenerateMethodSignature(string method, 
            string secretKey,
            Dictionary<string, string> parameters = null)
        {
            if (parameters == null)
            {
                parameters = new Dictionary<string, string>();
            }

            if (!parameters.ContainsKey("api_key")) 
                parameters.Add("api_key", ApiKey);

            if (!parameters.ContainsKey("method"))
                parameters.Add("method", method);

            if (parameters.ContainsKey("api_sig"))
                parameters.Remove("api_sig");

            //if (Authenticated)
            //{
            //    parameters.Add("sk", UserSession.Token);
            //}

            var builder = new StringBuilder();

            foreach (var kv in parameters.OrderBy(kv => kv.Key, StringComparer.Ordinal))
            {
                builder.Append(kv.Key);
                builder.Append(kv.Value);
            }

            builder.Append(secretKey);

            var str = builder.ToString();
            //str = HttpUtility.UrlEncode(str);
            var md5 = MD5Helper.GetHashString(str);

            return md5;
        }

        public interface ILastResponse
        {
            bool IsSuccessful { get; }

            string Message { get; }

            LastResponseStatus Status { get; }
        }

        public class LastResponse : ILastResponse
        {
            public virtual bool IsSuccessful
            {
                get { return Status == LastResponseStatus.Successful; }
            }

            public string Message { get; protected set; }

            public LastResponseStatus Status { get; internal set; }

            [Obsolete("This property has been renamed to Status and will be removed soon.")]
            public LastResponseStatus Error { get { return Status; } }

            public static LastResponse CreateSuccessResponse(string message)
            {
                var r = new LastResponse
                {
                    Status = LastResponseStatus.Successful,
                    Message = message
                };

                return r;
            }

            public static T CreateErrorResponse<T>(LastResponseStatus status, string message) where T : LastResponse, new()
            {
                var r = new T
                {
                    Status = status,
                    Message = message
                };

                return r;
            }

            public static async Task<LastResponse> HandleResponse(HttpResponseMessage response)
            {
                var json = await response.Content.ReadAsStringAsync();

                LastResponseStatus status;
                if (IsResponseValid(json, out status) && response.IsSuccessStatusCode)
                {
                    return LastResponse.CreateSuccessResponse(json);
                }
                else
                {
                    return LastResponse.CreateErrorResponse<LastResponse>(status, json);
                }
            }

            public static bool IsResponseValid(string json, out LastResponseStatus status)
            {
                if (string.IsNullOrWhiteSpace(json))
                {
                    status = LastResponseStatus.Unknown;
                    return false;
                }

                JObject jo;
                try
                {
                    jo = JsonConvert.DeserializeObject<JObject>(json);
                }
                catch (JsonException)
                {
                    status = LastResponseStatus.Unknown;
                    return false;
                }

                var codeString = jo.Value<string>("error");
                if (string.IsNullOrWhiteSpace(codeString) && json.Length > 1)
                {
                    status = LastResponseStatus.Successful;
                    return true;
                }

                status = LastResponseStatus.Unknown;

                int code;
                if (Int32.TryParse(codeString, out code))
                {
                    status = (LastResponseStatus)code;
                }

                return false;
            }
        }

        public enum LastResponseStatus
        {
            Unknown = 0,

            /// <summary>
            /// The service requested does not exist (2)
            /// </summary>
            BadService = 2,

            /// <summary>
            /// The method requested does not exist in this service (3)
            /// </summary>
            BadMethod = 3,

            /// <summary>
            /// This credential does not have permission to access the service requested (4)
            /// </summary>
            BadAuth = 4,

            /// <summary>
            /// This service doesn't exist in the requested format
            /// </summary>
            BadFormat = 5,

            /// <summary>
            /// Required parameters were missing from the request (6)
            /// </summary>
            MissingParameters = 6,

            /// <summary>
            /// The requested resource is invalid (7)
            /// </summary>
            BadResource = 7,

            /// <summary>
            /// An unknown failure occured when creating the response (8)
            /// </summary>
            Failure = 8,

            /// <summary>
            /// The session has expired, reauthenticate before retrying (9)
            /// </summary>
            SessionExpired = 9,

            /// <summary>
            /// The provided API key was invalid (10)
            /// </summary>
            BadApiKey = 10,

            /// <summary>
            /// This service is temporarily offline, retry later (11)
            /// </summary>
            ServiceDown = 11,

            /// <summary>
            /// The request signature was invalid. Check that your API key and secret are valid. (13)
            /// You can generate new keys at https://www.last.fm/api/account/create
            /// </summary>
            BadMethodSignature = 13,

            /// <summary>
            /// There was a temporary error while processing the request, retry later (16)
            /// </summary>
            TemporaryFailure = 16,

            /// <summary>
            /// User required to be logged in. (17)
            /// Requested profile might not have privacy set to public.
            /// </summary>
            LoginRequired = 17,

            /// <summary>
            /// The request was successful!
            /// </summary>
            Successful = 20,

            /// <summary>
            /// The request has been cached, it will be sent later
            /// </summary>
            Cached = 21,

            /// <summary>
            /// The request could not be sent, and could not be cached.
            /// Check the Exception property of the response for details.
            /// </summary>
            CacheFailed = 22,

            /// <summary>
            /// The request failed, check for network connectivity
            /// </summary>
            RequestFailed = 23,

            /// <summary>
            /// This API key has been suspended, please generate a new key at http://www.last.fm/api/accounts (26)
            /// </summary>
            KeySuspended = 26,

            /// <summary>
            /// This API key has been rate-limited because too many requests have been made in a short period. Retry later (29)
            /// For more information on rate limits, please contact Last.FM at the partners@last.fm email address.
            /// </summary>
            RateLimited = 29
        }
    }
}
