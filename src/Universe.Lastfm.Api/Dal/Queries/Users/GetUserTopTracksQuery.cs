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
using Universe.Lastfm.Api.Dto.GetTrackInfo;
using Universe.Lastfm.Api.Helpers;
using Universe.Lastfm.Api.Models;
using Universe.Lastfm.Api.Models.Base;
using Universe.Lastfm.Api.Models.Res.Base;
using static Universe.Lastfm.Api.Dal.Queries.Users.GetUserTopTracksQuery;

namespace Universe.Lastfm.Api.Dal.Queries.Users
{
    /// <summary>
    ///     The query gets the full information about top listened tracks of an user of the Last.fm.
    ///     Запрос, получающий полную информацию о топ прослушиваемых трэках пользователя Last.fm. 
    /// <author>Alex Universe</author>
    /// <author>Alex Envision</author>
    /// </summary>
    public class GetUserTopTracksQuery : LastQuery<GetUserTopTracksRequest, GetUserTopTracksResponce>
    {
        protected override Func<BaseRequest, BaseResponce> ExecutableBaseFunc =>
            req => Execute(req.As<GetUserTopTracksRequest>());

        /// <summary>
        ///     Get the top tracks used by this user.
        /// </summary>
        /// <param name="request.user">
        ///     The user name to fetch top tracks for.
        /// </param>
        /// <param name="request.limit">
        ///     The number of results to fetch per page. Defaults to 50.
        /// </param>
        /// <param name="request">
        ///     Request with parameters.
        /// </param>
        /// <returns></returns>
        public override GetUserTopTracksResponce Execute(
            GetUserTopTracksRequest request)
        {
            string user = request.User;
            int limit = request.Limit;

            var sessionResponce = Adapter.GetRequest("user.getTopTracks",
                Argument.Create("api_key", Settings.ApiKey),
                Argument.Create("user", user),
                Argument.Create("format", "json"),
                Argument.Create("limit", limit.ToString()),
                Argument.Create("callback", "?"));

            Adapter.FixCallback(sessionResponce);

            var getAlbumInfoResponce = ResponceExt.CreateFrom<BaseResponce, GetUserTopTracksResponce>(sessionResponce);
            return getAlbumInfoResponce;
        }

        /// <summary>
        ///     The request for full information about user of the Last.fm.
        ///     Запрос на полную информациею о пользователе Last.fm.
        /// </summary>
        public class GetUserTopTracksRequest : BaseRequest
        {
            public string User { get; set; }

            public int Limit { get; set; }

            public GetUserTopTracksRequest()
            {
                Limit = 50;
            }

            /// <summary>
            ///     Get the top tracks used by this user.
            /// </summary>
            /// <param name="user">
            ///     The user name to fetch top tracks for.
            /// </param>
            /// <param name="limit">
            ///     The number of results to fetch per page. Defaults to 50.
            /// </param>
            /// <returns>Returns <see cref="GetUserTopTracksRequest"/></returns>
            public static GetUserTopTracksRequest Build(
                string user,
                int limit = 50)
            {
                return new GetUserTopTracksRequest
                {
                    User = user,
                    Limit = limit
                };
            }
        }

        /// <summary>
        ///     The responce with full information about user of the Last.fm.
        ///     Ответ с полной информацией о пользователе Last.fm.
        /// </summary>
        public class GetUserTopTracksResponce : LastFmBaseResponce<UserTopTracksContainer>
        {
        }

        /// <summary>
        ///     The container with information about the top tracks listened to by a user on the Last.fm.
        ///     Контейнер с информацией о топ-исполнителях, которые прослушивал пользователь на Last.fm.
        /// </summary>
        public class UserTopTracksContainer : LastFmBaseContainer
        {
            public TopTracksDto TopTracks { get; set; }
        }

        /// <summary>
        ///     The full information about track on the Last.fm.
        ///     Полная информация о трэке на Last.fm.
        /// </summary>
        public class TopTracksDto
        {
            /*
                 <toptracks user="LFUser" type="overall">  
                  <track rank="1">
                    <name>Learning to Live</name>
                    <playcount>42</playcount>
                    <mbid/>
                    <url>
                      http://www.last.fm/music/Dream+Theater/_/Learning+to+Live
                    </url>
                    <streamable fulltrack="0">1</streamable>
                    <artist>
                      <name>Dream Theater</name>
                      <mbid>28503ab7-8bf2-4666-a7bd-2644bfc7cb1d</mbid>
                      <url>http://www.last.fm/music/Dream+Theater</url>
                    </artist>
                    <image size="small">...</image>
                    <image size="medium">...</image>
                    <image size="large">...</image>
                  </track>
                  ...
                </toptracks>
            */

            public TopTracksAttribute Attribute { get; set; }

            public TopTrack[] Track { get; set; }
        }

        /// <summary>
        ///     The special attribite of <see cref="TopTracksDto"/>
        /// </summary>
        public class TopTracksAttribute
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