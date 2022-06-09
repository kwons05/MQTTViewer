using Prism.Mvvm;
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


	public class Node : BindableBase
	{
		public int Key { get; set; }
		public string Name { get; set; }

		private string _value;
		public string Value
		{
			get { return _value; }
			set { SetProperty(ref _value, value); }
		}

        #region UI
        private bool _isExpanded;
		public bool IsExpanded
		{
			get { return _isExpanded; }
			set { SetProperty(ref _isExpanded, value); }
		}
        #endregion

        public ObservableCollection<Node> SubNodes { get; set; }
	}
}
