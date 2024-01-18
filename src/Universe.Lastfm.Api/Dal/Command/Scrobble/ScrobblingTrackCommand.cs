using System;
using Universe.Helpers.Extensions;
using Universe.Lastfm.Api.Dto.Base;
using Universe.Lastfm.Api.Dto.Common;
using Universe.Lastfm.Api.Helpers;
using Universe.Lastfm.Api.Models;
using Universe.Lastfm.Api.Models.Base;
using Universe.Lastfm.Api.Models.Res.Base;

namespace Universe.Lastfm.Api.Dal.Command.Scrobble
{
    /// <summary>
    ///     The command used to add a track-play to a user's profile.
    ///     Scrobble a track, or a batch of tracks.
    /// </summary>
    public class ScrobblingTrackCommand : LastCommand<ScrobblingTrackRequest, ScrobblingTrackCommandResponce>
    {
        protected override Func<BaseRequest, BaseResponce> ExecutableBaseFunc =>
            req => Execute(req.As<ScrobblingTrackRequest>());

        /// <summary>
        ///     Used to add a track-play to a user's profile. Scrobble a track, or a batch of tracks.
        ///     Tracks are passed to the service using array notation for each of the below params,
        ///     up to a maximum of 50 scrobbles per batch [0<=i<=49].
        ///     If you are only sending a single scrobble the array notation may be ommited.
        ///     Note: Extra care should be taken while calculating the signature when using array notation
        ///     as the parameter names MUST be sorted according to the ASCII table
        ///     (i.e., artist[10] comes before artist[1]). It is important to not use the corrections returned
        ///     by the now playing service as input for the scrobble request, unless they have been explicitly
        ///     approved by the user. Parameter names are case sensitive.
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
        public override ScrobblingTrackCommandResponce Execute(
            ScrobblingTrackRequest request)
        {
            if (request.Performer.IsNullOrEmpty())
                throw new ArgumentException("request.Performer is empty. This is required parameter");
            if (request.Track.IsNullOrEmpty())
                throw new ArgumentException("request.Track is empty. This is required parameter");

            if (request.SecretKey.IsNullOrEmpty())
                throw new ArgumentException("request.SecretKey is empty. This is required parameter");
            if (request.SessionKey.IsNullOrEmpty())
                throw new ArgumentException("request.SessionKey is empty. This is required parameter");

            string method = "track.scrobble";

            // Required parameters
            string artist = request.Performer;
            string track = request.Track;
            long timestamp = request.Timestamp;

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
                    Argument.Create("timestamp", timestamp.ToString()),

                    Argument.Create("album", album),
                    Argument.Create("albumArtist", albumArtist),
                    Argument.Create("context", context),
                    Argument.Create("mbid", mbid),
                    Argument.Create("duration", duration)
            );

            Adapter.FixCallback(sessionResponce);
            ScrobblingTrackCommandResponce infoResponce = ResponceExt.CreateFrom<BaseResponce, ScrobblingTrackCommandResponce>(sessionResponce);

            return infoResponce;
        }
    }

    /// <summary>
    ///     The request with parameters for full information about Artist on the Last.fm.
    ///     Запрос с параметрами для добавления тэгов альбома Last.fm.
    /// </summary>
    public class ScrobblingTrackRequest : BaseRequest
    {
        public string Performer { get; set; }

        /// <summary>
        ///      A track name (utf8 encoded)
        /// </summary>
        public string Track { get; set; }

        public long Timestamp { get; set; }

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

        public ScrobblingTrackRequest()
        {
        }

        public static ScrobblingTrackRequest Build(string artist, 
            string apiSig, string[] tags, string session)
        {
            return new ScrobblingTrackRequest
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
    public class ScrobblingTrackCommandResponce : LastFmBaseResponce<ScrobblingTrackContainer>
    {
    }

    public class ScrobblingTrackContainer : LastFmBaseContainer
    {
        public LfmResultDto Lfm { get; set; }
    }
}