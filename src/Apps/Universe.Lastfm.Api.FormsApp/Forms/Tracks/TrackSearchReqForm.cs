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

namespace Universe.Lastfm.Api.FormsApp.Forms.Tracks
{
    public partial class TrackSearchReqForm : TrackInfoReqForm
    {
        public TrackSearchReqForm()
        {
            InitializeComponent();
        }

        public TrackSearchReqForm(UniverseLastApiAppSettings settings) : base(settings)
        {
            InitializeComponent();
        }
    }
}