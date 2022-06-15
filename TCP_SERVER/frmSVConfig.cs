using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCP_SERVER
{
    public partial class frmSVConfig : MaterialForm
    {
        public frmSVConfig()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);

            if (skinfrm == "100")
            {

                materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;


            }
            if (skinfrm == "50")
            {

                materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;

            }
        }
        public string skinfrm;
        
        private void frmSVConfig_Load(object sender, EventArgs e)
        {
            

            SetHostNameAndAddress();
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Upgrade();
            textBox2.Text = Properties.Settings.Default.managPort;
            txtPath.Text = Properties.Settings.Default.ServerSavePath;

            if (Properties.Settings.Default.ServerConnect =="serverconnectconfig")
            {
                checkBoxservercon.Checked = true;
            }
            else
            {
                checkBoxservercon.Checked = false;
            }
            if(Properties.Settings.Default.listviewremover == "listsaveremove")
            {
                checkboxlvlogremove.Checked = true;
            }
            else
            {
                checkboxlvlogremove.Checked = false;
            }



        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
            
        }

        private void SetHostNameAndAddress()
        {
            //int MyPort = Convert.ToInt32(this.textBox1.Text);
            string strHostName = Dns.GetHostName();



            IPAddress[] ips = Dns.GetHostAddresses(strHostName);

            foreach (IPAddress ip in ips)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    textBox1.Text = ip.ToString();  
                Properties.Settings.Default.Ipaddresi = ip.ToString();
                Properties.Settings.Default.Save();
                Properties.Settings.Default.Upgrade();


            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (checkBoxservercon.Checked)
            {
                Properties.Settings.Default.ServerConnect = "serverconnectconfig";
                
                Properties.Settings.Default.Save();
                Properties.Settings.Default.Upgrade();
                
            }
            if (checkBoxservercon.Checked == false)
            {
                Properties.Settings.Default.ServerConnect = "sAAAA";

                Properties.Settings.Default.Save();
                Properties.Settings.Default.Upgrade();

            }

            if (checkboxlvlogremove.Checked == true)
            {
                Properties.Settings.Default.listviewremover = "listsaveremove";

                Properties.Settings.Default.Save();
                Properties.Settings.Default.Upgrade();


            }


            Properties.Settings.Default.ServerSavePath = txtPath.Text;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Upgrade();
            txtPath.Text = Properties.Settings.Default.ServerSavePath;

            Properties.Settings.Default.managPort = textBox2.Text;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Upgrade();
            textBox2.Text = Properties.Settings.Default.managPort;
            this.Close();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog() { Description = "Select your path." })
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                    txtPath.Text = fbd.SelectedPath;
            }

        }

        private void checkboxlvlogremove_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
