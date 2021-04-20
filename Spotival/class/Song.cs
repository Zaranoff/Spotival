using Spotival.visuals;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Spotival.classes
{
    public class Song
    {
        public string Titre { get; set; }
        public string Artiste { get; set; }
        public TimeSpan Durée { get; set; }
        public uint BPM { get; set; }
        public string LocalisationFichier { get; set; }
        public TagLib.IPicture[] Cover {get; set;}

        public void LaunchMusic()
        {
            var main = App.Current.MainWindow;
            ((TextBlock)main.FindName("txtTitle")).Text = Titre;
            ((TextBlock)main.FindName("txtArtist")).Text = Artiste;

            if(Cover.Length > 0)
            {
                TagLib.IPicture pic = Cover[0];
                MemoryStream ms = new MemoryStream(pic.Data.Data);
                ms.Seek(0, SeekOrigin.Begin);

                // ImageSource for System.Windows.Controls.Image
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = ms;
                bitmap.EndInit();

                // Create a System.Windows.Controls.Image control

                ((Image)main.FindName("CoverSong")).Source = bitmap;
            }
            else
            {
                string fullFilePath = "../resources/album.jpg";
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.UriSource = new Uri(fullFilePath, UriKind.Relative);
                bi.EndInit();

                ((Image)main.FindName("CoverSong")).Source = bi;
            }
            ((Label)main.FindName("TotalTime")).Content = Durée;
            ((Slider)main.FindName("progressSong")).Value = 0;
        }
    }
}
