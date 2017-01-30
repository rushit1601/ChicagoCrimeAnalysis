namespace ChicagoCrime
{
  partial class Form1
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
      this.cmdByYear = new System.Windows.Forms.Button();
      this.graphPanel = new System.Windows.Forms.Panel();
      this.txtFilename = new System.Windows.Forms.TextBox();
      this.cmdArrestPercentages = new System.Windows.Forms.Button();
      this.cmdByCrime = new System.Windows.Forms.Button();
      this.txtCrimeCode = new System.Windows.Forms.TextBox();
      this.cmdByArea = new System.Windows.Forms.Button();
      this.cmdChicagoAreas = new System.Windows.Forms.Button();
      this.txtArea = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // cmdByYear
      // 
      this.cmdByYear.Location = new System.Drawing.Point(12, 12);
      this.cmdByYear.Name = "cmdByYear";
      this.cmdByYear.Size = new System.Drawing.Size(95, 62);
      this.cmdByYear.TabIndex = 0;
      this.cmdByYear.Text = "By Year";
      this.cmdByYear.UseVisualStyleBackColor = true;
      this.cmdByYear.Click += new System.EventHandler(this.cmdByYear_Click);
      // 
      // graphPanel
      // 
      this.graphPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.graphPanel.BackColor = System.Drawing.Color.White;
      this.graphPanel.Location = new System.Drawing.Point(123, 12);
      this.graphPanel.Name = "graphPanel";
      this.graphPanel.Size = new System.Drawing.Size(769, 454);
      this.graphPanel.TabIndex = 1;
      // 
      // txtFilename
      // 
      this.txtFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtFilename.BackColor = System.Drawing.SystemColors.Control;
      this.txtFilename.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtFilename.Location = new System.Drawing.Point(123, 474);
      this.txtFilename.Name = "txtFilename";
      this.txtFilename.Size = new System.Drawing.Size(769, 26);
      this.txtFilename.TabIndex = 2;
      this.txtFilename.Text = "Crimes-2013-2015.csv";
      // 
      // cmdArrestPercentages
      // 
      this.cmdArrestPercentages.Location = new System.Drawing.Point(12, 101);
      this.cmdArrestPercentages.Name = "cmdArrestPercentages";
      this.cmdArrestPercentages.Size = new System.Drawing.Size(95, 62);
      this.cmdArrestPercentages.TabIndex = 3;
      this.cmdArrestPercentages.Text = "Arrest %";
      this.cmdArrestPercentages.UseVisualStyleBackColor = true;
      this.cmdArrestPercentages.Click += new System.EventHandler(this.cmdArrestPercentages_Click);
      // 
      // cmdByCrime
      // 
      this.cmdByCrime.Location = new System.Drawing.Point(12, 188);
      this.cmdByCrime.Name = "cmdByCrime";
      this.cmdByCrime.Size = new System.Drawing.Size(95, 62);
      this.cmdByCrime.TabIndex = 4;
      this.cmdByCrime.Text = "By Crime";
      this.cmdByCrime.UseVisualStyleBackColor = true;
      this.cmdByCrime.Click += new System.EventHandler(this.cmdByCrime_Click);
      // 
      // txtCrimeCode
      // 
      this.txtCrimeCode.Location = new System.Drawing.Point(12, 256);
      this.txtCrimeCode.Name = "txtCrimeCode";
      this.txtCrimeCode.Size = new System.Drawing.Size(95, 29);
      this.txtCrimeCode.TabIndex = 5;
      this.txtCrimeCode.Text = "0820";
      // 
      // cmdByArea
      // 
      this.cmdByArea.Location = new System.Drawing.Point(12, 314);
      this.cmdByArea.Name = "cmdByArea";
      this.cmdByArea.Size = new System.Drawing.Size(95, 62);
      this.cmdByArea.TabIndex = 6;
      this.cmdByArea.Text = "By Area";
      this.cmdByArea.UseVisualStyleBackColor = true;
      this.cmdByArea.Click += new System.EventHandler(this.cmdByArea_Click);
      // 
      // cmdChicagoAreas
      // 
      this.cmdChicagoAreas.Location = new System.Drawing.Point(12, 438);
      this.cmdChicagoAreas.Name = "cmdChicagoAreas";
      this.cmdChicagoAreas.Size = new System.Drawing.Size(95, 62);
      this.cmdChicagoAreas.TabIndex = 7;
      this.cmdChicagoAreas.Text = "Chicago";
      this.cmdChicagoAreas.UseVisualStyleBackColor = true;
      this.cmdChicagoAreas.Click += new System.EventHandler(this.cmdChicagoAreas_Click);
      // 
      // txtArea
      // 
      this.txtArea.Location = new System.Drawing.Point(12, 382);
      this.txtArea.Name = "txtArea";
      this.txtArea.Size = new System.Drawing.Size(95, 29);
      this.txtArea.TabIndex = 8;
      this.txtArea.Text = "Loop";
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(904, 508);
      this.Controls.Add(this.txtArea);
      this.Controls.Add(this.cmdChicagoAreas);
      this.Controls.Add(this.cmdByArea);
      this.Controls.Add(this.txtCrimeCode);
      this.Controls.Add(this.cmdByCrime);
      this.Controls.Add(this.cmdArrestPercentages);
      this.Controls.Add(this.txtFilename);
      this.Controls.Add(this.graphPanel);
      this.Controls.Add(this.cmdByYear);
      this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Margin = new System.Windows.Forms.Padding(6);
      this.Name = "Form1";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Chicago Crime Analysis";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button cmdByYear;
    private System.Windows.Forms.Panel graphPanel;
    private System.Windows.Forms.TextBox txtFilename;
    private System.Windows.Forms.Button cmdArrestPercentages;
    private System.Windows.Forms.Button cmdByCrime;
    private System.Windows.Forms.TextBox txtCrimeCode;
    private System.Windows.Forms.Button cmdByArea;
    private System.Windows.Forms.Button cmdChicagoAreas;
    private System.Windows.Forms.TextBox txtArea;
  }
}

