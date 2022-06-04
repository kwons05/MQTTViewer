using MahApps.Metro.Controls;
using MqttViewer.Utility;
using MqttViewer.ViewModel;
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
using System.Windows.Threading;

namespace MqttViewer
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : MetroWindow
    {


        private MainViewModel ViewModel;

        public MainWindow()
        {
            InitializeComponent();

            Logger.Instance.ConsoleBox = new Action<string>(WriteLogBox);

            ViewModel = new MainViewModel();

            this.DataContext = ViewModel;
        }
        public void WriteLogBox(string message)
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => {
                this.logText.AppendText(message + "\r");
    
                if(ViewModel.IsAutoScroll)
                    this.logText.ScrollToEnd();
            }));
        }

    }
}
