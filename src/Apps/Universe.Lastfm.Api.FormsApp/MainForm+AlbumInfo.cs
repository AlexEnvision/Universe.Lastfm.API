using System;
using System.Linq;
using System.Windows.Forms;
using Universe.Algorithm.MultiThreading;
using Universe.Lastfm.Api.Dal.Queries.Albums;
using Universe.Lastfm.Api.FormsApp.Extensions;
using Universe.Lastfm.Api.FormsApp.Forms.Albums;
using Universe.Lastfm.Api.Helpers;
using Universe.Lastfm.Api.Models.Req;
using Universe.Lastfm.Api.Dal.Command.Albums;

using static Universe.Lastfm.Api.Dal.Queries.Albums.GetAlbumInfoQuery;
using static Universe.Lastfm.Api.Dal.Queries.Albums.GetAlbumTagsQuery;
using static Universe.Lastfm.Api.Dal.Queries.Albums.SearchAlbumQuery;
using static Universe.Lastfm.Api.Dal.Queries.Albums.GetAlbumTopTagsQuery;

using static Universe.Lastfm.Api.Dal.Command.Albums.AddAlbumTagsCommand;
using System.Threading;
using Universe.Helpers.Extensions;

namespace Universe.Lastfm.Api.FormsApp
{
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

            ThreadMachine.Create(1).RunInMultiTheadsWithoutWaiting(() =>
            {
                try
                {
                    // _adapter.GetAlbumInfo(performer, album);
                    var responce = Scope.GetQuery<GetAlbumInfoQuery>().Execute(ReqCtx.As<GetAlbumInfoRequest>())
                        .LightColorResult(btAlbumGetInfo);
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

                }
                catch (Exception ex)
                {
                    _log.Error(ex, ex.Message);
                    Thread.Sleep(LightErrorDelay);
                    btUserGetInfo.LightErrorColorResult();
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
                    }

                    _log.Info(
                        $"Успешно выгружена информация по пользователю {albumName}: {Environment.NewLine}{Environment.NewLine}{responce.ServiceAnswer}{Environment.NewLine}.");

                    var tagsStr = string.Join(";", ReqCtx.Tags);

                    var data = responce.Responces.Select(x => x.DataContainer);
                    var dataStr = string.Join(", ", data.SelectMany(x => x.Lfm.Status));

                    _log.Info($"Result of adding tags/genres of album by the names {tagsStr} / Результат добавления тэгов/жанров альбома по названиям {tagsStr}: {dataStr}.");

                    _log.Info(Environment.NewLine);
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
            ReqCtx.Tag = tag;

            ThreadMachine.Create(1).RunInMultiTheadsWithoutWaiting(() =>
            {
                try
                {
                    var responce = Scope.GetCommand<DeleteAlbumTagsCommand>().Execute(ReqCtx.As<DeleteAlbumTagsRequest>())
                        .LightColorResult(btAlbumRemoveTag);
                    if (!responce.IsSuccessful)
                    {
                        _log.Info($"{responce.Message} {responce.ServiceAnswer}");
                    }

                    _log.Info(
                        $"Успешно выгружена информация по пользователю {albumName}: {Environment.NewLine}{Environment.NewLine}{responce.ServiceAnswer}{Environment.NewLine}.");

                    var tagsStr = string.Join(";", ReqCtx.Tags);

                    var data = responce.DataContainer;
                    var dataStr = string.Join(", ", data.Lfm.Status);

                    _log.Info($"Result of deleting tags/genres of album by the names {tagsStr} / Результат удаления тэгов/жанров альбома по названиям {tagsStr}: {dataStr}.");

                    _log.Info(Environment.NewLine);
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