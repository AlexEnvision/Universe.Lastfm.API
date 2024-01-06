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
                    var responce = Scope.GetQuery<GetUserInfoQuery>().Execute(userName);
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

                    EnableButtonsSafe();
                }
                catch (Exception ex)
                {
                    _log.Error(ex, ex.Message);
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
                    var responce = Scope.GetQuery<GetUserTopArtistsQuery>().Execute(userName);
                    if (!responce.IsSuccessful)
                    {
                        _log.Info($"{responce.Message} {responce.ServiceAnswer}");
                    }

                    _log.Info(
                        $"Успешно выгружена информация по пользователю {userName}: {Environment.NewLine}{Environment.NewLine}{responce.ServiceAnswer}{Environment.NewLine}.");

                    var attribute = responce.DataContainer.TopArtists.Attribute;
                    var artists = responce.DataContainer.TopArtists;
                    var artistsStr = string.Join(", ", artists.Artist.Select(x => x.Name));

                    _log.Info($"List of the top performers of the user {attribute.User} / Список топ-исполнителей пользователя {attribute.User}: {artistsStr}.");

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
                    var responce = Scope.GetQuery<GetUserTopAlbumsQuery>().Execute(userName);
                    if (!responce.IsSuccessful)
                    {
                        _log.Info($"{responce.Message} {responce.ServiceAnswer}");
                    }

                    _log.Info(
                        $"Успешно выгружена информация по пользователю {userName}: {Environment.NewLine}{Environment.NewLine}{responce.ServiceAnswer}{Environment.NewLine}.");

                    var attribute = responce.DataContainer.TopAlbums.Attribute;
                    var albums = responce.DataContainer.TopAlbums;
                    var albumsStr = string.Join(", ", albums.Album.Select(x => x.Name));

                    _log.Info($"List of the top albums of the user {attribute.User} / Список топ альбомов пользователя {attribute.User}: {albumsStr}.");

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
                    var responce = Scope.GetQuery<GetUserTopTagsQuery>().Execute(userName);
                    if (!responce.IsSuccessful)
                    {
                        _log.Info($"{responce.Message} {responce.ServiceAnswer}");
                    }

                    _log.Info(
                        $"Успешно выгружена информация по пользователю {userName}: {Environment.NewLine}{Environment.NewLine}{responce.ServiceAnswer}{Environment.NewLine}.");

                    var attribute = responce.DataContainer.TopTags.Attribute;
                    var tags = responce.DataContainer.TopTags;
                    var tagsStr = string.Join(", ", tags.Tag.Select(x => x.Name));

                    _log.Info($"List of the top tags of the user {attribute.User} / Список топ тэгов/жанров пользователя {attribute.User}: {tagsStr}.");

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
                    var responce = Scope.GetQuery<GetUserTopTracksQuery>().Execute(userName);
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