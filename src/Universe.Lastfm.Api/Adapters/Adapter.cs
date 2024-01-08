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
using System.Text;
using Universe.Lastfm.Api.Models;
using Universe.Lastfm.Api.Models.Base;

namespace Universe.Lastfm.Api.Adapters
{
    public class Adapter
    {
        protected readonly string BaseAdress;

        protected readonly string ApiKey;

        protected Adapter(string baseAdress, string apiKey)
        {
            ApiKey = apiKey;
            BaseAdress = baseAdress;
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

        protected BaseResponce CreatePostRequest(string method, string submissionReqString)
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
            submissionRequest.Timeout = 6000;

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
    }
}
