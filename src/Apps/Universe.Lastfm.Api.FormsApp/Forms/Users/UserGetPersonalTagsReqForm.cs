using Microsoft.VisualBasic.ApplicationServices;
using System;
using Universe.Lastfm.Api.FormsApp.Settings;

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
        }

        protected override void btOk_Click(object sender, EventArgs e)
        {
            User = UserName.Trim();
            Genre = GenreName.Trim();
            GenreType = GenreTypeName.Trim();
            Close();
        }
    }
}