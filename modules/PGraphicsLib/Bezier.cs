using System.Drawing;

namespace PGraphicsLib
{
	public static class Bezier
	{
		#region Public functions
		public static List<PointF> FitCubicBezierCurve(List<Point> points)
		{
			List<PointF> bezierPoints = [];

			if (points.Count < 2)
				return bezierPoints;

			Point start = points.First();
			Point end = points.Last();

			Point mid = points[points.Count / 2];

			int dx = end.X - start.X;
			int dy = end.Y - start.Y;

			Point control1 = new Point(mid.X - dx / 4, mid.Y - dy / 3);
			Point control2 = new Point(mid.X + dx / 4, mid.Y + dy / 3);

			for (float t = 0; t <= 1; t += 1.0f / points.Count)
			{
				float u = 1 - t;
				float uu = u * u;
				float uuu = uu * u;
				float tt = t * t;
				float ttt = tt * t;

				float x = uuu * start.X + 3 * uu * t * control1.X + 3 * u * tt * control2.X + ttt * end.X;
				float y = uuu * start.Y + 3 * uu * t * control1.Y + 3 * u * tt * control2.Y + ttt * end.Y;

				bezierPoints.Add(new PointF(x, y));
			}

			return bezierPoints;
		}
		#endregion //Public functions
	}
}
