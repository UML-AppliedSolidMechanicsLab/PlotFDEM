/*
 * Created by SharpDevelop.
 * User: IFAM
 * Date: 12-Nov-14
 * Time: 2:07 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace PlotFDEM
{
	/// <summary>
	/// Description of CreatePlot.
	/// </summary>
	public class CreatePlot:Animation.IDrawGraphic
	{
		private List<Fiber> lFibers;
		private List<Fiber []> lProjFibers;
		private Boundary boundary;
		private List<Contact> lContacts;
		private List<Contact> lSizing;
		private StressStrain results;
		private bool bShowContact;
		private bool bShowSizing;
		private bool bShowVelocity;
		private bool bColorFiberVel;
		Animation.AnimatedPlot myAnimation;
		public double maxSizingForce;
		public double maxContactForce;

        //Do I need these for anything?  I don't think so.....
        public float ScaleFactor = 1f;
        public float TranslateX;
        public float TranslateY;
		

        public CreatePlot(bool showContact, bool showVelocity, bool colorFiberVel, bool showSizing)
		{
			bShowContact = showContact;
			bShowSizing = showSizing;
			bShowVelocity = showVelocity;
			bColorFiberVel = colorFiberVel;
			//Read CSV
			ReadCSV myData = new ReadCSV();
			lFibers = myData.lFibers;
			lContacts = myData.lContacts;
			lSizing = myData.lSizing;
			boundary = myData.boundary;
			lProjFibers = myData.lProjFibers;
			results = myData.results;
			maxSizingForce = myData.maxSizingForce;
			maxContactForce = myData.maxContactForce;
			
			
			//Get the boundaries
			float[] corners = boundary.FindExtremeCorners();
			myAnimation = new Animation.AnimatedPlot(this, results.lPointList, results.Labels, myData.fileName, "Iterations", "Stress/Strain",
			                                                                corners[0], corners[1], corners[2], corners[3]);
			//Find the maximum velocity for all fibers at all time steps
			if (bShowVelocity||bColorFiberVel) {
				foreach (Fiber f in lFibers) {
					f.findMaxVel();
				}
			}
		}
		public void Draw(Graphics graphic, int i){
			
			//Color the background
			//graphic.Clear( Color.FromArgb(230,47,214,63));
			
			if (bShowSizing) {
				foreach (Contact sp in lSizing) {
					//sp.ContactScaleFactor = (Max(homogenizedStress) / lFibers.Count * cBoundary.ODimensions[1] * cBoundary.ODimensions[2]*200);//TODO Get some scaling method that makes more sense Math.Abs(homogenizedStress.Max());
					sp.Draw(graphic, i, maxSizingForce); //Add transform???
				}
			}
			
			if (bShowContact) {
				foreach (Contact sp in lContacts) {
					//sp.ContactScaleFactor = (Max(homogenizedStress) / lFibers.Count * cBoundary.ODimensions[1] * cBoundary.ODimensions[2]*200);//TODO Get some scaling method that makes more sense Math.Abs(homogenizedStress.Max());
					sp.Draw(graphic, i, maxContactForce); //Add transform???
				}
			}
			
			//draw the fibers
			foreach (Fiber[] fi in lProjFibers) {
				foreach (Fiber f in fi) {
					f.Draw(graphic, i, Color.FromArgb(230,50, 60, 60));//DarkerGray//220,220,220));//50, 60, 60));//DarkerGray //Color.DodgerBlue); //Add transform???
				}
			}
			foreach (Fiber fi in lFibers) {
				fi.Draw(graphic, i, Color.FromArgb(230,80, 90, 90));//dark grey//240,240,240)); //Color.DodgerBlue); //Add transform???
			}
			
			
			if (bShowVelocity) {
				foreach (Fiber f in lFibers) {
					f.DrawVelocity(graphic, i);
				}
			}
			
			if (bColorFiberVel) {
				foreach (Fiber fi in lFibers) {
					fi.DrawVelFiber(graphic, i);
				}
			}
			
			boundary.Draw(graphic, i, Color.White); //Add transform???
			
		}

		public bool IsClosed()
        {
			return myAnimation.IsDisposed;
		}
	}
	
}
