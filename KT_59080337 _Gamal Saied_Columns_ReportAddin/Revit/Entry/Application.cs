using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using System.IO;
using System.Reflection;
using System.Windows.Media.Imaging;

namespace KT_59080337__Gamal_Saied_Columns_ReportAddin.Revit.Entry
{
    [Transaction(TransactionMode.Manual)]

    public class Application:IExternalApplication
    {
        public Result OnStartup(UIControlledApplication application)
        {
            ExtCommand.AddRibbonPanel(application);
            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

    }

}

