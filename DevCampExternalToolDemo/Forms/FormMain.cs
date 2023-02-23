using DevCampExternalToolDemo.Forms;
using DevCampExternalToolDemo.Models;
using DevCampExternalToolDemo.Services;
using Microsoft.AnalysisServices.Tabular;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection;
using System.Windows.Forms;

namespace DevCampExternalToolDemo {

  public partial class FormMain : Form {

    public FormMain() {
      InitializeComponent();
      this.Text = GlobalConstants.ApplicationTitle;
    }

    private void FormMain_Load(object sender, EventArgs e) {

      if (TomApi.IsConnected) { LoadModel(); }
      else { labelStatusBar.Text = "Not connected"; }

    }

    public void LoadModel() {
      if (TomApi.IsConnected) {
        labelStatusBar.Text = "Loading Data Model...";
        TomApi.RefreshDataFromServer();
        var model = TomApi.model;
        var DatasetName = TomApi.DatasetName;
        PopulateTree();
        DisplayDatasetInfo();
        SetMenuCommands();
        labelStatusBar.Text = "Data Model loaded successfully";
        this.Text = GlobalConstants.ApplicationName + " | " + DatasetName;
      }
    }

    public void Connect() {

      bool sessionsExist = (PowerBiDesktopUtilities.GetActiveDatasetConnections().Count() > 0);

      if (sessionsExist) {
        using (FormConnect dialog = new FormConnect()) {
          dialog.StartPosition = FormStartPosition.CenterScreen;
          dialog.ShowDialog(this);
          if (dialog.DialogResult == DialogResult.OK && !string.IsNullOrEmpty(dialog.ConnectString)) {
            if (TomApi.IsConnected) {
              TomApi.Disconnect();
              ResetUiAfterDisconected();
            }
            TomApi.Connect(dialog.ConnectString);
            LoadModel();
          }
        }
      }
      else {
        UserInteraction.PromptUserWithInformation("You cannot create a local dataset connection because there are no active sessions of Power BI Desktop.\r\n\r\nStart a session of Power BI Desktop so you can to connect.");
      }

      SetMenuCommands();
    }

    private void SetMenuCommands() {

      menuCommandDisconnect.Enabled = TomApi.IsConnected;
      menuCommandExportModel.Enabled = TomApi.IsConnected;
      menuDatasetOperations.Visible = TomApi.IsConnected;
      panelToolbar.Enabled = TomApi.IsConnected;
      chkExpandTableMembers.Checked = AppSettings.ExpandTableMembers;
      chkShowHiddenDatasetObjects.Checked = AppSettings.ShowHiddenDatasetObjects;
      bool translatedReportLabelsTableExists = TomApi.DoesTableExistInModel(TomApi.TranslatedReportLabelMatrixName);
      menuCommandGenerateTranslatedReportLabelMatrixMeasures.Enabled = translatedReportLabelsTableExists;
      bool translatedReportLabelsByLanguageTableExists = TomApi.DoesTableExistInModel(TomApi.TranslatedReportLabelTableName);
      menuCommandGenerateTranslatedReportLabelTableMeasures.Enabled = translatedReportLabelsByLanguageTableExists;

      bool tableIsSelected = treeModel?.SelectedNode?.Tag?.Equals("Table") ?? false;
      menuCommandExportTable.Enabled = tableIsSelected;

      bool dateColumnIsSelected = treeModel?.SelectedNode?.Tag?.Equals("DateColumn") ?? false;
      menuCommandCreateCalendarTable.Enabled = dateColumnIsSelected;

    }

    private void menuDatasetOperations_DropDownOpening(object sender, EventArgs e) {
      SetMenuCommands();
    }

    public void PopulateTree() {
      if (TomApi.IsConnected) {


        treeModel.Nodes.Clear();

        bool showHiddenDatasetObjects = chkShowHiddenDatasetObjects.Checked;

        var model = TomApi.model;

        TreeNode nodeModel = new TreeNode("Dataset", 0, 0);
        nodeModel.Tag = "Dataset";

        TreeNode nodeTables = new TreeNode("Tables", 1, 1);
        nodeTables.Tag = "Tables";


        foreach (var table in model.Tables) {

          if (!table.IsHidden || showHiddenDatasetObjects) {

            var nodeTable = new TreeNode(table.Name, 2, 2);
            nodeTable.Name = table.Name;
            nodeTable.Tag = "Table";
            nodeTables.Nodes.Add(nodeTable);

            foreach (var column in table.Columns) {

              if (!column.IsHidden || showHiddenDatasetObjects) {
                int nodeColumnIcon = column.DataType == DataType.DateTime ? 4 : 3;
                var nodeColumn = new TreeNode(column.Name, nodeColumnIcon, nodeColumnIcon);
                nodeColumn.Tag = column.DataType == DataType.DateTime ? "DateColumn" : "Column";
                nodeColumn.Name = table.Name + "[" + column.Name + "]";
                string displayFolderName = column.DisplayFolder;
                if (string.IsNullOrEmpty(displayFolderName)) {
                  nodeTable.Nodes.Add(nodeColumn);
                }
                else {
                  TreeNode nodeFolder = nodeTable.Nodes.Find(displayFolderName, true).FirstOrDefault();
                  if (nodeFolder == null) {
                    nodeFolder = new TreeNode(displayFolderName, 6, 6);
                    nodeFolder.Name = displayFolderName;
                    nodeFolder.Tag = "DisplayFolder:" + displayFolderName;
                    nodeTable.Nodes.Add(nodeFolder);
                  }
                  nodeFolder.Nodes.Add(nodeColumn);
                }
              }
            }

            foreach (var measure in table.Measures) {

              if (!measure.IsHidden || showHiddenDatasetObjects) {
                var nodeMeasure = new TreeNode(measure.Name, 5, 5);
                nodeMeasure.Name = table.Name + "[" + measure.Name + "]";
                nodeMeasure.Tag = "Measure";
                string displayFolderName = measure.DisplayFolder;
                if (string.IsNullOrEmpty(displayFolderName)) {
                  nodeTable.Nodes.Add(nodeMeasure);
                }
                else {
                  TreeNode nodeFolder = nodeTable.Nodes.Find(displayFolderName, true).FirstOrDefault();
                  if (nodeFolder == null) {
                    nodeFolder = new TreeNode(displayFolderName, 6, 6);
                    nodeFolder.Name = displayFolderName;
                    nodeFolder.Tag = "DisplayFolder";
                    nodeTable.Nodes.Add(nodeFolder);
                  }
                  nodeFolder.Nodes.Add(nodeMeasure);
                }
              }
            }


            foreach (var hierarchy in table.Hierarchies) {

              if (!hierarchy.IsHidden || showHiddenDatasetObjects) {
                var nodeHierarchy = new TreeNode(hierarchy.Name, 7, 7);
                nodeHierarchy.Name = table.Name + "[" + hierarchy.Name + "]";
                nodeHierarchy.Tag = "Hierarchy";

                foreach (var level in hierarchy.Levels) {
                  var nodeLevel = new TreeNode(level.Name, 8, 8);
                  nodeLevel.Name = table.Name + "[" + hierarchy.Name + "]" + level.Name;
                  nodeLevel.Tag = "Level";
                  nodeHierarchy.Nodes.Add(nodeLevel);
                }

                string displayFolderName = hierarchy.DisplayFolder;
                if (string.IsNullOrEmpty(displayFolderName)) {
                  nodeTable.Nodes.Add(nodeHierarchy);
                }
                else {
                  TreeNode nodeFolder = nodeTable.Nodes.Find(displayFolderName, true).FirstOrDefault();
                  if (nodeFolder == null) {
                    nodeFolder = new TreeNode(displayFolderName, 6, 6);
                    nodeFolder.Name = displayFolderName;
                    nodeFolder.Tag = "DisplayFolder";
                    nodeTable.Nodes.Add(nodeFolder);
                  }
                  nodeFolder.Nodes.Add(nodeHierarchy);
                }
              }
            }
          }
        }

        nodeTables.Expand();
        nodeModel.Nodes.Add(nodeTables);


        if (model.Roles.Count > 0) {
          TreeNode nodeRoles = new TreeNode("Roles", 9, 9);
          nodeTables.Tag = "Roles";
          foreach (ModelRole role in model.Roles) {
            var nodeRole = new TreeNode(role.Name, 10, 10);
            nodeRole.Name = role.Name;
            nodeRole.Tag = "Role";
            nodeRoles.Nodes.Add(nodeRole);
          }
          nodeRoles.Expand();
          nodeModel.Nodes.Add(nodeRoles);
        }

        treeModel.Nodes.Add(nodeModel);
        ExpandTreeView();

      }

    }

    private void ExpandTreeView() {
      if (TomApi.IsConnected) {


        if (chkExpandTableMembers.Checked) {
          treeModel.ExpandAll();
          treeModel.Nodes[0].ExpandAll();
        }
        else {
          treeModel.CollapseAll();
          treeModel.Nodes[0].Expand();
          foreach (TreeNode node in treeModel.Nodes[0].Nodes) {
            node.Expand();
          }

        }
        treeModel.Nodes[0].EnsureVisible();
      }
    }

    public void ResetUiAfterDisconected() {
      treeModel.Nodes.Clear();
      SetMenuCommands();
      this.Text = GlobalConstants.ApplicationTitle;

    }

    private void DisplayDatasetInfo() {
      lblDatasetObjectName.Text = TomApi.DatasetName;
      propsDatasetObjects.SelectedObject = new ModelProperties();
    }

    private void DisplayTableInfo(string TableName) {
      lblDatasetObjectName.Text = TableName;
      Table table = TomApi.model.Tables[TableName];
      propsDatasetObjects.SelectedObject = new TableProperties(table);
    }

    private void DisplayColumnInfo(string ColumnFullName) {
      lblDatasetObjectName.Text = ColumnFullName;
      int splitCharPosition = ColumnFullName.IndexOf("[");
      string tableName = ColumnFullName.Substring(0, splitCharPosition);
      int columnNameLength = ColumnFullName.Length - tableName.Length - 2;
      string columnName = ColumnFullName.Substring(splitCharPosition + 1, columnNameLength);
      Table table = TomApi.model.Tables[tableName];
      Column column = table.Columns[columnName];
      propsDatasetObjects.SelectedObject = new ColumnProperties(column);
    }

    private void DisplayMeasureInfo(string MeasureFullNameName) {
      lblDatasetObjectName.Text = MeasureFullNameName;
      int splitCharPosition = MeasureFullNameName.IndexOf("[");
      string tableName = MeasureFullNameName.Substring(0, splitCharPosition);
      int measureNameLength = MeasureFullNameName.Length - tableName.Length - 2;
      string measureName = MeasureFullNameName.Substring(splitCharPosition + 1, measureNameLength);
      Table table = TomApi.model.Tables[tableName];
      Measure measure = table.Measures[measureName];
      propsDatasetObjects.SelectedObject = new MeasureProperties(measure);
    }

    private void DisplayHierarchyInfo(string HierarchyFullNameName) {
      lblDatasetObjectName.Text = HierarchyFullNameName;
      int splitCharPosition = HierarchyFullNameName.IndexOf("[");
      string tableName = HierarchyFullNameName.Substring(0, splitCharPosition);
      int hierarchyNameLength = HierarchyFullNameName.Length - tableName.Length - 2;
      string hierarchyName = HierarchyFullNameName.Substring(splitCharPosition + 1, hierarchyNameLength);
      Table table = TomApi.model.Tables[tableName];
      Hierarchy hierarchy = table.Hierarchies[hierarchyName];
      propsDatasetObjects.SelectedObject = new HierarchyProperties(hierarchy);
    }

    private void DisplayHierarchyLevelInfo(string HierarchyLevelFullNameName) {
      lblDatasetObjectName.Text = HierarchyLevelFullNameName;
      int splitCharPosition1 = HierarchyLevelFullNameName.IndexOf("[");
      int splitCharPosition2 = HierarchyLevelFullNameName.IndexOf("]");
      string tableName = HierarchyLevelFullNameName.Substring(0, splitCharPosition1);
      int hierarchyNameLength = splitCharPosition2 - splitCharPosition1 - 1;
      string hierarchyName = HierarchyLevelFullNameName.Substring(splitCharPosition1 + 1, hierarchyNameLength);
      int hierarchyLevelNameLength = HierarchyLevelFullNameName.Length - splitCharPosition2 - 1;
      string hierarchyLevelName = HierarchyLevelFullNameName.Substring(splitCharPosition2 + 1, hierarchyLevelNameLength);
      Table table = TomApi.model.Tables[tableName];
      Hierarchy hierarchy = table.Hierarchies[hierarchyName];
      Level hierarchyLevel = hierarchy.Levels[hierarchyLevelName];
      propsDatasetObjects.SelectedObject = new HierarchyLevelProperties(hierarchyLevel);
    }

    private void DisplayRoleInfo(string RoleName) {
      lblDatasetObjectName.Text = RoleName;
      ModelRole role = TomApi.model.Roles[RoleName];
      propsDatasetObjects.SelectedObject = role;
    }

    // event handlers

    private void chkExpandTableMembers_CheckedChanged(object sender, EventArgs e) {
      AppSettings.ExpandTableMembers = chkExpandTableMembers.Checked;
      ExpandTreeView();
    }

    private void chkShowHiddenDatasetObjects_CheckedChanged(object sender, EventArgs e) {
      AppSettings.ShowHiddenDatasetObjects = chkShowHiddenDatasetObjects.Checked;
      PopulateTree();
    }

    private void treeModel_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e) {

      string nodeTag = e.Node.Tag?.ToString();
      string nodeText = e.Node.Text;
      string nodeName = e.Node?.Name;

      string objectType = nodeTag;

      switch (objectType) {
        case "Dataset":
          DisplayDatasetInfo();
          break;
        case "Table":
          DisplayTableInfo(nodeName);
          break;
        case "Column":
        case "DateColumn":
          DisplayColumnInfo(nodeName);
          break;
        case "Measure":
          DisplayMeasureInfo(nodeName);
          break;
        case "Hierarchy":
          DisplayHierarchyInfo(nodeName);
          break;
        case "Level":
          DisplayHierarchyLevelInfo(nodeName);
          break;
        case "Role":
          DisplayRoleInfo(nodeName);
          break;
      }

      SetMenuCommands();

    }

    private void menuCommandConnect_Click(object sender, EventArgs e) {
      Connect();
    }

    private void menuCommandDisconnect_Click(object sender, EventArgs e) {
      TomApi.Disconnect();
      ResetUiAfterDisconected();
    }

    private void menuCommandExportModel_Click(object sender, EventArgs e) {
      TomApi.ExportModelAsBim();
    }

    private void menuCommandFormatDAXExpressions_Click(object sender, EventArgs e) {
      TomApi.FormatDaxForModel(TomApi.model, labelStatusBar);
    }

    private void menuCommandGenerateModelSchemaReport_Click(object sender, EventArgs e) {
      TomApi.GenerateModelSchemaReport();
    }

    private void menuCommandFormatDatesAsShortDate_Click(object sender, EventArgs e) {
      TomApi.FormatDates("Short Date");
    }

    private void menuCommandFormatDatesAsLongDate_Click(object sender, EventArgs e) {
      TomApi.FormatDates("Long Date");
    }

    private void menuCommandFormatDatesWithCustomFormat_Click(object sender, EventArgs e) {
      TomApi.FormatDates("MMM d, yyyy");
    }

    private void menuCommandCreateCalendarTable_Click(object sender, EventArgs e) {

      bool dateColumnIsSelected = treeModel?.SelectedNode?.Tag?.Equals("DateColumn") ?? false;

      if (dateColumnIsSelected) {
        string ColumnFullName = treeModel.SelectedNode.Name;
        int splitCharPosition = ColumnFullName.IndexOf("[");
        string tableName = ColumnFullName.Substring(0, splitCharPosition);
        int columnNameLength = ColumnFullName.Length - tableName.Length - 2;
        string columnName = ColumnFullName.Substring(splitCharPosition + 1, columnNameLength);
        Table table = TomApi.model.Tables[tableName];
        Column column = table.Columns[columnName];
        TomApi.CreateCalendarTable(column);

      }
      else {
        UserInteraction.PromptUserWithError("Cannot create Calendar table unless Date column is selected");
      }

      PopulateTree();
    }

    private void menuCommandGenerateTranslatedReportLabelMatrixMeasures_Click(object sender, EventArgs e) {

      labelStatusBar.Text = "Creating Translated Report Label Matrix measures...";
      TomApi.GenerateTranslatedReportLabelMatrixMeasures();
      LoadModel();
      labelStatusBar.Text = "[Translated Report Label Matrix] measures generated at " + DateTime.Now.ToShortTimeString();

    }

    private void menuCommandGenerateTranslatedReportLabelTableMeasures_Click(object sender, EventArgs e) {

      labelStatusBar.Text = "Creating Translated Report Label Table measures...";
      TomApi.GenerateTranslatedReportLabelTableMeasures();
      LoadModel();
      labelStatusBar.Text = "[Translated Report Label Table] measures generated at " + DateTime.Now.ToShortTimeString();

    }

    private void menuCommandCreateStandardRlsRoles_Click(object sender, EventArgs e) {
      TomApi.CreateStandardRlsRoles();
      labelStatusBar.Text = "Standard RLS Roles Created";
      PopulateTree();
    }

    private void menuCommandDeleteAllRlsRoles_Click(object sender, EventArgs e) {
      TomApi.DeleteAllRlsRoles();
      PopulateTree();
    }

    private void btnCommandSyncDataModel_Click(object sender, EventArgs e) {
      LoadModel();
    }

    private void menuCommandExportTable_Click(object sender, EventArgs e) {

      bool tableIsSelected = treeModel?.SelectedNode?.Tag?.Equals("Table") ?? false;

      if (tableIsSelected) {
        string tableName = treeModel.SelectedNode.Name;
        Table table = TomApi.model.Tables[tableName];
        TomApi.ExportTableAsCsv(table);
      }
      else {
        UserInteraction.PromptUserWithError("Cannot export anything other than a table.");
      }

      PopulateTree();
    }

  }
}
