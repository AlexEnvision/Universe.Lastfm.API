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
using Universe.Lastfm.Api.Dto.GetAlbumInfo;
using Universe.Lastfm.Api.Helpers;
using Universe.Lastfm.Api.Models;
using Universe.Lastfm.Api.Models.Base;
using Universe.Lastfm.Api.Models.Req.Genre;
using Universe.Lastfm.Api.Models.Res.Base;

namespace Universe.Lastfm.Api.Dal.Queries.Tags
{
    /// <summary>
    ///     The query gets the top albums tagged by this tag, ordered by tag count.
    /// </summary>
    public class GetTagTopAlbumsQuery : LastQuery<GetTagTopAlbumsRequest, GetTagTopAlbumsResponce>
    {
        protected override Func<BaseRequest, BaseResponce> ExecutableBaseFunc =>
            req => Execute(req.As<GetTagTopAlbumsRequest>());

        /// <summary>
        ///     Get the top albums tagged by this tag, ordered by tag count.
        /// </summary>
        /// <param name="request.tag">
        ///     The tag name.
        ///     (Required) 
        /// </param>
        /// <param name="request.page">
        ///     The page number to fetch. Defaults to first page.
        /// </param>
        /// <param name="request.limit">
        ///     The number of results to fetch per page. Defaults to 50.
        /// </param>
        /// <param name="request">
        ///     Request with parameters for a getting of some tag.
        /// </param>
        /// <returns></returns>
        public override GetTagTopAlbumsResponce Execute(GetTagTopAlbumsRequest request)
        {
            string tag = request.Tag ?? throw new ArgumentNullException("request.Tag");
            int page = request.Page;
            int limit = request.Limit;

            var sessionResponce = Adapter.GetRequest("tag.getTopAlbums",
                Argument.Create("tag", tag),
                Argument.Create("page", page.ToString()),
                Argument.Create("limit", limit.ToString()),
                Argument.Create("api_key", Settings.ApiKey),
                Argument.Create("format", "json"),
                Argument.Create("callback", "?"));

            Adapter.FixCallback(sessionResponce);

            var infoResponce = ResponceExt.CreateFrom<BaseResponce, GetTagTopAlbumsResponce>(sessionResponce);
            return infoResponce;
        }
    }

    public class GetTagTopAlbumsResponce : LastFmBaseResponce<TagTopAlbumsContainer>
    {
    }

    public class TagTopAlbumsContainer : LastFmBaseContainer
    {
        /*
            <albums tag="Disco">
              <album rank="">
                <name>Overpowered</name>
                <mbid/>
                <url>
                  http://www.last.fm/music/Róisín+Murphy/Overpowered
                </url>
                <artist>
                  <name>Róisín Murphy</name>
                  <mbid>4c56405d-ba8e-4283-99c3-1dc95bdd50e7</mbid>
                  <url>http://www.last.fm/music/Róisín+Murphy</url>
                </artist>
                <image size="small">...</image>
                <image size="medium">...</image>
                <image size="large">...</image>
              </album>
              ...
            </albums>
         */

        public TopAlbumsTags Albums { get; set; }
    }

    public class TopAlbumsTags : LastFmBaseModel
    {
        public AlbumFull[] Album { get; set; }
    }
}