using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static System.Windows.Forms.Control;

namespace _2048
{
    enum Direction
    {
        Up,
        Rigth,
        Down,
        Left
    }

    class Ground
    {
        Random r = new Random();
        public List<Cell> Cells = new List<Cell>(16);
        public Cell GetRandomEmpty()
        {
            var temp = new List<Cell>();
            foreach (var c in Cells)
            {
                if (c.Value == 0)
                {
                    temp.Add(c);
                }
            }
            if (temp.Count == 0)
            {
                throw new Exception("Пустых ячеек не осталось!");
            }
            return temp[r.Next(0, temp.Count)];
        }

        public Ground(ControlCollection controls)
        {
            for (int i = 0; i < controls.Count; i++)
            {
                int currTag = -1;
                try
                {
                    currTag = (int)controls[i].Tag;
                }
                catch { }

                if (controls[i] is Button && (currTag > -1 && currTag < 17))
                {
                    var x = currTag % 4;
                    var y = currTag / 4;
                    Cells.Add(new Cell(y, x, controls[i] as Button, Cells, 0));
                }
            }
            InitRandomCell();
        }

        public void InitRandomCell()
        {
            var t = GetRandomEmpty();
            if (t != null)
                t.SetRandomNum();
        }

        public bool MoveUp()
        {
            int res = 0;
            for (int i = 0; i < 4; i++)
            {
                //if (ColGroundIsEmpty(i))
                //    continue;

                for (int j = 0; j < 4; j++)
                {
                    var currCell = SearchOnPos(i, j);
                    if (currCell.Value != 0)
                    {
                        var nextCell = CheckDir(Direction.Up, currCell);
                        if ((nextCell + currCell) != Consts.TypeChange.None)
                            res++;
                    }
                }
            }
            return res > 0 ? true : false;
        }

        public bool MoveRigth()
        {
            int res = 0;
            for (int i = 0; i < 4; i++)
            {
                //if (StrGroundIsEmpty(i))
                //    continue;

                for (int j = 3; j >= 0; j--)
                {
                    var currCell = SearchOnPos(j, i);
                    if (currCell.Value != 0)
                    {
                        //res += CheckDir(Direction.Rigth, currCell);
                        var nextCell = CheckDir(Direction.Rigth, currCell);
                        if ((nextCell + currCell) != Consts.TypeChange.None)
                            res++;
                    }
                }
            }
            return res > 0 ? true : false;
        }

        public bool MoveLeft()
        {
            int res = 0;
            for (int i = 0; i < 4; i++)
            {
                //if (StrGroundIsEmpty(i))
                //    continue;

                for (int j = 0; j < 4; j++)
                {
                    var currCell = SearchOnPos(j, i);
                    if (currCell.Value != 0)
                    {
                        var nextCell = CheckDir(Direction.Left, currCell);
                        if ((nextCell + currCell) != Consts.TypeChange.None)
                            res++;
                    }
                }
            }
            return res > 0 ? true : false;
        }

        public bool MoveDown()
        {
            int res = 0;
            for (int i = 3; i >= 0; i--)
            {
                //if (ColGroundIsEmpty(i))
                //    continue;

                for (int j = 0; j < 4; j++)
                {
                    var currCell = SearchOnPos(j, i);
                    if (currCell.Value != 0)
                    {
                        var nextCell = CheckDir(Direction.Down, currCell);
                        if ((nextCell + currCell) != Consts.TypeChange.None)
                            res++;
                    }
                }
            }
            return res > 0 ? true : false;
        }

        public Cell CheckDir(Direction dir, Cell c)
        {
            Cell nextCell = c;
            switch (dir)
            {
                case Direction.Up:
                    for (int y = c.y - 1; y >= 0; y--)
                    {
                        var nc = SearchOnPos(c.x, y);
                        if (nc.Value == c.Value)
                        {
                            nextCell = nc;
                            return nextCell;
                        }
                        if (nc.Value != c.Value && nc.Value != 0)
                        {
                            break;
                        }
                        if (nc.Value == 0)
                        {
                            nextCell = nc;
                        }
                    }
                    break;
                case Direction.Rigth:
                    for (int x = c.x + 1; x < 4; x++)
                    {
                        var nc = SearchOnPos(x, c.y);
                        if (nc.Value == c.Value)
                        {
                            nextCell = nc;
                            return nextCell;
                        }
                        if (nc.Value != c.Value && nc.Value != 0)
                        {
                            break;
                        }
                        if (nc.Value == 0)
                        {
                            nextCell = nc;
                        }
                    }
                    break;
                case Direction.Down:
                    for (int y = c.y + 1; y < 4; y++)
                    {
                        var nc = SearchOnPos(c.x, y);
                        if (nc.Value == c.Value)
                        {
                            nextCell = nc;
                            return nextCell;
                        }
                        if (nc.Value != c.Value && nc.Value != 0)
                        {
                            break;
                        }
                        if (nc.Value == 0)
                        {
                            nextCell = nc;
                        }
                    }
                    break;
                case Direction.Left:
                    for (int x = c.x - 1; x >= 0; x--)
                    {
                        var nc = SearchOnPos(x, c.y);
                        if (nc.Value == c.Value)
                        {
                            nextCell = nc;
                            return nextCell;
                        }
                        if (nc.Value != c.Value && nc.Value != 0)
                        {
                            break;
                        }
                        if (nc.Value == 0)
                        {
                            nextCell = nc;
                        }
                    }
                    break;
            }
            return nextCell;
        }

        /// <summary>
        /// Возвращает ячейку по её координатам
        /// </summary>
        /// <param name="x">координата по ширине</param>
        /// <param name="y">координата по высоте</param>
        /// <returns></returns>
        public Cell SearchOnPos(int x, int y)
        {
            return Cells[y * 4 + x];
        }

        public bool StrGroundIsEmpty(int z)
        {
            for (int i = 0; i < 4; i++)
            {
                if (SearchOnPos(i, z).Value != 0)
                {
                    return false;
                }
            }
            return true;
        }

        public bool ColGroundIsEmpty(int z)
        {
            for (int i = 0; i < 4; i++)
            {
                if (SearchOnPos(z, i).Value != 0)
                {
                    return false;
                }
            }
            return true;
        }

        public void SetMovedToFalseOnAllCells()
        {
            foreach (var c in Cells)
            {
                c.SetMovedFalse();
            }
        }

        //public bool CheckGround()
        //{
        //    var t = GetRandomEmpty();
        //    if (t == null)

        //}
    }
}
