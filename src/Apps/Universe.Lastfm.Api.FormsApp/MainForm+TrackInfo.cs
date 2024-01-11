using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Universe.Algorithm.MultiThreading;
using Universe.Lastfm.Api.Dal.Queries.Track;
using Universe.Lastfm.Api.FormsApp.Extensions;
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
                        return;
                    }

                    trackName = form.Track;
                    if (string.IsNullOrEmpty(performer))
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
    }
}