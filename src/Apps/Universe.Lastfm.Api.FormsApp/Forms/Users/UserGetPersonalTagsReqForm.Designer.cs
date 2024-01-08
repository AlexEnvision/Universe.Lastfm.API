namespace Universe.Lastfm.Api.FormsApp.Forms.Users
{
    partial class UserGetPersonalTagsReqForm
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
            tbTag = new System.Windows.Forms.TextBox();
            label3 = new System.Windows.Forms.Label();
            cbTaggingType = new System.Windows.Forms.ComboBox();
            SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(24, 74);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(76, 20);
            label2.TabIndex = 16;
            label2.Text = "Tag name:";
            // 
            // tbTag
            // 
            tbTag.Location = new System.Drawing.Point(111, 71);
            tbTag.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tbTag.Name = "tbTag";
            tbTag.Size = new System.Drawing.Size(377, 27);
            tbTag.TabIndex = 15;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(24, 111);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(98, 20);
            label3.TabIndex = 18;
            label3.Text = "Tagging type:";
            // 
            // cbTaggingType
            // 
            cbTaggingType.FormattingEnabled = true;
            cbTaggingType.Items.AddRange(new object[] { "artist", "album", "track" });
            cbTaggingType.Location = new System.Drawing.Point(131, 107);
            cbTaggingType.Name = "cbTaggingType";
            cbTaggingType.Size = new System.Drawing.Size(357, 28);
            cbTaggingType.TabIndex = 19;
            // 
            // UserGetPersonalTagsReqForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(512, 208);
            Controls.Add(cbTaggingType);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(tbTag);
            Name = "UserGetPersonalTagsReqForm";
            Controls.SetChildIndex(tbTag, 0);
            Controls.SetChildIndex(label2, 0);
            Controls.SetChildIndex(label3, 0);
            Controls.SetChildIndex(cbTaggingType, 0);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbTag;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbTaggingType;
    }
}