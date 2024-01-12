namespace Universe.Lastfm.Api.FormsApp.Forms
{
    partial class ReqContextForm
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
            label1 = new System.Windows.Forms.Label();
            tbPerformer = new System.Windows.Forms.TextBox();
            btOk = new System.Windows.Forms.Button();
            label2 = new System.Windows.Forms.Label();
            tbUser = new System.Windows.Forms.TextBox();
            label3 = new System.Windows.Forms.Label();
            tbAlbum = new System.Windows.Forms.TextBox();
            label4 = new System.Windows.Forms.Label();
            tbTrack = new System.Windows.Forms.TextBox();
            label5 = new System.Windows.Forms.Label();
            tbTag = new System.Windows.Forms.TextBox();
            label6 = new System.Windows.Forms.Label();
            tbRemovingTags = new System.Windows.Forms.TextBox();
            label7 = new System.Windows.Forms.Label();
            tbCreatingTags = new System.Windows.Forms.TextBox();
            label8 = new System.Windows.Forms.Label();
            tbOutputPath = new System.Windows.Forms.TextBox();
            btChoosePath = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(16, 42);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(77, 20);
            label1.TabIndex = 6;
            label1.Text = "Performer:";
            // 
            // tbPerformer
            // 
            tbPerformer.Location = new System.Drawing.Point(101, 39);
            tbPerformer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tbPerformer.Name = "tbPerformer";
            tbPerformer.Size = new System.Drawing.Size(379, 27);
            tbPerformer.TabIndex = 5;
            tbPerformer.Text = "Ayreon";
            // 
            // btOk
            // 
            btOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            btOk.Location = new System.Drawing.Point(16, 344);
            btOk.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btOk.Name = "btOk";
            btOk.Size = new System.Drawing.Size(1013, 45);
            btOk.TabIndex = 4;
            btOk.Text = "ОК";
            btOk.UseVisualStyleBackColor = true;
            btOk.Click += btOk_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(544, 46);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(41, 20);
            label2.TabIndex = 14;
            label2.Text = "User:";
            // 
            // tbUser
            // 
            tbUser.Location = new System.Drawing.Point(632, 41);
            tbUser.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tbUser.Name = "tbUser";
            tbUser.Size = new System.Drawing.Size(397, 27);
            tbUser.TabIndex = 13;
            tbUser.Text = "Howling91";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(15, 92);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(56, 20);
            label3.TabIndex = 16;
            label3.Text = "Album:";
            // 
            // tbAlbum
            // 
            tbAlbum.Location = new System.Drawing.Point(101, 88);
            tbAlbum.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tbAlbum.Name = "tbAlbum";
            tbAlbum.Size = new System.Drawing.Size(379, 27);
            tbAlbum.TabIndex = 15;
            tbAlbum.Text = "01011001";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(15, 143);
            label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(46, 20);
            label4.TabIndex = 18;
            label4.Text = "Track:";
            // 
            // tbTrack
            // 
            tbTrack.Location = new System.Drawing.Point(101, 140);
            tbTrack.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tbTrack.Name = "tbTrack";
            tbTrack.Size = new System.Drawing.Size(379, 27);
            tbTrack.TabIndex = 17;
            tbTrack.Text = "Age Of Shadows";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(15, 193);
            label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(80, 20);
            label5.TabIndex = 20;
            label5.Text = "Tag/Genre:";
            // 
            // tbTag
            // 
            tbTag.Location = new System.Drawing.Point(101, 190);
            tbTag.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tbTag.Name = "tbTag";
            tbTag.Size = new System.Drawing.Size(379, 27);
            tbTag.TabIndex = 19;
            tbTag.Text = "progressive metal";
            // 
            // label6
            // 
            label6.Location = new System.Drawing.Point(544, 193);
            label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(88, 65);
            label6.TabIndex = 22;
            label6.Text = "Tags for deletion:";
            // 
            // tbRemovingTags
            // 
            tbRemovingTags.Location = new System.Drawing.Point(632, 189);
            tbRemovingTags.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tbRemovingTags.Multiline = true;
            tbRemovingTags.Name = "tbRemovingTags";
            tbRemovingTags.Size = new System.Drawing.Size(397, 79);
            tbRemovingTags.TabIndex = 21;
            tbRemovingTags.Text = "Non-Progressive Metal";
            // 
            // label7
            // 
            label7.Location = new System.Drawing.Point(544, 95);
            label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(88, 72);
            label7.TabIndex = 24;
            label7.Text = "Tags for creation:";
            // 
            // tbCreatingTags
            // 
            tbCreatingTags.Location = new System.Drawing.Point(632, 92);
            tbCreatingTags.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tbCreatingTags.Multiline = true;
            tbCreatingTags.Name = "tbCreatingTags";
            tbCreatingTags.Size = new System.Drawing.Size(397, 75);
            tbCreatingTags.TabIndex = 23;
            tbCreatingTags.Text = "Progressive Metal";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(16, 307);
            label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(83, 20);
            label8.TabIndex = 25;
            label8.Text = "Output file:";
            // 
            // tbOutputPath
            // 
            tbOutputPath.Location = new System.Drawing.Point(101, 305);
            tbOutputPath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tbOutputPath.Name = "tbOutputPath";
            tbOutputPath.Size = new System.Drawing.Size(841, 27);
            tbOutputPath.TabIndex = 26;
            // 
            // btChoosePath
            // 
            btChoosePath.Location = new System.Drawing.Point(949, 304);
            btChoosePath.Name = "btChoosePath";
            btChoosePath.Size = new System.Drawing.Size(80, 29);
            btChoosePath.TabIndex = 27;
            btChoosePath.Text = "...";
            btChoosePath.UseVisualStyleBackColor = true;
            btChoosePath.Click += btChoosePath_Click;
            // 
            // ReqContextForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1042, 403);
            Controls.Add(btChoosePath);
            Controls.Add(tbOutputPath);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(tbCreatingTags);
            Controls.Add(label6);
            Controls.Add(tbRemovingTags);
            Controls.Add(label5);
            Controls.Add(tbTag);
            Controls.Add(label4);
            Controls.Add(tbTrack);
            Controls.Add(label3);
            Controls.Add(tbAlbum);
            Controls.Add(label2);
            Controls.Add(tbUser);
            Controls.Add(label1);
            Controls.Add(tbPerformer);
            Controls.Add(btOk);
            Name = "ReqContextForm";
            Text = "COLLECT FULL INFORMATION";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbPerformer;
        private System.Windows.Forms.Button btOk;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbUser;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbAlbum;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbTrack;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbTag;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbRemovingTags;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbCreatingTags;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbOutputPath;
        private System.Windows.Forms.Button btChoosePath;
    }
}