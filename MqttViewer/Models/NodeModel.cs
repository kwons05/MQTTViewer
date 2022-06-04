using MQTTnet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MqttViewer.Models
{
    public class NodeModel
    {
        public MqttApplicationMessage   Message { get; set; }

        public DateTime TimeStamp { get; set; }

        /// <summary>
        /// Payload location
        /// </summary>
        public location Location    { get; set; }
        /// <summary>
        /// Payload config
        /// </summary>
        public config   Config      { get; set; }
        /// <summary>
        /// Payload status
        /// </summary>
        public status   Status      { get; set; }
    }
}
