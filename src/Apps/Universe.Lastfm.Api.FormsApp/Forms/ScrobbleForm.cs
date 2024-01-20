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
using System.Windows.Forms;
using Universe.Lastfm.Api.FormsApp.Settings;
using Universe.Lastfm.Api.FormsApp.Themes;
using Universe.Lastfm.Api.Helpers;
using Universe.Lastfm.Api.Models;

namespace Universe.Lastfm.Api.FormsApp.Forms
{
    /// <summary>
    /// <author>Alex Universe</author>
    /// <author>Alex Envision</author>
    /// </summary>
    public partial class ScrobbleForm : Form
    {
        protected string PerformerName => tbPerformer?.Text;

        protected string AlbumName => tbAlbum?.Text;

        public string Performer { get; protected set; }

        public string Album { get; protected set; }

        protected string TrackName => tbTrack?.Text;

        public string Track { get; protected set; }

        protected DateTime? TimestampTime => dtTimeStampPicker?.Value;

        public long Timestamp { get; protected set; }

        protected string UserName => tbUser?.Text;

        public string User { get; protected set; }


        public ScrobbleForm()
        {
            InitializeComponent();
        }

        public ScrobbleForm(UniverseLastApiAppSettings settings)
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterScreen;

            if (settings.IsSpaceMode)
                SpaceThemeStyle.Set.Apply(this);

            InitializeParametersByReqCtx(settings.ReqCtx);

            var tShift = DateTimeOffset.Now.Offset.Hours;
            var timestamp = DateTime.Now.AddHours(-1 * tShift);
            if (DateTime.Now.Hour - tShift < 0)
                timestamp = timestamp.AddDays(-1);

            dtTimeStampPicker.Value = timestamp;
            dtTimeStampPicker.Text = dtTimeStampPicker.Value.ToShortTimeString();
        }

        private void InitializeParametersByReqCtx(ReqContext reqCtx)
        {
            tbPerformer.Text = reqCtx.Performer ?? tbPerformer.Text;
            tbAlbum.Text = reqCtx.Album ?? tbAlbum.Text;
            tbTrack.Text = reqCtx.Track ?? tbTrack.Text;

            tbUser.Text = reqCtx.User ?? tbUser.Text;
        }

        protected virtual void btOk_Click(object sender, EventArgs e)
        {
            Album = AlbumName.Trim();
            Performer = PerformerName.Trim();
            Track = TrackName.Trim();
            Timestamp = TimestampTime.Value.ToUnixTimeStampInt64();

            User = UserName.Trim();
            Close();
        }
    }
}