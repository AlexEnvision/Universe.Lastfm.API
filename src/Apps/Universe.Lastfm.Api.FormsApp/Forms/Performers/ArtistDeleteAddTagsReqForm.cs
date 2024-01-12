using System;
using Universe.Lastfm.Api.FormsApp.Forms.Albums;
using Universe.Lastfm.Api.FormsApp.Settings;
using Universe.Lastfm.Api.FormsApp.Themes;

namespace Universe.Lastfm.Api.FormsApp.Forms.Performers
{
    public partial class ArtistDeleteAddTagsReqForm : AlbumReqInfoForm
    {
        protected string TagNames => tbRemovingTags.Text;

        public string[] TagsArray { get; set; }

        public ArtistDeleteAddTagsReqForm()
        {
            InitializeComponent();

            TagsArray = new string[] { };
        }

        public ArtistDeleteAddTagsReqForm(UniverseLastApiAppSettings settings)
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
