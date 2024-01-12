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

namespace Universe.Lastfm.Api.Dal.Queries.Albums
{
    /// <summary>
    ///     The query gets tags of an album
    /// </summary>
    public class GetAlbumTagsQuery : LastQuery<GetAlbumTagsRequest, GetAlbumTagsResponce>
    {
        protected override Func<BaseRequest, BaseResponce> ExecutableBaseFunc =>
            req => Execute(req.As<GetAlbumTagsRequest>());

        /// <summary>
        ///     Get the tags applied by an individual user to an album on Last.fm.
        ///     To retrieve the list of top tags applied to an album by all users use album.getTopTags.
        /// </summary>
        /// <param name="request.artist">
        ///     The artist name.
        ///     (Required (unless mbid)] 
        /// </param>
        /// <param name="request.album">
        ///     The album name.
        ///     (Required (unless mbid)] 
        /// </param>
        /// <param name="request.user">
        ///     If called in non-authenticated mode you must specify the user to look up
        /// </param>
        /// <param name="request.mbid">
        ///     The musicbrainz id for the album.
        ///     (Optional) 
        /// </param>
        /// <param name="request.autocorrect">
        ///     
        /// </param>
        /// <param name="request"></param>
        /// <returns></returns>
        public override GetAlbumTagsResponce Execute(
            GetAlbumTagsRequest request)
        {
            string album = request.Album;
            string performer = request.Performer;
            string user = request.User;
            string mbid = request.Mbid;
            string autocorrect = request.Autocorrect;

            var sessionResponce = Adapter.GetRequest("album.getTags",
                Argument.Create("artist", performer),
                Argument.Create("album", album),
                Argument.Create("user", user),
                Argument.Create("api_key", Settings.ApiKey),
                Argument.Create("format", "json"),
                Argument.Create("callback", "?"));

            Adapter.FixCallback(sessionResponce);

            var infoResponce = ResponceExt.CreateFrom<BaseResponce, GetAlbumTagsResponce>(sessionResponce);
            return infoResponce;
        }
    }
}