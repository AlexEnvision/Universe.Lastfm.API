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
using Universe.Lastfm.Api.Dto.Common;
using Universe.Lastfm.Api.Dto.Common.Short;
using Universe.Lastfm.Api.Dto.GetAlbumInfo;
using Universe.Lastfm.Api.Dto.GetTagInfo;

namespace Universe.Lastfm.Api.Dto.GetTrackInfo
{
    /// <summary>
    ///     The full information about track on the Last.fm.
    ///     Полная информация о трэке на Last.fm.
    /// </summary>
    public class TrackFull : LastFmBaseModel
    {
        /*
            <track>
              <id>1019817</id>
              <name>Believe</name>
              <mbid/>
              <url>http://www.last.fm/music/Cher/_/Believe</url>
              <duration>240000</duration>
              <streamable fulltrack="1">1</streamable>
              <listeners>69572</listeners>
              <playcount>281445</playcount>
              <artist>
                <name>Cher</name>
                <mbid>bfcc6d75-a6a5-4bc6-8282-47aec8531818</mbid>
                <url>http://www.last.fm/music/Cher</url>
              </artist>
              <album position="1">
                <artist>Cher</artist>
                <title>Believe</title>
                <mbid>61bf0388-b8a9-48f4-81d1-7eb02706dfb0</mbid>
                <url>http://www.last.fm/music/Cher/Believe</url>
                <image size="small">http://userserve-ak.last.fm/serve/34/8674593.jpg</image>
                <image size="medium">http://userserve-ak.last.fm/serve/64/8674593.jpg</image>
                <image size="large">http://userserve-ak.last.fm/serve/126/8674593.jpg</image>
              </album>
              <toptags>
                <tag>
                  <name>pop</name>
                  <url>http://www.last.fm/tag/pop</url>
                </tag>
                ...
              </toptags>
              <wiki>
                <published>Sun, 27 Jul 2008 15:44:58 +0000</published>
                <summary>...</summary>
                <content>...</content>
              </wiki>
            </track>
       */

        public string Id { get; set; }

        public string Duration { get; set; }

        public StreamableDescription Streamable { get; set; }

        public string Listeners { get; set; }

        public string Playcount { get; set; }

        public ArtistShort Artist { get; set; }

        public string Mbid { get; set; }

        public AlbumInfoContainer Album { get; set; }

        public TagsContainer Toptags { get; set; }

        public Wiki Wiki { get; set; }
    }
}