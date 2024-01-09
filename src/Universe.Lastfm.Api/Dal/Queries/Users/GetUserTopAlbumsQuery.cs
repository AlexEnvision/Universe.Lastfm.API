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
using Universe.Lastfm.Api.Dto.Base;
using Universe.Lastfm.Api.Dto.GetAlbumInfo;
using Universe.Lastfm.Api.Helpers;
using Universe.Lastfm.Api.Models;
using Universe.Lastfm.Api.Models.Base;
using Universe.Lastfm.Api.Models.Req;
using Universe.Lastfm.Api.Models.Res.Base;
using static Universe.Lastfm.Api.Dal.Queries.Users.GetUserTopAlbumsQuery;
using static Universe.Lastfm.Api.Dal.Queries.Users.GetUserTopArtistsQuery;

namespace Universe.Lastfm.Api.Dal.Queries.Users
{
    /// <summary>
    ///     The query gets the full information about top albums of an user of the Last.fm.
    ///     Запрос, получающий полную информацию о топ-альбомах пользователя Last.fm. 
    /// </summary>
    public class GetUserTopAlbumsQuery : LastQuery<GetUserTopAlbumsRequest, GetUserTopAlbumsResponce>
    {
        protected override Func<BaseRequest, BaseResponce> ExecutableBaseFunc =>
            req => Execute(req.As<GetUserTopAlbumsRequest>());

        /// <summary>
        ///     Get the top albums listened to by a user.
        ///     You can stipulate a time period.
        ///     Sends the overall chart by default.
        /// </summary>
        /// <param name="request.user">
        ///     The user name to fetch top albums for.
        /// </param>
        /// <param name="request.page">
        ///     The page number to fetch. Defaults to first page.
        /// </param>
        /// <param name="request.limit">
        ///     The number of results to fetch per page. Defaults to 50.
        /// </param>
        /// <param name="request.period">
        ///     Period (Optional) : overall | 7day | 1month | 3month | 6month | 12month -
        ///     The time period over which to retrieve top albums for.
        /// </param>
        /// <param name="request">
        ///     Request model
        /// </param>
        /// <returns></returns>
        public override GetUserTopAlbumsResponce Execute(
               GetUserTopAlbumsRequest request
            )
        {
            string user = request.User;
            string period = request.Period;
            int page = request.Page;
            int limit = request.Limit;

            var sessionResponce = Adapter.GetRequest("user.getTopAlbums",
                Argument.Create("api_key", Settings.ApiKey),
                Argument.Create("user", user),
                Argument.Create("format", "json"),
                Argument.Create("page", page.ToString()),
                Argument.Create("limit", limit.ToString()),
                Argument.Create("callback", "?"));

            Adapter.FixCallback(sessionResponce);

            var getAlbumInfoResponce = ResponceExt.CreateFrom<BaseResponce, GetUserTopAlbumsResponce>(sessionResponce);
            return getAlbumInfoResponce;
        }

        /// <summary>
        ///     The request with parameters for information about user data of the Last.fm.
        ///     Запрос с параметрами для полной информацией о данных пользователе Last.fm.
        /// </summary>
        public class GetUserTopAlbumsRequest : BaseRequest
        {
            public string User { get; set; }

            public string Period { get; set; }

            public int Page { get; set; }

            public int Limit { get; set; }

            public GetUserTopAlbumsRequest()
            {
                Period = "";
                Page = 1;
                Limit = 50;
            }

            /// <summary>
            ///     Build request
            /// </summary>
            /// <param name="user">
            ///     The user name to fetch top albums for.
            /// </param>
            /// <param name="page">
            ///     The page number to fetch. Defaults to first page.
            /// </param>
            /// <param name="limit">
            ///     The number of results to fetch per page. Defaults to 50.
            /// </param>
            /// <param name="period">
            ///     Period (Optional) : overall | 7day | 1month | 3month | 6month | 12month -
            ///     The time period over which to retrieve top albums for.
            /// </param>
            /// <returns>Returns <see cref="GetUserTopAlbumsRequest"/></returns>
            public static GetUserTopAlbumsRequest Build(
                string user,
                string period = "",
                int page = 1,
                int limit = 50)
            {
                return new GetUserTopAlbumsRequest
                {
                    User = user,
                    Period = period,
                    Limit = limit,
                    Page = page
                };
            }
        }

        /// <summary>
        ///     The responce with information about user data of the Last.fm.
        ///     Ответ с полной информацией о данных пользователе Last.fm.
        /// </summary>
        public class GetUserTopAlbumsResponce : LastFmBaseResponce<UserTopAlbumsContainer>
        {
        }

        /// <summary>
        ///     The container with information about the top albums listened to by a user on the Last.fm.
        ///     Контейнер с информацией о топ-исполнителях, которые прослушивал пользователь на Last.fm.
        /// </summary>
        public class UserTopAlbumsContainer : LastFmBaseContainer
        {
            public TopAlbumsDto TopAlbums { get; set; }
        }

        /// <summary>
        ///     The full information about track on the Last.fm.
        ///     Полная информация о трэке на Last.fm.
        /// </summary>
        public class TopAlbumsDto
        {
            /*
                 <topalbums user="LFUser" type="overall">
                  <album rank="1">
                    <name>Images and Words</name>
                    <playcount>174</playcount>
                    <mbid>f20971f2-c8ad-4d26-91ab-730f6dedafb2</mbid>  
                    <url>
                      http://www.last.fm/music/Dream+Theater/Images+and+Words
                    </url>
                    <artist>
                      <name>Dream Theater</name>
                      <mbid>28503ab7-8bf2-4666-a7bd-2644bfc7cb1d</mbid>
                      <url>http://www.last.fm/music/Dream+Theater</url>
                    </artist>
                    <image size="small">...</image>
                    <image size="medium">...</image>
                    <image size="large">...</image>
                  </album>
                </topalbums>
            */

            public TopAlbumsAttribute Attribute { get; set; }

            public AlbumFull[] Album { get; set; }
        }

        /// <summary>
        ///     The special attribite of <see cref="TopAlbumsDto"/>
        /// </summary>
        public class TopAlbumsAttribute
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