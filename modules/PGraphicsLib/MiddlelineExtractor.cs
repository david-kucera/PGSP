using System.Drawing;

namespace PGraphicsLib
{
	public static class MiddlelineExtractor
	{
		public static List<Point> ExtractBetweenWhiteLines(int width, int height, byte[] data)
		{
			List<Point> midline = [];

			for (int y = 0; y < height; y++)
			{
				List<int> whitePixelStart = [];
				List<int> whitePixelEnd = [];

				bool whiteSegment = false;
				for (int x = 0; x < width; x++)
				{
					if (data[y * width + x] == 255)
					{
						if (whiteSegment) continue;
						whitePixelStart.Add(x);
						whiteSegment = true;
					}
					else
					{
						if (!whiteSegment) continue;
						whitePixelEnd.Add(x - 1);
						whiteSegment = false;
					}
				}
				if (whiteSegment) whitePixelEnd.Add(width - 1);

				if (whitePixelStart.Count >= 2)
				{
					int maxDist = int.MinValue;
					int bestLeft = 0, bestRight = 0;

					for (int i = 0; i < whitePixelStart.Count - 1; i++)
					{
						for (int j = i + 1; j < whitePixelStart.Count; j++)
						{
							int dist = Math.Abs(whitePixelStart[j] - whitePixelEnd[i]);
							if (dist > maxDist)
							{
								maxDist = dist;
								bestLeft = i;
								bestRight = j;
							}
						}
					}

					int leftCenter = (whitePixelStart[bestLeft] + whitePixelEnd[bestLeft]) / 2;
					int rightCenter = (whitePixelStart[bestRight] + whitePixelEnd[bestRight]) / 2;
					int midX = (leftCenter + rightCenter) / 2;

					midline.Add(new Point(midX, y));
				}
			}

			return midline;
		}

		public static List<Point> ExtractBlackLine(int width, int height, byte[] data)
		{
			var centers = new List<Point>();

			for (int y = 0; y < height; y++)
			{
				var blackPixels = new List<int>();
				for (int x = 0; x < width; x++)
				{
					if (data[y * width + x] == 0)
					{
						blackPixels.Add(x);
					}
				}

				if (blackPixels.Count > 0)
				{
					int midX = (int)blackPixels.Average();
					centers.Add(new Point(midX, y));
				}
			}

			return centers;
		}
	}
}
