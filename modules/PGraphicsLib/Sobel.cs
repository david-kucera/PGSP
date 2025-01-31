namespace PGraphicsLib
{
	public class Sobel
	{
		#region Constants
		private static int[,] X
		{
			get
			{
				return new int[,]
				{
					{ -1, 0, 1 },
					{ -2, 0, 2 },
					{ -1, 0, 1 }
				};
			}
		}

		private static int[,] Y
		{
			get
			{
				return new int[,]
				{
					{ -1, -2, -1 },
					{ 0, 0, 0 },
					{ 1, 2, 1 }
				};
			}
		}
		#endregion // Constants

		#region Public functions
		public static byte[] ApplyEdgeDetection(int width, int height, byte[] data)
		{
			byte[] newData = new byte[width * height];

			for (int y = 1; y < height - 1; y++)
			{
				for (int x = 1; x < width - 1; x++)
				{
					int gradX = 0;
					int gradY = 0;

					for (int ky = -1; ky <= 1; ky++)
					{
						for (int kx = -1; kx <= 1; kx++)
						{
							int pixelValue = data[(y + ky) * width + (x + kx)];
							gradX += pixelValue * X[ky + 1, kx + 1];
							gradY += pixelValue * Y[ky + 1, kx + 1];
						}
					}

					int magnitude = (int)Math.Sqrt(gradX * gradX + gradY * gradY);
					magnitude = Math.Min(255, magnitude);

					newData[y * width + x] = (byte)magnitude;
				}
			}

			return newData;
		}
		#endregion // Public functions
	}
}
