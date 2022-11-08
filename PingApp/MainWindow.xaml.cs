using PingApp.Controllers;
using PingApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Xml.Linq;

namespace PingApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public SettingsController SettingsController { get => SettingsController.Instance; }
        public PingManager PingManager { get => PingManager.Instance; }

        public ObservableCollection<PingTableData> TableData { get => PingManager.TableCollection; }
        public MainWindow()
        {
            InitializeComponent();
            SettingsController.Instance.LoadSettings();
            PingDataInfo.ItemsSource = TableData;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SettingsController.Instance.SaveSettings();
            PingManager.StopAll();
        }

        private void RestartBtn_Click(object sender, RoutedEventArgs e)
        {
            var obj = ((FrameworkElement)sender).DataContext as PingTableData;
            if (obj != null)
                PingManager.RestartByName(obj.Name);
        }

        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            var obj = ((FrameworkElement)sender).DataContext as PingTableData;
            if(obj != null)
                PingManager.StartByName(obj.Name);
        }

        private void StoptBtn_Click(object sender, RoutedEventArgs e)
        {
            var obj = ((FrameworkElement)sender).DataContext as PingTableData;
            if (obj != null)
                PingManager.StopByName(obj.Name);
        }

        private void CommandTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                try
                {
                    CommandController.Instance.Run(CommandTextBox.Text); 

                    CommandTextBox.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                } 

            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            PingManager.StopAll();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            PingManager.StartAll();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            SettingsController.Instance.OpenLocation();
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            new HelpWindow().Show();
        }
    }
}
