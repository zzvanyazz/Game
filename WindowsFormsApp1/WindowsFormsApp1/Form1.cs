using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        int amountHomes = 10;

        home[] Homes;
        public Form1()
        {
            Homes = new home[amountHomes];
            InitializeComponent();
            MinimumSize = new Size(800, 800);
            MaximumSize = new Size(800, 800);
            g = this.CreateGraphics();
        }
        
        Random a = new Random();

        private void label1_Click_1(object sender, EventArgs e)
        {
            for (int i = 0; i < amountHomes; i++)
            {
                home h = new home(a);
                Homes[i] = h;
                if (i == 1)
                    h.setParams(true, false, 2000);
                else if (i == 9)
                    h.setParams(true, true, 2000);
                else
                    h.setParams(false, true, 2000);
                Controls.Add(h);
                Thread th = new Thread(h.Speed);
                th.Start();
            }
            DrawWaysToRandomV();
        }




        void DrawWaysToRandom()
        {
            for (int i = 0; i < amountHomes; i++)
            {
                for (int j= 0; j < 1; j++)
                {
                    int r = a.Next(10);
                    g.DrawLine(new Pen(Color.Green, 2.0f), Homes[i].Location.X + 25, Homes[i].Location.Y + 25,
                        Homes[r].Location.X + 25, Homes[r].Location.Y + 25);
                }
                
            }
        }

        void DrawWaysToRandomV()
        {
            for (int i = 0; i < amountHomes; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    
                    g.DrawLine(new Pen(Color.Green, 2.0f), Homes[i].Location.X + 25, Homes[i].Location.Y + 25,
                        Homes[a.Next(10)].Location.X + 25, Homes[a.Next(10)].Location.Y + 25);
                }

            }
        }


        void DrawWaysWithAll()
        {
            for (int i = 0; i < amountHomes; i++)
            {
                for (int j = 0; j < amountHomes; j++)
                {
                    g.DrawLine(new Pen(Color.Green, 2.0f), Homes[i].Location.X + 25, Homes[i].Location.Y + 25,
                        Homes[j].Location.X + 25, Homes[j].Location.Y + 25);
                }
            }
        }
        Graphics g;


       void DrawWaysToNearest()
        {
            
            for (int i = 0; i < amountHomes; i++)
            {
                for (int j = i; j < amountHomes; j++)
                    g.DrawLine(new Pen(Color.Green, 2.0f), Homes[i].Location.X+25, Homes[i].Location.Y + 25, 
                        findClose(i, 1)[0] + 25, findClose(i, 1)[1] + 25);
            }
        }

        int[] findClose(int NumberHome, int amount)
        {
            int[] MaxNumbers = new int[amountHomes];
            int[] distanse   = new int[amountHomes];
           /* for(int i = 0; i < amountHomes; i++){
                distanse[i]   = (int)Math.Sqrt(Math.Pow(Homes[NumberHome].Location.X - Homes[i].Location.X, 2) -
                    Math.Pow(Homes[NumberHome].Location.Y - Homes[i].Location.Y, 2));
                MaxNumbers[i] = (int)Math.Sqrt(Math.Pow(Homes[NumberHome].Location.X - Homes[i].Location.X, 2) -
                    Math.Pow(Homes[NumberHome].Location.Y - Homes[i].Location.Y, 2));
            }*/
            int min = 0;
            int x =0, y =0;
            for (int i = 0; i < amountHomes; i++)
            {
                if (i != NumberHome)
                {
                    if ((int)Math.Sqrt(Math.Pow(Homes[NumberHome].Location.X - Homes[i].Location.X, 2) -
                        Math.Pow(Homes[NumberHome].Location.Y - Homes[i].Location.Y, 2)) > min)
                    {
                        min = (int)Math.Sqrt(Math.Pow(Homes[NumberHome].Location.X - Homes[i].Location.X, 2) -
                        Math.Pow(Homes[NumberHome].Location.Y - Homes[i].Location.X, 2));
                        x = Homes[i].Location.X;
                        y = Homes[i].Location.Y;
                    }
                }
           
            }

           /* for (int i = 10; i > 0; i++)
            {
                for (int j = 0; j < i; j++)
                {

                }
            }*/
            int[] d = new int[2];
            d[0] = x;
            d[1] = y;

            return d;
       }
        

        public delegate void ArmyAdd();

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            
        }
    }
    class home : Label
    {
        bool Active = false;
        bool Enemy = false;
        public int Army = 0;
        int speed;
       
        public void setParams(bool Active, bool Enemy, int speed)
        {
            this.speed = speed;
            this.Active = Active;
            this.Enemy = Enemy;
            if (!Active) BackColor = Color.Gray;
            else if (Active && Enemy) BackColor = Color.Red;
            else if (Active && !Enemy) BackColor = Color.Blue;

            
        }
        public delegate void ArmyAdd();
        public void AddArmy()
        {
            Army++;
            Text = Army + "";
        }
        public void Speed()
        {
            if (Active)
            {
                
                this.BeginInvoke(new ArmyAdd(this.AddArmy));

                Thread.Sleep(speed);
                Speed();
            }
        }
        public home(Random r)
        {
            Size = new Size(50, 50);
            Location = new Point(r.Next(700), r.Next(700));
            
        }
    }
}
