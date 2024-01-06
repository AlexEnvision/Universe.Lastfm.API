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

using Universe.Lastfm.Api.Dto.Base;
using Universe.Lastfm.Api.Dto.Common;

namespace Universe.Lastfm.Api.Dto.GetUserInfo
{
    /// <summary>
    ///     The full information about user of the Last.fm.
    ///     Полная информация о пользователе Last.fm.
    /// </summary>
    public class UserFull : LastFmBaseModel
    {
        /*
            <user>
                <id>1000002</id>
                <name>RJ</name>
                <realname>Richard Jones </realname>
                <url>http://www.last.fm/user/RJ</url>
                <image>http://userserve-ak.last.fm/serve/126/8270359.jpg</image>
                <country>UK</country>
                <age>27</age>
                <gender>m</gender>
                <subscriber>1</subscriber>
                <playcount>54189</playcount>
                <playlists>4</playlists>
                <bootstrap>0</bootstrap>
                <registered unixtime="1037793040">2002-11-20 11:50</registered>
            </user>
       */

        public string Id { get; set; }

        public string Realname { get; set; }

        public string Age { get; set; }

        public string Gender { get; set; }

        public string Playcount { get; set; }

        public string Playlists { get; set; }

        public string Bootstrap { get; set; }

        public string Subscriber { get; set; }

        public RegisteredDto Registered { get; set; }
    }
}