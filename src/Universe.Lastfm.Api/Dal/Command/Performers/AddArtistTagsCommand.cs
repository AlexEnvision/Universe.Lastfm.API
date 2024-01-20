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
using System.Linq;
using System.Security.Cryptography;
using Universe.Helpers.Extensions;
using Universe.Lastfm.Api.Dal.Queries.Hash;
using Universe.Lastfm.Api.Dto.Base;
using Universe.Lastfm.Api.Dto.Common;
using Universe.Lastfm.Api.Helpers;
using Universe.Lastfm.Api.Models;
using Universe.Lastfm.Api.Models.Base;
using Universe.Lastfm.Api.Models.Res.Base;
using Universe.Types.Collection;

namespace Universe.Lastfm.Api.Dal.Command.Performers
{
    /// <summary>
    ///     The command adds tag an artist using a list of user supplied tags.
    /// <author>Alex Universe</author>
    /// <author>Alex Envision</author>
    /// </summary>
    public class AddArtistTagsCommand : LastCommand<AddArtistTagsRequest, MassAddArtistTagsCommandResponce>
    {
        protected override Func<BaseRequest, BaseResponce> ExecutableBaseFunc =>
            req => Execute(req.As<AddArtistTagsRequest>());

        /// <summary>
        ///     Tag an artist with one or more user supplied tags.
        /// </summary>
        /// <param name="request.artist">
        ///     The artist name.
        ///     (Required (unless mbid)] 
        /// </param>
        /// <param name="request.tags">
        ///     A comma delimited list of user supplied tags to apply to this Artist.
        ///     Accepts a maximum of 10 tags.
        /// </param>
        /// <param name="request.token">
        ///     The auth token.
        ///     (Required) 
        /// </param>
        /// <param name="request.secretKey">
        ///     The secret key from setting of the application
        ///     (Required) 
        /// </param>
        /// <param name="request"></param>
        /// <returns></returns>
        public override MassAddArtistTagsCommandResponce Execute(
            AddArtistTagsRequest request)
        {
            if (request.Tags.Length == 0)
                throw new ArgumentException("request.Tags is empty. Need to specify one or more tags");
            if (request.Performer.IsNullOrEmpty())
                throw new ArgumentException("request.Performer is empty. This is required parameter");

            if (request.SecretKey.IsNullOrEmpty())
                throw new ArgumentException("request.SecretKey is empty. This is required parameter");
            if (request.Token.IsNullOrEmpty())
                throw new ArgumentException("request.Token is empty. This is required parameter");
            if (request.SessionKey.IsNullOrEmpty())
                throw new ArgumentException("request.SessionKey is empty. This is required parameter");

            string method = "artist.addTags";

            string artist = request.Performer;

            var responce = new MassAddArtistTagsCommandResponce();

            var batchSize = 10;
            for (int i = 0; i < request.Tags.Length; i += batchSize)
            {
                var batch = request.Tags.Skip(0).Take(batchSize).ToArray();
                string tags = string.Join(",", request.Tags);

                string sk = request.SessionKey;

                //  A Last.fm method signature. See authentication for more information.
                //string sig = "api_key" + Settings.ApiKey + "methodArtist.addTags" + request.Token + request.SecretKey;
                string sig = $"api_key{Settings.ApiKey}artist{artist}method{method}sk{sk}tags{tags}{request.SecretKey}";

                var md5Hash = MD5.Create();
                var getMd5Hash = new Md5HashQuery();
                string apiSig = getMd5Hash.Execute(md5Hash, sig);

                var sessionResponce = Adapter.PostRequest(method,
                    Argument.Create("api_key", Settings.ApiKey),
                    Argument.Create("sk", sk),
                    Argument.Create("artist", artist),
                    Argument.Create("tags", tags)
                );

                Adapter.FixCallback(sessionResponce);
                AddArtistTagsCommandResponce infoResponce = ResponceExt.CreateFrom<BaseResponce, AddArtistTagsCommandResponce>(sessionResponce);

                responce.Responces += infoResponce;
            }

            responce.IsSuccessful = true;
            responce.Message = "OK";
            return responce;
        }
    }

    /// <summary>
    ///     The request with parameters for full information about Artist on the Last.fm.
    ///     Запрос с параметрами для добавления тэгов альбома Last.fm.
    /// </summary>
    public class AddArtistTagsRequest : BaseRequest
    {
        public string Performer { get; set; }

        /// <summary>
        ///     A Last.fm method signature. See authentication for more information.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        ///    A session key generated by authenticating a user via the authentication protocol.
        /// </summary>
        public string SessionKey { get; set; }

        /// <summary>
        ///    A comma delimited list of user supplied tags to apply to this Artist. Accepts a maximum of 10 tags.
        /// </summary>
        public string[] Tags { get; set; }

        /// <summary>
        ///     The secret key from setting of the application
        /// </summary>
        public string SecretKey { get; set; }

        public AddArtistTagsRequest()
        {
        }

        public static AddArtistTagsRequest Build(string artist, string Artist, 
            string apiSig, string[] tags, string session)
        {
            return new AddArtistTagsRequest
            {
                Token = apiSig,
                Performer = artist,
                Tags = tags, 
                SessionKey = session
            };
        }
    }

    /// <summary>
    ///     The responces with full information about tag addings of the Last.fm.
    ///     Ответы с полной информацией о добавлении тэгов Last.fm.
    /// </summary>
    public class MassAddArtistTagsCommandResponce : BaseResponce
    {
        public MatList<AddArtistTagsCommandResponce> Responces { get; set; }

        public MassAddArtistTagsCommandResponce()
        {
            Responces = new MatList<AddArtistTagsCommandResponce>();
        }
    }

    /// <summary>
    ///     The responce with full information about tag addings of the Last.fm.
    ///     Ответ с полной информацией о добавлении тэгов Last.fm.
    /// </summary>
    public class AddArtistTagsCommandResponce : LastFmBaseResponce<AddArtistTagsContainer>
    {
    }

    public class AddArtistTagsContainer : LastFmBaseContainer
    {
        public LfmResultDto Lfm { get; set; }
    }
}