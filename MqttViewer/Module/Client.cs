using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Client.Options;
using MQTTnet.Client.Receiving;
using MQTTnet.Extensions.ManagedClient;
using MqttViewer.Utility;
using System;
using System.Threading.Tasks;

namespace MqttViewer.Module
{

    // https://github.com/linfx/MqttFx

    public class Client
    {
        private Logger logger = Logger.Instance;

        private IManagedMqttClient mqttClient;


        public event EventHandler<MqttApplicationMessageReceivedEventArgs> MessageEvent;
        public event EventHandler<MqttClientConnectedEventArgs>            ConnectEvent;
        public event EventHandler<MqttClientDisconnectedEventArgs>         DisConnectEvent;

        public void Test()
        {


        }

        public async void Stop()
        {
            if(mqttClient != null)
            {
                await mqttClient.StopAsync();
            }
        }
        public async void Run(string host, int port)
        {
            try
            {

                ManagedMqttClientOptions options = new ManagedMqttClientOptions();

                options.ClientOptions = new MqttClientOptions()
                {
                    ClientId = "MqttClientApp",
                    ChannelOptions = new MqttClientTcpOptions
                    {
                        Server = host,
                        Port = port,
                    },
                    
                };
                options.AutoReconnectDelay = TimeSpan.FromSeconds(5);

                mqttClient = new MqttFactory().CreateManagedMqttClient();

                mqttClient.ConnectedHandler = new MqttClientConnectedHandlerDelegate(OnConnected);
                mqttClient.DisconnectedHandler = new MqttClientDisconnectedHandlerDelegate(OnDisconnected);
                mqttClient.ConnectingFailedHandler = new ConnectingFailedHandlerDelegate(OnConnectingFailed);
                mqttClient.ApplicationMessageReceivedHandler = new MqttApplicationMessageReceivedHandlerDelegate(OnMessageReceived);


                try
                {
                    await mqttClient.StartAsync(options);
                }
                catch (Exception exception)
                {
                    logger.Write("[ CONNECTING FAILED ]" + Environment.NewLine + exception);
                }

                try
                {
                    await mqttClient.SubscribeAsync("#");
                }
                catch (Exception exception)
                {
                    logger.Write(exception.Message);
                }

                //while (true)
                //{
                //    Console.ReadLine();

                //    await client.SubscribeAsync("test");

                //    var applicationMessage = new MqttApplicationMessageBuilder()
                //        .WithTopic("A/B/C")
                //        .WithPayload("Hello World")
                //        .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
                //        .Build();

                //    await client.PublishAsync(applicationMessage);
                //}

                //await managedClient.UnsubscribeAsync("xyz", "abc");
                //await managedClient.StopAsync();
            }
            catch (Exception exception)
            {
                logger.Write(exception.ToString());
            }
        }


        public void OnConnected(MqttClientConnectedEventArgs obj)
        {
            logger.Write("CONNECTED WITH SERVER");
            ConnectEvent?.Invoke(this, obj);
        }

        public void OnConnectingFailed(ManagedProcessFailedEventArgs obj)
        {
            Console.WriteLine("Couldn't connect to broker.");
        }

        public void OnDisconnected(MqttClientDisconnectedEventArgs obj)
        {
            logger.Write("DISCONNECTED FROM SERVER");
            DisConnectEvent?.Invoke(this, obj);
        }
        public void OnMessageReceived(MqttApplicationMessageReceivedEventArgs obj)
        {
            MessageEvent?.Invoke(this, obj);

            //logger.Write("### RECEIVED MESSAGE ###");
            //logger.Write($"+ Topic = {obj.ApplicationMessage.Topic}");
            //logger.Write($"+ Payload = {Encoding.UTF8.GetString(obj.ApplicationMessage.Payload)}");
            ////logger.Write($"+ QoS = {obj.ApplicationMessage.QualityOfServiceLevel}");
            //logger.Write($"+ Retain = {obj.ApplicationMessage.Retain}");
            //logger.Write("");
        }
    }
}
