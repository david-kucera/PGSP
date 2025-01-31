using System.Diagnostics;
using System.Drawing;
using PGraphicsLib;

namespace AlgorithmTesterConsole
{
	internal class Program
	{
		private static void Main()
		{
			int replicationCount = 0;
			bool saveResults = false;

			while (true)
			{
				Console.Clear();
				Console.WriteLine("===PGSP algo speed tester===");
				Console.WriteLine("Available command:");
				Console.WriteLine("Run n times:\trun -n [-s to save results in csv format to output folder]");

				var input = Console.ReadLine();
				if (string.IsNullOrEmpty(input) || !input.Contains("run")) continue;

				// Parse input
				try
				{
					var comms = input.Split(' ');
					replicationCount = int.Parse(comms[1].Substring(1));
					if (replicationCount < 1) continue;

					saveResults = false;
					if (comms.Length == 3)
					{
						string saveString = comms[2].Substring(1);
						if (saveString.Equals("s")) saveResults = true;
					}

					break;
				}
				catch
				{
					continue;
				}
			}

			Console.WriteLine($"Run details: {replicationCount} replications, save results: {saveResults}");

			Stopwatch sw = new();
			List<double> loadResults = [];
			List<double> gaussResults = [];
			List<double> thresholdResults = [];
			List<double> lineCenterResults = [];
			List<double> bezierResults = [];
			List<double> sobelResults = [];

			for (int i = 0; i < replicationCount; i++)
			{
				Console.WriteLine(i+1);
				// Load image
				sw.Start();
				var imageBytes = File.ReadAllBytes("../../data/NewImage.txt");
				GrayscaleImage image = new(512,512, imageBytes);
				sw.Stop();
				loadResults.Add(sw.Elapsed.TotalMilliseconds);
				sw.Reset();

				// Apply gauss filter
				sw.Start();
				image.ApplyGaussianBlur(1.0);
				sw.Stop();
				gaussResults.Add(sw.Elapsed.TotalMilliseconds);
				sw.Reset();

				// Apply thresholding
				sw.Start();
				image.ApplyThreshold();
				sw.Stop();
				thresholdResults.Add(sw.Elapsed.TotalMilliseconds);
				sw.Reset();

				// Find middle line
				sw.Start();
				List<Point> centres = image.ExtractLineCentersBlackLine();
				sw.Stop();
				lineCenterResults.Add(sw.Elapsed.TotalMilliseconds);
				sw.Reset();

				// Fit bezier curve
				sw.Start();
				image.FitCubicBezierCurve(centres);
				sw.Stop();
				bezierResults.Add(sw.Elapsed.TotalMilliseconds);
				sw.Reset();

				// Apply sobel edge detection
				sw.Start();
				image.ApplySobelEdgeDetection();
				sw.Stop();
				sobelResults.Add(sw.Elapsed.TotalMilliseconds);
				sw.Reset();
			}

			Console.WriteLine();
			Console.WriteLine("Results:");
			Console.WriteLine($"Load image: {loadResults.Average()} ms");
			Console.WriteLine($"Gauss filter: {gaussResults.Average()} ms");
			Console.WriteLine($"Thresholding: {thresholdResults.Average()} ms");
			Console.WriteLine($"Find middle line: {lineCenterResults.Average()} ms");
			Console.WriteLine($"Fit bezier curve: {bezierResults.Average()} ms");
			Console.WriteLine($"Sobel edge detection: {sobelResults.Average()} ms");
			Console.WriteLine();
		}
	}
}
