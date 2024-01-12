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

namespace Universe.Lastfm.Api.FormsApp.Forms.Performers
{
    public partial class ArtistGetCorrectionReqForm : ArtistReqInfoForm
    {
        public ArtistGetCorrectionReqForm()
        {
            InitializeComponent();
        }

        public ArtistGetCorrectionReqForm(UniverseLastApiAppSettings settings)
        {
            InitializeComponent();

            if (settings.IsSpaceMode)
                SpaceThemeStyle.Set.Apply(this);
        }

        protected override void btOk_Click(object sender, EventArgs e)
        {
            base.btOk_Click(sender, e);
        }
    }
}