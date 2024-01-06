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
using Universe.Lastfm.Api.Dal.Queries.Albums;
using Universe.Lastfm.Api.Dal.Queries.Auth;
using Universe.Lastfm.Api.Dal.Queries.Performers;
using Universe.Lastfm.Api.Dal.Queries.Tags;
using Universe.Lastfm.Api.Dal.Queries.Track;
using Universe.Lastfm.Api.Dto.GetArtists;
using Universe.Lastfm.Api.FormsApp.Extensions;
using Universe.Lastfm.Api.FormsApp.Forms.Albums;
using Universe.Lastfm.Api.FormsApp.Forms.Genres;
using Universe.Lastfm.Api.FormsApp.Forms.Tracks;
using Universe.Lastfm.Api.FormsApp.Infrastracture;
using Universe.Lastfm.Api.FormsApp.Settings;
using Universe.Lastfm.Api.Helpers;
using Universe.Lastfm.Api.Infrastracture;
using Universe.Lastfm.Api.Meta.Consts;
using Universe.Lastfm.Api.Repos;
using Universe.Windows.Forms.Controls;
using Universe.Windows.Forms.Controls.Settings;

namespace Universe.Lastfm.Api.FormsApp
{
    public partial class MainForm : Form
    {
        private GenreRepo _genreService;

        private readonly EventLogger _log;

        private UniverseLastApiAppSettings _programSettings;

        private ThreadMachine _threadMachine;

        private IUnityContainer _container;

        public UniverseLastApiScope? Scope { get; private set; }

        public MainForm()
        {
            InitializeComponent();

            _log = new EventLogger();

            _log.LogInfo += e =>
            {
                if (e.AllowReport)
                {
                    var currentDate = DateTime.Now;
                    var message = $"[{currentDate}] {e.Message}{Environment.NewLine}";
                    this.SafeCall(() => this.tbLog.AppendText(message));
                }
            };

            _log.LogError += e =>
            {
                if (e.AllowReport)
                {
                    var currentDate = DateTime.Now;
                    var message = $"[{currentDate}] Во время выполнения операции произошла ошибка. Текст ошибки: {e.Message}.{Environment.NewLine} Трассировка стека: {e.Ex.StackTrace}{Environment.NewLine}";
                    this.SafeCall(() => this.tbLog.AppendText(message));
                }
            };

            MapperConfiguration.Configure();

            _container = UnityAppConfig.Container;
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

            _programSettings.IsTrustedApiApp = chTrustedApp.Checked;
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
                //var auth = LastScopeExtensions.GetQuery<AuthQuery>(Scope).Execute(allowAccess);

                _log.Info($"Ответ сервиса: {allowAccess.ServiceAnswer}. ");

                pApiControls.EnableButtonsSafe();
            }
            catch (Exception ex)
            {
                _log.Error(ex, ex.Message);
            }
        }

        private void btRun_Click(object sender, EventArgs e)
        {
            var tag = "progressive metal";

            _genreService = new GenreRepo(Scope);
            _genreService.RunConnection(_programSettings.NeedApprovement);

            var info = _genreService.GetInfo(tag);
            _log.Info("Genre Info:" + info);

            var similar = _genreService.GetSimilar(tag);
            _log.Info("Similar:" + similar);

            int position = _genreService.GetNumber(tag);
            _log.Info("Position:" + position);
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

        private void btAlbumGetInfo_Click(object sender, EventArgs e)
        {
            string performer;
            string album;

            using (var albumReqInfoForm = new AlbumReqInfoForm())
            {
                if (albumReqInfoForm.ShowDialog() == DialogResult.OK)
                {
                    performer = albumReqInfoForm.Performer;
                    if (string.IsNullOrEmpty(performer))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указан performer!" + Environment.NewLine);
                        return;
                    }

                    album = albumReqInfoForm.Album;
                    if (string.IsNullOrEmpty(album))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указан album!" + Environment.NewLine);
                        return;
                    }
                }
                else
                {
                    return;
                }
            }

            DisableButtons(sender);

            ThreadMachine.Create(1).RunInMultiTheadsWithoutWaiting(() =>
            {
                try
                {
                    // _adapter.GetAlbumInfo(performer, album);
                    var responce = Scope.GetQuery<GetAlbumInfoQuery>().Execute(performer, album);
                    if (!responce.IsSuccessful)
                    {
                        _log.Info($"{responce.Message} {responce.ServiceAnswer}");
                    }

                    _log.Info(
                        $"Успешно выгружена информация по альбому {album} исполнителя {performer}: {Environment.NewLine}{Environment.NewLine}{responce.ServiceAnswer}{Environment.NewLine}.");

                    var tags = responce.DataContainer.Album.Tags.Tag;
                    var metalGenres = tags.Where(x => x.Name.ToLower().Contains("metal")).ToList();

                    var tagsStr = string.Join(", ", tags.Select(x => x.Name));
                    var metalGenresStr = string.Join(", ", metalGenres.Select(x => x.Name));
                    _log.Info($"Теги Last.fm: {tagsStr}.");
                    _log.Info($"Метал жанры: {metalGenresStr}.");

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

        private void btArtistGetInfo_Click(object sender, EventArgs e)
        {
            string performer;

            using (var albumReqInfoForm = new ArtistReqInfoForm())
            {
                if (albumReqInfoForm.ShowDialog() == DialogResult.OK)
                {
                    performer = albumReqInfoForm.Performer;
                    if (string.IsNullOrEmpty(performer))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указан performer!" + Environment.NewLine);
                        return;
                    }
                }
                else
                {
                    return;
                }
            }

            DisableButtons(sender);

            ThreadMachine.Create(1).RunInMultiTheadsWithoutWaiting(() =>
            {
                try
                {
                    // _adapter.GetArtistInfo(performer);
                    var responce = Scope.GetQuery<GetPerformerInfoQuery>().Execute(performer);
                    if (!responce.IsSuccessful)
                    {
                        _log.Info($"{responce.Message} {responce.ServiceAnswer}");
                    }

                    _log.Info(
                        $"Успешно выгружена информация по исполнителю {performer}: {Environment.NewLine}{Environment.NewLine}{responce.ServiceAnswer}{Environment.NewLine}.");

                    var tags = responce.DataContainer.Artist.Tags.Tag;
                    var metalGenres = tags.Where(x => x.Name.ToLower().Contains("metal")).ToList();

                    var tagsStr = string.Join(", ", tags.Select(x => x.Name));
                    var metalGenresStr = string.Join(", ", metalGenres.Select(x => x.Name));
                    _log.Info($"Теги Last.fm: {tagsStr}.");
                    _log.Info($"Метал жанры: {metalGenresStr}.");

                    EnableButtonsSafe();
                }
                catch (Exception ex)
                {
                    _log.Error(ex, ex.Message);
                    EnableButtonsSafe();
                }
            });
        }

        private void btGetAlbumTags_Click(object sender, EventArgs e)
        {
            string performer;
            string album;
            string user;

            using (var albumReqInfoForm = new AlbumTagReqForm())
            {
                if (albumReqInfoForm.ShowDialog() == DialogResult.OK)
                {
                    performer = albumReqInfoForm.Performer;
                    if (string.IsNullOrEmpty(performer))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указан performer!" + Environment.NewLine);
                        return;
                    }

                    album = albumReqInfoForm.Album;
                    if (string.IsNullOrEmpty(album))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указан album!" + Environment.NewLine);
                        return;
                    }

                    user = albumReqInfoForm.User;
                    if (string.IsNullOrEmpty(user))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указан user!" + Environment.NewLine);
                        return;
                    }
                }
                else
                {
                    return;
                }
            }

            DisableButtons(sender);

            ThreadMachine.Create(1).RunInMultiTheadsWithoutWaiting(() =>
            {
                try
                {
                    //  _adapter.GetAlbumTags(performer, album, user);
                    var responce = Scope.GetQuery<GetAlbumTagsQuery>().Execute(performer, album, user);
                    if (!responce.IsSuccessful)
                    {
                        _log.Info($"{responce.Message} {responce.ServiceAnswer}");
                    }

                    _log.Info(
                        $"Успешно выгружена информация по альбому {album} исполнителя {performer}: {Environment.NewLine}{Environment.NewLine}{responce.ServiceAnswer}{Environment.NewLine}.");

                    var tags = responce.DataContainer.Tag;
                    var metalGenres = tags.Where(x => x.Name.ToLower().Contains("metal")).ToList();

                    var tagsStr = string.Join(", ", tags.Select(x => x.Name));
                    var metalGenresStr = string.Join(", ", metalGenres.Select(x => x.Name));
                    _log.Info($"Теги Last.fm: {tagsStr}.");
                    _log.Info($"Метал жанры: {metalGenresStr}.");

                    EnableButtonsSafe();
                }
                catch (Exception ex)
                {
                    _log.Error(ex, ex.Message);
                    EnableButtonsSafe();
                }
            });
        }

        private void btGetArtistTags_Click(object sender, EventArgs e)
        {
            string performer;
            string user;

            using (var form = new ArtistTagsReqForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    performer = form.Performer;
                    if (string.IsNullOrEmpty(performer))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указан performer!" + Environment.NewLine);
                        return;
                    }

                    user = form.User;
                    if (string.IsNullOrEmpty(user))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указан user!" + Environment.NewLine);
                        return;
                    }
                }
                else
                {
                    return;
                }
            }

            DisableButtons(sender);

            ThreadMachine.Create(1).RunInMultiTheadsWithoutWaiting(() =>
            {
                try
                {
                    // _adapter.GetArtistTags(performer, user);
                    var responce = Scope.GetQuery<GetPerformersTagsQuery>().Execute(performer, user);
                    if (!responce.IsSuccessful)
                    {
                        _log.Info($"{responce.Message} {responce.ServiceAnswer}");
                    }

                    _log.Info(
                        $"Успешно выгружена информация по исполнителю {performer}: {Environment.NewLine}{Environment.NewLine}{responce.ServiceAnswer}{Environment.NewLine}.");

                    var tags = responce.DataContainer.Tag;
                    var metalGenres = tags.Where(x => x.Name.ToLower().Contains("metal")).ToList();

                    var tagsStr = string.Join(", ", tags.Select(x => x.Name));
                    var metalGenresStr = string.Join(", ", metalGenres.Select(x => x.Name));
                    _log.Info($"Теги Last.fm: {tagsStr}.");
                    _log.Info($"Метал жанры: {metalGenresStr}.");

                    EnableButtonsSafe();
                }
                catch (Exception ex)
                {
                    _log.Error(ex, ex.Message);
                    EnableButtonsSafe();
                }
            });
        }

        private void btTagGetInfo_Click(object sender, EventArgs e)
        {
            string tagName;

            using (var form = new TagInfoReqForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    tagName = form.Tag;
                    if (string.IsNullOrEmpty(tagName))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указан tag / genre!" + Environment.NewLine);
                        return;
                    }
                }
                else
                {
                    return;
                }
            }

            DisableButtons(sender);

            ThreadMachine.Create(1).RunInMultiTheadsWithoutWaiting(() =>
            {
                try
                {
                    var responce = Scope.GetQuery<GetTagInfoQuery>().Execute(tagName);
                    if (!responce.IsSuccessful)
                    {
                        _log.Info($"{responce.Message} {responce.ServiceAnswer}");
                    }

                    _log.Info(
                        $"Успешно выгружена информация по тэгу / жанру {tagName}: {Environment.NewLine}{Environment.NewLine}{responce.ServiceAnswer}{Environment.NewLine}.");

                    var tag = responce.DataContainer.Tag;
                    var metalGenre = tag.Name.ToLower().Contains("metal") ? tag.Name : string.Empty;

                    var tagsStr = tag.Name;
                    var metalGenresStr = metalGenre;
                    _log.Info($"Теги Last.fm: {tagsStr}.");
                    _log.Info($"Метал жанры: {metalGenresStr}.");

                    _log.Info($"Total / Используется: {tag.Total}.");
                    _log.Info($"Short description / Краткое описание: {tag.Wiki.Summary}.");
                    _log.Info($"Full description / Полное описание: {tag.Wiki.Content}.");

                    EnableButtonsSafe();
                }
                catch (Exception ex)
                {
                    _log.Error(ex, ex.Message);
                    EnableButtonsSafe();
                }
            });
        }

        private void btTrackGetInfo_Click(object sender, EventArgs e)
        {
            string performer;
            string trackName;

            using (var form = new TrackInfoReqForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    performer = form.Performer;
                    if (string.IsNullOrEmpty(performer))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указан performer!" + Environment.NewLine);
                        return;
                    }

                    trackName = form.Track;
                    if (string.IsNullOrEmpty(trackName))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указан track!" + Environment.NewLine);
                        return;
                    }
                }
                else
                {
                    return;
                }
            }

            DisableButtons(sender);

            ThreadMachine.Create(1).RunInMultiTheadsWithoutWaiting(() =>
            {
                try
                {
                    var responce = Scope.GetQuery<GetTrackInfoQuery>().Execute(performer, trackName);
                    if (!responce.IsSuccessful)
                    {
                        _log.Info($"{responce.Message} {responce.ServiceAnswer}");
                    }

                    _log.Info(
                        $"Успешно выгружена информация по альбому {trackName} исполнителя {performer}: {Environment.NewLine}{Environment.NewLine}{responce.ServiceAnswer}{Environment.NewLine}.");

                    var tags = responce.DataContainer.Track.Toptags.Tag;
                    var metalGenres = tags.Where(x => x.Name.ToLower().Contains("metal")).ToList();

                    var tagsStr = string.Join(", ", tags.Select(x => x.Name));
                    var metalGenresStr = string.Join(", ", metalGenres.Select(x => x.Name));
                    _log.Info($"Теги трэка на Last.fm: {tagsStr}.");
                    _log.Info($"Метал жанры в трэке: {metalGenresStr}.");

                    var track = responce.DataContainer.Track;

                    _log.Info($"Total / Прослушиваний: {track.Playcount}.");
                    _log.Info($"Short description / Краткое описание: {track.Wiki.Summary}.");
                    _log.Info($"Full description / Полное описание: {track.Wiki.Content}.");

                    _log.Info(Environment.NewLine);

                    EnableButtonsSafe();
                }
                catch (Exception ex)
                {
                    _log.Error(ex, ex.Message);
                    EnableButtonsSafe();
                }
            });
        }
    }
}