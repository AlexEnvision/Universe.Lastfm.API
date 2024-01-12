using Microsoft.VisualBasic.ApplicationServices;
using System;
using Universe.Lastfm.Api.FormsApp.Settings;
using Universe.Lastfm.Api.FormsApp.Themes;

namespace Universe.Lastfm.Api.FormsApp.Forms.Users
{
    public partial class UserGetPersonalTagsReqForm : UserInfoReqForm
    {
        protected string GenreName => tbTag?.Text;

        public string Genre { get; protected set; }

        protected string GenreTypeName => cbTaggingType?.Text;

        public string GenreType { get; protected set; }

        public UserGetPersonalTagsReqForm()
        {
            InitializeComponent();
        }

        public UserGetPersonalTagsReqForm(UniverseLastApiAppSettings settings) : base(settings)
        {
            InitializeComponent();

            if (settings.IsSpaceMode)
                SpaceThemeStyle.Set.Apply(this);
        }

        protected override void btOk_Click(object sender, EventArgs e)
        {
            Genre = GenreName.Trim();
            GenreType = GenreTypeName.Trim();
            base.btOk_Click(sender, e);
        }
    }
}