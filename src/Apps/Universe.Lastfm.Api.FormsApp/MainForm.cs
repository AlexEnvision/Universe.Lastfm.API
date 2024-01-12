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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;
using Unity;
using Universe.Algorithm.MultiThreading;
using Universe.CQRS;
using Universe.CQRS.Infrastructure;
using Universe.Diagnostic.Logger;
using Universe.Lastfm.Api.Dal.Command;
using Universe.Lastfm.Api.Dal.Command.Albums;
using Universe.Lastfm.Api.Dal.Queries;
using Universe.Lastfm.Api.Dal.Queries.Albums;
using Universe.Lastfm.Api.Dal.Queries.ApiConnect;
using Universe.Lastfm.Api.Dal.Queries.Auth;
using Universe.Lastfm.Api.Dal.Queries.Performers;
using Universe.Lastfm.Api.Dal.Queries.Tags;
using Universe.Lastfm.Api.Dal.Queries.Track;
using Universe.Lastfm.Api.Dal.Queries.Users;
using Universe.Lastfm.Api.Dto.GetArtists;
using Universe.Lastfm.Api.FormsApp.Extensions;
using Universe.Lastfm.Api.FormsApp.Extensions.Model;
using Universe.Lastfm.Api.FormsApp.Forms.Genres;
using Universe.Lastfm.Api.FormsApp.Forms.Performers;
using Universe.Lastfm.Api.FormsApp.Forms.Tracks;
using Universe.Lastfm.Api.FormsApp.Infrastracture;
using Universe.Lastfm.Api.FormsApp.Settings;
using Universe.Lastfm.Api.Helpers;
using Universe.Lastfm.Api.Infrastracture;
using Universe.Lastfm.Api.Meta.Consts;
using Universe.Lastfm.Api.Models;
using Universe.Windows.Forms.Controls;
using Universe.Windows.Forms.Controls.Settings;
using Universe.Lastfm.Api.FormsApp.Themes;
using Universe.Lastfm.Api.Models.Base;
using Universe.Lastfm.Api.Models.Req;
using Universe.Lastfm.Api.Models.Res;
using Universe.Types.Collection;
using Universe.Lastfm.Api.Dto.Base;
using Universe.Lastfm.Api.FormsApp.Forms;
using Universe.Lastfm.Api.Dto.Common;

namespace Universe.Lastfm.Api.FormsApp
{
    public partial class MainForm : Form
    {
        //private GenreRepo _genreService;

        private readonly EventLogger _log;

        private UniverseLastApiAppSettings _programSettings;

        private ThreadMachine _threadMachine;

        private IUnityContainer _container;

        public UniverseLastApiScope? Scope { get; private set; }

        public ResizeFormState ResizeState;

        public ReqContext ReqCtx;

        public int LightErrorDelay => 500;

        public MainForm()
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterScreen;

            _log = new EventLogger();

            _log.LogInfo += e =>
            {
                if (e.AllowReport)
                {
                    var currentDate = DateTime.Now;
                    var message = $"[{currentDate}] {e.Message}{Environment.NewLine}";
                    this.SafeCall(() => this.tbLog.AppendText(message));
                    this.SafeCall(() => this.tbLog.Invalidate());
                }
            };

            _log.LogError += e =>
            {
                if (e.AllowReport)
                {
                    var currentDate = DateTime.Now;
                    var message = $"[{currentDate}] Во время выполнения операции произошла ошибка. Текст ошибки: {e.Message}.{Environment.NewLine} Трассировка стека: {e.Ex.StackTrace}{Environment.NewLine}";
                    this.SafeCall(() => this.tbLog.AppendText(message));
                    this.SafeCall(() => this.tbLog.Invalidate());
                }
            };

            MapperConfiguration.Configure();

            _container = UnityAppConfig.Container;

            ResizeState = new ResizeFormState(this.Size);
            ReqCtx = new ReqContext();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _programSettings = _programSettings.Load();
            LoadOnForm();

            _container.RegisterInstance<IUniverseLastApiSettings>(UnityRegistrations.ExternalAppSettings, _programSettings);
            Scope = _container.Resolve<IUniverseScope>() as UniverseLastApiScope;
        }

        public void LoadOnForm()
        {
            tbApiKey.Text = _programSettings.ApiKey;
            tbSecretKey.Text = _programSettings.SecretKey;

            chTrustedApp.Checked = _programSettings.IsTrustedApiApp;
            fullRubberyUIToolStripMenuItem.Checked = _programSettings.IsFullRubberUI;
            spaceModeToolStripMenuItem.Checked = _programSettings.IsSpaceMode;

            if (_programSettings.IsMaximized)
                this.WindowState = FormWindowState.Maximized;

            if (_programSettings.IsFullRubberUI)
                pMainForm.MassSetControlProperty(ctrl => { ctrl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left; });

            if (_programSettings.IsSpaceMode)
                SpaceThemeStyle.Set.Apply(pMainForm);

            ReqCtx = _programSettings.ReqCtx;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            UnLoadFromForm();
            _programSettings.Save();

            _threadMachine?.CancelAllThreads(true);
        }

        public void UnLoadFromForm()
        {
            _programSettings.ApiKey = tbApiKey.Text;
            _programSettings.SecretKey = tbSecretKey.Text;

            _programSettings.IsMaximized = this.WindowState == FormWindowState.Maximized;

            _programSettings.IsTrustedApiApp = chTrustedApp.Checked;
            _programSettings.IsFullRubberUI = fullRubberyUIToolStripMenuItem.Checked;

            _programSettings.IsSpaceMode = spaceModeToolStripMenuItem.Checked;

            _programSettings.ReqCtx = ReqCtx;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialogResult = MessageBox.Show(@"Do you want to exit from the application?", @"Exit", MessageBoxButtons.OKCancel);

            if (dialogResult == DialogResult.OK)
                Application.Exit();
        }

        private void chTrustedApp_CheckedChanged(object sender, EventArgs e)
        {
            _programSettings.IsTrustedApiApp = chTrustedApp.Checked;

            _container.RegisterInstance<IUniverseLastApiSettings>(UnityRegistrations.ExternalAppSettings, _programSettings);
            Scope = _container.Resolve<IUniverseScope>() as UniverseLastApiScope;
        }

        private void tbApiKey_TextChanged(object sender, EventArgs e)
        {
            _programSettings.ApiKey = tbApiKey.Text.Trim();

            _container.RegisterInstance<IUniverseLastApiSettings>(UnityRegistrations.ExternalAppSettings, _programSettings);
            Scope = _container.Resolve<IUniverseScope>() as UniverseLastApiScope;
        }

        private void tbClientSecret_TextChanged(object sender, EventArgs e)
        {
            _programSettings.SecretKey = tbSecretKey.Text.Trim();

            _container.RegisterInstance<IUniverseLastApiSettings>(UnityRegistrations.ExternalAppSettings, _programSettings);
            Scope = _container.Resolve<IUniverseScope>() as UniverseLastApiScope;
        }


        /// <summary>
        ///     Включение кнопок на панели методов апи Ласт Фм. Потокобезопасное
        /// </summary>
        public void EnableButtonsSafe()
        {
            pApiControls.EnableButtonsSafe();
        }

        /// <summary>
        ///     Выключение кнопок на панели, содержащей кнопки
        /// </summary>
        public void DisableButtons(object sender)
        {
            var senderButtonControl = sender as Button;
            pApiControls.DisableButtonsSafe();
        }

        private void btConnect_Click(object sender, EventArgs e)
        {
            ThreadMachine.Create(1).RunInMultiTheadsWithoutWaiting(() =>
            {

                try
                {
                    var apiKey = !string.IsNullOrEmpty(tbApiKey.Text.Trim())
                        ? tbApiKey.Text.Trim()
                        : throw new Exception("Не указан Api Key!");
                    var clientSecret = !string.IsNullOrEmpty(tbSecretKey.Text.Trim())
                        ? tbSecretKey.Text.Trim()
                        : throw new Exception("Не указан Client Secret!");

                    //_adapter = new LastfmFormsAdapter(apiKey, clientSecret);
                    //var responce = _adapter.SendAuthRequest(_programSettings.IsTrustedApiApp);

                    var allowAccess = Scope.GetQuery<RequestAccessQuery>().Execute(_programSettings.IsTrustedApiApp);

                    _log.Info($"Ответ сервиса: {allowAccess.ServiceAnswer}. ");

                    pApiControls.EnableButtonsSafe();
                    this.SafeCall(() => btRun.Enabled = true);

                    // SECOND STAGE

                    _log.Info($"Was started the second stage of auth: {allowAccess.ServiceAnswer}. ");
                    //var auth = Scope.GetQuery<AuthQuery>().ExecuteBaseSafe(new AuthRequest { Token = allowAccess.Token });

                    var connection = Scope.GetQuery<GetConnectQuery>();

                    var needApprovement = !this.SafeCallResult(() => chTrustedApp.Checked);
                    if (needApprovement)
                    {
                        _log.Info("You have 15 second for allow auth in opened browser page...");
                    }
                    connection.Connect(needApprovement);

                    var sk = connection.GetSecretKey();
                    var token = connection.GetToken();

                    ReqCtx.Token = token;
                    ReqCtx.SecretKey = sk;

                    connection.SetSessionKey(_programSettings.ApiKey, _programSettings.SecretKey, token);
                    var session = connection.GetSessionKey();
                    ReqCtx.SessionKey = session;

                    _log.Info($"Current session: {session}");
                }
                catch (Exception ex)
                {
                    _log.Error(ex, ex.Message);
                }
            });
        }

        private void btRun_Click(object sender, EventArgs e)
        {
            //var tag = "progressive metal";

            //_genreService = new GenreRepo(Scope);
            //_genreService.RunConnection(_programSettings.NeedApprovement);

            //var info = _genreService.GetInfo(tag);
            //_log.Info("Genre Info:" + info);

            //var similar = _genreService.GetSimilar(tag);
            //_log.Info("Similar:" + similar);

            //int position = _genreService.GetNumber(tag);
            //_log.Info("Position:" + position);

            string performer;
            string albumName;
            string trackName;
            string tagName;

            string userName;
            string[] addTags = new string[] { };
            string[] delTags = new string[] { };

            string outputFile;

            using (var form = new ReqContextForm(_programSettings))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    performer = form.Performer;
                    if (string.IsNullOrEmpty(performer))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указан performer!" + Environment.NewLine);
                        btRun.LightWarningColorResult();
                        return;
                    }

                    albumName = form.Album;
                    if (string.IsNullOrEmpty(albumName))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указан album!" + Environment.NewLine);
                        btRun.LightWarningColorResult();
                        return;
                    }

                    trackName = form.Track;
                    if (string.IsNullOrEmpty(trackName))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указан track!" + Environment.NewLine);
                        btRun.LightWarningColorResult();
                        return;
                    }

                    tagName = form.Tag;
                    if (string.IsNullOrEmpty(tagName))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указан tag / genre!" + Environment.NewLine);
                        btRun.LightWarningColorResult();
                        return;
                    }

                    userName = form.User;
                    if (string.IsNullOrEmpty(userName))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указано userName!" + Environment.NewLine);
                        btRun.LightWarningColorResult();
                        return;
                    }

                    addTags = form.AddTagsArray;
                    if (addTags.Length == 0)
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указаны tags!" + Environment.NewLine);
                        btRun.LightWarningColorResult();
                        return;
                    }

                    outputFile = form.OutputFile;
                }
                else
                {
                    return;
                }
            }


            ReqCtx.Performer = performer;
            ReqCtx.Album = albumName;
            ReqCtx.Track = trackName;
            ReqCtx.Tag = tagName;

            ReqCtx.User = userName;
            ReqCtx.Tags = addTags;
            ReqCtx.RemTag = delTags.FirstOrDefault();

            ReqCtx.Period = "";
            ReqCtx.Page = 2;
            ReqCtx.Limit = 25;

            ThreadMachine.Create(1).RunInMultiTheadsWithoutWaiting(() =>
            {
                var queries = new (BaseQuery Itself, Control Ctrl)[]
                {
                    (Scope.GetQuery<GetAlbumInfoQuery>(), btAlbumGetInfo),
                    (Scope.GetQuery<GetAlbumTagsQuery>(), btGetAlbumTags),
                    (Scope.GetQuery<SearchAlbumQuery>(), btAlbumSearch),
                    (Scope.GetQuery<GetAlbumTopTagsQuery>(), btAlbumGetTopTags),

                    (Scope.GetQuery<GetPerformerInfoQuery>(), btArtistGetInfo),
                    (Scope.GetQuery<GetPerformersTagsQuery>(), btGetArtistTags),
                    (Scope.GetQuery<SearchPerformersQuery>(), btArtistSearch),
                    (Scope.GetQuery<GetSimilarPerformersQuery>(), btArtistGetSimilar),
                    (Scope.GetQuery<GetPerformerCorrectionQuery>(), btArtistGetCorrection),
                    (Scope.GetQuery<GetPerformerGetTopAlbumsQuery>(), btArtistGetTopAlbums),
                    (Scope.GetQuery<GetPerformerGetTopTagsQuery>(), btArtistGetTopTags),
                    (Scope.GetQuery<GetPerformerGetTopTracksQuery>(), btArtistGetTopTracks),

                    (Scope.GetQuery<GetTagInfoQuery>(), btTagGetInfo),

                    (Scope.GetQuery<GetTrackInfoQuery>(), btTrackGetInfo),
                    (Scope.GetQuery<GetTrackTagQuery>(), btTrackGetTags),
                    (Scope.GetQuery<SearchTrackQuery>(), btTrackSearch),

                    (Scope.GetQuery<GetUserInfoQuery>(), btUserGetInfo),
                    (Scope.GetQuery<GetUserTopArtistsQuery>(),btUserGetTopArtists),
                    (Scope.GetQuery<GetUserTopAlbumsQuery>(),btUserGetTopAlbums),
                    (Scope.GetQuery<GetUserTopTagsQuery>(), btUserGetTopTags),
                    (Scope.GetQuery<GetUserTopTracksQuery>(),btUserGetTopTracks),
                    (Scope.GetQuery<GetUserLovedTracksQuery>(),btUserGetLovedTracks),
                    (Scope.GetQuery<GetPersonalTagsQuery>(), btUserGetPersonalTags),
                    (Scope.GetQuery<GetUserRecentTracksQuery>(), btUserGetRecentTracks),
                    (Scope.GetQuery<GetUserWeeklyAlbumChartQuery>(), btUserGetWeeklyAlbumChart),
                    (Scope.GetQuery<GetUserWeeklyArtistChartQuery>(), btUserGetWeeklyArtistChart),
                    (Scope.GetQuery<GetUserWeeklyChartListQuery>(), btUserGetWeeklyChartList),
                    (Scope.GetQuery<GetUserWeeklyTrackChartQuery>(), btUserGetWeeklyTrackChart),
                };

                var data = new MatList<LastFmBaseContainer>();
                foreach (var query in queries)
                {
                    var responce = query.Itself.ExecuteBaseSafe(ReqCtx).ReportResult(tbLog).LightColorResult(query.Ctrl, 50) as LastFmBaseContainer;
                    data += responce.DataContainer;
                }

                var fullInfo = JsonConvert.SerializeObject(data, Formatting.Indented);
                _log.Info($"FULL COLLECTION OF INFORMATION: {Environment.NewLine}{fullInfo}");

                var commands = new (BaseCommand Itself, Control Ctrl)[]
                {
                    (Scope.GetCommand<AddAlbumTagsCommand>(), btAlbumAddTags),
                    (Scope.GetCommand<DeleteAlbumTagsCommand>(), btAlbumRemoveTag)
                };

                foreach (var command in commands)
                {
                    command.Itself.ExecuteBaseSafe(ReqCtx).ReportResult(tbLog).LightColorResult(command.Ctrl, 50);
                }

                using (var fs = File.CreateText(outputFile))
                {
                    fs.WriteLine(fullInfo);
                    fs.Flush();
                    fs.Close();
                }
            });
        }

        private void btChartGetTopArtists_Click(object sender, EventArgs e)
        {
            DisableButtons(sender);

            ThreadMachine.Create(1).RunInMultiTheadsWithoutWaiting(() =>
            {
                try
                {
                    var filename = "ChartGetTopArtistsResearchRes.json";
                    var maxPages = 50;
                    var pageSize = 50;
                    var artists = new List<Artist>();
                    var loadedPages = 0;
                    var maxArtists = 500 * 100;

                    var unSuccessCounter = 0;
                    var chartPosition = 0;

                    for (int index = 1; index <= maxPages; index++)
                    {
                        //_adapter.ChartGetTopArtists(index, pageSize);
                        var responce = Scope.GetQuery<GetTopPerformersQuery>().Execute(index, pageSize);
                        if (!responce.IsSuccessful)
                        {
                            _log.Info($"{responce.Message} {responce.ServiceAnswer}");
                            unSuccessCounter++;
                            if (unSuccessCounter >= 50)
                                break;

                            continue;
                        }

                        unSuccessCounter = 0;
                        if (index == 1)
                            responce.ServiceAnswer = responce.ServiceAnswer.Replace("@attr", "ArtistsAttribute");

                        var deserialized = JsonConvert.DeserializeObject<ArtistsContainer>(responce.ServiceAnswer);

                        artists.AddRange(deserialized.Artists.Artist);

                        if (index == 1)
                        {
                            maxPages = deserialized.Artists.ArtistsAttribute.TotalPages;
                            maxArtists = deserialized.Artists.ArtistsAttribute.Total;
                        }

                        //if (index == 500)
                        //    break;

                        loadedPages++;
                        _log.Info($"Загружено {loadedPages * pageSize} исполнителей с Last.fm из {maxArtists}.");
                    }

                    artists = artists.OrderByDescending(x => x.Playcount).ToList();
                    foreach (var artist in artists)
                    {
                        artist.ChartPosition = ++chartPosition;
                    }

                    var serviceAnswer = JsonConvert.SerializeObject(artists, Formatting.Indented);

                    _log.Info(
                        $"Успешно выгружено {loadedPages} страниц по {pageSize} элементов в файл {filename}.");

                    using (var writer = File.CreateText(filename))
                    {
                        writer.WriteLine(serviceAnswer);
                    }

                    EnableButtonsSafe();
                }
                catch (Exception ex)
                {
                    _log.Error(ex, ex.Message);
                    EnableButtonsSafe();
                }
            });
        }

        private void btTrackScrobble_Click(object sender, EventArgs e)
        {
            DisableButtons(sender);
        }

        private void MainForm_MaximumSizeChanged(object sender, EventArgs e)
        {
            ResizeState.IsMaximized = !ResizeState.IsMaximized;
        }

        private void MainForm_MaximizedBoundsChanged(object sender, EventArgs e)
        {
            ResizeState.IsMaximized = !ResizeState.IsMaximized;
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState != FormWindowState.Minimized && fullRubberyUIToolStripMenuItem.Checked)
                ResizeState.ResizeUp(this, this.Size);
        }

        private void fullRubberyUIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fullRubberyUIToolStripMenuItem.Checked = !fullRubberyUIToolStripMenuItem.Checked;
            _programSettings.IsFullRubberUI = fullRubberyUIToolStripMenuItem.Checked;

            if (_programSettings.IsFullRubberUI)
                pMainForm.MassSetControlProperty(ctrl => { ctrl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left; });

            if (!_programSettings.IsFullRubberUI)
            {
                var msgState = MessageBox.Show(@"Need to restart application. Application will restart and apply settings.", @"Need to restart", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (msgState == DialogResult.OK)
                {
                    Application.Exit();
                }
                else
                {
                    fullRubberyUIToolStripMenuItem.Checked = !fullRubberyUIToolStripMenuItem.Checked;
                    _programSettings.IsFullRubberUI = fullRubberyUIToolStripMenuItem.Checked;
                }
            }
        }

        private void spaceModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            spaceModeToolStripMenuItem.Checked = !spaceModeToolStripMenuItem.Checked;
            _programSettings.IsSpaceMode = spaceModeToolStripMenuItem.Checked;

            if (_programSettings.IsSpaceMode)
                SpaceThemeStyle.Set.Apply(pMainForm);

            if (!_programSettings.IsSpaceMode)
            {
                var msgState = MessageBox.Show(@"Need to restart application. Application will restart and apply settings.", @"Need to restart", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (msgState == DialogResult.OK)
                {
                    Application.Exit();
                }
                else
                {
                    fullRubberyUIToolStripMenuItem.Checked = !fullRubberyUIToolStripMenuItem.Checked;
                    _programSettings.IsFullRubberUI = fullRubberyUIToolStripMenuItem.Checked;
                }
            }
        }
    }
}