//  ╔═════════════════════════════════════════════════════════════════════════════════╗
//  ║                                                                                 ║
//  ║   Copyright 2024 Universe.Lastfm.Api                                            ║
//  ║                                                                                 ║
//  ║   Licensed under the Apache License, Version 2.0 (the "License");               ║
//  ║   you may not use this file except in compliance with the License.              ║
//  ║   You may obtain a copy of the License at                                       ║
//  ║                                                                                 ║
//  ║       http://www.apache.org/licenses/LICENSE-2.0                                ║
//  ║                                                                                 ║
//  ║   Unless required by applicable law or agreed to in writing, software           ║
//  ║   distributed under the License is distributed on an "AS IS" BASIS,             ║
//  ║   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.      ║
//  ║   See the License for the specific language governing permissions and           ║
//  ║   limitations under the License.                                                ║
//  ║                                                                                 ║
//  ║                                                                                 ║
//  ║   Copyright 2024 Universe.Lastfm.Api                                            ║
//  ║                                                                                 ║
//  ║   Лицензировано согласно Лицензии Apache, Версия 2.0 ("Лицензия");              ║
//  ║   вы можете использовать этот файл только в соответствии с Лицензией.           ║
//  ║   Вы можете найти копию Лицензии по адресу                                      ║
//  ║                                                                                 ║
//  ║       http://www.apache.org/licenses/LICENSE-2.0.                               ║
//  ║                                                                                 ║
//  ║   За исключением случаев, когда это регламентировано существующим               ║
//  ║   законодательством или если это не оговорено в письменном соглашении,          ║
//  ║   программное обеспечение распространяемое на условиях данной Лицензии,         ║
//  ║   предоставляется "КАК ЕСТЬ" и любые явные или неявные ГАРАНТИИ ОТВЕРГАЮТСЯ.    ║
//  ║   Информацию об основных правах и ограничениях,                                 ║
//  ║   применяемых к определенному языку согласно Лицензии,                          ║
//  ║   вы можете найти в данной Лицензии.                                            ║
//  ║                                                                                 ║
//  ╚═════════════════════════════════════════════════════════════════════════════════╝

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