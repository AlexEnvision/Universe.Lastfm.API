using System;
using System.IO;
using System.Windows.Forms;
using Universe.Helpers.Extensions;
using Universe.Lastfm.Api.FormsApp.Settings;
using Universe.Lastfm.Api.FormsApp.Themes;
using Universe.Lastfm.Api.Models;

namespace Universe.Lastfm.Api.FormsApp.Forms
{
    public partial class ReqContextForm : Form
    {
        protected string PerformerName => tbPerformer?.Text;

        protected string AlbumName => tbAlbum?.Text;

        public string Performer { get; protected set; }

        public string Album { get; protected set; }

        protected string TrackName => tbTrack?.Text;

        public string Track { get; protected set; }

        protected string TagName => tbTag?.Text;

        public string Tag { get; protected set; }

        protected string UserName => tbUser?.Text;

        public string User { get; protected set; }

        protected string AddTagNames => tbCreatingTags.Text;

        public string[] AddTagsArray { get; set; }

        protected string DelTagNames => tbRemovingTags.Text;

        public string[] DelTagArray { get; set; }

        protected string OutputFilePath => tbOutputPath.Text;

        public string OutputFile => OutputFilePath;

        public ReqContextForm()
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterScreen;

            AddTagsArray = new string[] { };
            DelTagArray = new string[] { };

            InitializePaths();
        }

        private void InitializePaths()
        {
            if (OutputFile.IsNullOrEmpty())
            {
                var fullPath = GetDefaultFullPath;
                tbOutputPath.Text = fullPath;
            }
        }

        public ReqContextForm(UniverseLastApiAppSettings settings)
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterScreen;

            AddTagsArray = new string[] { };
            DelTagArray = new string[] { };

            if (settings.IsSpaceMode)
                SpaceThemeStyle.Set.Apply(this);

            InitializePaths();
            InitializeParametersByReqCtx(settings.ReqCtx);
        }

        private void InitializeParametersByReqCtx(ReqContext reqCtx)
        {
            tbPerformer.Text = reqCtx.Performer ?? tbPerformer.Text;
            tbAlbum.Text = reqCtx.Album ?? tbAlbum.Text;
            tbTrack.Text = reqCtx.Track ?? tbTrack.Text;
            tbTag.Text = reqCtx.Tag ?? tbTag.Text;

            tbUser.Text = reqCtx.User ?? tbUser.Text;
            tbCreatingTags.Text = reqCtx.Tags != null ? string.Join(";", reqCtx.Tags) : tbCreatingTags.Text;
            tbRemovingTags.Text = reqCtx.RemTag ?? tbRemovingTags.Text;
        }

        protected virtual void btOk_Click(object sender, EventArgs e)
        {
            Album = AlbumName.Trim();
            Performer = PerformerName.Trim();
            Track = TrackName.Trim();
            Tag = TagName.Trim();

            User = UserName.Trim();
            AddTagsArray = AddTagNames.Split(";");
            DelTagArray = DelTagNames.Split(";");
            Close();
        }

        private void btChoosePath_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog())
            {
                dialog.FileName = GetOutputFileName;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    tbOutputPath.Text = dialog.FileName;
                }
            }
        }

        private string GetOutputFileName => $"FullCollectionOfInformation_{DateTime.Now:ddMMyyyy}-{DateTime.Now:hhmmss}.json";

        private string GetDefaultFullPath => Path.Combine(Directory.GetCurrentDirectory(), GetOutputFileName);
    }
}