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
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Universe.Algorithm.MultiThreading;
using Universe.Helpers.Extensions;
using Universe.Lastfm.Api.Dal.Command.Performers;
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
                        btArtistGetInfo.LightWarningColorResult();
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
                        return;
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
                        btGetArtistTags.LightWarningColorResult();
                        return;
                    }

                    user = form.User;
                    if (string.IsNullOrEmpty(user))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указан user!" + Environment.NewLine);
                        btGetArtistTags.LightWarningColorResult();
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
                        return;
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
                        btArtistSearch.LightWarningColorResult();
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
                        return;
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

        private void btArtistGetSimilar_Click(object sender, EventArgs e)
        {
            string performer;

            using (var form = new ArtistGetSimilarReqForm(_programSettings))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    performer = form.Performer;
                    if (string.IsNullOrEmpty(performer))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указан performer!" + Environment.NewLine);
                        btArtistGetSimilar.LightWarningColorResult();
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
                    var responce = Scope.GetQuery<GetSimilarPerformersQuery>().Execute(ReqCtx.As<GetSimilarPerformersQuery.GetSimilarRequest>())
                        .LightColorResult(btArtistGetSimilar);
                    if (!responce.IsSuccessful)
                    {
                        _log.Info($"{responce.Message} {responce.ServiceAnswer}");
                        return;
                    }

                    _log.Info(
                        $"Успешно выгружена информация по исполнителю {performer}: {Environment.NewLine}{Environment.NewLine}{responce.ServiceAnswer}{Environment.NewLine}.");

                    var data = responce.DataContainer.SimilarArtists;
                    var dataStr = string.Join(", ", data.Artist.Select(x => x.Name));

                    _log.Info($"Search similar artist result by the name {performer} / Результат поиска похожих исполнителей по названию {performer}: {dataStr}.");

                    _log.Info(Environment.NewLine);
                }
                catch (Exception ex)
                {
                    _log.Error(ex, ex.Message);
                    Thread.Sleep(LightErrorDelay);
                    btArtistGetSimilar.LightErrorColorResult();
                }
                finally
                {
                    EnableButtonsSafe();
                }
            });
        }

        private void btArtistGetCorrection_Click(object sender, EventArgs e)
        {
            string performer;

            using (var form = new ArtistGetCorrectionReqForm(_programSettings))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    performer = form.Performer;
                    if (string.IsNullOrEmpty(performer))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указан performer!" + Environment.NewLine);
                        btArtistGetCorrection.LightWarningColorResult();
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
                    var responce = Scope.GetQuery<GetPerformerCorrectionQuery>().Execute(ReqCtx.As<GetPerformerCorrectionQuery.GetPerformerCorrectionRequest>())
                        .LightColorResult(btArtistGetCorrection);
                    if (!responce.IsSuccessful)
                    {
                        _log.Info($"{responce.Message} {responce.ServiceAnswer}");
                        return;
                    }

                    _log.Info(
                        $"Успешно выгружена информация по исполнителю {performer}: {Environment.NewLine}{Environment.NewLine}{responce.ServiceAnswer}{Environment.NewLine}.");

                    var data = responce.DataContainer.Corrections;
                    var dataStr = string.Join(", ", data.Correction.Name);

                    _log.Info($"Search correction artist result by the name {performer} / Результат скорректированного исполнителя по названию {performer}: {dataStr}.");

                    _log.Info(Environment.NewLine);
                }
                catch (Exception ex)
                {
                    _log.Error(ex, ex.Message);
                    Thread.Sleep(LightErrorDelay);
                    btArtistGetCorrection.LightErrorColorResult();
                }
                finally
                {
                    EnableButtonsSafe();
                }
            });
        }

        private void btArtistGetTopAlbums_Click(object sender, EventArgs e)
        {
            string performer;

            using (var form = new ArtistGetTopAlbumsReqForm(_programSettings))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    performer = form.Performer;
                    if (string.IsNullOrEmpty(performer))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указан performer!" + Environment.NewLine);
                        btArtistGetTopAlbums.LightWarningColorResult();
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
                    var responce = Scope.GetQuery<GetPerformerGetTopAlbumsQuery>().Execute(ReqCtx.As<GetPerformerGetTopAlbumsQuery.GetPerformerGetTopAlbumsRequest>())
                        .LightColorResult(btArtistGetTopAlbums);
                    if (!responce.IsSuccessful)
                    {
                        _log.Info($"{responce.Message} {responce.ServiceAnswer}");
                        return;
                    }

                    _log.Info(
                        $"Успешно выгружена информация по исполнителю {performer}: {Environment.NewLine}{Environment.NewLine}{responce.ServiceAnswer}{Environment.NewLine}.");

                    var data = responce.DataContainer.TopAlbums;
                    var dataStr = string.Join(", ", data.Album.Select(x => x.Name));

                    _log.Info($"Search correction artist result by the name {performer} / Результат скорректированного исполнителя по названию {performer}: {dataStr}.");

                    _log.Info(Environment.NewLine);
                }
                catch (Exception ex)
                {
                    _log.Error(ex, ex.Message);
                    Thread.Sleep(LightErrorDelay);
                    btArtistGetTopAlbums.LightErrorColorResult();
                }
                finally
                {
                    EnableButtonsSafe();
                }
            });
        }

        private void btArtistGetTopTags_Click(object sender, EventArgs e)
        {
            string performer;

            using (var form = new ArtistGetTopTagsReqForm(_programSettings))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    performer = form.Performer;
                    if (string.IsNullOrEmpty(performer))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указан performer!" + Environment.NewLine);
                        btArtistGetTopTags.LightWarningColorResult();
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
                    var responce = Scope.GetQuery<GetPerformerGetTopTagsQuery>().Execute(ReqCtx.As<GetPerformerGetTopTagsQuery.GetPerformerGetTopTagsRequest>())
                        .LightColorResult(btArtistGetTopTags);
                    if (!responce.IsSuccessful)
                    {
                        _log.Info($"{responce.Message} {responce.ServiceAnswer}");
                        return;
                    }

                    _log.Info(
                        $"Успешно выгружена информация по исполнителю {performer}: {Environment.NewLine}{Environment.NewLine}{responce.ServiceAnswer}{Environment.NewLine}.");

                    var data = responce.DataContainer.TopTags;
                    var dataStr = string.Join(", ", data.Tag.Select(x => x.Name));

                    _log.Info($"Getting tags by the name {performer} / Результат запроса на получение тегов по названию исполнителя {performer}: {dataStr}.");

                    _log.Info(Environment.NewLine);
                }
                catch (Exception ex)
                {
                    _log.Error(ex, ex.Message);
                    Thread.Sleep(LightErrorDelay);
                    btArtistGetTopTags.LightErrorColorResult();
                }
                finally
                {
                    EnableButtonsSafe();
                }
            });
        }

        private void btArtistGetTopTracks_Click(object sender, EventArgs e)
        {
            string performer;

            using (var form = new ArtistGetTopTracksReqForm(_programSettings))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    performer = form.Performer;
                    if (string.IsNullOrEmpty(performer))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указан performer!" + Environment.NewLine);
                        btArtistGetTopTracks.LightWarningColorResult();
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
                    var responce = Scope.GetQuery<GetPerformerGetTopTracksQuery>().Execute(ReqCtx.As<GetPerformerGetTopTracksQuery.GetPerformerGetTopTracksRequest>())
                        .LightColorResult(btArtistGetTopTracks);
                    if (!responce.IsSuccessful)
                    {
                        _log.Info($"{responce.Message} {responce.ServiceAnswer}");
                        return;
                    }

                    _log.Info(
                        $"Успешно выгружена информация по исполнителю {performer}: {Environment.NewLine}{Environment.NewLine}{responce.ServiceAnswer}{Environment.NewLine}.");

                    var data = responce.DataContainer.TopTracks;
                    var dataStr = string.Join(", ", data.Track.Select(x => x.Name));

                    _log.Info($"Search correction artist result by the name {performer} / Результат скорректированного исполнителя по названию {performer}: {dataStr}.");

                    _log.Info(Environment.NewLine);
                }
                catch (Exception ex)
                {
                    _log.Error(ex, ex.Message);
                    Thread.Sleep(LightErrorDelay);
                    btArtistGetTopTracks.LightErrorColorResult();
                }
                finally
                {
                    EnableButtonsSafe();
                }
            });
        }

        private void btArtistAddTags_Click(object sender, EventArgs e)
        {
            string performer;
            string[] tags;

            using (var form = new ArtistAddTagsReqForm(_programSettings))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    performer = form.Performer;
                    if (string.IsNullOrEmpty(performer))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указан performer!" + Environment.NewLine);
                        btArtistAddTags.LightWarningColorResult();
                        return;
                    }

                    tags = form.TagsArray;
                    if (tags.Length == 0)
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указаны tags!" + Environment.NewLine);
                        btArtistAddTags.LightWarningColorResult();
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
                    var responce = Scope.GetCommand<AddArtistTagsCommand>().Execute(ReqCtx.As<AddArtistTagsRequest>())
                        .LightColorResult(btArtistAddTags);
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
                    btArtistAddTags.LightErrorColorResult();
                }
                finally
                {
                    EnableButtonsSafe();
                }
            });
        }

        private void btArtistRemoveTag_Click(object sender, EventArgs e)
        {
            string performer;
            string tag;

            using (var form = new ArtistDeleteAddTagsReqForm(_programSettings))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    performer = form.Performer;
                    if (string.IsNullOrEmpty(performer))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указан performer!" + Environment.NewLine);
                        btArtistRemoveTag.LightWarningColorResult();
                        return;
                    }

                    tag = form.TagsArray.FirstOrDefault() ?? "";
                    if (tag.IsNullOrEmpty())
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указаны tags!" + Environment.NewLine);
                        btArtistRemoveTag.LightWarningColorResult();
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
                    var responce = Scope.GetCommand<DeleteArtistTagsCommand>().Execute(ReqCtx.As<DeleteArtistTagsRequest>())
                        .LightColorResult(btArtistRemoveTag);
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
                    btArtistRemoveTag.LightErrorColorResult();
                }
                finally
                {
                    EnableButtonsSafe();
                }
            });
        }
    }
}