using Universe.Lastfm.Api.FormsApp.Settings;

namespace Universe.Lastfm.Api.FormsApp.Forms.Users
{
    public partial class UserGetRecentTracksReqForm : UserInfoReqForm
    {
        public UserGetRecentTracksReqForm(UniverseLastApiAppSettings settings) : base(settings)
        {
            InitializeComponent();
        }
    }
}