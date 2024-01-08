namespace Universe.Lastfm.Api.FormsApp.Forms.Albums
{
    partial class AlbumReqInfoForm
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
            btOk = new System.Windows.Forms.Button();
            tbPerformer = new System.Windows.Forms.TextBox();
            tbAlbum = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            SuspendLayout();
            // 
            // btOk
            // 
            btOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            btOk.Location = new System.Drawing.Point(390, 144);
            btOk.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btOk.Name = "btOk";
            btOk.Size = new System.Drawing.Size(100, 35);
            btOk.TabIndex = 0;
            btOk.Text = "ОК";
            btOk.UseVisualStyleBackColor = true;
            btOk.Click += btOk_Click;
            // 
            // tbPerformer
            // 
            tbPerformer.Location = new System.Drawing.Point(101, 33);
            tbPerformer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tbPerformer.Name = "tbPerformer";
            tbPerformer.Size = new System.Drawing.Size(389, 27);
            tbPerformer.TabIndex = 1;
            // 
            // tbAlbum
            // 
            tbAlbum.Location = new System.Drawing.Point(101, 87);
            tbAlbum.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tbAlbum.Name = "tbAlbum";
            tbAlbum.Size = new System.Drawing.Size(389, 27);
            tbAlbum.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(16, 38);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(77, 20);
            label1.TabIndex = 3;
            label1.Text = "Performer:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(16, 91);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(56, 20);
            label2.TabIndex = 4;
            label2.Text = "Album:";
            // 
            // AlbumReqInfoForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(512, 208);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(tbAlbum);
            Controls.Add(tbPerformer);
            Controls.Add(btOk);
            Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            Name = "AlbumReqInfoForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button btOk;
        private System.Windows.Forms.TextBox tbPerformer;
        private System.Windows.Forms.TextBox tbAlbum;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}