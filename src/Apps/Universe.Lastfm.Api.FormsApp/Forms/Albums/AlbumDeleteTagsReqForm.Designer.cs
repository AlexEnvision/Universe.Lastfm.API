namespace Universe.Lastfm.Api.FormsApp.Forms.Albums
{
    /// <summary>
    /// <author>Alex Universe</author>
    /// <author>Alex Envision</author>
    /// </summary>
    partial class AlbumDeleteTagsReqForm
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
            label3 = new System.Windows.Forms.Label();
            tbRemovingTags = new System.Windows.Forms.TextBox();
            SuspendLayout();
            // 
            // label3
            // 
            label3.Location = new System.Drawing.Point(13, 149);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(80, 65);
            label3.TabIndex = 8;
            label3.Text = "Tags for deletion:";
            // 
            // tbRemovingTags
            // 
            tbRemovingTags.Location = new System.Drawing.Point(101, 146);
            tbRemovingTags.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tbRemovingTags.Multiline = true;
            tbRemovingTags.Name = "tbRemovingTags";
            tbRemovingTags.Size = new System.Drawing.Size(389, 68);
            tbRemovingTags.TabIndex = 7;
            // 
            // AlbumDeleteTagsReqForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(512, 273);
            Controls.Add(label3);
            Controls.Add(tbRemovingTags);
            Name = "AlbumDeleteTagsReqForm";
            Text = "DeleteAddTagsReqForm";
            Controls.SetChildIndex(tbRemovingTags, 0);
            Controls.SetChildIndex(label3, 0);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbRemovingTags;
    }
}