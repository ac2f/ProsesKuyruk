using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProsesKuyruk
{
    public partial class Form1 : Form
    {
        /*Dictionary<string, Queue<string>> prosesler = new Dictionary<string, Queue<string>>()
        {
            { "Islemci", new Queue<string>() },
            { "Proses1", new Queue<string>() },
            { "Proses2", new Queue<string>() },
            { "Proses3", new Queue<string>() },
        };*/
        bool islemciCalisiyor = false;
        Queue<string> proses1 = new Queue<string>();
        Queue<string> proses2 = new Queue<string>();
        Queue<string> proses3 = new Queue<string>();
        Stack<string> proses1Bitenler = new Stack<string>();
        Stack<string> proses2Bitenler = new Stack<string>();
        Stack<string> proses3Bitenler = new Stack<string>();
        Dictionary<int, int> prosesHizlari = new Dictionary<int, int>() { };
        //Stack<string> prosesler = new Stack<string>();
        Random random = new Random();
        public Form1()
        {
            InitializeComponent();
            IslemciCalistir();
        }
        private void KuyrukSirala (ref Queue<string> kuyruk)
        {
            kuyruk = new Queue<string>(kuyruk.OrderBy(z => z));
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                proses1.Enqueue("P1-" + random.Next(0, 6));
                proses2.Enqueue("P2-" + random.Next(0, 6));
                proses3.Enqueue("P3-" + random.Next(0, 6));
            }
            ;
            //proses1 = new Queue<string>(proses1.OrderBy(z => z));
            KuyrukSirala(ref proses1);
            KuyrukSirala(ref proses2);
            KuyrukSirala(ref proses3);
            listBox1.Items.AddRange(proses1.ToArray());
            listBox2.Items.AddRange(proses2.ToArray());
            listBox3.Items.AddRange(proses3.ToArray());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(trackBar1.Value.ToString());
            //MessageBox.Show(proses1.Reverse().ToArray()[0]);
            lstBitirilenProsesler.Items.Clear();
            proses1Bitenler.Clear();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
        }

        private void btnBirilenProsesleriGoster_Click(object sender, EventArgs e)
        {
            lstBitirilenProsesler.Items.Clear();
            Stack<string> gosterilecekKuyruk = new Stack<string>();
            if (checkBox1.Checked)
            {
                Stack<string> tempQueue = proses1Bitenler;
                foreach (var item in tempQueue)
                {
                    gosterilecekKuyruk.Push(item);
                }
            }
            if (checkBox2.Checked)
            {
                Stack<string> tempQueue = proses2Bitenler;
                foreach (var item in tempQueue)
                {
                    gosterilecekKuyruk.Push(item);
                }
            }
            if (checkBox3.Checked)
            {
                Stack<string> tempQueue = proses3Bitenler;
                foreach (var item in tempQueue)
                {
                    gosterilecekKuyruk.Push(item);
                }
            }

            gosterilecekKuyruk = new Stack<string>(gosterilecekKuyruk.OrderBy(z => z));
            foreach (var item in gosterilecekKuyruk)
            {
                lstBitirilenProsesler.Items.Add(item);
            }
            gosterilecekKuyruk = new Stack<string>();
        }
        private string[] ArrayBolme(string[] array, int baslangic = 0, int son = 0)
        {
            string[] cikti = new string[] {};
            for (int i = baslangic; i < array.Length && i < son; i++)
            {
                cikti = cikti.Concat(new string[] { array[i] }).ToArray();
            }
            return cikti;
        }
        private async void IslemciCalistir()
        {
            while (true)
            {
                while (islemciCalisiyor)
                {
                    lblIslemcıDurumu.Text = "Çalışıyor";
                    for (int i = 0; i < trackBar2.Value && i < proses1.Count ; i++)
                    {
                        string pt = proses1.Reverse().ToArray()[i];
                        proses1Bitenler.Push(pt);
                        proses1 = new Queue<string>(ArrayBolme(proses1.ToArray(), 1, proses1.Count));
                        MessageBox.Show(proses1.Count.ToString());
                        //proses1 = proses1.Reverse().ToArray();
                    }
                    for (int i = 0; i < trackBar3.Value && i < proses2.Count; i++)
                    {
                        string pt = proses2.Reverse().ToArray()[i];
                        proses2Bitenler.Push(pt);
                    }
                    for (int i = 0; i < trackBar4.Value && i < proses3.Count; i++)
                    {
                        string pt = proses3.Reverse().ToArray()[i];
                        proses3Bitenler.Push(pt);
                    }
                    await Task.Delay(999);
                }
                lblIslemcıDurumu.Text = "Çalışmıyor";
                await Task.Delay(1);
            }
        }

        private void btnIslemciBaslat_Click(object sender, EventArgs e)
        {
            islemciCalisiyor = true;
        }

        private void btnIslemciDurdur_Click(object sender, EventArgs e)
        {
            islemciCalisiyor = false;
        }
    }
}
