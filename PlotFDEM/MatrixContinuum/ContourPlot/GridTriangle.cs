/*
 * Created by SharpDevelop.
 * User: sstaple
 * Date: 8/19/2011
 * Time: 11:58 AM
 * 
 */
using System;
using System.Drawing;
using System.Collections.Generic;

namespace PlotFDEM.MatrixContinuum.ContourPlot
{
	/// <summary>
	/// Description of BiLinearApproximation.
	/// </summary>
	public class GridTriangle
	{
		protected float [] y;
		protected float [] x;
		protected float [] z;
		public IsoLevelRegion [] IsoLevelRegions;
		public IsoLine [] Lines;
		protected PlotPoint [] pts;
		
		public GridTriangle(PlotPoint pt1, PlotPoint pt2, PlotPoint pt3){
			pts = new PlotPoint[3]{pt1, pt2, pt3};
			y = new float[pts.Length];
			x = new float[pts.Length];
			z = new float[pts.Length];
			
			//separate out into x, y, and z arrays
			for (int i = 0; i < pts.Length; i++) {
				y[i] = pts[i].point.Y;
				x[i] = pts[i].point.X;
				z[i] = (float)pts[i].z;
			}
		}
		
		protected PlotPoint FindLocationBetweenPoints(double elevation, int n1, int n2){
			float factor = float.Equals(z[n1], z[n2]) ? 0f : ((float)elevation - z[n1]) / (z[n2] - z[n1]);
			float xnew = (1f - factor) * x[n1] + factor * x[n2];
			float ynew = (1f - factor) * y[n1] + factor * y[n2];

			return new PlotPoint(new PointF(xnew, ynew), elevation);
		}
		
		protected PointF FindMidpointBetweenPoints(int n1, int n2){
			float xnew = (float)((x[n2] + x[n1]) / 2.0);
			float ynew =(float)((y[n2] + y[n1]) / 2.0);
			
			return new PointF(xnew, ynew);
		}
		
		protected PointF FindTriangleCenterPoint(){
			float xc = (x[1] + x[0] + x[2]) / 3f;
			float yc = (y[1] + y[0] + y[2]) / 3f;
			return new PointF(xc, yc);

		}
		
		public virtual void FindLinesAndLevels(int [] levelOfPoints, double [] levels){
			
			int [] iPts = new int[4]{0,1,2,0};
			//Create a list of the isoregion and isoline points for each level, and instantiate
			List<PointF> [] lPointsAtLevels = new List<PointF>[levels.Length+1];
			List<PointF> [] lIsoLinePtsAtLevels = new List<PointF>[levels.Length+1];
			
			for (int i = 0; i < lPointsAtLevels.Length; i++) {
				lPointsAtLevels[i] = new List<PointF>();
				lIsoLinePtsAtLevels[i] = new List<PointF>();
			}
			
			//Now, loop through in between points and add points of each type (Warning, this may give error if the same point is there twice)
			for (int i = 0; i < iPts.Length-1; i++) {
				int i1 = iPts[i];
				int i2 = iPts[i+1];
				//Add first point
				lPointsAtLevels[levelOfPoints[i1]].Add(pts[i1].point);
				
				if (levelOfPoints[i1] != levelOfPoints[i2]) {
					
					#region For gradient between poionts with interpolation
					
					 //Two avenues
					 if (levelOfPoints[i1] < levelOfPoints[i2]) {
						for (int j = levelOfPoints[i1]; j < levelOfPoints[i2]; j++) {
							
							PlotPoint tempPt = FindLocationBetweenPoints(levels[j],i1,i2);
							lPointsAtLevels[j].Add(tempPt.point);
							lPointsAtLevels[j+1].Add(tempPt.point);
							lIsoLinePtsAtLevels[j].Add(tempPt.point);
						}
					}
					else{
						for (int j = levelOfPoints[i1]-1; j > levelOfPoints[i2]-1; j--) {
							
							PlotPoint tempPt = FindLocationBetweenPoints(levels[j],i1,i2);
							lPointsAtLevels[j].Add(tempPt.point);
							lPointsAtLevels[j+1].Add(tempPt.point);
							lIsoLinePtsAtLevels[j].Add(tempPt.point);
						}
					}
					
					#endregion
					
				}
			}
			
			//Now, load the isoregions
			List<IsoLevelRegion> lRegions = new List<IsoLevelRegion>();
			List<IsoLine> lLines = new List<IsoLine>();
			for (int i = 0; i < lPointsAtLevels.Length; i++) {
				if (lPointsAtLevels[i].Count > 2) {
					lRegions.Add(new IsoLevelRegion(lPointsAtLevels[i],i));
				}
				if (lIsoLinePtsAtLevels[i].Count > 1) {
					lLines.Add(new IsoLine(lIsoLinePtsAtLevels[i].ToArray()));
				}
			}
			IsoLevelRegions = lRegions.ToArray();
			Lines = lLines.ToArray();
		}
	}
	
	public class IsoLevelRegion{
		public PointF [] Points;
		public int Level;
		public IsoLevelRegion(PointF p1, PointF p2, PointF p3, int level){
			Points = new PointF[3];
			Points[0] = p1;
			Points[1] = p2;
			Points[2] = p3;
			Level = level;
		}
		public IsoLevelRegion(PointF p1, PointF p2, PointF p3, PointF p4, int level){
			Points = new PointF[4];
			Points[0] = p1;
			Points[1] = p2;
			Points[2] = p3;
			Points[3] = p4;
			Level = level;
		}
		public IsoLevelRegion(List<PointF> lPoints, int level){
			Points = lPoints.ToArray();
			Level = level;
		}
	}
	public class IsoLine{
		public PointF [] Points;
		public IsoLine(PointF [] pts){
			Points = pts;
		}
	}
}

