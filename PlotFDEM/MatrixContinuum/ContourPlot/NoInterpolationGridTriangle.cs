/*
 * Created by SharpDevelop.
 * User: scott_stapleton
 * Date: 6/27/2018
 * Time: 2:34 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;

namespace JointElement.PostProcessor.Plots.ContourPlot
{
	/// <summary>
	/// Description of NoInterpolationGridTriangle.
	/// </summary>
	public class NoInterpolationGridTriangle: GridTriangle
	{
		public NoInterpolationGridTriangle(PlotPoint pt1, PlotPoint pt2, PlotPoint pt3): base(pt1, pt2, pt3)
		{
		}
		public override void FindLinesAndLevels(int [] levelOfPoints, double [] levels){
			
			
			int [] iPts = new int[4]{0,1,2,0};
			//Create a list of the isoregion and isoline points for each level, and instantiate
			List<PointF> [] lPointsAtLevels = new List<PointF>[levels.Length+1];
			List<PointF> [] lIsoLinePtsAtLevels = new List<PointF>[levels.Length+1];
			
			for (int i = 0; i < lPointsAtLevels.Length; i++) {
				lPointsAtLevels[i] = new List<PointF>();
				lIsoLinePtsAtLevels[i] = new List<PointF>();
			}
			
			//Now, loop through in between points and add a point between the types
			for (int i = 0; i < iPts.Length-1; i++) {
				int i1 = iPts[i];
				int i2 = iPts[i+1];
				
				//Add the actual point itself
				lPointsAtLevels[levelOfPoints[i1]].Add(pts[i1].point);
				
				if (levelOfPoints[i1] != levelOfPoints[i2]) {
					
					#region Add a point in the middle if they are different
					PointF tempPt = FindMidpointBetweenPoints(i1,i2);
					lPointsAtLevels[levelOfPoints[i1]].Add(tempPt);
					lPointsAtLevels[levelOfPoints[i2]].Add(tempPt);
					lIsoLinePtsAtLevels[levelOfPoints[i1]].Add(tempPt);
					
					#endregion
					
				}
			}
			
			//If all three corners are different levels, add a point in the middle to tri-sect the triangle
			if (levelOfPoints[0] != levelOfPoints[1] && levelOfPoints[1] != levelOfPoints[2] && levelOfPoints[2] != levelOfPoints[0]) {
				PointF middle = base.FindTriangleCenterPoint();
				try {
					lPointsAtLevels[levelOfPoints[0]].Insert(2,middle);
					lPointsAtLevels[levelOfPoints[1]].Add(middle);
					lPointsAtLevels[levelOfPoints[2]].Add(middle);
					lIsoLinePtsAtLevels[levelOfPoints[0]].Insert(1,middle);
					lIsoLinePtsAtLevels[levelOfPoints[1]].Insert(1,middle);
					lIsoLinePtsAtLevels[levelOfPoints[2]].Insert(1,middle);
				} catch (Exception) {
					
					throw;
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
}