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
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Newtonsoft.Json;
using Universe.Helpers.Extensions;
using Universe.Lastfm.Api.Algorithm;
using Universe.Lastfm.Api.Dto.Common;
using Universe.Lastfm.Api.Dto.GetAlbumInfo;
using Universe.Lastfm.Api.Models.Base;
using Universe.Lastfm.Api.Models.Req;
using Universe.Lastfm.Api.Models.Res;

namespace Universe.Lastfm.Api.Dal.Queries.Albums
{
    /// <summary>
    ///      The query gets wiki for an album on Last.fm using the album name.
    /// <author>Alex Universe</author>
    /// <author>Alex Envision</author>
    /// </summary>
    public class GetAlbumWikiQuery : LastQuery<GetAlbumWikiRequest, GetAlbumWikiResponce>
    {
        protected override Func<BaseRequest, BaseResponce> ExecutableBaseFunc =>
            req => Execute(req.As<GetAlbumWikiRequest>());

        /// <summary>
        ///     Get the metadata and tracklist for an album on Last.fm using the album name or a musicbrainz id.
        /// </summary>
        /// <param name="request.album">
        ///     The album model, that ges by query <see cref="GetAlbumInfoQuery"/>
        ///     Required(unless mbid)
        /// </param>
        /// <param name="request">
        ///     Request with parameters.
        /// </param>
        /// <returns></returns>
        public override GetAlbumWikiResponce Execute(
            GetAlbumWikiRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (request.Album == null)
                throw new ArgumentNullException(nameof(request.Album));

            if (request.Album.Url.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(request.Album.Url));

            var album = request.Album;
            var url = album.Url;

            var engine = new DescriptionBrowserNavigationEngine(Settings, request.Log);
            var result = engine.Run(new DescriptionNavigateParameters
            {
                SiteNav = url,
                Limit = request.Limit
            });

            var serviceAnswer = JsonConvert.SerializeObject(result, Formatting.Indented);

            if (result.Description == null || result.Description.Count == 0)
                return new GetAlbumWikiResponce
                {
                    ServiceAnswer = serviceAnswer,
                    IsSuccessful = false
                };

            var innerHtmls = result.Description.Select(x => x.InnerHtml).ToArray();

            var content = string.Join("; ", innerHtmls);
            var summary = content.CutString(255);

            var wiki = new Wiki {
                Content = content,
                Summary = summary
            };

            var albumWikiContainer = new AlbumWikiContainer()
            {
                Description = result.Description,
                Wiki = wiki,
                IsSuccessful = true,
                Message = "ОК"
            };

            var albumWikiContainerSfy = JsonConvert.SerializeObject(albumWikiContainer, Formatting.Indented);

            return new GetAlbumWikiResponce
            {
                ServiceAnswer = albumWikiContainerSfy,
                IsSuccessful = true,
                Message = "ОК"
            };
        }
    }
}