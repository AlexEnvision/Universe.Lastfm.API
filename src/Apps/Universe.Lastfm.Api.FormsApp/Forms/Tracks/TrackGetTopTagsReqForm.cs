using System.Windows.Forms;
using Universe.Lastfm.Api.FormsApp.Settings;
using Universe.Lastfm.Api.FormsApp.Themes;

namespace Universe.Lastfm.Api.FormsApp.Forms.Tracks
{
    public partial class TrackGetTopTagsReqForm : TrackInfoReqForm
    {
        public TrackGetTopTagsReqForm()
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterScreen;
        }

        public TrackGetTopTagsReqForm(UniverseLastApiAppSettings settings) : base(settings) 
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterScreen;

            if (settings.IsSpaceMode)
                SpaceThemeStyle.Set.Apply(this);

            InitializeParametersByReqCtx(settings.ReqCtx);
        }
    }
}