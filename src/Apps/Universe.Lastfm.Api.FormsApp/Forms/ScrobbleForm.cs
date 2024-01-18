using System;
using System.Windows.Forms;
using Universe.Lastfm.Api.FormsApp.Settings;
using Universe.Lastfm.Api.FormsApp.Themes;
using Universe.Lastfm.Api.Helpers;
using Universe.Lastfm.Api.Models;

namespace Universe.Lastfm.Api.FormsApp.Forms
{
    public partial class ScrobbleForm : Form
    {
        protected string PerformerName => tbPerformer?.Text;

        protected string AlbumName => tbAlbum?.Text;

        public string Performer { get; protected set; }

        public string Album { get; protected set; }

        protected string TrackName => tbTrack?.Text;

        public string Track { get; protected set; }

        protected DateTime? TimestampTime => dtTimeStampPicker?.Value;

        public long Timestamp { get; protected set; }

        protected string UserName => tbUser?.Text;

        public string User { get; protected set; }


        public ScrobbleForm()
        {
            InitializeComponent();
        }

        public ScrobbleForm(UniverseLastApiAppSettings settings)
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterScreen;

            if (settings.IsSpaceMode)
                SpaceThemeStyle.Set.Apply(this);

            InitializeParametersByReqCtx(settings.ReqCtx);

            dtTimeStampPicker.Value = DateTime.Now;
            dtTimeStampPicker.Text = dtTimeStampPicker.Value.ToShortTimeString();
        }

        private void InitializeParametersByReqCtx(ReqContext reqCtx)
        {
            tbPerformer.Text = reqCtx.Performer ?? tbPerformer.Text;
            tbAlbum.Text = reqCtx.Album ?? tbAlbum.Text;
            tbTrack.Text = reqCtx.Track ?? tbTrack.Text;

            tbUser.Text = reqCtx.User ?? tbUser.Text;
        }

        protected virtual void btOk_Click(object sender, EventArgs e)
        {
            Album = AlbumName.Trim();
            Performer = PerformerName.Trim();
            Track = TrackName.Trim();
            Timestamp = TimestampTime.Value.ToUnixTimeStampInt64();

            User = UserName.Trim();
            Close();
        }
    }
}