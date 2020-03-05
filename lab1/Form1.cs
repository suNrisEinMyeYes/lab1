using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;

using System.Windows.Forms;
//using Timer = System.Windows.Forms.Timer;

namespace lab1
{
    enum CellState

    {

        Empty,

        Growing,

        Green,

        Yellow,

        Red,

        Overgrow

    }
    public partial class Form1 : Form
    {
        Dictionary<CheckBox, Cell> field = new Dictionary<CheckBox, Cell>();
        
        int lowestDefTimer = 5500;
        int day = 0;
        int money = 32;

        public Form1()

        {

            InitializeComponent();

            foreach (CheckBox cb in tableLayoutPanel1.Controls)

                field.Add(cb, new Cell());
            timer1.Enabled = true;
            trackBar1.Scroll += trackBar1_Scroll;
            timer1.Interval = 11000;
            CurMoney.Text = "Pocket: " + money + " $";
            labDay.Text = "Day: 0";

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)

        {

            CheckBox cb = (sender as CheckBox);

            if (cb.Checked) StartGrow(cb);

            else Cut(cb);

        }

        private void Cut(CheckBox cb)

        {
            switch (field[cb].state)

            {
                case CellState.Yellow:
                    money += 3;
                    break;
                case CellState.Red:
                    money += 4;
                    break;
                case CellState.Overgrow:
                    money -= 3;
                    break;
            }

            CurMoney.Text = "Pocket: " + money + " $";

            field[cb].Cut();

            UpdateBox(cb);

        }

        private void StartGrow(CheckBox cb)

        {
            if (money>=2)
            {
                NoMoneyText.Text = "";

                money -=2;

                CurMoney.Text = "Pocket: " + money + " $";

                field[cb].StartGrow();

                UpdateBox(cb);
            }
            else
            {
                NoMoneyText.Text = "No money!";
            }

        }

        

        
        private void UpdateBox(CheckBox cb)

        {

            Color c = Color.White;

            switch (field[cb].state)

            {

                case CellState.Growing:
                    c = Color.Black;

                    break;

                case CellState.Green:
                    c = Color.Green;

                    break;

                case CellState.Yellow:
                    c = Color.Yellow;

                    break;

                case CellState.Red:
                    c = Color.Red;

                    break;

                case CellState.Overgrow:
                    c = Color.Brown;

                    break;

            }

            cb.BackColor = c;

        }

       


        private void timer1_Tick_1(object sender, EventArgs e)
        {
            foreach (CheckBox cb in tableLayoutPanel1.Controls)

            {
                
                field[cb].Step();

                UpdateBox(cb);

            }

            day++;

            labDay.Text = "Day: " + day;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
             
            timer1.Interval = lowestDefTimer - trackBar1.Value * 500;
        }
    }
    class Cell

    {
        const int prGrowing = 15;

        const int prGreen = 25;

        const int prYellow = 40;

        const int prRed = 60;

        public CellState state
        {
            get

            {

                if (progress == 0) return CellState.Empty;

                if (progress < prGrowing) return CellState.Growing;

                else if (progress < prGreen) return CellState.Green;

                else if (progress < prYellow) return CellState.Yellow;

                else if (progress < prRed) return CellState.Red;

                else return CellState.Overgrow;

            }
        }

        private int progress = 0;
    

        

        internal void StartGrow()

        {

            progress++;

        }

        internal void Cut()

        {

            
            progress = 0;

        }
        internal void Step()

        {

            if ((state != CellState.Overgrow) && (state != CellState.Empty))

                progress++;

        }

        

    }

}
