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

using System.Drawing;
using System.Windows.Forms;
using Universe.Lastfm.Api.FormsApp.Extensions.Model;

namespace Universe.Lastfm.Api.FormsApp.Extensions
{
    /// <summary>
    ///     The extensions of state
    /// </summary>
    public static class ResizeFormStateExtensions
    {
        public static void ResizeUp(this ResizeFormState state, Control sender)
        {
            if (sender == null)
                return;

            foreach (var control in sender.Controls)
            {
                if (control is Control ctrl)
                {
                    ctrl.Width = (int)(ctrl.Width * state.WidthKoef);
                    ctrl.Height = (int)(ctrl.Height * state.HeightKoef);

                    ctrl.Location = new Point(
                        (int)(ctrl.Location.X * state.WidthKoef),
                        (int)(ctrl.Location.Y * state.HeightKoef)
                    );

                    state.ResizeUp(ctrl);
                }
            }
        }

        public static void ResizeDown(this ResizeFormState state, Control sender)
        {
            if (sender == null)
                return;

            foreach (var control in sender.Controls)
            {
                if (control is Control ctrl)
                {
                    ctrl.Width = (int)(ctrl.Width * state.WidthKoefReverse);
                    ctrl.Height = (int)(ctrl.Height * state.HeightKoefReverse);

                    ctrl.Location = new Point(
                        (int)(ctrl.Location.X * state.WidthKoefReverse),
                        (int)(ctrl.Location.Y * state.HeightKoefReverse)
                    );

                    state.ResizeDown(ctrl);
                }
            }
        }
    }
}