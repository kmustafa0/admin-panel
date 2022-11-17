using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace admin_panel
{
    public partial class adminPanel : Form
    {
        public adminPanel()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection("Data Source=MST\\SQLEXPRESS;Initial Catalog=admin-panel;Integrated Security=True");

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
        void musteriGetir()
        {
            conn = new SqlConnection("Data Source=MST\\SQLEXPRESS; Initial Catalog=admin-panel; Integrated Security=true");
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM uyeler", conn);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
            conn.Close();
        }
        void textBoxTemizle()
        {
            Action<Control.ControlCollection> func = null;

            func = (controls) =>
            {
                foreach (Control control in controls)
                {
                    if (control is TextBox)
                    {
                        (control as TextBox).Clear();
                    }
                    else
                    {
                        func(control.Controls);
                    }
                }
            };
            func(Controls);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM uyeler",conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button3_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand uyeSİl = new SqlCommand("DELETE FROM uyeler WHERE uyeID=@uyeıd",conn);
            uyeSİl.Parameters.AddWithValue("uyeID",textBox7.Text);
            uyeSİl.ExecuteNonQuery();
            textBoxTemizle();
            musteriGetir();
            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand uyeEkle = new SqlCommand("INSERT INTO uyeler (uyeAd, uyeSoyad, uyeTelefon, uyeTCKN, uyeBitisTarih) VALUES (@p1, @p2, @p3, @p4, @p5)",conn);
            uyeEkle.Parameters.AddWithValue("@p1",textBox1.Text);
            uyeEkle.Parameters.AddWithValue("@p2",textBox2.Text);
            uyeEkle.Parameters.AddWithValue("@p3",textBox3.Text);
            uyeEkle.Parameters.AddWithValue("@p4",textBox4.Text);
            uyeEkle.Parameters.AddWithValue("@p5", textBox5.Text);
            uyeEkle.ExecuteNonQuery();
            musteriGetir();
            textBoxTemizle();
            conn.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand uyeGuncelle = new SqlCommand("UPDATE uyeler SET uyeAd=@p1, uyeSoyad=@p2, uyeTelefon=@p3, uyeTCKN=@p5, uyeBitisTarih=@p6 WHERE uyeID=@p4", conn);
            uyeGuncelle.Parameters.AddWithValue("@p1",textBox1.Text);
            uyeGuncelle.Parameters.AddWithValue("@p2",textBox2.Text);
            uyeGuncelle.Parameters.AddWithValue("@p3",textBox3.Text);
            uyeGuncelle.Parameters.AddWithValue("@p4",textBox7.Text);
            uyeGuncelle.Parameters.AddWithValue("@p5",textBox4.Text);
            uyeGuncelle.Parameters.AddWithValue("@p6",textBox5.Text);
            uyeGuncelle.ExecuteNonQuery();
            musteriGetir();
            conn.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            string ad = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            string soyad = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            string telefon = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            string tckn = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
            string bitisTarih = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            string uyeID = dataGridView1.Rows[secilen].Cells[0].Value.ToString();

            textBox1.Text = ad;
            textBox2.Text = soyad;
            textBox3.Text = telefon;
            textBox4.Text = tckn;
            textBox5.Text = bitisTarih;
            textBox7.Text = uyeID;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBoxTemizle();
            musteriGetir();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * from uyeler where uyeAd='" + textBox6.Text + "'", conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            textBoxTemizle();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label6.Text = DateTime.Now.ToLongDateString();
            label9.Text = DateTime.Now.ToLongTimeString();
        }

        private void adminPanel_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
            timer1.Stop();
        }

    }
}
