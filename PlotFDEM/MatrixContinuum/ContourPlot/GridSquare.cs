/*
 * Created by SharpDevelop.
 * User: sstaple
 * Date: 8/19/2011
 * Time: 11:24 AM
 * 
 */
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace PlotFDEM.MatrixContinuum.ContourPlot
{
	/// <summary>
	/// Description of GridSquare.
	/// </summary>
	public class GridSquare
	{
		protected PlotPoint [] pts = new PlotPoint[4];
		PlotPoint Middle;
		protected double [] levels;
		protected int [] LevelOfPts = new int[5]; //The int tells what level the layer is Below (0=below first level, 1 = below 2nd level, etc)
		protected GridTriangle [] Triangles = new GridTriangle[4];
		
		public GridSquare(){}
		public GridSquare(PlotPoint UpperLeft, PlotPoint UpperRight, PlotPoint LowerRight, PlotPoint LowerLeft, double [] Levels)
		{
			pts[0] = UpperLeft;
			pts[1] = UpperRight;
			pts[2] = LowerRight;
			pts[3] = LowerLeft;
			Middle = FindCenterPoint();
			levels = Levels;
			
			//Assign a level to each point
			AssignLevelsToPoints();
			
			//Break up into triangles and find the lines and regions to color
			for (int i = 0; i < pts.Length - 1; i++) {
				Triangles[i] = new GridTriangle(pts[i], pts[i+1], Middle);
				Triangles[i].FindLinesAndLevels(new int[3]{LevelOfPts[i], LevelOfPts[i+1], LevelOfPts[4]}, levels);
			}
			Triangles[3] = new GridTriangle(pts[0], pts[3], Middle);
			Triangles[3].FindLinesAndLevels(new int[3]{LevelOfPts[0], LevelOfPts[3], LevelOfPts[4]}, levels);
			
		}
		
		public void ColorGrid(Graphics graphic, Color [] LevelColors, Matrix myTransform)
        {
			//graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
			//graphic.SmoothingMode = SmoothingMode.HighQuality;
			
			foreach (GridTriangle tri in Triangles) {
				for (int i = 0; i < tri.IsoLevelRegions.Length; i++) {
					PointF [] tempPoints = new PointF[tri.IsoLevelRegions[i].Points.Length];
					for (int j = 0; j < tempPoints.Length; j++) { //make a deep copy
						tempPoints[j] = new PointF(tri.IsoLevelRegions[i].Points[j].X, tri.IsoLevelRegions[i].Points[j].Y);
					}

                    myTransform.TransformPoints(tempPoints);
                   
                    try {
						Brush tempBrush = new SolidBrush(LevelColors[tri.IsoLevelRegions[i].Level]);
						graphic.FillPolygon(tempBrush, tempPoints);
						tempBrush.Dispose();
					} catch (Exception ex) {
						
						throw ex;
					}
					
				}
			}
			
		}
		
		public void PaintIsoLines(Graphics graphic, Matrix myTransform, Pen pen){
			//graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
			//graphic.SmoothingMode = SmoothingMode.HighQuality;
			
			foreach (GridTriangle tri in Triangles) {
				foreach (IsoLine iL in tri.Lines) {
					object tempPoints =  iL.Points.Clone();
					PointF [] tempPointFs = (PointF [])tempPoints;

                    myTransform.TransformPoints(tempPointFs);

                    graphic.DrawLines(pen, tempPointFs);
				}
			}
		}
		
		#region PrivateMethods
		
		protected PlotPoint FindCenterPoint(){
			float [] y = new float[pts.Length];
			float [] x = new float[pts.Length];
			float [] z = new float[pts.Length];
			for (int i = 0; i < pts.Length; i++) {
				y[i] = pts[i].point.Y;
				x[i] = pts[i].point.X;
				z[i] = (float)pts[i].z;
			}

			//Try just using midpoints and averages 9/2021
			float xMiddle = (x[0] + x[1] + x[2] + x[3]) / 4.0f;
			float yMiddle = (y[0] + y[1] + y[2] + y[3]) / 4.0f;
			double zMiddle = (z[0] + z[1] + z[2] + z[3]) / 4.0f;

			return new PlotPoint(new PointF(xMiddle, yMiddle), zMiddle);
		}
		
		protected virtual void AssignLevelsToPoints(){
			
			for (int i = 0; i < pts.Length; i++) {
				for (int j = 0; j < levels.Length; j++) {
					if (pts[i].z >= levels[j]) {
						LevelOfPts[i] += 1;
					}
				}
			}
			//Now check the middle Pt
			for (int j = 0; j < levels.Length; j++) {
				if (Middle.z >= levels[j]) {
					LevelOfPts[4] += 1;
				}
			}
			
		}
		
		#endregion
	}
	public class PlotPoint{
		public PointF point;
		public double z;
		
		public PlotPoint(PointF location, double elevation){
			z = elevation;
			point = location;
		}
	}
}
