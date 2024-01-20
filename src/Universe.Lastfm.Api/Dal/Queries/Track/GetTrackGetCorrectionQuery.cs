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
    ///     The query does search of a similar track of the Last.fm.
    ///     Запрос, ведущий поиск похожих трэков на Last.fm. 
    /// <author>Alex Universe</author>
    /// <author>Alex Envision</author>
    /// </summary>
    public class GetTrackGetCorrectionQuery : LastQuery<GetTrackCorrectionRequest, GetTrackCorrectionResponce>
    {
        protected override Func<BaseRequest, BaseResponce> ExecutableBaseFunc =>
            req => Execute(req.As<GetTrackCorrectionRequest>());

        /// <summary>
        ///     Use the last.fm corrections data to check whether
        ///     the supplied track has a correction to a canonical track
        /// </summary>
        /// <param name="request.artist">
        ///     The artist name to correct.
        /// </param>
        /// <param name="request.track">
        ///     The track name to correct.
        /// </param>
        /// <param name="request">
        ///     Request with parameters.
        /// </param>
        /// <returns></returns>
        public override GetTrackCorrectionResponce Execute(
            GetTrackCorrectionRequest request)
        {
            string artist = request.Performer;
            string track = request.Track;

            var sessionResponce = Adapter.GetRequest("track.getCorrection",
                Argument.Create("api_key", Settings.ApiKey),
                Argument.Create("artist", artist),
                Argument.Create("track", track),
                Argument.Create("format", "json"),
                Argument.Create("callback", "?"));

            Adapter.FixCallback(sessionResponce);

            var getTrackInfoResponce = ResponceExt.CreateFrom<BaseResponce, GetTrackCorrectionResponce>(sessionResponce);
            return getTrackInfoResponce;
        }
    }

    /// <summary>
    ///     The request with full information about similar of the Last.fm.
    ///     Запрос с полной информацией о поиске похожих Last.fm.
    /// </summary>
    public class GetTrackCorrectionRequest : BaseRequest
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

        public GetTrackCorrectionRequest()
        {
            Page = 1;
            Limit = 50;
        }
    }

    /// <summary>
    ///     The responce with full information about a search of similar in the Last.fm.
    ///     Ответ с полной информацией о поиске похожих Last.fm.
    /// </summary>
    public class GetTrackCorrectionResponce : LastFmBaseResponce<TrackCorrectionContainer>
    {
    }

    /// <summary>
    ///     The container with information about the top Tracks listened to by a Track on the Last.fm.
    ///     Контейнер с информацией о поиске, которые прослушивал пользователь на Last.fm.
    /// </summary>
    public class TrackCorrectionContainer : LastFmBaseContainer
    {
        public TrackCorrectionTracksDto Corrections { get; set; }
    }

    /// <summary>
    ///     The full information about track on the Last.fm.
    ///     Полная информация о похожих исполнителях на Last.fm.
    /// </summary>
    public class TrackCorrectionTracksDto
    {
        /*
             <corrections>
              <correction index="0" artistcorrected="1" trackcorrected="1">
                <track>
                  <name>Mr. Brownstone</name>
                  <mbid/>
                  <url>www.last.fm/music/Guns+N%27+Roses/_/Mr.+Brownstone</url>
                  <artist>
                    <name>Guns N' Roses</name>
                    <mbid>eeb1195b-f213-4ce1-b28c-8565211f8e43</mbid>
                    <url>http://www.last.fm/music/Guns+N%27+Roses</url>
                  </artist>
                </track>
              </correction>
            </corrections>
        */

        public TrackGetCorrectionAttribute Attribute { get; set; }

        public CorrectionDto Correction { get; set; }
    }

    /// <summary>
    ///     Correction of Track/perforner
    /// </summary>
    public class CorrectionDto : LastFmBaseModel
    {
        public CorrectedTrack Track { get; set; }
    }

    /// <summary>
    ///     Perform itself
    /// </summary>
    public class CorrectedTrack : LastFmBaseModel
    {
        public Guid Mbid { get; set; }
    }

    /// <summary>
    ///     The special attribite of <see cref="CorrectionDto"/>
    /// </summary>
    public class TrackGetCorrectionAttribute
    {
        /*
            @attr":{"Track":"Ayreon","track":"Age of Shadows"}
        */

        public string Track { get; set; }
    }
}