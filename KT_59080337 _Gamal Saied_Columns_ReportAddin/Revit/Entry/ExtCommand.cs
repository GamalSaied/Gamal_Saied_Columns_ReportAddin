using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.ApplicationServices;
using KT_59080337__Gamal_Saied_Columns_ReportAddin.UI;
using System.Reflection;
using System.Windows.Media.Imaging;
using KT_59080337__Gamal_Saied_Columns_ReportAddin.Revit.Models;
using System.Windows.Forms;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;



namespace KT_59080337__Gamal_Saied_Columns_ReportAddin.Revit.Entry
{
    [Transaction(TransactionMode.Manual)]

    public class ExtCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //uidoc
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            //doc
            Document doc = uidoc.Document;


            // Get the Revit file name
            string revitFileName = doc.PathName;
            if (string.IsNullOrWhiteSpace(revitFileName))
            {
                revitFileName = "Untitled";
            }
            else
            {
                revitFileName = System.IO.Path.GetFileNameWithoutExtension(revitFileName);
            }


            FilteredElementCollector collector = new FilteredElementCollector(doc);

            var columns=collector.OfCategory(BuiltInCategory.OST_StructuralColumns).WhereElementIsNotElementType().ToList();

            List<CloumnsData> columnDataList = new List<CloumnsData>();
            foreach (var column in columns)
            {
                FamilyInstance fi = column as FamilyInstance;
                if (fi != null)
                {
                    LocationPoint locPoint = fi.Location as LocationPoint;
                    Level baseLevel = doc.GetElement(fi.LevelId) as Level;
                    Level topLevel = doc.GetElement(fi.get_Parameter(BuiltInParameter.FAMILY_TOP_LEVEL_PARAM).AsElementId())as Level;

                    double baseOffset = fi.get_Parameter(BuiltInParameter.FAMILY_BASE_LEVEL_OFFSET_PARAM).AsDouble();
                    double topOffset = fi.get_Parameter(BuiltInParameter.FAMILY_TOP_LEVEL_OFFSET_PARAM).AsDouble();
                    double height = fi.get_Parameter(BuiltInParameter.INSTANCE_LENGTH_PARAM).AsDouble();
                    double volume = fi.get_Parameter(BuiltInParameter.HOST_VOLUME_COMPUTED).AsDouble();

                    columnDataList.Add(new CloumnsData
                    {
                        Family = fi.Symbol.Family.Name,
                        Type = fi.Symbol.Name,
                        ID = fi.Id.IntegerValue,
                        Easting = locPoint.Point.X,
                        Northing = locPoint.Point.Y,
                        BaseLevel = baseLevel.Name,
                        BaseOffset = baseOffset,
                        TopLevel = topLevel.Name,
                        TopOffset = topOffset,
                        Height = height,
                        Volume = volume
                    });
                }
            }




            MainForm mainForm = new MainForm(columnDataList,revitFileName);
            mainForm.ShowDialog();

            return Result.Succeeded;
        }


        public static void AddRibbonPanel(UIControlledApplication application)
        {
            string tabName = "KAITECH-BD-R06";
            string panelName = "Structure";
            string buttonName = "Columns Report";
            string dllPath = Assembly.GetExecutingAssembly().Location;
            string iconPath = Path.Combine(Path.GetDirectoryName(dllPath), "Resources", "icons8-civil-engineer-32.png"); // Path to your icon file

            // Create a custom ribbon tab
            try
            {
                application.CreateRibbonTab(tabName);
            }
            catch (Exception)
            {
                // Tab already exists, ignore the exception
            }

            // Create a ribbon panel
            RibbonPanel ribbonPanel = application.CreateRibbonPanel(tabName, panelName);

            // Create a push button on the panel
            PushButtonData buttonData = new PushButtonData(buttonName, buttonName, dllPath, typeof(ExtCommand).FullName);

            PushButton pushButton = ribbonPanel.AddItem(buttonData) as PushButton;
            pushButton.ToolTip = "Exports Columns Report";

            // Set the icon for the button
            Uri iconUri = new Uri(iconPath, UriKind.Absolute);
            BitmapImage bitmapImage = new BitmapImage(iconUri);
            pushButton.LargeImage = bitmapImage;
            pushButton.Image = bitmapImage;
        }
    }
}


