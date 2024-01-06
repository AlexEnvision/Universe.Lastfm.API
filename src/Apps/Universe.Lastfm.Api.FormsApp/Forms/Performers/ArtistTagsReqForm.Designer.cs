namespace Universe.Lastfm.Api.FormsApp
{
    partial class ArtistTagsReqForm
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
            label2 = new System.Windows.Forms.Label();
            tbUser = new System.Windows.Forms.TextBox();
            SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(16, 98);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(41, 20);
            label2.TabIndex = 5;
            label2.Text = "User:";
            // 
            // tbUser
            // 
            tbUser.Location = new System.Drawing.Point(101, 94);
            tbUser.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tbUser.Name = "tbUser";
            tbUser.Size = new System.Drawing.Size(378, 27);
            tbUser.TabIndex = 4;
            // 
            // ArtistTagsReqForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(512, 218);
            Controls.Add(label2);
            Controls.Add(tbUser);
            Margin = new System.Windows.Forms.Padding(5, 8, 5, 8);
            Name = "ArtistTagsReqForm";
            Controls.SetChildIndex(tbUser, 0);
            Controls.SetChildIndex(label2, 0);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbUser;
    }
}