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
using static Universe.Lastfm.Api.Dal.Queries.Users.GetUserRecentTracksQuery;

namespace Universe.Lastfm.Api.Dal.Queries.Users
{
    /// <summary>
    ///     The query gets the full information about recent listened tracks of an user of the Last.fm.
    ///     Запрос, получающий полную информацию об часто прослушиваемых трэках пользователя Last.fm. 
    /// <author>Alex Universe</author>
    /// <author>Alex Envision</author>
    /// </summary>
    public class GetUserRecentTracksQuery : LastQuery<GetUserRecentTracksRequest, GetUserRecentTracksResponce>
    {
        protected override Func<BaseRequest, BaseResponce> ExecutableBaseFunc =>
            req => Execute(req.As<GetUserRecentTracksRequest>());

        /// <summary>
        ///     Get a list of the recent tracks listened to by this user.
        ///     Also includes the currently playing track with the nowplaying="true" attribute
        ///     if the user is currently listening.
        /// </summary>
        /// <param name="request.user">
        ///     The last.fm username to fetch the recent tracks of.
        /// </param>
        /// <param name="request.from">
        ///     Beginning timestamp of a range - only display scrobbles after this time,
        ///     in UNIX timestamp format (integer number of seconds since 00:00:00,
        ///     January 1st 1970 UTC). This must be in the UTC time zone.
        /// </param>
        /// <param name="request.to">
        ///     End timestamp of a range - only display scrobbles before this time,
        ///     in UNIX timestamp format (integer number of seconds since 00:00:00,
        ///     January 1st 1970 UTC). This must be in the UTC time zone.
        /// </param>
        /// <param name="request.extend">
        ///     Includes extended data in each artist, and whether or not the user has loved each track
        /// </param>
        /// <param name="request.page">
        ///     The page number to fetch. Defaults to first page.
        /// </param>
        /// <param name="request.limit">
        ///     The number of results to fetch per page. Defaults to 50. Maximum is 200.
        /// </param>
        /// <param name="request">
        ///     Request with parameters.
        /// </param>
        /// <returns></returns>
        public override GetUserRecentTracksResponce Execute(
            GetUserRecentTracksRequest request)
        {
            string user = request.User;
            string from = request.From;
            string to = request.To;
            string extend = request.Extend;
            int page = request.Page;
            int limit = request.Limit;

            var sessionResponce = Adapter.GetRequest("user.getRecentTracks",
                Argument.Create("api_key", Settings.ApiKey),
                Argument.Create("user", user),
                Argument.Create("from", from),
                Argument.Create("to", to), 
                Argument.Create("extend", extend),
                Argument.Create("page", page.ToString()),
                Argument.Create("limit", limit.ToString()),
                Argument.Create("format", "json"),
                Argument.Create("callback", "?"));

            Adapter.FixCallback(sessionResponce);

            var getAlbumInfoResponce = ResponceExt.CreateFrom<BaseResponce, GetUserRecentTracksResponce>(sessionResponce);
            return getAlbumInfoResponce;
        }

        /// <summary>
        ///     The request for getting full information about user of the Last.fm.
        ///     Запрос для получения полной информации о пользователе Last.fm.
        /// </summary>
        public class GetUserRecentTracksRequest : BaseRequest
        {
            public string User { get; set; }

            public string From { get; set; }

            public string To { get; set; }

            public string Extend { get; set; }

            public int Page { get; set; }

            public int Limit { get; set; }

            public GetUserRecentTracksRequest()
            {
                From = null;
                To = null;
                Extend = null;
                Page = 1;
                Limit = 50;
            }
        }

        /// <summary>
        ///     The responce with information about user data of the Last.fm.
        ///     Ответ с полной информацией о данных пользователе Last.fm.
        /// </summary>
        public class GetUserRecentTracksResponce : LastFmBaseResponce<UserRecentTracksContainer>
        {
        }

        /// <summary>
        ///     The container with information about the top tracks listened to by a user on the Last.fm.
        ///     Контейнер с информацией о топ-исполнителях, которые прослушивал пользователь на Last.fm.
        /// </summary>
        public class UserRecentTracksContainer : LastFmBaseContainer
        {
            public RecentTracksDto RecentTracks { get; set; }
        }

        /// <summary>
        ///     The full information about track on the Last.fm.
        ///     Полная информация о трэке на Last.fm.
        /// </summary>
        public class RecentTracksDto
        {
            /*
                 <recenttracks user="RJ" page="1" perPage="10" totalPages="3019">
                  <track nowplaying="true">
                    <artist mbid="2f9ecbed-27be-40e6-abca-6de49d50299e">Aretha Franklin</artist>
                    <name>Sisters Are Doing It For Themselves</name>
                    <mbid/>
                    <album mbid=""/>
                    <url>www.last.fm/music/Aretha+Franklin/_/Sisters+Are+Doing+It+For+Themselves</url>
                    <date uts="1213031819">9 Jun 2008, 17:16</date>
                    <streamable>1</streamable>
                  </track>
                  ...
                </recenttracks>
            */

            public RecentTracksAttribute Attribute { get; set; }

            public TopTrack[] Track { get; set; }
        }

        /// <summary>
        ///     The special attribite of <see cref="RecentTracksDto"/>
        /// </summary>
        public class RecentTracksAttribute
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