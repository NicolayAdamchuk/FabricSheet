namespace ScheduleSplit.CS
{
    partial class ScheduleSplitForm
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
            this.buttonSplit = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.comboBoxScheduler = new System.Windows.Forms.ComboBox();
            this.comboBoxTitle = new System.Windows.Forms.ComboBox();
            this.numericUpDown = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonSplit
            // 
            this.buttonSplit.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonSplit.Location = new System.Drawing.Point(114, 369);
            this.buttonSplit.Name = "buttonSplit";
            this.buttonSplit.Size = new System.Drawing.Size(207, 45);
            this.buttonSplit.TabIndex = 0;
            this.buttonSplit.Text = "Split";
            this.buttonSplit.UseVisualStyleBackColor = true;
            this.buttonSplit.Click += new System.EventHandler(this.ButtonSplit_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(475, 369);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(204, 45);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // comboBoxScheduler
            // 
            this.comboBoxScheduler.FormattingEnabled = true;
            this.comboBoxScheduler.Location = new System.Drawing.Point(36, 164);
            this.comboBoxScheduler.Name = "comboBoxScheduler";
            this.comboBoxScheduler.Size = new System.Drawing.Size(285, 28);
            this.comboBoxScheduler.TabIndex = 2;
            this.comboBoxScheduler.SelectedIndexChanged += new System.EventHandler(this.ComboBoxScheduler_SelectedIndexChanged);
            // 
            // comboBoxTitle
            // 
            this.comboBoxTitle.FormattingEnabled = true;
            this.comboBoxTitle.Location = new System.Drawing.Point(355, 164);
            this.comboBoxTitle.Name = "comboBoxTitle";
            this.comboBoxTitle.Size = new System.Drawing.Size(285, 28);
            this.comboBoxTitle.TabIndex = 3;
            this.comboBoxTitle.SelectedIndexChanged += new System.EventHandler(this.ComboBoxTitle_SelectedIndexChanged);
            // 
            // numericUpDown
            // 
            this.numericUpDown.Location = new System.Drawing.Point(671, 164);
            this.numericUpDown.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDown.Name = "numericUpDown";
            this.numericUpDown.Size = new System.Drawing.Size(67, 26);
            this.numericUpDown.TabIndex = 4;
            this.numericUpDown.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // ScheduleSplitForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.numericUpDown);
            this.Controls.Add(this.comboBoxTitle);
            this.Controls.Add(this.comboBoxScheduler);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSplit);
            this.Name = "ScheduleSplitForm";
            this.Text = "ScheduleSplitForm";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonSplit;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ComboBox comboBoxScheduler;
        private System.Windows.Forms.ComboBox comboBoxTitle;
        private System.Windows.Forms.NumericUpDown numericUpDown;
    }
}