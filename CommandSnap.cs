using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnapMe
{
    // Attributes
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]

    public class CommandSnap: IExternalCommand
    {
        

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
                string fileDirectory = SettingsData.FolderDirectory;

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

        public void HideLevelLines3D(View activeView3D, Document doc)
        {
            // get at least of all levels in active view
            ElementId viewId = activeView3D.Id;

            FilteredElementCollector collector = new FilteredElementCollector(doc, viewId);
            ICollection<Element> LevelElements = collector.OfClass(typeof(Level)).ToElements();
            
            List<ElementId> levelIds = new List<ElementId>();

            foreach(Element element in LevelElements)
            {
                levelIds.Add(element.Id);
            }

            // check if level Id is empty if not pick the first Id in the list

            if(levelIds.Count > 0)
            {
                //first item in list
                ElementId levelId1 = levelIds.First();

                // Get visibility settings for the active view
                activeView3D.SetCategoryHidden(levelId1, false);

            }

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
