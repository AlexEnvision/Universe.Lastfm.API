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
using System.Windows.Forms;
using Universe.Algorithm.MultiThreading;
using Universe.Lastfm.Api.Dal.Queries.Users;
using Universe.Lastfm.Api.FormsApp.Extensions;
using Universe.Lastfm.Api.FormsApp.Forms.Users;
using Universe.Lastfm.Api.Helpers;

namespace Universe.Lastfm.Api.FormsApp
{
    public partial class MainForm : Form
    {

        private void btUserGetInfo_Click(object sender, EventArgs e)
        {
            string userName;

            using (var form = new UserInfoReqForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    userName = form.User;
                    if (string.IsNullOrEmpty(userName))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указано userName!" + Environment.NewLine);
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
                    var responce = Scope.GetQuery<GetUserInfoQuery>().Execute(userName).LightColorResult(btUserGetInfo);
                    if (!responce.IsSuccessful)
                    {
                        _log.Info($"{responce.Message} {responce.ServiceAnswer}");
                    }

                    _log.Info(
                        $"Успешно выгружена информация по пользователю {userName}: {Environment.NewLine}{Environment.NewLine}{responce.ServiceAnswer}{Environment.NewLine}.");

                    var user = responce.DataContainer.User;

                    _log.Info($"Playcount / Прослушиваний пользователя: {user.Playcount}.");
                    _log.Info($"Real name of user / Реальное имя пользователя: {user.Realname}.");
                    _log.Info($"Age / Возраст: {user.Age}.");
                    _log.Info($"Gender / Пол: {user.Gender}.");

                    _log.Info($"Registered / Зарегистрирован: {user.Registered.Unixtime.UnixTimeStampToDateTime()}.");

                    _log.Info(Environment.NewLine);
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

        private void btUserGetTopArtists_Click(object sender, EventArgs e)
        {
            string userName;

            using (var form = new UserInfoReqForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    userName = form.User;
                    if (string.IsNullOrEmpty(userName))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указано userName!" + Environment.NewLine);
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
                    var responce = Scope.GetQuery<GetUserTopArtistsQuery>().Execute(userName)
                        .LightColorResult(btUserGetTopArtists);
                    if (!responce.IsSuccessful)
                    {
                        _log.Info($"{responce.Message} {responce.ServiceAnswer}");
                    }

                    _log.Info(
                        $"Успешно выгружена информация по пользователю {userName}: {Environment.NewLine}{Environment.NewLine}{responce.ServiceAnswer}{Environment.NewLine}.");

                    var attribute = responce.DataContainer.TopArtists.Attribute;
                    var data = responce.DataContainer.TopArtists;
                    var dataStr = string.Join(", ", data.Artist.Select(x => x.Name));

                    _log.Info($"List of the top performers of the user {attribute.User} / Список топ-исполнителей пользователя {attribute.User}: {dataStr}.");

                    _log.Info(Environment.NewLine);
                }
                catch (Exception ex)
                {
                    _log.Error(ex, ex.Message);
                    btUserGetTopArtists.LightErrorColorResult();
                }
                finally
                {
                    EnableButtonsSafe();
                }
            });
        }

        private void btUserGetTopAlbums_Click(object sender, EventArgs e)
        {
            string userName;

            using (var form = new UserTopAlbumsReqForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    userName = form.User;
                    if (string.IsNullOrEmpty(userName))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указано userName!" + Environment.NewLine);
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
                    var responce = Scope.GetQuery<GetUserTopAlbumsQuery>().Execute(userName)
                        .LightColorResult(btUserGetTopAlbums);
                    if (!responce.IsSuccessful)
                    {
                        _log.Info($"{responce.Message} {responce.ServiceAnswer}");
                    }

                    _log.Info(
                        $"Успешно выгружена информация по пользователю {userName}: {Environment.NewLine}{Environment.NewLine}{responce.ServiceAnswer}{Environment.NewLine}.");

                    var attribute = responce.DataContainer.TopAlbums.Attribute;
                    var data = responce.DataContainer.TopAlbums;
                    var dataStr = string.Join(", ", data.Album.Select(x => x.Name));

                    _log.Info($"List of the top albums of the user {attribute.User} / Список топ альбомов пользователя {attribute.User}: {dataStr}.");

                    _log.Info(Environment.NewLine);
                }
                catch (Exception ex)
                {
                    _log.Error(ex, ex.Message);
                    btUserGetTopAlbums.LightErrorColorResult();
                }
                finally
                {
                    EnableButtonsSafe();
                }
            });
        }

        private void btUserGetTopTags_Click(object sender, EventArgs e)
        {
            string userName;

            using (var form = new UserTopGenresReqForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    userName = form.User;
                    if (string.IsNullOrEmpty(userName))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указано userName!" + Environment.NewLine);
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
                    var responce = Scope.GetQuery<GetUserTopTagsQuery>().Execute(userName)
                        .LightColorResult(btUserGetTopTags);
                    if (!responce.IsSuccessful)
                    {
                        _log.Info($"{responce.Message} {responce.ServiceAnswer}");
                    }

                    _log.Info(
                        $"Успешно выгружена информация по пользователю {userName}: {Environment.NewLine}{Environment.NewLine}{responce.ServiceAnswer}{Environment.NewLine}.");

                    var attribute = responce.DataContainer.TopTags.Attribute;
                    var data = responce.DataContainer.TopTags;
                    var dataStr = string.Join(", ", data.Tag.Select(x => x.Name));

                    _log.Info($"List of the top tags of the user {attribute.User} / Список топ тэгов/жанров пользователя {attribute.User}: {dataStr}.");

                    _log.Info(Environment.NewLine);
                }
                catch (Exception ex)
                {
                    _log.Error(ex, ex.Message);
                    btUserGetTopTags.LightErrorColorResult();
                }
                finally
                {
                    EnableButtonsSafe();
                }
            });
        }

        private void btUserGetTopTracks_Click(object sender, EventArgs e)
        {
            string userName;

            using (var form = new UserTopTracksReqForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    userName = form.User;
                    if (string.IsNullOrEmpty(userName))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указано userName!" + Environment.NewLine);
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
                    var responce = Scope.GetQuery<GetUserTopTracksQuery>().Execute(userName)
                        .LightColorResult(btUserGetTopTracks);
                    if (!responce.IsSuccessful)
                    {
                        _log.Info($"{responce.Message} {responce.ServiceAnswer}");
                    }

                    _log.Info(
                        $"Успешно выгружена информация по пользователю {userName}: {Environment.NewLine}{Environment.NewLine}{responce.ServiceAnswer}{Environment.NewLine}.");

                    var attribute = responce.DataContainer.TopTracks.Attribute;
                    var tracks = responce.DataContainer.TopTracks;
                    var tagsStr = string.Join(", ", tracks.Track.Select(x => x.Name));

                    _log.Info($"List of the top tracks of the user {attribute.User} / Список топ трэков пользователя {attribute.User}: {tagsStr}.");

                    _log.Info(Environment.NewLine);
                }
                catch (Exception ex)
                {
                    _log.Error(ex, ex.Message);
                    btUserGetTopTracks.LightErrorColorResult();
                }
                finally
                {
                    EnableButtonsSafe();
                }
            });
        }

        private void btUserGetLovedTracks_Click(object sender, EventArgs e)
        {
            string userName;

            using (var form = new UserGetLovedTracksReqForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    userName = form.User;
                    if (string.IsNullOrEmpty(userName))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указано userName!" + Environment.NewLine);
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
                    var responce = Scope.GetQuery<GetUserLovedTracksQuery>().Execute(userName)
                        .LightColorResult(btUserGetLovedTracks);
                    if (!responce.IsSuccessful)
                    {
                        _log.Info($"{responce.Message} {responce.ServiceAnswer}");
                    }

                    _log.Info(
                        $"Успешно выгружена информация по пользователю {userName}: {Environment.NewLine}{Environment.NewLine}{responce.ServiceAnswer}{Environment.NewLine}.");

                    var attribute = responce.DataContainer.LovedTracks.Attribute;
                    var data = responce.DataContainer.LovedTracks;
                    var dataStr = string.Join(", ", data.Track.Select(x => x.Name));

                    _log.Info($"List of the loved tracks of the user {attribute.User} / Список любимых трэков пользователя {attribute.User}: {dataStr}.");

                    _log.Info(Environment.NewLine);
                }
                catch (Exception ex)
                {
                    _log.Error(ex, ex.Message);
                    btUserGetPersonalTags.LightErrorColorResult();
                }
                finally
                {
                    EnableButtonsSafe();
                }
            });
        }

        private void btUserGetPersonalTags_Click(object sender, EventArgs e)
        {
            string userName;
            string tag;
            string tagType;

            using (var form = new UserGetPersonalTagsReqForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    userName = form.User;
                    if (string.IsNullOrEmpty(userName))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указано userName!" + Environment.NewLine);
                        return;
                    }

                    tag = form.Genre;
                    if (string.IsNullOrEmpty(userName))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указан tag!" + Environment.NewLine);
                        return;
                    }

                    tagType = form.GenreType;
                    if (string.IsNullOrEmpty(userName))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указан taggingType!" + Environment.NewLine);
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
                    var responce = Scope.GetQuery<GetPersonalTagsQuery>().Execute(userName, tag, tagType)
                        .LightColorResult(btUserGetPersonalTags);
                    if (!responce.IsSuccessful)
                    {
                        _log.Info($"{responce.Message} {responce.ServiceAnswer}");
                    }

                    _log.Info(
                        $"Успешно выгружена информация по пользователю {userName}: {Environment.NewLine}{Environment.NewLine}{responce.ServiceAnswer}{Environment.NewLine}.");

                    var attribute = responce.DataContainer.Taggings.Attribute;
                    var data = responce.DataContainer.Taggings;
                    var dataStr = string.Join(", ", data.Artists.Select(x => x.Name));

                    _log.Info($"List of the tagged artists of the user {attribute.User} / Список тэгированных исполнителей пользователя {attribute.User}: {dataStr}.");

                    _log.Info(Environment.NewLine);

                }
                catch (Exception ex)
                {
                    _log.Error(ex, ex.Message);
                    btUserGetPersonalTags.LightErrorColorResult();
                }
                finally
                {
                    EnableButtonsSafe();
                }
            });
        }

        private void btUserGetRecentTracks_Click(object sender, EventArgs e)
        {
            string userName;

            using (var form = new UserGetRecentTracksReqForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    userName = form.User;
                    if (string.IsNullOrEmpty(userName))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указано userName!" + Environment.NewLine);
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
                    var responce = Scope.GetQuery<GetUserRecentTracksQuery>().Execute(userName)
                        .LightColorResult(btUserGetRecentTracks);
                    if (!responce.IsSuccessful)
                    {
                        _log.Info($"{responce.Message} {responce.ServiceAnswer}");
                    }

                    _log.Info(
                        $"Успешно выгружена информация по пользователю {userName}: {Environment.NewLine}{Environment.NewLine}{responce.ServiceAnswer}{Environment.NewLine}.");

                    var attribute = responce.DataContainer.RecentTracks.Attribute;
                    var data = responce.DataContainer.RecentTracks;
                    var dataStr = string.Join(", ", data.Track.Select(x => x.Name));

                    _log.Info($"List of the top tracks of the user {attribute.User} / Список топ трэков пользователя {attribute.User}: {dataStr}.");

                    _log.Info(Environment.NewLine);

                }
                catch (Exception ex)
                {
                    _log.Error(ex, ex.Message);
                    btUserGetRecentTracks.LightErrorColorResult();
                }
                finally
                {
                    EnableButtonsSafe();
                }
            });
        }

        private void btUserGetWeeklyAlbumChart_Click(object sender, EventArgs e)
        {
            string userName;

            using (var form = new UserGetWeeklyAlbumChartReqForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    userName = form.User;
                    if (string.IsNullOrEmpty(userName))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указано userName!" + Environment.NewLine);
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
                    var responce = Scope.GetQuery<GetUserWeeklyAlbumChartQuery>().Execute(userName)
                        .LightColorResult(btUserGetWeeklyAlbumChart);
                    if (!responce.IsSuccessful)
                    {
                        _log.Info($"{responce.Message} {responce.ServiceAnswer}");
                    }

                    _log.Info(
                        $"Успешно выгружена информация по пользователю {userName}: {Environment.NewLine}{Environment.NewLine}{responce.ServiceAnswer}{Environment.NewLine}.");

                    var attribute = responce.DataContainer.WeeklyAlbumChart.Attribute;
                    var data = responce.DataContainer.WeeklyAlbumChart;
                    var dataStr = string.Join(", ", data.Album.Select(x => x.Name));

                    _log.Info($"Сhart of weekly albums of the user {attribute.User} / Чарт недельный чарта альбомов пользователя {attribute.User}: {dataStr}.");

                    _log.Info(Environment.NewLine);
                }
                catch (Exception ex)
                {
                    _log.Error(ex, ex.Message);
                    btUserGetWeeklyAlbumChart.LightErrorColorResult();
                }
                finally
                {
                    EnableButtonsSafe();
                }
            });
        }

        private void btUserGetWeeklyArtistChart_Click(object sender, EventArgs e)
        {
            string userName;

            using (var form = new UserGetWeeklyArtistChartReqForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    userName = form.User;
                    if (string.IsNullOrEmpty(userName))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указано userName!" + Environment.NewLine);
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
                    var responce = Scope.GetQuery<GetUserWeeklyArtistChartQuery>().Execute(userName)
                        .LightColorResult(btUserGetWeeklyArtistChart);
                    if (!responce.IsSuccessful)
                    {
                        _log.Info($"{responce.Message} {responce.ServiceAnswer}");
                    }

                    _log.Info(
                        $"Успешно выгружена информация по пользователю {userName}: {Environment.NewLine}{Environment.NewLine}{responce.ServiceAnswer}{Environment.NewLine}.");

                    var attribute = responce.DataContainer.WeeklyArtistChart.Attribute;
                    var data = responce.DataContainer.WeeklyArtistChart;
                    var dataStr = string.Join(", ", data.Artist.Select(x => x.Name));

                    _log.Info($"Сhart of weekly artists/performers of the user {attribute.User} / Чарт недельный чарта артистов/исполнителей пользователя {attribute.User}: {dataStr}.");

                    _log.Info(Environment.NewLine);

                }
                catch (Exception ex)
                {
                    _log.Error(ex, ex.Message);
                    btUserGetWeeklyArtistChart.LightErrorColorResult();
                }
                finally
                {
                    EnableButtonsSafe();
                }
            });
        }

        private void btUserGetWeeklyChartList_Click(object sender, EventArgs e)
        {
            string userName;

            using (var form = new UserGetWeeklyChartListReqForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    userName = form.User;
                    if (string.IsNullOrEmpty(userName))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указано userName!" + Environment.NewLine);
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
                    var responce = Scope.GetQuery<GetUserWeeklyChartListQuery>().Execute(userName)
                        .LightColorResult(btUserGetWeeklyChartList);
                    if (!responce.IsSuccessful)
                    {
                        _log.Info($"{responce.Message} {responce.ServiceAnswer}");
                    }

                    _log.Info(
                        $"Успешно выгружена информация по пользователю {userName}: {Environment.NewLine}{Environment.NewLine}{responce.ServiceAnswer}{Environment.NewLine}.");

                    var attribute = responce.DataContainer.WeeklyChartList.Attribute;
                    var data = responce.DataContainer.WeeklyChartList;
                    var dataStr = string.Join(", ", data.Chart.Select(x => $"From:{x.From} To:{x.To}"));

                    _log.Info(
                        $"Сhart of weekly artists/performers of the user {attribute.User} / Чарт недельный чарта артистов/исполнителей пользователя {attribute.User}: {dataStr}.");

                    _log.Info(Environment.NewLine);

                }
                catch (Exception ex)
                {
                    _log.Error(ex, ex.Message);
                    btUserGetWeeklyChartList.LightErrorColorResult();
                }
                finally
                {
                    EnableButtonsSafe();
                }
            });
        }

        private void btUserGetWeeklyTrackChart_Click(object sender, EventArgs e)
        {
            string userName;

            using (var form = new UserGetWeeklyTrackChartReqForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    userName = form.User;
                    if (string.IsNullOrEmpty(userName))
                    {
                        tbLog.AppendText($"[{DateTime.Now}] Не указано userName!" + Environment.NewLine);
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
                    var responce = Scope.GetQuery<GetUserWeeklyTrackChartQuery>().Execute(userName)
                        .LightColorResult(btUserGetWeeklyTrackChart);
                    if (!responce.IsSuccessful)
                    {
                        _log.Info($"{responce.Message} {responce.ServiceAnswer}");
                    }

                    _log.Info(
                        $"Успешно выгружена информация по пользователю {userName}: {Environment.NewLine}{Environment.NewLine}{responce.ServiceAnswer}{Environment.NewLine}.");

                    var attribute = responce.DataContainer.WeeklyTrackChart.Attribute;
                    var data = responce.DataContainer.WeeklyTrackChart;
                    var dataStr = string.Join(", ", data.Track.Select(x => x.Name));

                    _log.Info($"Сhart of weekly artists/performers of the user {attribute.User} / Чарт недельный чарта артистов/исполнителей пользователя {attribute.User}: {dataStr}.");

                    _log.Info(Environment.NewLine);
                }
                catch (Exception ex)
                {
                    _log.Error(ex, ex.Message);
                    btUserGetWeeklyTrackChart.LightErrorColorResult();
                }
                finally
                {
                    EnableButtonsSafe();
                }
            });
        }
    }
}