using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace KT_59080337__Gamal_Saied_Columns_ReportAddin.Revit.Models
{
    public class CloumnsData
    {
        public string Family { get; set; }
        public string Type { get; set; }
        public int ID { get; set; }
        public double Easting { get; set; }
        public double Northing { get; set; }
        public string BaseLevel { get; set; }
        public double BaseOffset { get; set; }
        public string TopLevel { get; set; }
        public double TopOffset { get; set; }
        public double Height { get; set; }
        public double Volume { get; set; }
    }
}











   

