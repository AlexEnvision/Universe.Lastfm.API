using Universe.Lastfm.Api.FormsApp.Settings;

namespace Universe.Lastfm.Api.FormsApp.Forms.Users
{
    public partial class UserGetLovedTracksReqForm : UserInfoReqForm
    {
        public UserGetLovedTracksReqForm(UniverseLastApiAppSettings settings) : base(settings)
        {
            InitializeComponent();
        }
    }
}