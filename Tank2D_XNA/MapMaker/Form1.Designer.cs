namespace MapMaker
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.Map = new System.Windows.Forms.PictureBox();
            this.EntityType = new System.Windows.Forms.GroupBox();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.FileName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Save = new System.Windows.Forms.Button();
            this.SW = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SH = new System.Windows.Forms.TextBox();
            this.SetAttr = new System.Windows.Forms.Button();
            this.Zoom = new System.Windows.Forms.DomainUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.Clear = new System.Windows.Forms.Button();
            this.Load = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Map)).BeginInit();
            this.EntityType.SuspendLayout();
            this.SuspendLayout();
            // 
            // Map
            // 
            this.Map.Location = new System.Drawing.Point(12, 12);
            this.Map.Name = "Map";
            this.Map.Size = new System.Drawing.Size(867, 626);
            this.Map.TabIndex = 0;
            this.Map.TabStop = false;
            this.Map.Paint += new System.Windows.Forms.PaintEventHandler(this.Map_Paint);
            this.Map.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Map_MouseClick);
            this.Map.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Map_MouseDown);
            this.Map.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Map_MouseMove);
            this.Map.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Map_MouseUp);
            // 
            // EntityType
            // 
            this.EntityType.Controls.Add(this.radioButton4);
            this.EntityType.Controls.Add(this.radioButton3);
            this.EntityType.Controls.Add(this.radioButton2);
            this.EntityType.Controls.Add(this.radioButton1);
            this.EntityType.Location = new System.Drawing.Point(918, 25);
            this.EntityType.Name = "EntityType";
            this.EntityType.Size = new System.Drawing.Size(242, 145);
            this.EntityType.TabIndex = 1;
            this.EntityType.TabStop = false;
            this.EntityType.Text = "Entity Types";
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(6, 106);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(62, 21);
            this.radioButton4.TabIndex = 3;
            this.radioButton4.TabStop = true;
            this.radioButton4.Tag = "3";
            this.radioButton4.Text = "Clear";
            this.radioButton4.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(6, 78);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(63, 21);
            this.radioButton3.TabIndex = 2;
            this.radioButton3.Tag = "2";
            this.radioButton3.Text = "Block";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(6, 51);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(41, 21);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.Tag = "1";
            this.radioButton2.Text = "AI";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(6, 24);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(69, 21);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Tag = "0";
            this.radioButton1.Text = "Player";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // FileName
            // 
            this.FileName.Location = new System.Drawing.Point(1007, 529);
            this.FileName.Name = "FileName";
            this.FileName.Size = new System.Drawing.Size(153, 22);
            this.FileName.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(920, 532);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Output File:";
            // 
            // Save
            // 
            this.Save.Location = new System.Drawing.Point(961, 572);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(155, 47);
            this.Save.TabIndex = 4;
            this.Save.Text = "Save";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // SW
            // 
            this.SW.Location = new System.Drawing.Point(1012, 190);
            this.SW.Name = "SW";
            this.SW.Size = new System.Drawing.Size(147, 22);
            this.SW.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(914, 193);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Screen Width";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(914, 221);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "Screen Height";
            // 
            // SH
            // 
            this.SH.Location = new System.Drawing.Point(1012, 218);
            this.SH.Name = "SH";
            this.SH.Size = new System.Drawing.Size(147, 22);
            this.SH.TabIndex = 7;
            // 
            // SetAttr
            // 
            this.SetAttr.Location = new System.Drawing.Point(961, 316);
            this.SetAttr.Name = "SetAttr";
            this.SetAttr.Size = new System.Drawing.Size(155, 47);
            this.SetAttr.TabIndex = 15;
            this.SetAttr.Text = "Set Attributes";
            this.SetAttr.UseVisualStyleBackColor = true;
            this.SetAttr.Click += new System.EventHandler(this.SetAttr_Click);
            // 
            // Zoom
            // 
            this.Zoom.Items.Add("10");
            this.Zoom.Items.Add("11");
            this.Zoom.Items.Add("12");
            this.Zoom.Items.Add("13");
            this.Zoom.Items.Add("14");
            this.Zoom.Items.Add("15");
            this.Zoom.Items.Add("16");
            this.Zoom.Items.Add("17");
            this.Zoom.Items.Add("18");
            this.Zoom.Items.Add("19");
            this.Zoom.Items.Add("20");
            this.Zoom.Items.Add("21");
            this.Zoom.Items.Add("22");
            this.Zoom.Items.Add("23");
            this.Zoom.Items.Add("24");
            this.Zoom.Items.Add("25");
            this.Zoom.Items.Add("26");
            this.Zoom.Items.Add("27");
            this.Zoom.Location = new System.Drawing.Point(1040, 246);
            this.Zoom.Name = "Zoom";
            this.Zoom.Size = new System.Drawing.Size(120, 22);
            this.Zoom.TabIndex = 16;
            this.Zoom.Text = "20";
            this.Zoom.SelectedItemChanged += new System.EventHandler(this.Zoom_SelectedItemChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(988, 248);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 17);
            this.label4.TabIndex = 17;
            this.label4.Text = "Zoom";
            // 
            // Clear
            // 
            this.Clear.Location = new System.Drawing.Point(961, 369);
            this.Clear.Name = "Clear";
            this.Clear.Size = new System.Drawing.Size(155, 49);
            this.Clear.TabIndex = 19;
            this.Clear.Text = "Clear Field";
            this.Clear.UseVisualStyleBackColor = true;
            this.Clear.Click += new System.EventHandler(this.Clear_Click);
            // 
            // Load
            // 
            this.Load.Location = new System.Drawing.Point(961, 424);
            this.Load.Name = "Load";
            this.Load.Size = new System.Drawing.Size(155, 49);
            this.Load.TabIndex = 20;
            this.Load.Text = "Load Field";
            this.Load.UseVisualStyleBackColor = true;
            this.Load.Click += new System.EventHandler(this.Load_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1188, 651);
            this.Controls.Add(this.Load);
            this.Controls.Add(this.Clear);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Zoom);
            this.Controls.Add(this.SetAttr);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.SH);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.SW);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.FileName);
            this.Controls.Add(this.EntityType);
            this.Controls.Add(this.Map);
            this.Name = "Form1";
            this.Text = "MapMaker";
            ((System.ComponentModel.ISupportInitialize)(this.Map)).EndInit();
            this.EntityType.ResumeLayout(false);
            this.EntityType.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Map;
        private System.Windows.Forms.GroupBox EntityType;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.TextBox FileName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.TextBox SW;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox SH;
        private System.Windows.Forms.Button SetAttr;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.DomainUpDown Zoom;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button Clear;
        private System.Windows.Forms.Button Load;
    }
}

