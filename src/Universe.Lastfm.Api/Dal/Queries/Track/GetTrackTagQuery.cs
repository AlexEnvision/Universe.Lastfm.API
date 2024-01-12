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
using Universe.Lastfm.Api.Dto.Common.Short;
using Universe.Lastfm.Api.Dto.Common;
using Universe.Lastfm.Api.Dto.GetAlbumInfo;
using Universe.Lastfm.Api.Dto.GetTrackInfo;
using Universe.Lastfm.Api.Helpers;
using Universe.Lastfm.Api.Models;
using Universe.Lastfm.Api.Models.Base;
using Universe.Lastfm.Api.Models.Res.Base;

namespace Universe.Lastfm.Api.Dal.Queries.Track
{
    /// <summary>
    ///     The query does search of an Track of the Last.fm.
    ///     Запрос, ведущий поиск альбома на Last.fm. 
    /// </summary>
    public class GetTrackTagQuery : LastQuery<GetTrackTagRequest, GetTrackTagResponce>
    {
        protected override Func<BaseRequest, BaseResponce> ExecutableBaseFunc =>
            req => Execute(req.As<GetTrackTagRequest>());

        /// <summary>
        ///     Search for an Track by name. Returns Track matches sorted by relevance.
        /// </summary>
        /// <param name="request.Track">
        ///     The Track name.
        /// </param>
        /// <param name="request.page">
        ///     The page number to fetch. Defaults to first page.
        /// </param>
        /// <param name="request.limit">
        ///     The number of results to fetch per page. Defaults to 30.
        /// </param>
        /// <param name="request">
        ///     Request with parameters.
        /// </param>
        /// <returns></returns>
        public override GetTrackTagResponce Execute(
            GetTrackTagRequest request)
        {
            string performer = request.Performer;
            string track = request.Track;
            string user = request.User;
            int page = request.Page;
            int limit = request.Limit;

            var sessionResponce = Adapter.GetRequest("track.getTags",
                Argument.Create("api_key", Settings.ApiKey),
                Argument.Create("artist", performer),
                Argument.Create("track", track),
                Argument.Create("user", user),
                Argument.Create("page", page.ToString()),
                Argument.Create("limit", limit.ToString()),
                Argument.Create("format", "json"),
                Argument.Create("callback", "?"));

            Adapter.FixCallback(sessionResponce);

            var getTrackInfoResponce = ResponceExt.CreateFrom<BaseResponce, GetTrackTagResponce>(sessionResponce);
            return getTrackInfoResponce;
        }
    }

    /// <summary>
    ///     The request with full information about Track of the Last.fm.
    ///     Запрос с полной информацией о поиске Last.fm.
    /// </summary>
    public class GetTrackTagRequest : BaseRequest
    {
        private int _page;
        private int _limit;

        public int Limit
        {
            get => _page == 0 ? 50 : _page;
            set => _limit = value;
        }

        public int Page
        {
            get => _page == 0 ? 1 : _page;
            set => _page = value;
        }

        public string Performer { get; set; }

        public string Track { get; set; }

        public string User { get; set; }

        public GetTrackTagRequest()
        {
            Page = 1;
            Limit = 50;
        }
    }

    /// <summary>
    ///     The responce with full information about Track of the Last.fm.
    ///     Ответ с полной информацией о поиске Last.fm.
    /// </summary>
    public class GetTrackTagResponce : LastFmBaseResponce<TrackTagContainer>
    {
    }

    /// <summary>
    ///     The container with information about the top Tracks listened to by a track on the Last.fm.
    ///     Контейнер с информацией о поиске трэков, которые прослушивал пользователь на Last.fm.
    /// </summary>
    public class TrackTagContainer : LastFmBaseContainer
    {
        public TrackTagDto Tags { get; set; }
    }

    /// <summary>
    ///     The full information about track on the Last.fm.
    ///     Полная информация о поиске на Last.fm.
    /// </summary>
    public class TrackTagDto
    {
        /*
             <tags artist="Sally Shapiro" track="I'll be by your side">
              <tag>
                <name>swedish</name>
                <url>http://www.last.fm/tag/swedish</url>
              </tag>
              ...
            </tags>
        */

        public TrackTagAttribute Attribute { get; set; }

        public string Artist { get; set; }

        public string Track { get; set; }

        public Tag[] Tag { get; set; }
    }

    public class TagsDto : LastFmBaseModel
    {
        public string Artist { get; set; }

        public string Track { get; set; }

        public Tag[] Tag { get; set; }
    }

    /// <summary>
    ///     The special attribite of <see cref="TrackTagDto"/>
    /// </summary>
    public class TrackTagAttribute
    {
        /*
            @attr":{"artist":"Ayreon","track":"Age of Shadows"}
        */

        public string Artist { get; set; }

        public string Track { get; set; }
    }
}