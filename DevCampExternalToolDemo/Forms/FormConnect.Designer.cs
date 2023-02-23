namespace DevCampExternalToolDemo.Forms {
  partial class FormConnect {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormConnect));
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnConnect = new System.Windows.Forms.Button();
      this.lstSessions = new System.Windows.Forms.ListBox();
      this.SuspendLayout();
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(534, 63);
      this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(107, 39);
      this.btnCancel.TabIndex = 5;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      // 
      // btnConnect
      // 
      this.btnConnect.Location = new System.Drawing.Point(534, 16);
      this.btnConnect.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.btnConnect.Name = "btnConnect";
      this.btnConnect.Size = new System.Drawing.Size(107, 39);
      this.btnConnect.TabIndex = 4;
      this.btnConnect.Text = "Connect";
      this.btnConnect.UseVisualStyleBackColor = true;
      this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
      // 
      // lstSessions
      // 
      this.lstSessions.FormattingEnabled = true;
      this.lstSessions.ItemHeight = 20;
      this.lstSessions.Location = new System.Drawing.Point(14, 16);
      this.lstSessions.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.lstSessions.Name = "lstSessions";
      this.lstSessions.Size = new System.Drawing.Size(502, 244);
      this.lstSessions.TabIndex = 3;
      this.lstSessions.SelectedIndexChanged += new System.EventHandler(this.lstSessions_SelectedIndexChanged);
      this.lstSessions.DoubleClick += new System.EventHandler(this.lstSessions_DoubleClick);
      this.lstSessions.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstSessions_KeyDown);
      // 
      // FormConnect
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(683, 283);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnConnect);
      this.Controls.Add(this.lstSessions);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.Name = "FormConnect";
      this.Text = "Connect to a Dataset in Power BI Desktop";
      this.ResumeLayout(false);

    }

    #endregion

    private Button btnCancel;
    private Button btnConnect;
    private ListBox lstSessions;
  }
}