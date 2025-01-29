namespace PGViewer
{
	public class GrayscaleImage
	{
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
		#endregion //Public functions
	}
}
