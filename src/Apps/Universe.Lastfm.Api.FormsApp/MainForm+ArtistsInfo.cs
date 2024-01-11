using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Universe.Algorithm.MultiThreading;
using Universe.Lastfm.Api.Dal.Queries.Performers;
using Universe.Lastfm.Api.FormsApp.Extensions;
using Universe.Lastfm.Api.FormsApp.Forms.Performers;
using Universe.Lastfm.Api.Helpers;
using Universe.Lastfm.Api.Models.Req;

namespace Universe.Lastfm.Api.FormsApp
{
    public partial class MainForm
    {
        private void btArtistGetInfo_Click(object sender, EventArgs e)
        {
            string performer;

            using (var albumReqInfoForm = new ArtistReqInfoForm(_programSettings))
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
            ReqCtx.Performer = performer;

            ThreadMachine.Create(1).RunInMultiTheadsWithoutWaiting(() =>
            {
                try
                {
                    // _adapter.GetArtistInfo(performer);
                    var responce = Scope.GetQuery<GetPerformerInfoQuery>().Execute(ReqCtx.As<GetPerformerInfoRequest>()).LightColorResult(btArtistGetInfo);
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
                }
                catch (Exception ex)
                {
                    _log.Error(ex, ex.Message);
                    Thread.Sleep(LightErrorDelay);
                    btArtistGetInfo.LightErrorColorResult();
                }
                finally
                {
                    EnableButtonsSafe();
                }
            });
        }

        private void btGetArtistTags_Click(object sender, EventArgs e)
        {
            string performer;
            string user;

            using (var form = new ArtistTagsReqForm(_programSettings))
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
            ReqCtx.Performer = performer;
            ReqCtx.User = user;

            ThreadMachine.Create(1).RunInMultiTheadsWithoutWaiting(() =>
            {
                try
                {
                    // _adapter.GetArtistTags(performer, user);
                    var responce = Scope.GetQuery<GetPerformersTagsQuery>().Execute(ReqCtx.As<GetPerformerTagsRequest>())
                        .LightColorResult(btGetArtistTags);
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

                }
                catch (Exception ex)
                {
                    _log.Error(ex, ex.Message);
                    Thread.Sleep(LightErrorDelay);
                    btGetArtistTags.LightErrorColorResult();
                }
                finally
                {
                    EnableButtonsSafe();
                }
            });
        }

        private void btArtistSearch_Click(object sender, EventArgs e)
        {
            string performer;

            using (var form = new ArtistSearchReqForm(_programSettings))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    performer = form.Performer;
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
            ReqCtx.Performer = performer;


            ThreadMachine.Create(1).RunInMultiTheadsWithoutWaiting(() =>
            {
                try
                {
                    var responce = Scope.GetQuery<SearchPerformersQuery>().Execute(ReqCtx.As<SearchPerformersQuery.GetArtistSearchRequest>())
                        .LightColorResult(btArtistSearch);
                    if (!responce.IsSuccessful)
                    {
                        _log.Info($"{responce.Message} {responce.ServiceAnswer}");
                    }

                    _log.Info(
                        $"Успешно выгружена информация по исполнителю {performer}: {Environment.NewLine}{Environment.NewLine}{responce.ServiceAnswer}{Environment.NewLine}.");

                    var data = responce.DataContainer.Results;
                    var dataStr = string.Join(", ", data.ArtistMatches.Artist.Select(x => x.Name));

                    _log.Info($"Search artist result by the name {performer} / Результат поиска исполнителей по названию {performer}: {dataStr}.");

                    _log.Info(Environment.NewLine);
                }
                catch (Exception ex)
                {
                    _log.Error(ex, ex.Message);
                    Thread.Sleep(LightErrorDelay);
                    btArtistSearch.LightErrorColorResult();
                }
                finally
                {
                    EnableButtonsSafe();
                }
            });
        }
    }
}