using CommonClassLibs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TCP_SERVER;
using System.Drawing.Imaging;
using Microsoft.Office.Interop.Excel;
using MaterialSkin.Controls;
using MaterialSkin;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.Devices;
using Squirrel;
using System.Windows.Threading;

namespace TCP_SERVER
{
    public partial class frmServer : MaterialForm
    {
        public frmServer()
        {
            InitializeComponent();
            listView1.MouseDown+=new MouseEventHandler(listView1_MouseDown);
            listView1.MouseDoubleClick+=new MouseEventHandler(listView1_MouseDoubleClick);
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);

        }
        
       
        bool ServerIsExiting = false;
        Server svr = null;

        private Dictionary<int, MotherOfRawPackets> dClientRawPacketList = null;
        private Queue<FullPacket> FullPacketList = null;
        static AutoResetEvent autoEvent;//mutex
        static AutoResetEvent autoEvent2;//mutex
        private Thread DataProcessThread = null;
        private Thread FullPacketDataProcessThread = null;
        System.Timers.Timer timerGarbagePatrol = null;
        System.Timers.Timer timerPing = null;

        
        public System.Windows.Forms.ColorDepth ColorDepth { get; set; }
        private void frmServer_Load(object sender, EventArgs e)
        {

            AddVersionNumber();
            CheckForUpdates();
            label1.Text = Properties.Settings.Default.Ipaddresi;
            this.label1.ForeColor = Color.Green;
            label2.Text = Properties.Settings.Default.managPort;
            Control.CheckForIllegalCrossThreadCalls = false;
            // Comm -- İletişim
            
            // pictureBox1.BackColor = Color.Red;
            pictureBox1.Image = Properties.Resources.offline; 

            labelstatusinfo.ForeColor = System.Drawing.Color.Red;

            
            

            if (txtboxComunication.TextLength > 0)
                txtboxComunication.AppendText(Environment.NewLine);

            txtboxComunication.AppendText($"{GeneralFunction.GetDateTimeFormatted} >>>>> Please Press 'Start Server Button'... ");


            txtboxComunication.AppendText(Environment.NewLine);
            serverlogsaver();
            //mydocukectme set
            CheckOnApplicationDirectory();

            //Start listen


            // ping düzgün -TİMERR --- düzgün deigl
            timerPing = new System.Timers.Timer();
            timerPing.Interval = 240000;// 4 minute ping timer
            timerPing.Enabled = true;
            timerPing.Elapsed += timerPing_Elapsed;

            timerGarbagePatrol = new System.Timers.Timer();
            timerGarbagePatrol.Interval = 600000; // 5 min bağlantı bütünlüğü
            timerGarbagePatrol.Enabled = true;
            timerGarbagePatrol.Elapsed += timerGarbagePatrol_Elapsed;
            SetHostNameAndAddress();


        }


        private void serverlogsaver()
        {

            if (Properties.Settings.Default.ServerSavePath == "")
            {
                Properties.Settings.Default.ServerSavePath= Environment.GetFolderPath(Environment.SpecialFolder.Desktop)+"/SERVER_RECEIVE_FILES";
            }

            string saveInfoFolder = Properties.Settings.Default.ServerSavePath;
            // Server_LOG un kayıt yeri             
            
            DirectoryInfo newDirectory = new DirectoryInfo(saveInfoFolder);


            if (!newDirectory.Exists)
            {
                newDirectory.Create();
            }
            
            // LOG html olarak kayıt oluyor çözüm üret
            string saveRepositoryResults = saveInfoFolder + "/SERVER_LOG.txt";
            FileInfo RepositoryResults = new FileInfo(saveRepositoryResults);
            if (!RepositoryResults.Exists)
            {
                string createText;
                createText = txtboxComunication.Text;

                System.IO.File.WriteAllText(saveRepositoryResults, createText);
                
            }
            else
            {
                string createText;
                createText = txtboxComunication.Text.ToString();
                File.WriteAllText(saveRepositoryResults, createText);

            }
        }
        
        private void frmServer_FormClosing(object sender, FormClosingEventArgs e)
        { // Form Kapandığı takdirde
            if (svr != null)
            {
                PACKET_DATA xdata = new PACKET_DATA();

                xdata.Packet_Type = (UInt16)PACKETTYPES.TYPE_HostExiting; 
                xdata.Data_Type = 0;
                xdata.Packet_Size = 16;
                xdata.maskTo = 0;
                xdata.idTo = 0;
                xdata.idFrom = 0;

                byte[] byData = PACKET_FUNCTIONS.StructureToByteArray(xdata);

                svr.SendMessage(byData);

                Thread.Sleep(250);
            }

            ServerIsExiting = true; // server yoksa 
            try
            {
                if (timerGarbagePatrol != null)
                { //durdur ve dispose et
                    timerGarbagePatrol.Stop();
                    timerGarbagePatrol.Elapsed -= timerGarbagePatrol_Elapsed;
                    timerGarbagePatrol.Dispose();
                    timerGarbagePatrol = null;
                }
            }
            catch { }

            try
            {
                if (timerPing != null)
                { //ping time durdur
                    timerPing.Stop();
                    timerPing.Elapsed -= timerPing_Elapsed;
                    timerPing.Dispose();
                    timerPing = null;
                }
            }
            catch { }

            KillTheServer(); // serverı öldürmek için gerekli fonksunu çağır
            System.Windows.Forms.Application.Exit(); // programı kapat
        }
        

        private void StartPacketCommunicationsServiceThread() //Communication için iletişim başlatma
        {
            int MyPort;
            if (int.TryParse(Properties.Settings.Default.managPort, out MyPort))
            {
                if (5000 <= MyPort)
                {


                    try
                    {
                        //Packet processor mutex and loop
                        autoEvent = new AutoResetEvent(false); //the RawPacket data mutex
                        autoEvent2 = new AutoResetEvent(false);//the FullPacket data mutex
                        DataProcessThread = new Thread(new ThreadStart(NormalizeThePackets));
                        FullPacketDataProcessThread = new Thread(new ThreadStart(ProcessReceivedData));


                        //Lists
                        dClientRawPacketList = new Dictionary<int, MotherOfRawPackets>();
                        FullPacketList = new Queue<FullPacket>();

                        //Create HostServer
                        svr = new Server();

                        svr.Listen(MyPort);//MySettings.HostPort);
                        svr.OnReceiveData += new Server.ReceiveDataCallback(OnDataReceived);
                        svr.OnClientConnect += new Server.ClientConnectCallback(NewClientConnected);
                        svr.OnClientDisconnect += new Server.ClientDisconnectCallback(ClientDisconnect);

                        DataProcessThread.Start();
                        FullPacketDataProcessThread.Start();


                        if (txtboxComunication.TextLength > 0)
                            txtboxComunication.AppendText(Environment.NewLine);

                        txtboxComunication.AppendText($"{GeneralFunction.GetDateTimeFormatted} >>>>> SERVER ONLINE Listening Port: ({MyPort}) ");


                        txtboxComunication.AppendText(Environment.NewLine);
                        serverlogsaver();

                    }
                    catch (Exception ex)
                    {
                        var exceptionMessage = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;


                        if (txtboxComunication.TextLength > 0)
                            txtboxComunication.AppendText(Environment.NewLine);

                        txtboxComunication.AppendText($"{GeneralFunction.GetDateTimeFormatted} >>>>> SERVER FAILED TO START ");


                        txtboxComunication.AppendText(Environment.NewLine);

                        serverlogsaver();


                    }


                }
                else
                {

                    MessageBox.Show("please enter a port number above 5000");
                }
            }
                                  
    
        }

        private void KillTheServer() //form kapandığı yada server Disconnect edildiği zaman çağrılır
        {
            try
            {
                if (svr != null)
                {
                    svr.Stop(); //Server=svr;
                }
            }
            catch { }

            try
            {
                if (autoEvent != null)
                {
                    autoEvent.Set();

                    Thread.Sleep(30);
                    autoEvent.Close();
                    autoEvent.Dispose();
                    autoEvent = null;
                }
            }
            catch { }

            try
            {
                if (autoEvent2 != null)
                {
                    autoEvent2.Set();

                    Thread.Sleep(30);
                    autoEvent2.Close();
                    autoEvent2.Dispose();
                    autoEvent2 = null;
                }
            }
            catch { }

            Thread.Sleep(15);

            try
            {
                if (dClientRawPacketList != null)
                {
                    dClientRawPacketList.Clear();
                    dClientRawPacketList = null;
                }
            }
            catch { }

            svr = null;
        }

        #region TIMERS
        /// <summary>
        /// Fires every 4 minutes
        /// </summary>
        void timerPing_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            PingTheConnections();
        }
        void PingTheConnections()
        {
            if (svr == null)
                return;

            try
            {
                PACKET_DATA xdata = new PACKET_DATA();

                xdata.Packet_Type = (UInt16)PACKETTYPES.TYPE_Ping;
                xdata.Data_Type = 0;
                xdata.Packet_Size = 16;
                xdata.maskTo = 0;
                xdata.idTo = 0;
                xdata.idFrom = 0;

                xdata.DataLong1 = DateTime.UtcNow.Ticks;

                byte[] byData = PACKET_FUNCTIONS.StructureToByteArray(xdata);

                //Stopwatch sw = new Stopwatch();

                //sw.Start();
                lock (svr.workerSockets)
                {
                    foreach (Server.UserSock s in svr.workerSockets.Values)
                    {
                        //Console.WriteLine("Ping id - " + s.iClientID.ToString());
                        //Thread.Sleep(25);//allow a slight moment so all the replies dont happen at the same time
                        s.PingStatClass.StartTheClock();

                        try
                        {
                            svr.SendMessage(s.iClientID, byData);

                        }
                        catch { }
                    }
                }
                //sw.Stop();
                //Debug.WriteLine("TimeAfterSend: " + sw.ElapsedMilliseconds.ToString() + "ms");
            }
            catch { }
        }
        /**********************************************************************************************************************/

        //private void GarbagePatrol_Tick(object sender, EventArgs e)
        private void timerGarbagePatrol_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                CheckConnectionTimersGarbagePatrol();
            }
            catch { }
        }

        private void CheckConnectionTimersGarbagePatrol()
        {
            List<int> ClientIDsToClear = new List<int>();

            Debug.WriteLine($"{svr.workerSockets.Values.Count} - List Count: {svr.workerSockets.Values.Count}");

            lock (svr.workerSockets)
            {
                foreach (Server.UserSock s in svr.workerSockets.Values)
                {
                    TimeSpan diff = DateTime.Now - s.dTimer;
                    //Debug.WriteLine("iClientID: " + s.iClientID + " - " + "Time: " + diff.TotalSeconds.ToString());

                    if (diff.TotalSeconds >= 600 || s.UserSocket.Connected == false)//10 minutes
                    {
                        //Punt the ListVeiw item here but we must make a list of
                        //clients that we have lost connection with, its not good to remove
                        //the Servers internal client item while inside its foreach loop;
                        //listView1.Items.RemoveByKey(s.iClientID.ToString());
                        ClientIDsToClear.Add(s.iClientID);
                    }
                }
            }

            Debug.WriteLine($"{DateTime.Now.ToLongTimeString()} - Garbage Patrol num of IDs to remove: {ClientIDsToClear.Count}");

            //Ok remove any internal data items we may have
            if (ClientIDsToClear.Count > 0)
            {
                foreach (int cID in ClientIDsToClear)
                {
                    SendMessageOfClientDisconnect(cID);

                    CleanupDeadClient(cID);
                    Thread.Sleep(5);
                }
            }
        }

        private delegate void CleanupDeadClientDelegate(int clientNumber);
        private void CleanupDeadClient(int clientNumber)
        {
            if (InvokeRequired)
            {
                this.Invoke(new CleanupDeadClientDelegate(CleanupDeadClient), new object[] { clientNumber });
                return;
            }

            try
            {
                lock (dClientRawPacketList)
                {
                    if (dClientRawPacketList.ContainsKey(clientNumber))
                    {
                        dClientRawPacketList[clientNumber].ClearList();
                        dClientRawPacketList.Remove(clientNumber);
                    }
                }
            }
            catch (Exception )
            {
                
            }

            try
            {
                lock (svr.workerSockets)
                {
                    if (svr.workerSockets.ContainsKey(clientNumber))
                    {
                        svr.workerSockets[clientNumber].UserSocket.Close();
                        svr.workerSockets.Remove(clientNumber);
                    }
                }
            }
            catch { }

            try
            {                
                if (listView1.Items.ContainsKey(clientNumber.ToString()))
                {
                    if (Properties.Settings.Default.listviewremover == "listsaveremove")
                    {
                        listView1.Items.RemoveByKey(clientNumber.ToString());

                    }
                    else
                    {
                        return;
                    }
                        
                }
            }
            catch { }

        }
        #endregion

        #region TCPIP Layer incoming data
        private void OnDataReceived(int clientNumber, byte[] message, int messageSize)
        {
            if (dClientRawPacketList.ContainsKey(clientNumber))
            {
                dClientRawPacketList[clientNumber].AddToList(message, messageSize);
                //bağlanmak için gelen clientNumber
                
                autoEvent.Set();//
            }
        }
        #endregion

        #region CLIENT CONNECTION PROCESS
        private void NewClientConnected(int ConnectionID)
        {
            try
            {
                Debug.WriteLine($"(RT Client)NewClientConnected: {ConnectionID}");

                
                if (svr.workerSockets.ContainsKey(ConnectionID))//bağlanan client number için com ekranında yazı yazıdılır
                {
                    lock (dClientRawPacketList)
                    {
                        //packet raw a eklenir ve yenisi açılır
                        if (!dClientRawPacketList.ContainsKey(ConnectionID))
                        {
                            dClientRawPacketList.Add(ConnectionID, new MotherOfRawPackets(ConnectionID));
                            
                        }
                    }
                    
                    if (Properties.Settings.Default.ServerConnect== "serverconnectconfig")
                    {
                        

                        if (MessageBox.Show("Do you want the client to connect?  "+   ConnectionID, "Client Connection confirmation? ", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {


                            SetNewConnectionData_FromThread(ConnectionID);
                        }
                    }
                    else
                    {
                        SetNewConnectionData_FromThread(ConnectionID);
                    }
                }
                else
                {   //Expec ConnectiOn ID NONE
                    Debug.WriteLine("UNKNOWN CONNECTIONID" + ConnectionID.ToString());
                }
            }
            catch (Exception ex)
            {
                
                if (txtboxComunication.TextLength > 0)
                    txtboxComunication.AppendText(Environment.NewLine);

                txtboxComunication.AppendText($"{GeneralFunction.GetDateTimeFormatted} >>>>> EXCEPTION: NewClientConnected on client {ConnectionID}, exception: {ex.Message} ");


                txtboxComunication.AppendText(Environment.NewLine);

                serverlogsaver();
            }
        }

        private delegate void SetNewConnectionDataDelegate(int clientNumber);
        private void SetNewConnectionData_FromThread(int clientNumber)//new connection set listview1
        {
            if(InvokeRequired) 
            {
                this.Invoke(new SetNewConnectionDataDelegate(SetNewConnectionData_FromThread), new object[] { clientNumber });
                return;
            }

            try
            {
                lock (svr.workerSockets)
                {
                    // add new data listview1.ıtem
                    ListViewItem li = new ListViewItem(svr.workerSockets[clientNumber].UserSocket.RemoteEndPoint.ToString());
                    li.Name = clientNumber.ToString();//Set the Key as a unique identifier
                    li.Tag = clientNumber;

                    listView1.Items.Add(li);                    //index 0 Clients IP address
                    li.SubItems.Add("Receiving...");            //index 1 Computer name
                    li.SubItems.Add("Receiving...");            //index 2 Client ID                   
                    li.SubItems.Add("Receiving...");            //index 3 CLient Name Surname
                    li.SubItems.Add(clientNumber.ToString());     //index 4 Client Number
                     li.SubItems.Add("Receiving...");             //index 6 ping tiem


                    
                    
                }
                if (svr.workerSockets[clientNumber].UserSocket.Connected)
                {
                    
                        RequestNewConnectionCredentials(clientNumber);
                    

                }
                else
                {
                    Debug.WriteLine($"ISSUE!!!(RequestNewConnectionCredentials) UserSocket.Connected is FALSE from: {clientNumber}");
                }
            }
            catch (Exception ex)
            {
                
                if (txtboxComunication.TextLength > 0)
                    txtboxComunication.AppendText(Environment.NewLine);

                txtboxComunication.AppendText($"{GeneralFunction.GetDateTimeFormatted} >>>>> EXCEPTION: SetNewConnectionData_FromThread on client {clientNumber}, exception: {ex.Message} ");


                txtboxComunication.AppendText(Environment.NewLine);

                serverlogsaver();
            }
        }

        
        private delegate void PostUserCredentialsDelegate(int clientNumber, byte[] message);
        /// <summary>
        /// return bool, TRUE if its a FullClient Connection
        /// </summary>
        /// <param name="clientNumber"></param>
        /// <param name="message"></param>
        private void PostUserCredentials(int clientNumber, byte[] message)
        { //ıncoming client dataları ID name computer name burada
            if (InvokeRequired)
            {
                this.Invoke(new PostUserCredentialsDelegate(PostUserCredentials), new object[] { clientNumber, message });
                return;
            }

            try
            {
                PACKET_DATA IncomingData = new PACKET_DATA();
                IncomingData = (PACKET_DATA)PACKET_FUNCTIONS.ByteArrayToStructure(message, typeof(PACKET_DATA));

                lock (svr.workerSockets)
                {
                    string ComputerName = new string(IncomingData.szStringDataA).TrimEnd('\0');
                    string VersionStr = new string(IncomingData.szStringDataB).TrimEnd('\0');
                    string ClientsName = new string(IncomingData.szStringData150).TrimEnd('\0');
                    

                    listView1.Items[clientNumber.ToString()].SubItems[1].Text = ComputerName;
                    listView1.Items[clientNumber.ToString()].SubItems[3].Text = VersionStr;
                    
                    listView1.Items[clientNumber.ToString()].SubItems[2].Text = ClientsName;
                    if(ClientsName.ToString()=="SERVER")
                    {
                        listView1.Items.RemoveByKey(clientNumber.ToString());


                    }
                    

                    if (svr.workerSockets.ContainsKey(clientNumber))
                    {
                        svr.workerSockets[clientNumber].szStationName = ComputerName;
                        //client Computer Name
                        svr.workerSockets[clientNumber].szClientName = ClientsName;
                        //client number 

                        if (ClientsName.ToString() != "SERVER")
                        {
                            if (txtboxComunication.TextLength > 0)
                                txtboxComunication.AppendText(Environment.NewLine);

                            txtboxComunication.AppendText($"{GeneralFunction.GetDateTimeFormatted} >>>>> Client ID: '{ClientsName}'   Client Name: '{VersionStr}'  Client Number: '{clientNumber}'");


                            txtboxComunication.AppendText(Environment.NewLine);


                        }


                        serverlogsaver();
                        //com display write 
                        SendTheClientListToTheNewClient(clientNumber);


                        
                    }
                }
                
            }
            catch (Exception ex)
            {
                
                if (txtboxComunication.TextLength > 0)
                    txtboxComunication.AppendText(Environment.NewLine);

                txtboxComunication.AppendText($"{GeneralFunction.GetDateTimeFormatted} >>>>> EXCEPTION: PostUserCredentials on client {clientNumber}, exception: {ex.Message}");


                txtboxComunication.AppendText(Environment.NewLine);
                serverlogsaver();

            }


        }
        
        /// <summary>
        /// Send 
        /// </summary>
        /// <param name="clientNumber"></param>
        /// <param name="SendList"></param>
        private void SendTheClientListToTheNewClient(int clientNumber, bool SendList = true)
        {
            if (!svr.workerSockets.ContainsKey(clientNumber))
            {
                
                return;
            }

            string NewClientIP = string.Empty;
            string NewClientName = string.Empty;
            string NewStationtName = string.Empty;
            UInt16 NewClientPort;
            
            string NewAltIP = string.Empty;
            string clientversion = string.Empty;

            //hang onto the new clients ipaddress to send to the other clients
            lock (svr.workerSockets)
            {
                NewClientIP = ((IPEndPoint)svr.workerSockets[clientNumber].UserSocket.RemoteEndPoint).Address.ToString();
                NewClientName = svr.workerSockets[clientNumber].szClientName;
                NewStationtName = svr.workerSockets[clientNumber].szStationName;
                NewClientPort = svr.workerSockets[clientNumber].UserListentingPort;
                NewAltIP = svr.workerSockets[clientNumber].szAlternateIP;                
            }


            PACKET_CLIENTDATA xdata = new PACKET_CLIENTDATA();
            xdata.Packet_Type = (UInt16)PACKETTYPES.TYPE_ClientData;
            xdata.Data_Type = 0;
            xdata.Packet_Size = (UInt16)Marshal.SizeOf(typeof(PACKET_CLIENTDATA));
            xdata.maskTo = 0;
            xdata.idTo = 0;
            xdata.idFrom = 0;

            if (SendList)//true if this client just connected, then we need the list of ther other connected clients
            {
                lock (svr.workerSockets)
                {
                    foreach (Server.UserSock s in svr.workerSockets.Values)
                    {
                        if (clientNumber != s.iClientID) //Send all of the other connected clients to the one who just connected.
                        {
                            Array.Clear(xdata.szUsersAddress, 0, xdata.szUsersAddress.Length);
                            Array.Clear(xdata.szUserName, 0, xdata.szUserName.Length);
                            Array.Clear(xdata.szStationName, 0, xdata.szStationName.Length);
                            Array.Clear(xdata.szUsersAlternateAddress, 0, xdata.szUsersAlternateAddress.Length);
                            Array.Clear(xdata.szUsersClientVersion, 0, xdata.szUsersClientVersion.Length);

                            string p = s.szClientName;
                            if (p.Length > 49)
                                p.CopyTo(0, xdata.szUserName, 0, 49);
                            else
                                p.CopyTo(0, xdata.szUserName, 0, p.Length);
                            xdata.szUserName[49] = '\0';

                            p = s.szStationName;
                            if (p.Length > 49)
                                p.CopyTo(0, xdata.szStationName, 0, 49);
                            else
                                p.CopyTo(0, xdata.szStationName, 0, p.Length);
                            xdata.szStationName[49] = '\0';

                            string ip = ((IPEndPoint)s.UserSocket.RemoteEndPoint).Address.ToString();
                            ip.CopyTo(0, xdata.szUsersAddress, 0, ip.Length);
                            s.szAlternateIP.CopyTo(0, xdata.szUsersAlternateAddress, 0, s.szAlternateIP.Length);

                            xdata.iClientID = (UInt16)s.iClientID;

                            xdata.ListeningPort = s.UserListentingPort;
                            
                            byte[] byData = PACKET_FUNCTIONS.StructureToByteArray(xdata);

                            svr.SendMessage(clientNumber, byData);
                            Thread.Sleep(10);//set s short delay 
                            //ods.DebugOut("sent out: " + s.iClientID.ToString());
                        }
                    }
                }//end lock
            }

            lock (svr.workerSockets)
            {
                //Go through the list and send to the other clients the arrival or updates of the new client
                foreach (Server.UserSock s in svr.workerSockets.Values)
                {
                    if (clientNumber != s.iClientID)
                    {
                        Array.Clear(xdata.szUsersAddress, 0, xdata.szUsersAddress.Length);
                        Array.Clear(xdata.szUserName, 0, xdata.szUserName.Length);
                        Array.Clear(xdata.szStationName, 0, xdata.szStationName.Length);
                        Array.Clear(xdata.szUsersAlternateAddress, 0, xdata.szUsersAlternateAddress.Length);

                        string p = NewClientName;
                        if (p.Length > 49)
                            p.CopyTo(0, xdata.szUserName, 0, 49);
                        else
                            p.CopyTo(0, xdata.szUserName, 0, p.Length);
                        xdata.szUserName[49] = '\0';

                        p = NewStationtName;
                        if (p.Length > 49)
                            p.CopyTo(0, xdata.szStationName, 0, 49);
                        else
                            p.CopyTo(0, xdata.szStationName, 0, p.Length);
                        xdata.szStationName[49] = '\0';

                        string ip = NewClientIP;
                        ip.CopyTo(0, xdata.szUsersAddress, 0, ip.Length);

                        NewAltIP.CopyTo(0, xdata.szUsersAlternateAddress, 0, NewAltIP.Length);
                        clientversion.CopyTo(0, xdata.szUsersClientVersion, 0, clientversion.Length);

                        xdata.iClientID = (UInt16)clientNumber;
                        xdata.ListeningPort = NewClientPort;

                        byte[] byData = PACKET_FUNCTIONS.StructureToByteArray(xdata);

                        svr.SendMessage(s.iClientID, byData);
                        //Thread.Sleep(10);
                    }
                }
            }//end lock
        }



        private void ClientDisconnect(int clientNumber)
        {
            if (ServerIsExiting)
                return;

            /*******************************************************/
            lock (dClientRawPacketList)//Make sure we don't do this twice
            {
                if (!dClientRawPacketList.ContainsKey(clientNumber))
                {
                    lock (svr.workerSockets)
                    {
                        if (!svr.workerSockets.ContainsKey(clientNumber))
                        {
                            return;
                        }
                    }
                }
            }
            /*******************************************************/
            
            try
            {
                RemoveClient_FromThread(clientNumber);
            }
            catch (Exception ex)
            {
               
                if (txtboxComunication.TextLength > 0)
                    txtboxComunication.AppendText(Environment.NewLine);

                txtboxComunication.AppendText($"{GeneralFunction.GetDateTimeFormatted} >>>>> EXCEPTION: ClientDisconnect on client {clientNumber}, exception: {ex.Message} ");


                txtboxComunication.AppendText(Environment.NewLine);
                serverlogsaver();

            }

            CleanupDeadClient(clientNumber);

        
            Thread.Sleep(10);
        }

        private void RemoveClient_FromThread(int clientNumber)
        {
            try
            {
                SendMessageOfClientDisconnect(clientNumber);
                
                if (txtboxComunication.TextLength > 0)
                    txtboxComunication.AppendText(Environment.NewLine);

                txtboxComunication.AppendText($"{GeneralFunction.GetDateTimeFormatted} >>>>> Client Number '{clientNumber}' Disconnected ");


                txtboxComunication.AppendText(Environment.NewLine);
                serverlogsaver();

            }
            catch (Exception ex)
            {
                
                if (txtboxComunication.TextLength > 0)
                    txtboxComunication.AppendText(Environment.NewLine);

                txtboxComunication.AppendText($"{GeneralFunction.GetDateTimeFormatted} >>>>> EXCEPTION: RemoveClient_FromThread on client {clientNumber}, Exception: {ex.Message} ");


                txtboxComunication.AppendText(Environment.NewLine);

                serverlogsaver();
            }
        }
        #endregion

        #region Packet factory Processing from clients
        private void NormalizeThePackets()
        {
            if (svr == null)
                return;

            while (svr.IsListening)
            {
                autoEvent.WaitOne(10000);//wait at mutex until signal, or drop through every 10 seconds for fun

                
                lock (dClientRawPacketList)
                {
                    foreach (MotherOfRawPackets MRP in dClientRawPacketList.Values)
                    {
                        if (MRP.GetItemCount.Equals(0))
                            continue;
                        try
                        {
                            byte[] packetplayground = new byte[11264];//good for 10 full packets(10240) + 1 remainder(1024)
                            RawPackets rp;

                            int actualPackets = 0;

                            while (true)
                            {
                                if (MRP.GetItemCount == 0)
                                    break;

                                int holdLen = 0;

                                if (MRP.bytesRemaining > 0)
                                    Copy(MRP.Remainder, 0, packetplayground, 0, MRP.bytesRemaining);

                                holdLen = MRP.bytesRemaining;

                                for (int i = 0; i < 10; i++)//only go through a max of 10 times so there will be room for any remainder
                                {
                                    rp = MRP.GetTopItem;//dequeue

                                    Copy(rp.dataChunk, 0, packetplayground, holdLen, rp.iChunkLen);

                                    holdLen += rp.iChunkLen;

                                    if (MRP.GetItemCount.Equals(0))//make sure there is more in the list befor continuing
                                        break;
                                }

                                actualPackets = 0;

                                #region PACKET_SIZE 1024
                                if (holdLen >= 1024)//make sure we have at least one packet in there
                                {
                                    actualPackets = holdLen / 1024;
                                    MRP.bytesRemaining = holdLen - (actualPackets * 1024);

                                    for (int i = 0; i < actualPackets; i++)
                                    {
                                        byte[] tmpByteArr = new byte[1024];
                                        Copy(packetplayground, i * 1024, tmpByteArr, 0, 1024);
                                        lock (FullPacketList)
                                            FullPacketList.Enqueue(new FullPacket(MRP.iListClientID, tmpByteArr));
                                    }
                                }
                                else
                                {
                                    MRP.bytesRemaining = holdLen;
                                }

                                //hang onto the remainder
                                Copy(packetplayground, actualPackets * 1024, MRP.Remainder, 0, MRP.bytesRemaining);
                                #endregion

                                if (FullPacketList.Count > 0)
                                    autoEvent2.Set();

                            }//end of while(true)
                        }
                        catch (Exception ex)
                        {
                            MRP.ClearList();//pe 03-20-2013
                            string msg = (ex.InnerException == null) ? ex.Message : ex.InnerException.Message;
                            
                            
                            if (txtboxComunication.TextLength > 0)
                                txtboxComunication.AppendText(Environment.NewLine);

                            txtboxComunication.AppendText($"{GeneralFunction.GetDateTimeFormatted} >>>>> EXCEPTION in  NormalizeThePackets - " + msg );


                            txtboxComunication.AppendText(Environment.NewLine);

                            serverlogsaver();
                        }
                    }//end of foreach (dClientRawPacketList)
                }//end of lock
                /**********************************************/
                if (ServerIsExiting)
                    break;
            }//Endof of while(svr.IsListening)

            Debug.WriteLine("Exiting the packet normalizer");
            
            if (txtboxComunication.TextLength > 0)
                txtboxComunication.AppendText(Environment.NewLine);

            txtboxComunication.AppendText($"{GeneralFunction.GetDateTimeFormatted} >>>>> Exiting the packet normalizer ");


            txtboxComunication.AppendText(Environment.NewLine);
            serverlogsaver();

        }

        private void ProcessReceivedData()
        {
            if (svr == null)
                return;

            while (svr.IsListening)
            {                
                autoEvent2.WaitOne();//wait at mutex until signal               
                try
                {
                    while (FullPacketList.Count > 0)
                    {
                        FullPacket fp;
                        lock (FullPacketList)
                            fp = FullPacketList.Dequeue();                        
                        UInt16 type = (ushort)(fp.ThePacket[1] << 8 | fp.ThePacket[0]);
                        switch (type)
                        {
                            case (UInt16)PACKETTYPES.TYPE_MyCredentials:
                                {
                                    PostUserCredentials(fp.iFromClient, fp.ThePacket);
                                    SendRegisteredMessage(fp.iFromClient, fp.ThePacket);
                                }
                                break;
                            case (UInt16)PACKETTYPES.TYPE_CredentialsUpdate:
                                break;
                            case (UInt16)PACKETTYPES.TYPE_PingResponse:                                
                                UpdateTheConnectionTimers(fp.iFromClient, fp.ThePacket);
                                break;
                            case (UInt16)PACKETTYPES.TYPE_Close:
                                ClientDisconnect(fp.iFromClient);
                                break;
                            case (UInt16)PACKETTYPES.TYPE_Message:
                                {
                                    AssembleMessage(fp.iFromClient, fp.ThePacket);
                                }
                                break;
                            default:
                                PassDataThru(type, fp.iFromClient, fp.ThePacket);
                                break;
                        }
                    }//END  while 
                }//try
                catch (Exception ex)
                {
                    try
                    {
                        string msg = (ex.InnerException == null) ? ex.Message : ex.InnerException.Message;
                        
                        if (txtboxComunication.TextLength > 0)
                            txtboxComunication.AppendText(Environment.NewLine);

                        txtboxComunication.AppendText($"{GeneralFunction.GetDateTimeFormatted} >>>>> EXCEPTION in  ProcessRecievedData - {msg} ");


                        txtboxComunication.AppendText(Environment.NewLine);

                        serverlogsaver();
                    }
                    catch { }
                }

                if (ServerIsExiting)
                    break;
            }//End while (svr.IsListening)

            string info2 = string.Format("AppIsExiting = {0}", ServerIsExiting.ToString());
            string info3 = string.Format("Past the ProcessRecievedData loop");

            Debug.WriteLine(info2);
            Debug.WriteLine(info3);

            try
            {
                
                if (txtboxComunication.TextLength > 0)
                    txtboxComunication.AppendText(Environment.NewLine);

                txtboxComunication.AppendText($"{GeneralFunction.GetDateTimeFormatted} >>>>> { info3} ");


                txtboxComunication.AppendText(Environment.NewLine);
                serverlogsaver();


            }
            catch { }

            if (!ServerIsExiting)
            {
                //if we got here then something went wrong, we need to shut down the service
                
                if (txtboxComunication.TextLength > 0)
                    txtboxComunication.AppendText(Environment.NewLine);

                txtboxComunication.AppendText($"{GeneralFunction.GetDateTimeFormatted} >>>>> SOMETHING CRASHED ");


                txtboxComunication.AppendText(Environment.NewLine);

                serverlogsaver();
            }
        }
        
        private void PassDataThru(UInt16 type, int MessageFrom, byte[] message)
        {
            try
            {
                int ForwardTo = 0;
                switch (type)
                {
                    case (UInt16)PACKETTYPES.TYPE_FileStart:
                    case (UInt16)PACKETTYPES.TYPE_FileChunk:
                    case (UInt16)PACKETTYPES.TYPE_FileEnd:
                        {
                            
                            // Bitshift the messages 2nd and 3rd bits, which are the 'idTo' for these message types
                            // which contains the hostID of the client this message will be forwarded to.
                            ForwardTo = (ushort)(message[3] << 8 | message[2]);
                        }
                        break;
                    default:
                        {
                            /*********************************************************************************************************/
                            // Bitshift the messages 8th, 9th, 10th and 11th bits, which are the 'idTo' 
                            // which contains the hostID of the client this message will be forwarded to.
                            ForwardTo = (int)message[11] << 24 | (int)message[10] << 16 | (int)message[9] << 8 | (int)message[8];

                            /*********************************************************************************************************/

                            /*********************************************************************************************************/
                            //Then take the sending clients HostID and stuff in who this packet's 'idFrom' so we know who sent it!
                            byte[] x = BitConverter.GetBytes(MessageFrom);
                            message[12] = (byte)x[0];//idFrom
                            message[13] = (byte)x[1];//idFrom
                            message[14] = (byte)x[2];//idFrom
                            message[15] = (byte)x[3];//idFrom
                            /*********************************************************************************************************/
                        }
                        break;
                }

                if (ForwardTo > 0)
                    svr.SendMessage(ForwardTo, message);
                else
                    svr.SendMessage(message);
            }
            catch (Exception ex)
            {
                string msg = (ex.InnerException == null) ? ex.Message : ex.InnerException.Message;
                
                if (txtboxComunication.TextLength > 0)
                    txtboxComunication.AppendText(Environment.NewLine);

                txtboxComunication.AppendText($"{GeneralFunction.GetDateTimeFormatted} >>>>> EXCEPTION in  PassDataThru - {msg} ");


                txtboxComunication.AppendText(Environment.NewLine);

                serverlogsaver();
            }
        }

        #endregion

        private void AssembleMessage(int clientID, byte[] message)
        {
            try
            {
                PACKET_DATA IncomingData = new PACKET_DATA();
                IncomingData = (PACKET_DATA)PACKET_FUNCTIONS.ByteArrayToStructure(message, typeof(PACKET_DATA));

                switch(IncomingData.Data_Type)
                {
                    case (UInt16)PACKETTYPES_SUBMESSAGE.SUBMSG_MessageStart:
                        {
                            if (svr.workerSockets.ContainsKey(clientID))
                            {
                                if (IncomingData.idTo == 0)// message meant for the server
                                {
                                    
                                    if (txtboxComunication.TextLength > 0)
                                        txtboxComunication.AppendText(Environment.NewLine);

                                    txtboxComunication.AppendText($"{GeneralFunction.GetDateTimeFormatted} >>>>> Client '{svr.workerSockets[clientID].szClientName}':".TrimEnd('\0') );

                                    txtboxComunication.AppendText(Environment.NewLine);
                                                                       
                                    if (txtboxComunication.TextLength > 0)
                                        txtboxComunication.AppendText(Environment.NewLine);

                                    txtboxComunication.AppendText($"{GeneralFunction.GetDateTimeFormatted} >>>>> {new string(IncomingData.szStringDataA).TrimEnd('\0')}");


                                    txtboxComunication.AppendText(Environment.NewLine);

                                    serverlogsaver();
                                }
                                else
                                    svr.SendMessage((int)IncomingData.idTo, message);
                            }
                        }
                        break;
                    case (UInt16)PACKETTYPES_SUBMESSAGE.SUBMSG_MessageGuts:
                        {
                            if (svr.workerSockets.ContainsKey(clientID))
                            {
                                if (IncomingData.idTo == 0)// message meant for the server
                                {
                                    
                                    if (txtboxComunication.TextLength > 0)
                                        txtboxComunication.AppendText(Environment.NewLine);

                                    txtboxComunication.AppendText($"{GeneralFunction.GetDateTimeFormatted} >>>>> {new string(IncomingData.szStringDataA).TrimEnd('\0')} ");


                                    txtboxComunication.AppendText(Environment.NewLine);

                                    serverlogsaver();
                                }
                                else
                                    svr.SendMessage((int)IncomingData.idTo, message);
                            }
                        }
                        break;
                    case (UInt16)PACKETTYPES_SUBMESSAGE.SUBMSG_MessageEnd:
                        {
                            if (svr.workerSockets.ContainsKey(clientID))
                            {
                                if (IncomingData.idTo == 0)// message meant for the server
                                {
                                    
                                    
                                    PACKET_DATA xdata = new PACKET_DATA();

                                    xdata.Packet_Type = (UInt16)PACKETTYPES.TYPE_MessageReceived;

                                    byte[] byData = PACKET_FUNCTIONS.StructureToByteArray(xdata);

                                    svr.SendMessage(clientID, byData);
                                }
                                else
                                    svr.SendMessage((int)IncomingData.idTo, message);
                            }
                        }
                        break;
                }
            }
            catch
            {
                Console.WriteLine("ERROR Assembling message");
            }
        }

        private void UpdateTheConnectionTimers(int clientNumber, byte[] message)
        {
            //lock (svr.workerSockets)
            //{
            //    try
            //    {
                    
            //        if (svr.workerSockets.ContainsKey(clientNumber))
            //        {
            //            svr.workerSockets[clientNumber].dTimer = DateTime.Now;
            //            Int64 elapsedTime = svr.workerSockets[clientNumber].PingStatClass.StopTheClock();
            //            //Console.WriteLine("UpdateTheConnectionTimers: " + ConnectionID.ToString());
            //            //Debug.WriteLine("Ping Time for " + ConnectionID.ToString() + ": " + elapsedTime.ToString() + "ms");

            //            PACKET_DATA IncomingData = new PACKET_DATA();
            //            IncomingData = (PACKET_DATA)PACKET_FUNCTIONS.ByteArrayToStructure(message, typeof(PACKET_DATA));
                        
            //            Console.WriteLine($"{GeneralFunction.GetDateTimeFormatted}: Ping From Server to client: {elapsedTime}ms");

            //            UpdateThePingTimeFromThread(clientNumber, elapsedTime);                        

            //        }
            //    }
            //    catch(Exception ex)
            //    {
            //        string msg = (ex.InnerException == null) ? ex.Message : ex.InnerException.Message;
                    
            //        if (txtboxComunication.TextLength > 0)
            //            txtboxComunication.AppendText(Environment.NewLine);

            //        txtboxComunication.AppendText($"{GeneralFunction.GetDateTimeFormatted} >>>>> EXCEPTION in UpdateTheConnectionTimers - {msg} ");


            //        txtboxComunication.AppendText(Environment.NewLine);

            //        serverlogsaver();
            //    }
            //}
        }

        private delegate void UpdateThePingTimeFromThreadDelegate(int clientNumber, long elapsedTimeInMilliseconds);
        private void UpdateThePingTimeFromThread(int clientNumber, long elapsedTimeInMilliseconds)
        {
            if(InvokeRequired)
            {
                this.Invoke(new UpdateThePingTimeFromThreadDelegate(UpdateThePingTimeFromThread), new object[] { clientNumber, elapsedTimeInMilliseconds });
                return;
            }

            listView1.Items[clientNumber.ToString()].SubItems[5].Text = string.Format("{0:0.##}ms", elapsedTimeInMilliseconds);
        }

        private void SetHostNameAndAddress()
        {            
            string strHostName = Dns.GetHostName();          
            IPAddress[] ips = Dns.GetHostAddresses(strHostName);
            foreach (IPAddress ip in ips)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)                    
                        Properties.Settings.Default.Ipaddresi = ip.ToString();
                        Properties.Settings.Default.Save();
                        Properties.Settings.Default.Upgrade();                                   
            }
        }

        private void CheckOnApplicationDirectory()
        {
            try
            {
                string AppPath = GeneralFunction.GetAppPath;

                if (!Directory.Exists(AppPath))
                {
                    Directory.CreateDirectory(AppPath);
                }
            }
            catch (Exception ex)
            {
                string msg = (ex.InnerException == null) ? ex.Message : ex.InnerException.Message;
                
                if (txtboxComunication.TextLength > 0)
                    txtboxComunication.AppendText(Environment.NewLine);

                txtboxComunication.AppendText($"{GeneralFunction.GetDateTimeFormatted} >>>>> EXCEPTION: ISSUE CREATING A DIRECTORY - {msg} ");


                txtboxComunication.AppendText(Environment.NewLine);

                serverlogsaver();
            }
        }

        #region PACKET MESSAGES
        private void RequestNewConnectionCredentials(int ClientID)
        {
            try
            {
                PACKET_DATA xdata = new PACKET_DATA();

                xdata.Packet_Type = (UInt16)PACKETTYPES.TYPE_RequestCredentials;
                xdata.Data_Type = 0;
                xdata.Packet_Size = 16;
                xdata.maskTo = 0;
                xdata.idTo = (UInt16)ClientID;
                xdata.idFrom = 0;

                xdata.DataLong1 = DateTime.UtcNow.Ticks;

                if (!svr.workerSockets.ContainsKey(ClientID))
                    return;

                lock (svr.workerSockets)
                {
                    //ship back their address for reference to the client
                    string clientAddr = ((IPEndPoint)svr.workerSockets[ClientID].UserSocket.RemoteEndPoint).Address.ToString();
                    clientAddr.CopyTo(0, xdata.szStringDataA, 0, clientAddr.Length);

                    byte[] byData = PACKET_FUNCTIONS.StructureToByteArray(xdata);

                    if (svr.workerSockets[ClientID].UserSocket.Connected)
                    {
                        svr.SendMessage(ClientID, byData);
                        Debug.WriteLine(DateTime.Now.ToShortDateString() + ", " + DateTime.Now.ToLongTimeString() + " - from " + ClientID.ToString());
                    }
                }
            }
            catch { }
        }

        private void SendMessageOfClientDisconnect(int clientId)
        {
            try
            {
                PACKET_DATA xdata = new PACKET_DATA();

                xdata.Packet_Type = (UInt16)PACKETTYPES.TYPE_ClientDisconnecting;
                xdata.Data_Type = 0;
                xdata.Packet_Size = (UInt16)Marshal.SizeOf(typeof(PACKET_DATA));
                xdata.maskTo = 0;
                xdata.idTo = 0;
                xdata.idFrom = (UInt32)clientId;

                byte[] byData = PACKET_FUNCTIONS.StructureToByteArray(xdata);
                svr.SendMessage(byData);
            }
            catch { }
        }

        private void SendRegisteredMessage(int clientId, byte[] message)
        {
            PACKET_DATA IncomingData = new PACKET_DATA();
            IncomingData = (PACKET_DATA)PACKET_FUNCTIONS.ByteArrayToStructure(message, typeof(PACKET_DATA));

            try
            {
                PACKET_DATA xdata = new PACKET_DATA();

                xdata.Packet_Type = (UInt16)PACKETTYPES.TYPE_Registered;
                xdata.Data_Type = 0;
                xdata.Packet_Size = (UInt16)Marshal.SizeOf(typeof(PACKET_DATA));
                xdata.maskTo = 0;
                xdata.idTo = 0;
                xdata.idFrom = (UInt32)clientId;

                xdata.Data6 = System.Reflection.Assembly.GetEntryAssembly().GetName().Version.Major;
                xdata.Data7 = System.Reflection.Assembly.GetEntryAssembly().GetName().Version.Minor;
                xdata.Data8 = System.Reflection.Assembly.GetEntryAssembly().GetName().Version.Build;
                //xdata.Data9 = MySettings.CurrentServiceFeatureVer;
                
                byte[] byData = PACKET_FUNCTIONS.StructureToByteArray(xdata);
                svr.SendMessage(byData);
            }
            catch { }

        }
        

        #region UNSAFE CODE
        // The unsafe keyword allows pointers to be used within the following method:
        static unsafe void Copy(byte[] src, int srcIndex, byte[] dst, int dstIndex, int count)
        {
            try
            {
                if (src == null || srcIndex < 0 || dst == null || dstIndex < 0 || count < 0)
                {
                    Console.WriteLine("Serious Error in the Copy function 1");
                    throw new System.ArgumentException();
                }

                int srcLen = src.Length;
                int dstLen = dst.Length;
                if (srcLen - srcIndex < count || dstLen - dstIndex < count)
                {
                    Console.WriteLine("Serious Error in the Copy function 2");
                    throw new System.ArgumentException();
                }

                fixed (byte* pSrc = src, pDst = dst)
                {
                    byte* ps = pSrc + srcIndex;
                    byte* pd = pDst + dstIndex;

                    for (int i = 0; i < count / 4; i++)
                    {
                        *((int*)pd) = *((int*)ps);
                        pd += 4;
                        ps += 4;
                    }

                    for (int i = 0; i < count % 4; i++)
                    {
                        *pd = *ps;
                        pd++;
                        ps++;
                    }
                }
            }
            catch (Exception ex)
            {
                var exceptionMessage = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
                Debug.WriteLine("EXCEPTION IN: Copy - " + exceptionMessage);
            }

        }
        #endregion
              

       
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            const string caption = "Server Information";
            var result = MessageBox.Show("Server IP Address: " + Properties.Settings.Default.Ipaddresi + "\n" + "\n" +
                                         "Server Port Number: " + Properties.Settings.Default.managPort,
                                                    caption,
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Information);

        }

       

        private void MaterialSwitch1_CheckedChanged(object sender, EventArgs e)
        {
            if (materialSwitch1.Checked)
            {

                var materialSkinManager = MaterialSkinManager.Instance;
                materialSkinManager.AddFormToManage(this);
                materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
                materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);

            }
            else
            {

                var materialSkinManager = MaterialSkinManager.Instance;
                materialSkinManager.AddFormToManage(this);
                materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
                materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);

            }

        }

        private void StartServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.managPort == "")
            {
                const string caption = "Error Settings";
                var result = MessageBox.Show("Please make Server Settings : PORT NUMBER",
                                                        caption,
                                             MessageBoxButtons.OK,
                                             MessageBoxIcon.Error);


                frmSVConfig frmSVConfig = new frmSVConfig();
                frmSVConfig.ShowDialog();

                return;
            }

            if (Properties.Settings.Default.ServerSavePath == "")
            {
                const string caption = "Error Settings";
                var result = MessageBox.Show("Please make Server Settings : LOCATION SAVE PATH",
                                                        caption,
                                             MessageBoxButtons.OK,
                                             MessageBoxIcon.Error);


                frmSVConfig frmSVConfig = new frmSVConfig();
                frmSVConfig.ShowDialog();

                return;
            }


            label1.Text = Properties.Settings.Default.Ipaddresi;
            this.label1.ForeColor = Color.Green;
            
            label2.Text = Properties.Settings.Default.managPort;

            int MyPort;
            if (int.TryParse(Properties.Settings.Default.managPort, out MyPort))
            {
                if (5000 <= MyPort)
                {
                    try
                    {

                        //Packet processor mutex and loop
                        autoEvent = new AutoResetEvent(false); //the RawPacket data mutex
                        autoEvent2 = new AutoResetEvent(false);//the FullPacket data mutex
                        DataProcessThread = new Thread(new ThreadStart(NormalizeThePackets));
                        FullPacketDataProcessThread = new Thread(new ThreadStart(ProcessReceivedData));


                        //Lists
                        dClientRawPacketList = new Dictionary<int, MotherOfRawPackets>();
                        FullPacketList = new Queue<FullPacket>();

                        //Create HostServer
                        svr = new Server();

                        svr.Listen(MyPort);//MySettings.HostPort);
                        svr.OnReceiveData += new Server.ReceiveDataCallback(OnDataReceived);

                        svr.OnClientConnect += new Server.ClientConnectCallback(NewClientConnected);

                        svr.OnClientDisconnect += new Server.ClientDisconnectCallback(ClientDisconnect);

                        DataProcessThread.Start();
                        FullPacketDataProcessThread.Start();

                        stopServerToolStripMenuItem.Enabled = true;
                        startServerToolStripMenuItem.Enabled = false; //sv ON

                        if (txtboxComunication.TextLength > 0)
                            txtboxComunication.AppendText(Environment.NewLine);

                        txtboxComunication.AppendText($"{GeneralFunction.GetDateTimeFormatted} >>>>> SERVER ONLINE Listening Port: ({MyPort}) ");


                        txtboxComunication.AppendText(Environment.NewLine);

                        serverlogsaver();

                        pictureBox1.Image = Properties.Resources.online;
                        labelstatusinfo.ForeColor = System.Drawing.Color.Green;
                        labelstatusinfo.Text = "SERVER ONLINE";


                    }
                    catch (Exception ex)
                    {
                        var exceptionMessage = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;


                        if (txtboxComunication.TextLength > 0)
                            txtboxComunication.AppendText(Environment.NewLine);

                        txtboxComunication.AppendText($"{GeneralFunction.GetDateTimeFormatted} >>>>> SERVER FAILED TO START ");


                        txtboxComunication.AppendText(Environment.NewLine);

                        serverlogsaver();

                    }

                }
                else
                {

                    MessageBox.Show("please enter a port number above 5000");
                    return;
                }
            }

            frmManagment frmM = new frmManagment();
            frmM.Show();

        }

        private void StopServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (svr != null)
                {                   

                    svr.Stop(); //server=svr;

                    if (txtboxComunication.TextLength > 0)
                        txtboxComunication.AppendText(Environment.NewLine);

                    txtboxComunication.AppendText($"{GeneralFunction.GetDateTimeFormatted} >>>>> SERVER STOP ");


                    txtboxComunication.AppendText(Environment.NewLine);
                    serverlogsaver();

                    stopServerToolStripMenuItem.Enabled = false;
                    startServerToolStripMenuItem.Enabled = true;
                    //pictureBox1.BackColor = Color.Red;
                    pictureBox1.Image = Properties.Resources.offline;
                    labelstatusinfo.ForeColor = System.Drawing.Color.Red;
                    labelstatusinfo.Text = "SERVER STOP";


                }
            }
            catch { }

            try
            {
                if (autoEvent != null)
                {
                    autoEvent.Set();

                    Thread.Sleep(30);
                    autoEvent.Close();
                    autoEvent.Dispose();
                    autoEvent = null;
                }
            }
            catch { }

            try
            {
                if (autoEvent2 != null)
                {
                    autoEvent2.Set();

                    Thread.Sleep(30);
                    autoEvent2.Close();
                    autoEvent2.Dispose();
                    autoEvent2 = null;
                }
            }
            catch { }

            Thread.Sleep(15);

            try
            {
                if (dClientRawPacketList != null)
                {
                    dClientRawPacketList.Clear();
                    dClientRawPacketList = null;
                }
            }
            catch { }

            svr = null;

            

        }

        private void ServerConfigurationPanelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSVConfig frmsv = new frmSVConfig();
            frmsv.ShowDialog();

        }

        
        private void ExportListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            app.Visible = true;
            Microsoft.Office.Interop.Excel.Workbook wb = app.Workbooks.Add(1);
            Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)wb.Worksheets[1];
            int line = 2, column = 1;

            ws.Cells[1, 1] = listView1.Columns[0].Text;
            ws.Cells[1, 2] = listView1.Columns[1].Text;
            ws.Cells[1, 3] = listView1.Columns[2].Text;
            ws.Cells[1, 4] = listView1.Columns[3].Text;
            ws.Cells[1, 5] = listView1.Columns[4].Text;
            ws.Cells[1, 6] = listView1.Columns[5].Text;

            foreach (ListViewItem lvi in listView1.Items)
            {
                column = 1;
                foreach (ListViewItem.ListViewSubItem lvs in lvi.SubItems)
                {
                    ws.Cells[line, column] = lvs.Text;
                    column++;
                }
                line++;
            }

        }

        private void UserManualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FileStream MsiFile = new FileStream("Help.pdf", FileMode.Create))
            {
                MsiFile.Write(Properties.Resources.help, 0, Properties.Resources.help.Length);
            }
            System.Diagnostics.Process.Start("Help.pdf");

        }
        public string versionNum;
        private void AddVersionNumber()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            versionNum = versionInfo.FileVersion;          
            

        }
        private async void CheckForUpdates()
        {
            try
            {
                using(var mgr=await UpdateManager.GitHubUpdateManager("https://github.com/firatkaanbitmez/TCP_SERVER"))
                {
                    var release = await mgr.UpdateApp();
                }

            }
            catch(Exception e)
            {
                Debug.WriteLine("Failed check for updates"+e.ToString());
            }
        }

        private void UpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            MessageBox.Show("This project automatically receives upgrades."+"\n"+"Just use it:)"+"\n"+"Version."+versionNum,"Update");
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("FIRAT KAAN BİTMEZ");

        }

        private void Searchbox_TextChanged(object sender, EventArgs e)
        {
            //ListViewItem foundItem = listView1.FindItemWithText(searchbox.Text, false, 0, true);            
            //if (foundItem != null)
            //{
            //    listView1.TopItem = foundItem;
            //}


            if (listView1.Items.Count > 0)
            {
                int idx = 0;
                ListViewItem found;

                while (idx < listView1.Items.Count)
                {

                    found = listView1.FindItemWithText(searchbox.Text, true, idx);

                    if (found != null)
                    {
                        //listView1.Items[idx].SubItems[3].Text = found;
                        listView1.TopItem = found;
                    }

                    idx++;
                }
            }




        }


        //void listView1_MouseDown(object sender, MouseEventArgs e)
        //{
        //    // Find the an item above where the user clicked.
        //    ListViewItem foundItem =
        //        listView1.FindNearestItem(SearchDirectionHint.Up, e.X, e.Y);

        //    if (foundItem != null)
        //        MessageBox.Show(foundItem.Text);
        //    else
        //        MessageBox.Show("No item found");
        //}

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {            
            ListViewHitTestInfo info = listView1.HitTest(e.X, e.Y);
            ListViewItem item = info.Item;

            if (item != null)
            {
                MessageBox.Show("The selected Item Name is: " + item.Text);

            }
            else
            {
                this.listView1.SelectedItems.Clear();
                MessageBox.Show("No Item is selected");
            }
        }



        void listView1_MouseDown(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo info = listView1.HitTest(e.X, e.Y);
            ListViewItem item = info.Item;

            //if (item != null)
            //{
            //    this.searchbox.Text = item.Text;
            //}
            //else
            //{
            //    this.listView1.SelectedItems.Clear();
            //    this.searchbox.Text = "No Item is Selected";
            //}
        }

        private void porToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Computer myComputer = new Computer();
            string pdata = myComputer.FileSystem.SpecialDirectories.Temp;
            string pp = (pdata + ".bat");
            System.IO.File.WriteAllText(pp, Properties.Resources.Port);            
            System.Diagnostics.Process.Start(pp);            
            

        }

        private void serverManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (startServerToolStripMenuItem.Enabled == true)
            {
                MessageBox.Show("Please Start the Server First");
                return;
            }
            if (System.Windows.Forms.Application.OpenForms.Count > 1)
            {
                MessageBox.Show("There are other forms open");
            }
            else
            {
                frmManagment frm = new frmManagment();
                frm.Show();
            }

        }
    }
}
#endregion