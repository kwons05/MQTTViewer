using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MqttViewer.Models
{
    /// <summary>
    /// MQTT Payload Value
    /// </summary>
    public class status : BindableBase
    {
        private bool _present;
        public bool present
        {
            get { return _present; }
            set { SetProperty(ref _present, value); }
        }
    }
}
