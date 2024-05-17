using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.Attributes;
using System.Reflection;
using System.Windows.Media.Imaging;

namespace SnapMe
{

    public class App : IExternalApplication
    {

        
        private void AddRibbonButton(UIControlledApplication application)
        {
            // Ribbon Panel
            RibbonPanel ribbonPanel = application.CreateRibbonPanel("SnapMe");
            string assembly = Assembly.GetExecutingAssembly().Location;

            //Button data
            PushButtonData data = new PushButtonData("SnapMe", "SnapMe", assembly, "SnapMe.CommandSnap");


            PushButton button = ribbonPanel.AddItem(data) as PushButton;
            button.ToolTip = "Automatically snap images like a pro";
            Uri uri = new Uri("pack://application:,,,/SnapMe;component/snapme.png");
            BitmapImage image = new BitmapImage(uri);
            button.LargeImage = image;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication application)
        {
            AddRibbonButton(application);

            return Result.Succeeded;
        }

    }

}
