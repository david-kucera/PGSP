namespace PGViewer
{
	public class ImageProcessor
	{
		private static double[,] GenerateGaussianKernel(int size, double sigma)
		{
			double[,] kernel = new double[size, size];
			int halfSize = size / 2;
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
			for (int y = 0; y < size; y++)
				for (int x = 0; x < size; x++)
					kernel[y, x] /= sum;

			return kernel;
		}

		public static GrayscaleImage ApplyGaussianBlur(GrayscaleImage inputImage, int kernelSize = 5, double sigma = 1.0)
		{
			if (kernelSize % 2 == 0 || kernelSize < 3)
				throw new ArgumentException("Kernel size must be an odd number and at least 3.");
			int halfKernel = kernelSize / 2;
			double[,] kernel = GenerateGaussianKernel(kernelSize, sigma);
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
	}
}
