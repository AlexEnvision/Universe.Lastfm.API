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
using Universe.Lastfm.Api.Dto.GetArtistInfo;
using Universe.Lastfm.Api.Helpers;
using Universe.Lastfm.Api.Models;
using Universe.Lastfm.Api.Models.Base;
using Universe.Lastfm.Api.Models.Res.Base;

namespace Universe.Lastfm.Api.Dal.Queries.Users
{
    /// <summary>
    ///     The query gets the full information about weekly artists/performers chart of an user of the Last.fm.
    ///     Запрос, получающий полную информацию о недельном чарте исполнителей у пользователя Last.fm. 
    /// <author>Alex Universe</author>
    /// <author>Alex Envision</author>
    /// </summary>
    public class GetUserWeeklyArtistChartQuery : LastQuery
    {
        protected override Func<BaseRequest, BaseResponce> ExecutableBaseFunc =>
            req => Execute(req.As<GetUserWeeklyArtistChartRequest>());

        /// <summary>
        ///     Get an artist chart for a user profile, for a given date range.
        ///     If no date range is supplied, it will return the most recent artist chart for this user.
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
        public GetUserWeeklyArtistChartResponce Execute(
            GetUserWeeklyArtistChartRequest request)
        {
            string user = request.User;
            string from = request.From;
            string to = request.To;

            var sessionResponce = Adapter.GetRequest("user.getWeeklyArtistChart",
                Argument.Create("api_key", Settings.ApiKey),
                Argument.Create("user", user),
                Argument.Create("from", from),
                Argument.Create("to", to),
                Argument.Create("format", "json"),
                Argument.Create("callback", "?"));

            Adapter.FixCallback(sessionResponce);

            var getartistInfoResponce = ResponceExt.CreateFrom<BaseResponce, GetUserWeeklyArtistChartResponce>(sessionResponce);
            return getartistInfoResponce;
        }

        /// <summary>
        ///     The request for full information about user of the Last.fm.
        ///     Запрос на полную информациею о пользователе Last.fm.
        /// </summary>
        public class GetUserWeeklyArtistChartRequest : BaseRequest
        {
            public string User { get; set; }

            public string From { get; set; }

            public string To { get; set; }


            public GetUserWeeklyArtistChartRequest()
            {
                From = null;
                To = null;
            }
        }

        /// <summary>
        ///     The responce with information about user data of the Last.fm.
        ///     Ответ с полной информацией о данных пользователе Last.fm.
        /// </summary>
        public class GetUserWeeklyArtistChartResponce : LastFmBaseResponce<UserWeeklyArtistChartContainer>
        {
        }

        /// <summary>
        ///     The container with information about the top tracks listened to by a user on the Last.fm.
        ///     Контейнер с информацией о топ-исполнителях, которые прослушивал пользователь на Last.fm.
        /// </summary>
        public class UserWeeklyArtistChartContainer : LastFmBaseContainer
        {
            public WeeklyArtistChartDto WeeklyArtistChart { get; set; }
        }

        /// <summary>
        ///     The full information about weekly artist chart of an user of the Last.fm.
        ///     Полная информация о недельном чарте альбомов пользователя на Last.fm.
        /// </summary>
        public class WeeklyArtistChartDto
        {
            /*
                 <<weeklyartistchart user="LFUser" from="1212321600" to="1212926400">
                  <artist rank="1">
                    <name>David Hudson and Friends</name>
                    <mbid>80e577ba-841f-43ba-9f32-72e7c1692336</mbid>
                    <playcount>18</playcount>
                    <url>http://www.last.fm/music/David+Hudson+and+Friends</url>
                  </artist>
                  ...
                </weeklyartistchart>
            */

            public WeeklyArtistChartAttribute Attribute { get; set; }

            public ArtistFull[] Artist { get; set; }
        }

        /// <summary>
        ///     The special attribite of <see cref="WeeklyArtistChartDto"/>
        /// </summary>
        public class WeeklyArtistChartAttribute
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