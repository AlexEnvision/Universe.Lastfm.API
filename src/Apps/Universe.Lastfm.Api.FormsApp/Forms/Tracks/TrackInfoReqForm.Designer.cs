namespace Universe.Lastfm.Api.FormsApp.Forms.Tracks
{
    partial class TrackInfoReqForm
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
            label1 = new System.Windows.Forms.Label();
            tbTrack = new System.Windows.Forms.TextBox();
            tbPerformer = new System.Windows.Forms.TextBox();
            btOk = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(16, 85);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(46, 20);
            label2.TabIndex = 8;
            label2.Text = "Track:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(16, 42);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(77, 20);
            label1.TabIndex = 7;
            label1.Text = "Performer:";
            // 
            // tbTrack
            // 
            tbTrack.Location = new System.Drawing.Point(101, 82);
            tbTrack.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tbTrack.Name = "tbTrack";
            tbTrack.Size = new System.Drawing.Size(379, 27);
            tbTrack.TabIndex = 6;
            // 
            // tbPerformer
            // 
            tbPerformer.Location = new System.Drawing.Point(101, 39);
            tbPerformer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tbPerformer.Name = "tbPerformer";
            tbPerformer.Size = new System.Drawing.Size(379, 27);
            tbPerformer.TabIndex = 5;
            // 
            // btOk
            // 
            btOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            btOk.Location = new System.Drawing.Point(380, 224);
            btOk.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btOk.Name = "btOk";
            btOk.Size = new System.Drawing.Size(100, 35);
            btOk.TabIndex = 9;
            btOk.Text = "ОК";
            btOk.UseVisualStyleBackColor = true;
            btOk.Click += btOk_Click;
            // 
            // TrackInfoReqForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(512, 273);
            Controls.Add(btOk);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(tbTrack);
            Controls.Add(tbPerformer);
            Name = "TrackInfoReqForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbTrack;
        private System.Windows.Forms.TextBox tbPerformer;
        private System.Windows.Forms.Button btOk;
    }
}