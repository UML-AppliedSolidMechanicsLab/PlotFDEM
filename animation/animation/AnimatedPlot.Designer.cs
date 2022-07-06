/*
 * Created by SharpDevelop.
 * User: sstaple
 * Date: 8/4/2010
 * Time: 1:01 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Animation
{
	partial class AnimatedPlot
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AnimatedPlot));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.bBackToFirst = new System.Windows.Forms.Button();
            this.bReverse = new System.Windows.Forms.Button();
            this.bPlay = new System.Windows.Forms.Button();
            this.bPause = new System.Windows.Forms.Button();
            this.bLoop = new System.Windows.Forms.Button();
            this.bForwardToEnd = new System.Windows.Forms.Button();
            this.bForwardFrame = new System.Windows.Forms.Button();
            this.bBackFrame = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.nudFPS = new System.Windows.Forms.NumericUpDown();
            this.bSave = new System.Windows.Forms.Button();
            this.bOptions = new System.Windows.Forms.Button();
            this.bPlotAll = new System.Windows.Forms.Button();
            this.bExport = new System.Windows.Forms.Button();
            this.lFrameNumber = new System.Windows.Forms.Label();
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tbProgress = new System.Windows.Forms.TrackBar();
            this.nudSkip = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.contextMenuStrip_Panel1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsMI_Copy = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.nudFPS)).BeginInit();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbProgress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSkip)).BeginInit();
            this.contextMenuStrip_Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.Timer1Tick);
            // 
            // bBackToFirst
            // 
            this.bBackToFirst.BackColor = System.Drawing.Color.Transparent;
            this.bBackToFirst.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.bBackToFirst.FlatAppearance.BorderSize = 0;
            this.bBackToFirst.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bBackToFirst.Image = ((System.Drawing.Image)(resources.GetObject("bBackToFirst.Image")));
            this.bBackToFirst.Location = new System.Drawing.Point(3, 3);
            this.bBackToFirst.Name = "bBackToFirst";
            this.bBackToFirst.Size = new System.Drawing.Size(35, 35);
            this.bBackToFirst.TabIndex = 1;
            this.bBackToFirst.Tag = "Back To First Frame";
            this.bBackToFirst.UseVisualStyleBackColor = false;
            this.bBackToFirst.Click += new System.EventHandler(this.BBackToFirstClick);
            // 
            // bReverse
            // 
            this.bReverse.AccessibleDescription = "";
            this.bReverse.BackColor = System.Drawing.Color.Transparent;
            this.bReverse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.bReverse.FlatAppearance.BorderSize = 0;
            this.bReverse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bReverse.Image = ((System.Drawing.Image)(resources.GetObject("bReverse.Image")));
            this.bReverse.Location = new System.Drawing.Point(290, 3);
            this.bReverse.Name = "bReverse";
            this.bReverse.Size = new System.Drawing.Size(35, 35);
            this.bReverse.TabIndex = 2;
            this.bReverse.Tag = "";
            this.bReverse.UseVisualStyleBackColor = false;
            this.bReverse.Click += new System.EventHandler(this.BReverseClick);
            // 
            // bPlay
            // 
            this.bPlay.BackColor = System.Drawing.Color.Transparent;
            this.bPlay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.bPlay.FlatAppearance.BorderSize = 0;
            this.bPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bPlay.Image = ((System.Drawing.Image)(resources.GetObject("bPlay.Image")));
            this.bPlay.Location = new System.Drawing.Point(85, 3);
            this.bPlay.Name = "bPlay";
            this.bPlay.Size = new System.Drawing.Size(35, 35);
            this.bPlay.TabIndex = 3;
            this.bPlay.Tag = "Play";
            this.bPlay.UseVisualStyleBackColor = false;
            this.bPlay.Click += new System.EventHandler(this.BPlayClick);
            // 
            // bPause
            // 
            this.bPause.BackColor = System.Drawing.Color.Transparent;
            this.bPause.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.bPause.Enabled = false;
            this.bPause.FlatAppearance.BorderSize = 0;
            this.bPause.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bPause.Image = ((System.Drawing.Image)(resources.GetObject("bPause.Image")));
            this.bPause.Location = new System.Drawing.Point(126, 3);
            this.bPause.Name = "bPause";
            this.bPause.Size = new System.Drawing.Size(35, 35);
            this.bPause.TabIndex = 4;
            this.bPause.Tag = "Pause";
            this.bPause.UseVisualStyleBackColor = false;
            this.bPause.Click += new System.EventHandler(this.BPauseClick);
            // 
            // bLoop
            // 
            this.bLoop.BackColor = System.Drawing.Color.Transparent;
            this.bLoop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.bLoop.FlatAppearance.BorderSize = 0;
            this.bLoop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bLoop.ForeColor = System.Drawing.Color.Transparent;
            this.bLoop.Image = ((System.Drawing.Image)(resources.GetObject("bLoop.Image")));
            this.bLoop.Location = new System.Drawing.Point(249, 3);
            this.bLoop.Name = "bLoop";
            this.bLoop.Size = new System.Drawing.Size(35, 35);
            this.bLoop.TabIndex = 5;
            this.bLoop.Tag = "Toggle Continuous Play";
            this.bLoop.UseVisualStyleBackColor = false;
            this.bLoop.Click += new System.EventHandler(this.BLoopClick);
            // 
            // bForwardToEnd
            // 
            this.bForwardToEnd.BackColor = System.Drawing.Color.Transparent;
            this.bForwardToEnd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.bForwardToEnd.FlatAppearance.BorderSize = 0;
            this.bForwardToEnd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bForwardToEnd.Image = ((System.Drawing.Image)(resources.GetObject("bForwardToEnd.Image")));
            this.bForwardToEnd.Location = new System.Drawing.Point(208, 3);
            this.bForwardToEnd.Name = "bForwardToEnd";
            this.bForwardToEnd.Size = new System.Drawing.Size(35, 35);
            this.bForwardToEnd.TabIndex = 6;
            this.bForwardToEnd.Tag = "To Last Frame";
            this.bForwardToEnd.UseVisualStyleBackColor = false;
            this.bForwardToEnd.Click += new System.EventHandler(this.BForwardToEndClick);
            // 
            // bForwardFrame
            // 
            this.bForwardFrame.BackColor = System.Drawing.Color.Transparent;
            this.bForwardFrame.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.bForwardFrame.FlatAppearance.BorderSize = 0;
            this.bForwardFrame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bForwardFrame.Image = ((System.Drawing.Image)(resources.GetObject("bForwardFrame.Image")));
            this.bForwardFrame.Location = new System.Drawing.Point(167, 3);
            this.bForwardFrame.Name = "bForwardFrame";
            this.bForwardFrame.Size = new System.Drawing.Size(35, 35);
            this.bForwardFrame.TabIndex = 7;
            this.bForwardFrame.Tag = "Forward One Frame";
            this.bForwardFrame.UseVisualStyleBackColor = false;
            this.bForwardFrame.Click += new System.EventHandler(this.BForwardFrameClick);
            // 
            // bBackFrame
            // 
            this.bBackFrame.BackColor = System.Drawing.Color.Transparent;
            this.bBackFrame.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.bBackFrame.FlatAppearance.BorderSize = 0;
            this.bBackFrame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bBackFrame.Image = ((System.Drawing.Image)(resources.GetObject("bBackFrame.Image")));
            this.bBackFrame.Location = new System.Drawing.Point(44, 3);
            this.bBackFrame.Name = "bBackFrame";
            this.bBackFrame.Size = new System.Drawing.Size(35, 35);
            this.bBackFrame.TabIndex = 8;
            this.bBackFrame.Tag = "Back One Frame";
            this.bBackFrame.UseVisualStyleBackColor = false;
            this.bBackFrame.Click += new System.EventHandler(this.BBackFrameClick);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(337, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 23);
            this.label1.TabIndex = 9;
            this.label1.Text = "Frames/ Sec:";
            // 
            // nudFPS
            // 
            this.nudFPS.Location = new System.Drawing.Point(430, 15);
            this.nudFPS.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudFPS.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudFPS.Name = "nudFPS";
            this.nudFPS.Size = new System.Drawing.Size(36, 20);
            this.nudFPS.TabIndex = 10;
            this.nudFPS.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudFPS.ValueChanged += new System.EventHandler(this.nudFPSClick);
            // 
            // bSave
            // 
            this.bSave.Location = new System.Drawing.Point(571, 12);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(45, 23);
            this.bSave.TabIndex = 11;
            this.bSave.Text = "Save";
            this.bSave.UseVisualStyleBackColor = true;
            this.bSave.Click += new System.EventHandler(this.BSaveClick);
            // 
            // bOptions
            // 
            this.bOptions.Location = new System.Drawing.Point(622, 12);
            this.bOptions.Name = "bOptions";
            this.bOptions.Size = new System.Drawing.Size(49, 23);
            this.bOptions.TabIndex = 12;
            this.bOptions.Text = "Optons";
            this.bOptions.UseVisualStyleBackColor = true;
            this.bOptions.Click += new System.EventHandler(this.BOptionsClick);
            // 
            // bPlotAll
            // 
            this.bPlotAll.Location = new System.Drawing.Point(678, 12);
            this.bPlotAll.Name = "bPlotAll";
            this.bPlotAll.Size = new System.Drawing.Size(50, 23);
            this.bPlotAll.TabIndex = 14;
            this.bPlotAll.Text = "Plot All";
            this.bPlotAll.UseVisualStyleBackColor = true;
            this.bPlotAll.Click += new System.EventHandler(this.bPlotAllClick);
            // 
            // bExport
            // 
            this.bExport.Location = new System.Drawing.Point(734, 12);
            this.bExport.Name = "bExport";
            this.bExport.Size = new System.Drawing.Size(50, 23);
            this.bExport.TabIndex = 15;
            this.bExport.Text = "Export";
            this.bExport.UseVisualStyleBackColor = true;
            this.bExport.Click += new System.EventHandler(this.BExportClick);
            // 
            // lFrameNumber
            // 
            this.lFrameNumber.ForeColor = System.Drawing.Color.White;
            this.lFrameNumber.Location = new System.Drawing.Point(791, 12);
            this.lFrameNumber.Name = "lFrameNumber";
            this.lFrameNumber.Size = new System.Drawing.Size(38, 23);
            this.lFrameNumber.TabIndex = 16;
            this.lFrameNumber.Text = "label2";
            this.lFrameNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.zedGraphControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.zedGraphControl1.Location = new System.Drawing.Point(0, 43);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0D;
            this.zedGraphControl1.ScrollMaxX = 0D;
            this.zedGraphControl1.ScrollMaxY = 0D;
            this.zedGraphControl1.ScrollMaxY2 = 0D;
            this.zedGraphControl1.ScrollMinX = 0D;
            this.zedGraphControl1.ScrollMinY = 0D;
            this.zedGraphControl1.ScrollMinY2 = 0D;
            this.zedGraphControl1.Size = new System.Drawing.Size(442, 516);
            this.zedGraphControl1.TabIndex = 17;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(3, 45);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AllowDrop = true;
            this.splitContainer1.Panel1.ContextMenuStrip = this.contextMenuStrip_Panel1;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.zedGraphControl1);
            this.splitContainer1.Panel2.Controls.Add(this.tbProgress);
            this.splitContainer1.Size = new System.Drawing.Size(830, 559);
            this.splitContainer1.SplitterDistance = 384;
            this.splitContainer1.TabIndex = 18;
            // 
            // tbProgress
            // 
            this.tbProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbProgress.BackColor = System.Drawing.Color.DimGray;
            this.tbProgress.Location = new System.Drawing.Point(12, 8);
            this.tbProgress.Name = "tbProgress";
            this.tbProgress.Size = new System.Drawing.Size(425, 45);
            this.tbProgress.TabIndex = 21;
            this.tbProgress.Scroll += new System.EventHandler(this.TbProgressScroll);
            // 
            // nudSkip
            // 
            this.nudSkip.Location = new System.Drawing.Point(515, 15);
            this.nudSkip.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudSkip.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSkip.Name = "nudSkip";
            this.nudSkip.Size = new System.Drawing.Size(36, 20);
            this.nudSkip.TabIndex = 20;
            this.nudSkip.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSkip.ValueChanged += new System.EventHandler(this.NudSkipValueChanged);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(475, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 23);
            this.label2.TabIndex = 19;
            this.label2.Text = "Skip";
            // 
            // contextMenuStrip_Panel1
            // 
            this.contextMenuStrip_Panel1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsMI_Copy});
            this.contextMenuStrip_Panel1.Name = "contextMenuStrip_Panel1";
            this.contextMenuStrip_Panel1.Size = new System.Drawing.Size(181, 48);
            this.contextMenuStrip_Panel1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip_Panel1_ItemClicked);
            // 
            // tsMI_Copy
            // 
            this.tsMI_Copy.Name = "tsMI_Copy";
            this.tsMI_Copy.Size = new System.Drawing.Size(180, 22);
            this.tsMI_Copy.Text = "Copy";
            // 
            // AnimatedPlot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(845, 609);
            this.Controls.Add(this.nudSkip);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.lFrameNumber);
            this.Controls.Add(this.bExport);
            this.Controls.Add(this.bPlotAll);
            this.Controls.Add(this.bOptions);
            this.Controls.Add(this.bSave);
            this.Controls.Add(this.nudFPS);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bBackFrame);
            this.Controls.Add(this.bForwardFrame);
            this.Controls.Add(this.bForwardToEnd);
            this.Controls.Add(this.bLoop);
            this.Controls.Add(this.bPause);
            this.Controls.Add(this.bPlay);
            this.Controls.Add(this.bReverse);
            this.Controls.Add(this.bBackToFirst);
            this.ForeColor = System.Drawing.Color.DimGray;
            this.Name = "AnimatedPlot";
            this.Text = "Animated Plot";
            ((System.ComponentModel.ISupportInitialize)(this.nudFPS)).EndInit();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tbProgress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSkip)).EndInit();
            this.contextMenuStrip_Panel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		private System.Windows.Forms.TrackBar tbProgress;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown nudSkip;
		private System.Windows.Forms.SplitContainer splitContainer1;
		protected ZedGraph.ZedGraphControl zedGraphControl1;
		private System.Windows.Forms.Label lFrameNumber;
		protected System.Windows.Forms.Button bExport;
		private System.Windows.Forms.NumericUpDown nudFPS;
		protected System.Windows.Forms.Button bPlotAll;
		private System.Windows.Forms.Button bOptions;
		protected System.Windows.Forms.Button bSave;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button bBackFrame;
		private System.Windows.Forms.Button bForwardFrame;
		private System.Windows.Forms.Button bForwardToEnd;
		private System.Windows.Forms.Button bLoop;
		private System.Windows.Forms.Button bPause;
		private System.Windows.Forms.Button bPlay;
		private System.Windows.Forms.Button bReverse;
		private System.Windows.Forms.Button bBackToFirst;
		private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_Panel1;
        private System.Windows.Forms.ToolStripMenuItem tsMI_Copy;
    }
}
