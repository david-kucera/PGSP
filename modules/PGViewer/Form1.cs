using System.Data;
using PGraphicsLib;

namespace PGViewer
{
	public partial class Form1 : Form
	{
		#region Class members
		private GrayscaleImage? _image = null;
		private GrayscaleImage? _originalImage = null;
		private List<PointF> _bezierPoints = [];
		private List<Point> _centers = [];
		private int _imageWidth;
		private int _imageHeight;
		#endregion // Class members

		#region Constructor
		public Form1()
		{
			InitializeComponent();
		}
		#endregion // Constructor

		#region Event handlers
		private void doubleBufferPanelDrawing_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;

			if (_image != null && _originalImage != null)
			{
				// Image
				Bitmap bmp = _image.ToBitmap();
				g.DrawImage(bmp, 0, 0);

				// Image histogram
				if (checkBox_ShowHistograms.Checked)
				{
					Bitmap histogramBmp = _image.HistogramToBitmap(640, 150);
					g.DrawImage(histogramBmp, 0, 640);
				}

				// Original image
				Bitmap originalImage = _originalImage.ToBitmap();
				g.DrawImage(originalImage, 650, 0);

				// Original image histogram
				if (checkBox_ShowHistograms.Checked)
				{
					Bitmap processedHistogramBmp = _originalImage.HistogramToBitmap(640, 150);
					g.DrawImage(processedHistogramBmp, 650, 640);
				}

				// Bezier curve
				if (checkBox_FitBezierCurve.Checked && _bezierPoints.Count >= 4)
				{
					Pen pen = new Pen(Color.Red, 4);
					for (int i = 1; i < _bezierPoints.Count - 3; i += 3)
					{
						g.DrawBezier(pen, _bezierPoints[i], _bezierPoints[i + 1], _bezierPoints[i + 2], _bezierPoints[i + 3]);
					}
				}

				// Middle line
				if (checkBox_ShowMiddleLine.Checked)
				{
					Pen pen = new Pen(Color.Blue, 1);
					foreach (var point in _centers)
					{
						g.DrawEllipse(pen, point.X - 2, point.Y - 2, 4, 4);
					}
				}
			}
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

			comboBox_File.DataSource = table;
			comboBox_File.DisplayMember = "File Name";
			comboBox_File.ValueMember = "File Path";
		}

		private void ComboBoxFileSelectedIndexChanged(object sender, EventArgs e)
		{
			checkBox_ApplyGaussianBlur.Checked = false;
			checkBox_FitBezierCurve.Checked = false;
			checkBox_ApplySobelEdge.Checked = false;
			checkBox_ApplyOtsuThreshold.Checked = false;
			_imageHeight = (int)numericUpDown_ImageHeight.Value;
			_imageWidth = (int)numericUpDown_ImageWidth.Value;
			ReloadImage();
		}

		private void UpDownNum_ImageWidth_Changed(object sender, EventArgs e)
		{
			_imageWidth = (int)numericUpDown_ImageWidth.Value;
		}

		private void UpDownNum_ImageHeight_Changed(object sender, EventArgs e)
		{
			_imageHeight = (int)numericUpDown_ImageHeight.Value;
		}

		private void CheckBox_ApplyGaussianBlur_Changed(object sender, EventArgs e)
		{
			if (_image == null) return;

			if (checkBox_ApplyGaussianBlur.Checked)
			{
				_image.ApplyGaussianBlur((double)numericUpDown_SigmaValue.Value);
				doubleBufferPanelDrawing.Invalidate();
			}
			else
			{
				numericUpDown_SigmaValue.Value = 1;
				ReloadImage();
			}
		}

		private void UpDownNum_Sigma_Changed(object sender, EventArgs e)
		{
			if (!checkBox_ApplyGaussianBlur.Checked) return;

			_image.ApplyGaussianBlur((double)numericUpDown_SigmaValue.Value);
			doubleBufferPanelDrawing.Invalidate();
		}

		private void checkBox_ApplyOtsuTreshold_Changed(object sender, EventArgs e)
		{
			if (_image == null) return;

			if (checkBox_ApplyOtsuThreshold.Checked)
			{
				_image.ApplyThreshold();
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
			if (_image == null) return;

			if (checkBox_ApplySobelEdge.Checked)
			{
				_image.ApplySobelEdgeDetection();
				doubleBufferPanelDrawing.Invalidate();
			}
			else ReloadImage();
		}

		private void checkBox_FitBezierCurve_CheckedChanged(object sender, EventArgs e)
		{
			if (_image == null) return;

			if (checkBox_FitBezierCurve.Checked)
			{
				_centers = _image.ExtractLineCenters();
				if (_centers.Count < 2)
					return;
				_bezierPoints = Bezier.FitCubicBezierCurve(_centers);
				doubleBufferPanelDrawing.Invalidate();
			}
			else
			{
				_bezierPoints.Clear();
				_centers.Clear();
				checkBox_ShowMiddleLine.Checked = false;
				ReloadImage();
			}
		}

		private void CheckBoxShowMiddleLineCheckedChanged(object sender, EventArgs e)
		{
			if (_image == null) return;

			if (checkBox_ShowMiddleLine.Checked)
			{
				if (_centers.Count < 2)
					return;
			}
			doubleBufferPanelDrawing.Invalidate();
		}
		#endregion // Event handlers

		#region Private functions
		private void ReloadImage()
		{
			_image = null;
			_originalImage = null;
			string? selectedString = comboBox_File.SelectedValue as string;

			if (string.IsNullOrEmpty(selectedString)) return;

			try
			{
				var imageBytes = File.ReadAllBytes(selectedString);
				_image = new GrayscaleImage(_imageWidth, _imageHeight, imageBytes);
				_originalImage = new GrayscaleImage(_imageWidth, _imageHeight, imageBytes);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error loading file!", MessageBoxButtons.OK);
				return;
			}

			checkBox_FitBezierCurve.Checked = false;
			checkBox_ShowMiddleLine.Checked = false;
			checkBox_ApplyGaussianBlur.Checked = false;
			checkBox_ApplySobelEdge.Checked = false;
			checkBox_ApplyOtsuThreshold.Checked = false;
			numericUpDown_SigmaValue.Value = 1;
			_centers.Clear();
			_bezierPoints.Clear();

			doubleBufferPanelDrawing.Invalidate();
		}
		#endregion
	}
}
