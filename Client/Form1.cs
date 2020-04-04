using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Client.ServiceClientProduct;
using Data;

namespace Client
{
    public partial class Form1 : Form
    {
        int secid;
        Urunler Securun;
        ServiceProductClient service = new ServiceProductClient();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {          
            dataGridView1.DataSource = service.GetProductsWin();
        }            

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            secid = (int)dataGridView1.CurrentRow.Cells[0].Value;
            Securun = service.FindProducts(secid);
            txt_id.Text = Securun.UrunId.ToString();
            txt_ad.Text = Securun.UrunAd;
            txt_bf.Text = Securun.BirimFiyat.ToString();
            check_satilamaz.Checked = Securun.Satılamaz;
        }
        private void btn_sikl_Click(object sender, EventArgs e)
        {
            service.DeleteProducts(secid);
            button1_Click(null,null);
        }

        private void btn_guncel_Click(object sender, EventArgs e)
        {
            service.UpdatePriceANDName(secid, Convert.ToDecimal(txt_bf.Text), txt_ad.Text);
            button1_Click(null, null);
        }
    }
}
