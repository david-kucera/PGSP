namespace PGViewer
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

			Point control1 = points[points.Count / 3];
			Point control2 = points[2 * points.Count / 3];

			for (float t = 0; t <= 1; t += 0.01f)
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
