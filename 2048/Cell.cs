using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static _2048.Consts;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;

namespace _2048
{
    class Cell
    {
        static Random r = new Random();

        private static int Count;

        private int id;
        public int Id 
        {
            get { return id; } 
            private set { id = value; } 
        }
        public int y;
        public int x;
        private int _value;
        public int Value
        {
            get { return _value; }
            set
            {
                _value = value;
                Change?.Invoke(_value);
            }
        }
        public Button button { get; private set; }
        private bool isSummedInLastStep = false;
        public List<Cell> Cells; 

        delegate void ButtonValHandler(int c);
        event ButtonValHandler Change;

        public Cell(int y, int x, Button b, List<Cell> cells, int v = 0)
        {
            this.y = y;
            this.x = x;
            this.Value = v;
            this.button = b;
            Change += UpdateBtn;
            this.Id = Count;
            this.Cells = cells;
        }

        public Cell(Cell c, int v = 0)
        {
            this.y = c.y;
            this.x = c.x;
            this.Value = v;
            this.button = c.button;
            Change = UpdateBtn;
        }

        public int SetRandomNum()
        {
            int rNum = 2;
            if (r.Next(0, 100) > 80)
                rNum = 4;
            if (r.Next(0, 100) > 97)
                rNum = 8;
            this.Value = rNum;
            return rNum;
        }

        private void UpdateBtn(int val)
        {
            if (val > 0)
            {
                button.Text = val.ToString(); // + "\n{" + button.Tag + "}"
                button.BackColor = Consts.Colors[val];
            }
            else
            {
                button.Text = "";
                button.BackColor = Consts.Colors[0];
            }
        }

        internal void SetMovedFalse()
        {
            isSummedInLastStep = false;
        }

        public static TypeChange operator +(Cell a, Cell b)
        {
            if (a.Equals(b))            
                return TypeChange.None;
            
            if ((a.y == b.y || a.x == b.x))
            {
                if (a.Value == 0)
                {
                    //Task t = Task.Run(() =>  b.GUIMove(a));
                    a.Value += b.Value;
                    b.Value = 0;
                    return TypeChange.Move;
                }
                if (a.Value == b.Value && a.isSummedInLastStep != true)
                {
                    //Task t = Task.Run(() =>  b.GUIMove(a));
                    a.isSummedInLastStep = true;
                    a.Value += b.Value;
                    b.Value = 0;
                    return TypeChange.Sum;
                }
            }
            return TypeChange.None;
        }

        private void GUIMove(Cell targetCell)
        {
            var startLoc = this.button.Location;
            var nextStep = new Point(0, 0);
            if (targetCell.y == this.y)
            {
                var step = (targetCell.button.Location.X - startLoc.X) / 100;
                nextStep.Y = this.button.Location.Y;
                for (int i = 0; i < 100; i++)
                {
                    nextStep.X += step;
                    this.button.Location = nextStep;
                    Thread.Sleep(50);
                    //Task.WaitAll();
                }
            }
            else
            {

            }

        } 

        ~Cell()
        {
            Count--;
        }
    }
}
