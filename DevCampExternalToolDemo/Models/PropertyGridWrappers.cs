using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Microsoft.AnalysisServices.Tabular;
using DevCampExternalToolDemo.Services;
using System.Data.Common;

namespace DevCampExternalToolDemo.Models {

  [DefaultProperty("Name")]
  public class ModelProperties {

    [Category("Connection Properties"),
      Description("Connection string to dataset")]
    public string Server { get; }

    [Category("Database Properties"),
     Description("Name of PBIX file")]
    public string Name { get; }

    [Category("Database Properties"),
      Description("ID of dataset")]
    public string ID { get; }

    [Category("Database Properties"),
     Description("Language code for dataset")]
    public string Language { get; }

    [Category("Database Properties"),
     Description("Compatibility Level")]
    public string CompatibilityLevel { get; }

    [Category("Database Properties"),
     Description("Date/Time of Last Update to dataset")]
    public string LastUpdate { get; }

    [Category("Database Properties"),
      Description("Date/Time of Last Update to dataset schema")]
    public string LastSchemaUpdate { get; }

    [Category("Model Properties"),
      Description("Social Security Number of the customer")]
    public string Culture { get; }

    public ModelProperties() {
      this.Server = AppSettings.Server;
      this.Name = TomApi.DatasetName;
      this.ID = TomApi.database.ID;
      this.Language = TomApi.database.Language.ToString();
      this.CompatibilityLevel = TomApi.database.CompatibilityLevel.ToString();
      this.LastUpdate = TomApi.database.LastUpdate.ToString();
      this.LastSchemaUpdate = TomApi.database.LastSchemaUpdate.ToString();
      this.Culture = TomApi.model.Culture;
    }
  }

  [DefaultProperty("Name")]
  public class TableProperties {

    [Category("Table Properties"),
     Description("Table Name ")]
    public string Name { get; }

    [Category("Table Properties"),
      Description("Lineage Tag")]
    public string LineageTag { get; }

    [Category("Table Properties"),
      Description("Object Type")]
    public string ObjectType { get; }

    [Category("Table Properties"),
      Description("Modified Time ")]
    public DateTime ModifiedTime { get; }

    [Category("Table Properties"),
      Description("Is Hidden")]
    public bool IsHidden { get; }

    [Category("Table Properties"),
      Description("Is Private")]
    public bool IsPrivate { get; }

    public TableProperties(Table table) {
      this.Name = table.Name;
      this.IsPrivate = table.IsPrivate;
      this.IsHidden = table.IsHidden;
      this.LineageTag = table.LineageTag;
      this.ObjectType = table.ObjectType.ToString();
      this.ModifiedTime = table.ModifiedTime;
    }

  }

  [DefaultProperty("Name")]
  public class ColumnProperties {

    [Category("Column Properties"),
     Description("Column Name ")]
    public string Name { get; }

    [Category("Column Properties"),
    Description("Lineage Tag")]
    public string LineageTag { get; }

    [Category("Column Properties"),
      Description("Object Type")]
    public string ObjectType { get; }

    [Category("Column Properties"),
      Description("Modified Time ")]
    public DateTime ModifiedTime { get; }

    [Category("Column Properties"),
      Description("Is Hidden")]
    public bool IsHidden { get; }

    [Category("Column Properties"),
      Description("Is Private")]
    public bool IsPrivate { get; }

    [Category("Column Properties"),
      Description("Alignment")]
    public string Alignment { get; }

    [Category("Column Properties"),
      Description("DataType")]
    public string DataType { get; }

    [Category("Column Properties"),
      Description("DataCategory")]
    public string DataCategory { get; }

    [Category("Column Properties"),
      Description("Display Folder")]
    public string DisplayFolder { get; }

    [Category("Column Properties"),
      Description("IsKey")]
    public bool IsKey { get; }

    [Category("Column Properties"),
      Description("SummarizeBy")]
    public string SummarizeBy { get; }

    [Category("Column Properties"),
      Description("IsNullable")]
    public bool IsNullable { get; }

    [Category("Column Properties"),
      Description("IsUnique")]
    public bool IsUnique { get; }

    [Category("Column Properties"),
      Description("SortByColumn")]
    public string SortByColumn { get; }

    [Category("Column Properties"),
      Description("FormatString")]
    public string FormatString { get; }

    public ColumnProperties(Column column) {
      this.Name = column.Name;
      this.IsHidden = column.IsHidden;
      this.IsNullable = column.IsNullable;
      this.IsUnique = column.IsUnique;
      this.Alignment = column.Alignment.ToString();
      this.LineageTag = column.LineageTag;
      this.ObjectType = column.ObjectType.ToString();
      this.ModifiedTime = column.ModifiedTime;
      this.DataType = column.DataType.ToString();
      this.DataCategory = column.DataCategory;
      this.DisplayFolder = column.DisplayFolder;
      this.SummarizeBy = column.SummarizeBy.ToString();
      this.SortByColumn = column.SortByColumn?.Name;
      this.FormatString = column.FormatString;
    }
  }

  [DefaultProperty("Name")]
  public class MeasureProperties {

    [Category("Measure Properties"),
     Description("Measure Name ")]
    public string Name { get; }

    [Category("Measure Properties"),
    Description("Lineage Tag")]
    public string LineageTag { get; }

    [Category("Measure Properties"),
      Description("Object Type")]
    public string ObjectType { get; }

    [Category("Measure Properties"),
      Description("Modified Time ")]
    public DateTime ModifiedTime { get; }

    [Category("Measure Properties"),
      Description("Is Hidden")]
    public bool IsHidden { get; }

    [Category("Measure Properties"),
      Description("Is Private")]
    public bool IsPrivate { get; }

    [Category("Measure Properties"),
      Description("DataType")]
    public string DataType { get; }

    [Category("Measure Properties"),
      Description("DataCategory")]
    public string DataCategory { get; }

    [Category("Measure Properties"),
      Description("Display Folder")]
    public string DisplayFolder { get; }

    [Category("Measure Properties"),
      Description("FormatString")]
    public string FormatString { get; }

    [Category("Measure Properties"),
     Description("FormatString")]
    public string Expression { get; }

    public MeasureProperties(Measure measure) {
      this.Name = measure.Name;
      this.IsHidden = measure.IsHidden;
      this.LineageTag = measure.LineageTag;
      this.ObjectType = measure.ObjectType.ToString();
      this.ModifiedTime = measure.ModifiedTime;
      this.DataType = measure.DataType.ToString();
      this.DataCategory = measure.DataCategory;
      this.DisplayFolder = measure.DisplayFolder;
      this.FormatString = measure.FormatString;
      this.Expression = measure.Expression;
    }

  }

  [DefaultProperty("Name")]
  public class HierarchyProperties {

    [Category("Hierarchy Properties"),
     Description("Hierarchy Name ")]
    public string Name { get; }

    [Category("Hierarchy Properties"),
    Description("Lineage Tag")]
    public string LineageTag { get; }

    [Category("Hierarchy Properties"),
      Description("Object Type")]
    public string ObjectType { get; }

    [Category("Hierarchy Properties"),
      Description("Is Hidden")]
    public bool IsHidden { get; }

    [Category("Hierarchy Properties"),
      Description("Display Folder")]
    public string DisplayFolder { get; }

    public HierarchyProperties(Hierarchy hierarchy) {
      this.Name = hierarchy.Name;
      this.IsHidden = hierarchy.IsHidden;
      this.LineageTag = hierarchy.LineageTag;
      this.ObjectType = hierarchy.ObjectType.ToString();
      this.DisplayFolder = hierarchy.DisplayFolder;
    }
  }

  [DefaultProperty("Name")]
  public class HierarchyLevelProperties {

    [Category("Hierarchy Level Properties"),
     Description("Hierarchy Name ")]
    public string Name { get; }

    [Category("Hierarchy Level Properties"),
    Description("Lineage Tag")]
    public string LineageTag { get; }

    [Category("Hierarchy Level Properties"),
      Description("Object Type")]
    public string ObjectType { get; }

    [Category("Hierarchy Level Properties"),
      Description("Ordinal")]
    public int Ordinal { get; }

    public HierarchyLevelProperties(Level level) {
      this.Name = level.Name;
      this.LineageTag = level.LineageTag;
      this.ObjectType = level.ObjectType.ToString();
      this.Ordinal = level.Ordinal;
    }
  }

}

