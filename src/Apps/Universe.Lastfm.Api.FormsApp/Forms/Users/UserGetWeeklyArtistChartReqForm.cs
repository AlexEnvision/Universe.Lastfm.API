using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Universe.Lastfm.Api.FormsApp.Settings;

namespace Universe.Lastfm.Api.FormsApp.Forms.Users
{
    public partial class UserGetWeeklyArtistChartReqForm : UserInfoReqForm
    {
        public UserGetWeeklyArtistChartReqForm(UniverseLastApiAppSettings settings) : base(settings)
        {
            InitializeComponent();
        }
    }
}
