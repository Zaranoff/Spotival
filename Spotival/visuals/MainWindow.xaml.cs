using Spotival.classes;
using Spotival.visuals;
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
            SelectFunction(btnHome, null);
        }

        public void SelectFunction(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            btnDeezer.Background = new SolidColorBrush(Color.FromRgb(48,48,48));
            btnExplorer.Background = new SolidColorBrush(Color.FromRgb(48, 48, 48));
            btnHome.Background = new SolidColorBrush(Color.FromRgb(48, 48, 48));
            btnSettings.Background = new SolidColorBrush(Color.FromRgb(48, 48, 48));
            btnSpotify.Background = new SolidColorBrush(Color.FromRgb(48, 48, 48));

            switch (btn.Name)
            {
                case "btnDeezer":
                    btn.Background = new SolidColorBrush(Color.FromRgb(158, 158, 158));
                    ChangeWindow(btn);
                    break;
                case "btnExplorer":
                    btn.Background = new SolidColorBrush(Color.FromRgb(158, 158, 158));
                    ChangeWindow(btn);
                    break;
                case "btnHome":
                    btn.Background = new SolidColorBrush(Color.FromRgb(158, 158, 158));
                    ChangeWindow(btn);
                    break;
                case "btnSettings":
                    btn.Background = new SolidColorBrush(Color.FromRgb(158, 158, 158));
                    ChangeWindow(btn);
                    break;
                case "btnSpotify":
                    btn.Background = new SolidColorBrush(Color.FromRgb(158, 158, 158));
                    ChangeWindow(btn);
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
            Button btn = sender as Button;
            switch (btn.Name)
            {
                case "btnHome":
                    mainFrame.Navigate(new System.Uri("visuals/HomeWindow.xaml", UriKind.RelativeOrAbsolute));
                    break;
                case "btnDeezer":
                    mainFrame.Navigate(new System.Uri("visuals/WorkingWindow.xaml", UriKind.RelativeOrAbsolute));
                    break;
                case "btnSpotify":
                    mainFrame.Navigate(new System.Uri("visuals/WorkingWindow.xaml", UriKind.RelativeOrAbsolute));
                    break;
                case "btnExplorer":
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

        }
    }
}
