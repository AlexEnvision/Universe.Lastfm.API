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

namespace Universe.Lastfm.Api.Dal.Queries.Performers
{
    /// <summary>
    ///     The query of getting the tags applied by an individual user to an artist on Last.fm.
    ///     If accessed as an authenticated service /and/ you don't supply a user parameter then
    ///     this service will return tags for the authenticated user.
    ///     To retrieve the list of top tags applied to an artist by all users use artist.getTopTags.
    /// </summary>
    public class GetPerformersTagsQuery : LastQuery<GetPerformerTagsRequest, GetArtistTagsResponce>
    {
        protected override Func<BaseRequest, BaseResponce> ExecutableBaseFunc =>
            req => Execute(req.As<GetPerformerTagsRequest>());

        /// <summary>
        ///     The query of getting the tags applied by an individual user to an artist on Last.fm.
        ///     If accessed as an authenticated service /and/ you don't supply a user parameter then
        ///     this service will return tags for the authenticated user.
        ///     To retrieve the list of top tags applied to an artist by all users use artist.getTopTags.
        /// </summary>
        /// <param name="request.artist">
        ///     The artist name
        /// </param>
        /// <param name="request.user">
        ///     If called in non-authenticated mode you must specify the user to look up
        /// </param>
        /// <param name="request">
        ///     Request with parameters, that described before.
        /// </param>
        /// <returns></returns>
        public override GetArtistTagsResponce Execute(
            GetPerformerTagsRequest request)
        {
            string artist = request.Performer ?? throw new ArgumentNullException("request.Performer");
            string user = request.User ?? throw new ArgumentNullException("request.User");

            var sessionResponce = Adapter.GetRequest("artist.getTags",
                Argument.Create("artist", artist),
                Argument.Create("user", user),
                Argument.Create("api_key", Settings.ApiKey),
                Argument.Create("format", "json"),
                Argument.Create("callback", "?"));

            Adapter.FixCallback(sessionResponce);

            var infoResponce = ResponceExt.CreateFrom<BaseResponce, GetArtistTagsResponce>(sessionResponce);
            return infoResponce;
        }
    }
}