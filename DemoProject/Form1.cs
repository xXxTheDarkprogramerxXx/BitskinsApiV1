using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace DemoProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Bitskins_Api_Key = txtApiKey.Text.ToString().Trim();

            Properties.Settings.Default.Bitskins_Secret = txtSecret.Text.ToString().Trim();

            Properties.Settings.Default.Save();

            LoadSettings();

        }

        public void LoadSettings()
        {
            //settings required for the api to function
            BitskinsApiV1.Bitskins.Api_Key = Properties.Settings.Default.Bitskins_Api_Key;
            txtApiKey.Text = Properties.Settings.Default.Bitskins_Api_Key;
            BitskinsApiV1.Bitskins.SECRET_FROM_BITSKINS = Properties.Settings.Default.Bitskins_Secret;
            txtSecret.Text = Properties.Settings.Default.Bitskins_Secret;
            //we need to set the appid once for the api to function
            BitskinsApiV1.Bitskins.AppID = BitskinsApiV1.Bitskins.AppID_Enum.CSGO;

            //load 2fa
            BitskinsApiV1.Bitskins.FA();

            //Assign text
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer = new System.Timers.Timer(TimeSpan.FromSeconds(30).TotalMilliseconds); //refresh every 30 seconds
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Start();
            txt2FA.Text = BitskinsApiV1.Bitskins.FACode;


        }

        public void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            //just get the new f2a code
            txt2FA.Invoke(new Action(() => txt2FA.Text = BitskinsApiV1.Bitskins.FACode));
            Application.DoEvents();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.Bitskins_Secret != string.Empty)
            {
                LoadSettings();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
           BitskinsApiV1.Bitskins.WalletObject wallet =
                BitskinsApiV1.Bitskins.Get_Account_Balance();

            lblAccountBalance.Text = "$" + wallet.data.available_balance;
        }

        private void btnGetAllItemPrices_Click(object sender, EventArgs e)
        {
            DynamicMarketInfo dynamic = new DynamicMarketInfo();
            dynamic.Show();
        }

        private void spinner_Click(object sender, EventArgs e)
        {

        }
    }
}
