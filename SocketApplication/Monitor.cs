using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;
using SocketLayerOperation;

namespace SocketApplication
{
    class Monitor
    {
        SocketLayer socketLay = new SocketLayer();
        public void checkSocket(string server1,string data, string port)
        {
            string[] array = new string[] { server1, data, port };
            string[]  args = array;

           
            String server = args[0]; // Server name or IP address  

            // Convert input String to bytes  
            byte[] byteBuffer = socketLay.stringtoByte(args[1]);

            // Use port argument if supplied, otherwise default to 7  
            int servPort = (args.Length == 3) ? Int32.Parse(args[2]) : 7;

            TcpClient client = null;
            NetworkStream ns = null;

            try
            {
                // Create socket that is connected to server on specified port  
                client = socketLay.tcpClientCreate(server, servPort);

                Console.WriteLine("Connected to server......");

                ns = socketLay.networkStream(client);

                // Send the encoded string to the server  
                ns.Write(byteBuffer, 0, byteBuffer.Length);

                Console.WriteLine("Sent {0} bytes to server...", byteBuffer.Length);

                int totalBytesRcvd = 0; // Total bytes received so far  
                int bytesRcvd = 0; // Bytes received in last read  

                
                //Receive the same string back from the server
                var task = Task.Run(() =>
                {
                              
                while (totalBytesRcvd < byteBuffer.Length)
                {
                    if ((bytesRcvd = ns.Read(byteBuffer, totalBytesRcvd,
                    byteBuffer.Length - totalBytesRcvd)) == 0)
                    {
                        Console.WriteLine("Connection closed prematurely.");
                        break;
                    }
                    totalBytesRcvd += bytesRcvd;
                }
                });
                bool isCompletedSuccessfully = task.Wait(TimeSpan.FromMilliseconds(300));

                if (isCompletedSuccessfully)
                {
                    //return task.Result;
                    Console.WriteLine("Received {0} bytes from server: {1}", totalBytesRcvd,
               Encoding.ASCII.GetString(byteBuffer, 0, totalBytesRcvd));
                }
                else
                {
                    throw new TimeoutException("The function has taken longer than the maximum time allowed.");
                }
              
                ns.Close();
                client.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                
            }
        }
    }
}
