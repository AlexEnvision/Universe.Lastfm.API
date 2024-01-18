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
using Universe.Lastfm.Api.Dto.GetArtistInfo;
using Universe.Lastfm.Api.Helpers;
using Universe.Lastfm.Api.Models;
using Universe.Lastfm.Api.Models.Base;
using Universe.Lastfm.Api.Models.Req.Genre;
using Universe.Lastfm.Api.Models.Res.Base;

namespace Universe.Lastfm.Api.Dal.Queries.Tags
{
    /// <summary>
    ///     The query gets the top artists tagged by this tag, ordered by tag count.
    /// </summary>
    public class GetTagTopArtistsQuery : LastQuery<GetTagTopArtistsRequest, GetTagTopArtistsResponce>
    {
        protected override Func<BaseRequest, BaseResponce> ExecutableBaseFunc =>
            req => Execute(req.As<GetTagTopArtistsRequest>());

        /// <summary>
        ///     Get the top artists tagged by this tag, ordered by tag count.
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
        public override GetTagTopArtistsResponce Execute(GetTagTopArtistsRequest request)
        {
            string tag = request.Tag ?? throw new ArgumentNullException("request.Tag");
            int page = request.Page;
            int limit = request.Limit;

            var sessionResponce = Adapter.GetRequest("tag.getTopArtists",
                Argument.Create("tag", tag),
                Argument.Create("page", page.ToString()),
                Argument.Create("limit", limit.ToString()),
                Argument.Create("api_key", Settings.ApiKey),
                Argument.Create("format", "json"),
                Argument.Create("callback", "?"));

            Adapter.FixCallback(sessionResponce);

            var infoResponce = ResponceExt.CreateFrom<BaseResponce, GetTagTopArtistsResponce>(sessionResponce);
            return infoResponce;
        }
    }

    public class GetTagTopArtistsResponce : LastFmBaseResponce<TagTopArtistsContainer>
    {
    }

    public class TagTopArtistsContainer : LastFmBaseContainer
    {
        /*
            <topartists tag="Disco">
              <artist rank="">
                <name>ABBA</name>
                <mbid>d87e52c5-bb8d-4da8-b941-9f4928627dc8</mbid>
                <url>http://www.last.fm/music/ABBA</url>
                <streamable>1</streamable>
                <image size="small">...</image>
                <image size="medium">...</image>
                <image size="large">...</image>
              </artist>
              ...
            </topartists>
         */

        public TopArtistsTags TopArtists { get; set; }
    }

    public class TopArtistsTags : LastFmBaseModel
    {
        public ArtistFull[] Artist { get; set; }
    }
}