using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;

namespace SocketLayerOperation
{
    public class SocketLayer
    {
        public byte[] stringtoByte(string stringVal)
        {
            // Convert input String to bytes  
            byte[] byteBuffer = Encoding.ASCII.GetBytes(stringVal);

            return byteBuffer;
        }

        public TcpClient tcpClientCreate(String server, int port)
        {
            TcpClient client = null;
            client = new TcpClient(server, port);
            return client;
        }

        public NetworkStream networkStream(TcpClient tcpStream)
        {
            NetworkStream network = null;
            network = tcpStream.GetStream();
            return network;
        }
          

       


    }
}
