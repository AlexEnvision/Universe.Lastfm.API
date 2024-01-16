using System;
using Universe.Lastfm.Api.FormsApp.Settings;
using Universe.Lastfm.Api.FormsApp.Themes;

namespace Universe.Lastfm.Api.FormsApp.Forms.Albums
{
    public partial class AlbumDeleteTagsReqForm : AlbumReqInfoForm
    {
        protected string TagNames => tbRemovingTags.Text;

        public string[] TagsArray { get; set; }

        public AlbumDeleteTagsReqForm()
        {
            InitializeComponent();

            TagsArray = new string[] { };
        }

        public AlbumDeleteTagsReqForm(UniverseLastApiAppSettings settings)
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
