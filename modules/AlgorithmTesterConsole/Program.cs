using System.Diagnostics;
using System.Drawing;
using PGraphicsLib;

namespace AlgorithmTesterConsole
{
	internal class Program
	{
		#region Constants
		private static string IMAGE_PATH = "../../data/NewImage.txt";
		private static string DIRECTORY_PATH = "../../data";
		private static bool SAVE_RESULTS = false;
        private static int IMAGE_WIDTH = 512;
		private static int IMAGE_HEIGHT = 512;
		private static int PROCESS_EVERY_NTH_LINE = 0; // If 0, process all lines
        #endregion // Constants

        private static void Main()
		{
			int replicationCount = 0;
			bool runCyclically = false;

			while (true)
			{
				Console.Clear();
				Console.WriteLine("===PGSP algo speed tester===");
				Console.WriteLine("Available command:");
				Console.WriteLine("Run n times:\t [insert 'c' to run cyclically all images]");

				var input = Console.ReadLine();
				if (string.IsNullOrEmpty(input)) continue;

				// Parse input
				try
				{
                    if (input.Equals("c")) runCyclically = true;
					else 
					{
						replicationCount = int.Parse(input);
						if (replicationCount < 1) continue;
					}
                    break;
				}
				catch
				{
					continue;
				}
			}

			Console.WriteLine($"Run details: {replicationCount} replications, run cyclically: {runCyclically}");

            if (!runCyclically) Replications(replicationCount);
            else Cyclically();
		}

        private static void Cyclically()
        {
            Stopwatch sw = new();
            List<double> loadResults = [];
            List<double> gaussResults = [];
            List<double> thresholdResults = [];
            List<double> lineCenterResults = [];
            List<double> bezierResults = [];
            //List<double> sobelResults = [];

            string[] filesArr = Directory.GetFiles(DIRECTORY_PATH, "*.txt");
            List<string> files = filesArr.ToList();
            files.RemoveAll(f => f.Contains("NewImage"));

            if (files.Count == 0)
            {
                Console.WriteLine("No files found in directory!");
                return;
            }

            IMAGE_WIDTH = 640;
            IMAGE_HEIGHT = 480;

            for (int i = 0; i < files.Count; i++)
            {
                Console.WriteLine(i + 1);

                // Load image with optional skip rows
                sw.Start();
                var imageBytes = File.ReadAllBytes(files[i]);
                GrayscaleImage image = new(IMAGE_WIDTH, IMAGE_HEIGHT, imageBytes);

                if (PROCESS_EVERY_NTH_LINE > 0)
                {
                    int rowSize = IMAGE_WIDTH;
                    List<byte> bytes = [];
                    for (int j = 0; j < IMAGE_HEIGHT; j++)
                    {
                        if ((j % PROCESS_EVERY_NTH_LINE) == 0)
                        {
                            bytes.AddRange(imageBytes.Skip(j * rowSize).Take(rowSize));
                        }
                    }
                    int newHeight = bytes.Count / IMAGE_WIDTH;

                    image = new(IMAGE_WIDTH, newHeight, bytes.ToArray());
                }

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
                //sw.Start();
                //image.ApplySobelEdgeDetection();
                //sw.Stop();
                //sobelResults.Add(sw.Elapsed.TotalMilliseconds);
                //sw.Reset();
            }

            Console.WriteLine();
            Console.WriteLine("Results:");
            Console.WriteLine($"Load image: {loadResults.Average()} ms");
            Console.WriteLine($"Gauss filter: {gaussResults.Average()} ms");
            Console.WriteLine($"Thresholding: {thresholdResults.Average()} ms");
            Console.WriteLine($"Find middle line: {lineCenterResults.Average()} ms");
            Console.WriteLine($"Fit bezier curve: {bezierResults.Average()} ms");
            //Console.WriteLine($"Sobel edge detection: {sobelResults.Average()} ms");

            // Save data to csv
            if (SAVE_RESULTS)
            {
                string path = "../../output/results.csv";
                if (!Directory.Exists("../../output")) Directory.CreateDirectory("../../output");
                if (!File.Exists(path))
                {
                    File.Create(path).Close();
                    File.WriteAllText(path, "Load,Gauss,Threshold,LineCenter,Bezier\n");
                }
                File.AppendAllText(path, $"{loadResults.Average()},{gaussResults.Average()},{thresholdResults.Average()},{lineCenterResults.Average()},{bezierResults.Average()}\n");
            }
            Console.WriteLine();
        }

        private static void Replications(int replicationCount)
        {
            Stopwatch sw = new();
            List<double> loadResults = [];
            List<double> gaussResults = [];
            List<double> thresholdResults = [];
            List<double> lineCenterResults = [];
            List<double> bezierResults = [];
            //List<double> sobelResults = [];

            for (int i = 0; i < replicationCount; i++)
            {
                Console.WriteLine(i + 1);

                // Load image with optional skip rows
                sw.Start();
                var imageBytes = File.ReadAllBytes(IMAGE_PATH);
                GrayscaleImage image = new(IMAGE_WIDTH, IMAGE_HEIGHT, imageBytes);

                if (PROCESS_EVERY_NTH_LINE > 0)
                {
                    int rowSize = IMAGE_WIDTH;
                    List<byte> bytes = [];
                    for (int j = 0; j < IMAGE_HEIGHT; j++)
                    {
                        if ((j % PROCESS_EVERY_NTH_LINE) == 0)
                        {
                            bytes.AddRange(imageBytes.Skip(j * rowSize).Take(rowSize));
                        }
                    }
                    int newHeight = bytes.Count / IMAGE_WIDTH;

                    image = new(IMAGE_WIDTH, newHeight, bytes.ToArray());
                }

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
                //sw.Start();
                //image.ApplySobelEdgeDetection();
                //sw.Stop();
                //sobelResults.Add(sw.Elapsed.TotalMilliseconds);
                //sw.Reset();
            }

            Console.WriteLine();
            Console.WriteLine("Results:");
            Console.WriteLine($"Load image: {loadResults.Average()} ms");
            Console.WriteLine($"Gauss filter: {gaussResults.Average()} ms");
            Console.WriteLine($"Thresholding: {thresholdResults.Average()} ms");
            Console.WriteLine($"Find middle line: {lineCenterResults.Average()} ms");
            Console.WriteLine($"Fit bezier curve: {bezierResults.Average()} ms");
            //Console.WriteLine($"Sobel edge detection: {sobelResults.Average()} ms");

            // Save data to csv
            if (SAVE_RESULTS)
            {
                string path = "../../output/results.csv";
                if (!Directory.Exists("../../output")) Directory.CreateDirectory("../../output");
                if (!File.Exists(path))
                {
                    File.Create(path).Close();
                    File.WriteAllText(path, "Load,Gauss,Threshold,LineCenter,Bezier\n");
                }
                File.AppendAllText(path, $"{loadResults.Average()},{gaussResults.Average()},{thresholdResults.Average()},{lineCenterResults.Average()},{bezierResults.Average()}\n");
            }
            Console.WriteLine();
        }
    }
}
