using System;
using Universe.Lastfm.Api.FormsApp.Forms.Albums;
using Universe.Lastfm.Api.FormsApp.Settings;
using Universe.Lastfm.Api.FormsApp.Themes;

namespace Universe.Lastfm.Api.FormsApp.Forms.Tracks
{
    public partial class TrackDeleteTagsReqForm : TrackInfoReqForm
    {
        protected string TagNames => tbRemovingTags.Text;

        public string[] TagsArray { get; set; }

        public TrackDeleteTagsReqForm()
        {
            InitializeComponent();

            TagsArray = new string[] { };
        }

        public TrackDeleteTagsReqForm(UniverseLastApiAppSettings settings)
        {
            InitializeComponent();

            TagsArray = new string[] { };

            if (settings.IsSpaceMode)
                SpaceThemeStyle.Set.Apply(this);

            InitializeParametersByReqCtx(settings.ReqCtx);
        }

        protected override void btOk_Click(object sender, EventArgs e)
        {
            TagsArray = TagNames.Split(";");
            base.btOk_Click(sender, e);
        }
    }
}
