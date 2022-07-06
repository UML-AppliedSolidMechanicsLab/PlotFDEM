/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 18-Dec-12
 * Time: 8:47 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace PlotFDEM.MatrixContinuum.ContourPlot
{
	/// <summary>
	/// Description of ColorSchemeMakerForm.
	/// </summary>
    public partial class ColorSchemeMakerForm : Form
	{
		#region members
		private List<Color> lColors = new List<Color>();
		private string name = "SchemeName";
		private bool bSuccess = false;
		
		public string SchemeName {
			get { return name; }
		}
		public List<Color> LColors {
			get { return lColors; }
		}
		public bool Success {
			get { return bSuccess; }
		}
		
		#endregion
		
		#region Constructors
		public ColorSchemeMakerForm()
		{
			lColors = new List<Color>();
			lColors.Add(Color.Red);
			lColors.Add(Color.White);
			
			Initialize();
		}
		public ColorSchemeMakerForm(Color [] colors, string schemeName)
		{
			lColors = colors.ToList();
			name = schemeName;
			Initialize();
		}
		private void Initialize(){
			InitializeComponent();
			UpdateListBox();
			Activate();
			ShowDialog();
		}
		
		#endregion
		
		#region Form Controls
		void BAddClick(object sender, EventArgs e)
		{
			Color tempColor = ChoseColor();
			lColors.Add(tempColor);
			UpdateListBox();
		}
		
		void BRemoveClick(object sender, EventArgs e)
		{
			if (lbColors.SelectedIndex != -1) {
				lColors.RemoveAt(lbColors.SelectedIndex);
				UpdateListBox();
			}
		}
		
		void BMoveUpClick(object sender, EventArgs e)
		{
			if (lbColors.SelectedIndex > 0) {
				Color temp = lColors[lbColors.SelectedIndex];
				lColors.RemoveAt(lbColors.SelectedIndex);
				lColors.Insert(lbColors.SelectedIndex-1, temp);
				UpdateListBox();
			}
		}
		
		void BMoveDownClick(object sender, EventArgs e)
		{
			if (lbColors.SelectedIndex != -1 && lbColors.SelectedIndex < (lColors.Count - 1)) {
				Color temp = lColors[lbColors.SelectedIndex];
				lColors.RemoveAt(lbColors.SelectedIndex);
				lColors.Insert(lbColors.SelectedIndex+1, temp);
				UpdateListBox();
			}
		}
		
		void BOKClick(object sender, EventArgs e)
		{
			bSuccess = true;
			this.Close();
		}
		
		void BCancelClick(object sender, EventArgs e)
		{
			this.Close();
		}
		
		void lbColorsDoubleClick(object sender, EventArgs e)
		{
			Color tempColor = ChoseColor();
			lColors[lbColors.SelectedIndex] = tempColor;
			UpdateListBox();
		}
		
		void TbNameTextChanged(object sender, EventArgs e)
		{
			name = tbName.Text;
			UpdateListBox();
		}
		#endregion
	
		#region Private Methods
		private Color ChoseColor(){
			//add a color dialogue box
			ColorDialog myColor = new ColorDialog();
			
			myColor.AllowFullOpen = true;
			myColor.AnyColor = true;
			myColor.SolidColorOnly = true;
			
			if (myColor.ShowDialog() == DialogResult.OK)
			{
				return myColor.Color;
			}
			else{return Color.White;}
		}
		
		private void UpdateListBox(){
			
			tbName.Text = name;
			lbColors.Items.Clear();
			
			foreach (Color col in lColors) {
				
				lbColors.Items.Add(col.ToString());
			}
		}
		#endregion
		
		
	}
}
