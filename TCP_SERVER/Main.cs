using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCP_SERVER
{
    public partial class Main : Form
    {
        private TcpListener connListener;
        private int connListenerPort = 4000;

        private TcpListener imgListener;
        private int imgListenerPort = 4001;

        private TcpClient client;
        private TcpClient imgClient;
        private Thread imageListenThread;
        private Thread listenerThread;

        public Main()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            try
            {
                listenerThread = new Thread(ListenConnection);
                listenerThread.Start();
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(imgListener != null)
            {
                return;
            }
            try
            {
                if (imageListenThread == null)
                { 
                    imageListenThread.Abort();
                }
            }
            catch { }
            KillTheServer();
            Close();
        }
        private void KillTheServer() //form kapandığı yada server Disconnect edildiği zaman çağrılır
        {
            try
            {
                if (connListener != null)
                {
                    connListener.Stop(); //Server=svr;
                }
            }
            catch { }

          

            Thread.Sleep(15);

            connListener = null;
        }

        private void ListenConnection()
        {
            if(connListener == null)
            {
                connListener = new TcpListener(IPAddress.Any, connListenerPort);
                connListener.Start();

            }
            
            // Starts listening for connections.
            while (true)
            {
                client = connListener.AcceptTcpClient();
                // After it gets accepted, it breaks the loop since we don't need it anymore.
                break;
            }
            lblConnected.Text = "Connected: " + client.Client.RemoteEndPoint.ToString();
            imageListenThread = new Thread(ListenImageReceive);
            // Sets the new thread to Deserialize the images.
            imageListenThread.Start();
        }

        private void ListenImageReceive()
        {
            imgListener = new TcpListener(IPAddress.Any, imgListenerPort);
            imgListener.Start();
            // Starts the imgListener.
            while (imageListenThread.IsAlive)
            {
                imgClient = imgListener.AcceptTcpClient();
                // Same like above.
                break;
            }
            BinaryFormatter formatter = new BinaryFormatter();
            while (imgClient.Connected)
            {
                using (NetworkStream stream = imgClient.GetStream())
                {
                    while (imgClient.Connected==true)
                    {
                        try
                        {
                            picDisplay.Image = (Image)formatter.Deserialize(stream);
                        }
                        catch (Exception)
                        {
                            picDisplay = null;
                            break;

                        }                       
                    }                                       
                }
            }
        }

        private void BtnListen_Click(object sender, EventArgs e)
        {
            try
            {
                listenerThread = new Thread(ListenConnection);
                listenerThread.Start();
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void PicDisplay_Click(object sender, EventArgs e)
        {

        }
    }
}
