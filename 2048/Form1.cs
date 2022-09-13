using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace _2048
{

    public partial class Form1 : Form
    {
        Random r = new Random();
        private int сountMove = 0;
        public int CountMove { 
            get {
                return сountMove;
            }
            set {
                if (value > -1)
                {
                    сountMove = value;
                    if (_Ground != null)
                    {
                        _Ground.SetMovedToFalseOnAllCells();
                        _Ground.InitRandomCell();
                    }
                    //label1.Text = сountMove.ToString();
                }
            }
        }
        //int CurrentScore;
        Ground _Ground;
        public Form1()
        {
            CountMove = 0;
            InitializeComponent();
            NewGame();
            this.Focus();
        }

        void NewGame()
        {
            if (_Ground != null)
            {
                _Ground.Cells.Clear();
            }
            for (int i = 0; i < Controls.Count; i++)
            {
                var item = Controls[i];
                if (item is Button)
                {
                    Controls.Remove(item);
                    item.Dispose();
                }
            }
            int Tag = 0;
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    GameButton b = new GameButton();
                    b.Size = new Size(100, 100);
                    b.Location = new Point(10 + (99 * x), 30 + (99 * y));
                    b.Tag = Tag++;
                    b.ForeColor = Color.Gray;
                    b.Font = new Font("Microsoft Tai Le", 20, FontStyle.Bold);
                    b.ForeColor = Color.Black;
                    b.TabStop = false;
                    b.posY = y;
                    b.posX = x;
                    b.MouseClick += new System.Windows.Forms.MouseEventHandler(InitTwo);
                    Controls.Add(b);                   
                }
            }
            //Label l = new Label();
            //l.Location = new Point(13, 460);
            //Controls.Add(l);
            label1.Text = "";
            _Ground = new Ground(Controls);
        }

        /// <summary>
        /// Инициализация кнопки рандомным числом для простого тестирования
        /// </summary>
        private void InitTwo(object sender, MouseEventArgs e)
        {
            if (sender is GameButton)
            {
                var b = sender as GameButton;
                var cell = _Ground.SearchOnPos(b.posX, b.posY);
                if (Control.ModifierKeys == Keys.Shift)
                {
                    cell.Value = 0;
                }
                else if (Control.ModifierKeys == Keys.Control)
                {
                    cell.Value = 8192;
                }
                else
                {
                    cell.SetRandomNum();
                }                
            }
            this.Focus();
        }

        //private void InitTwo(object sender, MouseEventArgs e)
        //{
        //    if (sender is GameButton)
        //    {
        //        if (e is System.Windows.Forms.MouseEventArgs)
        //        {
        //            var b = sender as GameButton;
        //            var cell = _Ground.SearchOnPos(b.posX, b.posY);
        //            if ((e as System.Windows.Forms.MouseEventArgs).Button == MouseButtons.Right)
        //            {
        //                cell.Value = 0;
        //            }
        //            else
        //            {
        //                cell.SetRandomNum();
        //            }
        //        }
        //    }
        //    this.Focus();
        //}

        private void новаяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewGame();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    if(_Ground.MoveUp())
                        CountMove++;
                    break;
                case Keys.D:
                    if (_Ground.MoveRigth())
                        CountMove++;
                    break;
                case Keys.S:
                    if (_Ground.MoveDown())
                        CountMove++;
                    break;
                case Keys.A:
                    if (_Ground.MoveLeft())
                        CountMove++;
                    break;

                case Keys.Up:
                    if (_Ground.MoveUp())
                        CountMove++;
                    break;
                case Keys.Right:
                    if (_Ground.MoveRigth())
                        CountMove++;
                    break;
                case Keys.Down:
                    if (_Ground.MoveDown())
                        CountMove++;
                    break;
                case Keys.Left:
                    if (_Ground.MoveLeft())
                        CountMove++;
                    break;
            }
            //_Ground.CheckGround();
        }
    }
}
