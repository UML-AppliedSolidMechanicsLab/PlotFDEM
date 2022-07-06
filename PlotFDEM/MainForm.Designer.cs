/*
 * Created by SharpDevelop.
 * User: IFAM
 * Date: 11-Nov-14
 * Time: 4:15 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace PlotFDEM
{
	partial class MainForm
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
            this.bContactOnly = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.bFVel = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.bFiberOnly = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.bSizingPlot = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.bMatrixContinuumPlot = new System.Windows.Forms.Button();
            this.bPackPlot = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.bConnections = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bContactOnly
            // 
            this.bContactOnly.Location = new System.Drawing.Point(21, 58);
            this.bContactOnly.Name = "bContactOnly";
            this.bContactOnly.Size = new System.Drawing.Size(75, 23);
            this.bContactOnly.TabIndex = 0;
            this.bContactOnly.Text = "New Plot";
            this.bContactOnly.UseVisualStyleBackColor = true;
            this.bContactOnly.Click += new System.EventHandler(this.BContactOnlyClick);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(103, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Contact Force Plot";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(103, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 3;
            this.label2.Text = "Velocity Vector Plot";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(21, 87);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "New Plot";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.BVelVectorClik);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(103, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 23);
            this.label3.TabIndex = 5;
            this.label3.Text = "Velocity Fiber Plot";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // bFVel
            // 
            this.bFVel.Location = new System.Drawing.Point(21, 116);
            this.bFVel.Name = "bFVel";
            this.bFVel.Size = new System.Drawing.Size(75, 23);
            this.bFVel.TabIndex = 4;
            this.bFVel.Text = "New Plot";
            this.bFVel.UseVisualStyleBackColor = true;
            this.bFVel.Click += new System.EventHandler(this.BFVelClick);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(103, 145);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 23);
            this.label4.TabIndex = 7;
            this.label4.Text = "Fiber Only";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // bFiberOnly
            // 
            this.bFiberOnly.Location = new System.Drawing.Point(21, 145);
            this.bFiberOnly.Name = "bFiberOnly";
            this.bFiberOnly.Size = new System.Drawing.Size(75, 23);
            this.bFiberOnly.TabIndex = 6;
            this.bFiberOnly.Text = "New Plot";
            this.bFiberOnly.UseVisualStyleBackColor = true;
            this.bFiberOnly.Click += new System.EventHandler(this.BFiberOnlyClick);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(103, 172);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 23);
            this.label5.TabIndex = 9;
            this.label5.Text = "Sizing Plot";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // bSizingPlot
            // 
            this.bSizingPlot.Location = new System.Drawing.Point(21, 172);
            this.bSizingPlot.Name = "bSizingPlot";
            this.bSizingPlot.Size = new System.Drawing.Size(75, 23);
            this.bSizingPlot.TabIndex = 8;
            this.bSizingPlot.Text = "New Plot";
            this.bSizingPlot.UseVisualStyleBackColor = true;
            this.bSizingPlot.Click += new System.EventHandler(this.BSizingPlotClick);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(103, 201);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(127, 23);
            this.label6.TabIndex = 10;
            this.label6.Text = "Matrix Continuum Plot";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // bMatrixContinuumPlot
            // 
            this.bMatrixContinuumPlot.Location = new System.Drawing.Point(22, 201);
            this.bMatrixContinuumPlot.Name = "bMatrixContinuumPlot";
            this.bMatrixContinuumPlot.Size = new System.Drawing.Size(75, 23);
            this.bMatrixContinuumPlot.TabIndex = 11;
            this.bMatrixContinuumPlot.Text = "New Plot";
            this.bMatrixContinuumPlot.UseVisualStyleBackColor = true;
            this.bMatrixContinuumPlot.Click += new System.EventHandler(this.bMatrixContinuumPlot_Click);
            // 
            // bPackPlot
            // 
            this.bPackPlot.Location = new System.Drawing.Point(22, 231);
            this.bPackPlot.Name = "bPackPlot";
            this.bPackPlot.Size = new System.Drawing.Size(75, 23);
            this.bPackPlot.TabIndex = 13;
            this.bPackPlot.Text = "New Plot";
            this.bPackPlot.UseVisualStyleBackColor = true;
            this.bPackPlot.Click += new System.EventHandler(this.bPackPlot_Click);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(103, 231);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(127, 23);
            this.label7.TabIndex = 12;
            this.label7.Text = "Pack Plot";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // bConnections
            // 
            this.bConnections.Location = new System.Drawing.Point(166, 231);
            this.bConnections.Name = "bConnections";
            this.bConnections.Size = new System.Drawing.Size(75, 23);
            this.bConnections.TabIndex = 14;
            this.bConnections.Text = "Connections";
            this.bConnections.UseVisualStyleBackColor = true;
            this.bConnections.Click += new System.EventHandler(this.bConnections_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.bConnections);
            this.Controls.Add(this.bPackPlot);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.bMatrixContinuumPlot);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.bSizingPlot);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.bFiberOnly);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.bFVel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bContactOnly);
            this.Name = "MainForm";
            this.Text = "Main";
            this.ResumeLayout(false);

		}
		private System.Windows.Forms.Button bContactOnly;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button bFVel;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button bFiberOnly;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button bSizingPlot;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button bMatrixContinuumPlot;
        private System.Windows.Forms.Button bPackPlot;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button bConnections;
    }
}
