﻿namespace Universe.Lastfm.Api.FormsApp
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
            gbBorders002 = new System.Windows.Forms.GroupBox();
            tbSecretKey = new System.Windows.Forms.TextBox();
            gbBorders001 = new System.Windows.Forms.GroupBox();
            tbApiKey = new System.Windows.Forms.TextBox();
            lbSecretKey = new System.Windows.Forms.Label();
            lbApiKey = new System.Windows.Forms.Label();
            chTrustedApp = new System.Windows.Forms.CheckBox();
            btConnect = new System.Windows.Forms.Button();
            groupBox2 = new System.Windows.Forms.GroupBox();
            pApiControls = new System.Windows.Forms.Panel();
            groupBox10 = new System.Windows.Forms.GroupBox();
            btAlbumRemoveTag = new System.Windows.Forms.Button();
            btAlbumAddTags = new System.Windows.Forms.Button();
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
            btAlbumGetTopTags = new System.Windows.Forms.Button();
            btGetAlbumTags = new System.Windows.Forms.Button();
            btAlbumSearch = new System.Windows.Forms.Button();
            btAlbumGetInfo = new System.Windows.Forms.Button();
            pMainForm = new System.Windows.Forms.Panel();
            gbBorders003 = new System.Windows.Forms.GroupBox();
            tbLog = new System.Windows.Forms.TextBox();
            menuStrip1 = new System.Windows.Forms.MenuStrip();
            applicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            viewToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            fullRubberyUIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            spaceModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            groupBox1.SuspendLayout();
            gbBorders002.SuspendLayout();
            gbBorders001.SuspendLayout();
            groupBox2.SuspendLayout();
            pApiControls.SuspendLayout();
            groupBox10.SuspendLayout();
            groupBox6.SuspendLayout();
            groupBox9.SuspendLayout();
            groupBox8.SuspendLayout();
            groupBox7.SuspendLayout();
            groupBox5.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox3.SuspendLayout();
            pMainForm.SuspendLayout();
            gbBorders003.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // btRun
            // 
            btRun.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            btRun.Enabled = false;
            btRun.Location = new System.Drawing.Point(15, 748);
            btRun.Name = "btRun";
            btRun.Size = new System.Drawing.Size(1336, 45);
            btRun.TabIndex = 0;
            btRun.Text = "Run";
            btRun.UseVisualStyleBackColor = true;
            btRun.Click += btRun_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(gbBorders002);
            groupBox1.Controls.Add(gbBorders001);
            groupBox1.Controls.Add(lbSecretKey);
            groupBox1.Controls.Add(lbApiKey);
            groupBox1.Controls.Add(chTrustedApp);
            groupBox1.Controls.Add(btConnect);
            groupBox1.Location = new System.Drawing.Point(15, 11);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(1336, 194);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "Last.fm API access settings";
            // 
            // gbBorders002
            // 
            gbBorders002.Controls.Add(tbSecretKey);
            gbBorders002.Location = new System.Drawing.Point(107, 69);
            gbBorders002.Name = "gbBorders002";
            gbBorders002.Size = new System.Drawing.Size(1214, 49);
            gbBorders002.TabIndex = 17;
            gbBorders002.TabStop = false;
            // 
            // tbSecretKey
            // 
            tbSecretKey.BorderStyle = System.Windows.Forms.BorderStyle.None;
            tbSecretKey.Location = new System.Drawing.Point(6, 16);
            tbSecretKey.Name = "tbSecretKey";
            tbSecretKey.PasswordChar = '*';
            tbSecretKey.Size = new System.Drawing.Size(1202, 20);
            tbSecretKey.TabIndex = 6;
            tbSecretKey.TextChanged += tbClientSecret_TextChanged;
            // 
            // gbBorders001
            // 
            gbBorders001.Controls.Add(tbApiKey);
            gbBorders001.Location = new System.Drawing.Point(107, 20);
            gbBorders001.Name = "gbBorders001";
            gbBorders001.Size = new System.Drawing.Size(1214, 48);
            gbBorders001.TabIndex = 16;
            gbBorders001.TabStop = false;
            // 
            // tbApiKey
            // 
            tbApiKey.BorderStyle = System.Windows.Forms.BorderStyle.None;
            tbApiKey.Location = new System.Drawing.Point(6, 18);
            tbApiKey.Name = "tbApiKey";
            tbApiKey.PasswordChar = '*';
            tbApiKey.Size = new System.Drawing.Size(1202, 20);
            tbApiKey.TabIndex = 4;
            tbApiKey.TextChanged += tbApiKey_TextChanged;
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
            chTrustedApp.Location = new System.Drawing.Point(11, 129);
            chTrustedApp.Name = "chTrustedApp";
            chTrustedApp.Size = new System.Drawing.Size(170, 24);
            chTrustedApp.TabIndex = 15;
            chTrustedApp.Text = "Verification is passed";
            chTrustedApp.UseVisualStyleBackColor = true;
            chTrustedApp.CheckedChanged += chTrustedApp_CheckedChanged;
            // 
            // btConnect
            // 
            btConnect.Location = new System.Drawing.Point(778, 129);
            btConnect.Name = "btConnect";
            btConnect.Size = new System.Drawing.Size(539, 39);
            btConnect.TabIndex = 14;
            btConnect.Text = "Connect";
            btConnect.UseVisualStyleBackColor = true;
            btConnect.Click += btConnect_Click;
            // 
            // groupBox2
            // 
            groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            groupBox2.Controls.Add(pApiControls);
            groupBox2.Location = new System.Drawing.Point(15, 203);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(1336, 538);
            groupBox2.TabIndex = 0;
            groupBox2.TabStop = false;
            groupBox2.Text = "API methods";
            // 
            // pApiControls
            // 
            pApiControls.Controls.Add(groupBox10);
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
            pApiControls.Size = new System.Drawing.Size(1330, 512);
            pApiControls.TabIndex = 1;
            // 
            // groupBox10
            // 
            groupBox10.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            groupBox10.Controls.Add(btAlbumRemoveTag);
            groupBox10.Controls.Add(btAlbumAddTags);
            groupBox10.Location = new System.Drawing.Point(8, 216);
            groupBox10.Name = "groupBox10";
            groupBox10.Size = new System.Drawing.Size(173, 284);
            groupBox10.TabIndex = 5;
            groupBox10.TabStop = false;
            groupBox10.Text = "album commands";
            // 
            // btAlbumRemoveTag
            // 
            btAlbumRemoveTag.Enabled = false;
            btAlbumRemoveTag.Location = new System.Drawing.Point(7, 75);
            btAlbumRemoveTag.Name = "btAlbumRemoveTag";
            btAlbumRemoveTag.Size = new System.Drawing.Size(160, 32);
            btAlbumRemoveTag.TabIndex = 6;
            btAlbumRemoveTag.Text = "album.removeTag";
            btAlbumRemoveTag.UseVisualStyleBackColor = true;
            btAlbumRemoveTag.Click += btAlbumRemoveTag_Click;
            // 
            // btAlbumAddTags
            // 
            btAlbumAddTags.Enabled = false;
            btAlbumAddTags.Location = new System.Drawing.Point(7, 40);
            btAlbumAddTags.Name = "btAlbumAddTags";
            btAlbumAddTags.Size = new System.Drawing.Size(160, 32);
            btAlbumAddTags.TabIndex = 5;
            btAlbumAddTags.Text = "album.addTags";
            btAlbumAddTags.UseVisualStyleBackColor = true;
            btAlbumAddTags.Click += btAlbumAddTags_Click;
            // 
            // groupBox6
            // 
            groupBox6.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            groupBox6.Controls.Add(btTrackScrobble);
            groupBox6.Location = new System.Drawing.Point(1169, 11);
            groupBox6.Name = "groupBox6";
            groupBox6.Size = new System.Drawing.Size(150, 489);
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
            groupBox9.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
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
            groupBox9.Size = new System.Drawing.Size(238, 489);
            groupBox9.TabIndex = 8;
            groupBox9.TabStop = false;
            groupBox9.Text = "user";
            // 
            // btUserGetWeeklyTrackChart
            // 
            btUserGetWeeklyTrackChart.Enabled = false;
            btUserGetWeeklyTrackChart.Location = new System.Drawing.Point(8, 415);
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
            btUserGetWeeklyChartList.Location = new System.Drawing.Point(8, 381);
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
            btUserGetWeeklyArtistChart.Location = new System.Drawing.Point(8, 347);
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
            btUserGetWeeklyAlbumChart.Location = new System.Drawing.Point(8, 313);
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
            btUserGetRecentTracks.Location = new System.Drawing.Point(8, 279);
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
            groupBox8.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            groupBox8.Controls.Add(btTrackGetInfo);
            groupBox8.Location = new System.Drawing.Point(740, 11);
            groupBox8.Name = "groupBox8";
            groupBox8.Size = new System.Drawing.Size(179, 489);
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
            groupBox7.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            groupBox7.Controls.Add(btTagGetInfo);
            groupBox7.Location = new System.Drawing.Point(555, 11);
            groupBox7.Name = "groupBox7";
            groupBox7.Size = new System.Drawing.Size(179, 489);
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
            groupBox5.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            groupBox5.Controls.Add(btChartGetTopArtists);
            groupBox5.Location = new System.Drawing.Point(371, 11);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new System.Drawing.Size(178, 489);
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
            groupBox4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            groupBox4.Controls.Add(btArtistGetInfo);
            groupBox4.Controls.Add(btGetArtistTags);
            groupBox4.Location = new System.Drawing.Point(187, 11);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new System.Drawing.Size(178, 489);
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
            groupBox3.Controls.Add(btAlbumGetTopTags);
            groupBox3.Controls.Add(btGetAlbumTags);
            groupBox3.Controls.Add(btAlbumSearch);
            groupBox3.Controls.Add(btAlbumGetInfo);
            groupBox3.Location = new System.Drawing.Point(8, 11);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new System.Drawing.Size(173, 186);
            groupBox3.TabIndex = 3;
            groupBox3.TabStop = false;
            groupBox3.Text = "album queries";
            // 
            // btAlbumGetTopTags
            // 
            btAlbumGetTopTags.Enabled = false;
            btAlbumGetTopTags.Location = new System.Drawing.Point(7, 142);
            btAlbumGetTopTags.Name = "btAlbumGetTopTags";
            btAlbumGetTopTags.Size = new System.Drawing.Size(160, 32);
            btAlbumGetTopTags.TabIndex = 4;
            btAlbumGetTopTags.Text = "album.getTopTags";
            btAlbumGetTopTags.UseVisualStyleBackColor = true;
            btAlbumGetTopTags.Click += btAlbumGetTopTags_Click;
            // 
            // btGetAlbumTags
            // 
            btGetAlbumTags.Enabled = false;
            btGetAlbumTags.Location = new System.Drawing.Point(7, 109);
            btGetAlbumTags.Name = "btGetAlbumTags";
            btGetAlbumTags.Size = new System.Drawing.Size(160, 32);
            btGetAlbumTags.TabIndex = 1;
            btGetAlbumTags.Text = "album.getTags";
            btGetAlbumTags.Click += btGetAlbumTags_Click;
            // 
            // btAlbumSearch
            // 
            btAlbumSearch.Enabled = false;
            btAlbumSearch.Location = new System.Drawing.Point(7, 77);
            btAlbumSearch.Name = "btAlbumSearch";
            btAlbumSearch.Size = new System.Drawing.Size(160, 33);
            btAlbumSearch.TabIndex = 3;
            btAlbumSearch.Text = "album.search";
            btAlbumSearch.UseVisualStyleBackColor = true;
            btAlbumSearch.Click += btAlbumSearch_Click;
            // 
            // btAlbumGetInfo
            // 
            btAlbumGetInfo.Enabled = false;
            btAlbumGetInfo.Location = new System.Drawing.Point(7, 39);
            btAlbumGetInfo.Name = "btAlbumGetInfo";
            btAlbumGetInfo.Size = new System.Drawing.Size(160, 35);
            btAlbumGetInfo.TabIndex = 2;
            btAlbumGetInfo.Text = "album.getInfo";
            btAlbumGetInfo.UseVisualStyleBackColor = true;
            btAlbumGetInfo.Click += btAlbumGetInfo_Click;
            // 
            // pMainForm
            // 
            pMainForm.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pMainForm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pMainForm.Controls.Add(gbBorders003);
            pMainForm.Controls.Add(groupBox1);
            pMainForm.Controls.Add(btRun);
            pMainForm.Controls.Add(groupBox2);
            pMainForm.Location = new System.Drawing.Point(0, 34);
            pMainForm.Name = "pMainForm";
            pMainForm.Size = new System.Drawing.Size(1739, 807);
            pMainForm.TabIndex = 6;
            // 
            // gbBorders003
            // 
            gbBorders003.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            gbBorders003.Controls.Add(tbLog);
            gbBorders003.Location = new System.Drawing.Point(1365, 9);
            gbBorders003.Name = "gbBorders003";
            gbBorders003.Size = new System.Drawing.Size(361, 787);
            gbBorders003.TabIndex = 8;
            gbBorders003.TabStop = false;
            // 
            // tbLog
            // 
            tbLog.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            tbLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            tbLog.Location = new System.Drawing.Point(6, 11);
            tbLog.Multiline = true;
            tbLog.Name = "tbLog";
            tbLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            tbLog.Size = new System.Drawing.Size(349, 765);
            tbLog.TabIndex = 4;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { applicationToolStripMenuItem, viewToolStripMenuItem, viewToolStripMenuItem1, aboutToolStripMenuItem });
            menuStrip1.Location = new System.Drawing.Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new System.Drawing.Size(1739, 28);
            menuStrip1.TabIndex = 7;
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
            // viewToolStripMenuItem
            // 
            viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { undoToolStripMenuItem, redoToolStripMenuItem });
            viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            viewToolStripMenuItem.Size = new System.Drawing.Size(49, 24);
            viewToolStripMenuItem.Text = "Edit";
            // 
            // undoToolStripMenuItem
            // 
            undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            undoToolStripMenuItem.Size = new System.Drawing.Size(128, 26);
            undoToolStripMenuItem.Text = "Undo";
            // 
            // redoToolStripMenuItem
            // 
            redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            redoToolStripMenuItem.Size = new System.Drawing.Size(128, 26);
            redoToolStripMenuItem.Text = "Redo";
            // 
            // viewToolStripMenuItem1
            // 
            viewToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { fullRubberyUIToolStripMenuItem, spaceModeToolStripMenuItem });
            viewToolStripMenuItem1.Name = "viewToolStripMenuItem1";
            viewToolStripMenuItem1.Size = new System.Drawing.Size(55, 24);
            viewToolStripMenuItem1.Text = "View";
            // 
            // fullRubberyUIToolStripMenuItem
            // 
            fullRubberyUIToolStripMenuItem.Checked = true;
            fullRubberyUIToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            fullRubberyUIToolStripMenuItem.Name = "fullRubberyUIToolStripMenuItem";
            fullRubberyUIToolStripMenuItem.Size = new System.Drawing.Size(192, 26);
            fullRubberyUIToolStripMenuItem.Text = "Full Rubbery UI";
            fullRubberyUIToolStripMenuItem.Click += fullRubberyUIToolStripMenuItem_Click;
            // 
            // spaceModeToolStripMenuItem
            // 
            spaceModeToolStripMenuItem.Name = "spaceModeToolStripMenuItem";
            spaceModeToolStripMenuItem.Size = new System.Drawing.Size(192, 26);
            spaceModeToolStripMenuItem.Text = "SpaceMode";
            spaceModeToolStripMenuItem.Click += spaceModeToolStripMenuItem_Click;
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new System.Drawing.Size(64, 24);
            aboutToolStripMenuItem.Text = "About";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1739, 843);
            Controls.Add(menuStrip1);
            Controls.Add(pMainForm);
            MinimumSize = new System.Drawing.Size(1757, 865);
            Name = "MainForm";
            MaximizedBoundsChanged += MainForm_MaximizedBoundsChanged;
            MaximumSizeChanged += MainForm_MaximumSizeChanged;
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            SizeChanged += MainForm_SizeChanged;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            gbBorders002.ResumeLayout(false);
            gbBorders002.PerformLayout();
            gbBorders001.ResumeLayout(false);
            gbBorders001.PerformLayout();
            groupBox2.ResumeLayout(false);
            pApiControls.ResumeLayout(false);
            groupBox10.ResumeLayout(false);
            groupBox6.ResumeLayout(false);
            groupBox9.ResumeLayout(false);
            groupBox8.ResumeLayout(false);
            groupBox7.ResumeLayout(false);
            groupBox5.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            pMainForm.ResumeLayout(false);
            gbBorders003.ResumeLayout(false);
            gbBorders003.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button btRun;
        private System.Windows.Forms.GroupBox groupBox1;
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
        private System.Windows.Forms.Panel pMainForm;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem applicationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem fullRubberyUIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.TextBox tbLog;
        private System.Windows.Forms.GroupBox gbBorders003;
        private System.Windows.Forms.GroupBox gbBorders001;
        private System.Windows.Forms.GroupBox gbBorders002;
        private System.Windows.Forms.ToolStripMenuItem spaceModeToolStripMenuItem;
        private System.Windows.Forms.Button btAlbumGetTopTags;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.Button btAlbumAddTags;
        private System.Windows.Forms.Button btAlbumRemoveTag;
    }
}
