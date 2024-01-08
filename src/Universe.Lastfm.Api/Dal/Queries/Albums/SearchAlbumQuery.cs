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

using Universe.Lastfm.Api.Dto.Base;
using Universe.Lastfm.Api.Dto.GetAlbumInfo;
using Universe.Lastfm.Api.Helpers;
using Universe.Lastfm.Api.Models;
using Universe.Lastfm.Api.Models.Base;
using Universe.Lastfm.Api.Models.Res.Base;

namespace Universe.Lastfm.Api.Dal.Queries.Albums
{
    /// <summary>
    ///     The query does search of an album of the Last.fm.
    ///     Запрос, ведущий поиск альбома на Last.fm. 
    /// </summary>
    public class SearchAlbumQuery : LastQuery
    {
        /// <summary>
        ///     Search for an album by name. Returns album matches sorted by relevance.
        /// </summary>
        /// <param name="album">
        ///     The album name.
        /// </param>
        /// <param name="page">
        ///     The page number to fetch. Defaults to first page.
        /// </param>
        /// <param name="limit">
        ///     The number of results to fetch per page. Defaults to 30.
        /// </param>
        /// <returns></returns>
        public GetAlbumSearchResponce Execute(
            string album,
            int page = 1,
            int limit = 50)
        {
            var sessionResponce = Adapter.GetRequest("album.search",
                Argument.Create("api_key", Settings.ApiKey),
                Argument.Create("album", album),
                Argument.Create("page", page.ToString()),
                Argument.Create("limit", limit.ToString()),
                Argument.Create("format", "json"),
                Argument.Create("callback", "?"));

            Adapter.FixCallback(sessionResponce);

            var getAlbumInfoResponce = ResponceExt.CreateFrom<BaseResponce, GetAlbumSearchResponce>(sessionResponce);
            return getAlbumInfoResponce;
        }

        /// <summary>
        ///     The responce with full information about album of the Last.fm.
        ///     Ответ с полной информацией о поиске Last.fm.
        /// </summary>
        public class GetAlbumSearchResponce : LastFmBaseResponce<AlbumSearchContainer>
        {
        }

        /// <summary>
        ///     The container with information about the top artists listened to by a album on the Last.fm.
        ///     Контейнер с информацией о поиске, которые прослушивал пользователь на Last.fm.
        /// </summary>
        public class AlbumSearchContainer : LastFmBaseContainer
        {
            public SearchDto Results { get; set; }
        }

        /// <summary>
        ///     The full information about track on the Last.fm.
        ///     Полная информация о поиске на Last.fm.
        /// </summary>
        public class SearchDto
        {
            /*
                 <results for="believe">
                  <opensearch:Query role="request" searchTerms="believe" startPage="1"/>
                  <opensearch:totalResults>734</opensearch:totalResults>
                  <opensearch:startIndex>0</opensearch:startIndex>
                  <opensearch:itemsPerPage>20</opensearch:itemsPerPage>
                  <albummatches>
                    <album>
                      <name>Make Believe</name>
                      <artist>Weezer</artist>
                      <id>2025180</id>
                      <url>http://www.last.fm/music/Weezer/Make+Believe</url>
                      <image size="small">http://userserve-ak.last.fm/serve/34/8673675.jpg</image>
                      <image size="medium">http://userserve-ak.last.fm/serve/64/8673675.jpg</image>
                      <image size="large">http://userserve-ak.last.fm/serve/126/8673675.jpg</image>
                      <streamable>0</streamable>
                    </album>
                    ...
                  </albummatches>
                </results>
            */

            public SearchAttribute Attribute { get; set; }

            public AlbumMatchesDto AlbumMatches { get; set; }
        }

        /// <summary>
        ///     The list of matches in a search.
        ///     Список совпадений в поиске.
        /// </summary>
        public class AlbumMatchesDto
        {
            public Album[] Album { get; set; }
        }

        /// <summary>
        ///     The special attribite of <see cref="SearchDto"/>
        /// </summary>
        public class SearchAttribute
        {
            /*
                "@attr":
                {
                "for": "01011001"
                },
            */

            public string For { get; set; }
        }
    }
}