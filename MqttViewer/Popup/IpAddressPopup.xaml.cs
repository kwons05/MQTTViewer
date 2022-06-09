using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Prism.Mvvm;
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
using System.Windows.Shapes;

namespace MqttViewer.Popup
{
    /// <summary>
    /// IpAddressPopup.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class IpAddressPopup : MetroWindow
    {
        public class Bind : BindableBase
        {
            private string _ipAddress = string.Empty;
            public string IpAddress
            {
                get { return _ipAddress; }
                set { SetProperty(ref _ipAddress, value); }
            }

            private int _port = 1883;
            public int Port
            {
                get { return _port; }
                set { SetProperty(ref _port, value); }
            }
        }

        public Bind BindData { get; set; }

        public IpAddressPopup()
        {
            InitializeComponent();

            this.Owner = Application.Current.MainWindow;

            this.BindData = new Bind(); 

            this.DataContext = this;
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
