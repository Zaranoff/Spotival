using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.IO;
using Spotival.classes;
using Spotival.visuals;

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
                txtDestination.Text = openFileDlg.SelectedPath;
            }
        }

        [Obsolete]
        public void LoadMusic(string sender)
        {

            String[] fichiers = System.IO.Directory.GetFiles(sender.ToString().Replace("//", "/"));
            ListView list = new ListView();

            foreach (string fichier in fichiers)
            {
                if (fichier.EndsWith("mp3"))
                {
                    var fi = TagLib.File.Create(fichier);
                    list.Items.Add(new Song() { Titre = fi.Tag.Title, Artiste = String.Join(",", fi.Tag.Artists), Durée = fi.Properties.Duration, BPM = fi.Tag.BeatsPerMinute, LocalisationFichier = fichier, Cover = fi.Tag.Pictures });
                }
            }
            //this.ListViewMusic.Items.Clear();
            this.ListViewMusic.ItemsSource = list.Items;
        }

        [Obsolete]
        private void TreeViewNavigate_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            FileSystemObjectInfo item = (FileSystemObjectInfo)TreeViewNavigate.SelectedItem;
            if ((item != null))
            {
                string path = item.FileSystemInfo.FullName;
                LoadMusic(path);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (txtDestination.Text != "")
            {
                Song musicInfo = (Song)(sender as Button).DataContext;
                File.Copy(musicInfo.LocalisationFichier, txtDestination.Text + "\\" + musicInfo.Titre + ".mp3", true);
            }
            else 
            {
                MessageBox.Show("Pas de dossier de destination");
            }
        }

        private void bntAddList_Click(object sender, RoutedEventArgs e)
        {
            if(txtDestination.Text != "" && txtSearch.Text != "")
            {
                string path = txtDestination.Text + "/_a_prevoir.txt";
                if (!File.Exists(path))
                {
                    File.Create(path).Close();
                }

                using (StreamWriter writer = new StreamWriter(path))
                {
                    writer.WriteLine(txtSearch.Text);
                }
            }
            else
            {
                MessageBox.Show("Merci de choisir un dossier de destination et de saisir un titre");
            }
        }

        private void btnSearchSong_Click(object sender, RoutedEventArgs e)
        {

            FileSystemObjectInfo path = (FileSystemObjectInfo)TreeViewNavigate.SelectedItem;
            ListView allFiles = new ListView();
            allFiles = listAllFiles(allFiles, path.FileSystemInfo.FullName, "mp3", true);
            ListViewMusic.ItemsSource = allFiles.Items;
        }

        public ListView listAllFiles(ListView allFiles, string path, string ext, bool scanDirOk)
        {
            string[] listFilesCurrDir = Directory.GetFiles(path);

            foreach (string rowFile in listFilesCurrDir)
            {
                if (rowFile.ToUpper().Contains(txtSearch.Text.ToUpper()) && rowFile.EndsWith("mp3"))
                {
                    try
                    {
                        var fi = TagLib.File.Create(rowFile);
                        allFiles.Items.Add(new Song() { Titre = fi.Tag.Title, Artiste = String.Join(",", fi.Tag.Artists), Durée = fi.Properties.Duration, BPM = fi.Tag.BeatsPerMinute, LocalisationFichier = rowFile, Cover = fi.Tag.Pictures });
                    }
                    catch
                    {

                    }
                }
            }

            listFilesCurrDir = null;

            if (scanDirOk)
            {
                string[] listDirCurrDir = Directory.GetDirectories(path);

                if (listDirCurrDir.Length != 0)
                {
                    foreach (string rowDir in listDirCurrDir)
                    {
                        listAllFiles(allFiles, rowDir, ext, scanDirOk);
                    }
                }
                listDirCurrDir = null;

            }
            return allFiles;
        }

        private void btnLaunchMusic_Click(object sender, RoutedEventArgs e)
        {
            Song musicInfo = (Song)(sender as Button).DataContext;
            musicInfo.LoadInfoMusic();
        }
    }
}
