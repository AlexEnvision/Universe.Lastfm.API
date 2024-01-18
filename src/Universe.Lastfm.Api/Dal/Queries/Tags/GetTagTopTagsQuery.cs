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
using Newtonsoft.Json;
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
    /// </summary>
    public class GetTagTopTagsQuery : LastQuery<GetTagTopTagsRequest, GetTagTopTagsResponce>
    {
        protected override Func<BaseRequest, BaseResponce> ExecutableBaseFunc =>
            req => Execute(req.As<GetTagTopTagsRequest>());

        /// <summary>
        ///     Fetches the top global tags on Last.fm, sorted by popularity (number of times used)
        /// </summary>
        /// <param name="request.tag">
        ///     The tag name.
        ///     (Required) 
        /// </param>
        /// <param name="request.page">
        ///     The page number to fetch. Defaults to first page.
        /// </param>
        /// <param name="request.limit">
        ///     The number of results to fetch per page. Defaults to 50.
        /// </param>
        /// <param name="request">
        ///     Request with parameters for a getting of some tag.
        /// </param>
        /// <returns></returns>
        public override GetTagTopTagsResponce Execute(GetTagTopTagsRequest request)
        {
            string tag = request.Tag ?? throw new ArgumentNullException("request.Tag");
            int page = request.Page;
            int limit = request.Limit;

            var sessionResponce = Adapter.GetRequest("tag.getTopTags",
                Argument.Create("tag", tag),
                Argument.Create("page", page.ToString()),
                Argument.Create("limit", limit.ToString()),
                Argument.Create("api_key", Settings.ApiKey),
                Argument.Create("format", "json"),
                Argument.Create("callback", "?"));

            Adapter.FixCallback(sessionResponce);

            var infoResponce = ResponceExt.CreateFrom<BaseResponce, GetTagTopTagsResponce>(sessionResponce);
            return infoResponce;
        }
    }

    /// <summary>
    ///     The responce with top tags
    /// </summary>
    public class GetTagTopTagsResponce : LastFmBaseResponce<TagTopTagsContainer>
    {
        public override TagTopTagsContainer DataContainer
        {
            get
            {
                if (_dataContainer == null)
                {
                    if (string.IsNullOrEmpty(ServiceAnswer))
                        return _dataContainer;

                    var answer = ServiceAnswer
                        .Replace("@attr", "Attribute")
                        .Replace("#text", "text")
                        .Replace("num_res", "NumRes");
                    var wrapped = $"{{\"{answer}}}}}";  // Впервые вижу такую ошибку, чтобы без скобок и вначале строки без кавычек был ответ.
                    ServiceAnswer = wrapped;
                    answer = ServiceAnswer;
                    var deserialized = JsonConvert.DeserializeObject<TagTopTagsContainer>(answer);
                    _dataContainer = deserialized;
                    return _dataContainer;
                }

                return _dataContainer;
            }
        }

        private TagTopTagsContainer _dataContainer;
    }

    public class TagTopTagsContainer : LastFmBaseContainer
    {
        /*
            <toptags>
              <tag>
                <name>rock</name>
                <count>1994155</count>
                <url>www.last.fm/tag/rock</url>
              </tag>
              ...
            </toptags>
         */

        public TopTagAttribute Attribute { get; set; }

        public TopTags TopTags { get; set; }
    }

    public class TopTagAttribute
    {
        public int Offset { get; set; }

        public int NumRes { get; set; }

        public int Total { get; set; }
    }

    public class TopTags : LastFmBaseModel
    {
        public TopTag[] Tag { get; set; }
    }

    public class TopTag : LastFmBaseModel
    {
        public int Count { get; set; }

        public int Reach { get; set; }
    }
}