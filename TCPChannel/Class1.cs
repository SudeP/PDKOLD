using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels.Ipc;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Metadata;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Runtime.Remoting.MetadataServices;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Remoting.Services;
using System.Net.NetworkInformation;

namespace PDK.TCPChannel
{
    public class StaticValues
    {
        public const int port = 11000;
    }
    public class TCPTeller
    {
        public int Port { get; private set; }
        public string ServiceName { get; private set; }
        public string TCPUri { get; private set; }
        internal TCPTeller(Type serviceType, int port, string serviceName)
        {
            Port = port;

            if (!IsAvailable())
                throw new Exception("port address is full");

            ServiceName = serviceName ?? serviceType.Name;

            var tc = new TcpChannel(Port);

            ChannelServices.RegisterChannel(tc, true);

            RemotingConfiguration.RegisterWellKnownServiceType(serviceType, ServiceName, WellKnownObjectMode.SingleCall);

            TCPUri = tc.GetUrlsForUri(ServiceName).FirstOrDefault();
        }
        private bool IsAvailable()
        {

            bool isAvailable = true;

            IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
            TcpConnectionInformation[] tcpConnInfoArray = ipGlobalProperties.GetActiveTcpConnections();

            foreach (TcpConnectionInformation tcpi in tcpConnInfoArray)
            {
                if (tcpi.LocalEndPoint.Port == Port)
                {
                    isAvailable = false;
                    break;
                }
            }

            return isAvailable;
        }
    }
    public class TCPListener<T> where T : class, new()
    {
        public T Way { get; set; }
        public TCPListener(string uri) => Way = Activator.GetObject(typeof(T), uri) as T;
    }
    public class TCPSession<T> where T : class, new()
    {
        public TCPTeller Teller { get; set; }
        public TCPListener<T> Listener { get; set; }
        public void SetTeller(Type serviceType) => SetTeller(serviceType, StaticValues.port, null);
        public void SetTeller(Type serviceType, int port) => SetTeller(serviceType, port, null);
        public void SetTeller(Type serviceType, string serviceName) => SetTeller(serviceType, StaticValues.port, serviceName);
        public void SetTeller(Type serviceType, int port, string serviceName) => Teller = new TCPTeller(serviceType, port, serviceName);
        public void CreateNewListener(string uri) => Listener = new TCPListener<T>(uri);
    }
    public class BasicDialogue
    {
        public ResponseObject Execute(RequestObject requestObject)
        {
            return default;
        }
    }
    [Serializable]
    public class RequestObject
    {
        public string Data { get; set; }
        public void SetData(object data)
        {

        }
    }
    [Serializable]
    public class ResponseObject
    {
        public string Data { get; private set; }
        public ResponseStatus ResponseStatus { get; private set; }
        public objectType GetData<objectType>()
        {
            return default;
        }
    }
    [Serializable]
    [Flags]
    public enum ResponseStatus : byte
    {
        None = 0,
        Success = 2,
        Error = 4,
        UnAuth = 6
    }
    public class DialogueArgs : EventArgs
    {
        public string ClientName { get; set; }
        public string ClientIp { get; set; }
        public string ClientId { get; set; }
    }
}
