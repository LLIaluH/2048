using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace _2048
{
    public static class Consts
    {
        public enum TypeChange
        {
            None,
            Move,
            Sum
        } 

        static public Dictionary<int, Color> Colors = new Dictionary<int, Color>();
        static Consts()
        {
            Colors.Add(0, Color.Gainsboro);
            Colors.Add(2, Color.Red);
            Colors.Add(4, Color.Yellow);
            Colors.Add(8, Color.Green);
            Colors.Add(16, Color.Blue);
            Colors.Add(32, Color.Aqua);
            Colors.Add(64, Color.Brown);
            Colors.Add(128, Color.Purple);
            Colors.Add(256, Color.Coral);
            Colors.Add(512, Color.DarkBlue);
            Colors.Add(1024, Color.DimGray);
            Colors.Add(2048, Color.DarkGreen);
            Colors.Add(4096, Color.Lime);
            Colors.Add(8192, Color.AntiqueWhite);
        }

    }
}
