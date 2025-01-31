namespace PGViewer
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			panelTools = new Panel();
			checkBox_ShowMiddleLine = new CheckBox();
			checkBox_FitBezierCurve = new CheckBox();
			checkBox_ApplySobelEdge = new CheckBox();
			checkBox_ShowHistograms = new CheckBox();
			checkBox_ApplyOtsuThreshold = new CheckBox();
			numericUpDown_SigmaValue = new NumericUpDown();
			label3 = new Label();
			checkBox_ApplyGaussianBlur = new CheckBox();
			label2 = new Label();
			label1 = new Label();
			numericUpDown_ImageHeight = new NumericUpDown();
			numericUpDown_ImageWidth = new NumericUpDown();
			comboBox_File = new ComboBox();
			doubleBufferPanelDrawing = new DoubleBufferPanel();
			panelTools.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)numericUpDown_SigmaValue).BeginInit();
			((System.ComponentModel.ISupportInitialize)numericUpDown_ImageHeight).BeginInit();
			((System.ComponentModel.ISupportInitialize)numericUpDown_ImageWidth).BeginInit();
			SuspendLayout();
			// 
			// panelTools
			// 
			panelTools.BackColor = SystemColors.AppWorkspace;
			panelTools.Controls.Add(checkBox_ShowMiddleLine);
			panelTools.Controls.Add(checkBox_FitBezierCurve);
			panelTools.Controls.Add(checkBox_ApplySobelEdge);
			panelTools.Controls.Add(checkBox_ShowHistograms);
			panelTools.Controls.Add(checkBox_ApplyOtsuThreshold);
			panelTools.Controls.Add(numericUpDown_SigmaValue);
			panelTools.Controls.Add(label3);
			panelTools.Controls.Add(checkBox_ApplyGaussianBlur);
			panelTools.Controls.Add(label2);
			panelTools.Controls.Add(label1);
			panelTools.Controls.Add(numericUpDown_ImageHeight);
			panelTools.Controls.Add(numericUpDown_ImageWidth);
			panelTools.Controls.Add(comboBox_File);
			panelTools.Dock = DockStyle.Left;
			panelTools.Location = new Point(0, 0);
			panelTools.Margin = new Padding(4, 5, 4, 5);
			panelTools.Name = "panelTools";
			panelTools.Size = new Size(256, 864);
			panelTools.TabIndex = 0;
			// 
			// checkBox_ShowMiddleLine
			// 
			checkBox_ShowMiddleLine.AutoSize = true;
			checkBox_ShowMiddleLine.Location = new Point(28, 458);
			checkBox_ShowMiddleLine.Name = "checkBox_ShowMiddleLine";
			checkBox_ShowMiddleLine.Size = new Size(174, 29);
			checkBox_ShowMiddleLine.TabIndex = 12;
			checkBox_ShowMiddleLine.Text = "Show middle line";
			checkBox_ShowMiddleLine.UseVisualStyleBackColor = true;
			checkBox_ShowMiddleLine.CheckedChanged += CheckBoxShowMiddleLineCheckedChanged;
			// 
			// checkBox_FitBezierCurve
			// 
			checkBox_FitBezierCurve.AutoSize = true;
			checkBox_FitBezierCurve.Location = new Point(28, 367);
			checkBox_FitBezierCurve.Name = "checkBox_FitBezierCurve";
			checkBox_FitBezierCurve.Size = new Size(201, 29);
			checkBox_FitBezierCurve.TabIndex = 11;
			checkBox_FitBezierCurve.Text = "Fit Bezier cubic curve";
			checkBox_FitBezierCurve.UseVisualStyleBackColor = true;
			checkBox_FitBezierCurve.CheckedChanged += checkBox_FitBezierCurve_CheckedChanged;
			// 
			// checkBox_ApplySobelEdge
			// 
			checkBox_ApplySobelEdge.AutoSize = true;
			checkBox_ApplySobelEdge.Location = new Point(28, 319);
			checkBox_ApplySobelEdge.Name = "checkBox_ApplySobelEdge";
			checkBox_ApplySobelEdge.Size = new Size(180, 29);
			checkBox_ApplySobelEdge.TabIndex = 10;
			checkBox_ApplySobelEdge.Text = "Apply Sobel edge";
			checkBox_ApplySobelEdge.UseVisualStyleBackColor = true;
			checkBox_ApplySobelEdge.CheckedChanged += checkBox_ApplySobelEdge_CheckedChanged;
			// 
			// checkBox_ShowHistograms
			// 
			checkBox_ShowHistograms.AutoSize = true;
			checkBox_ShowHistograms.Location = new Point(28, 414);
			checkBox_ShowHistograms.Name = "checkBox_ShowHistograms";
			checkBox_ShowHistograms.Size = new Size(176, 29);
			checkBox_ShowHistograms.TabIndex = 9;
			checkBox_ShowHistograms.Text = "Show histograms";
			checkBox_ShowHistograms.UseVisualStyleBackColor = true;
			checkBox_ShowHistograms.CheckedChanged += checkBox_ShowHistograms_CheckedChanged;
			// 
			// checkBox_ApplyOtsuThreshold
			// 
			checkBox_ApplyOtsuThreshold.AutoSize = true;
			checkBox_ApplyOtsuThreshold.Location = new Point(28, 274);
			checkBox_ApplyOtsuThreshold.Name = "checkBox_ApplyOtsuThreshold";
			checkBox_ApplyOtsuThreshold.Size = new Size(208, 29);
			checkBox_ApplyOtsuThreshold.TabIndex = 8;
			checkBox_ApplyOtsuThreshold.Text = "Apply Otsu threshold";
			checkBox_ApplyOtsuThreshold.UseVisualStyleBackColor = true;
			checkBox_ApplyOtsuThreshold.CheckedChanged += checkBox_ApplyOtsuTreshold_Changed;
			// 
			// numericUpDown_SigmaValue
			// 
			numericUpDown_SigmaValue.Location = new Point(147, 228);
			numericUpDown_SigmaValue.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
			numericUpDown_SigmaValue.Name = "numericUpDown_SigmaValue";
			numericUpDown_SigmaValue.Size = new Size(69, 31);
			numericUpDown_SigmaValue.TabIndex = 7;
			numericUpDown_SigmaValue.Value = new decimal(new int[] { 1, 0, 0, 0 });
			numericUpDown_SigmaValue.ValueChanged += UpDownNum_Sigma_Changed;
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new Point(24, 232);
			label3.Name = "label3";
			label3.Size = new Size(108, 25);
			label3.TabIndex = 6;
			label3.Text = "Sigma value";
			// 
			// checkBox_ApplyGaussianBlur
			// 
			checkBox_ApplyGaussianBlur.AutoSize = true;
			checkBox_ApplyGaussianBlur.Location = new Point(28, 188);
			checkBox_ApplyGaussianBlur.Name = "checkBox_ApplyGaussianBlur";
			checkBox_ApplyGaussianBlur.Size = new Size(195, 29);
			checkBox_ApplyGaussianBlur.TabIndex = 5;
			checkBox_ApplyGaussianBlur.Text = "Apply Gaussian Blur";
			checkBox_ApplyGaussianBlur.UseVisualStyleBackColor = true;
			checkBox_ApplyGaussianBlur.CheckedChanged += CheckBox_ApplyGaussianBlur_Changed;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(24, 135);
			label2.Name = "label2";
			label2.Size = new Size(117, 25);
			label2.TabIndex = 4;
			label2.Text = "Image height";
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(24, 89);
			label1.Name = "label1";
			label1.Size = new Size(111, 25);
			label1.TabIndex = 3;
			label1.Text = "Image width";
			// 
			// numericUpDown_ImageHeight
			// 
			numericUpDown_ImageHeight.Location = new Point(147, 133);
			numericUpDown_ImageHeight.Maximum = new decimal(new int[] { 1024, 0, 0, 0 });
			numericUpDown_ImageHeight.Minimum = new decimal(new int[] { 16, 0, 0, 0 });
			numericUpDown_ImageHeight.Name = "numericUpDown_ImageHeight";
			numericUpDown_ImageHeight.Size = new Size(69, 31);
			numericUpDown_ImageHeight.TabIndex = 2;
			numericUpDown_ImageHeight.Value = new decimal(new int[] { 480, 0, 0, 0 });
			numericUpDown_ImageHeight.ValueChanged += UpDownNum_ImageHeight_Changed;
			// 
			// numericUpDown_ImageWidth
			// 
			numericUpDown_ImageWidth.Location = new Point(147, 83);
			numericUpDown_ImageWidth.Maximum = new decimal(new int[] { 1024, 0, 0, 0 });
			numericUpDown_ImageWidth.Minimum = new decimal(new int[] { 16, 0, 0, 0 });
			numericUpDown_ImageWidth.Name = "numericUpDown_ImageWidth";
			numericUpDown_ImageWidth.Size = new Size(69, 31);
			numericUpDown_ImageWidth.TabIndex = 1;
			numericUpDown_ImageWidth.Value = new decimal(new int[] { 640, 0, 0, 0 });
			numericUpDown_ImageWidth.ValueChanged += UpDownNum_ImageWidth_Changed;
			// 
			// comboBox_File
			// 
			comboBox_File.FormattingEnabled = true;
			comboBox_File.Location = new Point(28, 30);
			comboBox_File.Margin = new Padding(4);
			comboBox_File.Name = "comboBox_File";
			comboBox_File.Size = new Size(188, 33);
			comboBox_File.TabIndex = 0;
			comboBox_File.SelectedIndexChanged += ComboBoxFileSelectedIndexChanged;
			// 
			// doubleBufferPanelDrawing
			// 
			doubleBufferPanelDrawing.Dock = DockStyle.Fill;
			doubleBufferPanelDrawing.Location = new Point(256, 0);
			doubleBufferPanelDrawing.Margin = new Padding(4, 5, 4, 5);
			doubleBufferPanelDrawing.Name = "doubleBufferPanelDrawing";
			doubleBufferPanelDrawing.Size = new Size(1322, 864);
			doubleBufferPanelDrawing.TabIndex = 1;
			doubleBufferPanelDrawing.Paint += doubleBufferPanelDrawing_Paint;
			// 
			// Form1
			// 
			AutoScaleDimensions = new SizeF(10F, 25F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(1578, 864);
			Controls.Add(doubleBufferPanelDrawing);
			Controls.Add(panelTools);
			Margin = new Padding(4, 5, 4, 5);
			Name = "Form1";
			Text = "Form1";
			Load += Form1_Load;
			panelTools.ResumeLayout(false);
			panelTools.PerformLayout();
			((System.ComponentModel.ISupportInitialize)numericUpDown_SigmaValue).EndInit();
			((System.ComponentModel.ISupportInitialize)numericUpDown_ImageHeight).EndInit();
			((System.ComponentModel.ISupportInitialize)numericUpDown_ImageWidth).EndInit();
			ResumeLayout(false);
		}

		#endregion

		private Panel panelTools;
        private DoubleBufferPanel doubleBufferPanelDrawing;
		private ComboBox comboBox_File;
		private Label label1;
		private NumericUpDown numericUpDown_ImageHeight;
		private NumericUpDown numericUpDown_ImageWidth;
		private Label label2;
		private CheckBox checkBox_ApplyGaussianBlur;
		private NumericUpDown numericUpDown_SigmaValue;
		private Label label3;
		private CheckBox checkBox_ApplyOtsuThreshold;
		private CheckBox checkBox_ShowHistograms;
		private CheckBox checkBox_ApplySobelEdge;
		private CheckBox checkBox_FitBezierCurve;
		private CheckBox checkBox_ShowMiddleLine;
	}
}
