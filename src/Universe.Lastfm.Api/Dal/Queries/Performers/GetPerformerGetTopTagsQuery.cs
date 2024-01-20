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

namespace Universe.Lastfm.Api.Dal.Queries.Performers
{
    /// <summary>
    ///     The query gets performer tags of the Last.fm.
    ///     Запрос, получающий тэги исполнителей на Last.fm. 
    /// <author>Alex Universe</author>
    /// <author>Alex Envision</author>
    /// </summary>
    public class GetPerformerGetTopTagsQuery : LastQuery<GetPerformerGetTopTagsQuery.GetPerformerGetTopTagsRequest, GetPerformerGetTopTagsQuery.GetPerformerGetTopTagsResponce>
    {
        protected override Func<BaseRequest, BaseResponce> ExecutableBaseFunc =>
            req => Execute(req.As<GetPerformerGetTopTagsRequest>());

        /// <summary>
        ///     Get the top tags for an artist on Last.fm, ordered by popularity.
        /// </summary>
        /// <param name="request.artist">
        ///     The artist name.
        /// </param>
        /// <param name="request">
        ///     Request with parameters.
        /// </param>
        /// <returns></returns>
        public override GetPerformerGetTopTagsResponce Execute(
            GetPerformerGetTopTagsRequest request)
        {
            string artist = request.Performer ?? throw new ArgumentNullException("request.Performer");

            var sessionResponce = Adapter.GetRequest("artist.getTopTags",
                Argument.Create("api_key", Settings.ApiKey),
                Argument.Create("artist", artist),
                Argument.Create("format", "json"),
                Argument.Create("callback", "?"));

            Adapter.FixCallback(sessionResponce);

            var getArtistInfoResponce = ResponceExt.CreateFrom<BaseResponce, GetPerformerGetTopTagsResponce>(sessionResponce);
            return getArtistInfoResponce;
        }

        /// <summary>
        ///     The request with full information about Artist of the Last.fm.
        ///     Запрос с полной информацией о поиске Last.fm.
        /// </summary>
        public class GetPerformerGetTopTagsRequest : BaseRequest
        {
            public string Performer { get; set; }

            public GetPerformerGetTopTagsRequest()
            {
            }
        }

        /// <summary>
        ///     The responce with full information about PerformerGetTopTags performers of the Last.fm.
        ///     Ответ с полной информацией о похожих исполнителей Last.fm.
        /// </summary>
        public class GetPerformerGetTopTagsResponce : LastFmBaseResponce<PerformerContainer>
        {
        }

        /// <summary>
        ///     The container with information about the top artists listened to by a Artist on the Last.fm.
        ///     Контейнер с информацией о поиске, которые прослушивал пользователь на Last.fm.
        /// </summary>
        public class PerformerContainer : LastFmBaseContainer
        {
            public PerformerGetTopTagsArtistsDto TopTags { get; set; }
        }

        /// <summary>
        ///     The full information about track on the Last.fm.
        ///     Полная информация о похожих исполнителях на Last.fm.
        /// </summary>
        public class PerformerGetTopTagsArtistsDto
        {
            /*
                <toptags artist="Cher">
                  <tag>
                    <name>pop</name>
                    <url>http://www.last.fm/tag/pop</url>
                  </tag>
                  ...
                </toptags>
            */

            public PerformerGetTopTagsAttribute Attribute { get; set; }

            public string Artist { get; set; }

            public Tag[] Tag { get; set; }
        }

        /// <summary>
        ///     The special attribite of <see cref="PerformerGetTopTagsArtistsDto"/>
        /// </summary>
        public class PerformerGetTopTagsAttribute
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