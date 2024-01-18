using System;
using System.Windows.Forms;
using Universe.Lastfm.Api.FormsApp.Settings;
using Universe.Lastfm.Api.FormsApp.Themes;
using Universe.Lastfm.Api.Models;

namespace Universe.Lastfm.Api.FormsApp.Forms.Tracks
{
    public partial class TrackUpdateNowPlayingReqForm : TrackInfoReqForm
    {
        protected string AlbumName => tbAlbum.Text;

        public string Album { get; set; }

        public TrackUpdateNowPlayingReqForm()
        {
            InitializeComponent();
        }

        public TrackUpdateNowPlayingReqForm(UniverseLastApiAppSettings settings)
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterScreen;

            if (settings.IsSpaceMode)
                SpaceThemeStyle.Set.Apply(this);

            InitializeParametersByReqCtx(settings.ReqCtx);
        }

        protected override void InitializeParametersByReqCtx(ReqContext reqCtx)
        {
            base.InitializeParametersByReqCtx(reqCtx);
            tbAlbum.Text = reqCtx.Album ?? tbAlbum.Text;
        }

        protected override void btOk_Click(object sender, EventArgs e)
        {
            Album = AlbumName.Trim();
            base.btOk_Click(sender, e);
        }
    }
}