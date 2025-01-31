using PGraphicsLib;

namespace PGViewer
{
	public class GrayscaleImage
	{
		#region Class members
		private int _width { get; }
		private int _height { get; }
		private byte[] _data { get; set; }
		private int[] _histogram { get; set; } = new int[256];
		#endregion // Class members

		#region Constructors
		public GrayscaleImage(int width, int height, byte[] data)
		{
			ArgumentNullException.ThrowIfNull(data);

			if (width < 16 || height < 16 || width > 1024 || height > 1024)
				throw new ArgumentException("Grayscale image is out of size");

			_width = width;
			_height = height;
			_data = data;
		}
		#endregion //Constructors

		#region Public functions
		public Bitmap ToBitmap()
		{
			Bitmap bmp = new(_width, _height);
			for (int y = 0; y < _height; y++)
			{
				for (int x = 0; x < _width; x++)
				{
					byte color = _data[y * _width + x];
					_histogram[color]++;
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

			int maxHistogramValue = _histogram.Max();
			float scale = (float)canvasHeight / maxHistogramValue;

			for (int i = 0; i < _histogram.Length; i++)
			{
				int barHeight = (int)(_histogram[i] * scale);
				g.FillRectangle(Brushes.Black, i * (canvasWidth / 256), canvasHeight - barHeight, (canvasWidth / 256), barHeight);
			}

			return histogramBitmap;
		}

		public void ApplyGaussianBlur(double sigma)
		{
			_data = Gauss.ApplyGaussianBlur(_width, _height, _data, sigma);
		}

		public List<Point> ExtractLineCenters()
		{
			return MiddlelineExtractor.Extract(_width, _height, _data);
		}

		public void ApplyThreshold()
		{
			int threshold = Otsu.Threshold(_width, _height, _data);
			for (int i = 0; i < _data.Length; i++) _data[i] = _data[i] > threshold ? (byte)255 : (byte)0;
		}

		public void ApplySobelEdgeDetection()
		{
			_data = Sobel.Apply(_width, _height, _data);
		}
		#endregion //Public functions
	}
}
