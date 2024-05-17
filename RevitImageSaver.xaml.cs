using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Autodesk.Revit.UI;

namespace SnapMe
{
    /// <summary>
    /// Interaction logic for RevitImageSaver.xaml
    /// </summary>
    public partial class RevitImageSaver : Window
    {
        public RevitImageSaver()
        {
            InitializeComponent();
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            using( var dialog = new FolderBrowserDialog())
            {
                DialogResult result = dialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.SelectedPath))
                    {
                    DirectoryTextBox.Text = dialog.SelectedPath;
                    }
            }

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string selectedDirectory = DirectoryTextBox.Text;
            if (!string.IsNullOrWhiteSpace(selectedDirectory)) 
            {
                
                SaveDirectoryToRevit(selectedDirectory);
                Close();
              
            }
            else
            {
                TaskDialog.Show("RevitImageSaver", "Please select a directory first");
            }


        }

        private void SaveDirectoryToRevit(string selectedDirectory)
        {
            CommandSnap.SelectedDirectory = selectedDirectory;
        }
    }
}
