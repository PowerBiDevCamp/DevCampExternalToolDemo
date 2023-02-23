using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.AnalysisServices.Tabular;
using AMO = Microsoft.AnalysisServices;
using AdomdClient = Microsoft.AnalysisServices.AdomdClient;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Forms;
using System.Reflection.Emit;
using System.Data.Common;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Linq;
using DevCampExternalToolDemo.Models;
using System.Runtime.CompilerServices;
using System.Net.Http.Headers;
using System.Security.Cryptography;

namespace DevCampExternalToolDemo.Services {

  public class TomApi {

    private static Server server = new Server();

    public static Database database;
    public static Model model;
    public static bool IsConnected = false;

    private static DatasetConnection ActiveConnection;

    static TomApi() {

      var sessions = PowerBiDesktopUtilities.GetActiveDatasetConnections();

      if (!string.IsNullOrEmpty(AppSettings.Server)) {
        var session = sessions.Find((s) => s.ConnectString == AppSettings.Server);
        if (session != null) {
          server.Connect(AppSettings.Server);
          database = server.Databases[0];
          model = database.Model;
          IsConnected = true;
          ActiveConnection = session;
          SetDatasetAnnotation();
          return;
        }
      }

      AppSettings.Server = "";
      AppSettings.Database = "";
      IsConnected = false;
      ActiveConnection = null;

    }

    public static string DatasetName {
      get {
        return TomApi.ActiveConnection.DatasetName;
      }
    }

    public static string ConnectString {
      get {
        return ActiveConnection.ConnectString;
      }
    }

    public static void RefreshDataFromServer() {
      try {
        model.Sync(new SyncOptions { DiscardLocalChanges = false });
      }
      catch { }
    }

    public static void Connect(string ConnectString) {

      if (IsConnected) {
        server.Disconnect(true);
      }

      server.Connect(ConnectString);
      database = server.Databases[0];
      model = database.Model;
      IsConnected = true;
      AppSettings.Server = ConnectString;

      SetDatasetAnnotation();

      var sessions = PowerBiDesktopUtilities.GetActiveDatasetConnections();
      ActiveConnection = sessions.Find((s) => s.ConnectString == AppSettings.Server);

    }

    public static void Disconnect() {
      IsConnected = false;
      ActiveConnection = null;
      server.Disconnect(true);
    }

    public static bool DoesTableExistInModel(string TableName) {
      return model.Tables.Find(TableName) != null;
    }

    public static void EnumerateDatasetObjects() {

      Model model = TomApi.model;

      foreach (var table in model.Tables) {
        // read or update table objects

        foreach (var column in table.Columns) {
          // read or update column objects
        }

        foreach (var measure in table.Measures) {
          // read or update measure objects
        }

        foreach (var hierarchy in table.Hierarchies) {
          // read or update hierarchy objects

          foreach (var level in hierarchy.Levels) {
            // read or update hierarchy level objects
          }
        }

      }
    }


    // demo code starts here

    public static void ExportModelAsBim() {

      // create file path in Documents folder
      string folderPathMyDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
      string filePath = folderPathMyDocuments + @"/" + ActiveConnection.DatasetName.Replace(".pbix", "") + ".model.bim";

      // convert model into JSON format for export
      string fileContent = JsonSerializer.SerializeDatabase(database, new SerializeOptions {
        IgnoreTimestamps = true,
        IgnoreInferredProperties = true,
        IgnoreInferredObjects = true,
      });

      // create a file, write JSON content into it and then save
      StreamWriter writer = new StreamWriter(File.Open(filePath, FileMode.Create), Encoding.UTF8);
      writer.Write(fileContent);
      writer.Flush();
      writer.Dispose();

      // open model.bim file in Notepad
      ProcessStartInfo startInfo = new ProcessStartInfo();
      startInfo.FileName = "Notepad.exe";
      startInfo.Arguments = filePath;
      Process.Start(startInfo);

    }


    public static void SetDatasetAnnotation() {

      string annotationName = "DevCamExternalToolDemo";
      string annotationValue = "Last access at " + DateTime.Now.ToString();

      if (model.Annotations.Contains(annotationName)) {
        // update annotation if it already exists
        model.Annotations[annotationName].Value = annotationValue;
      }
      else {
        // create annotation if it doesn't exists
        model.Annotations.Add(new Annotation {
          Name = annotationName,
          Value = annotationValue
        });
      }

      model.SaveChanges();

    }

  
    public static void GenerateModelSchemaReport() {

      string folderPathMyDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
      string filePath = folderPathMyDocuments + @"/" + ActiveConnection.DatasetName + ".SchemaReport.txt";
      StreamWriter writer = new StreamWriter(File.Open(filePath, FileMode.Create), Encoding.UTF8);

      var model = TomApi.model;

      writer.WriteLine("Table Schema Report for " + DatasetName);
      writer.WriteLine("--------------------------------------------------");

      foreach (var table in model.Tables) {
        writer.WriteLine("");
        writer.WriteLine(string.Format("{0} (Hidden={1})", table.Name, table.IsHidden));

        if (table.Columns.Count > 0) {
          writer.WriteLine("- Columns:");
          foreach (var column in table.Columns) {
            if (!column.Name.Contains("RowNumber")) {
              string columnDescription = string.Format("   - {0} ( DataType={1}, Hidden={2}", 
                                                       column.Name, column.DataType, column.IsHidden);
              if(!string.IsNullOrEmpty(column.FormatString)) {
                columnDescription += string.Format(", FormatString={0}", column.FormatString);
              }
              columnDescription += " )";
              writer.WriteLine(columnDescription);
            }
          }          
        }

        if (table.Measures.Count > 0) {
          writer.WriteLine("- Measures:");
          foreach (var measure in table.Measures) {
            string measureDescription = string.Format("   - {0} ( DataType={1}, Hidden={2}", 
                                                      measure.Name, measure.DataType, measure.IsHidden);
            if (!string.IsNullOrEmpty(measure.FormatString)) {
              measureDescription += string.Format(", FormatString={0}", measure.FormatString);
            }
            measureDescription += ")";
            writer.WriteLine(measureDescription);
          }
        }

        if (table.Hierarchies.Count > 0) {
          writer.WriteLine("- Hierarchies:");
          foreach (var hierarchy in table.Hierarchies) {
            string hierarchyDescription = string.Format("   - {0} (Hidden={1})", hierarchy.Name, hierarchy.IsHidden);
            writer.WriteLine(hierarchyDescription);
            foreach (var level in hierarchy.Levels) {
              string levelDescription = string.Format("     - Level {0}: {1}", level.Ordinal, level.Name);
              writer.WriteLine(levelDescription);
            }
          }
        }

      }

      writer.Flush();
      writer.Dispose();

      ProcessStartInfo startInfo = new ProcessStartInfo();
      startInfo.FileName = "Notepad.exe";
      startInfo.Arguments = filePath;
      Process.Start(startInfo);
    }

    public static void FormatDates(string FormatString) {

      foreach (var table in model.Tables) {

        foreach (var column in table.Columns) {
          if (column.DataType == DataType.DateTime) {
            column.FormatString = FormatString;
          }
        }

        foreach (var measure in table.Measures) {
          if (measure.DataType == DataType.DateTime) {
            measure.FormatString = FormatString;
          }
        }

        model.SaveChanges();
      }

    }

    public static void CreateCalendarTable(Column DateColumn) {

      string calendarTableName = "Calendar";
      string displayFolderName = "Time Periods";

      // delete calendar table if it exists
      if (model.Tables.Contains(calendarTableName)) {
        // delete any relationships with calendar table
        foreach (var relationship in model.Relationships) {
          if (relationship.ToTable.Name == calendarTableName || relationship.FromTable.Name == calendarTableName) {
            model.Relationships.Remove(relationship);
          }
        }
        model.Tables.Remove(model.Tables[calendarTableName]);
        model.SaveChanges();
      }

      string FullColumnName = DateColumn.Table.Name + "[" + DateColumn.Name + "]";

      Table tableCalendar = new Table() {
        Name = "Calendar",
        Description = "Calendar Table",
        Partitions = {
            new Partition() {
                Name = "All Dates",
                Mode = ModeType.Import,
                Source = new CalculatedPartitionSource {
                  Expression = Properties.Resources.CreateCalendarTable_dax.Replace("@DateColumnName", FullColumnName)
                }
            }
        }
      };

      model.Tables.Add(tableCalendar);
      model.RequestRefresh(RefreshType.Calculate);
      model.SaveChanges();

      tableCalendar.DataCategory = "Time";
      tableCalendar.Columns["Date"].Annotations.Add(new Annotation { Name = "UnderlyingDateTimeDataType", Value = "Date" });
      tableCalendar.Columns["Date"].DisplayFolder = displayFolderName;
      tableCalendar.Columns["Year"].SummarizeBy = AggregateFunction.None;
      tableCalendar.Columns["Year"].DisplayFolder = displayFolderName;
      tableCalendar.Columns["Quarter"].DisplayFolder = displayFolderName;
      tableCalendar.Columns["Month"].SortByColumn = tableCalendar.Columns["MonthSort"];
      tableCalendar.Columns["Month"].DisplayFolder = displayFolderName;
      tableCalendar.Columns["MonthSort"].IsHidden = true;
      tableCalendar.Columns["MonthSort"].DisplayFolder = "Sort Columns";
      tableCalendar.Columns["Day of Week"].SortByColumn = tableCalendar.Columns["DaySort"];
      tableCalendar.Columns["Day of Week"].DisplayFolder = displayFolderName;
      tableCalendar.Columns["DaySort"].IsHidden = true;
      tableCalendar.Columns["DaySort"].DisplayFolder = "Sort Columns";

      tableCalendar.Hierarchies.Add(
      new Hierarchy() {
        Name = "Calendar Drilldown",
        Levels = {
              new Level() { Ordinal=0, Name="Year", Column=tableCalendar.Columns["Year"]  },
              new Level() { Ordinal=1, Name="Quarter", Column=tableCalendar.Columns["Quarter"] },
              new Level() { Ordinal=2, Name="Month", Column=tableCalendar.Columns["Month"] },
              new Level() { Ordinal=3, Name="Date", Column=tableCalendar.Columns["Date"] }
          }
      });

      model.RequestRefresh(RefreshType.Calculate);
      model.SaveChanges();

      model.Relationships.Add(new SingleColumnRelationship {
        Name = "Calendar Date",
        ToColumn = tableCalendar.Columns["Date"],
        ToCardinality = RelationshipEndCardinality.One,
        FromColumn = DateColumn,
        FromCardinality = RelationshipEndCardinality.Many
      });

      model.RequestRefresh(RefreshType.Calculate);
      model.SaveChanges();

    }

    public static void ExportTableAsCsv(Table TableToExport) {

      string tableName = TableToExport.Name;
      List<string> columnLongNames = new List<string>();
      List<string> columnShortNames = new List<string>();

      foreach (var column in TableToExport.Columns) {
        if (!column.Name.Contains("RowNumber")) {
          columnLongNames.Add("'" + tableName + "'[" + column.Name + "]");
          columnShortNames.Add(column.Name);
        }
      }

      int columnCount = columnLongNames.Count();

      // DAX query header
      string DaxQuery = @"EVALUATE SUMMARIZECOLUMNS( " + string.Join(", ", columnLongNames.ToArray()) + " )";

      // execute DAX query
      AdomdClient.AdomdConnection adomdConnection = new AdomdClient.AdomdConnection("DataSource=" + AppSettings.Server);
      adomdConnection.Open();
      AdomdClient.AdomdCommand adomdCommand = new AdomdClient.AdomdCommand(DaxQuery, adomdConnection);
      AdomdClient.AdomdDataReader Reader = adomdCommand.ExecuteReader();

      // generate text content for CSV file
      string linebreak = "\r\n";
      StringBuilder csvContentBuffer = new StringBuilder();

      csvContentBuffer.Append(string.Join(",", columnShortNames.ToArray()) + linebreak);

      // Create a loop for every row in the resultset
      while (Reader.Read()) {
        List<string> rowValues = new List<string>();
        for (int columnIndex = 0; columnIndex < columnCount; columnIndex++) {
          rowValues.Add(Reader.GetValue(columnIndex).ToString().Replace(",", "."));
        }
        csvContentBuffer.Append(string.Join(",", rowValues) + linebreak);
      }

      Reader.Close();
      adomdCommand.Dispose();
      adomdConnection.Close();

      string folderPathMyDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
      string filePath = folderPathMyDocuments + @"/" + tableName + ".csv";

      StreamWriter writer = new StreamWriter(File.Open(filePath, FileMode.Create), Encoding.UTF8);
      writer.Write(csvContentBuffer.ToString());
      writer.Flush();
      writer.Dispose();

      ExcelUtilities.OpenCsvInExcel(filePath);

    }

    // well-known table names to look for in data model
    public const string TranslatedReportLabelMatrixName = "Translated Report Label Matrix";
    public const string TranslatedReportLabelTableName = "Translated Report Label Table";

    public static void GenerateTranslatedReportLabelMatrixMeasures() {

      Console.WriteLine(" - Executing GenerateReportLabelMeasures...");

      Table reportLabelsTable = model.Tables[TranslatedReportLabelMatrixName];

      foreach (var column in reportLabelsTable.Columns) {
        if (column.IsHidden == false) {
          reportLabelsTable.Columns[column.Name].DisplayFolder = "Columns";
        }
      }

      model.SaveChanges();

      foreach (var measure in reportLabelsTable.Measures) {
        reportLabelsTable.Measures.Remove(measure);
        model.SaveChanges();
      }

      AdomdClient.AdomdConnection adomdConnection = new AdomdClient.AdomdConnection("DataSource=" + AppSettings.Server);
      adomdConnection.Open();

      // DAX query header
      string DaxQuery = @"EVALUATE SUMMARIZECOLUMNS( '" + TranslatedReportLabelMatrixName + "'[Label]";
      // DAX query body
      foreach (var column in reportLabelsTable.Columns) {
        if (column.Name.Length == 2) {
          DaxQuery += ", '" + TranslatedReportLabelMatrixName + "'[" + column.Name + "]";
        }
      }
      // DAX query fotter
      DaxQuery += " )";

      // execute DAX query
      AdomdClient.AdomdCommand adomdCommand = new AdomdClient.AdomdCommand(DaxQuery, adomdConnection);
      AdomdClient.AdomdDataReader Reader = adomdCommand.ExecuteReader();

      string defaultLanguage = Reader.GetName(1);
      List<string> secondaryLanguagesList = new List<string>();

      for (int col = 2; col < Reader.FieldCount; col++) {
        secondaryLanguagesList.Add(Reader.GetName(col).Replace(TranslatedReportLabelMatrixName, "").Replace("[", "").Replace("]", ""));
      }

      string[] secondaryLanguages = secondaryLanguagesList.ToArray();

      // Create a loop for every row in the resultset
      while (Reader.Read()) {
        string label = Reader.GetValue(0).ToString();

        string defaultranslation = Reader.GetValue(1).ToString();

        List<(string language, string value)> secondaryLanguageTranslations = new List<(string, string)>();

        for (int col = 2; col < Reader.FieldCount; col++) {
          secondaryLanguageTranslations.Add((language: secondaryLanguages[col - 2], value: Reader.GetValue(col).ToString()));
        }

        reportLabelsTable.Measures.Add(new Measure {
          Name = label + " (Static)",
          Expression = GetReportLabelStaticMeasureDax(defaultranslation, secondaryLanguageTranslations),
          DisplayFolder = "Static Measures"
        });

        reportLabelsTable.Measures.Add(new Measure {
          Name = label + " (Dynamic)",
          Expression = GetReportLabelDynamicMeasureDax(label, defaultranslation, secondaryLanguages),
          DisplayFolder = "Dynamic Measures"
        });
      }

      Reader.Close();

      model.SaveChanges();

    }

    public static void GenerateTranslatedReportLabelTableMeasures() {

      Table reportLabelsByLanguageTable = model.Tables[TranslatedReportLabelTableName];

      foreach (var column in reportLabelsByLanguageTable.Columns) {
        if (column.IsHidden == false) {
          reportLabelsByLanguageTable.Columns[column.Name].DisplayFolder = "Columns";
        }
      }

      model.SaveChanges();

      foreach (var measure in reportLabelsByLanguageTable.Measures) {
        reportLabelsByLanguageTable.Measures.Remove(measure);
        model.SaveChanges();
      }

      AdomdClient.AdomdConnection adomdConnection = new AdomdClient.AdomdConnection("DataSource=" + AppSettings.Server);
      adomdConnection.Open();

      string DaxQuery = "EVALUATE SUMMARIZECOLUMNS('" + TranslatedReportLabelTableName + "'[Label])";

      AdomdClient.AdomdCommand adomdCommand = new AdomdClient.AdomdCommand(DaxQuery, adomdConnection);
      AdomdClient.AdomdDataReader Reader = adomdCommand.ExecuteReader();

      while (Reader.Read()) {

        string label = Reader.GetValue(0).ToString();
        Console.WriteLine(label);

        reportLabelsByLanguageTable.Measures.Add(new Measure {
          Name = label,
          Expression = GetReportLabelTableMeasureDax(TranslatedReportLabelTableName, label),
        });

        model.SaveChanges();
      }

    }

      #region "Utility Methods for Generating Measures"

    public static string GetReportLabelStaticMeasureDax(string defaultTranslation, List<(string language, string value)> secondaryTranslations) {

      if (secondaryTranslations.Count == 0) {
        return @"""" + defaultTranslation + @"""";
      }

      string lineBreak = "\r\n";
      string measureExpression = "SWITCH(LEFT(USERCULTURE(), 2)," + lineBreak;

      foreach (var translation in secondaryTranslations) {
        measureExpression += @"   """ + translation.language + @""", """ + translation.value + @"""," + lineBreak;
      }

      measureExpression += @"   """ + defaultTranslation + @"""" + lineBreak;
      measureExpression += ")";
      return measureExpression;

    }

    public static string GetReportLabelDynamicMeasureDax(string labelName, string defaultTranslation, string[] secondaryLanguages) {

      string lineBreak = "\r\n";
      string measureExpression = "SWITCH(LEFT(USERCULTURE(), 2)," + lineBreak;

      foreach (var language in secondaryLanguages) {
        measureExpression += @"""" + language + @""", LOOKUPVALUE('" +
                                                          TranslatedReportLabelMatrixName + "'[" + language + @"], '" +
                                                          TranslatedReportLabelMatrixName + @"'[Label], """ + labelName + @""")," + lineBreak;
      }

      measureExpression += @"LOOKUPVALUE('" +
                               TranslatedReportLabelMatrixName + @"'[en],'" +
                               TranslatedReportLabelMatrixName + @"'[Label], """ + labelName + @""")" + lineBreak;

      measureExpression += ")";
      return measureExpression;
    }

    public static string GetReportLabelTableMeasureDax(string tableName, string labelName) {

      string lineBreak = "\r\n";
      string measureExpression = "LOOKUPVALUE(" + lineBreak +
                                 "  '" + tableName + "'[Value]," + lineBreak +
                                 "  '" + tableName + "'[Language]," + lineBreak +
                                 "   LEFT(USERCULTURE(), 2)," + lineBreak +
                                 "  '" + tableName + "'[Label]," + lineBreak +
                                @"  ""{0}"", " + lineBreak +
                                 "  LOOKUPVALUE(" + lineBreak +
                                 "    '" + tableName + "'[Value]," + lineBreak +
                                 "    '" + tableName + "'[Language]," + lineBreak +
                                @"    ""en""," + lineBreak +
                                 "    '" + tableName + "'[Label]," + lineBreak +
                                @"    ""{0}""" + lineBreak +
                                 "  )" + lineBreak +
                                 ")";


      return string.Format(measureExpression, labelName);
    }

    #endregion

    public static void FormatDaxForModel(Model model, ToolStripStatusLabel statusLabel) {

      TomApi.RefreshDataFromServer();

      statusLabel.Text = "Iterating measures, calculated columns and calulated tables for DAX formatting...";

      int formattedDaxExpressionsCount = 0;

      foreach (Table table in model.Tables) {

        // check which measures require formatting
        foreach (var measure in table.Measures) {
          if (MeasureRequiresFormatting(measure)) {
            formattedDaxExpressionsCount += 1;
            statusLabel.Text = "Formatting DAX for measure: " + table.Name + "[" + measure.Name + "]";
            string expressionOwner = measure.Name;
            string originalDaxExpression = measure.Expression;
            string formattedDaxExpression = FormatDaxExpression(originalDaxExpression, expressionOwner);
            // write formatted expression back to measure
            measure.Expression = formattedDaxExpression;
            // store hash of formatted expression for later comparison
            string hashedDaxExpression = GetHashValueAsString(formattedDaxExpression);
            if (measure.Annotations.Contains("HashedExpression")) {
              measure.Annotations["HashedExpression"].Value = hashedDaxExpression;
            }
            else {
              measure.Annotations.Add(new Annotation { Name = "HashedExpression", Value = hashedDaxExpression });
            }
          }
          else {
            // nothing to do here - DAX already formatted
          }

        }

        // check which calculated columns require formatting
        foreach (var column in table.Columns) {
          if (column.Type == ColumnType.Calculated) {
            CalculatedColumn col = (CalculatedColumn)column;
            if (CalculatedColumnRequiresFormatting(col)) {
              formattedDaxExpressionsCount += 1;
              statusLabel.Text = "Formatting DAX for calculated column: " + table.Name + "[" + col.Name + "]";
              string expressionOwner = "'" + table.Name + "'[" + col.Name + "]";
              string originalDaxExpression = col.Expression;
              string formattedDaxExpression = FormatDaxExpression(originalDaxExpression, expressionOwner);
              // write formatted expression back to calculated column
              col.Expression = formattedDaxExpression;
              // store hash of formatted expression for later comparison
              string hashedDaxExpression = GetHashValueAsString(formattedDaxExpression);
              if (col.Annotations.Contains("HashedExpression")) {
                col.Annotations["HashedExpression"].Value = hashedDaxExpression;
              }
              else {
                col.Annotations.Add(new Annotation { Name = "HashedExpression", Value = hashedDaxExpression });
              }
            }
            else {
              // nothing to do here - DAX already formatted
            }
          }

        }

        // check which calculated tables require formatting
        if ((table.Partitions.Count > 0) &&
            (table.Partitions[0].SourceType == PartitionSourceType.Calculated)) {
          if (CalculatedTableRequiresFormatting(table)) {
            formattedDaxExpressionsCount += 1;
            var source = table.Partitions[0].Source as CalculatedPartitionSource;
            statusLabel.Text = "Formatting DAX for calculated table: " + table.Name;
            string expressionOwner = table.Name;
            string originalDaxExpression = source.Expression;
            string formattedDaxExpression = FormatDaxExpression(originalDaxExpression, expressionOwner);
            // write formatted expression back to calculated column
            source.Expression = formattedDaxExpression;
            // store hash of formatted expression for later comparison
            string hashedDaxExpression = GetHashValueAsString(formattedDaxExpression);
            if (table.Annotations.Contains("HashedExpression")) {
              table.Annotations["HashedExpression"].Value = hashedDaxExpression;
            }
            else {
              table.Annotations.Add(new Annotation { Name = "HashedExpression", Value = hashedDaxExpression });
            }

          }
          else {
            // nothing to do here - DAX already formatted
          }
        }

      }

      model.SaveChanges();

      model.RequestRefresh(RefreshType.Automatic);
      model.SaveChanges();

      if (formattedDaxExpressionsCount == 0) {
        statusLabel.Text = "There were no DAX expressions that required formatting.";
      }
      else {
        statusLabel.Text = string.Format("DAX formatting completed. {0} DAX expressions were reformtted.", formattedDaxExpressionsCount); ;
      }

    }

    #region "Utility Classes and Methods for Calling SQLBI DAX Formatter API to Format DAX"

    class RequestBody {
      public string Dax { get; set; }
      public object MaxLineLenght { get; set; }
      public bool SkipSpaceAfterFunctionName { get; set; }
      public string ListSeparator { get; set; }
      public string DecimalSeparator { get; set; }
      public string CallerApp { get; set; }
      public string CallerVersion { get; set; }
      public string ServerName { get; set; }
      public string ServerEdition { get; set; }
      public string ServerType { get; set; }
      public string ServerMode { get; set; }
      public string ServerLocation { get; set; }
      public string ServerVersion { get; set; }
      public string DatabaseName { get; set; }
      public string DatabaseCompatibilityLevel { get; set; }
    }

    class ResponseBody {
      public string formatted { get; set; }
      public List<object> errors { get; set; }
    }

    private const string restUrl = "https://daxformatter.azurewebsites.net/api/daxformatter/DaxTextFormat";

    private static string FormatDaxExpression(string daxInput, string expressionOwner = "") {

      string prefix = string.IsNullOrEmpty(expressionOwner) ? "" : expressionOwner + " =";

      string daxExpression = prefix + daxInput;

      RequestBody requestBody = new RequestBody {
        CallerApp = "Apollo",
        Dax = daxExpression,
        DecimalSeparator = ".",
        ListSeparator = ",",
        DatabaseCompatibilityLevel = "550",
        SkipSpaceAfterFunctionName = false
      };

      string postBody = Newtonsoft.Json.JsonConvert.SerializeObject(requestBody);

      HttpContent body = new StringContent(postBody);
      body.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
      HttpClient client = new HttpClient();
      client.DefaultRequestHeaders.Add("Accept", "application/json; charset=UTF-8");
      HttpResponseMessage response = client.PostAsync(restUrl, body).Result;

      if (response.IsSuccessStatusCode) {
        string jsonResponse = response.Content.ReadAsStringAsync().Result;
        ResponseBody responseBody = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseBody>(jsonResponse);
        string formattedExression = responseBody.formatted.Replace(prefix, "").Replace("\r\n", "\n");
        return formattedExression;
      }
      else {
        // Console.WriteLine();
        // Console.WriteLine("OUCH! - error occurred during POST REST call");
        // Console.WriteLine();
        return string.Empty;
      }
    }

    private static bool CalculatedColumnRequiresFormatting(CalculatedColumn column) {
      if (!column.Annotations.Contains("HashedExpression")) {
        return true;
      }
      string daxExpression = column.Expression;
      string hashedDaxExpression = GetHashValueAsString(daxExpression);
      string lastStoredHash = column.Annotations["HashedExpression"].Value;
      return hashedDaxExpression != lastStoredHash;
    }

    private static bool MeasureRequiresFormatting(Measure measure) {
      if (!measure.Annotations.Contains("HashedExpression")) {
        return true;
      }
      string daxExpression = measure.Expression;
      string hashedDaxExpression = GetHashValueAsString(daxExpression);
      string lastStoredHash = measure.Annotations["HashedExpression"].Value;
      return hashedDaxExpression != lastStoredHash;
    }

    private static bool CalculatedTableRequiresFormatting(Table table) {
      if (!table.Annotations.Contains("HashedExpression")) {
        return true;
      }
      var source = table.Partitions[0].Source as CalculatedPartitionSource;
      string daxExpression = source.Expression;
      string hashedDaxExpression = GetHashValueAsString(daxExpression);
      string lastStoredHash = table.Annotations["HashedExpression"].Value;
      return hashedDaxExpression != lastStoredHash;
    }

    private static string GetHashValueAsString(string input) {
      byte[] hashedData = SHA1.HashData(Encoding.UTF8.GetBytes(input));
      return Convert.ToBase64String(hashedData);
    }

    #endregion

    public static void CreateStandardRlsRoles() {

      DeleteAllRlsRoles();

      ModelRole roleAdmin = new ModelRole();
      roleAdmin.Name = "Admin";
      model.Roles.Add(roleAdmin);

      ModelRole roleAuditor2022 = new ModelRole();
      roleAuditor2022.Name = "Sales Auditor 2022";

      ModelRole roleAuditor2021 = new ModelRole();
      roleAuditor2021.Name = "Sales Auditor 2021";

      ModelRole roleAuditor2020 = new ModelRole();
      roleAuditor2020.Name = "Sales Auditor 2020";

      if (model.Tables.Contains("Sales") &&
          model.Tables["Sales"].Columns.Contains("Date")) {

        roleAuditor2022.TablePermissions.Add(new TablePermission {
          Name = "Sales2022",
          Table = model.Tables["Sales"],
          FilterExpression = "[Date] >= DATE(2022, 1, 1) && [Date] <= DATE(2022, 12, 31)",
        });

        roleAuditor2021.TablePermissions.Add(new TablePermission {
          Name = "Sales2021",
          Table = model.Tables["Sales"],
          FilterExpression = "[Date] >= DATE(2021, 1, 1) && [Date] <= DATE(2021, 12, 31)",
        });

        roleAuditor2020.TablePermissions.Add(new TablePermission {
          Name = "Sales2020",
          Table = model.Tables["Sales"],
          FilterExpression = "[Date] >= DATE(2020, 1, 1) && [Date] <= DATE(2020, 12, 31)",
        });

      }

      model.Roles.Add(roleAuditor2022);
      model.Roles.Add(roleAuditor2021);
      model.Roles.Add(roleAuditor2020);

      model.SaveChanges();
    }

    public static void DeleteAllRlsRoles() {
      model.Roles.Clear();
      model.SaveChanges();
    }



  }
}
