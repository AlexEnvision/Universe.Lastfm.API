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
using static Universe.Lastfm.Api.Dal.Queries.Users.GetUserLovedTracksQuery;


namespace Universe.Lastfm.Api.Dal.Queries.Users
{
    /// <summary>
    ///     The query gets the full information about loved listened tracks of an user of the Last.fm.
    ///     Запрос, получающий полную информацию о топ прослушиваемых трэках пользователя Last.fm. 
    /// </summary>
    public class GetUserLovedTracksQuery : LastQuery<GetUserLovedTracksRequest, GetUserLovedTracksResponce>
    {
        protected override Func<BaseRequest, BaseResponce> ExecutableBaseFunc =>
            req => Execute(req.As<GetUserLovedTracksRequest>());

        /// <summary>
        ///     Get the last 50 tracks loved by a user.
        /// </summary>
        /// <param name="request.user">
        ///     The user name to fetch loved tracks for.
        /// </param>
        /// <param name="request.limit">
        ///     The number of results to fetch per page. Defaults to 50.
        /// </param>
        /// <param name="request">
        ///
        /// </param>
        /// <returns></returns>
        public override GetUserLovedTracksResponce Execute(
            GetUserLovedTracksRequest request)
        {
            string user = request.User;
            int limit = request.Limit;

            var sessionResponce = Adapter.GetRequest("user.getLovedTracks",
                Argument.Create("api_key", Settings.ApiKey),
                Argument.Create("user", user),
                Argument.Create("format", "json"),
                Argument.Create("limit", limit.ToString()),
                Argument.Create("callback", "?"));

            Adapter.FixCallback(sessionResponce);

            var getAlbumInfoResponce = ResponceExt.CreateFrom<BaseResponce, GetUserLovedTracksResponce>(sessionResponce);
            return getAlbumInfoResponce;
        }

        /// <summary>
        ///     The request for getting full information about user of the Last.fm.
        ///     Запрос для получения полной информации о пользователе Last.fm.  
        /// </summary>
        public class GetUserLovedTracksRequest : BaseRequest
        {
            public string User { get; set; }

            public int Limit { get; set; }

            public GetUserLovedTracksRequest()
            {
                Limit = 50;
            }
        }

        /// <summary>
        ///     The responce with information about user data of the Last.fm.
        ///     Ответ с полной информацией о данных пользователе Last.fm.
        /// </summary>
        public class GetUserLovedTracksResponce : LastFmBaseResponce<UserLovedTracksContainer>
        {
        }

        /// <summary>
        ///     The container with information about the loved tracks listened to by a user on the Last.fm.
        ///     Контейнер с информацией о топ-исполнителях, которые прослушивал пользователь на Last.fm.
        /// </summary>
        public class UserLovedTracksContainer : LastFmBaseContainer
        {
            public LovedTracksDto LovedTracks { get; set; }
        }

        /// <summary>
        ///     The full information about track on the Last.fm.
        ///     Полная информация о трэке на Last.fm.
        /// </summary>
        public class LovedTracksDto
        {
            /*
                 <lovedtracks user="LFUser">
                  <track>
                    <name>The Glass Prison</name>
                    <mbid/>
                    <url>www.last.fm/music/Dream+Theater/_/The+Glass+Prison</url>
                    <date uts="1216371514">18 Jul 2008, 08:58</date>
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
                </lovedtracks>
            */

            public LovedTracksAttribute Attribute { get; set; }

            public TopTrack[] Track { get; set; }
        }

        /// <summary>
        ///     The special attribite of <see cref="LovedTracksDto"/>
        /// </summary>
        public class LovedTracksAttribute
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