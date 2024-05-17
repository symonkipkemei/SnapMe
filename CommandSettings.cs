using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnapMe
{
    // Attributes
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]

    public class CommandSettings : IExternalCommand
    {
       

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            try
            {
                // the user can select the directory on his/her own from the inteface
                //instantiate the directory selection window
                RevitImageSaver window = new RevitImageSaver();
                window.ShowDialog();

            }

            catch(Exception ex)
            {
                message = ex.Message;
                return Result.Failed;
            }
            return Result.Succeeded;


        }





    }
}
