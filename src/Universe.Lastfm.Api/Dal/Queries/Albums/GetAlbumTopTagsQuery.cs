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
using Universe.Lastfm.Api.Dto.Common;
using Universe.Lastfm.Api.Helpers;
using Universe.Lastfm.Api.Models;
using Universe.Lastfm.Api.Models.Base;
using Universe.Lastfm.Api.Models.Res.Base;
using static Universe.Lastfm.Api.Dal.Queries.Albums.GetAlbumTopTagsQuery;

namespace Universe.Lastfm.Api.Dal.Queries.Albums
{
    /// <summary>
    ///     The query does AlbumTopTags of an album of the Last.fm.
    ///     Запрос, ведущий поиск альбома на Last.fm. 
    /// <author>Alex Universe</author>
    /// <author>Alex Envision</author>
    /// </summary>
    public class GetAlbumTopTagsQuery : LastQuery<GetAlbumTopTagsRequest, GetAlbumTopTagsResponce>
    {
        protected override Func<BaseRequest, BaseResponce> ExecutableBaseFunc =>
            req => Execute(req.As<GetAlbumTopTagsRequest>());

        /// <summary>
        ///     AlbumTopTags for an album by name. Returns album matches sorted by relevance.
        /// </summary>
        /// <param name="request.album">
        ///     The album name.
        /// </param>
        /// <param name="request.page">
        ///     The page number to fetch. Defaults to first page.
        /// </param>
        /// <param name="request.limit">
        ///     The number of results to fetch per page. Defaults to 30.
        /// </param>
        /// <param name="request">
        /// 
        /// </param>
        /// <returns></returns>
        public GetAlbumTopTagsResponce Execute(
            GetAlbumTopTagsRequest request)
        {
            string album = request.Album;
            string performer = request.Performer;
            string mbid = request.Mbid;
            string autocorrect = request.Autocorrect;

            var sessionResponce = Adapter.GetRequest("album.getTopTags",
                Argument.Create("api_key", Settings.ApiKey),
                Argument.Create("album", album),
                Argument.Create("artist", performer),
                Argument.Create("mbid", mbid),
                Argument.Create("autocorrect", autocorrect),
                Argument.Create("format", "json"),
                Argument.Create("callback", "?"));

            Adapter.FixCallback(sessionResponce);

            var getAlbumInfoResponce = ResponceExt.CreateFrom<BaseResponce, GetAlbumTopTagsResponce>(sessionResponce);
            return getAlbumInfoResponce;
        }

        /// <summary>
        ///     The request with full information about album of the Last.fm.
        ///     Запрос с полной информацией о поиске Last.fm.
        /// </summary>
        public class GetAlbumTopTagsRequest : BaseRequest
        {
            public string Performer { get; set; }

            public string Album { get; set; }

            public string Mbid { get; set; }

            /// <summary>
            ///     Autocorrect[0|1] (Optional) : Transform misspelled artist names into correct artist names,
            ///     returning the correct version instead. The corrected artist name will be returned in the response.
            /// </summary>
            public string Autocorrect { get; set; }

            public GetAlbumTopTagsRequest()
            {
            }
        }

        /// <summary>
        ///     The responce with full information about album of the Last.fm.
        ///     Ответ с полной информацией о поиске Last.fm.
        /// </summary>
        public class GetAlbumTopTagsResponce : LastFmBaseResponce<AlbumTopTagsContainer>
        {
        }

        /// <summary>
        ///     The container with information about the top artists listened to by a album on the Last.fm.
        ///     Контейнер с информацией о поиске, которые прослушивал пользователь на Last.fm.
        /// </summary>
        public class AlbumTopTagsContainer : LastFmBaseContainer
        {
            public AlbumTopTagsDto TopTags { get; set; }
        }

        /// <summary>
        ///     The full information about top tags of an album on the Last.fm.
        ///     Полная информация о топ-тегах альбома на Last.fm.
        /// </summary>
        public class AlbumTopTagsDto
        {
            /*
                 <?xml version="1.0" encoding="utf-8"?>
                 <lfm status="ok">
                    <toptags artist="Radiohead" album="The Bends">
                        <tag>
                            <name>albums I own</name>
                            <count>100</count>
                            <url>http://www.last.fm/tag/albums%20i%20own</url>
                        </tag>
                        ...
                    </toptags>
                 </lfm>
            */

            public AlbumTopTagsAttribute Attribute { get; set; }

            public Tag[] Tag { get; set; }
        }

        /// <summary>
        ///     The special attribite of <see cref="AlbumTopTagsDto"/>
        /// </summary>
        public class AlbumTopTagsAttribute
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