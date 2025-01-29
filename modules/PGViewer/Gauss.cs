namespace PGViewer
{
	public class Gauss
	{
		#region Constants
		private const int KERNEL_SIZE = 3;
		#endregion // Constants

		#region Publix functions
		public static GrayscaleImage ApplyGaussianBlur(GrayscaleImage inputImage, double sigma = 1.0)
		{
			int halfKernel = KERNEL_SIZE / 2;
			double[,] kernel = GenerateGaussianKernel(sigma);
			double kernelSum = kernel.Cast<double>().Sum();

			int width = inputImage.Width;
			int height = inputImage.Height;
			byte[] inputData = inputImage.Data;
			byte[] outputData = new byte[width * height];

			for (int y = 0; y < height; y++)
			{
				for (int x = 0; x < width; x++)
				{
					double sum = 0;

					for (int ky = -halfKernel; ky <= halfKernel; ky++)
					{
						for (int kx = -halfKernel; kx <= halfKernel; kx++)
						{
							int pixelX = Math.Clamp(x + kx, 0, width - 1);
							int pixelY = Math.Clamp(y + ky, 0, height - 1);
							byte pixelValue = inputData[pixelY * width + pixelX];

							sum += pixelValue * kernel[ky + halfKernel, kx + halfKernel];
						}
					}

					outputData[y * width + x] = (byte)(sum / kernelSum);
				}
			}

			return new GrayscaleImage(width, height, outputData);
		}
		#endregion // Public functions

		#region Private functions
		private static double[,] GenerateGaussianKernel(double sigma)
		{
			double[,] kernel = new double[KERNEL_SIZE, KERNEL_SIZE];
			int halfSize = KERNEL_SIZE / 2;
			double sum = 0;
			double sigma2 = 2 * sigma * sigma;
			double piSigma2 = Math.PI * sigma2;

			for (int y = -halfSize; y <= halfSize; y++)
			{
				for (int x = -halfSize; x <= halfSize; x++)
				{
					double value = Math.Exp(-(x * x + y * y) / sigma2) / piSigma2;
					kernel[y + halfSize, x + halfSize] = value;
					sum += value;
				}
			}

			// kernel normalisation
			for (int y = 0; y < KERNEL_SIZE; y++)
			for (int x = 0; x < KERNEL_SIZE; x++)
				kernel[y, x] /= sum;

			return kernel;
		}
		#endregion
	}
}
