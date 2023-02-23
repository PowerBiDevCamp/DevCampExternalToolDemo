using System;
using System.Collections.Generic;

namespace DevCampExternalToolDemo.Models {

  public enum ConnectionType {
    PowerBiDesktop,
    PowerBiService
  }

  public class DatasetConnection {
    public string ConnectString { get; set; }
    public string DatasetName { get; set; }
    public ConnectionType ConnectionType { get; set; }

    public string DisplayName {
      get {
        return DatasetName + " (" + ConnectString + ")";
      }
    }
  }

}
