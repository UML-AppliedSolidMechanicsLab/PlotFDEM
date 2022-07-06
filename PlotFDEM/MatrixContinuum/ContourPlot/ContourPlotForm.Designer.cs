/*
 * Created by SharpDevelop.
 * User: Scott
 * Date: 9/1/2011
 * Time: 2:34 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace PlotFDEM.MatrixContinuum.ContourPlot
{
    partial class ContourPlotForm
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
            if (disposing)
            {
                if (components != null)
                {
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
            this.spContourPlot = new System.Windows.Forms.SplitContainer();
            this.gb1 = new System.Windows.Forms.GroupBox();
            this.label16 = new System.Windows.Forms.Label();
            this.cbZPointQuery = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.cbYPointQuery = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.cbFiberQueryPair = new System.Windows.Forms.ComboBox();
            this.cbXPointQuery = new System.Windows.Forms.ComboBox();
            this.bQueryPoint = new System.Windows.Forms.Button();
            this.cbFiberNumbers = new System.Windows.Forms.CheckBox();
            this.cbRotations = new System.Windows.Forms.CheckBox();
            this.cbShowCrack = new System.Windows.Forms.CheckBox();
            this.cbConnections = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.bBoundaryColor = new System.Windows.Forms.Button();
            this.bFiberColor = new System.Windows.Forms.Button();
            this.bProjFiberColor = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cbAutomaticRange = new System.Windows.Forms.CheckBox();
            this.tbLowRange = new System.Windows.Forms.TextBox();
            this.tbHighRange = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cbPlotComponent = new System.Windows.Forms.ComboBox();
            this.cbPlotType = new System.Windows.Forms.ComboBox();
            this.cbColorSchemes = new System.Windows.Forms.ComboBox();
            this.nudIsoLines = new System.Windows.Forms.NumericUpDown();
            this.nudGridPtsX = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbIsoLines = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.bHighColor = new System.Windows.Forms.Button();
            this.bLowColor = new System.Windows.Forms.Button();
            this.bUpdate = new System.Windows.Forms.Button();
            this.tbMatrixTransparancy = new System.Windows.Forms.TrackBar();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cbProjections = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.spContourPlot)).BeginInit();
            this.spContourPlot.Panel1.SuspendLayout();
            this.spContourPlot.SuspendLayout();
            this.gb1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudIsoLines)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGridPtsX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbMatrixTransparancy)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // spContourPlot
            // 
            this.spContourPlot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spContourPlot.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.spContourPlot.Location = new System.Drawing.Point(0, 0);
            this.spContourPlot.Name = "spContourPlot";
            // 
            // spContourPlot.Panel1
            // 
            this.spContourPlot.Panel1.Controls.Add(this.cbProjections);
            this.spContourPlot.Panel1.Controls.Add(this.gb1);
            this.spContourPlot.Panel1.Controls.Add(this.cbFiberNumbers);
            this.spContourPlot.Panel1.Controls.Add(this.cbRotations);
            this.spContourPlot.Panel1.Controls.Add(this.cbShowCrack);
            this.spContourPlot.Panel1.Controls.Add(this.cbConnections);
            this.spContourPlot.Panel1.Controls.Add(this.label11);
            this.spContourPlot.Panel1.Controls.Add(this.label10);
            this.spContourPlot.Panel1.Controls.Add(this.label7);
            this.spContourPlot.Panel1.Controls.Add(this.bBoundaryColor);
            this.spContourPlot.Panel1.Controls.Add(this.bFiberColor);
            this.spContourPlot.Panel1.Controls.Add(this.bProjFiberColor);
            this.spContourPlot.Panel1.Controls.Add(this.label2);
            this.spContourPlot.Panel1.Controls.Add(this.cbAutomaticRange);
            this.spContourPlot.Panel1.Controls.Add(this.tbLowRange);
            this.spContourPlot.Panel1.Controls.Add(this.tbHighRange);
            this.spContourPlot.Panel1.Controls.Add(this.label9);
            this.spContourPlot.Panel1.Controls.Add(this.label8);
            this.spContourPlot.Panel1.Controls.Add(this.cbPlotComponent);
            this.spContourPlot.Panel1.Controls.Add(this.cbPlotType);
            this.spContourPlot.Panel1.Controls.Add(this.cbColorSchemes);
            this.spContourPlot.Panel1.Controls.Add(this.nudIsoLines);
            this.spContourPlot.Panel1.Controls.Add(this.nudGridPtsX);
            this.spContourPlot.Panel1.Controls.Add(this.label6);
            this.spContourPlot.Panel1.Controls.Add(this.label5);
            this.spContourPlot.Panel1.Controls.Add(this.cbIsoLines);
            this.spContourPlot.Panel1.Controls.Add(this.label4);
            this.spContourPlot.Panel1.Controls.Add(this.label3);
            this.spContourPlot.Panel1.Controls.Add(this.label1);
            this.spContourPlot.Panel1.Controls.Add(this.bHighColor);
            this.spContourPlot.Panel1.Controls.Add(this.bLowColor);
            this.spContourPlot.Panel1.Controls.Add(this.bUpdate);
            this.spContourPlot.Panel1.Controls.Add(this.tbMatrixTransparancy);
            // 
            // spContourPlot.Panel2
            // 
            this.spContourPlot.Panel2.AutoScroll = true;
            this.spContourPlot.Panel2.AutoScrollMinSize = new System.Drawing.Size(0, 10);
            this.spContourPlot.Panel2.ContextMenuStrip = this.contextMenuStrip1;
            this.spContourPlot.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.scContourPlot_Panel2_Paint);
            this.spContourPlot.Size = new System.Drawing.Size(449, 479);
            this.spContourPlot.SplitterDistance = 242;
            this.spContourPlot.SplitterWidth = 11;
            this.spContourPlot.TabIndex = 0;
            // 
            // gb1
            // 
            this.gb1.Controls.Add(this.label16);
            this.gb1.Controls.Add(this.cbZPointQuery);
            this.gb1.Controls.Add(this.label15);
            this.gb1.Controls.Add(this.cbYPointQuery);
            this.gb1.Controls.Add(this.label14);
            this.gb1.Controls.Add(this.label13);
            this.gb1.Controls.Add(this.label12);
            this.gb1.Controls.Add(this.cbFiberQueryPair);
            this.gb1.Controls.Add(this.cbXPointQuery);
            this.gb1.Controls.Add(this.bQueryPoint);
            this.gb1.Location = new System.Drawing.Point(8, 340);
            this.gb1.Name = "gb1";
            this.gb1.Size = new System.Drawing.Size(210, 130);
            this.gb1.TabIndex = 42;
            this.gb1.TabStop = false;
            this.gb1.Text = "Point Query";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(56, 76);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(12, 13);
            this.label16.TabIndex = 48;
            this.label16.Text = "z";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbZPointQuery
            // 
            this.cbZPointQuery.AllowDrop = true;
            this.cbZPointQuery.FormattingEnabled = true;
            this.cbZPointQuery.Items.AddRange(new object[] {
            "Top",
            "Center-Top",
            "Center-Bottom",
            "Bottom"});
            this.cbZPointQuery.Location = new System.Drawing.Point(70, 73);
            this.cbZPointQuery.Name = "cbZPointQuery";
            this.cbZPointQuery.Size = new System.Drawing.Size(95, 21);
            this.cbZPointQuery.TabIndex = 47;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(56, 49);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(12, 13);
            this.label15.TabIndex = 46;
            this.label15.Text = "y";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbYPointQuery
            // 
            this.cbYPointQuery.AllowDrop = true;
            this.cbYPointQuery.FormattingEnabled = true;
            this.cbYPointQuery.Items.AddRange(new object[] {
            "Left",
            "Center",
            "Right"});
            this.cbYPointQuery.Location = new System.Drawing.Point(70, 46);
            this.cbYPointQuery.Name = "cbYPointQuery";
            this.cbYPointQuery.Size = new System.Drawing.Size(95, 21);
            this.cbYPointQuery.TabIndex = 45;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(55, 22);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(12, 13);
            this.label14.TabIndex = 44;
            this.label14.Text = "x";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(3, 103);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(51, 13);
            this.label13.TabIndex = 43;
            this.label13.Text = "Fiber Pair";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(1, 22);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(48, 13);
            this.label12.TabIndex = 42;
            this.label12.Text = "Location";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbFiberQueryPair
            // 
            this.cbFiberQueryPair.AllowDrop = true;
            this.cbFiberQueryPair.FormattingEnabled = true;
            this.cbFiberQueryPair.Location = new System.Drawing.Point(56, 99);
            this.cbFiberQueryPair.Name = "cbFiberQueryPair";
            this.cbFiberQueryPair.Size = new System.Drawing.Size(79, 21);
            this.cbFiberQueryPair.TabIndex = 40;
            // 
            // cbXPointQuery
            // 
            this.cbXPointQuery.AllowDrop = true;
            this.cbXPointQuery.FormattingEnabled = true;
            this.cbXPointQuery.Items.AddRange(new object[] {
            "Front",
            "Center",
            "Back"});
            this.cbXPointQuery.Location = new System.Drawing.Point(70, 19);
            this.cbXPointQuery.Name = "cbXPointQuery";
            this.cbXPointQuery.Size = new System.Drawing.Size(95, 21);
            this.cbXPointQuery.TabIndex = 41;
            // 
            // bQueryPoint
            // 
            this.bQueryPoint.Location = new System.Drawing.Point(145, 102);
            this.bQueryPoint.Name = "bQueryPoint";
            this.bQueryPoint.Size = new System.Drawing.Size(59, 23);
            this.bQueryPoint.TabIndex = 38;
            this.bQueryPoint.Text = "Query";
            this.bQueryPoint.UseVisualStyleBackColor = true;
            this.bQueryPoint.Click += new System.EventHandler(this.bQueryPoint_Click);
            // 
            // cbFiberNumbers
            // 
            this.cbFiberNumbers.BackColor = System.Drawing.Color.Transparent;
            this.cbFiberNumbers.Location = new System.Drawing.Point(105, 212);
            this.cbFiberNumbers.Name = "cbFiberNumbers";
            this.cbFiberNumbers.Size = new System.Drawing.Size(68, 24);
            this.cbFiberNumbers.TabIndex = 39;
            this.cbFiberNumbers.Text = "Fiber #s";
            this.cbFiberNumbers.UseVisualStyleBackColor = false;
            // 
            // cbRotations
            // 
            this.cbRotations.Location = new System.Drawing.Point(15, 214);
            this.cbRotations.Name = "cbRotations";
            this.cbRotations.Size = new System.Drawing.Size(93, 24);
            this.cbRotations.TabIndex = 37;
            this.cbRotations.Text = "Rotations";
            this.cbRotations.UseVisualStyleBackColor = true;
            // 
            // cbShowCrack
            // 
            this.cbShowCrack.Location = new System.Drawing.Point(105, 194);
            this.cbShowCrack.Name = "cbShowCrack";
            this.cbShowCrack.Size = new System.Drawing.Size(116, 24);
            this.cbShowCrack.TabIndex = 36;
            this.cbShowCrack.Text = "Crack";
            this.cbShowCrack.UseVisualStyleBackColor = true;
            // 
            // cbConnections
            // 
            this.cbConnections.Location = new System.Drawing.Point(15, 194);
            this.cbConnections.Name = "cbConnections";
            this.cbConnections.Size = new System.Drawing.Size(93, 24);
            this.cbConnections.TabIndex = 35;
            this.cbConnections.Text = "Connections";
            this.cbConnections.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label11.Location = new System.Drawing.Point(137, 309);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(56, 23);
            this.label11.TabIndex = 34;
            this.label11.Text = "Boundary";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label10.Location = new System.Drawing.Point(137, 282);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 23);
            this.label10.TabIndex = 33;
            this.label10.Text = "Proj Fiber";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label7.Location = new System.Drawing.Point(137, 253);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 23);
            this.label7.TabIndex = 32;
            this.label7.Text = "Fiber";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bBoundaryColor
            // 
            this.bBoundaryColor.Location = new System.Drawing.Point(194, 309);
            this.bBoundaryColor.Name = "bBoundaryColor";
            this.bBoundaryColor.Size = new System.Drawing.Size(24, 23);
            this.bBoundaryColor.TabIndex = 31;
            this.bBoundaryColor.UseVisualStyleBackColor = true;
            this.bBoundaryColor.Click += new System.EventHandler(this.bBoundaryColor_Click);
            // 
            // bFiberColor
            // 
            this.bFiberColor.Location = new System.Drawing.Point(194, 253);
            this.bFiberColor.Name = "bFiberColor";
            this.bFiberColor.Size = new System.Drawing.Size(24, 23);
            this.bFiberColor.TabIndex = 30;
            this.bFiberColor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.bFiberColor.UseVisualStyleBackColor = true;
            this.bFiberColor.Click += new System.EventHandler(this.bFiberColor_Click);
            // 
            // bProjFiberColor
            // 
            this.bProjFiberColor.Location = new System.Drawing.Point(194, 282);
            this.bProjFiberColor.Name = "bProjFiberColor";
            this.bProjFiberColor.Size = new System.Drawing.Size(24, 23);
            this.bProjFiberColor.TabIndex = 29;
            this.bProjFiberColor.UseVisualStyleBackColor = true;
            this.bProjFiberColor.Click += new System.EventHandler(this.bProjFiberColor_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 262);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 23);
            this.label2.TabIndex = 28;
            this.label2.Text = "Matrix Transparancy";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbAutomaticRange
            // 
            this.cbAutomaticRange.Location = new System.Drawing.Point(105, 174);
            this.cbAutomaticRange.Name = "cbAutomaticRange";
            this.cbAutomaticRange.Size = new System.Drawing.Size(116, 24);
            this.cbAutomaticRange.TabIndex = 26;
            this.cbAutomaticRange.Text = "Automatic Range";
            this.cbAutomaticRange.UseVisualStyleBackColor = true;
            this.cbAutomaticRange.CheckedChanged += new System.EventHandler(this.cbAutomaticRange_CheckedChanged);
            // 
            // tbLowRange
            // 
            this.tbLowRange.Location = new System.Drawing.Point(102, 66);
            this.tbLowRange.Name = "tbLowRange";
            this.tbLowRange.Size = new System.Drawing.Size(116, 20);
            this.tbLowRange.TabIndex = 8;
            // 
            // tbHighRange
            // 
            this.tbHighRange.Location = new System.Drawing.Point(102, 38);
            this.tbHighRange.Name = "tbHighRange";
            this.tbHighRange.Size = new System.Drawing.Size(116, 20);
            this.tbHighRange.TabIndex = 6;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(6, 122);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(90, 23);
            this.label9.TabIndex = 25;
            this.label9.Text = "Plot Component";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(6, 94);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(90, 23);
            this.label8.TabIndex = 24;
            this.label8.Text = "Plot Type";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbPlotComponent
            // 
            this.cbPlotComponent.FormattingEnabled = true;
            this.cbPlotComponent.Location = new System.Drawing.Point(102, 124);
            this.cbPlotComponent.Name = "cbPlotComponent";
            this.cbPlotComponent.Size = new System.Drawing.Size(116, 21);
            this.cbPlotComponent.TabIndex = 23;
            this.cbPlotComponent.SelectedIndexChanged += new System.EventHandler(this.CbPlotComponentSelectedIndexChanged);
            // 
            // cbPlotType
            // 
            this.cbPlotType.FormattingEnabled = true;
            this.cbPlotType.Items.AddRange(new object[] {
            "Stress",
            "Strain",
            "Local Displacements",
            "Global Displacements"});
            this.cbPlotType.Location = new System.Drawing.Point(102, 96);
            this.cbPlotType.Name = "cbPlotType";
            this.cbPlotType.Size = new System.Drawing.Size(116, 21);
            this.cbPlotType.TabIndex = 22;
            this.cbPlotType.SelectedIndexChanged += new System.EventHandler(this.CbPlotTypeSelectedIndexChanged);
            // 
            // cbColorSchemes
            // 
            this.cbColorSchemes.FormattingEnabled = true;
            this.cbColorSchemes.Location = new System.Drawing.Point(102, 11);
            this.cbColorSchemes.Name = "cbColorSchemes";
            this.cbColorSchemes.Size = new System.Drawing.Size(116, 21);
            this.cbColorSchemes.TabIndex = 21;
            this.cbColorSchemes.SelectedIndexChanged += new System.EventHandler(this.CbColorSchemesSelectedIndexChanged);
            // 
            // nudIsoLines
            // 
            this.nudIsoLines.Location = new System.Drawing.Point(170, 151);
            this.nudIsoLines.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudIsoLines.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudIsoLines.Name = "nudIsoLines";
            this.nudIsoLines.Size = new System.Drawing.Size(48, 20);
            this.nudIsoLines.TabIndex = 18;
            this.nudIsoLines.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // nudGridPtsX
            // 
            this.nudGridPtsX.Location = new System.Drawing.Point(60, 151);
            this.nudGridPtsX.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudGridPtsX.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudGridPtsX.Name = "nudGridPtsX";
            this.nudGridPtsX.Size = new System.Drawing.Size(48, 20);
            this.nudGridPtsX.TabIndex = 17;
            this.nudGridPtsX.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label6.Location = new System.Drawing.Point(114, 148);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 23);
            this.label6.TabIndex = 12;
            this.label6.Text = "# IsoLines";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label5.Location = new System.Drawing.Point(5, 148);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 23);
            this.label5.TabIndex = 11;
            this.label5.Text = "# Grid Pts";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbIsoLines
            // 
            this.cbIsoLines.Location = new System.Drawing.Point(15, 174);
            this.cbIsoLines.Name = "cbIsoLines";
            this.cbIsoLines.Size = new System.Drawing.Size(84, 24);
            this.cbIsoLines.TabIndex = 10;
            this.cbIsoLines.Text = "Iso Lines";
            this.cbIsoLines.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label4.Location = new System.Drawing.Point(36, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 23);
            this.label4.TabIndex = 9;
            this.label4.Text = "Low Range";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.Location = new System.Drawing.Point(36, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 23);
            this.label3.TabIndex = 7;
            this.label3.Text = "High Range";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 23);
            this.label1.TabIndex = 4;
            this.label1.Text = "Color Scheme";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bHighColor
            // 
            this.bHighColor.Location = new System.Drawing.Point(9, 35);
            this.bHighColor.Name = "bHighColor";
            this.bHighColor.Size = new System.Drawing.Size(24, 23);
            this.bHighColor.TabIndex = 3;
            this.bHighColor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.bHighColor.UseVisualStyleBackColor = true;
            this.bHighColor.Click += new System.EventHandler(this.BHighColorClick);
            // 
            // bLowColor
            // 
            this.bLowColor.Location = new System.Drawing.Point(9, 64);
            this.bLowColor.Name = "bLowColor";
            this.bLowColor.Size = new System.Drawing.Size(24, 23);
            this.bLowColor.TabIndex = 2;
            this.bLowColor.UseVisualStyleBackColor = true;
            this.bLowColor.Click += new System.EventHandler(this.BLowColorClick);
            // 
            // bUpdate
            // 
            this.bUpdate.Location = new System.Drawing.Point(9, 308);
            this.bUpdate.Name = "bUpdate";
            this.bUpdate.Size = new System.Drawing.Size(75, 23);
            this.bUpdate.TabIndex = 0;
            this.bUpdate.Text = "Update";
            this.bUpdate.UseVisualStyleBackColor = true;
            this.bUpdate.Click += new System.EventHandler(this.BUpdateClick);
            // 
            // tbMatrixTransparancy
            // 
            this.tbMatrixTransparancy.BackColor = System.Drawing.SystemColors.Control;
            this.tbMatrixTransparancy.LargeChange = 20;
            this.tbMatrixTransparancy.Location = new System.Drawing.Point(8, 287);
            this.tbMatrixTransparancy.Maximum = 255;
            this.tbMatrixTransparancy.Name = "tbMatrixTransparancy";
            this.tbMatrixTransparancy.Size = new System.Drawing.Size(135, 45);
            this.tbMatrixTransparancy.TabIndex = 27;
            this.tbMatrixTransparancy.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbMatrixTransparancy.Value = 255;
            this.tbMatrixTransparancy.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(103, 26);
            this.contextMenuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip1_ItemClicked);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            // 
            // cbProjections
            // 
            this.cbProjections.Location = new System.Drawing.Point(14, 235);
            this.cbProjections.Name = "cbProjections";
            this.cbProjections.Size = new System.Drawing.Size(93, 24);
            this.cbProjections.TabIndex = 43;
            this.cbProjections.Text = "Projections";
            this.cbProjections.UseVisualStyleBackColor = true;
            // 
            // ContourPlotForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 479);
            this.Controls.Add(this.spContourPlot);
            this.Name = "ContourPlotForm";
            this.Text = "ContourPlotForm";
            this.spContourPlot.Panel1.ResumeLayout(false);
            this.spContourPlot.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spContourPlot)).EndInit();
            this.spContourPlot.ResumeLayout(false);
            this.gb1.ResumeLayout(false);
            this.gb1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudIsoLines)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGridPtsX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbMatrixTransparancy)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        private System.Windows.Forms.ComboBox cbColorSchemes;
        private System.Windows.Forms.Button bUpdate;
        private System.Windows.Forms.Button bLowColor;
        private System.Windows.Forms.Button bHighColor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbHighRange;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbLowRange;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox cbIsoLines;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nudGridPtsX;
        private System.Windows.Forms.NumericUpDown nudIsoLines;
        private System.Windows.Forms.SplitContainer spContourPlot;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbPlotComponent;
        protected System.Windows.Forms.ComboBox cbPlotType;
        private System.Windows.Forms.CheckBox cbAutomaticRange;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar tbMatrixTransparancy;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button bBoundaryColor;
        private System.Windows.Forms.Button bFiberColor;
        private System.Windows.Forms.Button bProjFiberColor;
        private System.Windows.Forms.CheckBox cbConnections;
        private System.Windows.Forms.CheckBox cbShowCrack;
        private System.Windows.Forms.CheckBox cbRotations;
        private System.Windows.Forms.Button bQueryPoint;
        private System.Windows.Forms.CheckBox cbFiberNumbers;
        private System.Windows.Forms.GroupBox gb1;
        private System.Windows.Forms.ComboBox cbFiberQueryPair;
        private System.Windows.Forms.ComboBox cbXPointQuery;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox cbZPointQuery;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cbYPointQuery;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.CheckBox cbProjections;
    }
}
