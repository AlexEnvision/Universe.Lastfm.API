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
using Universe.Lastfm.Api.Helpers;
using Universe.Lastfm.Api.Models;
using Universe.Lastfm.Api.Models.Base;
using Universe.Lastfm.Api.Models.Req.Genre;
using Universe.Lastfm.Api.Models.Res.Base;

namespace Universe.Lastfm.Api.Dal.Queries.Tags
{
    /// <summary>
    ///     The query fetches the top global tags on Last.fm, sorted by popularity (number of times used)
    /// <author>Alex Universe</author>
    /// <author>Alex Envision</author>
    /// </summary>
    public class GetTagWeeklyChartListQuery : LastQuery<GetTagWeeklyChartListRequest, GetTagWeeklyChartListResponce>
    {
        protected override Func<BaseRequest, BaseResponce> ExecutableBaseFunc =>
            req => Execute(req.As<GetTagWeeklyChartListRequest>());

        /// <summary>
        ///     Fetches the top global tags on Last.fm, sorted by popularity (number of times used)
        /// </summary>
        /// <param name="request.tag">
        ///     The tag name.
        ///     (Required) 
        /// </param>
        /// <param name="request">
        ///     Request with parameters for a getting of some tag.
        /// </param>
        /// <returns></returns>
        public override GetTagWeeklyChartListResponce Execute(GetTagWeeklyChartListRequest request)
        {
            string tag = request.Tag ?? throw new ArgumentNullException("request.Tag"); ;

            var sessionResponce = Adapter.GetRequest("tag.getWeeklyChartList",
                Argument.Create("tag", tag),
                Argument.Create("api_key", Settings.ApiKey),
                Argument.Create("format", "json"),
                Argument.Create("callback", "?"));

            Adapter.FixCallback(sessionResponce);

            var infoResponce = ResponceExt.CreateFrom<BaseResponce, GetTagWeeklyChartListResponce>(sessionResponce);
            return infoResponce;
        }
    }

    public class GetTagWeeklyChartListResponce : LastFmBaseResponce<TagWeeklyChartListContainer>
    {
    }

    public class TagWeeklyChartListContainer : LastFmBaseContainer
    {
        /*
            <weeklychartlist tag="rock">
              <chart from="1108296002" to="1108900802"/>
              <chart from="1108900801" to="1109505601"/>
              ...
            </weeklychartlist>
         */

        public WeeklyChartListTags WeeklyChartList { get; set; }
    }

    public class WeeklyChartListTags : LastFmBaseModel
    {
        public WeeklyChart[] Chart { get; set; }
    }

    public class WeeklyChart
    {
        public string From { get; set; }

        public string To { get; set; }
    }
}