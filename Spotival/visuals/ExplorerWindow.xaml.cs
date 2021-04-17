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
using System.ComponentModel;

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
        public void InitializeFileSystemObjects()
        {
            var drives = DriveInfo.GetDrives();
            DriveInfo.GetDrives().ToList().ForEach(drive =>
            {
                var fileSystemObject = new FileSystemObjectInfo(drive);
                fileSystemObject.BeforeExplore += FileSystemObject_BeforeExplore;
                fileSystemObject.AfterExplore += FileSystemObject_AfterExplore;
                TreeViewNavigate.Items.Add(fileSystemObject);
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

        [Obsolete]
        public void LoadMusic(string sender)
        {

            String[] fichiers = System.IO.Directory.GetFiles(sender.ToString().Replace("//", "/"));
            //String[] fichiers = System.IO.Directory.GetFiles();
            ListView list = new ListView();

            foreach (string fichier in fichiers)
            {
                if (fichier.EndsWith("mp3"))
                {
                    var fi = TagLib.File.Create(fichier);
                    list.Items.Add(new Song() { Titre = fi.Tag.Title, Artiste = String.Join(",", fi.Tag.Artists), Durée = fi.Properties.Duration });
                }
            }
            //this.ListViewMusic.Items.Clear();
            this.ListViewMusic.ItemsSource = list.Items;
        }

        [Obsolete]
        private void TreeViewNavigate_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            FileSystemObjectInfo item = (FileSystemObjectInfo)TreeViewNavigate.SelectedItem;
            if(!(item.FileSystemInfo.FullName is null)){
                string path = item.FileSystemInfo.FullName;
                LoadMusic(path);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(((Button)sender).CommandParameter.ToString());
        }
    }
}
