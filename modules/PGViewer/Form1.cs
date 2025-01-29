using System.Data;

namespace PGViewer
{
	public partial class Form1 : Form
	{
		private GrayscaleImage? originalImage = null;
		private bool gaussianBlurApplied = false;
		private List<PointF> _bezierPoints = new List<PointF>();
		private int imageWidth;
		private int imageHeight;

		public Form1()
		{
			InitializeComponent();
		}

		private void doubleBufferPanelDrawing_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;

			if (originalImage != null)
			{
				// Image
				Bitmap bmp = originalImage.ToBitmap();
				g.DrawImage(bmp, 0, 0);

				// Histogram
				if (checkBox_ShowHistograms.Checked)
				{
					Bitmap histogramBmp = originalImage.HistogramToBitmap(400, 150);
					g.DrawImage(histogramBmp, 512, 0);
				}

				// Bezier curve
				if (checkBox_FitBezierCurve.Checked)
				{
					Pen pen = new Pen(Color.Red, 2);
					for (int i = 1; i < _bezierPoints.Count; i++)
					{
						g.DrawLine(pen, _bezierPoints[i - 1], _bezierPoints[i]);
					}
				}
			}
		}

		private void doubleBufferPanelDrawing_MouseMove(object sender, MouseEventArgs e)
		{

			doubleBufferPanelDrawing.Invalidate();
		}

		private void doubleBufferPanelDrawing_MouseUp(object sender, MouseEventArgs e)
		{
			doubleBufferPanelDrawing.Invalidate();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			DataTable table = new DataTable();
			table.Columns.Add("File Name");
			table.Columns.Add("File Path");

			string[] files = Directory.GetFiles("../../data", "*.txt");

			foreach (var file in files)
			{
				FileInfo fi = new FileInfo(file);
				table.Rows.Add(fi.Name, fi.FullName);
			}

			comboBox1.DataSource = table;
			comboBox1.DisplayMember = "File Name";
			comboBox1.ValueMember = "File Path";

			string? selectedString = comboBox1.SelectedValue as string;

			if (string.IsNullOrEmpty(selectedString)) return;

			imageHeight = (int)numericUpDown_ImageHeight.Value;
			imageWidth = (int)numericUpDown_ImageWidth.Value;

			ReloadImage();
		}

		private void ReloadImage()
		{
			originalImage = null;
			string? selectedString = comboBox1.SelectedValue as string;

			if (string.IsNullOrEmpty(selectedString)) return;

			try
			{
				var imageBytes = File.ReadAllBytes(selectedString);
				originalImage = new GrayscaleImage(imageWidth, imageHeight, imageBytes);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error loading file!", MessageBoxButtons.OK);
				return;
			}

			doubleBufferPanelDrawing.Invalidate();
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			checkBox_ApplyGaussianBlur.Checked = false;
			ReloadImage();
		}

		private void UpDownNum_ImageWidth_Changed(object sender, EventArgs e)
		{
			imageWidth = (int)numericUpDown_ImageWidth.Value;
			ReloadImage();
		}

		private void UpDownNum_ImageHeight_Changed(object sender, EventArgs e)
		{
			imageHeight = (int)numericUpDown_ImageHeight.Value;
			ReloadImage();
		}

		private void CheckBox_ApplyGaussianBlur_Changed(object sender, EventArgs e)
		{
			if (originalImage == null) return;

			if (checkBox_ApplyGaussianBlur.Checked)
			{
				originalImage = Gauss.ApplyGaussianBlur(originalImage, (double)numericUpDown_SigmaValue.Value);
				gaussianBlurApplied = true;
				doubleBufferPanelDrawing.Invalidate();
			}
			else
			{
				numericUpDown_SigmaValue.Value = 1;
				gaussianBlurApplied = false;
				ReloadImage();
			}
		}

		private void doubleBufferPanelDrawing_MouseDown(object? sender, MouseEventArgs e)
		{
			return;
		}

		private void UpDownNum_Sigma_Changed(object sender, EventArgs e)
		{
			if (!gaussianBlurApplied) return;

			originalImage = Gauss.ApplyGaussianBlur(originalImage, (double)numericUpDown_SigmaValue.Value);
			doubleBufferPanelDrawing.Invalidate();
		}

		private void checkBox_ApplyOtsuTreshold_Changed(object sender, EventArgs e)
		{
			if (originalImage == null) return;

			if (checkBoxApplyOtsuThreshold.Checked)
			{
				originalImage.ApplyThreshold();
				doubleBufferPanelDrawing.Invalidate();
			}
			else ReloadImage();

		}

		private void checkBox_ShowHistograms_CheckedChanged(object sender, EventArgs e)
		{
			doubleBufferPanelDrawing.Invalidate();
		}

		private void checkBox_ApplySobelEdge_CheckedChanged(object sender, EventArgs e)
		{
			if (originalImage == null) return;

			if (checkBox_ApplySobelEdge.Checked)
			{
				originalImage.ApplySobelEdgeDetection();
				doubleBufferPanelDrawing.Invalidate();
			}
			else ReloadImage();
		}

		private void checkBox_FitBezierCurve_CheckedChanged(object sender, EventArgs e)
		{
			if (originalImage == null) return;

			if (checkBox_FitBezierCurve.Checked)
			{
				List<Point> centers = originalImage.ExtractLineCenters();
				if (centers.Count < 2) 
					return;
				_bezierPoints = Bezier.FitCubicBezierCurve(centers);
				doubleBufferPanelDrawing.Invalidate();
			}
			else
			{
				_bezierPoints.Clear();
				ReloadImage();
			}
		}
	}
}
