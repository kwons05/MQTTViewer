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
    public class position : BindableBase
    {
        private double _x;
        private double _y;
        private double _z;
        private double _quality;

        public double x
        {
            get { return _x; }
            set { SetProperty(ref _x, value); }
        }
        public double y
        {
            get { return _y; }
            set { SetProperty(ref _y, value); }
        }
        public double z
        {
            get { return _z; }
            set { SetProperty(ref _z, value); }
        }
        public double quality
        {
            get { return _quality; }
            set { SetProperty(ref _quality, value); }
        }
    }
    /// <summary>
    /// MQTT Payload Value
    /// </summary>
    public class location : BindableBase
    {
        private position _position;
        public position position
        {
            get { return _position; }
            set { SetProperty(ref _position, value); }
        }

        private int _superFrameNumber;
        public int superFrameNumber
        {
            get { return _superFrameNumber; }
            set { SetProperty(ref _superFrameNumber, value); }
        }
    }
}
