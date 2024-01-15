using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Universe.Algorithm.MultiThreading;
using Universe.Lastfm.Api.Dal.Queries.Tags;
using Universe.Lastfm.Api.FormsApp.Extensions;
using Universe.Lastfm.Api.FormsApp.Forms.Genres;
using Universe.Lastfm.Api.Helpers;
using Universe.Lastfm.Api.Models.Req;
using Universe.Lastfm.Api.Models.Req.Genre;

namespace Universe.Lastfm.Api.FormsApp
{
    public partial class MainForm : Form
    {
        private void btTagGetInfo_Click(object sender, EventArgs e)
        {
            string tagName;

            using (var form = new TagInfoReqForm(_programSettings))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    tagName = form.Tag;
                    if (string.IsNullOrEmpty(tagName))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указан tag / genre!" + Environment.NewLine);
                        btTagGetInfo.LightWarningColorResult();
                        return;
                    }
                }
                else
                {
                    return;
                }
            }

            DisableButtons(sender);
            ReqCtx.Tag = tagName;

            ThreadMachine.Create(1).RunInMultiTheadsWithoutWaiting(() =>
            {
                try
                {
                    var responce = Scope.GetQuery<GetTagInfoQuery>().Execute(ReqCtx.As<GetTagInfoRequest>())
                        .LightColorResult(btTagGetInfo);
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
                }
                catch (Exception ex)
                {
                    _log.Error(ex, ex.Message);
                    Thread.Sleep(LightErrorDelay);
                    btTagGetInfo.LightErrorColorResult();
                }
                finally
                {
                    EnableButtonsSafe();
                }
            });
        }

        private void btTagGetSimilar_Click(object sender, EventArgs e)
        {
            string tagName;

            using (var form = new TagGetSimilarReqForm(_programSettings))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    tagName = form.Tag;
                    if (string.IsNullOrEmpty(tagName))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указан tag / genre!" + Environment.NewLine);
                        btTagGetSimilar.LightWarningColorResult();
                        return;
                    }
                }
                else
                {
                    return;
                }
            }

            DisableButtons(sender);
            ReqCtx.Tag = tagName;

            ThreadMachine.Create(1).RunInMultiTheadsWithoutWaiting(() =>
            {
                try
                {
                    var responce = Scope.GetQuery<GetTagSimilarQuery>().Execute(ReqCtx.As<GetTagSimilarRequest>())
                        .LightColorResult(btTagGetSimilar);
                    if (!responce.IsSuccessful)
                    {
                        _log.Info($"{responce.Message} {responce.ServiceAnswer}");
                    }

                    _log.Info(
                        $"Успешно выгружена информация по тэгу / жанру {tagName}: {Environment.NewLine}{Environment.NewLine}{responce.ServiceAnswer}{Environment.NewLine}.");

                    var tag = responce.DataContainer.SimilarTags.Tag;
                    var metalGenre = tag.Select(x => x.Name.ToLower().Contains("metal") ? x.Name : string.Empty).ToList();

                    var tagsStr = string.Join(", ", tag.Select(x => x.Name));
                    var metalGenresStr = metalGenre;
                    _log.Info($"Теги Last.fm: {tagsStr}.");
                    _log.Info($"Метал жанры: {metalGenresStr}.");
                }
                catch (Exception ex)
                {
                    _log.Error(ex, ex.Message);
                    Thread.Sleep(LightErrorDelay);
                    btTagGetSimilar.LightErrorColorResult();
                }
                finally
                {
                    EnableButtonsSafe();
                }
            });
        }

        private void btTagGetTopAlbums_Click(object sender, EventArgs e)
        {
            string tagName;

            using (var form = new TagGetTopAlbumsReqForm(_programSettings))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    tagName = form.Tag;
                    if (string.IsNullOrEmpty(tagName))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указан tag / genre!" + Environment.NewLine);
                        btTagGetTopAlbums.LightWarningColorResult();
                        return;
                    }
                }
                else
                {
                    return;
                }
            }

            DisableButtons(sender);
            ReqCtx.Tag = tagName;

            ThreadMachine.Create(1).RunInMultiTheadsWithoutWaiting(() =>
            {
                try
                {
                    var responce = Scope.GetQuery<GetTagTopAlbumsQuery>().Execute(ReqCtx.As<GetTagTopAlbumsRequest>())
                        .LightColorResult(btTagGetTopAlbums);
                    if (!responce.IsSuccessful)
                    {
                        _log.Info($"{responce.Message} {responce.ServiceAnswer}");
                    }

                    _log.Info(
                        $"Успешно выгружена информация по тэгу / жанру {tagName}: {Environment.NewLine}{Environment.NewLine}{responce.ServiceAnswer}{Environment.NewLine}.");

                    var tag = responce.DataContainer.Albums;

                    var tagsStr = string.Join("; ", tag.Album.Select(x => x.Name));
                    _log.Info($"Top albums genres/tags Last.fm: {tagsStr}.");
                }
                catch (Exception ex)
                {
                    _log.Error(ex, ex.Message);
                    Thread.Sleep(LightErrorDelay);
                    btTagGetTopAlbums.LightErrorColorResult();
                }
                finally
                {
                    EnableButtonsSafe();
                }
            });
        }

        private void btTagGetTopArtists_Click(object sender, EventArgs e)
        {
            string tagName;

            using (var form = new TagGetTopArtistsReqForm(_programSettings))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    tagName = form.Tag;
                    if (string.IsNullOrEmpty(tagName))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указан tag / genre!" + Environment.NewLine);
                        btTagGetTopArtists.LightWarningColorResult();
                        return;
                    }
                }
                else
                {
                    return;
                }
            }

            DisableButtons(sender);
            ReqCtx.Tag = tagName;

            ThreadMachine.Create(1).RunInMultiTheadsWithoutWaiting(() =>
            {
                try
                {
                    var responce = Scope.GetQuery<GetTagTopArtistsQuery>().Execute(ReqCtx.As<GetTagTopArtistsRequest>())
                        .LightColorResult(btTagGetTopArtists);
                    if (!responce.IsSuccessful)
                    {
                        _log.Info($"{responce.Message} {responce.ServiceAnswer}");
                    }

                    _log.Info(
                        $"Успешно выгружена информация по тэгу / жанру {tagName}: {Environment.NewLine}{Environment.NewLine}{responce.ServiceAnswer}{Environment.NewLine}.");

                    var tag = responce.DataContainer.TopArtists;
                    var tagsStr = string.Join(Environment.NewLine, tag.Artist.Select(x => $"{x.Name}"));

                    _log.Info($"Artist tags Last.fm: {tagsStr}.");
                }
                catch (Exception ex)
                {
                    _log.Error(ex, ex.Message);
                    Thread.Sleep(LightErrorDelay);
                    btTagGetTopArtists.LightErrorColorResult();
                }
                finally
                {
                    EnableButtonsSafe();
                }
            });
        }

        private void btTagGetTopTracks_Click(object sender, EventArgs e)
        {
            string tagName;

            using (var form = new TagGetTopTracksReqForm(_programSettings))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    tagName = form.Tag;
                    if (string.IsNullOrEmpty(tagName))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указан tag / genre!" + Environment.NewLine);
                        btTagGetTopTracks.LightWarningColorResult();
                        return;
                    }
                }
                else
                {
                    return;
                }
            }

            DisableButtons(sender);
            ReqCtx.Tag = tagName;

            ThreadMachine.Create(1).RunInMultiTheadsWithoutWaiting(() =>
            {
                try
                {
                    var responce = Scope.GetQuery<GetTagTopTracksQuery>().Execute(ReqCtx.As<GetTagTopTracksRequest>())
                        .LightColorResult(btTagGetTopTracks);
                    if (!responce.IsSuccessful)
                    {
                        _log.Info($"{responce.Message} {responce.ServiceAnswer}");
                    }

                    _log.Info(
                        $"Успешно выгружена информация по тэгу / жанру {tagName}: {Environment.NewLine}{Environment.NewLine}{responce.ServiceAnswer}{Environment.NewLine}.");

                    var tag = responce.DataContainer.Tracks;

                    var tagsStr = string.Join(Environment.NewLine, tag.Track.Select(x => $"{x.Name}: Artist - {x.Artist.Name}, Duration - {x.Duration} "));

                    _log.Info($"Album tags Last.fm: {tagsStr}.");
                }
                catch (Exception ex)
                {
                    _log.Error(ex, ex.Message);
                    Thread.Sleep(LightErrorDelay);
                    btTagGetTopTracks.LightErrorColorResult();
                }
                finally
                {
                    EnableButtonsSafe();
                }
            });
        }

        private void btTagGetTopTags_Click(object sender, EventArgs e)
        {
            string tagName;

            using (var form = new TagGetTopTagsReqForm(_programSettings))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    tagName = form.Tag;
                    if (string.IsNullOrEmpty(tagName))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указан tag / genre!" + Environment.NewLine);
                        btTagGetTopTags.LightWarningColorResult();
                        return;
                    }
                }
                else
                {
                    return;
                }
            }

            DisableButtons(sender);
            ReqCtx.Tag = tagName;

            ThreadMachine.Create(1).RunInMultiTheadsWithoutWaiting(() =>
            {
                try
                {
                    var responce = Scope.GetQuery<GetTagTopTagsQuery>().Execute(ReqCtx.As<GetTagTopTagsRequest>())
                        .LightColorResult(btTagGetTopTags);
                    if (!responce.IsSuccessful)
                    {
                        _log.Info($"{responce.Message} {responce.ServiceAnswer}");
                    }

                    _log.Info(
                        $"Успешно выгружена информация по тэгу / жанру {tagName}: {Environment.NewLine}{Environment.NewLine}{responce.ServiceAnswer}{Environment.NewLine}.");

                    var tag = responce.DataContainer.TopTags;

                    var tagsStr = string.Join(Environment.NewLine, tag.Tag.Select(x => $"{x.Name}: {x.Count}, {x.Reach} " ));

                    _log.Info($"Теги Last.fm: {tagsStr}.");
                }
                catch (Exception ex)
                {
                    _log.Error(ex, ex.Message);
                    Thread.Sleep(LightErrorDelay);
                    btTagGetTopTags.LightErrorColorResult();
                }
                finally
                {
                    EnableButtonsSafe();
                }
            });
        }

        private void btTagGetWeeklyChartList_Click(object sender, EventArgs e)
        {
            string tagName;

            using (var form = new TagGetWeeklyChartListReqForm(_programSettings))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    tagName = form.Tag;
                    if (string.IsNullOrEmpty(tagName))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указан tag / genre!" + Environment.NewLine);
                        btTagGetWeeklyChartList.LightWarningColorResult();
                        return;
                    }
                }
                else
                {
                    return;
                }
            }

            DisableButtons(sender);
            ReqCtx.Tag = tagName;

            ThreadMachine.Create(1).RunInMultiTheadsWithoutWaiting(() =>
            {
                try
                {
                    var responce = Scope.GetQuery<GetTagWeeklyChartListQuery>().Execute(ReqCtx.As<GetTagWeeklyChartListRequest>())
                        .LightColorResult(btTagGetWeeklyChartList);
                    if (!responce.IsSuccessful)
                    {
                        _log.Info($"{responce.Message} {responce.ServiceAnswer}");
                    }

                    _log.Info(
                        $"Успешно выгружена информация по тэгу / жанру {tagName}: {Environment.NewLine}{Environment.NewLine}{responce.ServiceAnswer}{Environment.NewLine}.");

                    var tag = responce.DataContainer.WeeklyChartList;

                    var tagsStr = string.Join(Environment.NewLine, tag.Chart.Select(x => x.From + " - " + x.To).ToList());
                    _log.Info($"Weekly chart: {tagsStr}.");
                }
                catch (Exception ex)
                {
                    _log.Error(ex, ex.Message);
                    Thread.Sleep(LightErrorDelay);
                    btTagGetWeeklyChartList.LightErrorColorResult();
                }
                finally
                {
                    EnableButtonsSafe();
                }
            });
        }
    }
}