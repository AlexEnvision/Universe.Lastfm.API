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
using Universe.Lastfm.Api.Dto.GetTrackInfo;
using Universe.Lastfm.Api.Helpers;
using Universe.Lastfm.Api.Models;
using Universe.Lastfm.Api.Models.Base;
using Universe.Lastfm.Api.Models.Res.Base;

namespace Universe.Lastfm.Api.Dal.Queries.Users
{
    /// <summary>
    ///     The query gets the full information about weekly chart list of an user of the Last.fm.
    ///     Запрос, получающий полную информацию о недельном чарте списков/плэйлистов пользователя Last.fm. 
    /// </summary>
    public class GetUserWeeklyChartListQuery : LastQuery
    {
        /// <summary>
        ///     Get a list of available charts for this user, expressed as date ranges
        ///     which can be sent to the chart services.
        /// </summary>
        /// <param name="user">
        ///     The last.fm username to fetch the charts list for.
        /// </param>
        /// <returns></returns>
        public GetUserWeeklyChartListResponce Execute(
            string user)
        {
            var sessionResponce = Adapter.GetRequest("user.getWeeklyChartList",
                Argument.Create("api_key", Settings.ApiKey),
                Argument.Create("user", user),
                Argument.Create("format", "json"),
                Argument.Create("callback", "?"));

            Adapter.FixCallback(sessionResponce);

            var getTrackInfoResponce =
                ResponceExt.CreateFrom<BaseResponce, GetUserWeeklyChartListResponce>(sessionResponce);
            return getTrackInfoResponce;
        }

        /// <summary>
        ///     The responce with full information about user of the Last.fm.
        ///     Ответ с полной информацией о пользователе Last.fm.
        /// </summary>
        public class GetUserWeeklyChartListResponce : LastFmBaseResponce<UserWeeklyChartListContainer>
        {
        }

        /// <summary>
        ///     The container with information about the weekly chart tracks, that were created/made user on the Last.fm.
        ///     Контейнер с информацией о недельном чарте, которые формировались по пользователь на Last.fm.
        /// </summary>
        public class UserWeeklyChartListContainer : LastFmBaseContainer
        {
            public WeeklyChartListDto WeeklyChartList { get; set; }
        }

        /// <summary>
        ///     The full information about the weekly chart list of an user of the Last.fm.
        ///     Полная информация о недельном чарте пользователя на Last.fm.
        /// </summary>
        public class WeeklyChartListDto
        {
            /*
                 <weeklychartlist user="LFUser">
                  <chart from="1108296002" to="1108900802"/>
                  <chart from="1108900801" to="1109505601"/>
                  ...
                </weeklychartlist>
            */

            public WeeklyChartListAttribute Attribute { get; set; }

            public Chart[] Chart { get; set; }
        }

        /// <summary>
        ///     The special attribite of <see cref="WeeklyChartListDto"/>
        /// </summary>
        public class WeeklyChartListAttribute
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
        public class Chart : LastFmBaseModel
        {
        }
    }
}