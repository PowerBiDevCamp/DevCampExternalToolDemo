using DevCampExternalToolDemo.Models;
using DevCampExternalToolDemo.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DevCampExternalToolDemo.Forms {
  public partial class FormConnect : Form {

    public string ConnectString;

    public FormConnect() {
      InitializeComponent();
      RefreshActiveSessionsListbox();
    }

    public void RefreshActiveSessionsListbox() {

      var sessions = PowerBiDesktopUtilities.GetActiveDatasetConnections();
      lstSessions.DataSource = sessions;
      lstSessions.DisplayMember = "DisplayName";

      if (TomApi.IsConnected) {
        for (int index = 0; index < lstSessions.Items.Count; index++) {
          DatasetConnection session = lstSessions.Items[index] as DatasetConnection;
          if (session.ConnectString.Equals(TomApi.ConnectString)) {
            lstSessions.SelectedIndex = index;
            break;
          }
        }
      }

    }

    private void btnConnect_Click(object sender, EventArgs e) {
      this.DialogResult = DialogResult.OK;
      this.Close();
    }

    private void btnCancel_Click(object sender, EventArgs e) {

      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    private void lstSessions_SelectedIndexChanged(object sender, EventArgs e) {
      string selectedServerConnectString = (lstSessions.SelectedValue as DatasetConnection).ConnectString;
      ConnectString = selectedServerConnectString;
    }

    private void lstSessions_DoubleClick(object sender, EventArgs e) {
      if(lstSessions.SelectedItem!= null) {
        this.DialogResult = DialogResult.OK;
        this.Close();
      }

    }
    
    private void lstSessions_KeyDown(object sender, KeyEventArgs e) {

      if (e.KeyCode == Keys.Enter && lstSessions.SelectedItem != null) {
        this.DialogResult = DialogResult.OK;
        this.Close();
      }
    }
  }
}
