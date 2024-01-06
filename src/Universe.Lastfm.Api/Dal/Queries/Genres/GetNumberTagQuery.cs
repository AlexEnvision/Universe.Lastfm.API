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

using System.IO;
using System.Net;
using System.Text;

namespace Universe.Lastfm.Api.Dal.Queries.Genre
{
    public class GetNumberTagQuery
    {
        public int GetNumber(string api, string genre)
        {
            WebRequest request = WebRequest.Create("http://ws.audioscrobbler.com/2.0/?method=tag.getTopTags&api_key=" + api);
            WebResponse response = request.GetResponse();

            string result = new StreamReader(response.GetResponseStream(), Encoding.UTF8).ReadToEnd();
            string answer = "";
            bool l = true;// переменная, сообщающая, что позиция нужного тэга не найдена
            int i = 0;
            int count = 0;
            int len = genre.Length;
            while (l && count != 50)
            {
                count++;
                i = result.IndexOf("<name>") + 6;

                for (int j = i; j < i + len; j++)
                {
                    answer += result[j];
                }
                if (answer == genre)
                {
                    l = false;
                }
                else
                {
                    result = result.Remove(0, i);
                    i = 0;
                    answer = "";
                }
            }
            if (count >= 50)
            {
                return 0;
            }
            else return count;
        }
    }
}
