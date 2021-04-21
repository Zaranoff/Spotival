using Spotival.classes;
using Spotival.visuals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Spotival
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SelectFunction(tt_home, null);
        }

        public void SelectFunction(object sender, RoutedEventArgs e)
        {
            ToolTip btn = sender as ToolTip;
            //tt_deezer.Background = new SolidColorBrush(Color.FromRgb(48,48,48));
            //tt_explorer.Background = new SolidColorBrush(Color.FromRgb(48, 48, 48));
            //tt_home.Background = new SolidColorBrush(Color.FromRgb(48, 48, 48));
            //tt_settings.Background = new SolidColorBrush(Color.FromRgb(48, 48, 48));
            //tt_settings.Background = new SolidColorBrush(Color.FromRgb(48, 48, 48));

            switch (btn.Content)
            {
                case "Deezer":
                    btn.Background = new SolidColorBrush(Color.FromRgb(158, 158, 158));
                    ChangeWindow(SPDeezer.Name);
                    break;
                case "Explorer":
                    btn.Background = new SolidColorBrush(Color.FromRgb(158, 158, 158));
                    ChangeWindow(SPExplorer.Name);
                    break;
                case "Home":
                    //btn.Background = new SolidColorBrush(Color.FromRgb(158, 158, 158));
                    ChangeWindow(SPHome.Name);
                    break;
                case "Settings":
                    btn.Background = new SolidColorBrush(Color.FromRgb(158, 158, 158));
                    ChangeWindow(SPSettings.Name);
                    break;
                case "Spotify":
                    btn.Background = new SolidColorBrush(Color.FromRgb(158, 158, 158));
                    ChangeWindow(SPSpotify.Name);
                    break;
            }
        }

        public void CloseWindow(object sender, EventArgs e)
        {
            Close();
        }

        public void MinimizeWindow(object sender, EventArgs e)
        {
           WindowState = WindowState.Minimized;
        }

        public void MaximizeWindow(object sender, EventArgs e)
        {
            if(WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
            }
            else
            {
                WindowState = WindowState.Normal;
            }
        }

        public void ChangeWindow(object sender)
        {
            switch (sender)
            {
                case "SPHome":
                    mainFrame.Navigate(new System.Uri("visuals/HomeWindow.xaml", UriKind.RelativeOrAbsolute));
                    break;
                case "SPDeezer":
                    mainFrame.Navigate(new System.Uri("visuals/WorkingWindow.xaml", UriKind.RelativeOrAbsolute));
                    break;
                case "SPSpotify":
                    mainFrame.Navigate(new System.Uri("visuals/WorkingWindow.xaml", UriKind.RelativeOrAbsolute));
                    break;
                case "SPExplorer":
                    mainFrame.Navigate(new System.Uri("visuals/ExplorerWindow.xaml", UriKind.RelativeOrAbsolute));
                    break;
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void btnPlayPause_Click(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).Content == FindResource("Play"))
            {
                ((Button)sender).Content = FindResource("Stop");
                mediaPlayer.Pause();
            }
            else
            {
                ((Button)sender).Content = FindResource("Play");
                mediaPlayer.Play();
            }
        }

        private void progressSong_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaPlayer.Position = TimeSpan.FromSeconds(progressSong.Value);
        }

        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            if(tg_btn.IsChecked == true)
            {
                tt_home.Visibility = Visibility.Collapsed;
                tt_deezer.Visibility = Visibility.Collapsed;
                tt_spotify.Visibility = Visibility.Collapsed;
                tt_explorer.Visibility = Visibility.Collapsed;
                tt_settings.Visibility = Visibility.Collapsed;
            }
            else
            {
                tt_home.Visibility = Visibility.Visible;
                tt_deezer.Visibility = Visibility.Visible;
                tt_spotify.Visibility = Visibility.Visible;
                tt_explorer.Visibility = Visibility.Visible;
                tt_settings.Visibility = Visibility.Visible;
            }
        }

        private void tg_btn_Unchecked(object sender, RoutedEventArgs e)
        {
            mainFrame.Opacity = 1;
            GridLecture.Opacity = 1;
        }

        private void tg_btn_Checked(object sender, RoutedEventArgs e)
        {
            mainFrame.Opacity = 0.3;
            GridLecture.Opacity = 0.3;
        }

        private void ListViewItem_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListViewItem test = (ListViewItem)sender;

            ChangeWindow(((StackPanel)test.Content).Name);
        }
    }
}
