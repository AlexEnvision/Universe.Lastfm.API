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
using Universe.Lastfm.Api.Dto.GetArtists;
using Universe.Lastfm.Api.Helpers;
using Universe.Lastfm.Api.Models;
using Universe.Lastfm.Api.Models.Base;
using Universe.Lastfm.Api.Models.Res.Base;

namespace Universe.Lastfm.Api.Dal.Queries.Users
{
    /// <summary>
    ///     The query gets the full information about top artists/performers of an user of the Last.fm.
    ///     Запрос, получающий полную информацию о топ-исполнителях пользователя Last.fm. 
    /// </summary>
    public class GetUserTopArtistsQuery : LastQuery
    {
        /// <summary>
        ///     Get the top artists listened to by a user.
        ///     You can stipulate a time period.
        ///     Sends the overall chart by default.
        /// </summary>
        /// <param name="user">
        ///     The user name to fetch top artists for.
        /// </param>
        /// <param name="page">
        ///     The page number to fetch. Defaults to first page.
        /// </param>
        /// <param name="limit">
        ///     The number of results to fetch per page. Defaults to 50.
        /// </param>
        /// <param name="period">
        ///     Period (Optional) : overall | 7day | 1month | 3month | 6month | 12month -
        ///     The time period over which to retrieve top artists for.
        /// </param>
        /// <returns></returns>
        public GetUserTopArtistsResponce Execute(
            string user,
            string period = "",
            int page = 1,
            int limit = 50)
        {
            var sessionResponce = Adapter.GetRequest("user.getTopArtists",
                Argument.Create("api_key", Settings.ApiKey),
                Argument.Create("user", user),
                Argument.Create("format", "json"),
                Argument.Create("page", page.ToString()),
                Argument.Create("limit", limit.ToString()),
                Argument.Create("callback", "?"));

            Adapter.FixCallback(sessionResponce);

            var getAlbumInfoResponce = ResponceExt.CreateFrom<BaseResponce, GetUserTopArtistsResponce>(sessionResponce);
            return getAlbumInfoResponce;
        }

        /// <summary>
        ///     The responce with full information about user of the Last.fm.
        ///     Ответ с полной информацией о пользователе Last.fm.
        /// </summary>
        public class GetUserTopArtistsResponce : LastFmBaseResponce<UserTopArtistsContainer>
        {
        }

        /// <summary>
        ///     The container with information about the top artists listened to by a user on the Last.fm.
        ///     Контейнер с информацией о топ-исполнителях, которые прослушивал пользователь на Last.fm.
        /// </summary>
        public class UserTopArtistsContainer : LastFmBaseContainer
        {
            public TopArtistsDto TopArtists { get; set; }
        }

        /// <summary>
        ///     The full information about track on the Last.fm.
        ///     Полная информация о трэке на Last.fm.
        /// </summary>
        public class TopArtistsDto
        {
            /*
                 <topartists user="RJ" type="overall">
                  <artist rank="1">
                    <name>Dream Theater</name>
                    <playcount>1337</playcount>
                    <mbid>28503ab7-8bf2-4666-a7bd-2644bfc7cb1d</mbid>
                    <url>http://www.last.fm/music/Dream+Theater</url>
                    <streamable>1</streamable>
                    <image size="small">...</image>
                    <image size="medium">...</image>
                    <image size="large">...</image>
                  </artist>
                  ...
                </topartists>
            */

            public TopArtistsAttribute Attribute { get; set; }

            public Artist[] Artist { get; set; }
        }

        /// <summary>
        ///     The special attribite of <see cref="TopArtistsDto"/>
        /// </summary>
        public class TopArtistsAttribute
        {
            /*
                 "@attr":
                 {
                    "page": "1",
                    "perPage": "50",
                    "total": "362",
                    "totalPages": "8",
                    "user": "LFUser"
                 },
            */

            public string Page { get; set; }

            public string PerPage { get; set; }

            public string Total { get; set; }

            public string TotalPages { get; set; }

            public string User { get; set; }
        }
    }
}