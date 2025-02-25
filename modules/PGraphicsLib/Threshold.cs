﻿namespace PGraphicsLib
{
	public static class Threshold
	{
		public static int OtsuThreshold(int width, int height, byte[] data)
		{
			int[] histogram = new int[256];
			for (int i = 0; i < data.Length; i++) histogram[data[i]]++;

			int totalPixels = width * height;
			float sum = 0;
			for (int i = 0; i < 256; i++) sum += i * histogram[i];

			float sumB = 0;
			int wB = 0;
			int wF = 0;
			float varMax = 0;
			int threshold = 0;

			for (int i = 0; i < 256; i++)
			{
				wB += histogram[i];
				if (wB == 0) continue;

				wF = totalPixels - wB;
				if (wF == 0) break;

				sumB += i * histogram[i];
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

		public static int MeanThreshold(int width, int height, byte[] data)
		{
			int totalPixels = width * height;
			int sum = 0;

			for (int y = 0; y < height; y++)
			{
				int rowSum = 0;
				for (int x = 0; x < width; x++)
				{
					rowSum += data[y * width + x];
				}

				sum += rowSum;
			}

			return sum / totalPixels;
		}
	}
}
