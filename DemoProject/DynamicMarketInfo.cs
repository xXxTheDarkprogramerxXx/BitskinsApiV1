using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemoProject
{
    public partial class DynamicMarketInfo : Form
    {
        public DynamicMarketInfo()
        {
            InitializeComponent();
        }


        int pointy = -294;

        private void DynamicMarketInfo_Load(object sender, EventArgs e)
        {
            try
            {
                new System.Threading.Thread(new System.Threading.ThreadStart(delegate
                {

                    BitskinsApiV1.Bitskins.GetAllItemPricesObject itemobject = BitskinsApiV1.Bitskins.Get_All_Item_Prices();
                    for (int i = 0; i < itemobject.prices.Count; i++)
                    {
                        pointy = pointy + 306;
                        ItemPannel item = new ItemPannel();
                        item.pictureBox1.Load(itemobject.prices[i].icon_url);
                        item.txtItemName.Text = itemobject.prices[i].market_hash_name;
                        item.txtItemPrice.Text = "$" + itemobject.prices[i].price;
                        item.Location = new Point(12, pointy);
                        System.Drawing.Color col = System.Drawing.ColorTranslator.FromHtml("#" + itemobject.prices[i].quality_color);
                        item.pictureBox1.BackColor = col;
                        item.panel1.BackColor = col;
                        splitContainer1.Panel1.Invoke(new Action(() => splitContainer1.Panel1.Controls.Add(item)));
                        Application.DoEvents();
                    }


                })).Start();
            }
            catch(Exception ex)
            {

            }
        }
    }
}
