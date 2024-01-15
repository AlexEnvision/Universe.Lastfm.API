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
using Unity;
using Universe.Lastfm.Api.Dto.Base;
using Universe.Lastfm.Api.Dto.GetArtistInfo;
using Universe.Lastfm.Api.Dto.GetTrackInfo;
using Universe.Lastfm.Api.Helpers;
using Universe.Lastfm.Api.Models;
using Universe.Lastfm.Api.Models.Base;
using Universe.Lastfm.Api.Models.Req.Genre;
using Universe.Lastfm.Api.Models.Res.Base;

namespace Universe.Lastfm.Api.Dal.Queries.Tags
{
    /// <summary>
    ///     The query gets the top tracks tagged by this tag, ordered by tag count.
    /// </summary>
    public class GetTagTopTracksQuery : LastQuery<GetTagTopTracksRequest, GetTagTopTracksResponce>
    {
        protected override Func<BaseRequest, BaseResponce> ExecutableBaseFunc =>
            req => Execute(req.As<GetTagTopTracksRequest>());

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
        public override GetTagTopTracksResponce Execute(GetTagTopTracksRequest request)
        {
            string tag = request.Tag ?? throw new ArgumentNullException("request.Tag");
            int page = request.Page;
            int limit = request.Limit;

            var sessionResponce = Adapter.GetRequest("tag.getTopTracks",
                Argument.Create("tag", tag),
                Argument.Create("page", page.ToString()),
                Argument.Create("limit", limit.ToString()),
                Argument.Create("api_key", Settings.ApiKey),
                Argument.Create("format", "json"),
                Argument.Create("callback", "?"));

            Adapter.FixCallback(sessionResponce);

            var infoResponce = ResponceExt.CreateFrom<BaseResponce, GetTagTopTracksResponce>(sessionResponce);
            return infoResponce;
        }
    }

    public class GetTagTopTracksResponce : LastFmBaseResponce<TagTopTracksContainer>
    {
    }

    public class TagTopTracksContainer : LastFmBaseContainer
    {
        /*
            <tracks tag="Disco">
              <track rank="">
                <name>Stayin' Alive</name>
                <mbid/>
                <url>
                  http://www.last.fm/music/Bee+Gees/_/Stayin'+Alive
                </url>
                <streamable fulltrack="0">1</streamable>
                <artist>
                  <name>Bee Gees</name>
                  <mbid>bf0f7e29-dfe1-416c-b5c6-f9ebc19ea810</mbid>
                  <url>http://www.last.fm/music/Bee+Gees</url>
                </artist>
                <image size="small">...</image>
                <image size="medium">...</image>
                <image size="large">...</image>
              </track>
              ...
            </tracks>
         */

        public TopTracksTags Tracks { get; set; }
    }

    public class TopTracksTags : LastFmBaseModel
    {
        public TrackFull[] Track { get; set; }
    }
}