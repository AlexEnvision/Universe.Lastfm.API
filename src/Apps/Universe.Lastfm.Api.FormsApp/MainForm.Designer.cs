namespace Universe.Lastfm.Api.FormsApp
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btRun = new System.Windows.Forms.Button();
            groupBox1 = new System.Windows.Forms.GroupBox();
            tbSecretKey = new System.Windows.Forms.TextBox();
            lbSecretKey = new System.Windows.Forms.Label();
            tbApiKey = new System.Windows.Forms.TextBox();
            lbApiKey = new System.Windows.Forms.Label();
            chTrustedApp = new System.Windows.Forms.CheckBox();
            btConnect = new System.Windows.Forms.Button();
            groupBox2 = new System.Windows.Forms.GroupBox();
            pApiControls = new System.Windows.Forms.Panel();
            groupBox6 = new System.Windows.Forms.GroupBox();
            btTrackScrobble = new System.Windows.Forms.Button();
            groupBox9 = new System.Windows.Forms.GroupBox();
            btUserGetWeeklyTrackChart = new System.Windows.Forms.Button();
            btUserGetWeeklyChartList = new System.Windows.Forms.Button();
            btUserGetWeeklyArtistChart = new System.Windows.Forms.Button();
            btUserGetWeeklyAlbumChart = new System.Windows.Forms.Button();
            btUserGetRecentTracks = new System.Windows.Forms.Button();
            btUserGetPersonalTags = new System.Windows.Forms.Button();
            btUserGetLovedTracks = new System.Windows.Forms.Button();
            btUserGetTopTracks = new System.Windows.Forms.Button();
            btUserGetTopTags = new System.Windows.Forms.Button();
            btUserGetTopAlbums = new System.Windows.Forms.Button();
            btUserGetTopArtists = new System.Windows.Forms.Button();
            btUserGetInfo = new System.Windows.Forms.Button();
            groupBox8 = new System.Windows.Forms.GroupBox();
            btTrackGetInfo = new System.Windows.Forms.Button();
            groupBox7 = new System.Windows.Forms.GroupBox();
            btTagGetInfo = new System.Windows.Forms.Button();
            groupBox5 = new System.Windows.Forms.GroupBox();
            btChartGetTopArtists = new System.Windows.Forms.Button();
            groupBox4 = new System.Windows.Forms.GroupBox();
            btArtistGetInfo = new System.Windows.Forms.Button();
            btGetArtistTags = new System.Windows.Forms.Button();
            groupBox3 = new System.Windows.Forms.GroupBox();
            btAlbumGetInfo = new System.Windows.Forms.Button();
            btGetAlbumTags = new System.Windows.Forms.Button();
            tbLog = new System.Windows.Forms.TextBox();
            menuStrip1 = new System.Windows.Forms.MenuStrip();
            applicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            btAlbumSearch = new System.Windows.Forms.Button();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            pApiControls.SuspendLayout();
            groupBox6.SuspendLayout();
            groupBox9.SuspendLayout();
            groupBox8.SuspendLayout();
            groupBox7.SuspendLayout();
            groupBox5.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox3.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // btRun
            // 
            btRun.Location = new System.Drawing.Point(9, 757);
            btRun.Name = "btRun";
            btRun.Size = new System.Drawing.Size(1339, 49);
            btRun.TabIndex = 0;
            btRun.Text = "Run";
            btRun.UseVisualStyleBackColor = true;
            btRun.Click += btRun_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(tbSecretKey);
            groupBox1.Controls.Add(lbSecretKey);
            groupBox1.Controls.Add(tbApiKey);
            groupBox1.Controls.Add(lbApiKey);
            groupBox1.Controls.Add(chTrustedApp);
            groupBox1.Controls.Add(btConnect);
            groupBox1.Location = new System.Drawing.Point(18, 43);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(1304, 178);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "Last.fm API access settings";
            // 
            // tbSecretKey
            // 
            tbSecretKey.Location = new System.Drawing.Point(108, 81);
            tbSecretKey.Name = "tbSecretKey";
            tbSecretKey.PasswordChar = '*';
            tbSecretKey.Size = new System.Drawing.Size(1184, 27);
            tbSecretKey.TabIndex = 6;
            tbSecretKey.TextChanged += tbClientSecret_TextChanged;
            // 
            // lbSecretKey
            // 
            lbSecretKey.AutoSize = true;
            lbSecretKey.Location = new System.Drawing.Point(11, 83);
            lbSecretKey.Name = "lbSecretKey";
            lbSecretKey.Size = new System.Drawing.Size(91, 20);
            lbSecretKey.TabIndex = 5;
            lbSecretKey.Text = "SECRET KEY:";
            // 
            // tbApiKey
            // 
            tbApiKey.Location = new System.Drawing.Point(108, 37);
            tbApiKey.Name = "tbApiKey";
            tbApiKey.PasswordChar = '*';
            tbApiKey.Size = new System.Drawing.Size(1184, 27);
            tbApiKey.TabIndex = 4;
            tbApiKey.TextChanged += tbApiKey_TextChanged;
            // 
            // lbApiKey
            // 
            lbApiKey.AutoSize = true;
            lbApiKey.Location = new System.Drawing.Point(11, 39);
            lbApiKey.Name = "lbApiKey";
            lbApiKey.Size = new System.Drawing.Size(63, 20);
            lbApiKey.TabIndex = 3;
            lbApiKey.Text = "API KEY:";
            // 
            // chTrustedApp
            // 
            chTrustedApp.AutoSize = true;
            chTrustedApp.Location = new System.Drawing.Point(11, 123);
            chTrustedApp.Name = "chTrustedApp";
            chTrustedApp.Size = new System.Drawing.Size(170, 24);
            chTrustedApp.TabIndex = 15;
            chTrustedApp.Text = "Verification is passed";
            chTrustedApp.UseVisualStyleBackColor = true;
            chTrustedApp.CheckedChanged += chTrustedApp_CheckedChanged;
            // 
            // btConnect
            // 
            btConnect.Location = new System.Drawing.Point(783, 123);
            btConnect.Name = "btConnect";
            btConnect.Size = new System.Drawing.Size(509, 39);
            btConnect.TabIndex = 14;
            btConnect.Text = "Connect";
            btConnect.UseVisualStyleBackColor = true;
            btConnect.Click += btConnect_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(pApiControls);
            groupBox2.Location = new System.Drawing.Point(12, 240);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(1336, 511);
            groupBox2.TabIndex = 0;
            groupBox2.TabStop = false;
            groupBox2.Text = "API methods";
            // 
            // pApiControls
            // 
            pApiControls.Controls.Add(groupBox6);
            pApiControls.Controls.Add(groupBox9);
            pApiControls.Controls.Add(groupBox8);
            pApiControls.Controls.Add(groupBox7);
            pApiControls.Controls.Add(groupBox5);
            pApiControls.Controls.Add(groupBox4);
            pApiControls.Controls.Add(groupBox3);
            pApiControls.Dock = System.Windows.Forms.DockStyle.Fill;
            pApiControls.Location = new System.Drawing.Point(3, 23);
            pApiControls.Name = "pApiControls";
            pApiControls.Size = new System.Drawing.Size(1330, 485);
            pApiControls.TabIndex = 1;
            // 
            // groupBox6
            // 
            groupBox6.Controls.Add(btTrackScrobble);
            groupBox6.Location = new System.Drawing.Point(1169, 11);
            groupBox6.Name = "groupBox6";
            groupBox6.Size = new System.Drawing.Size(150, 457);
            groupBox6.TabIndex = 6;
            groupBox6.TabStop = false;
            groupBox6.Text = "scrobbling";
            // 
            // btTrackScrobble
            // 
            btTrackScrobble.Enabled = false;
            btTrackScrobble.Location = new System.Drawing.Point(6, 39);
            btTrackScrobble.Name = "btTrackScrobble";
            btTrackScrobble.Size = new System.Drawing.Size(138, 35);
            btTrackScrobble.TabIndex = 0;
            btTrackScrobble.Text = "track.scrobble";
            btTrackScrobble.UseVisualStyleBackColor = true;
            btTrackScrobble.Click += btTrackScrobble_Click;
            // 
            // groupBox9
            // 
            groupBox9.Controls.Add(btUserGetWeeklyTrackChart);
            groupBox9.Controls.Add(btUserGetWeeklyChartList);
            groupBox9.Controls.Add(btUserGetWeeklyArtistChart);
            groupBox9.Controls.Add(btUserGetWeeklyAlbumChart);
            groupBox9.Controls.Add(btUserGetRecentTracks);
            groupBox9.Controls.Add(btUserGetPersonalTags);
            groupBox9.Controls.Add(btUserGetLovedTracks);
            groupBox9.Controls.Add(btUserGetTopTracks);
            groupBox9.Controls.Add(btUserGetTopTags);
            groupBox9.Controls.Add(btUserGetTopAlbums);
            groupBox9.Controls.Add(btUserGetTopArtists);
            groupBox9.Controls.Add(btUserGetInfo);
            groupBox9.Location = new System.Drawing.Point(925, 11);
            groupBox9.Name = "groupBox9";
            groupBox9.Size = new System.Drawing.Size(238, 457);
            groupBox9.TabIndex = 8;
            groupBox9.TabStop = false;
            groupBox9.Text = "user";
            // 
            // btUserGetWeeklyTrackChart
            // 
            btUserGetWeeklyTrackChart.Enabled = false;
            btUserGetWeeklyTrackChart.Location = new System.Drawing.Point(8, 417);
            btUserGetWeeklyTrackChart.Name = "btUserGetWeeklyTrackChart";
            btUserGetWeeklyTrackChart.Size = new System.Drawing.Size(224, 33);
            btUserGetWeeklyTrackChart.TabIndex = 15;
            btUserGetWeeklyTrackChart.Text = "user.getWeeklyTrackChart";
            btUserGetWeeklyTrackChart.UseVisualStyleBackColor = true;
            btUserGetWeeklyTrackChart.Click += btUserGetWeeklyTrackChart_Click;
            // 
            // btUserGetWeeklyChartList
            // 
            btUserGetWeeklyChartList.Enabled = false;
            btUserGetWeeklyChartList.Location = new System.Drawing.Point(8, 383);
            btUserGetWeeklyChartList.Name = "btUserGetWeeklyChartList";
            btUserGetWeeklyChartList.Size = new System.Drawing.Size(224, 33);
            btUserGetWeeklyChartList.TabIndex = 14;
            btUserGetWeeklyChartList.Text = "user.getWeeklyChartList";
            btUserGetWeeklyChartList.UseVisualStyleBackColor = true;
            btUserGetWeeklyChartList.Click += btUserGetWeeklyChartList_Click;
            // 
            // btUserGetWeeklyArtistChart
            // 
            btUserGetWeeklyArtistChart.Enabled = false;
            btUserGetWeeklyArtistChart.Location = new System.Drawing.Point(8, 349);
            btUserGetWeeklyArtistChart.Name = "btUserGetWeeklyArtistChart";
            btUserGetWeeklyArtistChart.Size = new System.Drawing.Size(224, 33);
            btUserGetWeeklyArtistChart.TabIndex = 13;
            btUserGetWeeklyArtistChart.Text = "user.getWeeklyArtistChart";
            btUserGetWeeklyArtistChart.UseVisualStyleBackColor = true;
            btUserGetWeeklyArtistChart.Click += btUserGetWeeklyArtistChart_Click;
            // 
            // btUserGetWeeklyAlbumChart
            // 
            btUserGetWeeklyAlbumChart.Enabled = false;
            btUserGetWeeklyAlbumChart.Location = new System.Drawing.Point(8, 315);
            btUserGetWeeklyAlbumChart.Name = "btUserGetWeeklyAlbumChart";
            btUserGetWeeklyAlbumChart.Size = new System.Drawing.Size(224, 33);
            btUserGetWeeklyAlbumChart.TabIndex = 12;
            btUserGetWeeklyAlbumChart.Text = "user.getWeeklyAlbumChart";
            btUserGetWeeklyAlbumChart.UseVisualStyleBackColor = true;
            btUserGetWeeklyAlbumChart.Click += btUserGetWeeklyAlbumChart_Click;
            // 
            // btUserGetRecentTracks
            // 
            btUserGetRecentTracks.Enabled = false;
            btUserGetRecentTracks.Location = new System.Drawing.Point(8, 281);
            btUserGetRecentTracks.Name = "btUserGetRecentTracks";
            btUserGetRecentTracks.Size = new System.Drawing.Size(224, 33);
            btUserGetRecentTracks.TabIndex = 11;
            btUserGetRecentTracks.Text = "user.getRecentTracks";
            btUserGetRecentTracks.UseVisualStyleBackColor = true;
            btUserGetRecentTracks.Click += btUserGetRecentTracks_Click;
            // 
            // btUserGetPersonalTags
            // 
            btUserGetPersonalTags.Enabled = false;
            btUserGetPersonalTags.Location = new System.Drawing.Point(8, 244);
            btUserGetPersonalTags.Name = "btUserGetPersonalTags";
            btUserGetPersonalTags.Size = new System.Drawing.Size(224, 33);
            btUserGetPersonalTags.TabIndex = 10;
            btUserGetPersonalTags.Text = "user.getPersonalTags";
            btUserGetPersonalTags.UseVisualStyleBackColor = true;
            btUserGetPersonalTags.Click += btUserGetPersonalTags_Click;
            // 
            // btUserGetLovedTracks
            // 
            btUserGetLovedTracks.Enabled = false;
            btUserGetLovedTracks.Location = new System.Drawing.Point(8, 210);
            btUserGetLovedTracks.Name = "btUserGetLovedTracks";
            btUserGetLovedTracks.Size = new System.Drawing.Size(224, 33);
            btUserGetLovedTracks.TabIndex = 9;
            btUserGetLovedTracks.Text = "user.getLovedTracks";
            btUserGetLovedTracks.UseVisualStyleBackColor = true;
            btUserGetLovedTracks.Click += btUserGetLovedTracks_Click;
            // 
            // btUserGetTopTracks
            // 
            btUserGetTopTracks.Enabled = false;
            btUserGetTopTracks.Location = new System.Drawing.Point(8, 176);
            btUserGetTopTracks.Name = "btUserGetTopTracks";
            btUserGetTopTracks.Size = new System.Drawing.Size(224, 33);
            btUserGetTopTracks.TabIndex = 8;
            btUserGetTopTracks.Text = "user.getTopTracks";
            btUserGetTopTracks.UseVisualStyleBackColor = true;
            btUserGetTopTracks.Click += btUserGetTopTracks_Click;
            // 
            // btUserGetTopTags
            // 
            btUserGetTopTags.Enabled = false;
            btUserGetTopTags.Location = new System.Drawing.Point(8, 142);
            btUserGetTopTags.Name = "btUserGetTopTags";
            btUserGetTopTags.Size = new System.Drawing.Size(224, 33);
            btUserGetTopTags.TabIndex = 7;
            btUserGetTopTags.Text = "user.getTopTags";
            btUserGetTopTags.UseVisualStyleBackColor = true;
            btUserGetTopTags.Click += btUserGetTopTags_Click;
            // 
            // btUserGetTopAlbums
            // 
            btUserGetTopAlbums.Enabled = false;
            btUserGetTopAlbums.Location = new System.Drawing.Point(8, 108);
            btUserGetTopAlbums.Name = "btUserGetTopAlbums";
            btUserGetTopAlbums.Size = new System.Drawing.Size(224, 33);
            btUserGetTopAlbums.TabIndex = 6;
            btUserGetTopAlbums.Text = "user.getTopAlbums";
            btUserGetTopAlbums.UseVisualStyleBackColor = true;
            btUserGetTopAlbums.Click += btUserGetTopAlbums_Click;
            // 
            // btUserGetTopArtists
            // 
            btUserGetTopArtists.Enabled = false;
            btUserGetTopArtists.Location = new System.Drawing.Point(8, 74);
            btUserGetTopArtists.Name = "btUserGetTopArtists";
            btUserGetTopArtists.Size = new System.Drawing.Size(224, 33);
            btUserGetTopArtists.TabIndex = 5;
            btUserGetTopArtists.Text = "user.getTopArtists";
            btUserGetTopArtists.UseVisualStyleBackColor = true;
            btUserGetTopArtists.Click += btUserGetTopArtists_Click;
            // 
            // btUserGetInfo
            // 
            btUserGetInfo.Enabled = false;
            btUserGetInfo.Location = new System.Drawing.Point(8, 39);
            btUserGetInfo.Name = "btUserGetInfo";
            btUserGetInfo.Size = new System.Drawing.Size(224, 35);
            btUserGetInfo.TabIndex = 4;
            btUserGetInfo.Text = "user.getInfo";
            btUserGetInfo.UseVisualStyleBackColor = true;
            btUserGetInfo.Click += btUserGetInfo_Click;
            // 
            // groupBox8
            // 
            groupBox8.Controls.Add(btTrackGetInfo);
            groupBox8.Location = new System.Drawing.Point(740, 11);
            groupBox8.Name = "groupBox8";
            groupBox8.Size = new System.Drawing.Size(179, 457);
            groupBox8.TabIndex = 8;
            groupBox8.TabStop = false;
            groupBox8.Text = "track";
            // 
            // btTrackGetInfo
            // 
            btTrackGetInfo.Enabled = false;
            btTrackGetInfo.Location = new System.Drawing.Point(8, 39);
            btTrackGetInfo.Name = "btTrackGetInfo";
            btTrackGetInfo.Size = new System.Drawing.Size(165, 35);
            btTrackGetInfo.TabIndex = 3;
            btTrackGetInfo.Text = "track.getInfo";
            btTrackGetInfo.UseVisualStyleBackColor = true;
            btTrackGetInfo.Click += btTrackGetInfo_Click;
            // 
            // groupBox7
            // 
            groupBox7.Controls.Add(btTagGetInfo);
            groupBox7.Location = new System.Drawing.Point(555, 11);
            groupBox7.Name = "groupBox7";
            groupBox7.Size = new System.Drawing.Size(179, 457);
            groupBox7.TabIndex = 7;
            groupBox7.TabStop = false;
            groupBox7.Text = "tag/genre";
            // 
            // btTagGetInfo
            // 
            btTagGetInfo.Enabled = false;
            btTagGetInfo.Location = new System.Drawing.Point(8, 39);
            btTagGetInfo.Name = "btTagGetInfo";
            btTagGetInfo.Size = new System.Drawing.Size(165, 35);
            btTagGetInfo.TabIndex = 2;
            btTagGetInfo.Text = "tag.getInfo";
            btTagGetInfo.UseVisualStyleBackColor = true;
            btTagGetInfo.Click += btTagGetInfo_Click;
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(btChartGetTopArtists);
            groupBox5.Location = new System.Drawing.Point(371, 11);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new System.Drawing.Size(178, 457);
            groupBox5.TabIndex = 5;
            groupBox5.TabStop = false;
            groupBox5.Text = "chart";
            // 
            // btChartGetTopArtists
            // 
            btChartGetTopArtists.Enabled = false;
            btChartGetTopArtists.Location = new System.Drawing.Point(6, 39);
            btChartGetTopArtists.Name = "btChartGetTopArtists";
            btChartGetTopArtists.Size = new System.Drawing.Size(165, 35);
            btChartGetTopArtists.TabIndex = 1;
            btChartGetTopArtists.Text = "chart.getTopArtists";
            btChartGetTopArtists.UseVisualStyleBackColor = true;
            btChartGetTopArtists.Click += btChartGetTopArtists_Click;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(btArtistGetInfo);
            groupBox4.Controls.Add(btGetArtistTags);
            groupBox4.Location = new System.Drawing.Point(187, 11);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new System.Drawing.Size(178, 457);
            groupBox4.TabIndex = 4;
            groupBox4.TabStop = false;
            groupBox4.Text = "artist/performer";
            // 
            // btArtistGetInfo
            // 
            btArtistGetInfo.Enabled = false;
            btArtistGetInfo.Location = new System.Drawing.Point(6, 39);
            btArtistGetInfo.Name = "btArtistGetInfo";
            btArtistGetInfo.Size = new System.Drawing.Size(165, 35);
            btArtistGetInfo.TabIndex = 2;
            btArtistGetInfo.Text = "artist.getInfo";
            btArtistGetInfo.Click += btArtistGetInfo_Click;
            // 
            // btGetArtistTags
            // 
            btGetArtistTags.Enabled = false;
            btGetArtistTags.Location = new System.Drawing.Point(6, 75);
            btGetArtistTags.Name = "btGetArtistTags";
            btGetArtistTags.Size = new System.Drawing.Size(165, 35);
            btGetArtistTags.TabIndex = 0;
            btGetArtistTags.Text = "artist.getTags";
            btGetArtistTags.Click += btGetArtistTags_Click;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(btAlbumSearch);
            groupBox3.Controls.Add(btAlbumGetInfo);
            groupBox3.Controls.Add(btGetAlbumTags);
            groupBox3.Location = new System.Drawing.Point(8, 11);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new System.Drawing.Size(173, 457);
            groupBox3.TabIndex = 3;
            groupBox3.TabStop = false;
            groupBox3.Text = "album";
            // 
            // btAlbumGetInfo
            // 
            btAlbumGetInfo.Enabled = false;
            btAlbumGetInfo.Location = new System.Drawing.Point(6, 37);
            btAlbumGetInfo.Name = "btAlbumGetInfo";
            btAlbumGetInfo.Size = new System.Drawing.Size(160, 37);
            btAlbumGetInfo.TabIndex = 2;
            btAlbumGetInfo.Text = "album.getInfo";
            btAlbumGetInfo.UseVisualStyleBackColor = true;
            btAlbumGetInfo.Click += btAlbumGetInfo_Click;
            // 
            // btGetAlbumTags
            // 
            btGetAlbumTags.Enabled = false;
            btGetAlbumTags.Location = new System.Drawing.Point(6, 74);
            btGetAlbumTags.Name = "btGetAlbumTags";
            btGetAlbumTags.Size = new System.Drawing.Size(160, 35);
            btGetAlbumTags.TabIndex = 1;
            btGetAlbumTags.Text = "album.getTags";
            btGetAlbumTags.Click += btGetAlbumTags_Click;
            // 
            // tbLog
            // 
            tbLog.Location = new System.Drawing.Point(1354, 43);
            tbLog.Multiline = true;
            tbLog.Name = "tbLog";
            tbLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            tbLog.Size = new System.Drawing.Size(370, 763);
            tbLog.TabIndex = 4;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { applicationToolStripMenuItem });
            menuStrip1.Location = new System.Drawing.Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new System.Drawing.Size(1739, 28);
            menuStrip1.TabIndex = 5;
            menuStrip1.Text = "menuStrip1";
            // 
            // applicationToolStripMenuItem
            // 
            applicationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { exitToolStripMenuItem });
            applicationToolStripMenuItem.Name = "applicationToolStripMenuItem";
            applicationToolStripMenuItem.Size = new System.Drawing.Size(100, 24);
            applicationToolStripMenuItem.Text = "Application";
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new System.Drawing.Size(116, 26);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // btAlbumSearch
            // 
            btAlbumSearch.Enabled = false;
            btAlbumSearch.Location = new System.Drawing.Point(6, 108);
            btAlbumSearch.Name = "btAlbumSearch";
            btAlbumSearch.Size = new System.Drawing.Size(160, 37);
            btAlbumSearch.TabIndex = 3;
            btAlbumSearch.Text = "album.search";
            btAlbumSearch.UseVisualStyleBackColor = true;
            btAlbumSearch.Click += btAlbumSearch_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1739, 818);
            Controls.Add(tbLog);
            Controls.Add(groupBox1);
            Controls.Add(groupBox2);
            Controls.Add(btRun);
            Controls.Add(menuStrip1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            MainMenuStrip = menuStrip1;
            MaximizeBox = false;
            Name = "MainForm";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            pApiControls.ResumeLayout(false);
            groupBox6.ResumeLayout(false);
            groupBox9.ResumeLayout(false);
            groupBox8.ResumeLayout(false);
            groupBox7.ResumeLayout(false);
            groupBox5.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button btRun;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbLog;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem applicationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.TextBox tbSecretKey;
        private System.Windows.Forms.Label lbSecretKey;
        private System.Windows.Forms.TextBox tbApiKey;
        private System.Windows.Forms.Label lbApiKey;

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btConnect;
        private System.Windows.Forms.Button btTrackScrobble;
        private System.Windows.Forms.Panel pApiControls;
        private System.Windows.Forms.CheckBox chTrustedApp;
        private System.Windows.Forms.Button btChartGetTopArtists;
        private System.Windows.Forms.Button btAlbumGetInfo;
        private System.Windows.Forms.Button btArtistGetInfo;
        private System.Windows.Forms.Button btGetArtistTags;
        private System.Windows.Forms.Button btGetAlbumTags;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button btTagGetInfo;
        private System.Windows.Forms.Button btTrackGetInfo;
        private System.Windows.Forms.Button btUserGetInfo;
        private System.Windows.Forms.Button btUserGetTopArtists;
        private System.Windows.Forms.Button btUserGetTopAlbums;
        private System.Windows.Forms.Button btUserGetTopTags;
        private System.Windows.Forms.Button btUserGetTopTracks;
        private System.Windows.Forms.Button btUserGetPersonalTags;
        private System.Windows.Forms.Button btUserGetLovedTracks;
        private System.Windows.Forms.Button btUserGetWeeklyTrackChart;
        private System.Windows.Forms.Button btUserGetWeeklyChartList;
        private System.Windows.Forms.Button btUserGetWeeklyArtistChart;
        private System.Windows.Forms.Button btUserGetWeeklyAlbumChart;
        private System.Windows.Forms.Button btUserGetRecentTracks;
        private System.Windows.Forms.Button btAlbumSearch;
    }
}
