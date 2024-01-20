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

using Universe.Lastfm.Api.Dto.GetTagInfo;
using Universe.Lastfm.Api.Models.Res.Base;

namespace Universe.Lastfm.Api.Models.Res
{
    /// <summary>
    ///     The chart with tags
    /// <author>Alex Universe</author>
    /// <author>Alex Envision</author>
    /// </summary>
    public class ChartGetTopTagsResponce : LastFmBaseResponce<ChartTagsContainer>
    {
        /*
            <tags page="1" perPage="50" totalPages="5" total="250">
              <tag>
                <name>rock</name>
                <url>http://www.last.fm/tag/rock</url>
                <reach>309437</reach>
                <taggings>3064604</taggings>
                <streamable>1</streamable>
                <wiki>
                  <published>Sun, 24 Oct 2010 17:40:33 +0000</published>
                  <summary>
            Rock music is a genre of music started in America. It h...
                  </summary>
                  <content>
            Rock music is a genre of music started in America. It has its roots in 1940s and 1950s rock and roll and rockabilly, which evolved from blues, country music and other influences. According to the All Music Guide, “In its pu...
                  </content>
                </wiki>
            </tag>
            ...
            </tags>
       */
    }
}