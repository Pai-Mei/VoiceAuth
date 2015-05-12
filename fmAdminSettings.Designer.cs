namespace VoiceAuth
{
	partial class fmAdminSettings
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fmAdminSettings));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.btnAdd = new System.Windows.Forms.ToolStripButton();
			this.btnRemove = new System.Windows.Forms.ToolStripButton();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.pictureBoxSpectr = new System.Windows.Forms.PictureBox();
			this.comboBoxDevice = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.numericRecords = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.comboBoxFrameSize = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.numericMels = new System.Windows.Forms.NumericUpDown();
			this.numericUpDownBufferSize = new System.Windows.Forms.NumericUpDown();
			this.label5 = new System.Windows.Forms.Label();
			this.numericUpDownError = new System.Windows.Forms.NumericUpDown();
			this.numericMaxCount = new System.Windows.Forms.NumericUpDown();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxSpectr)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericRecords)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericMels)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownBufferSize)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownError)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericMaxCount)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.listBox1);
			this.groupBox1.Controls.Add(this.toolStrip1);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(272, 360);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Пользователи";
			// 
			// listBox1
			// 
			this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listBox1.FormattingEnabled = true;
			this.listBox1.Location = new System.Drawing.Point(3, 41);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(266, 316);
			this.listBox1.TabIndex = 1;
			this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAdd,
            this.btnRemove,
            this.toolStripButton1});
			this.toolStrip1.Location = new System.Drawing.Point(3, 16);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(266, 25);
			this.toolStrip1.TabIndex = 0;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// btnAdd
			// 
			this.btnAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
			this.btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(23, 22);
			this.btnAdd.Text = "Добавить";
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// btnRemove
			// 
			this.btnRemove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnRemove.Image = ((System.Drawing.Image)(resources.GetObject("btnRemove.Image")));
			this.btnRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnRemove.Name = "btnRemove";
			this.btnRemove.Size = new System.Drawing.Size(23, 22);
			this.btnRemove.Text = "Удалить";
			this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
			// 
			// toolStripButton1
			// 
			this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
			this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
			this.toolStripButton1.Text = "Записать голос";
			this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
			// 
			// splitContainer1
			// 
			this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
			this.splitContainer1.Panel1MinSize = 200;
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.label7);
			this.splitContainer1.Panel2.Controls.Add(this.label6);
			this.splitContainer1.Panel2.Controls.Add(this.numericMaxCount);
			this.splitContainer1.Panel2.Controls.Add(this.numericUpDownError);
			this.splitContainer1.Panel2.Controls.Add(this.label5);
			this.splitContainer1.Panel2.Controls.Add(this.numericUpDownBufferSize);
			this.splitContainer1.Panel2.Controls.Add(this.numericMels);
			this.splitContainer1.Panel2.Controls.Add(this.label4);
			this.splitContainer1.Panel2.Controls.Add(this.comboBoxFrameSize);
			this.splitContainer1.Panel2.Controls.Add(this.label3);
			this.splitContainer1.Panel2.Controls.Add(this.numericRecords);
			this.splitContainer1.Panel2.Controls.Add(this.label2);
			this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
			this.splitContainer1.Panel2.Controls.Add(this.comboBoxDevice);
			this.splitContainer1.Panel2.Controls.Add(this.label1);
			this.splitContainer1.Panel2MinSize = 300;
			this.splitContainer1.Size = new System.Drawing.Size(714, 362);
			this.splitContainer1.SplitterDistance = 274;
			this.splitContainer1.TabIndex = 2;
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.pictureBoxSpectr);
			this.groupBox2.Location = new System.Drawing.Point(3, 183);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(420, 166);
			this.groupBox2.TabIndex = 6;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Спектр";
			// 
			// pictureBoxSpectr
			// 
			this.pictureBoxSpectr.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pictureBoxSpectr.Location = new System.Drawing.Point(3, 16);
			this.pictureBoxSpectr.Name = "pictureBoxSpectr";
			this.pictureBoxSpectr.Size = new System.Drawing.Size(414, 147);
			this.pictureBoxSpectr.TabIndex = 0;
			this.pictureBoxSpectr.TabStop = false;
			// 
			// comboBoxDevice
			// 
			this.comboBoxDevice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxDevice.FormattingEnabled = true;
			this.comboBoxDevice.Location = new System.Drawing.Point(150, 5);
			this.comboBoxDevice.Name = "comboBoxDevice";
			this.comboBoxDevice.Size = new System.Drawing.Size(273, 21);
			this.comboBoxDevice.TabIndex = 1;
			this.comboBoxDevice.SelectedIndexChanged += new System.EventHandler(this.comboBoxDevice_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(5, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(141, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Устройство записи звука:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(5, 60);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(146, 13);
			this.label2.TabIndex = 7;
			this.label2.Text = "Кол-во эталонных записей:";
			// 
			// numericRecords
			// 
			this.numericRecords.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.numericRecords.Location = new System.Drawing.Point(149, 58);
			this.numericRecords.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.numericRecords.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
			this.numericRecords.Name = "numericRecords";
			this.numericRecords.Size = new System.Drawing.Size(271, 20);
			this.numericRecords.TabIndex = 8;
			this.numericRecords.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(5, 89);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(92, 13);
			this.label3.TabIndex = 9;
			this.label3.Text = "Размер фрейма:";
			// 
			// comboBoxFrameSize
			// 
			this.comboBoxFrameSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxFrameSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxFrameSize.FormattingEnabled = true;
			this.comboBoxFrameSize.Items.AddRange(new object[] {
            "256",
            "512",
            "1024",
            "2048",
            "4096",
            "8192",
            "16384",
            "32765",
            ""});
			this.comboBoxFrameSize.Location = new System.Drawing.Point(149, 86);
			this.comboBoxFrameSize.Name = "comboBoxFrameSize";
			this.comboBoxFrameSize.Size = new System.Drawing.Size(271, 21);
			this.comboBoxFrameSize.TabIndex = 10;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(5, 115);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(121, 13);
			this.label4.TabIndex = 11;
			this.label4.Text = "Кол-во MEL-фильтров:";
			this.label4.Click += new System.EventHandler(this.label4_Click);
			// 
			// numericMels
			// 
			this.numericMels.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.numericMels.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.numericMels.Location = new System.Drawing.Point(149, 113);
			this.numericMels.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            0});
			this.numericMels.Name = "numericMels";
			this.numericMels.Size = new System.Drawing.Size(271, 20);
			this.numericMels.TabIndex = 12;
			this.numericMels.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
			// 
			// numericUpDownBufferSize
			// 
			this.numericUpDownBufferSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.numericUpDownBufferSize.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.numericUpDownBufferSize.Location = new System.Drawing.Point(150, 32);
			this.numericUpDownBufferSize.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
			this.numericUpDownBufferSize.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.numericUpDownBufferSize.Name = "numericUpDownBufferSize";
			this.numericUpDownBufferSize.Size = new System.Drawing.Size(270, 20);
			this.numericUpDownBufferSize.TabIndex = 13;
			this.numericUpDownBufferSize.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(5, 34);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(138, 13);
			this.label5.TabIndex = 14;
			this.label5.Text = "Размер аудиобуфера(мс):";
			// 
			// numericUpDownError
			// 
			this.numericUpDownError.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.numericUpDownError.DecimalPlaces = 2;
			this.numericUpDownError.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
			this.numericUpDownError.Location = new System.Drawing.Point(149, 139);
			this.numericUpDownError.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numericUpDownError.Name = "numericUpDownError";
			this.numericUpDownError.Size = new System.Drawing.Size(271, 20);
			this.numericUpDownError.TabIndex = 15;
			this.numericUpDownError.UseWaitCursor = true;
			this.numericUpDownError.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
			// 
			// numericMaxCount
			// 
			this.numericMaxCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.numericMaxCount.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.numericMaxCount.Location = new System.Drawing.Point(149, 165);
			this.numericMaxCount.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numericMaxCount.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            0});
			this.numericMaxCount.Name = "numericMaxCount";
			this.numericMaxCount.Size = new System.Drawing.Size(271, 20);
			this.numericMaxCount.TabIndex = 16;
			this.numericMaxCount.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(5, 141);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(99, 13);
			this.label6.TabIndex = 17;
			this.label6.Text = "Ошибка обучения:";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(5, 167);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(143, 13);
			this.label7.TabIndex = 18;
			this.label7.Text = "Макс. кол-во циклов обуч.:";
			// 
			// fmAdminSettings
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(714, 362);
			this.Controls.Add(this.splitContainer1);
			this.MinimumSize = new System.Drawing.Size(730, 400);
			this.Name = "fmAdminSettings";
			this.Text = "Администрирование";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.fmAdminSettings_FormClosing);
			this.Load += new System.EventHandler(this.fmAdminSettings_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxSpectr)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericRecords)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericMels)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownBufferSize)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownError)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericMaxCount)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton btnAdd;
		private System.Windows.Forms.ToolStripButton btnRemove;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.ComboBox comboBoxDevice;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.PictureBox pictureBoxSpectr;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private System.Windows.Forms.NumericUpDown numericRecords;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown numericMels;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox comboBoxFrameSize;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.NumericUpDown numericUpDownBufferSize;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.NumericUpDown numericMaxCount;
		private System.Windows.Forms.NumericUpDown numericUpDownError;
	}
}