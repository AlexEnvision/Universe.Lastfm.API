namespace Universe.Lastfm.Api.FormsApp.Forms.Tracks
{
    partial class TrackTagsReqForm
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
            tbUser = new System.Windows.Forms.TextBox();
            SuspendLayout();
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(16, 133);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(41, 20);
            label3.TabIndex = 11;
            label3.Text = "User:";
            // 
            // tbUser
            // 
            tbUser.Location = new System.Drawing.Point(101, 130);
            tbUser.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tbUser.Name = "tbUser";
            tbUser.Size = new System.Drawing.Size(379, 27);
            tbUser.TabIndex = 10;
            // 
            // TrackTagsReqForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(512, 273);
            Controls.Add(label3);
            Controls.Add(tbUser);
            Name = "TrackTagsReqForm";
            Controls.SetChildIndex(tbUser, 0);
            Controls.SetChildIndex(label3, 0);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbUser;
    }
}