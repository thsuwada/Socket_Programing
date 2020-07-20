using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Sockets;
using SocketTest;

namespace SocketTest
{
    [TestClass]
    public class socket
    {
        [TestMethod]
        public void Create_Socket_ReturnTrue()
        {
            
            var client = new SocketLayerOperation.SocketLayer();
            var result = client.tcpClientCreate("localhost", 80);
            Assert.IsTrue(result.Connected);
            
        }
        [TestMethod]
        public void Network_StreamData()
        {


            var client = new SocketLayerOperation.SocketLayer();
            var result = client.tcpClientCreate("localhost", 80);
            // var client = new SocketLayerOperation.SocketLayer().;
            var result2 = result.GetStream();
            Assert.IsTrue(result2.CanRead);

        }
    }
}
