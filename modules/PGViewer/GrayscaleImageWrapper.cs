using PGraphicsLib;

namespace PGViewer
{
	public class GrayscaleImageWrapper
	{
		#region Properties 
		public GrayscaleImage Image { get; set; } = null;
		#endregion // Properties

		#region Constructors
		public GrayscaleImageWrapper(int width, int height, byte[] data)
		{
			ArgumentNullException.ThrowIfNull(data);

			if (width < 16 || height < 16 || width > 1024 || height > 1024)
				throw new ArgumentException("Grayscale image is out of size");

			Image = new GrayscaleImage(width, height, data);
		}
		#endregion // Constructors

		#region Public functions
		public Bitmap ToBitmap()
		{
			Bitmap bmp = new(Image.Width, Image.Height);
			for (int y = 0; y < Image.Height; y++)
			{
				for (int x = 0; x < Image.Width; x++)
				{
					byte color = Image.Data[y * Image.Width + x];
					Image.Histogram[color]++;
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

			int maxHistogramValue = Image.Histogram.Max();
			float scale = (float)canvasHeight / maxHistogramValue;

			for (int i = 0; i < Image.Histogram.Length; i++)
			{
				int barHeight = (int)(Image.Histogram[i] * scale);
				g.FillRectangle(Brushes.Black, i * (canvasWidth / 256), canvasHeight - barHeight, (canvasWidth / 256), barHeight);
			}

			return histogramBitmap;
		}
		#endregion // Public functions
	}
}
