using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Spotival.classes;

namespace Spotival.visuals
{
    /// <summary>
    /// Logique d'interaction pour ExplorerWindow.xaml
    /// </summary>
    public partial class ExplorerWindow : Page
    {
        public ExplorerWindow()
        {
            InitializeComponent();
            InitializeFileSystemObjects();
        }
        private void InitializeFileSystemObjects()
        {
            var drives = DriveInfo.GetDrives();
            DriveInfo.GetDrives().ToList().ForEach(drive =>
            {
                var fileSystemObject = new FileSystemObjectInfo(drive);
                fileSystemObject.BeforeExplore += FileSystemObject_BeforeExplore;
                fileSystemObject.AfterExplore += FileSystemObject_AfterExplore;
                treeView.Items.Add(fileSystemObject);
            });
        }

        private void FileSystemObject_AfterExplore(object sender, System.EventArgs e)
        {
            Cursor = Cursors.Arrow;
        }

        private void FileSystemObject_BeforeExplore(object sender, System.EventArgs e)
        {
            Cursor = Cursors.Wait;
        }

        private void NavigateFolder(object sender, System.EventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog openFileDlg = new System.Windows.Forms.FolderBrowserDialog();
            var result = openFileDlg.ShowDialog();
            if (result.ToString() != string.Empty)
            {
                TextBlockDestination.Text = openFileDlg.SelectedPath;
            }
        }

        public static void LoadMusic(object sender)
        {
            
            String[] fichiers = System.IO.Directory.GetFiles(sender.ToString());

            foreach (string fichier in fichiers)
            {
                //GridViewMusic.Items.Add(new Song() { Titre = fichier });
                ListViewMusic.Items.Add(new Song() { Titre = fichier });
            }
        }
    }
}
