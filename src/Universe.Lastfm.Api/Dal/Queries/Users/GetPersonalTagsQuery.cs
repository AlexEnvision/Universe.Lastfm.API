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
using Universe.Lastfm.Api.Dto.GetArtists;
using Universe.Lastfm.Api.Dto.GetTrackInfo;
using Universe.Lastfm.Api.Helpers;
using Universe.Lastfm.Api.Models;
using Universe.Lastfm.Api.Models.Base;
using Universe.Lastfm.Api.Models.Res.Base;

namespace Universe.Lastfm.Api.Dal.Queries.Users
{
    /// <summary>
    ///     The query gets the full information about personal tags of an user of the Last.fm.
    ///     Запрос, получающий полную информацию о персональных тэгах пользователя Last.fm. 
    /// </summary>
    public class GetPersonalTagsQuery : LastQuery
    {
        public enum TaggingType
        {
            Artist,
            Album,
            Track
        }

        /// <summary>
        ///     Get the user's personal tags.
        /// </summary>
        /// <param name="user">
        ///     The user who performed the taggings.
        /// </param>
        /// <param name="tag">
        ///     The tag you're interested in.
        /// </param>
        /// <param name="limit">
        ///     The number of results to fetch per page. Defaults to 50.
        /// </param>
        /// <param name="taggingtype">
        ///     The type of items which have been tagged.
        ///     [artist|album|track]
        /// </param>
        /// <param name="page">
        ///     The page number to fetch. Defaults to first page.
        /// </param>
        /// <returns></returns>
        public GetUserPersonalTagsResponce Execute(
            string user,
            string tag,
            string taggingtype,
            int limit = 50,
            int page = 1)
        {
            var sessionResponce = Adapter.GetRequest("user.getPersonalTags",
                Argument.Create("api_key", Settings.ApiKey),
                Argument.Create("user", user),
                Argument.Create("tag", tag),
                Argument.Create("taggingtype", taggingtype),
                Argument.Create("page", page.ToString()),
                Argument.Create("limit", limit.ToString()),
                Argument.Create("format", "json"),
                Argument.Create("callback", "?"));

            Adapter.FixCallback(sessionResponce);

            var getAlbumInfoResponce = ResponceExt.CreateFrom<BaseResponce, GetUserPersonalTagsResponce>(sessionResponce);
            return getAlbumInfoResponce;
        }

        /// <summary>
        ///     The responce with full information about user of the Last.fm.
        ///     Ответ с полной информацией о пользователе Last.fm.
        /// </summary>
        public class GetUserPersonalTagsResponce : LastFmBaseResponce<UserPersonalTagsContainer>
        {
        }

        /// <summary>
        ///     The container with information about the person tags used by a user on the Last.fm.
        ///     Контейнер с информацией о персональных тэгах, которые использовал пользователь на Last.fm.
        /// </summary>
        public class UserPersonalTagsContainer : LastFmBaseContainer
        {
            public PersonalTagsDto Taggings { get; set; }
        }

        /// <summary>
        ///     The full information about tag on the Last.fm.
        ///     Полная информация о тэге на Last.fm.
        /// </summary>
        public class PersonalTagsDto
        {
            /*
                 <taggings user="LFUser" tag="rock" page="1" perPage="50" totalPages="1" total="11">
                  <artists>
                    <artist>
                      <name>John Hammond</name>
                      <mbid>d83e599c-2d5a-44ec-b727-587e1455b1b5</mbid>
                      <url>http://www.last.fm/music/John+Hammond</url>
                      <streamable>1</streamable>
                      <image size="small">http://userserve-ak.last.fm/serve/34/255418.jpg</image>
                      <image size="medium">http://userserve-ak.last.fm/serve/64/255418.jpg</image>
                      <image size="large">http://userserve-ak.last.fm/serve/126/255418.jpg</image>
                      <image size="extralarge">http://userserve-ak.last.fm/serve/252/255418.jpg</image>
                      <image size="mega">http://userserve-ak.last.fm/serve/_/255418/John+Hammond.jpg</image>
                    </artist>
                  </artists>
                </taggings>
            */

            public PersonalTagsAttribute Attribute { get; set; }

            public Artist[] Artists { get; set; }
        }

        /// <summary>
        ///     The special attribite of <see cref="PersonalTagsDto"/>
        /// </summary>
        public class PersonalTagsAttribute
        {
            /*
                 "@attr":
                 {
                    "page": "1",
                    "perPage": "50",
                    "total": "362",
                    "totalPages": "8",
                    "user": "LFUser"
                 },
            */

            public string Page { get; set; }

            public string PerPage { get; set; }

            public string Total { get; set; }

            public string TotalPages { get; set; }

            public string User { get; set; }
        }

        /// <summary>
        ///     The special class of the user personal tag
        /// </summary>
        public class PersonalTag
        {
        }
    }
}