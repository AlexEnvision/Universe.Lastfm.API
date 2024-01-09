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
using System.Windows.Forms;
using Universe.Lastfm.Api.FormsApp.Extensions;

namespace Universe.Lastfm.Api.FormsApp.Themes
{
    public class SpaceThemeStyle
    {
        public static SpaceThemeStyle Set => new SpaceThemeStyle();

        public void Apply(Control control)
        {
            control.MassSetControlProperty(ctrl =>
            {
                //ctrl.BackgroundImage = Resources.BackTexture;
                ctrl.BackColor = Color.FromArgb(1, 7, 51);
                ctrl.ForeColor = Color.Cyan;

                if (ctrl is GroupBox gbCtrl)
                {
                    gbCtrl.Paint += RePaintBorderlessGroupBox;
                }

                if (ctrl is Button btCtrl)
                {
                    btCtrl.Paint += RePaintButton;
                    btCtrl.MouseHover += BtCtrl_MouseHover;
                    btCtrl.MouseLeave += BtCtrl_MouseLeave;
                }

                if (ctrl is TextBox tbCtrl)
                {
                    tbCtrl.Paint += RePaintTextBox;
                }
            });
        }

        private void BtCtrl_MouseHover(object sender, EventArgs e)
        {
            Button box = (Button)sender;
            box.BackColor = Color.Blue;
        }

        private void BtCtrl_MouseLeave(object sender, EventArgs e)
        {
            Button box = (Button)sender;
            box.BackColor = Color.FromArgb(1, 7, 51);
        }

        private void RePaintBorderlessGroupBox(object sender, PaintEventArgs p)
        {
            GroupBox box = (GroupBox)sender;
            p.Graphics.Clear(box.BackColor);  //Color.FromArgb(1, 7, 51)

            if (box.Name.Contains("gbBorders"))
            {
                var color = Brushes.Cyan;
                p.Graphics.DrawRectangle(new Pen(color, 2), new Rectangle(5, 5, box.Width - 10, box.Height - 10));
            }
            else
            {
                var color = Brushes.Cyan;
                p.Graphics.DrawRectangle(new Pen(color, 2), new Rectangle(0, 0, box.Width, box.Height));
            }

            p.Graphics.DrawString(box.Text, box.Font, Brushes.Cyan, 2, 2);
        }

        private void RePaintButton(object sender, PaintEventArgs p)
        {
            Button box = (Button)sender;

            if (box.BackColor == Color.DarkGreen)
            {
                p.Graphics.Clear(Color.DarkGreen);
            }
            else if (box.BackColor == Color.DarkRed)
            {
                p.Graphics.Clear(Color.DarkRed);
            }
            else
            {
                p.Graphics.Clear(box.BackColor);  //Color.FromArgb(1, 7, 51)
            }

            var text = box.Enabled ? Brushes.Cyan : Brushes.DarkCyan;

            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;
            p.Graphics.DrawString(box.Text, box.Font, text, box.Width / 2f, box.Height / 2f, sf);

            p.Graphics.DrawRectangle(new Pen(text, 2), new Rectangle(0, 0, box.Width, box.Height));

            if (box.Capture)
            {
                p.Graphics.DrawRectangle(new Pen(Color.Blue, 5), new Rectangle(0, 0, box.Width, box.Height));
            }
        }

        private void RePaintTextBox(object sender, PaintEventArgs p)
        {
            TextBox box = (TextBox)sender;
            p.Graphics.Clear(box.BackColor);   //Color.FromArgb(1, 7, 51)

            p.Graphics.DrawString(box.Text, box.Font, Brushes.Cyan, 10, 10);

            var text = box.Enabled ? Brushes.Cyan : Brushes.DarkCyan;
            p.Graphics.DrawRectangle(new Pen(text, 5), new Rectangle(0, 0, box.Width, box.Height));
        }
    }
}