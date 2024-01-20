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
using Universe.Helpers.Extensions;
using Universe.Lastfm.Api.Dto.Base;
using Universe.Lastfm.Api.Dto.Common;
using Universe.Lastfm.Api.Helpers;
using Universe.Lastfm.Api.Models;
using Universe.Lastfm.Api.Models.Base;
using Universe.Lastfm.Api.Models.Res.Base;

namespace Universe.Lastfm.Api.Dal.Command.Tracks
{
    /// <summary>
    ///     The command uses to notify Last.fm that a user
    ///     has started listening to a track.
    ///     Parameter names are case sensitive.
    /// <author>Alex Universe</author>
    /// <author>Alex Envision</author>
    /// </summary>
    public class UpdateTrackUpdateNowPlayingCommand : LastCommand<UpdateTrackUpdateNowPlayingRequest, UpdateTrackUpdateNowPlayingCommandResponce>
    {
        protected override Func<BaseRequest, BaseResponce> ExecutableBaseFunc =>
            req => Execute(req.As<UpdateTrackUpdateNowPlayingRequest>());

        /// <summary>
        ///     Used to notify Last.fm that a user has started
        ///     listening to a track. Parameter names are case sensitive.
        /// </summary>
        /// <param name="request.artist">
        ///     The artist name.
        ///     (Required (unless mbid)] 
        /// </param>
        /// <param name="request.track">
        ///     The track name.
        /// </param>
        /// <param name="request.album">
        ///     The album name. (Optional)
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
        public override UpdateTrackUpdateNowPlayingCommandResponce Execute(
            UpdateTrackUpdateNowPlayingRequest request)
        {
            if (request.Performer.IsNullOrEmpty())
                throw new ArgumentException("request.Performer is empty. This is required parameter");
            if (request.Track.IsNullOrEmpty())
                throw new ArgumentException("request.Track is empty. This is required parameter");

            if (request.SecretKey.IsNullOrEmpty())
                throw new ArgumentException("request.SecretKey is empty. This is required parameter");
            if (request.SessionKey.IsNullOrEmpty())
                throw new ArgumentException("request.SessionKey is empty. This is required parameter");

            string method = "track.updateNowPlaying";

            // Required parameters

            string artist = request.Performer;
            string track = request.Track;
            string sk = request.SessionKey;

            // Optional parameters
            var album = request.Album;
            var albumArtist = request.AlbumArtist;
            var context = request.Context;
            var mbid = request.Mbid;
            var duration = request.Duration;

            //  A Last.fm method signature. See authentication for more information.
            //string sig = $"method{method}api_key{Settings.ApiKey}sk{sk}artist{artist}track{track}{request.SecretKey}";

            //var md5Hash = MD5.Create();
            //var getMd5Hash = new Md5HashQuery();
            //string apiSig = getMd5Hash.Execute(md5Hash, sig);

            //var parameters = new
            //{
            //    method = method,
            //    api_key = Settings.ApiKey,
            //    api_sig = apiSig,
            //    sk = sk,
            //    artist = artist,
            //    track = track,
            //};

            //var parametersSfy = JsonConvert.SerializeObject(parameters);

            var sessionResponce = Adapter.PostRequestNonEmptyArguments(method,
                    Argument.Create("api_key", Settings.ApiKey),
                    Argument.Create("sk", sk),
                    Argument.Create("artist", artist),
                    Argument.Create("track", track),

                    Argument.Create("album", album),
                    Argument.Create("albumArtist", albumArtist),
                    Argument.Create("context", context),
                    Argument.Create("mbid", mbid),
                    Argument.Create("duration", duration)
            );

            Adapter.FixCallback(sessionResponce);
            UpdateTrackUpdateNowPlayingCommandResponce infoResponce = ResponceExt.CreateFrom<BaseResponce, UpdateTrackUpdateNowPlayingCommandResponce>(sessionResponce);

            return infoResponce;
        }
    }

    /// <summary>
    ///     The request with parameters for full information about Artist on the Last.fm.
    ///     Запрос с параметрами для добавления тэгов альбома Last.fm.
    /// </summary>
    public class UpdateTrackUpdateNowPlayingRequest : BaseRequest
    {
        public string Performer { get; set; }

        /// <summary>
        ///      A track name (utf8 encoded)
        /// </summary>
        public string Track { get; set; }

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

        //album(Optional) : The album name.
        //trackNumber(Optional) : The track number of the track on the album.
        //context(Optional) : Sub-client version(not public, only enabled for certain API keys)
        //mbid(Optional) : The MusicBrainz Track ID.
        //duration(Optional) : The length of the track in seconds.
        //albumArtist(Optional) : The album artist - if this differs from the track artist.

        /// <summary>
        ///      The album name. (Optional)
        /// </summary>
        public string Album { get; set; }

        /// <summary>
        ///      The track number of the track on the album. (Optional)
        /// </summary>
        public string TrackNumber { get; set; }

        /// <summary>
        ///      Sub-client version(not public, only enabled for certain API keys) (Optional)
        /// </summary>
        public string Context { get; set; }

        /// <summary>
        ///      The MusicBrainz Track ID. (Optional)
        /// </summary>
        public string Mbid { get; set; }

        /// <summary>
        ///      The length of the track in seconds. (Optional)
        /// </summary>
        public string Duration { get; set; }

        /// <summary>
        ///      The album artist - if this differs from the track artist. (Optional)
        /// </summary>
        public string AlbumArtist { get; set; }

        public UpdateTrackUpdateNowPlayingRequest()
        {
        }

        public static UpdateTrackUpdateNowPlayingRequest Build(string artist, 
            string apiSig, string[] tags, string session)
        {
            return new UpdateTrackUpdateNowPlayingRequest
            {
                Token = apiSig,
                Performer = artist,
                SessionKey = session
            };
        }
    }

    /// <summary>
    ///     The responce with full information about tag addings of the Last.fm.
    ///     Ответ с полной информацией о добавлении тэгов Last.fm.
    /// </summary>
    public class UpdateTrackUpdateNowPlayingCommandResponce : LastFmBaseResponce<UpdateTrackUpdateNowPlayingContainer>
    {
    }

    public class UpdateTrackUpdateNowPlayingContainer : LastFmBaseContainer
    {
        public LfmResultDto Lfm { get; set; }
    }
}