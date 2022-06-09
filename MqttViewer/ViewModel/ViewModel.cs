using MQTTnet;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using MqttViewer.Models;
using MqttViewer.Module;
using MqttViewer.Utility;
using MQTTnet.Client;
using Newtonsoft.Json.Linq;
using MqttViewer.Popup;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;

namespace MqttViewer.ViewModel
{
    public class MainViewModel : BindableBase
    {
        private Logger logger = Logger.Instance;

        private bool isAutoScroll = true;
        public bool IsAutoScroll
        {
            get { return isAutoScroll; }
            set { SetProperty(ref isAutoScroll, value); }
        }

        private ObservableCollection<TagLocation> _tagLocations;
        public ObservableCollection<TagLocation> TagLocations
        {
            get { return _tagLocations; }
            set { SetProperty(ref _tagLocations, value); }
        }

        public ObservableCollection<Node> Nodes { get; set; }


        public ObservableCollection<Node> Nodes2 { get; set; }


        private string _topicValue;
        public string TopicValue
        {
            get { return _topicValue; }
            set { SetProperty(ref _topicValue, value); }
        }

        private Connection _connection;
        public Connection Connection
        {
            get { return _connection; }
            set { SetProperty(ref _connection, value); }
        }
        

        #region Command
        public DelegateCommand TestCommand { get; set; }
        public DelegateCommand ConnectCommand { get; set; }


        public DelegateCommand<object> TopicSelectCommand { get; set; }
        #endregion

        private Client MqttClient;

        public MainViewModel()
        {
            MqttClient = new Client();

            MqttClient.MessageEvent += MqttClient_MessageEvent;
            MqttClient.ConnectEvent += MqttClient_ConnectEvent;
            MqttClient.DisConnectEvent += MqttClient_DisConnectEvent;

            TestCommand = new DelegateCommand(TestMethod);
            ConnectCommand = new DelegateCommand(ConnectMethod);
            TopicSelectCommand = new DelegateCommand<object>(TopicSelectMethod);

            TagLocations = new ObservableCollection<TagLocation>();


            Nodes = new ObservableCollection<Node>();
            Nodes2 = new ObservableCollection<Node>();

            Connection = Connection.Connect;

            //Properties.Settings.Default.HOST;
            //Properties.Settings.Default.myCheck = checkBox1.Checked;
            //Properties.Settings.Default.myNumber = Convert.ToInt32(numericUpDown1.Value);
            //Properties.Settings.Default.Save();
        }

        public async void TestMethod()
        {
            string value = "{\"position\":{\"x\":4.5750785,\"y\":3.1802518,\"z\":-0.48464352,\"quality\":73},\"superFrameNumber\":677}";

            var node = JsonConvert.DeserializeObject<location>(value);

            Debug.WriteLine($"{node.position.y}");

            TagLocations.Add(new TagLocation()
            {
                Location = node,
            });

            Debug.WriteLine($"{TagLocations[0].Location.position.x}");


            //Node grp1 = new Node() { Key = 1, Name = "Group 1", SubNodes = new List<Node>() };
            //Node grp2 = new Node() { Key = 2, Name = "Group 2", SubNodes = new List<Node>() };
            //Node grp3 = new Node() { Key = 3, Name = "Group 3", SubNodes = new List<Node>() };
            //Node grp4 = new Node() { Key = 4, Name = "Group 4", SubNodes = new List<Node>() };

            //grp4.SubNodes.Add(grp1);
            //grp2.SubNodes.Add(grp4);

            //Nodes.Add(grp1);
            //Nodes.Add(grp2);
            //Nodes.Add(grp3);

            var popup = new IpAddressPopup();

            popup.ShowDialog();
        }

        public void ConnectMethod()
        {
            // to connect
            if(Connection == Connection.Connect){

                var popup = new IpAddressPopup();

                popup.BindData.IpAddress = Properties.Settings.Default.HOST;
                popup.BindData.Port = Properties.Settings.Default.PORT;

                var result = popup.ShowDialog();

                if (result.Value)
                {
                    var ipAddress = popup.BindData.IpAddress.Trim();
                    var port = popup.BindData.Port;

                    if (!string.IsNullOrEmpty(ipAddress))
                    {
                        MqttClient.Run(ipAddress, port);

                        Properties.Settings.Default.HOST = ipAddress;
                        Properties.Settings.Default.PORT = port;
                        Properties.Settings.Default.Save();
                    }
                }
            }

            // to disconnect
            if (Connection == Connection.Disconnect){
                MqttClient.Stop();
            }
            
        }
        private void TopicSelectMethod(object sender)
        {
            try
            {
                Node node = sender as Node;
                if (node != null)
                {
                    if(!string.IsNullOrEmpty(node.Value))
                    {
                        TopicValue = JValue.Parse(node.Value).ToString(Formatting.Indented);
                    }
                    else
                    {
                        TopicValue = node.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
        private void MqttClient_ConnectEvent(object sender, MqttClientConnectedEventArgs e)
        {
            Connection = Connection.Disconnect;
        }
        private void MqttClient_DisConnectEvent(object sender, MqttClientDisconnectedEventArgs e)
        {
            Connection = Connection.Connect;
        }
        private void MqttClient_MessageEvent(object sender, MqttApplicationMessageReceivedEventArgs e)
        {
            MqttApplicationMessage message = e.ApplicationMessage;

            logger.Write("### RECEIVED MESSAGE ###");
            logger.Write($"+ Topic = {message.Topic}");
            logger.Write($"+ Payload = {Encoding.UTF8.GetString(message.Payload)}");
            //logger.Write($"+ QoS = {obj.ApplicationMessage.QualityOfServiceLevel}");
            logger.Write($"+ Retain = {message.Retain}");
            logger.Write("");


            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
            {
                UpdateTreeNode(message);
            }));

        }
        private void UpdateTreeNode(MqttApplicationMessage message)
        {
            string[] array = message.Topic.Split(new char[] { '/' } , StringSplitOptions.RemoveEmptyEntries);


            IList<Node> nodes = Nodes;
            for (int index = 0; index < array.Length; index++)
            {
                var name = array[index];
                var value = string.Empty;
                var isExpanded = true;

                if (array.Length - index < 3){
                    isExpanded = false;
                }


                if (index == (array.Length - 1)){
                    value = $"{Encoding.UTF8.GetString(message.Payload)}";
                }

                var find = nodes.FirstOrDefault(x => x.Name == name);
                if(find != null)
                {
                    find.Value = value;
                    nodes = find.SubNodes;  //  다음 노드
                }
                else
                {
                    var newNode = new Node() { Key = index, Name = name, SubNodes = new ObservableCollection<Node>() };
                    newNode.Value = value;
                    newNode.IsExpanded = isExpanded;

                    nodes.Add(newNode);
                    nodes = newNode.SubNodes;
                }
            }


        }
        void Recursion(IList<Node> nodes, string value, int depth)
        {
            //var find = nodes.FirstOrDefault(x => x.Name == value);
            //if (find != null)
            //{

            //}

            //node.Depth = depth;
            //foreach (Node child in n.Children)
            //{
            //    this.Recursion(child, depth++);
            //}
        }
    }
}
