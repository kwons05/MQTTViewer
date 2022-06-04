using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MqttViewer.Models
{
    public class TopicModel
    {
    }


	public class Node
	{
		public int Key { get; set; }
		public string Name { get; set; }
		public string Value { get; set; }

		public ObservableCollection<Node> SubNodes { get; set; }
	}
}
