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
using Universe.Lastfm.Api.Dto.Common;
using Universe.Lastfm.Api.Helpers;
using Universe.Lastfm.Api.Models;
using Universe.Lastfm.Api.Models.Base;
using Universe.Lastfm.Api.Models.Res.Base;

namespace Universe.Lastfm.Api.Dal.Queries.Track
{
    /// <summary>
    ///     The query does search of a similar track of the Last.fm.
    ///     Запрос, ведущий поиск похожих трэков на Last.fm. 
    /// </summary>
    public class GetTrackSimilarQuery : LastQuery<GetTrackSimilarRequest, GetTrackSimilarResponce>
    {
        protected override Func<BaseRequest, BaseResponce> ExecutableBaseFunc =>
            req => Execute(req.As<GetTrackSimilarRequest>());

        /// <summary>
        ///     Get the similar tracks for this track on Last.fm,
        ///     based on listening data.
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
        public override GetTrackSimilarResponce Execute(
            GetTrackSimilarRequest request)
        {
            string performer = request.Performer;
            string track = request.Track;
            string user = request.User;
            int page = request.Page;
            int limit = request.Limit;

            var sessionResponce = Adapter.GetRequest("track.getSimilar",
                Argument.Create("api_key", Settings.ApiKey),
                Argument.Create("artist", performer),
                Argument.Create("track", track),
                Argument.Create("user", user),
                Argument.Create("page", page.ToString()),
                Argument.Create("limit", limit.ToString()),
                Argument.Create("format", "json"),
                Argument.Create("callback", "?"));

            Adapter.FixCallback(sessionResponce);

            var getTrackInfoResponce = ResponceExt.CreateFrom<BaseResponce, GetTrackSimilarResponce>(sessionResponce);
            return getTrackInfoResponce;
        }
    }

    /// <summary>
    ///     The request with full information about similar of the Last.fm.
    ///     Запрос с полной информацией о поиске похожих Last.fm.
    /// </summary>
    public class GetTrackSimilarRequest : BaseRequest
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

        public GetTrackSimilarRequest()
        {
            Page = 1;
            Limit = 50;
        }
    }

    /// <summary>
    ///     The responce with full information about a search of similar in the Last.fm.
    ///     Ответ с полной информацией о поиске похожих Last.fm.
    /// </summary>
    public class GetTrackSimilarResponce : LastFmBaseResponce<TrackSimilarContainer>
    {
    }

    /// <summary>
    ///     The container with information about the top Tracks listened to by a track on the Last.fm.
    ///     Контейнер с информацией о поиске трэков, которые прослушивал пользователь на Last.fm.
    /// </summary>
    public class TrackSimilarContainer : LastFmBaseContainer
    {
        public TrackSimilarDto Similartracks { get; set; }
    }

    /// <summary>
    ///     The full information about track on the Last.fm.
    ///     Полная информация о поиске на Last.fm.
    /// </summary>
    public class TrackSimilarDto
    {
        /*
             <similartracks track="Believe" artist="Cher">
              <track>
                <name>Ray of Light</name>
                <mbid/>
                <match>10.95</match>
                <url>http://www.last.fm/music/Madonna/_/Ray+of+Light</url>
                <streamable fulltrack="0">1</streamable>
                <artist>
                  <name>Madonna</name>
                  <mbid>79239441-bfd5-4981-a70c-55c3f15c1287</mbid>
                  <url>http://www.last.fm/music/Madonna</url>
                </artist>
                <image size="small">http://cdn.last.fm/coverart/50x50/1934.jpg</image>
                <image size="medium">http://cdn.last.fm/coverart/130x130/1934.jpg</image>
                <image size="large">http://cdn.last.fm/coverart/130x130/1934.jpg</image>
              </track>
              ...
            </similartracks>
        */

        public TrackSimilarAttribute Attribute { get; set; }

        public string Artist { get; set; }

        public TrackShort[] Track { get; set; }
    }

    /// <summary>
    ///     The special attribite of <see cref="TrackSimilarDto"/>
    /// </summary>
    public class TrackSimilarAttribute
    {
        /*
            @attr":{"artist":"Ayreon","track":"Age of Shadows"}
        */

        public string Artist { get; set; }

        public string Track { get; set; }
    }
}