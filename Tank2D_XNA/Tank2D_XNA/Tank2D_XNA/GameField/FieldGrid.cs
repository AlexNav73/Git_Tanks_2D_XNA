using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tank2D_XNA.GameField
{
    struct APoint
    {
        public int X;
        public int Y;
        public int F;
        public int H;

        public APoint(APoint point)
        {
            X = point.X;
            Y = point.Y;
            F = point.F;
            H = point.H;
        }
    }

    class FieldGrid
    {
        private readonly int _cellSize;
        private readonly int[,] _fieldGred;
        private int fx = -1, fy = -1, lx = -1, ly = -1;

        // for A* algorithm

        private readonly List<APoint> _openList = new List<APoint>();
        private readonly List<APoint> _closeList = new List<APoint>();

        //

        public FieldGrid(int width, int height, int cellSize)
        {
            _fieldGred = new int[width, height];
            _cellSize = cellSize;
        }

        public void SetFirstPosition(int x, int y)
        {
            fx = x / _cellSize;
            fy = y / _cellSize;
        }

        public void SetLastPosition(int x, int y)
        {
            lx = x / _cellSize;
            ly = y / _cellSize;

            if (_fieldGred[lx, ly] != 0) return;

            _openList.Clear();
            _closeList.Clear();

            _closeList.Add(new APoint { H = HCost(fx, fy), F = HCost(fx, fy), X = fx, Y = fy });
            MarkCell();
        }

        public void SetWall(int x, int y)
        {
            _fieldGred[x / _cellSize, y / _cellSize] = 1;
        }

        private int GCost(int parent_x, int parent_y, int kx, int ky)
        {
            if (parent_x + kx >= 0 && parent_x + kx < _fieldGred.GetLength(0) &&
                parent_y + ky >= 0 && parent_y + ky < _fieldGred.GetLength(1) &&
                _fieldGred[parent_x + kx, parent_y + ky] == 0)
            {
                if ((parent_x == parent_x + kx && parent_y != parent_y + ky) ||
                    (parent_x != parent_x + kx && parent_y == parent_y + ky))
                    return _cellSize;
                return (int)(_cellSize * 1.4142135623730950f);
            }

            return -1;
        }

        private int HCost(int parent_x, int parent_y)
        {
            int length = 0, offs_x = 0, offs_y = 0;

            if (Math.Abs(lx - parent_x) != 0)
                offs_x = (lx - parent_x) / Math.Abs(lx - parent_x);
            if (Math.Abs(ly - parent_y) != 0)
                offs_y = (ly - parent_y) / Math.Abs(ly - parent_y);

            while (parent_x != lx && parent_y != ly)
            {
                parent_x += offs_x;
                parent_y += offs_y;
                length += (int)(_cellSize * 1.4142135623730950f);
            }

            while (parent_x != lx || parent_y != ly)
            {
                if (parent_x != lx) parent_x += offs_x;
                else
                    if (parent_y != ly) parent_y += offs_y;
                length += _cellSize;
            }

            return length;
        }

        private void SetCell(int parent_x, int parent_y, int kx, int ky)
        {
            int g = GCost(parent_x, parent_y, kx, ky);

            if (g != -1)
            {
                int h = HCost(parent_x + kx, parent_y + ky);

                if (_openList.Exists((point) => point.X == parent_x + kx && point.Y == parent_y + ky))
                {
                    APoint tmp = _openList.Find((point) => point.X == parent_x + kx && point.Y == parent_y + ky);
                    if (tmp.F > g + h) tmp.F = g + h;
                }
                else
                    if (!_closeList.Exists((point) => point.X == parent_x + kx && point.Y == parent_y + ky))
                    {
                        int parent_g_cost = 0;
                        if (_closeList.Exists((point) => point.X == parent_x && point.Y == parent_y))
                        {
                            APoint tmp_node = _closeList.First((point) => point.X == parent_x && point.Y == parent_y);
                            parent_g_cost = tmp_node.F - tmp_node.H;
                        }
                        var tmp = new APoint { F = parent_g_cost + g + h, H = h, X = parent_x + kx, Y = parent_y + ky };
                        _openList.Add(tmp);
                    }
            }
        }

        private bool FindNextCell(ref int x, ref int y)
        {
            if (_openList.Count == 0) return false;
            x = _openList[0].X;
            y = _openList[0].Y;
            int min_F = _openList[0].F, min_H = _openList[0].H;

            for (int i = 0; i < _openList.Count; i++)
                if (_openList[i].F < min_F && _openList[i].H <= min_H)
                {
                    x = _openList[i].X;
                    y = _openList[i].Y;
                    min_F = _openList[i].F;
                    min_H = _openList[i].H;
                }
            return true;
        }

        private void MarkCell()
        {
            while (fx != lx || fy != ly)
            {
                SetCell(fx, fy, 0, -1);
                SetCell(fx, fy, 1, -1);
                SetCell(fx, fy, 1, 0);
                SetCell(fx, fy, 1, 1);
                SetCell(fx, fy, 0, 1);
                SetCell(fx, fy, -1, 1);
                SetCell(fx, fy, -1, 0);
                SetCell(fx, fy, -1, -1);

                int x = 0, y = 0;
                if (!FindNextCell(ref x, ref y)) break;
                if (x == -1 || y == -1) break;
                APoint tmp = _openList.Find(point => point.X == x && point.Y == y);
                APoint to_close_list = new APoint(tmp);
                _openList.Remove(tmp);
                _closeList.Add(to_close_list);
                fx = x;
                fy = y;
            }
        }

    }
}
