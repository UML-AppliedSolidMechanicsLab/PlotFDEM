/*
 * Created by SharpDevelop.
 * User: IFAM
 * Date: 11-Nov-14
 * Time: 4:15 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using Animation;
using System.Collections.Generic;
using System.Threading;
using System.Globalization; //to set the language
using PlotFDEM.MatrixContinuum.ContourPlot;

namespace PlotFDEM
{
	/// <summary>
	/// Description of Main.
	/// </summary>
	public partial class MainForm : Form
	{
		List<IDrawGraphic> lCreatePlot = new List<IDrawGraphic>();
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void BContactOnlyClick(object sender, EventArgs e)
		{
			CheckIfAnyWindowsClosed();
			lCreatePlot.Add(new CreatePlot(true, false, false, false));
		}
		void BVelVectorClik(object sender, EventArgs e)
		{
			CheckIfAnyWindowsClosed();
			lCreatePlot.Add(new CreatePlot(false, true, false, false));
		}
		void BFVelClick(object sender, EventArgs e)
		{
			CheckIfAnyWindowsClosed();
			lCreatePlot.Add(new CreatePlot(false, false, true, false));
		}
		void BFiberOnlyClick(object sender, EventArgs e)
		{
			CheckIfAnyWindowsClosed();
			lCreatePlot.Add(new CreatePlot(false, false, false, false));
		}
		void BSizingPlotClick(object sender, EventArgs e)
		{
			CheckIfAnyWindowsClosed();
			lCreatePlot.Add(new CreatePlot(false, false, false, true));
		}

        private void bMatrixContinuumPlot_Click(object sender, EventArgs e)
        {
			CheckIfAnyWindowsClosed();
			ContourPlotForm myCPForm = new ContourPlotForm();
            lCreatePlot.Add(myCPForm.myPlot);
        }
		private void bPackPlot_Click(object sender, EventArgs e)
		{
			CheckIfAnyWindowsClosed();
			PackPlot packPlot = new PackPlot();
		}
		private void bConnections_Click(object sender, EventArgs e)
		{
			CheckIfAnyWindowsClosed();
			ConnectionPlot conPlot = new ConnectionPlot();
		}

		/// <summary>
		/// This method is to close up and dispose of any plots that have been closed already.  
		/// I am hoping that this helps the memory leak buildup problem, though I may have to do more
		/// inside of "create Plot"...
		/// </summary>
		private void CheckIfAnyWindowsClosed()
        {
            for (int i = 0; i < lCreatePlot.Count; i++)
            {
				if (lCreatePlot[i].IsClosed())
                {
					//Does this invoke the garbage collection to kill the whole thing?
					//Garbage collection takes a while, but seems to evevntually get there....sometimes
					lCreatePlot[i] = null;
					lCreatePlot.RemoveAt(i);
					i--;
				}
			}
        }

        
    }
}
