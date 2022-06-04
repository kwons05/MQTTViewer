using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MqttViewer.Models
{


//{
//    "configuration":{
//        "label":"DW123B",
//        "nodeType":"TAG",
//        "ble":false,
//        "leds":true,
//        "uwbFirmwareUpdate":false,
//        "tag":{
//            "stationaryDetection":true,
//            "responsive":true,
//            "locationEngine":true,
//            "nomUpdateRate":100,
//            "statUpdateRate":1000
//        }
//    }
//}

    public class tag : BindableBase
    {
        private bool _stationaryDetection;
        private bool _responsive;
        private bool _locationEngine;
        private int  _nomUpdateRate;
        private int  _statUpdateRate;
    }

    public class configuration : BindableBase
    {
        private string  _label;
        private string  _nodeType;
        private bool    _ble;
        private bool    _leds;
        private bool    _uwbFirmwareUpdate;
        private tag     _tag;

        public string label
        {
            get { return _label; }
            set { SetProperty(ref _label, value); }
        }
        public string nodeType
        {
            get { return _nodeType; }
            set { SetProperty(ref _nodeType, value); }
        }

        public bool ble
        {
            get { return _ble; }
            set { SetProperty(ref _ble, value); }
        }
        public bool leds
        {
            get { return _leds; }
            set { SetProperty(ref _leds, value); }
        }
        public bool uwbFirmwareUpdate
        {
            get { return _uwbFirmwareUpdate; }
            set { SetProperty(ref _uwbFirmwareUpdate, value); }
        }
        public tag tag
        {
            get { return _tag; }
            set { SetProperty(ref _tag, value); }
        }
    }
    /// <summary>
    /// MQTT Payload Value
    /// </summary>
    public class config : BindableBase
    {
        private configuration _configuration;
        public configuration configuration
        {
            get { return _configuration; }
            set { SetProperty(ref _configuration, value); }
        }
    }
}
