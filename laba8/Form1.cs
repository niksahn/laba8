using Microsoft.VisualBasic.Logging;
using System.Drawing;
using System.Windows.Forms;

namespace laba8
{
    public partial class Form1 : Form
    {
        const int arrSize = 1000;
        int[] M;
        List<int>[] mas;
        const int maxAdres = 999;
        const int maxAdres2 = 9999;
        int compr = 0;
        const int maxNumber = 10000;
        int[] moa = new int[maxNumber];
        const int maxNumber2 = 20000;
        int[] M2 = new int[maxNumber];
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            compr = (int)numericUpDown1.Value;
            int t1 = 0;
            int t2 = 0;
            int t3 = 0;
            int t4 = 0;
            int adres = 0;
            genHeshArr();
            for (int i = 0; i < compr; i++)
            {
                int k1 = 0;
                int k2 = 0;
                int k3 = 0;
                int k4 = 0;
                generate_arr();
                clearHesh();
                for (int j = 0; j < arrSize; j++)
                {
                    adres = heshDivision(M[j]);
                    if (mas[adres].Count != 0) k1++;
                    mas[adres].Add(M[j]);
                }
                clearHesh();
                for (int j = 0; j < arrSize; j++)
                {
                    adres = heshSqureMid(M[j]);
                    if (mas[adres].Count != 0) k2++;
                    mas[adres].Add(M[j]);
                }
                clearHesh();
                for (int j = 0; j < arrSize; j++)
                {
                    adres = heshSvert(M[j]);
                    if (mas[adres].Count != 0) k3++;
                    mas[adres].Add(M[j]);
                }
                clearHesh();
                for (int j = 0; j < arrSize; j++)
                {
                    adres = heshMulti(M[j], maxAdres);
                    if (mas[adres].Count != 0) k4++;
                    mas[adres].Add(M[j]);
                }
                if (k1 <= k2 && k1 <= k3 && k1 <= k4) t1++;
                if (k2 <= k1 && k2 <= k3 && k2 <= k4) t2++;
                if (k3 <= k2 && k3 <= k1 && k3 <= k4) t3++;
                if (k4 <= k2 && k4 <= k3 && k4 <= k1) t4++;
            }
            textBox1.Text = t1.ToString();
            textBox2.Text = t2.ToString();
            textBox3.Text = t3.ToString();
            textBox4.Text = t4.ToString();
        }

        private int heshDivision(int k)
        {
            int adr = k % (997);
            return adr;
        }
        private int heshSqureMid(int k)
        {
            long adrLen = (long)Math.Log10(maxAdres + 1);
            long keySq = (long)Math.Pow(k, 2);
            if (keySq < maxAdres) { return (int)keySq; }
            else
            {
                long numLen = (long)Math.Log10(keySq);
                long cutRight = (numLen - adrLen) / 2;
                long adr = ((long)(keySq / ((long)Math.Pow(10, cutRight)))) % (long)Math.Pow(10, adrLen);
                return (int)adr;
            }
        }

        private int heshSvert(int k)
        {
            int adrLen = (int)Math.Log10(maxAdres + 1);
            int hashValue = 0;
            int input = k;
            int divider = (int)Math.Pow(10, adrLen);
            while (input > 0)
            {
                int digit = input % divider;
                hashValue += digit;
                input /= divider;
            }
            hashValue %= divider;
            return hashValue;
        }
        private int heshMulti(int k, int maxadr)
        {
            double a = (Math.Sqrt(5) - 1) / 2;
            return ((int)((maxadr + 1) * ((k * a) % 1)));
        }


        private void generate_arr()
        {
            Random rnd = new Random();
            M = new int[arrSize];
            for (int i = 0; i < arrSize; i++)
            {
                M[i] = (int)rnd.Next(0, maxNumber);

            }
        }

        private void genHeshArr()
        {
            mas = new List<int>[maxAdres + 1];
            for (int i = 0; i < maxAdres + 1; i++)
            {
                mas[i] = new List<int>();
            }
        }
        private void clearHesh()
        {
            for (int i = 0; i < maxAdres + 1; i++)
            {
                mas[i].Clear();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            int sr = 0;
            int k = 0;
            int k1 = 0;
            int sr1 = 0;
            M = new int[maxNumber];
            for (int i = 0; i < maxNumber; i++)
            {
                M[i] = (int)rnd.Next(0, maxNumber);

            }
            mas = new List<int>[maxNumber + 1];
            
            for (int i = 0; i < maxNumber; i++)
            {
                moa[i] = -1;
                mas[i] = new List<int>();
            }
            for (int i = 0; i < maxNumber; i++)
            {
                fill_moaVar(M[i], heshMulti(M[i], maxAdres2), moa);
                fill_chains(M[i], heshMulti(M[i], maxAdres2), mas);
            }
            for (int i = 0; i < maxNumber; i++)
            {
                M2[i] = (int)rnd.Next(0, maxNumber2);

            }
            var start = Environment.TickCount;
            for (int i = 0; i < maxNumber; i++)
            {
                int[] rez = find_key_moa(M2[i], heshMulti(M2[i], maxAdres2), moa);
                k += rez[1];
                sr += rez[0];
            }
            textBox5.Text = (Environment.TickCount - start).ToString();
            textBox7.Text = (k).ToString();
            textBox6.Text = (sr / maxNumber).ToString();
            start = Environment.TickCount;
            for (int i = 0; i < maxNumber; i++)
            {
                int[] rez = find_key_chains(M2[i], heshMulti(M2[i], maxAdres2), mas);
                k1 += rez[1];
                sr1 += rez[0];
            }
            textBox10.Text = (Environment.TickCount - start).ToString();
            textBox8.Text = (k1).ToString();
            textBox9.Text = ((float)(sr1)/maxNumber).ToString();
        }

        private void fill_moaVar(int key, int hesh, int[] arr)
        {
            int i = hesh;
            while (arr[i] != -1)
            {
                if (i < maxNumber - 1) i++;
                else i = 0;
                if (i == hesh) break;
            }
            arr[i] = key;
        }

        private void fill_chains(int key, int hesh, List<int>[] mas)
        {
            mas[hesh].Add(key);
        }



        private int[] find_key_moa(int key, int hesh, int[] arr)
        {
            int i = hesh;
            int sr = 0;
            int k = 0;
            while (arr[i] != key)
            {
                sr += 1;
                if (i < maxNumber - 1) i++;
                else i = 0;
                if (i == hesh) break;
            }
            if (arr[i] == key) k = 1;
            ///else sr = 0;
            return new int[] { sr, k };
        }

        private int[] find_key_chains(int key, int hesh, List<int>[] ar)
        {
            int k = 0;
            int sr = 1;
            foreach (var item in ar[hesh])
            {
                
                if (item == key)
                {
                    k = 1;
                    break;
                }
                sr += 1;
            }
            return new int[] { sr, k };
        }
    }
}