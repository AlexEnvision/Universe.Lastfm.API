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
using Universe.Lastfm.Api.Dto.GetArtists;
using Universe.Lastfm.Api.Helpers;
using Universe.Lastfm.Api.Models;
using Universe.Lastfm.Api.Models.Base;
using Universe.Lastfm.Api.Models.Res.Base;

namespace Universe.Lastfm.Api.Dal.Queries.Performers
{
    /// <summary>
    ///     The query does search of an similar artist of the Last.fm.
    ///     Запрос, ведущий поиск похожих исполнителей на Last.fm. 
    /// <author>Alex Universe</author>
    /// <author>Alex Envision</author>
    /// </summary>
    public class GetSimilarPerformersQuery : LastQuery<GetSimilarPerformersQuery.GetSimilarRequest, GetSimilarPerformersQuery.GetSimilarResponce>
    {
        protected override Func<BaseRequest, BaseResponce> ExecutableBaseFunc =>
            req => Execute(req.As<GetSimilarRequest>());

        /// <summary>
        ///     Get all the artists similar to this artist
        /// </summary>
        /// <param name="request.artist">
        ///     The Artist name.
        /// </param>
        /// <param name="request.page">
        ///     The page number to fetch. Defaults to first page.
        /// </param>
        /// <param name="request.limit">
        ///     The number of results to fetch per page. Defaults to 30.
        /// </param>
        /// <param name="request">
        ///     Request with parameters.
        /// </param>
        /// <returns></returns>
        public override GetSimilarResponce Execute(
            GetSimilarRequest request)
        {
            string artist = request.Performer ?? throw new ArgumentNullException("request.Performer");
            int page = request.Page;
            int limit = request.Limit;

            var sessionResponce = Adapter.GetRequest("artist.getSimilar",
                Argument.Create("api_key", Settings.ApiKey),
                Argument.Create("artist", artist),
                Argument.Create("page", page.ToString()),
                Argument.Create("limit", limit.ToString()),
                Argument.Create("format", "json"),
                Argument.Create("callback", "?"));

            Adapter.FixCallback(sessionResponce);

            var getArtistInfoResponce = ResponceExt.CreateFrom<BaseResponce, GetSimilarResponce>(sessionResponce);
            return getArtistInfoResponce;
        }

        /// <summary>
        ///     The request with full information about Artist of the Last.fm.
        ///     Запрос с полной информацией о поиске Last.fm.
        /// </summary>
        public class GetSimilarRequest : BaseRequest
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

            public string Performer { get; set; }

            public GetSimilarRequest()
            {
                Page = 1;
                Limit = 50;
            }
        }

        /// <summary>
        ///     The responce with full information about similar performers of the Last.fm.
        ///     Ответ с полной информацией о похожих исполнителей Last.fm.
        /// </summary>
        public class GetSimilarResponce : LastFmBaseResponce<ArtistSearchContainer>
        {
        }

        /// <summary>
        ///     The container with information about the top artists listened to by a Artist on the Last.fm.
        ///     Контейнер с информацией о поиске, которые прослушивал пользователь на Last.fm.
        /// </summary>
        public class ArtistSearchContainer : LastFmBaseContainer
        {
            public SimilarArtistsDto SimilarArtists { get; set; }
        }

        /// <summary>
        ///     The full information about track on the Last.fm.
        ///     Полная информация о похожих исполнителях на Last.fm.
        /// </summary>
        public class SimilarArtistsDto
        {
            /*
                 <similarartists artist="Cher">
                  <artist>
                    <name>Sonny & Cher</name>
                    <mbid>3d6e4b6d-2700-458c-9722-9021965a8164</mbid>
                    <match>1</match>
                    <url>www.last.fm/music/Sonny%2B%2526%2BCher</url>
                    <image size="small">http://userserve-ak.last.fm/serve/34/71168880.png</image>
                    <image size="medium">http://userserve-ak.last.fm/serve/64/71168880.png</image>
                    <image size="large">http://userserve-ak.last.fm/serve/126/71168880.png</image>
                    <image size="extralarge">http://userserve-ak.last.fm/serve/252/71168880.png</image>
                    <image size="mega">http://userserve-ak.last.fm/serve/500/71168880/Sonny++Cher.png</image>
                    <streamable>1</streamable>
                  </artist>
                  ...
                </similarartists>
            */

            public SimilarArtistsAttribute Attribute { get; set; }

            public Artist[] Artist { get; set; }
        }

        /// <summary>
        ///     The special attribite of <see cref="SimilarArtistsDto"/>
        /// </summary>
        public class SimilarArtistsAttribute
        {
            /*
                "@attr":
                {
                    "artist": "Ayreon"
                },
            */

            public string Artist { get; set; }
        }
    }
}