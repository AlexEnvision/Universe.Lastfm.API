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

using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Universe.Algorithm.MultiThreading;
using Universe.Helpers.Extensions;
using Universe.Lastfm.Api.Dal.Command.Albums;
using Universe.Lastfm.Api.Dal.Queries.Albums;
using Universe.Lastfm.Api.Dal.Queries.Performers;
using Universe.Lastfm.Api.Dto.GetAlbumInfo;
using Universe.Lastfm.Api.FormsApp.Extensions;
using Universe.Lastfm.Api.FormsApp.Forms.Albums;
using Universe.Lastfm.Api.Helpers;
using Universe.Lastfm.Api.Models.Req;
using Universe.Lastfm.Api.Models.Res;
using static Universe.Lastfm.Api.Dal.Queries.Albums.SearchAlbumQuery;

namespace Universe.Lastfm.Api.FormsApp
{
    /// <summary>
    /// <author>Alex Universe</author>
    /// <author>Alex Envision</author>
    /// </summary>
    public partial class MainForm
    {
        private void btAlbumGetInfo_Click(object sender, EventArgs e)
        {
            string performer;
            string album;

            using (var form = new AlbumReqInfoForm(_programSettings))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    performer = form.Performer;
                    if (string.IsNullOrEmpty(performer))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указан performer!" + Environment.NewLine);
                        btAlbumGetInfo.LightWarningColorResult();
                        return;
                    }

                    album = form.Album;
                    if (string.IsNullOrEmpty(album))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указан album!" + Environment.NewLine);
                        btAlbumGetInfo.LightWarningColorResult();
                        return;
                    }
                }
                else
                {
                    return;
                }
            }

            DisableButtons(sender);
            ReqCtx.Album = album;
            ReqCtx.Performer = performer;
            //ReqCtx.Lang = "en";

            ThreadMachine.Create(1).RunInMultiTheadsWithoutWaiting(() =>
            {
                try
                {
                    // _adapter.GetAlbumInfo(performer, album);
                    GetAlbumInfoResponce responce = Scope.GetQuery<GetAlbumInfoQuery>().Execute(ReqCtx.As<GetAlbumInfoRequest>())
                        .LightColorResult(btAlbumGetInfo);
                    if (!responce.IsSuccessful)
                    {
                        _log.Info($"{responce.Message} {responce.ServiceAnswer}");
                        return;
                    }

                    _log.Info(
                        $"Успешно выгружена информация по альбому {album} исполнителя {performer}: {Environment.NewLine}{Environment.NewLine}{responce.ServiceAnswer}{Environment.NewLine}.");

                    var req = new GetAlbumWikiRequest()
                    {
                        Album = responce.DataContainer.Album,
                        Log = _log,
                    };
                    GetAlbumWikiResponce wikiResponce = Scope.GetQuery<GetAlbumWikiQuery>().Execute(req)
                        .LightColorResult(btAlbumGetInfo);
                    if (!responce.IsSuccessful)
                    {
                        _log.Info($"{responce.Message} {responce.ServiceAnswer}");
                        return;
                    }

                    _log.Info(
                        $"Успешно выгружена wiki по альбому {album} исполнителя {performer}: {Environment.NewLine}{Environment.NewLine}{wikiResponce.ServiceAnswer}{Environment.NewLine}.");

                    var albumRes = responce.DataContainer.Album;
                    var imResponce = Scope.GetQuery<GetAlbumImagesQuery>().Execute(
                        new GetAlbumImagesQuery.GetAlbumImagesRequest
                        {
                            Limit = 5,
                            Log = _log,
                            Album = albumRes
                        });

                    var largeImageSize =
                        JsonConvert.SerializeObject(imResponce.LargeSizeImageLinks, Formatting.Indented);
                    _log.Info(
                        $"Успешно выгружены ссылки на Large изображения по альбому {album}: {Environment.NewLine}{Environment.NewLine}{largeImageSize}{Environment.NewLine}.");

                    Album? albumInfo = responce.DataContainer.Album;

                    _log.Info(
                        $"Performer: {albumInfo.Artist}");
                    _log.Info(
                        $"Playcounts: {albumInfo.Playcount}");

                }
                catch (Exception ex)
                {
                    _log.Error(ex, ex.Message);
                    Thread.Sleep(LightErrorDelay);
                    btAlbumGetInfo.LightErrorColorResult();
                }
                finally
                {
                    EnableButtonsSafe();
                }
            });
        }

        private void btGetAlbumTags_Click(object sender, EventArgs e)
        {
            string performer;
            string album;
            string user;

            using (var form = new AlbumTagReqForm(_programSettings))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    performer = form.Performer;
                    if (string.IsNullOrEmpty(performer))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указан performer!" + Environment.NewLine);
                        btGetAlbumTags.LightWarningColorResult();
                        return;
                    }

                    album = form.Album;
                    if (string.IsNullOrEmpty(album))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указан album!" + Environment.NewLine);
                        btGetAlbumTags.LightWarningColorResult();
                        return;
                    }

                    user = form.User;
                    if (string.IsNullOrEmpty(user))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указан user!" + Environment.NewLine);
                        btGetAlbumTags.LightWarningColorResult();
                        return;
                    }
                }
                else
                {
                    return;
                }
            }

            DisableButtons(sender);
            ReqCtx.Album = album;
            ReqCtx.Performer = performer;
            ReqCtx.User = user;

            ThreadMachine.Create(1).RunInMultiTheadsWithoutWaiting(() =>
            {
                try
                {
                    //  _adapter.GetAlbumTags(performer, album, user);
                    var responce = Scope.GetQuery<GetAlbumTagsQuery>().Execute(ReqCtx.As<GetAlbumTagsRequest>())
                        .LightColorResult(btGetAlbumTags);
                    if (!responce.IsSuccessful)
                    {
                        _log.Info($"{responce.Message} {responce.ServiceAnswer}");
                        return;
                    }

                    _log.Info(
                        $"Успешно выгружена информация по альбому {album} исполнителя {performer}: {Environment.NewLine}{Environment.NewLine}{responce.ServiceAnswer}{Environment.NewLine}.");

                    var tags = responce.DataContainer.Tag;
                    var metalGenres = tags.Where(x => x.Name.ToLower().Contains("metal")).ToList();

                    var tagsStr = string.Join(", ", tags.Select(x => x.Name));
                    var metalGenresStr = string.Join(", ", metalGenres.Select(x => x.Name));
                    _log.Info($"Теги Last.fm: {tagsStr}.");
                    _log.Info($"Метал жанры: {metalGenresStr}.");
                }
                catch (Exception ex)
                {
                    _log.Error(ex, ex.Message);
                    Thread.Sleep(LightErrorDelay);
                    btGetAlbumTags.LightErrorColorResult();
                }
                finally
                {
                    EnableButtonsSafe();
                }
            });
        }

        private void btAlbumSearch_Click(object sender, EventArgs e)
        {
            string performer;
            string albumName;

            using (var form = new AlbumSearchReqForm(_programSettings))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    performer = form.Performer;
                    if (string.IsNullOrEmpty(performer))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указан performer!" + Environment.NewLine);
                        btAlbumSearch.LightWarningColorResult();
                        return;
                    }

                    albumName = form.Album;
                    if (string.IsNullOrEmpty(albumName))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указан album!" + Environment.NewLine);
                        btAlbumSearch.LightWarningColorResult();
                        return;
                    }
                }
                else
                {
                    return;
                }
            }

            DisableButtons(sender);
            ReqCtx.Album = albumName;
            ReqCtx.Performer = performer;

            ThreadMachine.Create(1).RunInMultiTheadsWithoutWaiting(() =>
            {
                try
                {
                    var responce = Scope.GetQuery<SearchAlbumQuery>().Execute(ReqCtx.As<GetAlbumSearchRequest>())
                        .LightColorResult(btAlbumSearch);
                    if (!responce.IsSuccessful)
                    {
                        _log.Info($"{responce.Message} {responce.ServiceAnswer}");
                        return;
                    }

                    _log.Info(
                        $"Успешно выгружена информация по пользователю {albumName}: {Environment.NewLine}{Environment.NewLine}{responce.ServiceAnswer}{Environment.NewLine}.");

                    var attribute = responce.DataContainer.Results.Attribute;
                    var data = responce.DataContainer.Results;
                    var dataStr = string.Join(", ", data.AlbumMatches.Album.Select(x => x.Name));

                    _log.Info($"Search album result by the name {albumName} / Результат поиска альбомов по названию {albumName}: {dataStr}.");

                    _log.Info(Environment.NewLine);
                }
                catch (Exception ex)
                {
                    _log.Error(ex, ex.Message);
                    Thread.Sleep(LightErrorDelay);
                    btAlbumSearch.LightErrorColorResult();
                }
                finally
                {
                    EnableButtonsSafe();
                }
            });
        }

        private void btAlbumGetTopTags_Click(object sender, EventArgs e)
        {
            string performer;
            string albumName;

            using (var form = new AlbumTopTagsReqForm(_programSettings))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    performer = form.Performer;
                    if (string.IsNullOrEmpty(performer))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указан performer!" + Environment.NewLine);
                        btAlbumGetTopTags.LightWarningColorResult();
                        return;
                    }

                    albumName = form.Album;
                    if (string.IsNullOrEmpty(albumName))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указан album!" + Environment.NewLine);
                        btAlbumGetTopTags.LightWarningColorResult();
                        return;
                    }
                }
                else
                {
                    return;
                }
            }

            DisableButtons(sender);
            ReqCtx.Album = albumName;
            ReqCtx.Performer = performer;

            ThreadMachine.Create(1).RunInMultiTheadsWithoutWaiting(() =>
            {
                try
                {
                    var responce = Scope.GetQuery<GetAlbumTopTagsQuery>().Execute(ReqCtx.As<GetAlbumTopTagsQuery.GetAlbumTopTagsRequest>())
                        .LightColorResult(btAlbumGetTopTags);
                    if (!responce.IsSuccessful)
                    {
                        _log.Info($"{responce.Message} {responce.ServiceAnswer}");
                        return;
                    }

                    _log.Info(
                        $"Успешно выгружена информация по пользователю {albumName}: {Environment.NewLine}{Environment.NewLine}{responce.ServiceAnswer}{Environment.NewLine}.");

                    var attribute = responce.DataContainer.TopTags.Attribute;
                    var data = responce.DataContainer.TopTags;
                    var dataStr = string.Join(", ", data.Tag.Select(x => x.Name));

                    _log.Info($"Search top tags/genres of album result by the name {albumName} / Результат поиска топ-тэгов/жанров альбома по названию {albumName}: {dataStr}.");

                    _log.Info(Environment.NewLine);
                }
                catch (Exception ex)
                {
                    _log.Error(ex, ex.Message);
                    Thread.Sleep(LightErrorDelay);
                    btAlbumGetTopTags.LightErrorColorResult();
                }
                finally
                {
                    EnableButtonsSafe();
                }
            });
        }

        private void btAlbumAddTags_Click(object sender, EventArgs e)
        {
            string performer;
            string albumName;
            string[] tags;

            using (var form = new AlbumAddTagsReqForm(_programSettings))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    performer = form.Performer;
                    if (string.IsNullOrEmpty(performer))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указан performer!" + Environment.NewLine);
                        btAlbumAddTags.LightWarningColorResult();
                        return;
                    }

                    albumName = form.Album;
                    if (string.IsNullOrEmpty(albumName))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указан album!" + Environment.NewLine);
                        btAlbumAddTags.LightWarningColorResult();
                        return;
                    }

                    tags = form.TagsArray;
                    if (tags.Length == 0)
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указаны tags!" + Environment.NewLine);
                        btAlbumAddTags.LightWarningColorResult();
                        return;
                    }
                }
                else
                {
                    return;
                }
            }

            DisableButtons(sender);
            ReqCtx.Album = albumName;
            ReqCtx.Performer = performer;
            ReqCtx.Tags = tags;

            ThreadMachine.Create(1).RunInMultiTheadsWithoutWaiting(() =>
            {
                try
                {
                    var responce = Scope.GetCommand<AddAlbumTagsCommand>().Execute(ReqCtx.As<AddAlbumTagsRequest>())
                        .LightColorResult(btAlbumAddTags);
                    if (!responce.IsSuccessful)
                    {
                        _log.Info($"{responce.Message} {responce.ServiceAnswer}");
                        return;
                    }

                    _log.Info($"Update {albumName} result: {string.Join("", responce.Responces.Select(x => x.ServiceAnswer).ToList())}{Environment.NewLine}");
                }
                catch (Exception ex)
                {
                    _log.Error(ex, ex.Message);
                    Thread.Sleep(LightErrorDelay);
                    btAlbumAddTags.LightErrorColorResult();
                }
                finally
                {
                    EnableButtonsSafe();
                }
            });
        }

        private void btAlbumRemoveTag_Click(object sender, EventArgs e)
        {
            string performer;
            string albumName;
            string tag;

            using (var form = new AlbumDeleteTagsReqForm(_programSettings))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    performer = form.Performer;
                    if (string.IsNullOrEmpty(performer))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указан performer!" + Environment.NewLine);
                        btAlbumRemoveTag.LightWarningColorResult();
                        return;
                    }

                    albumName = form.Album;
                    if (string.IsNullOrEmpty(albumName))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указан album!" + Environment.NewLine);
                        btAlbumRemoveTag.LightWarningColorResult();
                        return;
                    }

                    tag = form.TagsArray.FirstOrDefault() ?? "";
                    if (tag.IsNullOrEmpty())
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указаны tags!" + Environment.NewLine);
                        btAlbumRemoveTag.LightWarningColorResult();
                        return;
                    }
                }
                else
                {
                    return;
                }
            }

            DisableButtons(sender);
            ReqCtx.Album = albumName;
            ReqCtx.Performer = performer;
            ReqCtx.RemTag = tag;

            ThreadMachine.Create(1).RunInMultiTheadsWithoutWaiting(() =>
            {
                try
                {
                    var responce = Scope.GetCommand<DeleteAlbumTagsCommand>().Execute(ReqCtx.As<DeleteAlbumTagsRequest>())
                        .LightColorResult(btAlbumRemoveTag);
                    if (!responce.IsSuccessful)
                    {
                        _log.Info($"{responce.Message} {responce.ServiceAnswer}");
                        return;
                    }

                    _log.Info($"Update {albumName} result: {responce.ServiceAnswer}{Environment.NewLine}");
                }
                catch (Exception ex)
                {
                    _log.Error(ex, ex.Message);
                    Thread.Sleep(LightErrorDelay);
                    btAlbumRemoveTag.LightErrorColorResult();
                }
                finally
                {
                    EnableButtonsSafe();
                }
            });
        }
    }
}