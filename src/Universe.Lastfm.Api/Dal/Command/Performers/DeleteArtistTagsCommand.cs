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
    ///     The command deletes tag an artist using a list of user supplied tags.
    /// <author>Alex Universe</author>
    /// <author>Alex Envision</author>
    /// </summary>
    public class DeleteArtistTagsCommand : LastCommand<DeleteArtistTagsRequest, DeleteArtistTagsCommandResponce>
    {
        protected override Func<BaseRequest, BaseResponce> ExecutableBaseFunc =>
            req => Execute(req.As<DeleteArtistTagsRequest>());

        /// <summary>
        ///     Remove a user's tag from an artist.
        /// </summary>
        /// <param name="request.artist">
        ///     The artist name.
        ///     (Required (unless mbid)] 
        /// </param>
        /// <param name="request.Artist">
        ///     The Artist name.
        ///     (Required (unless mbid)] 
        /// </param>
        /// <param name="request.tag">
        ///     A single user tag to remove from this Artist.
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
        public override DeleteArtistTagsCommandResponce Execute(
            DeleteArtistTagsRequest request)
        {
            if (request.RemTag.IsNullOrEmpty())
                throw new ArgumentException("request.Tag is empty. Need to specify one or more tags");
            if (request.Performer.IsNullOrEmpty())
                throw new ArgumentException("request.Performer is empty. This is required parameter");

            if (request.SecretKey.IsNullOrEmpty())
                throw new ArgumentException("request.SecretKey is empty. This is required parameter");
            if (request.Token.IsNullOrEmpty())
                throw new ArgumentException("request.Token is empty. This is required parameter");
            if (request.SessionKey.IsNullOrEmpty())
                throw new ArgumentException("request.SessionKey is empty. This is required parameter");

            string method = "artist.removeTag";

            string artist = request.Performer;

            string tag = request.RemTag;

            string sk = request.SessionKey;

            //  A Last.fm method signature. See authentication for more information.
            //string sig = "api_key" + Settings.ApiKey + "methodArtist.addTags" + request.Token + request.SecretKey;
            string sig = $"method{method}api_key{Settings.ApiKey}sk{sk}artist{artist}tag{tag}{request.SecretKey}";

            var md5Hash = MD5.Create();
            var getMd5Hash = new Md5HashQuery();
            string apiSig = getMd5Hash.Execute(md5Hash, sig);

            var sessionResponce = Adapter.PostRequest(method,
                Argument.Create("api_key", Settings.ApiKey),
                Argument.Create("sk", sk),
                Argument.Create("artist", artist),
                Argument.Create("tag", tag)
            );

            Adapter.FixCallback(sessionResponce);
                DeleteArtistTagsCommandResponce infoResponce =
                    ResponceExt.CreateFrom<BaseResponce, DeleteArtistTagsCommandResponce>(sessionResponce);

                return infoResponce;
        }
    }

    /// <summary>
    ///     The request with parameters for full information about Artist on the Last.fm.
    ///     Запрос с параметрами для добавления тэгов альбома Last.fm.
    /// </summary>
    public class DeleteArtistTagsRequest : BaseRequest
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
        ///     The secret key from setting of the application
        /// </summary>
        public string SecretKey { get; set; }

        /// <summary>
        ///    A single user tag to remove from this Artist.
        /// </summary>
        public string RemTag { get; set; }

        public DeleteArtistTagsRequest()
        {
        }

        public static DeleteArtistTagsRequest Build(string artist,
            string apiSig, string tag, string session)
        {
            return new DeleteArtistTagsRequest
            {
                Token = apiSig,
                Performer = artist,
                RemTag = tag, 
                SessionKey = session
            };
        }
    }

    /// <summary>
    ///     The responces with full information about tag addings of the Last.fm.
    ///     Ответы с полной информацией о добавлении тэгов Last.fm.
    /// </summary>
    public class MassDeleteArtistTagsCommandResponce : BaseResponce
    {
        public MatList<DeleteArtistTagsCommandResponce> Responces { get; set; }

        public MassDeleteArtistTagsCommandResponce()
        {
            Responces = new MatList<DeleteArtistTagsCommandResponce>();
        }
    }

    /// <summary>
    ///     The responce with full information about tag addings of the Last.fm.
    ///     Ответ с полной информацией о добавлении тэгов Last.fm.
    /// </summary>
    public class DeleteArtistTagsCommandResponce : LastFmBaseResponce<DeleteArtistTagsContainer>
    {
    }

    public class DeleteArtistTagsContainer : LastFmBaseContainer
    {
        public LfmResultDto Lfm { get; set; }
    }
}