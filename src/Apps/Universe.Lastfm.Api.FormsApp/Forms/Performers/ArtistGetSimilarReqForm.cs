﻿using System;
using Universe.Lastfm.Api.FormsApp.Settings;
using Universe.Lastfm.Api.FormsApp.Themes;

namespace Universe.Lastfm.Api.FormsApp.Forms.Performers
{
    public partial class ArtistGetSimilarReqForm : ArtistReqInfoForm
    {
        public ArtistGetSimilarReqForm()
        {
            InitializeComponent();
        }

        public ArtistGetSimilarReqForm(UniverseLastApiAppSettings settings)
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
