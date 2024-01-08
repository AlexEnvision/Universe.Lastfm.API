using Microsoft.VisualBasic.ApplicationServices;
using System;

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

        protected override void btOk_Click(object sender, EventArgs e)
        {
            User = UserName.Trim();
            Genre = GenreName.Trim();
            GenreType = GenreTypeName.Trim();
            Close();
        }
    }
}