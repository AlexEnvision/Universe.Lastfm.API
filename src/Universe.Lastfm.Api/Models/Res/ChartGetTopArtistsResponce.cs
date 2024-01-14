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

using Universe.Lastfm.Api.Dto.GetArtistInfo;
using Universe.Lastfm.Api.Models.Res.Base;

namespace Universe.Lastfm.Api.Models.Res
{
    /// <summary>
    ///     The chart with artists
    /// </summary>
    public class ChartGetTopArtistsResponce : LastFmBaseResponce<ArtistInfoContainer>
    {
         /*
             <artists page="1" perPage="50" totalPages="20" total="1000">
              <artist>
                <name>The Beatles</name>
                <playcount>1550293</playcount>
                <listeners>114106</listeners>
                <mbid>b10bbbfc-cf9e-42e0-be17-e2c3e1d2600d</mbid>
                <url>http://www.last.fm/music/The+Beatles</url>
                <streamable>1</streamable>
                <image size="small">http://userserve-ak.last.fm/serve/34/880929.jpg</image>
                <image size="medium">http://userserve-ak.last.fm/serve/64/880929.jpg</image>
                <image size="large">http://userserve-ak.last.fm/serve/126/880929.jpg</image>
                <image size="extralarge">http://userserve-ak.last.fm/serve/252/880929.jpg</image>
                <image size="mega">http://userserve-ak.last.fm/serve/500/880929/The+Beatles.jpg</image>
              </artist>
              ...
            </artists>
        */
    }
}