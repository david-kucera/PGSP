using System.Drawing;

namespace PGraphicsLib
{
	public class GrayscaleImage
	{
		#region Properties
		public int Width { get; }
		public int Height { get; }
		public byte[] Data { get; set; }
		public int[] Histogram { get; set; } = new int[256];
		#endregion // Class members

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
		#endregion // Constructors

		#region Public functions
		public void ApplyGaussianBlur(double sigma)
		{
			Data = Gauss.ApplyGaussianBlur(Width, Height, Data, sigma);
		}

		public List<Point> ExtractLineCentersBlackLine()
		{
			return MiddlelineExtractor.ExtractBlackLine(Width, Height, Data);
		}

		public List<Point> ExtractLineCentersBetweenWhiteLines()
		{
			return MiddlelineExtractor.ExtractBetweenWhiteLines(Width, Height, Data);
		}

		public void ApplyThreshold()
		{
			int threshold = Threshold.MeanThreshold(Width, Height, Data);
			for (int i = 0; i < Data.Length; i++) Data[i] = Data[i] > threshold ? (byte)255 : (byte)0;
		}

		public void ApplySobelEdgeDetection()
		{
			Data = Sobel.ApplyEdgeDetection(Width, Height, Data);
		}

		public List<PointF> FitCubicBezierCurve(List<Point> centres)
		{
			return Bezier.FitCubicBezierCurve(centres);
		}
		#endregion // Public functions
	}
}
