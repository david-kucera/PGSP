﻿namespace PGViewer
{
	public class GrayscaleImage
	{
		#region Class members
		private int _threshold = 128;
		#endregion // Class members

		#region Properties
		public int Width { get; set; }
		public int Height { get; set; }
		public byte[] Data { get; set; }
		public int[] Histogram { get; set; } = new int[256];
		#endregion //Properties

		#region Constructors
		public GrayscaleImage(int width, int height, byte[] data)
		{
			ArgumentNullException.ThrowIfNull(data);

			if (width < 16 || height < 16 || width > 1024 || height > 1024)
				throw new ArgumentException("Grayscale image is out of size");

			Width = width;
			Height = height;
			Data = data;
		}

		public GrayscaleImage(int width, int height)
		{
			if (width < 16 || height < 16 || width > 1024 || height > 1024)
				throw new ArgumentException("Grayscale image is out of size");

			Width = width;
			Height = height;
			Data = new byte[width * height];
		}
		#endregion //Constructors

		#region Public functions
		public Bitmap ToBitmap()
		{
			Bitmap bmp = new(Width, Height);
			for (int y = 0; y < Height; y++)
			{
				for (int x = 0; x < Width; x++)
				{
					byte color = Data[y * Width + x];
					Histogram[color]++;
					bmp.SetPixel(x, y, Color.FromArgb(color, color, color));
				}
			}

			return bmp;
		}

		public Bitmap HistogramToBitmap(int canvasWidth, int canvasHeight)
		{
			Bitmap histogramBitmap = new(canvasWidth, canvasHeight);
			using Graphics g = Graphics.FromImage(histogramBitmap);
			g.Clear(Color.White);

			int maxHistogramValue = Histogram.Max();
			float scale = (float)canvasHeight / maxHistogramValue;

			for (int i = 0; i < Histogram.Length; i++)
			{
				int barHeight = (int)(Histogram[i] * scale);
				g.FillRectangle(Brushes.Black, i * (canvasWidth / 256), canvasHeight - barHeight, (canvasWidth / 256), barHeight);
			}

			return histogramBitmap;
		}

		public List<Point> ExtractLineCenters()
		{
			List<Point> midline = [];

			for (int y = 0; y < Height; y++)
			{
				List<int> whitePixelStart = [];
				List<int> whitePixelEnd = [];

				bool whiteSegment = false;
				for (int x = 0; x < Width; x++)
				{
					if (Data[y * Width + x] == 255)
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

				if (whiteSegment) whitePixelEnd.Add(Width - 1);

				if (whitePixelStart.Count == 2 && whitePixelEnd.Count == 2)
				{
					int leftSegmentCenter = (whitePixelStart[0] + whitePixelEnd[0]) / 2;
					int rightSegmentCenter = (whitePixelStart[1] + whitePixelEnd[1]) / 2;

					int midX = (leftSegmentCenter + rightSegmentCenter) / 2;
					midline.Add(new Point(midX, y));
				}
				else if (whitePixelStart.Count > 2)
				{
					int maxDist = int.MinValue;
					int midX = 0;

					for (int i = 0; i < whitePixelStart.Count - 1; i++)
					{
						for (int j = i + 1; j < whitePixelStart.Count; j++)
						{
							int leftCenter = (whitePixelStart[i] + whitePixelEnd[i]) / 2;
							int rightCenter = (whitePixelStart[j] + whitePixelEnd[j]) / 2;

							int dist = Math.Abs(leftCenter - rightCenter);
							if (dist > maxDist)
							{
								maxDist = dist;
								midX = (leftCenter + rightCenter) / 2;
							}
						}
					}

					midline.Add(new Point(midX, y));
				}
			}

			return midline;
		}

		public void ApplyThreshold()
		{
			_threshold = OtsuThreshold();
			for (int i = 0; i < Data.Length; i++) Data[i] = Data[i] > _threshold ? (byte)255 : (byte)0;
		}

		public void ApplySobelEdgeDetection()
		{
			byte[] newData = new byte[Width * Height];

			for (int y = 1; y < Height - 1; y++)
			{
				for (int x = 1; x < Width - 1; x++)
				{
					int gradX = 0;
					int gradY = 0;

					for (int ky = -1; ky <= 1; ky++)
					{
						for (int kx = -1; kx <= 1; kx++)
						{
							int pixelValue = Data[(y + ky) * Width + (x + kx)];
							gradX += pixelValue * SobelKernel.X[ky + 1, kx + 1];
							gradY += pixelValue * SobelKernel.Y[ky + 1, kx + 1];
						}
					}

					int magnitude = (int)Math.Sqrt(gradX * gradX + gradY * gradY);
					magnitude = Math.Min(255, magnitude);

					newData[y * Width + x] = (byte)magnitude;
				}
			}

			Data = newData;
		}
		#endregion //Public functions

		#region Private functions
		private int OtsuThreshold()
		{
			Histogram = new int[256];
			for (int i = 0; i < Data.Length; i++) Histogram[Data[i]]++;

			int totalPixels = Width * Height;
			float sum = 0;
			for (int i = 0; i < 256; i++) sum += i * Histogram[i];

			float sumB = 0;
			int wB = 0;
			int wF = 0;
			float varMax = 0;
			int threshold = 0;

			for (int i = 0; i < 256; i++)
			{
				wB += Histogram[i];
				if (wB == 0) continue;

				wF = totalPixels - wB;
				if (wF == 0) break;

				sumB += i * Histogram[i];
				float mB = sumB / wB;
				float mF = (sum - sumB) / wF;
				float varBetween = wB * wF * (mB - mF) * (mB - mF);

				if (varBetween > varMax)
				{
					varMax = varBetween;
					threshold = i;
				}
			}

			return threshold;
		}
		#endregion // Private functions
	}
}
