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
using static Universe.Lastfm.Api.Dal.Queries.Users.GetUserTopTagsQuery;

namespace Universe.Lastfm.Api.Dal.Queries.Users
{
    /// <summary>
    ///     The query gets the full information about top tags/genres of an user of the Last.fm.
    ///     Запрос, получающий полную информацию о топ тэгах/жанрах пользователя Last.fm. 
    /// </summary>
    public class GetUserTopTagsQuery : LastQuery<GetUserTopTagsRequest, GetUserTopTagsResponce>
    {
        protected override Func<BaseRequest, BaseResponce> ExecutableBaseFunc =>
            req => Execute(req.As<GetUserTopTagsRequest>());

        /// <summary>
        ///     Get the top tags used by this user.
        /// </summary>
        /// <param name="request.user">
        ///     The user name to fetch top tags for.
        /// </param>
        /// <param name="request.limit">
        ///     The number of results to fetch per page. Defaults to 50.
        /// </param>
        /// <param name="request">
        ///     Request with parameters.
        /// </param>
        /// <returns></returns>
        public override GetUserTopTagsResponce Execute(
            GetUserTopTagsRequest request)
        {
            string user = request.User;
            int limit = request.Limit;

            var sessionResponce = Adapter.GetRequest("user.getTopTags",
                Argument.Create("api_key", Settings.ApiKey),
                Argument.Create("user", user),
                Argument.Create("format", "json"),
                Argument.Create("limit", limit.ToString()),
                Argument.Create("callback", "?"));

            Adapter.FixCallback(sessionResponce);

            var getAlbumInfoResponce = ResponceExt.CreateFrom<BaseResponce, GetUserTopTagsResponce>(sessionResponce);
            return getAlbumInfoResponce;
        }

        /// <summary>
        ///     The responce with full information about user of the Last.fm.
        ///     Ответ с полной информацией о пользователе Last.fm.
        /// </summary>
        public class GetUserTopTagsRequest : BaseRequest
        {
            public string User { get; set; }

            public int Limit { get; set; }

            public GetUserTopTagsRequest()
            {
                Limit = 50;
            }
        }

        /// <summary>
        ///     The responce with full information about user of the Last.fm.
        ///     Ответ с полной информацией о пользователе Last.fm.
        /// </summary>
        public class GetUserTopTagsResponce : LastFmBaseResponce<UserTopTagsContainer>
        {
        }

        /// <summary>
        ///     The container with information about the top tags listened to by a user on the Last.fm.
        ///     Контейнер с информацией о топ-исполнителях, которые прослушивал пользователь на Last.fm.
        /// </summary>
        public class UserTopTagsContainer : LastFmBaseContainer
        {
            public TopTagsDto TopTags { get; set; }
        }

        /// <summary>
        ///     The full information about track on the Last.fm.
        ///     Полная информация о трэке на Last.fm.
        /// </summary>
        public class TopTagsDto
        {
            /*
                 <toptags user="LFUser">
                   <tag>
                   <name>rock</name>
                   <count>12</count>
                   <url>www.last.fm/tag/rock</url>
                   </tag>
                   ...
                 </toptags>
            */

            public TopTagsAttribute Attribute { get; set; }

            public Tag[] Tag { get; set; }
        }

        /// <summary>
        ///     The special attribite of <see cref="TopTagsDto"/>
        /// </summary>
        public class TopTagsAttribute
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
    }
}