using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _2048
{
    class GameButton : Button
    {
        public int posY { get; set; }
        public int posX { get; set; }

        public GameButton()
        {
            this.SetStyle(ControlStyles.Selectable, false);
        }

        //public async void GUIMove(Point targetLocation)
        //{
        //    var startLocation = this.Location;
        //    for (int i = 0; i < 100; i++)
        //    {
        //        await Task.Delay();
        //    }
        //}
    }
}
