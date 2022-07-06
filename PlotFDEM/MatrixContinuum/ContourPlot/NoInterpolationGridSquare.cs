/*
 * Created by SharpDevelop.
 * User: scott_stapleton
 * Date: 6/27/2018
 * Time: 2:00 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace JointElement.PostProcessor.Plots.ContourPlot
{
	/// <summary>
	/// Description of NoInterpolationGridSquare.
	/// </summary>
	public class NoInterpolationGridSquare: GridSquare
	{
		public NoInterpolationGridSquare(PlotPoint UpperLeft, PlotPoint UpperRight, PlotPoint LowerRight, PlotPoint LowerLeft, double [] Levels):base()
		{
			pts[0] = UpperLeft;
			pts[1] = UpperRight;
			pts[2] = LowerRight;
			pts[3] = LowerLeft;
			levels = Levels;
			
			//Assign a level to each point
			AssignLevelsToPoints();
			
			//Break up into two triangles and find the lines and regions to color
			Triangles = new GridTriangle[2];
			
			Triangles[0] = new NoInterpolationGridTriangle(pts[0], pts[1], pts[2]);
			Triangles[0].FindLinesAndLevels(new int[3]{LevelOfPts[0], LevelOfPts[1], LevelOfPts[2]}, levels);
			
			Triangles[1] = new NoInterpolationGridTriangle(pts[0], pts[2], pts[3]);
			Triangles[1].FindLinesAndLevels(new int[3]{LevelOfPts[0], LevelOfPts[2], LevelOfPts[3]}, levels);
			
		}
		
		protected override void AssignLevelsToPoints(){
			
			for (int i = 0; i < pts.Length; i++) {
				for (int j = 0; j < levels.Length; j++) {
					if (pts[i].z >= levels[j]) {
						LevelOfPts[i] += 1;
					}
				}
			}
		}
	}
}
