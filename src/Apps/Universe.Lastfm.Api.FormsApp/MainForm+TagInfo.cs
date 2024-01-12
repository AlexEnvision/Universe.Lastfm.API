using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Universe.Algorithm.MultiThreading;
using Universe.Lastfm.Api.Dal.Queries.Tags;
using Universe.Lastfm.Api.FormsApp.Extensions;
using Universe.Lastfm.Api.FormsApp.Forms.Genres;
using Universe.Lastfm.Api.Helpers;
using Universe.Lastfm.Api.Models.Req;

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
    }
}