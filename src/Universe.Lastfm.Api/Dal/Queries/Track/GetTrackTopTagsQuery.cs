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
using Universe.Lastfm.Api.Helpers;
using Universe.Lastfm.Api.Models;
using Universe.Lastfm.Api.Models.Base;
using Universe.Lastfm.Api.Models.Res.Base;

namespace Universe.Lastfm.Api.Dal.Queries.Track
{
    /// <summary>
    ///     The query does search of a similar top-track of the Last.fm.
    ///     Запрос, ведущий поиск топ-трэков на Last.fm. 
    /// </summary>
    public class GetTrackTopTagsQuery : LastQuery<GetTrackTopTagsRequest, GetTrackTopTagsResponce>
    {
        protected override Func<BaseRequest, BaseResponce> ExecutableBaseFunc =>
            req => Execute(req.As<GetTrackTopTagsRequest>());

        /// <summary>
        ///     Get the top tags for this track on Last.fm, ordered by tag count.
        ///     Supply either track & artist name or mbid.
        /// </summary>
        /// <param name="request.artist">
        ///     The artist name.
        /// </param>
        /// <param name="request.Track">
        ///     The track name.
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
        public override GetTrackTopTagsResponce Execute(
            GetTrackTopTagsRequest request)
        {
            string artist = request.Performer ?? throw new ArgumentNullException("request.Performer");
            string track = request.Track ?? throw new ArgumentNullException("request.Track");

            var sessionResponce = Adapter.GetRequest("track.getTopTags",
                Argument.Create("api_key", Settings.ApiKey),
                Argument.Create("artist", artist),
                Argument.Create("track", track),
                Argument.Create("format", "json"),
                Argument.Create("callback", "?"));

            Adapter.FixCallback(sessionResponce);

            var getTrackInfoResponce = ResponceExt.CreateFrom<BaseResponce, GetTrackTopTagsResponce>(sessionResponce);
            return getTrackInfoResponce;
        }
    }

    /// <summary>
    ///     The request with full information about similar of the Last.fm.
    ///     Запрос с полной информацией о поиске похожих Last.fm.
    /// </summary>
    public class GetTrackTopTagsRequest : BaseRequest
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

        public string Track { get; set; }

        public string Performer { get; set; }

        public GetTrackTopTagsRequest()
        {
            Page = 1;
            Limit = 50;
        }
    }

    /// <summary>
    ///     The responce with full information about a search of similar in the Last.fm.
    ///     Ответ с полной информацией о поиске похожих Last.fm.
    /// </summary>
    public class GetTrackTopTagsResponce : LastFmBaseResponce<TrackTopTagsContainer>
    {
    }

    /// <summary>
    ///     The container with information about the top Tracks listened to by a Track on the Last.fm.
    ///     Контейнер с информацией о поиске, которые прослушивал пользователь на Last.fm.
    /// </summary>
    public class TrackTopTagsContainer : LastFmBaseContainer
    {
        public TrackTopTagsTracksDto TopTags { get; set; }
    }

    /// <summary>
    ///     The full information about track on the Last.fm.
    ///     Полная информация о похожих исполнителях на Last.fm.
    /// </summary>
    public class TrackTopTagsTracksDto
    {
        /*
             <toptags artist="Cher" track="Believe">
              <tag>
                <name>pop</name>
                <count>97</count>
                <url>www.last.fm/tag/pop</url>
              </tag>
              <tag>
                <name>dance</name>
                <count>88</count>
                <url>www.last.fm/tag/dance</url>
              </tag>
              ...
            </toptags>
        */

        public TrackTopTagsAttribute Attribute { get; set; }

        public TopTagsDto TopTags { get; set; }
    }

    /// <summary>
    ///     TopTags of Track/perforner
    /// </summary>
    public class TopTagsDto : LastFmBaseModel
    {
        public TopTagsTrack Track { get; set; }
    }

    /// <summary>
    ///     Perform itself
    /// </summary>
    public class TopTagsTrack : LastFmBaseModel
    {
        public Guid Mbid { get; set; }
    }

    /// <summary>
    ///     The special attribite of <see cref="TopTagsDto"/>
    /// </summary>
    public class TrackTopTagsAttribute
    {
        /*
            @attr":{"Track":"Ayreon","track":"Age of Shadows"}
        */

        public string Track { get; set; }
    }
}