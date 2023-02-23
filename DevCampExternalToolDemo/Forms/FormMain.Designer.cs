namespace DevCampExternalToolDemo {
  partial class FormMain {
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
      chkShowHiddenDatasetObjects = new CheckBox();
      statusStrip1 = new StatusStrip();
      labelStatusBar = new ToolStripStatusLabel();
      toolStripStatusLabel1 = new ToolStripStatusLabel();
      toolStripStatusLabel2 = new ToolStripStatusLabel();
      imageListForTreeView = new ImageList(components);
      splitContainer = new SplitContainer();
      treeModel = new TreeView();
      propsDatasetObjects = new PropertyGrid();
      lblDatasetObjectName = new Label();
      menuStripMain = new MenuStrip();
      menuDatasetConnection = new ToolStripMenuItem();
      menuCommandConnect = new ToolStripMenuItem();
      menuCommandDisconnect = new ToolStripMenuItem();
      toolStripSeparator1 = new ToolStripSeparator();
      menuDatasetOperations = new ToolStripMenuItem();
      menuCommandExportModel = new ToolStripMenuItem();
      menuCommandGenerateModelSchemaReport = new ToolStripMenuItem();
      menuCommandFormatDatesAsShortDate = new ToolStripMenuItem();
      menuCommandFormatDatesAsLongDate = new ToolStripMenuItem();
      menuCommandFormatDatesWithCustomFormat = new ToolStripMenuItem();
      menuCommandCreateCalendarTable = new ToolStripMenuItem();
      menuCommandExportTable = new ToolStripMenuItem();
      menuCommandGenerateTranslatedReportLabelMatrixMeasures = new ToolStripMenuItem();
      menuCommandGenerateTranslatedReportLabelTableMeasures = new ToolStripMenuItem();
      menuCommandCreateStandardRlsRoles = new ToolStripMenuItem();
      menuCommandDeleteAllRlsRoles = new ToolStripMenuItem();
      menuCommandFormatDAXExpressions = new ToolStripMenuItem();
      panelToolbar = new Panel();
      btnCommandSyncDataModel = new Button();
      chkExpandTableMembers = new CheckBox();
      statusStrip1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
      splitContainer.Panel1.SuspendLayout();
      splitContainer.Panel2.SuspendLayout();
      splitContainer.SuspendLayout();
      menuStripMain.SuspendLayout();
      panelToolbar.SuspendLayout();
      SuspendLayout();
      // 
      // chkShowHiddenDatasetObjects
      // 
      chkShowHiddenDatasetObjects.AutoSize = true;
      chkShowHiddenDatasetObjects.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
      chkShowHiddenDatasetObjects.ForeColor = Color.White;
      chkShowHiddenDatasetObjects.Location = new Point(169, 5);
      chkShowHiddenDatasetObjects.Margin = new Padding(3, 2, 3, 2);
      chkShowHiddenDatasetObjects.Name = "chkShowHiddenDatasetObjects";
      chkShowHiddenDatasetObjects.Size = new Size(180, 17);
      chkShowHiddenDatasetObjects.TabIndex = 24;
      chkShowHiddenDatasetObjects.Text = "Show Hidden Dataset Objects";
      chkShowHiddenDatasetObjects.UseVisualStyleBackColor = true;
      chkShowHiddenDatasetObjects.CheckedChanged += chkShowHiddenDatasetObjects_CheckedChanged;
      // 
      // statusStrip1
      // 
      statusStrip1.BackColor = SystemColors.MenuBar;
      statusStrip1.ImageScalingSize = new Size(20, 20);
      statusStrip1.Items.AddRange(new ToolStripItem[] { labelStatusBar, toolStripStatusLabel1, toolStripStatusLabel2 });
      statusStrip1.Location = new Point(0, 433);
      statusStrip1.Name = "statusStrip1";
      statusStrip1.Size = new Size(1003, 22);
      statusStrip1.TabIndex = 10;
      statusStrip1.Text = "statusStrip1";
      // 
      // labelStatusBar
      // 
      labelStatusBar.Name = "labelStatusBar";
      labelStatusBar.Size = new Size(0, 17);
      // 
      // toolStripStatusLabel1
      // 
      toolStripStatusLabel1.Name = "toolStripStatusLabel1";
      toolStripStatusLabel1.Size = new Size(0, 17);
      // 
      // toolStripStatusLabel2
      // 
      toolStripStatusLabel2.Name = "toolStripStatusLabel2";
      toolStripStatusLabel2.Size = new Size(0, 17);
      // 
      // imageListForTreeView
      // 
      imageListForTreeView.ColorDepth = ColorDepth.Depth8Bit;
      imageListForTreeView.ImageStream = (ImageListStreamer)resources.GetObject("imageListForTreeView.ImageStream");
      imageListForTreeView.TransparentColor = Color.Transparent;
      imageListForTreeView.Images.SetKeyName(0, "Dataset.png");
      imageListForTreeView.Images.SetKeyName(1, "tables.png");
      imageListForTreeView.Images.SetKeyName(2, "table.png");
      imageListForTreeView.Images.SetKeyName(3, "column.png");
      imageListForTreeView.Images.SetKeyName(4, "DateColumn.png");
      imageListForTreeView.Images.SetKeyName(5, "measure.png");
      imageListForTreeView.Images.SetKeyName(6, "folder.png");
      imageListForTreeView.Images.SetKeyName(7, "Hierarchy.png");
      imageListForTreeView.Images.SetKeyName(8, "HierarchyLevel.png");
      imageListForTreeView.Images.SetKeyName(9, "roles.png");
      imageListForTreeView.Images.SetKeyName(10, "role.png");
      // 
      // splitContainer
      // 
      splitContainer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      splitContainer.BackColor = Color.White;
      splitContainer.Location = new Point(0, 52);
      splitContainer.Margin = new Padding(2, 2, 2, 2);
      splitContainer.Name = "splitContainer";
      // 
      // splitContainer.Panel1
      // 
      splitContainer.Panel1.Controls.Add(treeModel);
      // 
      // splitContainer.Panel2
      // 
      splitContainer.Panel2.BackColor = Color.Transparent;
      splitContainer.Panel2.Controls.Add(propsDatasetObjects);
      splitContainer.Panel2.Controls.Add(lblDatasetObjectName);
      splitContainer.Size = new Size(1003, 378);
      splitContainer.SplitterDistance = 375;
      splitContainer.TabIndex = 12;
      // 
      // treeModel
      // 
      treeModel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      treeModel.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
      treeModel.ImageIndex = 0;
      treeModel.ImageList = imageListForTreeView;
      treeModel.Location = new Point(0, 3);
      treeModel.Margin = new Padding(3, 2, 3, 2);
      treeModel.Name = "treeModel";
      treeModel.SelectedImageIndex = 0;
      treeModel.Size = new Size(375, 376);
      treeModel.TabIndex = 12;
      treeModel.NodeMouseClick += treeModel_NodeMouseClick;
      // 
      // propsDatasetObjects
      // 
      propsDatasetObjects.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      propsDatasetObjects.Location = new Point(2, 33);
      propsDatasetObjects.Name = "propsDatasetObjects";
      propsDatasetObjects.Size = new Size(609, 345);
      propsDatasetObjects.TabIndex = 4;
      // 
      // lblDatasetObjectName
      // 
      lblDatasetObjectName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      lblDatasetObjectName.BackColor = Color.Black;
      lblDatasetObjectName.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
      lblDatasetObjectName.ForeColor = Color.FromArgb(255, 255, 192);
      lblDatasetObjectName.Location = new Point(0, 0);
      lblDatasetObjectName.Name = "lblDatasetObjectName";
      lblDatasetObjectName.Size = new Size(612, 27);
      lblDatasetObjectName.TabIndex = 3;
      lblDatasetObjectName.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // menuStripMain
      // 
      menuStripMain.BackColor = Color.LightGray;
      menuStripMain.ImageScalingSize = new Size(20, 20);
      menuStripMain.Items.AddRange(new ToolStripItem[] { menuDatasetConnection, menuDatasetOperations });
      menuStripMain.Location = new Point(0, 0);
      menuStripMain.Name = "menuStripMain";
      menuStripMain.Size = new Size(1003, 24);
      menuStripMain.TabIndex = 13;
      menuStripMain.Text = "menuMain";
      // 
      // menuDatasetConnection
      // 
      menuDatasetConnection.BackColor = Color.Transparent;
      menuDatasetConnection.DropDownItems.AddRange(new ToolStripItem[] { menuCommandConnect, menuCommandDisconnect, toolStripSeparator1 });
      menuDatasetConnection.ForeColor = Color.Black;
      menuDatasetConnection.Name = "menuDatasetConnection";
      menuDatasetConnection.Size = new Size(123, 20);
      menuDatasetConnection.Text = "Dataset Connection";
      // 
      // menuCommandConnect
      // 
      menuCommandConnect.Name = "menuCommandConnect";
      menuCommandConnect.ShortcutKeys = Keys.Control | Keys.Shift | Keys.C;
      menuCommandConnect.Size = new Size(207, 22);
      menuCommandConnect.Text = "Connect...";
      menuCommandConnect.Click += menuCommandConnect_Click;
      // 
      // menuCommandDisconnect
      // 
      menuCommandDisconnect.Name = "menuCommandDisconnect";
      menuCommandDisconnect.ShortcutKeys = Keys.Control | Keys.Shift | Keys.D;
      menuCommandDisconnect.Size = new Size(207, 22);
      menuCommandDisconnect.Text = "Disconnect";
      menuCommandDisconnect.Click += menuCommandDisconnect_Click;
      // 
      // toolStripSeparator1
      // 
      toolStripSeparator1.Name = "toolStripSeparator1";
      toolStripSeparator1.Size = new Size(204, 6);
      // 
      // menuDatasetOperations
      // 
      menuDatasetOperations.DropDownItems.AddRange(new ToolStripItem[] { menuCommandExportModel, menuCommandGenerateModelSchemaReport, menuCommandFormatDatesAsShortDate, menuCommandFormatDatesAsLongDate, menuCommandFormatDatesWithCustomFormat, menuCommandCreateCalendarTable, menuCommandExportTable, menuCommandGenerateTranslatedReportLabelMatrixMeasures, menuCommandGenerateTranslatedReportLabelTableMeasures, menuCommandFormatDAXExpressions, menuCommandCreateStandardRlsRoles, menuCommandDeleteAllRlsRoles });
      menuDatasetOperations.Name = "menuDatasetOperations";
      menuDatasetOperations.Size = new Size(119, 20);
      menuDatasetOperations.Text = "Dataset Operations";
      menuDatasetOperations.DropDownOpening += menuDatasetOperations_DropDownOpening;
      // 
      // menuCommandExportModel
      // 
      menuCommandExportModel.Name = "menuCommandExportModel";
      menuCommandExportModel.ShortcutKeys = Keys.Control | Keys.Shift | Keys.E;
      menuCommandExportModel.Size = new Size(413, 22);
      menuCommandExportModel.Text = "Export Model as Model.bim";
      menuCommandExportModel.Click += menuCommandExportModel_Click;
      // 
      // menuCommandGenerateModelSchemaReport
      // 
      menuCommandGenerateModelSchemaReport.Name = "menuCommandGenerateModelSchemaReport";
      menuCommandGenerateModelSchemaReport.ShortcutKeys = Keys.Control | Keys.Shift | Keys.S;
      menuCommandGenerateModelSchemaReport.Size = new Size(413, 22);
      menuCommandGenerateModelSchemaReport.Text = "Generate Dataset Model Schema Report";
      menuCommandGenerateModelSchemaReport.Click += menuCommandGenerateModelSchemaReport_Click;
      // 
      // menuCommandFormatDatesAsShortDate
      // 
      menuCommandFormatDatesAsShortDate.Name = "menuCommandFormatDatesAsShortDate";
      menuCommandFormatDatesAsShortDate.ShortcutKeys = Keys.Control | Keys.Shift | Keys.D1;
      menuCommandFormatDatesAsShortDate.Size = new Size(413, 22);
      menuCommandFormatDatesAsShortDate.Text = "Format Date Columns using Short Date";
      menuCommandFormatDatesAsShortDate.Click += menuCommandFormatDatesAsShortDate_Click;
      // 
      // menuCommandFormatDatesAsLongDate
      // 
      menuCommandFormatDatesAsLongDate.Name = "menuCommandFormatDatesAsLongDate";
      menuCommandFormatDatesAsLongDate.ShortcutKeys = Keys.Control | Keys.Shift | Keys.D2;
      menuCommandFormatDatesAsLongDate.Size = new Size(413, 22);
      menuCommandFormatDatesAsLongDate.Text = "Format Date Columns using Long Date";
      menuCommandFormatDatesAsLongDate.Click += menuCommandFormatDatesAsLongDate_Click;
      // 
      // menuCommandFormatDatesWithCustomFormat
      // 
      menuCommandFormatDatesWithCustomFormat.Name = "menuCommandFormatDatesWithCustomFormat";
      menuCommandFormatDatesWithCustomFormat.ShortcutKeys = Keys.Control | Keys.Shift | Keys.D3;
      menuCommandFormatDatesWithCustomFormat.Size = new Size(413, 22);
      menuCommandFormatDatesWithCustomFormat.Text = "Format Date Columns using Custom Format";
      menuCommandFormatDatesWithCustomFormat.Click += menuCommandFormatDatesWithCustomFormat_Click;
      // 
      // menuCommandCreateCalendarTable
      // 
      menuCommandCreateCalendarTable.Name = "menuCommandCreateCalendarTable";
      menuCommandCreateCalendarTable.ShortcutKeys = Keys.Control | Keys.Shift | Keys.B;
      menuCommandCreateCalendarTable.Size = new Size(413, 22);
      menuCommandCreateCalendarTable.Text = "Create Calendar Table from Date Column";
      menuCommandCreateCalendarTable.Click += menuCommandCreateCalendarTable_Click;
      // 
      // menuCommandExportTable
      // 
      menuCommandExportTable.Name = "menuCommandExportTable";
      menuCommandExportTable.ShortcutKeys = Keys.Control | Keys.Shift | Keys.X;
      menuCommandExportTable.Size = new Size(413, 22);
      menuCommandExportTable.Text = "Export Table from Data Model As CSV";
      menuCommandExportTable.Click += menuCommandExportTable_Click;
      // 
      // menuCommandGenerateTranslatedReportLabelMatrixMeasures
      // 
      menuCommandGenerateTranslatedReportLabelMatrixMeasures.Name = "menuCommandGenerateTranslatedReportLabelMatrixMeasures";
      menuCommandGenerateTranslatedReportLabelMatrixMeasures.ShortcutKeys = Keys.Control | Keys.Shift | Keys.M;
      menuCommandGenerateTranslatedReportLabelMatrixMeasures.Size = new Size(413, 22);
      menuCommandGenerateTranslatedReportLabelMatrixMeasures.Text = "Generate Translated Report Label Matrix Measures";
      menuCommandGenerateTranslatedReportLabelMatrixMeasures.Click += menuCommandGenerateTranslatedReportLabelMatrixMeasures_Click;
      // 
      // menuCommandGenerateTranslatedReportLabelTableMeasures
      // 
      menuCommandGenerateTranslatedReportLabelTableMeasures.Name = "menuCommandGenerateTranslatedReportLabelTableMeasures";
      menuCommandGenerateTranslatedReportLabelTableMeasures.ShortcutKeys = Keys.Control | Keys.Shift | Keys.T;
      menuCommandGenerateTranslatedReportLabelTableMeasures.Size = new Size(413, 22);
      menuCommandGenerateTranslatedReportLabelTableMeasures.Text = "Generate Translated Report Label Table Measures";
      menuCommandGenerateTranslatedReportLabelTableMeasures.Click += menuCommandGenerateTranslatedReportLabelTableMeasures_Click;
      // 
      // menuCommandCreateStandardRlsRoles
      // 
      menuCommandCreateStandardRlsRoles.Name = "menuCommandCreateStandardRlsRoles";
      menuCommandCreateStandardRlsRoles.ShortcutKeys = Keys.Control | Keys.Shift | Keys.R;
      menuCommandCreateStandardRlsRoles.Size = new Size(413, 22);
      menuCommandCreateStandardRlsRoles.Text = "Create Standard Set of RLS Roles";
      menuCommandCreateStandardRlsRoles.Click += menuCommandCreateStandardRlsRoles_Click;
      // 
      // menuCommandDeleteAllRlsRoles
      // 
      menuCommandDeleteAllRlsRoles.Name = "menuCommandDeleteAllRlsRoles";
      menuCommandDeleteAllRlsRoles.ShortcutKeys = Keys.Control | Keys.Shift | Keys.A;
      menuCommandDeleteAllRlsRoles.Size = new Size(413, 22);
      menuCommandDeleteAllRlsRoles.Text = "Delete All RLS Roles from Data Model";
      menuCommandDeleteAllRlsRoles.Click += menuCommandDeleteAllRlsRoles_Click;
      // 
      // menuCommandFormatDAXExpressions
      // 
      menuCommandFormatDAXExpressions.Name = "menuCommandFormatDAXExpressions";
      menuCommandFormatDAXExpressions.ShortcutKeys = Keys.Control | Keys.Shift | Keys.F;
      menuCommandFormatDAXExpressions.Size = new Size(413, 22);
      menuCommandFormatDAXExpressions.Text = "Format DAX Expressions using DAX Formatter API";
      menuCommandFormatDAXExpressions.Click += menuCommandFormatDAXExpressions_Click;
      // 
      // panelToolbar
      // 
      panelToolbar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      panelToolbar.BackColor = Color.Black;
      panelToolbar.BorderStyle = BorderStyle.FixedSingle;
      panelToolbar.Controls.Add(btnCommandSyncDataModel);
      panelToolbar.Controls.Add(chkExpandTableMembers);
      panelToolbar.Controls.Add(chkShowHiddenDatasetObjects);
      panelToolbar.Location = new Point(0, 24);
      panelToolbar.Name = "panelToolbar";
      panelToolbar.Size = new Size(1002, 28);
      panelToolbar.TabIndex = 14;
      // 
      // btnCommandSyncDataModel
      // 
      btnCommandSyncDataModel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      btnCommandSyncDataModel.BackColor = Color.White;
      btnCommandSyncDataModel.Font = new Font("Segoe UI", 7F, FontStyle.Regular, GraphicsUnit.Point);
      btnCommandSyncDataModel.ForeColor = Color.SaddleBrown;
      btnCommandSyncDataModel.Location = new Point(904, 2);
      btnCommandSyncDataModel.Name = "btnCommandSyncDataModel";
      btnCommandSyncDataModel.Size = new Size(93, 20);
      btnCommandSyncDataModel.TabIndex = 26;
      btnCommandSyncDataModel.Text = "Sync Data Model";
      btnCommandSyncDataModel.UseVisualStyleBackColor = false;
      btnCommandSyncDataModel.Click += btnCommandSyncDataModel_Click;
      // 
      // chkExpandTableMembers
      // 
      chkExpandTableMembers.AutoSize = true;
      chkExpandTableMembers.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
      chkExpandTableMembers.ForeColor = Color.White;
      chkExpandTableMembers.Location = new Point(4, 5);
      chkExpandTableMembers.Margin = new Padding(3, 2, 3, 2);
      chkExpandTableMembers.Name = "chkExpandTableMembers";
      chkExpandTableMembers.Size = new Size(144, 17);
      chkExpandTableMembers.TabIndex = 25;
      chkExpandTableMembers.Text = "Expand Table Members";
      chkExpandTableMembers.UseVisualStyleBackColor = true;
      chkExpandTableMembers.CheckedChanged += chkExpandTableMembers_CheckedChanged;
      // 
      // FormMain
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      BackColor = SystemColors.ActiveBorder;
      ClientSize = new Size(1003, 455);
      Controls.Add(menuStripMain);
      Controls.Add(panelToolbar);
      Controls.Add(splitContainer);
      Controls.Add(statusStrip1);
      Icon = (Icon)resources.GetObject("$this.Icon");
      MainMenuStrip = menuStripMain;
      MinimumSize = new Size(1000, 488);
      Name = "FormMain";
      Text = "External Tool Demo";
      Load += FormMain_Load;
      statusStrip1.ResumeLayout(false);
      statusStrip1.PerformLayout();
      splitContainer.Panel1.ResumeLayout(false);
      splitContainer.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
      splitContainer.ResumeLayout(false);
      menuStripMain.ResumeLayout(false);
      menuStripMain.PerformLayout();
      panelToolbar.ResumeLayout(false);
      panelToolbar.PerformLayout();
      ResumeLayout(false);
      PerformLayout();
    }

    #endregion
    private StatusStrip statusStrip1;
    private ToolStripStatusLabel labelStatusBar;
    private ToolStripStatusLabel toolStripStatusLabel1;
    private ToolStripStatusLabel toolStripStatusLabel2;
    private ImageList imageListForTreeView;
    private SplitContainer splitContainer;
    private TreeView treeModel;
    private MenuStrip menuStripMain;
    private ToolStripMenuItem menuDatasetConnection;
    private ToolStripMenuItem menuCommandConnect;
    private ToolStripMenuItem menuCommandDisconnect;
    private ToolStripSeparator toolStripSeparator1;
    private CheckBox chkShowHiddenDatasetObjects;
    private Panel panelToolbar;
    private CheckBox chkExpandTableMembers;
    private ToolStripMenuItem menuDatasetOperations;
    private ToolStripMenuItem menuCommandFormatDAXExpressions;
    private ToolStripMenuItem menuCommandExportModel;
    private ToolStripMenuItem menuCommandGenerateTranslatedReportLabelMatrixMeasures;
    private ToolStripMenuItem menuCommandGenerateTranslatedReportLabelTableMeasures;
    private PropertyGrid propsDatasetObjects;
    private Label lblDatasetObjectName;
    private Button btnCommandSyncDataModel;
    private ToolStripMenuItem menuCommandGenerateModelSchemaReport;
    private ToolStripMenuItem menuCommandCreateStandardRlsRoles;
    private ToolStripMenuItem menuCommandDeleteAllRlsRoles;
    private ToolStripMenuItem menuCommandCreateCalendarTable;
    private ToolStripMenuItem menuCommandFormatDatesAsShortDate;
    private ToolStripMenuItem menuCommandFormatDatesAsLongDate;
    private ToolStripMenuItem menuCommandFormatDatesWithCustomFormat;
    private ToolStripMenuItem menuCommandExportTable;
  }
}