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
using Universe.Lastfm.Api.Dto.GetArtists;
using Universe.Lastfm.Api.Helpers;
using Universe.Lastfm.Api.Models;
using Universe.Lastfm.Api.Models.Base;
using Universe.Lastfm.Api.Models.Res.Base;

namespace Universe.Lastfm.Api.Dal.Queries.Performers
{
    /// <summary>
    ///     The query gets performer corrected artist of the Last.fm.
    ///     Запрос, получающий скорректированных исполнителей на Last.fm. 
    /// </summary>
    public class GetPerformerGetTopAlbumsQuery : LastQuery<GetPerformerGetTopAlbumsQuery.GetPerformerGetTopAlbumsRequest, GetPerformerGetTopAlbumsQuery.GetPerformerGetTopAlbumsResponce>
    {
        protected override Func<BaseRequest, BaseResponce> ExecutableBaseFunc =>
            req => Execute(req.As<GetPerformerGetTopAlbumsRequest>());

        /// <summary>
        ///     Get the top albums for an artist on Last.fm, ordered by popularity.
        /// </summary>
        /// <param name="request.artist">
        ///     The artist name.
        /// </param>
        /// <param name="request">
        ///     Request with parameters.
        /// </param>
        /// <returns></returns>
        public override GetPerformerGetTopAlbumsResponce Execute(
            GetPerformerGetTopAlbumsRequest request)
        {
            string artist = request.Performer ?? throw new ArgumentNullException("request.Performer");

            var sessionResponce = Adapter.GetRequest("artist.getTopAlbums",
                Argument.Create("api_key", Settings.ApiKey),
                Argument.Create("artist", artist),
                Argument.Create("format", "json"),
                Argument.Create("callback", "?"));

            Adapter.FixCallback(sessionResponce);

            var getArtistInfoResponce = ResponceExt.CreateFrom<BaseResponce, GetPerformerGetTopAlbumsResponce>(sessionResponce);
            return getArtistInfoResponce;
        }

        /// <summary>
        ///     The request with full information about Artist of the Last.fm.
        ///     Запрос с полной информацией о поиске Last.fm.
        /// </summary>
        public class GetPerformerGetTopAlbumsRequest : BaseRequest
        {
            public string Performer { get; set; }

            public GetPerformerGetTopAlbumsRequest()
            {
            }
        }

        /// <summary>
        ///     The responce with full information about PerformerGetTopAlbums performers of the Last.fm.
        ///     Ответ с полной информацией о похожих исполнителей Last.fm.
        /// </summary>
        public class GetPerformerGetTopAlbumsResponce : LastFmBaseResponce<PerformerContainer>
        {
        }

        /// <summary>
        ///     The container with information about the top artists listened to by a Artist on the Last.fm.
        ///     Контейнер с информацией о поиске, которые прослушивал пользователь на Last.fm.
        /// </summary>
        public class PerformerContainer : LastFmBaseContainer
        {
            public PerformerGetTopAlbumsArtistsDto TopAlbums { get; set; }
        }

        /// <summary>
        ///     The full information about track on the Last.fm.
        ///     Полная информация о похожих исполнителях на Last.fm.
        /// </summary>
        public class PerformerGetTopAlbumsArtistsDto
        {
            /*
                 <topalbums artist="Cher">
                  <album rank="1">
                    <name>Believe</name>
                    <mbid>61bf0388-b8a9-48f4-81d1-7eb02706dfb0</mbid>
                    <listeners>24486</listeners>
                    <url>http://www.last.fm/music/Cher/Believe</url>
                    <image size="small">...</image>
                    <image size=" medium">...</image>
                    <image size="large">...</image>
                  </album>
                  ...
                </topalbums>
            */

            public PerformerGetTopAlbumsAttribute Attribute { get; set; }

            public string Artist { get; set; }

            public Album[] Album { get; set; }
        }

        /// <summary>
        ///     The special attribite of <see cref="PerformerGetTopAlbumsArtistsDto"/>
        /// </summary>
        public class PerformerGetTopAlbumsAttribute
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