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
using Universe.Lastfm.Api.FormsApp.Themes;

namespace Universe.Lastfm.Api.FormsApp.Forms.Tracks
{
    public partial class TrackGetCorrectionReqForm : TrackInfoReqForm
    {
        public TrackGetCorrectionReqForm()
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterScreen;
        }

        public TrackGetCorrectionReqForm(UniverseLastApiAppSettings settings) : base(settings)
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterScreen;

            if (settings.IsSpaceMode)
                SpaceThemeStyle.Set.Apply(this);

            InitializeParametersByReqCtx(settings.ReqCtx);
        }
    }
}
