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
using System.Web;
using Universe.Lastfm.Api.IO.Validators;
using Universe.Lastfm.Api.Models.Base;

namespace Universe.Lastfm.Api.Dal.Command.Scrobble
{
    public class ScrobbleRequestCommand : LastCommand
    {
        public BaseResponce Execute(string sessionKey, string artist, string track, string album)
        {
            // узнаем UNIX-время для текущего момента
            TimeSpan rtime = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0);
            TimeSpan t1 = new TimeSpan(3, 0, 0);
            rtime -= t1; // вычитаем три часа, чтобы не было несоответствия из-за разницы в часовых поясах
                         // получаем количество секунд
            int timestamp = (int)rtime.TotalSeconds;

            // формируем строку запроса
            string submissionReqString = string.Empty;

            //добавляем параметры (указываем метод, сессию и API Key):
            submissionReqString += "method=track.scrobble&sk=" + sessionKey + "&api_key=" + Settings.ApiKey;

            // добавляем только обязательную информацию о треке (исполнитель, трек, время прослушивания, альбом), кодируя их с помощью статического метода UrlEncode класса HttpUtility.
            submissionReqString += "&artist=" + HttpUtility.UrlEncode(artist);
            submissionReqString += "&track=" + HttpUtility.UrlEncode(track);
            submissionReqString += "& timestamp=" + timestamp; // в этой строке не должно быть пробела между & и t. Просто почему-то Хабр неправильно отображает этот участок, если пробел убрать.
            submissionReqString += "&album=" + HttpUtility.UrlEncode(album);

            // формируем сигнатуру (параметры должны записываться сплошняком (без символов '&' и '=' и в алфавитном порядке):
            string signature = string.Empty;

            // сначала добавляем альбом
            signature += "album" + album;

            // потом API Key
            signature += "api_key" + Settings.ApiKey;

            // исполнитель		   
            signature += "artist" + artist;

            // метод и ключ сессии
            signature += "methodtrack.scrobblesk" + sessionKey;

            // время
            signature += "timestamp" + timestamp;

            // имя трека
            signature += "track" + track;

            // добавляем секретный код в конец
            signature += Settings.SecretKey;

            // добавляем сформированную и захешированную MD5 сигнатуру к строке запроса
            var md5Validator = new Md5Validator();
            submissionReqString += "&api_sig=" + md5Validator.Hash(signature);

            // и на этот раз делаем POST запрос на нужную страницу
            var result = Adapter.PostRequest(submissionReqString);

            // считываем поток ответа
            string submissionResult = result.ServiceAnswer;

            // разбор полётов. Если ответ не содержит status="ok", то дело плохо, выкидываем Exception и где-нибудь ловим его.
            if (!submissionResult.Contains("status=\"ok\""))
                throw new Exception("Треки не отправлены! Причина - " + submissionResult);

            // иначе всё хорошо, выходим из метода и оповещаем пользователя, что трек заскробблен.
            return new BaseResponce
            {
                Message = "Трэк заскробблен!",
                IsSuccessful = true,
                ServiceAnswer = submissionResult
            };
        }
    }
}