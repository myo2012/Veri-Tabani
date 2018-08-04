using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        OleDbConnection conn = new OleDbConnection("provider=Microsoft.Jet.Oledb.4.0;data source=deneme.mdb");
        OleDbDataAdapter da = new OleDbDataAdapter();
        DataSet ds = new DataSet();
        private void btngoruntule_Click(object sender, EventArgs e)
        {
            da.SelectCommand = new OleDbCommand("Select * From bilgi",conn);
            ds.Clear();
            da.Fill(ds);
            dg.DataSource = ds.Tables[0];

        }

        private void btnara_Click(object sender, EventArgs e)
        {
            da.SelectCommand = new OleDbCommand("select * From bilgi where ad=@ad",conn);
            da.SelectCommand.Parameters.Add("@ad",OleDbType.Char).Value=txtara.Text;
            conn.Open();

            ds.Clear();
            //da.SelectCommand.ExecuteNonQuery(); DataGridWiew kullandığımızda yani dateset bu parametreye gerek yok .Faruk'dan notlar..
            da.Fill(ds);
            dg.DataSource=ds.Tables[0];
           
            conn.Close();
        }

        private void btnekle_Click(object sender, EventArgs e)
        {

            string hobi = "";
            if (checkBox1.Checked)
                hobi += "A";
            if (checkBox2.Checked)
                hobi += "B";
            if (checkBox3.Checked)
                hobi += "C";
            if (checkBox4.Checked)
                hobi += "D";

            string list = "";
            list = listBox1.SelectedItem.ToString();


            int kayittarihi;
            kayittarihi = DateTime.Today.Year;
    
                
            String saat = "";
            saat = DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;

            String cinsiyet = "";
            if (rbbay.Checked)
                cinsiyet = "Erkek";
            else if (rbbayan.Checked)
                cinsiyet = "Kadın";
            //else  istersen fazladan ekle 
            //    MessageBox.Show("cinsiyet yok");

            da.InsertCommand = new OleDbCommand("INSERT INTO bilgi VALUES(@ad,@soyad,@bolum,@dogumyeri,@dtarih,@cins,@ksaat,@ktarih,@listbox,@hobi)",conn);
            da.InsertCommand.Parameters.Add("@ad",OleDbType.Char).Value=txtadekle.Text;
            da.InsertCommand.Parameters.Add("@soyad", OleDbType.Char).Value = txtsoyadekle.Text;
            da.InsertCommand.Parameters.Add("@bolum", OleDbType.Char).Value = txtbolum.Text;
            da.InsertCommand.Parameters.Add("@dogumyeri", OleDbType.Char).Value = txtdogum.Text;
            da.InsertCommand.Parameters.Add("@dtarih", OleDbType.Char).Value = dateTimePicker1.Value.Year;
            da.InsertCommand.Parameters.Add("@cins", OleDbType.Char).Value = cinsiyet;
            da.InsertCommand.Parameters.Add("@ksaat", OleDbType.Char).Value = saat;
            da.InsertCommand.Parameters.Add("@ktarih", OleDbType.Integer).Value = kayittarihi;
            da.InsertCommand.Parameters.Add("@listbox", OleDbType.Char).Value = list;
            da.InsertCommand.Parameters.Add("@hobi", OleDbType.Char).Value = hobi;
            conn.Open();

            da.InsertCommand.ExecuteNonQuery();

            conn.Close();
            MessageBox.Show("EKLENDİ");

        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            da.DeleteCommand = new OleDbCommand("DELETE * FROM bilgi where (ad=@ad and soyad=@soyad)",conn);
            da.DeleteCommand.Parameters.Add("@ad", OleDbType.Char).Value = txtadsil.Text;
            da.DeleteCommand.Parameters.Add("@soyad", OleDbType.Char).Value = txtsoyadsil.Text;
            conn.Open();

            da.DeleteCommand.ExecuteNonQuery();
            

            conn.Close();
            MessageBox.Show("silindi");

     

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OleDbCommand cmd = new OleDbCommand("SELECT soyad,bolum,dogumyeri FROM bilgi WHERE ad=@ad",conn);
            cmd.Parameters.Add("@ad", OleDbType.Char).Value = textBox1.Text;
            conn.Open();

            OleDbDataReader dr=cmd.ExecuteReader();

            if (dr.Read())
            {

                textBox2.Text = dr["soyad"].ToString();
                textBox3.Text = dr["bolum"].ToString();
                textBox4.Text = dr["dogumyeri"].ToString();


            }
            else
            {
                MessageBox.Show("Girdiğiniz kayıt bulunamadığı için işlem gerçekleştirilemiyor");
            }
            conn.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Today.AddYears(-20);
        }

        
        

    }
}
