using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Universe.Lastfm.Api.FormsApp.Controls
{
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
