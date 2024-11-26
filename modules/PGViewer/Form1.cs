using System.Data;
using PG_Cvicenie02;

namespace cv1
{
	public partial class Form1 : Form
	{
		private List<Point> mousePositions = new List<Point>();
		private GrayscaleImage? originalImage = null;
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
				Bitmap bmp = originalImage.ToBitmap();
				g.DrawImage(bmp, 0, 0);

				// Histogram
				Bitmap histogramBmp = originalImage.HistogramToBitmap(400, 150);
				g.DrawImage(histogramBmp, 512, 0);
			}
		}

		private void doubleBufferPanelDrawing_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				mousePositions.Add(e.Location);
			}
			else if (e.Button == MouseButtons.Right)
			{
				mousePositions.Clear();
			}

			doubleBufferPanelDrawing.Invalidate();
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

			imageHeight = (int)numericUpDown2.Value;
			imageWidth = (int)numericUpDown1.Value;

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
			ReloadImage();
		}

		private void numericUpDown1_ValueChanged(object sender, EventArgs e)
		{
			imageWidth = (int)numericUpDown1.Value;
			ReloadImage();
		}

		private void numericUpDown2_ValueChanged(object sender, EventArgs e)
		{
			imageHeight = (int)numericUpDown2.Value;
			ReloadImage();
		}
	}
}
