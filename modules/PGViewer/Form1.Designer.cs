
namespace cv1
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
			label2 = new Label();
			label1 = new Label();
			numericUpDown2 = new NumericUpDown();
			numericUpDown1 = new NumericUpDown();
			comboBox1 = new ComboBox();
			doubleBufferPanelDrawing = new DoubleBufferPanel();
			panelTools.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)numericUpDown2).BeginInit();
			((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
			SuspendLayout();
			// 
			// panelTools
			// 
			panelTools.BackColor = SystemColors.AppWorkspace;
			panelTools.Controls.Add(label2);
			panelTools.Controls.Add(label1);
			panelTools.Controls.Add(numericUpDown2);
			panelTools.Controls.Add(numericUpDown1);
			panelTools.Controls.Add(comboBox1);
			panelTools.Dock = DockStyle.Left;
			panelTools.Location = new Point(0, 0);
			panelTools.Margin = new Padding(4, 5, 4, 5);
			panelTools.Name = "panelTools";
			panelTools.Size = new Size(256, 841);
			panelTools.TabIndex = 0;
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
			label1.Location = new Point(30, 89);
			label1.Name = "label1";
			label1.Size = new Size(111, 25);
			label1.TabIndex = 3;
			label1.Text = "Image width";
			// 
			// numericUpDown2
			// 
			numericUpDown2.Location = new Point(147, 133);
			numericUpDown2.Maximum = new decimal(new int[] { 1024, 0, 0, 0 });
			numericUpDown2.Minimum = new decimal(new int[] { 16, 0, 0, 0 });
			numericUpDown2.Name = "numericUpDown2";
			numericUpDown2.Size = new Size(69, 31);
			numericUpDown2.TabIndex = 2;
			numericUpDown2.Value = new decimal(new int[] { 512, 0, 0, 0 });
			numericUpDown2.ValueChanged += numericUpDown2_ValueChanged;
			// 
			// numericUpDown1
			// 
			numericUpDown1.Location = new Point(147, 83);
			numericUpDown1.Maximum = new decimal(new int[] { 1024, 0, 0, 0 });
			numericUpDown1.Minimum = new decimal(new int[] { 16, 0, 0, 0 });
			numericUpDown1.Name = "numericUpDown1";
			numericUpDown1.Size = new Size(69, 31);
			numericUpDown1.TabIndex = 1;
			numericUpDown1.Value = new decimal(new int[] { 512, 0, 0, 0 });
			numericUpDown1.ValueChanged += numericUpDown1_ValueChanged;
			// 
			// comboBox1
			// 
			comboBox1.FormattingEnabled = true;
			comboBox1.Location = new Point(28, 30);
			comboBox1.Margin = new Padding(4);
			comboBox1.Name = "comboBox1";
			comboBox1.Size = new Size(188, 33);
			comboBox1.TabIndex = 0;
			comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
			// 
			// doubleBufferPanelDrawing
			// 
			doubleBufferPanelDrawing.Dock = DockStyle.Fill;
			doubleBufferPanelDrawing.Location = new Point(256, 0);
			doubleBufferPanelDrawing.Margin = new Padding(4, 5, 4, 5);
			doubleBufferPanelDrawing.Name = "doubleBufferPanelDrawing";
			doubleBufferPanelDrawing.Size = new Size(1006, 841);
			doubleBufferPanelDrawing.TabIndex = 1;
			doubleBufferPanelDrawing.Paint += doubleBufferPanelDrawing_Paint;
			doubleBufferPanelDrawing.MouseDown += doubleBufferPanelDrawing_MouseDown;
			doubleBufferPanelDrawing.MouseMove += doubleBufferPanelDrawing_MouseMove;
			doubleBufferPanelDrawing.MouseUp += doubleBufferPanelDrawing_MouseUp;
			// 
			// Form1
			// 
			AutoScaleDimensions = new SizeF(10F, 25F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(1262, 841);
			Controls.Add(doubleBufferPanelDrawing);
			Controls.Add(panelTools);
			Margin = new Padding(4, 5, 4, 5);
			Name = "Form1";
			Text = "Form1";
			Load += Form1_Load;
			panelTools.ResumeLayout(false);
			panelTools.PerformLayout();
			((System.ComponentModel.ISupportInitialize)numericUpDown2).EndInit();
			((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
			ResumeLayout(false);
		}

		#endregion

		private Panel panelTools;
        private DoubleBufferPanel doubleBufferPanelDrawing;
		private ComboBox comboBox1;
		private Label label1;
		private NumericUpDown numericUpDown2;
		private NumericUpDown numericUpDown1;
		private Label label2;
	}
}
