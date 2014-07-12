namespace Solar_Magic_Advance
{
    partial class eCoinEditor
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.GFX = new System.Windows.Forms.PictureBox();
            this.PAL = new System.Windows.Forms.PictureBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.numericUpDownFloor = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownPos = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.pictureBoxSelected = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.GFX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PAL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFloor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSelected)).BeginInit();
            this.SuspendLayout();
            // 
            // GFX
            // 
            this.GFX.Location = new System.Drawing.Point(2, 2);
            this.GFX.Name = "GFX";
            this.GFX.Size = new System.Drawing.Size(192, 192);
            this.GFX.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.GFX.TabIndex = 0;
            this.GFX.TabStop = false;
            this.GFX.MouseClick += new System.Windows.Forms.MouseEventHandler(this.GFX_MouseClick);
            this.GFX.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GFX_MouseDown);
            this.GFX.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GFX_MouseMove);
            // 
            // PAL
            // 
            this.PAL.Location = new System.Drawing.Point(2, 200);
            this.PAL.Name = "PAL";
            this.PAL.Size = new System.Drawing.Size(192, 48);
            this.PAL.TabIndex = 1;
            this.PAL.TabStop = false;
            this.PAL.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PAL_MouseClick);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(2, 283);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(119, 283);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 3;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // numericUpDownFloor
            // 
            this.numericUpDownFloor.Location = new System.Drawing.Point(71, 259);
            this.numericUpDownFloor.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDownFloor.Name = "numericUpDownFloor";
            this.numericUpDownFloor.Size = new System.Drawing.Size(26, 20);
            this.numericUpDownFloor.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 261);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Floor:";
            // 
            // numericUpDownPos
            // 
            this.numericUpDownPos.Location = new System.Drawing.Point(149, 259);
            this.numericUpDownPos.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numericUpDownPos.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownPos.Name = "numericUpDownPos";
            this.numericUpDownPos.Size = new System.Drawing.Size(35, 20);
            this.numericUpDownPos.TabIndex = 6;
            this.numericUpDownPos.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(99, 261);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Position:";
            // 
            // pictureBoxSelected
            // 
            this.pictureBoxSelected.Location = new System.Drawing.Point(2, 255);
            this.pictureBoxSelected.Name = "pictureBoxSelected";
            this.pictureBoxSelected.Size = new System.Drawing.Size(24, 24);
            this.pictureBoxSelected.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxSelected.TabIndex = 8;
            this.pictureBoxSelected.TabStop = false;
            // 
            // eCoinEditor
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(196, 309);
            this.Controls.Add(this.pictureBoxSelected);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numericUpDownPos);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDownFloor);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.PAL);
            this.Controls.Add(this.GFX);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "eCoinEditor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Edit eCoin";
            ((System.ComponentModel.ISupportInitialize)(this.GFX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PAL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFloor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSelected)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox GFX;
        private System.Windows.Forms.PictureBox PAL;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.NumericUpDown numericUpDownFloor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownPos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.PictureBox pictureBoxSelected;
    }
}