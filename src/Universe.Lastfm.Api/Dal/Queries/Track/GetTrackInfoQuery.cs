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
using Universe.Lastfm.Api.Helpers;
using Universe.Lastfm.Api.Models;
using Universe.Lastfm.Api.Models.Base;
using Universe.Lastfm.Api.Models.Req;
using Universe.Lastfm.Api.Models.Res;

namespace Universe.Lastfm.Api.Dal.Queries.Track
{
    /// <summary>
    ///     The query gets the full information about track on the Last.fm.
    ///     Запрос, получающий полную информацию о трэке на Last.fm. 
    /// <author>Alex Universe</author>
    /// <author>Alex Envision</author>
    /// </summary>
    public class GetTrackInfoQuery : LastQuery<GetTrackInfoRequest, GetTrackInfoResponce>
    {
        protected override Func<BaseRequest, BaseResponce> ExecutableBaseFunc =>
            req => Execute(req.As<GetTrackInfoRequest>());

        /// <summary>
        ///     Get the metadata for a track on Last.fm using the artist/track name or a musicbrainz id.
        /// </summary>
        /// <param name="request.artist">
        ///     The artist name
        ///     (Required (unless mbid))
        /// </param>
        /// <param name="request.track">
        ///     The track name
        ///     (Required (unless mbid))
        /// </param>
        /// <param name="request.mbid">
        ///     The musicbrainz id for the track
        ///     (Optional)
        /// </param>
        /// <param name="request.username">
        ///     he username for the context of the request. If supplied, the user's
        ///     playcount for this track and whether they have loved the track
        ///     is included in the response.
        ///     (Optional)
        /// </param>
        /// <param name="request.autocorrect">
        ///     Transform misspelled artist and track names into correct artist
        ///     and track names, returning the correct version instead.
        ///     The corrected artist and track name will be returned in the response.
        ///     [0|1] (Optional) 
        /// </param>
        /// <param name="request">
        ///     Request with parameters.
        /// </param>
        /// <returns></returns>
        public override GetTrackInfoResponce Execute(
            GetTrackInfoRequest request)
        {
            string artist = request.Performer ?? throw new ArgumentNullException("request.Performer");
            string track = request.Track ?? throw new ArgumentNullException("request.Track");
            string mbid = request.Mbid;
            string username = request.User;
            string autocorrect = request.Autocorrect;

            var sessionResponce = Adapter.GetRequest("track.getInfo",
                Argument.Create("api_key", Settings.ApiKey),
                Argument.Create("artist", artist),
                Argument.Create("track", track),
                Argument.Create("format", "json"),
                Argument.Create("callback", "?"));

            Adapter.FixCallback(sessionResponce);

            var getAlbumInfoResponce = ResponceExt.CreateFrom<BaseResponce, GetTrackInfoResponce>(sessionResponce);
            return getAlbumInfoResponce;
        }
    }
}