namespace Universe.Lastfm.Api.FormsApp.Forms
{
    /// <summary>
    /// <author>Alex Universe</author>
    /// <author>Alex Envision</author>
    /// </summary>
    partial class ScrobbleForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScrobbleForm));
            label5 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            tbTrack = new System.Windows.Forms.TextBox();
            label3 = new System.Windows.Forms.Label();
            tbAlbum = new System.Windows.Forms.TextBox();
            label2 = new System.Windows.Forms.Label();
            tbUser = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            tbPerformer = new System.Windows.Forms.TextBox();
            btOk = new System.Windows.Forms.Button();
            label9 = new System.Windows.Forms.Label();
            dtTimeStampPicker = new System.Windows.Forms.DateTimePicker();
            SuspendLayout();
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(10, 225);
            label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(89, 20);
            label5.TabIndex = 38;
            label5.Text = "Timestamp*";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(10, 188);
            label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(49, 20);
            label4.TabIndex = 36;
            label4.Text = "Track*";
            // 
            // tbTrack
            // 
            tbTrack.Location = new System.Drawing.Point(105, 185);
            tbTrack.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tbTrack.Name = "tbTrack";
            tbTrack.Size = new System.Drawing.Size(379, 27);
            tbTrack.TabIndex = 35;
            tbTrack.Text = "Age Of Shadows";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(10, 263);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(53, 20);
            label3.TabIndex = 34;
            label3.Text = "Album";
            // 
            // tbAlbum
            // 
            tbAlbum.Location = new System.Drawing.Point(105, 259);
            tbAlbum.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tbAlbum.Name = "tbAlbum";
            tbAlbum.Size = new System.Drawing.Size(379, 27);
            tbAlbum.TabIndex = 33;
            tbAlbum.Text = "01011001";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(539, 155);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(41, 20);
            label2.TabIndex = 32;
            label2.Text = "User:";
            // 
            // tbUser
            // 
            tbUser.Location = new System.Drawing.Point(627, 150);
            tbUser.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tbUser.Name = "tbUser";
            tbUser.Size = new System.Drawing.Size(397, 27);
            tbUser.TabIndex = 31;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(11, 151);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(80, 20);
            label1.TabIndex = 30;
            label1.Text = "Performer*";
            // 
            // tbPerformer
            // 
            tbPerformer.Location = new System.Drawing.Point(105, 148);
            tbPerformer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tbPerformer.Name = "tbPerformer";
            tbPerformer.Size = new System.Drawing.Size(379, 27);
            tbPerformer.TabIndex = 29;
            tbPerformer.Text = "Ayreon";
            // 
            // btOk
            // 
            btOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            btOk.Location = new System.Drawing.Point(16, 360);
            btOk.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btOk.Name = "btOk";
            btOk.Size = new System.Drawing.Size(1013, 45);
            btOk.TabIndex = 28;
            btOk.Text = "ОК";
            btOk.UseVisualStyleBackColor = true;
            btOk.Click += btOk_Click;
            // 
            // label9
            // 
            label9.Location = new System.Drawing.Point(11, 19);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(1013, 111);
            label9.TabIndex = 46;
            label9.Text = resources.GetString("label9.Text");
            // 
            // dtTimeStampPicker
            // 
            dtTimeStampPicker.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right;
            dtTimeStampPicker.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            dtTimeStampPicker.Location = new System.Drawing.Point(105, 224);
            dtTimeStampPicker.Name = "dtTimeStampPicker";
            dtTimeStampPicker.ShowUpDown = true;
            dtTimeStampPicker.Size = new System.Drawing.Size(379, 27);
            dtTimeStampPicker.TabIndex = 47;
            dtTimeStampPicker.Value = new System.DateTime(2024, 1, 19, 1, 6, 0, 0);
            // 
            // ScrobbleForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1042, 424);
            Controls.Add(dtTimeStampPicker);
            Controls.Add(label9);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(tbTrack);
            Controls.Add(label3);
            Controls.Add(tbAlbum);
            Controls.Add(label2);
            Controls.Add(tbUser);
            Controls.Add(label1);
            Controls.Add(tbPerformer);
            Controls.Add(btOk);
            Name = "ScrobbleForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbTrack;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbAlbum;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbUser;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbPerformer;
        private System.Windows.Forms.Button btOk;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker dtTimeStampPicker;
    }
}