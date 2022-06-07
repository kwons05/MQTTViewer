using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MqttViewer.Popup
{
    public class IpAddressPopupViewModel
    {
        // The DialogCoordinator
        private IDialogCoordinator dialogCoordinator;

        public IpAddressPopupViewModel(IDialogCoordinator instance)
        {
            this.dialogCoordinator = instance;
        }
    }
}
