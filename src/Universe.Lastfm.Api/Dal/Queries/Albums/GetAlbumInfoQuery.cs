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
using static Universe.Lastfm.Api.Dal.Queries.Albums.GetAlbumImagesQuery;

namespace Universe.Lastfm.Api.Dal.Queries.Albums
{
    /// <summary>
    ///      The query gets metadata and tracklist for an album on Last.fm using the album name or a musicbrainz id
    /// <author>Alex Universe</author>
    /// <author>Alex Envision</author>
    /// </summary>
    public class GetAlbumInfoQuery : LastQuery<GetAlbumInfoRequest, GetAlbumInfoResponce>
    {
        protected override Func<BaseRequest, BaseResponce> ExecutableBaseFunc =>
            req => Execute(req.As<GetAlbumInfoRequest>());

        /// <summary>
        ///     Get the metadata and tracklist for an album on Last.fm using the album name or a musicbrainz id.
        /// </summary>
        /// <param name="request.performer">
        ///     The artist name
        ///     Required(unless mbid)
        /// </param>
        /// <param name="request.album">
        ///     The album name
        ///     Required(unless mbid)
        /// </param>
        /// <param name="request.lang">
        ///     The language to return the biography in, expressed as an ISO 639 alpha-2 code.
        ///     Optional
        /// </param>
        /// <param name="request">
        ///     Request with parameters.
        /// </param>
        /// <returns></returns>
        public override GetAlbumInfoResponce Execute(
            GetAlbumInfoRequest request)
        {
            //artist(Required(unless mbid)] : The artist name
            //album(Required(unless mbid)] : The album name

            string artist = request.Performer;
            string album = request.Album;

            string lang = request.Lang;

            var sessionResponce = Adapter.GetRequest("album.getInfo",
                Argument.Create("api_key", Settings.ApiKey),
                Argument.Create("artist", artist),
                Argument.Create("album", album),
                Argument.Create("lang", lang),
                Argument.Create("format", "json"),
                Argument.Create("callback", "?"));

            Adapter.FixCallback(sessionResponce);

            var getAlbumInfoResponce = ResponceExt.CreateFrom<BaseResponce, GetAlbumInfoResponce>(sessionResponce);
            return getAlbumInfoResponce;
        }
    }
}