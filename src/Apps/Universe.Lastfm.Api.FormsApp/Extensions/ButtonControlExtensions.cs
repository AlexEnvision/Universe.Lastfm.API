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
using System.ComponentModel;
using System.Windows.Forms;

namespace Universe.Lastfm.Api.FormsApp.Extensions
{
    public static class ButtonControlExtensions
    {
        /// <summary>
        ///     Включение кнопок на панели, содержащей кнопки
        /// </summary>
        public static void EnableButtons(this Control sender)
        {
            if (sender == null)
                return;

            foreach (var control in sender.Controls)
            {
                if (control is Button button)
                {
                    button.Enabled = true;
                }
                else if (control is Control other)
                {
                    EnableButtons(other);
                }
            }
        }

        /// <summary>
        ///     Включение кнопок на панели, содержащей кнопки. Потокобезопасное
        /// </summary>
        public static void EnableButtonsSafe(this Control sender)
        {
            try
            {
                sender.Invoke(
                    (MethodInvoker)delegate
                    {
                        try
                        {
                            EnableButtons(sender);
                        }
                        catch
                        {
                            // ignored
                        }
                    });
            }
            catch (InvalidOperationException)
            {
            }
            catch (InvalidAsynchronousStateException)
            {
                //ignored
            }
        }

        /// <summary>
        ///     Выключение кнопок на панели, содержащей кнопки.
        /// </summary>
        /// <param name="sender"></param>
        public static void DisableButtons(this Control sender)
        {
            if (sender == null)
                return;

            foreach (var control in sender.Controls)
            {
                if (control is Button button)
                {
                    button.Enabled = false;
                }
                else if (control is Control other)
                {
                    DisableButtons(other);
                }
            }
        }

        /// <summary>
        ///     Выключение кнопок на панели, содержащей кнопки. Потокобезопасное
        /// </summary>
        public static void DisableButtonsSafe(this Control sender)
        {
            try
            {
                sender.Invoke(
                    (MethodInvoker)delegate
                    {
                        try
                        {
                            DisableButtons(sender);
                        }
                        catch
                        {
                            // ignored
                        }
                    });
            }
            catch (InvalidOperationException)
            {
            }
            catch (InvalidAsynchronousStateException)
            {
                //ignored
            }
        }
        public static void MassSetControlProperty(this Control sender, Action<Control> setPropFunc)
        {
            if (sender == null)
                return;

            setPropFunc.Invoke(sender);

            foreach (var control in sender.Controls)
            {
                if (control is Control ctrl)
                {
                    setPropFunc.Invoke(ctrl);

                    ctrl.MassSetControlProperty(setPropFunc);
                }
            }
        }
    }

    public static class GraphicExtensions
    {
        //public static void DrawCenteredString(Graphics g, String text, Rectangle rect, Font font)
        //{
        //    // Get the FontMetrics
        //    FontMetrics metrics = g.GetFontMetrics(font);
        //    // Determine the X coordinate for the text
        //    int x = rect.x + (rect.width - metrics.stringWidth(text)) / 2;
        //    // Determine the Y coordinate for the text (note we add the ascent, as in java 2d 0 is top of the screen)
        //    int y = rect.y + ((rect.height - metrics.getHeight()) / 2) + metrics.getAscent();
        //    // Set the font
        //    g.setFont(font);
        //    // Draw the String
        //    g.drawString(text, x, y);
        //}
    }
}