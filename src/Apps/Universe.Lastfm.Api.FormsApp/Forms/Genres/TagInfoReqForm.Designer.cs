namespace Universe.Lastfm.Api.FormsApp.Forms.Genres
{
    /// <summary>
    /// <author>Alex Universe</author>
    /// <author>Alex Envision</author>
    /// </summary>
    partial class TagInfoReqForm
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
            tbTag = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            SuspendLayout();
            // 
            // btOk
            // 
            btOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            btOk.Location = new System.Drawing.Point(378, 224);
            btOk.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btOk.Name = "btOk";
            btOk.Size = new System.Drawing.Size(100, 35);
            btOk.TabIndex = 0;
            btOk.Text = "ОК";
            btOk.UseVisualStyleBackColor = true;
            btOk.Click += btOk_Click;
            // 
            // tbTag
            // 
            tbTag.Location = new System.Drawing.Point(104, 40);
            tbTag.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tbTag.Name = "tbTag";
            tbTag.Size = new System.Drawing.Size(374, 27);
            tbTag.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(16, 42);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(80, 20);
            label1.TabIndex = 3;
            label1.Text = "Tag/Genre:";
            // 
            // TagInfoReqForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(512, 273);
            Controls.Add(label1);
            Controls.Add(tbTag);
            Controls.Add(btOk);
            Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            Name = "TagInfoReqForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button btOk;
        private System.Windows.Forms.TextBox tbTag;
        private System.Windows.Forms.Label label1;
    }
}