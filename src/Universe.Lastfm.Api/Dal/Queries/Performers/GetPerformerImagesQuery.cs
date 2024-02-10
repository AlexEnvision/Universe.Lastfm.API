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
using System.Collections.Generic;
using System.Security.Policy;
using Newtonsoft.Json;
using Universe.Diagnostic.Logger;
using Universe.Diagnostic.Utilities;
using Universe.Helpers.Extensions;
using Universe.Lastfm.Api.Algorithm;
using Universe.Lastfm.Api.Algorithm.Searchers;
using Universe.Lastfm.Api.Dto.Base;
using Universe.Lastfm.Api.Dto.GetArtistInfo;
using Universe.Lastfm.Api.Helpers;
using Universe.Lastfm.Api.Models;
using Universe.Lastfm.Api.Models.Base;
using Universe.Lastfm.Api.Models.Res.Base;
using Universe.Types.Collection;

namespace Universe.Lastfm.Api.Dal.Queries.Performers
{
    /// <summary>
    ///     The query gets performer images of the Last.fm.
    ///     Запрос, получающий изображения исполнителей на Last.fm. 
    /// <author>Alex Universe</author>
    /// <author>Alex Envision</author>
    /// </summary>
    public class GetPerformerImagesQuery : LastQuery<GetPerformerImagesQuery.GetPerformerImagesRequest, GetPerformerImagesQuery.GetPerformerImagesResponce>
    {
        protected override Func<BaseRequest, BaseResponce> ExecutableBaseFunc =>
            req => Execute(req.As<GetPerformerImagesRequest>());

        /// <summary>
        ///     Use the last.fm corrections data to check whether the supplied artist
        ///     has a correction to a canonical artist
        /// </summary>
        /// <param name="request.artist">
        ///     The artist name.
        /// </param>
        /// <param name="request">
        ///     Request with parameters.
        /// </param>
        /// <returns></returns>
        public override GetPerformerImagesResponce Execute(
            GetPerformerImagesRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (request.Performer == null) 
                throw new ArgumentNullException(nameof(request.Performer));

            if (request.Performer.Url.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(request.Performer.Url));

            var artist = request.Performer;
            var url = artist.Url;

            var engine = new ImageBrowserNavigationEngine(Settings, request.Log);
            var result = engine.Run(new ImageNavigateParameters
            {
                SiteNav = url,
                Limit = request.Limit
            });

            var serviceAnswer = JsonConvert.SerializeObject(result, Formatting.Indented);

            return new GetPerformerImagesResponce {
                SmallLinkImages = result.SmallSizeImageLinks,
                LargeSizeImageLinks = result.LargeSizeImageLinks,
                ServiceAnswer = serviceAnswer
            };
        }

        /// <summary>
        ///     The request with full information about Artist of the Last.fm.
        ///     Запрос с полной информацией о поиске Last.fm.
        /// </summary>
        public class GetPerformerImagesRequest : BaseRequest
        {
            public int Limit { get; set; }

            public ArtistFull Performer { get; set; }

            public IUniverseLogger Log { get; set; }

            public GetPerformerImagesRequest()
            {
            }
        }

        /// <summary>
        ///     The responce with full information about PerformerImages performers of the Last.fm.
        ///     Ответ с полной информацией о похожих исполнителей Last.fm.
        /// </summary>
        public class GetPerformerImagesResponce : LastFmBaseResponce<PerformerContainer>
        {
            public List<LinkInformation> SmallLinkImages { get; set; }

            public MatList<LinkImageInformation> LargeSizeImageLinks { get; set; }
        }

        /// <summary>
        ///     The container with information about the top artists listened to by a artist on the Last.fm.
        ///     Контейнер с информацией о поиске, которые прослушивал пользователь на Last.fm.
        /// </summary>
        public class PerformerContainer : LastFmBaseContainer
        {
            public PerformerImagesArtistsDto Corrections { get; set; }
        }

        /// <summary>
        ///     The full information about track on the Last.fm.
        ///     Полная информация о похожих исполнителях на Last.fm.
        /// </summary>
        public class PerformerImagesArtistsDto
        {
            public PerformerImagesArtistAttribute Attribute { get; set; }

            public CorrectionDto Correction { get; set; }
        }

        /// <summary>
        ///     Correction of artist/perforner
        /// </summary>
        public class CorrectionDto : LastFmBaseModel
        {
            public CorrectedArtist Artist { get; set; }
        }

        /// <summary>
        ///     Perform itself
        /// </summary>
        public class CorrectedArtist : LastFmBaseModel
        {
            public Guid Mbid { get; set; }
        }

        /// <summary>
        ///     The special attribite of <see cref="PerformerImagesArtistsDto"/>
        /// </summary>
        public class PerformerImagesArtistAttribute
        {
            /*
                "@attr":
                {
                    "index": "0"
                },
            */

            public string Index { get; set; }
        }
    }
}