using KT_59080337__Gamal_Saied_Columns_ReportAddin.Excel.Utils;
using KT_59080337__Gamal_Saied_Columns_ReportAddin.Revit.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KT_59080337__Gamal_Saied_Columns_ReportAddin.UI
{
    public partial class MainForm : Form
    {
        private List<CloumnsData> ColumnDataList { get; set; }
        private string RevitFileName { get; set; }
        public MainForm(List<CloumnsData> columnDataList, string revitFileName)
        {
            InitializeComponent();
            ColumnDataList = columnDataList;
            RevitFileName = revitFileName;

            // Debugging: Show the number of columns received
            MessageBox.Show($"Number of columns received: {ColumnDataList.Count}");
        }
        private void btnbrowse_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss tt");
                string defaultFileName = $"{RevitFileName}-Columns Report [{timestamp}].xlsx";
                saveFileDialog.FileName = defaultFileName;
                saveFileDialog.Filter = "Excel Files|*.xlsx";
                saveFileDialog.Title = "Save Columns Report";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtPath.Text = saveFileDialog.FileName;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string filePath = txtPath.Text;
            if (string.IsNullOrWhiteSpace(filePath))
            {
                MessageBox.Show("Please select a path to save the report.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (ColumnDataList == null || ColumnDataList.Count == 0)
            {
                MessageBox.Show("No column data available to export.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ExportReport.ExportToExcel(ColumnDataList, filePath);
            MessageBox.Show("Report successfully saved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Open the file automatically
            try
            {
                Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to open the file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
           

        private void Piclinked_Click(object sender, EventArgs e)
        {
            string linkedInUrl = "https://www.linkedin.com/in/gamalsaiedaec44/";
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = linkedInUrl,
                UseShellExecute = true
            };
            Process.Start(startInfo);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }



}
