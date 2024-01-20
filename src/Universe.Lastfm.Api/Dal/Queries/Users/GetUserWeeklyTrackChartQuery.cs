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
    ///     The query gets the full information about weekly track chart of an user of the Last.fm.
    ///     Запрос, получающий полную информацию о недельном чарте композиций/трэков пользователя Last.fm. 
    /// <author>Alex Universe</author>
    /// <author>Alex Envision</author>
    /// </summary>
    public class GetUserWeeklyTrackChartQuery : LastQuery
    {
        protected override Func<BaseRequest, BaseResponce> ExecutableBaseFunc =>
            req => Execute(req.As<GetUserWeeklyTrackChartRequest>());

        /// <summary>
        ///     Get an track chart for a user profile, for a given date range.
        ///     If no date range is supplied, it will return the most recent Track chart for this user.
        /// </summary>
        /// <param name="request.user">
        ///     The last.fm username to fetch the recent tracks of.
        /// </param>
        /// <param name="request.from">
        ///     The date at which the chart should start from. See User.getChartsList for more.
        /// </param>
        /// <param name="request.to">
        ///     The date at which the chart should end on. See User.getChartsList for more.
        /// </param>
        /// <param name="request">
        ///     Request with parameters.
        /// </param>
        /// <returns></returns>
        public GetUserWeeklyTrackChartResponce Execute(
            GetUserWeeklyTrackChartRequest request)
        {
            string user = request.User;
            string from = request.From;
            string to = request.To;

            var sessionResponce = Adapter.GetRequest("user.getWeeklyTrackChart",
                Argument.Create("api_key", Settings.ApiKey),
                Argument.Create("user", user),
                Argument.Create("from", from),
                Argument.Create("to", to),
                Argument.Create("format", "json"),
                Argument.Create("callback", "?"));

            Adapter.FixCallback(sessionResponce);

            var getTrackInfoResponce =
                ResponceExt.CreateFrom<BaseResponce, GetUserWeeklyTrackChartResponce>(sessionResponce);
            return getTrackInfoResponce;
        }

        /// <summary>
        ///     The request for full information about user of the Last.fm.
        ///     Запрос на полную информациею о пользователе Last.fm.
        /// </summary>
        public class GetUserWeeklyTrackChartRequest : BaseRequest
        {
            public string User { get; set; }

            public string From { get; set; }

            public string To { get; set; }


            public GetUserWeeklyTrackChartRequest()
            {
                From = null;
                To = null;
            }
        }

        /// <summary>
        ///     The responce with full information about user of the Last.fm.
        ///     Ответ с полной информацией о пользователе Last.fm.
        /// </summary>
        public class GetUserWeeklyTrackChartResponce : LastFmBaseResponce<UserWeeklyTrackChartContainer>
        {
        }

        /// <summary>
        ///     The container with information about the weekly chart tracks, that listened by a user on the Last.fm.
        ///     Контейнер с информацией о недельном чарте композиций, которые прослушивал пользователь на Last.fm.
        /// </summary>
        public class UserWeeklyTrackChartContainer : LastFmBaseContainer
        {
            public WeeklyTrackChartDto WeeklyTrackChart { get; set; }
        }

        /// <summary>
        ///     The full information about the weekly track chart of an user of the Last.fm.
        ///     Полная информация о недельном чарте трэков/композиций пользователя на Last.fm.
        /// </summary>
        public class WeeklyTrackChartDto
        {
            /*
                 <weeklytrackchart user="joanofarctan" from="1212321600" to="1212926400">
                  <track rank="1">
                    <artist mbid="17b0d7f1-fad3-404e-87ae-874e6e158c3a">Dirk Leyers</artist>
                    <name>Wellen</name>
                    <mbid/>
                    <playcount>3</playcount>
                    <url>http://www.last.fm/music/Dirk+Leyers/_/Wellen</url>
                  </track>
                  ...
                </weeklytrackchart>
            */

            public WeeklyTrackChartAttribute Attribute { get; set; }

            public TrackFull[] Track { get; set; }
        }

        /// <summary>
        ///     The special attribite of <see cref="WeeklyTrackChartDto"/>
        /// </summary>
        public class WeeklyTrackChartAttribute
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