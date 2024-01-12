using Microsoft.VisualBasic.ApplicationServices;
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
    public partial class TrackTagsReqForm : TrackInfoReqForm
    {
        protected string UserName => tbUser?.Text;

        public string User { get; set; }

        public TrackTagsReqForm()
        {
            InitializeComponent();
        }

        public TrackTagsReqForm(UniverseLastApiAppSettings settings) : base(settings)
        {
            InitializeComponent();

            if (settings.IsSpaceMode)
                SpaceThemeStyle.Set.Apply(this);
        }

        protected override void btOk_Click(object sender, EventArgs e)
        {
            User = UserName.Trim();
            base.btOk_Click(sender, e);
        }
    }
}