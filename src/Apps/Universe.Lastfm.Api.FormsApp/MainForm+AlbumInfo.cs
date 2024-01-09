using System;
using System.Linq;
using System.Windows.Forms;
using Universe.Algorithm.MultiThreading;
using Universe.Lastfm.Api.Dal.Queries.Albums;
using Universe.Lastfm.Api.FormsApp.Extensions;
using Universe.Lastfm.Api.FormsApp.Forms.Albums;
using Universe.Lastfm.Api.Helpers;

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
                        return;
                    }

                    album = form.Album;
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
                    var responce = Scope.GetQuery<GetAlbumInfoQuery>().Execute(performer, album)
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
                        return;
                    }

                    album = form.Album;
                    if (string.IsNullOrEmpty(album))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указан album!" + Environment.NewLine);
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
                    //  _adapter.GetAlbumTags(performer, album, user);
                    var responce = Scope.GetQuery<GetAlbumTagsQuery>().Execute(performer, album, user)
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
                        return;
                    }

                    albumName = form.Album;
                    if (string.IsNullOrEmpty(albumName))
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
                    var responce = Scope.GetQuery<SearchAlbumQuery>().Execute(albumName)
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
                    btAlbumSearch.LightErrorColorResult();
                }
                finally
                {
                    EnableButtonsSafe();
                }
            });
        }
    }
}