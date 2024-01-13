using System;
using Universe.Lastfm.Api.FormsApp.Settings;
using Universe.Lastfm.Api.FormsApp.Themes;
using Universe.Lastfm.Api.Models;

namespace Universe.Lastfm.Api.FormsApp.Forms.Tracks
{
    public partial class TrackTagsReqForm : TrackInfoReqForm
    {
        protected string UserName => tbUser?.Text;

        public string User { get; set; }

        public TrackTagsReqForm()
        {
            InitializeComponent();
        }

        public TrackTagsReqForm(UniverseLastApiAppSettings settings) : base(settings)
        {
            InitializeComponent();

            if (settings.IsSpaceMode)
                SpaceThemeStyle.Set.Apply(this);

            InitializeParametersByReqCtx(settings.ReqCtx);
        }

        protected override void InitializeParametersByReqCtx(ReqContext reqCtx)
        {
            if (tbUser != null) 
                tbUser.Text = reqCtx.User ?? tbUser.Text;
            base.InitializeParametersByReqCtx(reqCtx);
        }

        protected override void btOk_Click(object sender, EventArgs e)
        {
            User = UserName.Trim();
            base.btOk_Click(sender, e);
        }
    }
}