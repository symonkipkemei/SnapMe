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
            PushButtonData data = new PushButtonData("SnapMe", "SnapMe", assembly, "SnapMe.Program");
            

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

    // Attributes
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]

    public class Program : IExternalCommand
    {

        // store our directory information
        public static string SelectedDirectory { get; set; }

        // Main method
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;

            try
            {
                string imageName = uidoc.ActiveView.Name;
                // All images exported have a suffix followed by an index
                string searchName_underscore = imageName + "_";
                string search_suffix = ".png";

                // the user can select the directory on his/her own from the inteface
                //instantiate the directory selection window
                RevitImageSaver window = new RevitImageSaver(); 
                window.ShowDialog();

                string fileDirectory = SelectedDirectory;
  
                int maxNumber = DirectoryChecker(fileDirectory, searchName_underscore, search_suffix);
                int nextNum = maxNumber + 1;

                string filepath = fileDirectory + @"\" + searchName_underscore + nextNum + search_suffix;

                ImageExportOptions export = ExportSettings(filepath);
                doc.ExportImage(export);
                
            }

            catch (Exception ex)
            {
                message = ex.Message;
                return Result.Failed;
            }

            TaskDialog.Show("Success", $"View exported successfully!");
            return Result.Succeeded;
        }

        public ImageExportOptions ExportSettings(string filePath)
        {

            // instanitate the image exportoptions class
            ImageExportOptions export = new ImageExportOptions();
            export.ExportRange = ExportRange.VisibleRegionOfCurrentView;

            export.FilePath = filePath;
            export.ShadowViewsFileType = ImageFileType.PNG;
            export.HLRandWFViewsFileType = ImageFileType.PNG;
            export.ImageResolution = ImageResolution.DPI_300;
            export.ZoomType = ZoomFitType.Zoom;
            export.Zoom = 100;

            return export;

        }


        public int DirectoryChecker(string folderPath, string searchName, string searchSuffix)
        {
            //searchName with highest index set 0
            int maxNumber = 0;

            //list of files in folders
            string[] files = Directory.GetFiles(folderPath);

            foreach (string filepath in files)
            {
                //name with suffix i.e. symon.png
                string fileName_suffix = Path.GetFileName(filepath);

                //name without suffix
                string fileName = RemoveSuffix(fileName_suffix, searchSuffix);

                //split names
                string[] prefix_suffix = SplitName(fileName);

                string fileNamePrefix = prefix_suffix[0];
                string fileNameSuffix = prefix_suffix[1];

                if (searchName == fileNamePrefix)
                {
                  
                    int fileNameSuffix_int = int.Parse(fileNameSuffix);
                    
                    //check the number of the prefix
                    if (fileNameSuffix_int > maxNumber)
                    {
                        maxNumber = fileNameSuffix_int;
                    }
                }

            }

            return maxNumber;
        }

        static string RemoveSuffix(string Input, string suffix)
        {
            //checkk if input and suffix are not null as well as input ends with suffix
            if (!string.IsNullOrEmpty(Input) && !string.IsNullOrEmpty(suffix) && Input.EndsWith(suffix, StringComparison.OrdinalIgnoreCase))
            {
                //remove suffix by replacing it with empty string
                return Input.Replace(suffix, string.Empty);
            }

            //if above conditions are not met then return the input string
            return Input;
        }

        static string[] SplitName(string input)
        {
            string prefix = "";
            string suffix = "";

            //find the last underscore index in the string
            int underscoreIndex = input.LastIndexOf("_");

            // ensure underscoe was found
            if (underscoreIndex >= 0)
            {
                //separate string using the underscore index
                prefix = input.Substring(0, underscoreIndex + 1);
                suffix = input.Substring(underscoreIndex + 1);
            }
            //if name has no underscore then the strings will remain empty

            string[] prefix_suffix = new string[] { prefix, suffix };

            return prefix_suffix;
        }

    }
}
