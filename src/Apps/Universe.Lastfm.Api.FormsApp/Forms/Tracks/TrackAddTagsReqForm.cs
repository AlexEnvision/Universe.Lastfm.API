using System;
using Universe.Lastfm.Api.FormsApp.Settings;
using Universe.Lastfm.Api.FormsApp.Themes;

namespace Universe.Lastfm.Api.FormsApp.Forms.Tracks
{
    public partial class TrackAddTagsReqForm : TrackInfoReqForm
    {
        protected string TagNames => tbCreatingTags.Text;

        public string[] TagsArray { get; set; }

        public TrackAddTagsReqForm()
        {
            InitializeComponent();

            TagsArray = new string[] { };
        }

        public TrackAddTagsReqForm(UniverseLastApiAppSettings settings) : base(settings)
        {
            InitializeComponent();

            TagsArray = new string[] { };

            if (settings.IsSpaceMode)
                SpaceThemeStyle.Set.Apply(this);
        }

        protected override void btOk_Click(object sender, EventArgs e)
        {
            TagsArray = TagNames.Split(";");
            base.btOk_Click(sender, e);
        }
    }
}