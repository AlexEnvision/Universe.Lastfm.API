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

using Newtonsoft.Json;
using Universe.Lastfm.Api.Dto.GetArtistInfo;
using Universe.Lastfm.Api.Models.Res.Base;

namespace Universe.Lastfm.Api.Models.Res
{
    /// <summary>
    ///     The responce with the full information about artist/performer on the Last.fm.
    ///     Ответ с полнуй информацией об артисте/исполнителе на Last.fm. 
    /// <author>Alex Universe</author>
    /// <author>Alex Envision</author>
    /// </summary>
    public class GetArtistInfoResponce : LastFmBaseResponce<ArtistInfoContainer>
    {
        public override ArtistInfoContainer DataContainer
        {
            get
            {
                if (_dataContainer == null)
                {
                    if (string.IsNullOrEmpty(ServiceAnswer))
                        return _dataContainer;

                    var answer = ServiceAnswer; //.Replace("@attr", "TrackAttribute");
                    var deserialized = JsonConvert.DeserializeObject<ArtistInfoContainer>(answer);
                    _dataContainer = deserialized;
                    return _dataContainer;
                }

                return _dataContainer;
            }
        }

        private ArtistInfoContainer _dataContainer;
    }
}
