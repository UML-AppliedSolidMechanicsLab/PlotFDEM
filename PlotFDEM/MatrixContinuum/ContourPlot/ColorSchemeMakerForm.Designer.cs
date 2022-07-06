/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 18-Dec-12
 * Time: 8:47 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace PlotFDEM.MatrixContinuum.ContourPlot
{
    partial class ColorSchemeMakerForm
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
            this.bAdd = new System.Windows.Forms.Button();
            this.bRemove = new System.Windows.Forms.Button();
            this.bMoveUp = new System.Windows.Forms.Button();
            this.bMoveDown = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.bOK = new System.Windows.Forms.Button();
            this.lbColors = new System.Windows.Forms.ListBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // bAdd
            // 
            this.bAdd.Location = new System.Drawing.Point(98, 36);
            this.bAdd.Name = "bAdd";
            this.bAdd.Size = new System.Drawing.Size(75, 23);
            this.bAdd.TabIndex = 0;
            this.bAdd.Text = "Add";
            this.bAdd.UseVisualStyleBackColor = true;
            this.bAdd.Click += new System.EventHandler(this.BAddClick);
            // 
            // bRemove
            // 
            this.bRemove.Location = new System.Drawing.Point(98, 65);
            this.bRemove.Name = "bRemove";
            this.bRemove.Size = new System.Drawing.Size(75, 23);
            this.bRemove.TabIndex = 1;
            this.bRemove.Text = "Remove";
            this.bRemove.UseVisualStyleBackColor = true;
            this.bRemove.Click += new System.EventHandler(this.BRemoveClick);
            // 
            // bMoveUp
            // 
            this.bMoveUp.Location = new System.Drawing.Point(98, 94);
            this.bMoveUp.Name = "bMoveUp";
            this.bMoveUp.Size = new System.Drawing.Size(75, 23);
            this.bMoveUp.TabIndex = 2;
            this.bMoveUp.Text = "Move Up";
            this.bMoveUp.UseVisualStyleBackColor = true;
            this.bMoveUp.Click += new System.EventHandler(this.BMoveUpClick);
            // 
            // bMoveDown
            // 
            this.bMoveDown.Location = new System.Drawing.Point(98, 123);
            this.bMoveDown.Name = "bMoveDown";
            this.bMoveDown.Size = new System.Drawing.Size(75, 23);
            this.bMoveDown.TabIndex = 3;
            this.bMoveDown.Text = "Move Down";
            this.bMoveDown.UseVisualStyleBackColor = true;
            this.bMoveDown.Click += new System.EventHandler(this.BMoveDownClick);
            // 
            // bCancel
            // 
            this.bCancel.Location = new System.Drawing.Point(98, 250);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(75, 23);
            this.bCancel.TabIndex = 6;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.BCancelClick);
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(98, 221);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(75, 23);
            this.bOK.TabIndex = 5;
            this.bOK.Text = "OK";
            this.bOK.UseVisualStyleBackColor = true;
            this.bOK.Click += new System.EventHandler(this.BOKClick);
            // 
            // lbColors
            // 
            this.lbColors.FormattingEnabled = true;
            this.lbColors.Location = new System.Drawing.Point(13, 36);
            this.lbColors.Name = "lbColors";
            this.lbColors.Size = new System.Drawing.Size(79, 238);
            this.lbColors.TabIndex = 7;
            this.lbColors.DoubleClick += new System.EventHandler(this.lbColorsDoubleClick);
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(13, 10);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(160, 20);
            this.tbName.TabIndex = 8;
            this.tbName.TextChanged += new System.EventHandler(this.TbNameTextChanged);
            // 
            // ColorSchemeMakerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(186, 283);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.lbColors);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bOK);
            this.Controls.Add(this.bMoveDown);
            this.Controls.Add(this.bMoveUp);
            this.Controls.Add(this.bRemove);
            this.Controls.Add(this.bAdd);
            this.Name = "ColorSchemeMakerForm";
            this.Text = "ColorSchemeMakerForm";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Button bOK;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.ListBox lbColors;
        private System.Windows.Forms.Button bMoveDown;
        private System.Windows.Forms.Button bMoveUp;
        private System.Windows.Forms.Button bRemove;
        private System.Windows.Forms.Button bAdd;
    }
}
