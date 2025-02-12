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
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Universe.Lastfm.Api.Models.Base;
using Universe.Windows.Forms.Controls;

namespace Universe.Lastfm.Api.FormsApp.Extensions
{
    /// <summary>
    /// <author>Alex Universe</author>
    /// <author>Alex Envision</author>
    /// </summary>
    public static class BaseResponceExtensions
    {
        public static BaseResponce LightColorResult(this BaseResponce responce, Control control, int delayAfter = 0)
        {
            if (responce.IsSuccessful)
            {
                control.SafeCall(() => control.BackColor = Color.DarkGreen);
                control.SafeCall(() => control.ForeColor = Color.Aqua);
            }
            else
            {
                control.SafeCall(() => control.BackColor = Color.DarkRed);
                control.SafeCall(() => control.ForeColor = Color.Aqua);
            }

            if (delayAfter > 0)
                Thread.Sleep(delayAfter);

            return responce;
        }

        public static T LightColorResult<T>(this T responce, Control control, int delayAfter = 0) where T: BaseResponce
        {
            if (responce.IsSuccessful)
            {
                control.SafeCall(() => control.BackColor = Color.DarkGreen);
                control.SafeCall(() => control.ForeColor = Color.Aqua);
            }
            else
            {
                control.SafeCall(() => control.BackColor = Color.DarkRed);
                control.SafeCall(() => control.ForeColor = Color.Aqua);
            }

            if (delayAfter > 0) 
                Thread.Sleep(delayAfter);

            return responce;
        }


        public static T ReportResult<T>(this T responce, TextBox tbLog, int delayAfter = 0) where T : BaseResponce
        {
            if (!responce.IsSuccessful)
            {
                var currentDate = DateTime.Now;
                var message = $"[{currentDate}] {responce.Message}{Environment.NewLine}";
                tbLog.SafeCall(() => tbLog.AppendText(message));
            }

            if (delayAfter > 0)
                Thread.Sleep(delayAfter);

            return responce;
        }

        public static void LightErrorColorResult(this Control control)
        {
            control.SafeCall(() => control.BackColor = Color.DarkRed);
            control.SafeCall(() => control.ForeColor = Color.Aqua);
        }

        public static void LightWarningColorResult(this Control control)
        {
            control.SafeCall(() => control.BackColor = Color.Yellow);
            control.SafeCall(() => control.ForeColor = Color.Aqua);
        }
    }
}