/*
 * Created by SharpDevelop.
 * User: Scott
 * Date: 2/18/2013
 * Time: 9:05 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Animation
{
	/// <summary>
	/// Description of PanelMethods.
	/// </summary>
	public class PanelMethods
	{
		public PanelMethods()
		{
		}
		public static void ZoomToFit(Panel panel, ref float ScaleFactor,
		                             ref float TranslateX, ref float TranslateY,
		                             float xMin, float yMin, float xMax, float yMax)
		{
			float LowerLimit = yMin;
			float LeftLimit = xMin;
			float UpperLimit = yMax;
			float RightLimit =xMax;
			
			float totalHeight = Math.Abs(UpperLimit - LowerLimit);
			float totalWidth = Math.Abs(RightLimit - LeftLimit);
			
			float fJointRatio = totalHeight/totalWidth;
			float fPanelRatio = (float)panel.Height/(float)panel.Width;
			
			if (fJointRatio < fPanelRatio) {
				//Base the ScaleFactor on the width
				ScaleFactor = (float)(panel.Width/((11.0/10.0)*totalWidth));
				
				TranslateX = (float)(panel.Width/20.0 - LeftLimit*ScaleFactor);
				TranslateY = (float)((panel.Height - ScaleFactor*totalHeight)/2 + UpperLimit*ScaleFactor);
			}
			else{
				//Base the ScaleFactor on the height
				ScaleFactor = (float)(panel.Height/((11.0/10.0)*totalHeight));
				TranslateY = (float)(panel.Height/20.0 + ScaleFactor*UpperLimit);
				TranslateX = (float)((panel.Width - ScaleFactor*totalWidth)/2 - LeftLimit*ScaleFactor);
			}
		}
		
		public static void ZoomToPoint(PointF location, float factor, ref float ScaleFactor,
		                               ref float TranslateX, ref float TranslateY)
		{
			/// <summary>
			/// This takes in a location and factor about which to zoom the graphic
			/// </summary>
			
			PointF[] PageLocation = new PointF[1]{location};
			
			//Now, transform the mouse location from page to world coor
			
			Matrix pageToWorld = new Matrix(1F,0F,0F,-1F,0F,0F); //reflection
			pageToWorld.Scale(ScaleFactor, ScaleFactor, MatrixOrder.Append);
			pageToWorld.Translate(TranslateX, TranslateY, MatrixOrder.Append);
			
			pageToWorld.Invert();
			
			pageToWorld.TransformPoints(PageLocation);
			
			//Now, change the scale factor
			ScaleFactor = factor * ScaleFactor;
			
			//the scale factor is now changed, so retransfer
			Matrix worldToPage = new Matrix(1F,0F,0F,-1F,0F,0F); //reflection
			worldToPage.Scale(ScaleFactor, ScaleFactor, MatrixOrder.Append);
			worldToPage.Translate(TranslateX, TranslateY, MatrixOrder.Append);
			
			worldToPage.TransformPoints(PageLocation);
			
			//Now, find the difference between the mouse location and scaled location
			
			TranslateX += location.X - PageLocation[0].X;
			TranslateY += location.Y - PageLocation[0].Y;
			
		}
	}
}
