namespace Universe.Lastfm.Api.FormsApp.Forms.Tracks
{
    /// <summary>
    /// <author>Alex Universe</author>
    /// <author>Alex Envision</author>
    /// </summary>
    partial class TrackUpdateNowPlayingReqForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tbAlbum = new System.Windows.Forms.TextBox();
            label3 = new System.Windows.Forms.Label();
            SuspendLayout();
            // 
            // tbAlbum
            // 
            tbAlbum.Location = new System.Drawing.Point(101, 127);
            tbAlbum.Name = "tbAlbum";
            tbAlbum.Size = new System.Drawing.Size(379, 27);
            tbAlbum.TabIndex = 10;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(16, 130);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(53, 20);
            label3.TabIndex = 11;
            label3.Text = "Album";
            // 
            // TrackUpdateNowPlayingReqForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(512, 273);
            Controls.Add(label3);
            Controls.Add(tbAlbum);
            Name = "TrackUpdateNowPlayingReqForm";
            Controls.SetChildIndex(tbAlbum, 0);
            Controls.SetChildIndex(label3, 0);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox tbAlbum;
        private System.Windows.Forms.Label label3;
    }
}