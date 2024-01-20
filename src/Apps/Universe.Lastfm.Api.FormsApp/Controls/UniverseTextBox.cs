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

namespace Universe.Lastfm.Api.FormsApp.Controls
{
    /// <summary>
    /// <author>Alex Universe</author>
    /// <author>Alex Envision</author>
    /// </summary>
    public class UniverseTextBox : TextBox
    {
        /// <summary>
        ///  The default BackColor of a generic top-level Control.  Subclasses may have
        ///  different defaults.
        /// </summary>
        public new static Color DefaultBackColor => Color.FromArgb(1, 7, 51);

        /// <summary>
        ///  The default BackColor of a generic top-level Control.  Subclasses may have
        ///  different defaults.
        /// </summary>
        public new static Color DefaultForeColor => Color.Cyan;

        public UniverseTextBox() : base()
        {
            SetStyle(ControlStyles.UserPaint, true);
            Multiline = true;
            Width = 130;
            Height = 119;


            BackColor = Color.FromArgb(1, 7, 51);
            ForeColor = Color.Cyan;
        }

        public sealed override Color ForeColor
        {
            get { return base.ForeColor; }
            set { base.ForeColor = value; }
        }

        public sealed override Color BackColor
        {
            get { return base.BackColor; }
            set { base.BackColor = value; }
        }

        public override sealed bool Multiline
        {
            get { return base.Multiline; }
            set { base.Multiline = value; }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            var buttonPath = new System.Drawing.Drawing2D.GraphicsPath();
            var newRectangle = ClientRectangle;

            //newRectangle.Inflate(-10, -10);
            //e.Graphics.DrawEllipse(pens, newRectangle);
            //newRectangle.Inflate(1, 1);
            //buttonPath.AddEllipse(newRectangle);
            //Region = new System.Drawing.Region(buttonPath);

            //newRectangle.Inflate(-5, -5);
            //e.Graphics.DrawEllipse(pens, newRectangle);
            //newRectangle.Inflate(1, 1);
            buttonPath.AddRectangle(newRectangle);
            Region = new System.Drawing.Region(buttonPath);

            base.OnPaintBackground(e);
        }
    }
}
