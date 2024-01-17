using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Universe.Algorithm.MultiThreading;
using Universe.Helpers.Extensions;
using Universe.Lastfm.Api.Dal.Command.Performers;
using Universe.Lastfm.Api.Dal.Command.Tracks;
using Universe.Lastfm.Api.Dal.Queries.Track;
using Universe.Lastfm.Api.FormsApp.Extensions;
using Universe.Lastfm.Api.FormsApp.Forms.Performers;
using Universe.Lastfm.Api.FormsApp.Forms.Tracks;
using Universe.Lastfm.Api.Helpers;
using Universe.Lastfm.Api.Models.Req;

namespace Universe.Lastfm.Api.FormsApp
{
    public partial class MainForm
    {
        private void btTrackGetInfo_Click(object sender, EventArgs e)
        {
            string performer;
            string trackName;

            using (var form = new TrackInfoReqForm(_programSettings))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    performer = form.Performer;
                    if (string.IsNullOrEmpty(performer))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указан performer!" + Environment.NewLine);
                        btTrackGetInfo.LightWarningColorResult();
                        return;
                    }

                    trackName = form.Track;
                    if (string.IsNullOrEmpty(trackName))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указан track!" + Environment.NewLine);
                        btTrackGetInfo.LightWarningColorResult();
                        return;
                    }
                }
                else
                {
                    return;
                }
            }

            DisableButtons(sender);
            ReqCtx.Track = trackName;
            ReqCtx.Performer = performer;

            ThreadMachine.Create(1).RunInMultiTheadsWithoutWaiting(() =>
            {
                try
                {
                    var responce = Scope.GetQuery<GetTrackInfoQuery>().Execute(ReqCtx.As<GetTrackInfoRequest>()).LightColorResult(btTrackGetInfo);
                    if (!responce.IsSuccessful)
                    {
                        _log.Info($"{responce.Message} {responce.ServiceAnswer}");
                        return;
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
                }
                catch (Exception ex)
                {
                    _log.Error(ex, ex.Message);
                    Thread.Sleep(LightErrorDelay);
                    btTrackGetInfo.LightErrorColorResult();
                }
                finally
                {
                    EnableButtonsSafe();
                }
            });
        }

        private void btTrackGetTags_Click(object sender, EventArgs e)
        {
            string performer;
            string trackName;
            string user;

            using (var form = new TrackTagsReqForm(_programSettings))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    performer = form.Performer;
                    if (string.IsNullOrEmpty(performer))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указан performer!" + Environment.NewLine);
                        btTrackGetTags.LightWarningColorResult();
                        return;
                    }

                    trackName = form.Track;
                    if (string.IsNullOrEmpty(trackName))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указан track!" + Environment.NewLine);
                        btTrackGetTags.LightWarningColorResult();
                        return;
                    }

                    user = form.User;
                    if (string.IsNullOrEmpty(user))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указан user!" + Environment.NewLine);
                        btTrackGetTags.LightWarningColorResult();
                        return;
                    }
                }
                else
                {
                    return;
                }
            }

            DisableButtons(sender);
            ReqCtx.Track = trackName;
            ReqCtx.Performer = performer;
            ReqCtx.User = user;

            ThreadMachine.Create(1).RunInMultiTheadsWithoutWaiting(() =>
            {
                try
                {
                    var responce = Scope.GetQuery<GetTrackTagQuery>().Execute(ReqCtx.As<GetTrackTagRequest>()).LightColorResult(btTrackGetTags);
                    if (!responce.IsSuccessful)
                    {
                        _log.Info($"{responce.Message} {responce.ServiceAnswer}");
                        return;
                    }

                    _log.Info(
                        $"Успешно выгружена информация по альбому {trackName} исполнителя {performer}: {Environment.NewLine}{Environment.NewLine}{responce.ServiceAnswer}{Environment.NewLine}.");

                    var tags = responce.DataContainer.Tags;
                    var metalGenres = tags.Tag.Where(x => x.Name.ToLower().Contains("metal")).ToList();

                    var tagsStr = string.Join(", ", tags.Tag.Select(x => x.Name));
                    var metalGenresStr = string.Join(", ", metalGenres.Select(x => x.Name));
                    _log.Info($"Теги трэка на Last.fm: {tagsStr}.");
                    _log.Info($"Метал жанры в трэке: {metalGenresStr}.");

                    _log.Info(Environment.NewLine);
                }
                catch (Exception ex)
                {
                    _log.Error(ex, ex.Message);
                    Thread.Sleep(LightErrorDelay);
                    btTrackGetTags.LightErrorColorResult();
                }
                finally
                {
                    EnableButtonsSafe();
                }
            });
        }

        private void btTrackSearch_Click(object sender, EventArgs e)
        {
            string performer;
            string trackName;

            using (var form = new TrackSearchReqForm(_programSettings))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    performer = form.Performer;
                    if (string.IsNullOrEmpty(performer))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указан performer!" + Environment.NewLine);
                        btTrackSearch.LightWarningColorResult();
                        return;
                    }

                    trackName = form.Track;
                    if (string.IsNullOrEmpty(performer))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указан track!" + Environment.NewLine);
                        btTrackSearch.LightWarningColorResult();
                        return;
                    }
                }
                else
                {
                    return;
                }
            }

            DisableButtons(sender);
            ReqCtx.Performer = performer;
            ReqCtx.Track = trackName;

            ThreadMachine.Create(1).RunInMultiTheadsWithoutWaiting(() =>
            {
                try
                {
                    var responce = Scope.GetQuery<SearchTrackQuery>().Execute(ReqCtx.As<GetTrackSearchRequest>())
                        .LightColorResult(btTrackSearch);
                    if (!responce.IsSuccessful)
                    {
                        _log.Info($"{responce.Message} {responce.ServiceAnswer}");
                        return;
                    }

                    _log.Info(
                        $"Успешно выгружена информация по исполнителю {performer}: {Environment.NewLine}{Environment.NewLine}{responce.ServiceAnswer}{Environment.NewLine}.");

                    var data = responce.DataContainer.Results;
                    var dataStr = string.Join(", ", data.TrackMatches.Track.Select(x => x.Name));

                    _log.Info($"Search artist result by the name {performer} / Результат поиска исполнителей по названию {performer}: {dataStr}.");

                    _log.Info(Environment.NewLine);
                }
                catch (Exception ex)
                {
                    _log.Error(ex, ex.Message);
                    Thread.Sleep(LightErrorDelay);
                    btTrackSearch.LightErrorColorResult();
                }
                finally
                {
                    EnableButtonsSafe();
                }
            });
        }

        private void btTrackGetSimilar_Click(object sender, EventArgs e)
        {
            string trackName;

            using (var form = new TrackGetSimilarReqForm(_programSettings))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    trackName = form.Track;
                    if (string.IsNullOrEmpty(trackName))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указан track!" + Environment.NewLine);
                        btTrackGetSimilar.LightWarningColorResult();
                        return;
                    }
                }
                else
                {
                    return;
                }
            }

            DisableButtons(sender);
            ReqCtx.Tag = trackName;

            ThreadMachine.Create(1).RunInMultiTheadsWithoutWaiting(() =>
            {
                try
                {
                    var responce = Scope.GetQuery<GetTrackSimilarQuery>().Execute(ReqCtx.As<GetTrackSimilarRequest>())
                        .LightColorResult(btTrackGetSimilar);
                    if (!responce.IsSuccessful)
                    {
                        _log.Info($"{responce.Message} {responce.ServiceAnswer}");
                        return;
                    }

                    _log.Info(
                        $"Успешно выгружена информация по тэгу / жанру {trackName}: {Environment.NewLine}{Environment.NewLine}{responce.ServiceAnswer}{Environment.NewLine}.");

                    var data = responce.DataContainer.Similartracks;
                    var dataStr = string.Join(", ", data.Track.Select(x => x.Name));

                    _log.Info($"Get similar track result by the name {trackName} / Результат похожего трэка по названию {trackName}: {dataStr}.");

                    _log.Info(Environment.NewLine);
                }
                catch (Exception ex)
                {
                    _log.Error(ex, ex.Message);
                    Thread.Sleep(LightErrorDelay);
                    btTrackGetSimilar.LightErrorColorResult();
                }
                finally
                {
                    EnableButtonsSafe();
                }
            });
        }

        private void brTrackGetCorrection_Click(object sender, EventArgs e)
        {
            string performer;
            string trackName;

            using (var form = new TrackGetCorrectionReqForm(_programSettings))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    performer = form.Performer;
                    if (string.IsNullOrEmpty(performer))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указан performer!" + Environment.NewLine);
                        btTrackGetCorrection.LightWarningColorResult();
                        return;
                    }

                    trackName = form.Track;
                    if (string.IsNullOrEmpty(performer))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указан track!" + Environment.NewLine);
                        btTrackSearch.LightWarningColorResult();
                        return;
                    }
                }
                else
                {
                    return;
                }
            }

            DisableButtons(sender);
            ReqCtx.Performer = performer;
            ReqCtx.Track = trackName;

            ThreadMachine.Create(1).RunInMultiTheadsWithoutWaiting(() =>
            {
                try
                {
                    var responce = Scope.GetQuery<GetTrackGetCorrectionQuery>().Execute(ReqCtx.As<GetTrackCorrectionRequest>())
                        .LightColorResult(btTrackGetCorrection);
                    if (!responce.IsSuccessful)
                    {
                        _log.Info($"{responce.Message} {responce.ServiceAnswer}");
                        return;
                    }

                    _log.Info(
                        $"Успешно выгружена информация по исполнителю {performer}: {Environment.NewLine}{Environment.NewLine}{responce.ServiceAnswer}{Environment.NewLine}.");

                    var data = responce.DataContainer.Corrections;
                    var dataStr = string.Join(", ", data.Correction.Track.Name);

                    _log.Info($"Search artist result by the name {performer} / Результат поиска исполнителей по названию {performer}: {dataStr}.");

                    _log.Info(Environment.NewLine);
                }
                catch (Exception ex)
                {
                    _log.Error(ex, ex.Message);
                    Thread.Sleep(LightErrorDelay);
                    btTrackGetCorrection.LightErrorColorResult();
                }
                finally
                {
                    EnableButtonsSafe();
                }
            });
        }

        private void btTrackGetTopTags_Click(object sender, EventArgs e)
        {
            string performer;
            string trackName;

            using (var form = new TrackGetTopTagsReqForm(_programSettings))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    performer = form.Performer;
                    if (string.IsNullOrEmpty(performer))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указан performer!" + Environment.NewLine);
                        btTrackGetTopTags.LightWarningColorResult();
                        return;
                    }

                    trackName = form.Track;
                    if (string.IsNullOrEmpty(performer))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указан track!" + Environment.NewLine);
                        btTrackGetTopTags.LightWarningColorResult();
                        return;
                    }
                }
                else
                {
                    return;
                }
            }

            DisableButtons(sender);
            ReqCtx.Performer = performer;
            ReqCtx.Track = trackName;

            ThreadMachine.Create(1).RunInMultiTheadsWithoutWaiting(() =>
            {
                try
                {
                    var responce = Scope.GetQuery<GetTrackTopTagsQuery>().Execute(ReqCtx.As<GetTrackTopTagsRequest>())
                        .LightColorResult(btTrackGetTopTags);
                    if (!responce.IsSuccessful)
                    {
                        _log.Info($"{responce.Message} {responce.ServiceAnswer}");
                        return;
                    }

                    _log.Info(
                        $"Успешно выгружена информация по исполнителю {performer}: {Environment.NewLine}{Environment.NewLine}{responce.ServiceAnswer}{Environment.NewLine}.");

                    var data = responce.DataContainer.TopTags;
                    var dataStr = string.Join(", ", data.TopTags.Track.Name);

                    _log.Info($"Search artist result by the name {performer} / Результат поиска исполнителей по названию {performer}: {dataStr}.");

                    _log.Info(Environment.NewLine);
                }
                catch (Exception ex)
                {
                    _log.Error(ex, ex.Message);
                    Thread.Sleep(LightErrorDelay);
                    btTrackGetTopTags.LightErrorColorResult();
                }
                finally
                {
                    EnableButtonsSafe();
                }
            });
        }

        private void btTrackAddTags_Click(object sender, EventArgs e)
        {
            string performer;
            string track;
            string[] tags;

            using (var form = new TrackAddTagsReqForm(_programSettings))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    performer = form.Performer;
                    if (string.IsNullOrEmpty(performer))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указан performer!" + Environment.NewLine);
                        btTrackAddTags.LightWarningColorResult();
                        return;
                    }

                    tags = form.TagsArray;
                    if (tags.Length == 0)
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указаны tags!" + Environment.NewLine);
                        btTrackAddTags.LightWarningColorResult();
                        return;
                    }
                }
                else
                {
                    return;
                }
            }

            DisableButtons(sender);

            ReqCtx.Performer = performer;
            ReqCtx.Tags = tags;

            ThreadMachine.Create(1).RunInMultiTheadsWithoutWaiting(() =>
            {
                try
                {
                    var responce = Scope.GetCommand<AddTrackTagsCommand>().Execute(ReqCtx.As<AddTrackTagsRequest>())
                        .LightColorResult(btTrackAddTags);
                    if (!responce.IsSuccessful)
                    {
                        _log.Info($"{responce.Message} {responce.ServiceAnswer}");
                        return;
                    }

                    _log.Info($"Update {performer} result: {string.Join("", responce.Responces.Select(x => x.ServiceAnswer).ToList())}{Environment.NewLine}");
                }
                catch (Exception ex)
                {
                    _log.Error(ex, ex.Message);
                    Thread.Sleep(LightErrorDelay);
                    btTrackAddTags.LightErrorColorResult();
                }
                finally
                {
                    EnableButtonsSafe();
                }
            });
        }

        private void btTrackRemoveTag_Click(object sender, EventArgs e)
        {
            string performer;
            string tag;

            using (var form = new TrackDeleteTagsReqForm(_programSettings))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    performer = form.Performer;
                    if (string.IsNullOrEmpty(performer))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указан performer!" + Environment.NewLine);
                        btTrackRemoveTag.LightWarningColorResult();
                        return;
                    }

                    tag = form.TagsArray.FirstOrDefault() ?? "";
                    if (tag.IsNullOrEmpty())
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указаны tags!" + Environment.NewLine);
                        btTrackRemoveTag.LightWarningColorResult();
                        return;
                    }
                }
                else
                {
                    return;
                }
            }

            DisableButtons(sender);

            ReqCtx.Performer = performer;
            ReqCtx.RemTag = tag;

            ThreadMachine.Create(1).RunInMultiTheadsWithoutWaiting(() =>
            {
                try
                {
                    var responce = Scope.GetCommand<DeleteTrackTagsCommand>().Execute(ReqCtx.As<DeleteTrackTagsRequest>())
                        .LightColorResult(btTrackRemoveTag);
                    if (!responce.IsSuccessful)
                    {
                        _log.Info($"{responce.Message} {responce.ServiceAnswer}");
                        return;
                    }

                    _log.Info($"Update {performer} result: {responce.ServiceAnswer}{Environment.NewLine}");
                }
                catch (Exception ex)
                {
                    _log.Error(ex, ex.Message);
                    Thread.Sleep(LightErrorDelay);
                    btTrackRemoveTag.LightErrorColorResult();
                }
                finally
                {
                    EnableButtonsSafe();
                }
            });
        }

        private void btTrackLove_Click(object sender, EventArgs e)
        {
            string performer;
            string trackName;

            using (var form = new TrackLoveReqForm(_programSettings))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    performer = form.Performer;
                    if (string.IsNullOrEmpty(performer))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указан performer!" + Environment.NewLine);
                        btTrackLove.LightWarningColorResult();
                        return;
                    }

                    trackName = form.Track;
                    if (string.IsNullOrEmpty(performer))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указан track!" + Environment.NewLine);
                        btTrackLove.LightWarningColorResult();
                        return;
                    }
                }
                else
                {
                    return;
                }
            }

            DisableButtons(sender);
            ReqCtx.Performer = performer;
            ReqCtx.Track = trackName;

            ThreadMachine.Create(1).RunInMultiTheadsWithoutWaiting(() =>
            {
                try
                {
                    var responce = Scope.GetCommand<UpdateTrackAsLoveCommand>().Execute(ReqCtx.As<UpdateTrackAsLoveRequest>())
                        .LightColorResult(btTrackLove);
                    if (!responce.IsSuccessful)
                    {
                        _log.Info($"{responce.Message} {responce.ServiceAnswer}");
                        return;
                    }

                    _log.Info(
                        $"Успешно внесены изменения по треку {performer} - {trackName}: {Environment.NewLine}{Environment.NewLine}{responce.ServiceAnswer}{Environment.NewLine}.");

                    //var data = responce.DataContainer;
                    //var dataStr = data.Lfm.Status;

                    //_log.Info($"Result of adding tags/genres of artist by the names {tagsStr} / Результат добавления тэгов/жанров альбома по названиям {tagsStr}: {dataStr}.");

                    _log.Info(Environment.NewLine);
                }
                catch (Exception ex)
                {
                    _log.Error(ex, ex.Message);
                    Thread.Sleep(LightErrorDelay);
                    btTrackLove.LightErrorColorResult();
                }
                finally
                {
                    EnableButtonsSafe();
                }
            });
        }

        private void btTrackUnlove_Click(object sender, EventArgs e)
        {

        }

        private void btTrackUpdateNowPlaying_Click(object sender, EventArgs e)
        {

        }
    }
}