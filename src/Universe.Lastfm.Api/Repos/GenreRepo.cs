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

using Universe.CQRS.Infrastructure;
using Universe.Lastfm.Api.Dal.Queries.ApiConnect;
using Universe.Lastfm.Api.Dal.Queries.Genre;
using Universe.Lastfm.Api.Dal.Queries.Genres;
using Universe.Lastfm.Api.Repos.Base;

namespace Universe.Lastfm.Api.Repos
{
    public class GenreRepo : BaseRepo
    {
        private GetConnectQuery _connectQuery;

        public GenreRepo(IUniverseScope domainScope) : base(domainScope)
        {
            _connectQuery = domainScope.GetQuery<GetConnectQuery>();
        }

        public void RunConnection(bool needApprovement = false)
        {
            _connectQuery.Connect(needApprovement);
        }

        public string GetTagTop()
        {
            GetTopTagQuery topTag = new GetTopTagQuery();
            return topTag.GetTag(_connectQuery.GetApi());
        }

        public string GetInfo(string genre)
        {
            GetTopAlbumQuery getTopAlbom = new GetTopAlbumQuery();
            GetTopArtistsQuery getTopArtist = new GetTopArtistsQuery();
            string api = _connectQuery.GetApi();
            return $"Жанр {genre}. В данном жанре самый популярный альбом - {getTopAlbom.GetAlbum(api, genre)}, самый популярный исполнитель " +
                $"- {getTopArtist.GetArtist(api, genre)} Интересует похожий жанр?";
        }

        public string GetSimilar(string genre)
        {
            GetSimilarTagQuery getSimilarTag = new GetSimilarTagQuery();
            return getSimilarTag.GetSimilar(_connectQuery.GetApi(), genre);
        }

        public string GetTrack(string genre)
        {
            GetTopTrackQuery getTopTrack = DomainScope.GetQuery<GetTopTrackQuery>();
            return getTopTrack.Execute(genre);
        }

        public int GetNumber(string genre)
        {
            GetNumberTagQuery getNumberTag = new GetNumberTagQuery();
            return getNumberTag.GetNumber(_connectQuery.GetApi(), genre);
        }
    }
}
