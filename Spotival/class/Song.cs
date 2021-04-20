using Spotival.visuals;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

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

        DispatcherTimer timer = new DispatcherTimer();

        public void LoadInfoMusic()
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
            PlaySong();
        }

        public void PlaySong()
        {
            StopSong();
            var main = App.Current.MainWindow;
            MediaElement mediaPlayer = (MediaElement)(main.FindName("mediaPlayer"));

            mediaPlayer.Source = (new Uri(LocalisationFichier, UriKind.Relative));
            mediaPlayer.Play();
            mediaPlayer.Position = TimeSpan.FromSeconds(((Slider)(main.FindName("progressSong"))).Value);

            ((Slider)(main.FindName("progressSong"))).Maximum = Durée.TotalSeconds;

            timer.Interval = TimeSpan.FromMilliseconds(1);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            var main = App.Current.MainWindow;
            MediaElement mediaPlayer = (MediaElement)(main.FindName("mediaPlayer"));
            ((Slider)(main.FindName("progressSong"))).Value = mediaPlayer.Position.TotalSeconds;
            ((Label)(main.FindName("lblTime"))).Content = ((mediaPlayer.Position.Hours).ToString("00") + ":" + (mediaPlayer.Position.Minutes).ToString("00") + ":" + mediaPlayer.Position.Seconds.ToString("00"));
        }

        public void StopSong()
        {
            var main = App.Current.MainWindow;
            MediaElement mediaPlayer = (MediaElement)(main.FindName("mediaPlayer"));
            mediaPlayer.Close();
            timer.Stop();

        }
    }
}
