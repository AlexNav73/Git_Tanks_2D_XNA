using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tank2D_XNA.GameField
{
    class FieldGrid
    {
        private struct APoint
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

        private readonly int _cellSize;
        private readonly int[,] _fieldGred;
        private int _fx = -1, _fy = -1, _lx = -1, _ly = -1;
        private readonly List<APoint> _openList = new List<APoint>();
        private readonly List<APoint> _closeList = new List<APoint>();

        public FieldGrid(int width, int height, int cellSize)
        {
            _fieldGred = new int[width, height];
            _cellSize = cellSize;
        }

        public void SetStartPosition(int x, int y)
        {
            _fx = x / _cellSize;
            _fy = y / _cellSize;
        }

        public void SetFinishPosition(int x, int y)
        {
            _lx = x / _cellSize;
            _ly = y / _cellSize;

            if (_fieldGred[_lx, _ly] != 0) return;

            _openList.Clear();
            _closeList.Clear();

            _closeList.Add(new APoint { H = HCost(_fx, _fy), F = HCost(_fx, _fy), X = _fx, Y = _fy });
            FindPath();
        }

        public void PlaceWall(int x, int y)
        {
            _fieldGred[x / _cellSize, y / _cellSize] = 1;
        }

        private int GCost(int parentX, int parentY, int kx, int ky)
        {
            if (parentX + kx >= 0 && parentX + kx < _fieldGred.GetLength(0) &&
                parentY + ky >= 0 && parentY + ky < _fieldGred.GetLength(1) &&
                _fieldGred[parentX + kx, parentY + ky] == 0)
            {
                if ((parentX == parentX + kx && parentY != parentY + ky) ||
                    (parentX != parentX + kx && parentY == parentY + ky))
                    return _cellSize;
                return (int)(_cellSize * 1.4142135623730950f);
            }

            return -1;
        }

        private int HCost(int parentX, int parentY)
        {
            int length = 0, offsX = 0, offsY = 0;

            if (Math.Abs(_lx - parentX) != 0)
                offsX = (_lx - parentX) / Math.Abs(_lx - parentX);
            if (Math.Abs(_ly - parentY) != 0)
                offsY = (_ly - parentY) / Math.Abs(_ly - parentY);

            while (parentX != _lx && parentY != _ly)
            {
                parentX += offsX;
                parentY += offsY;
                length += (int)(_cellSize * 1.4142135623730950f);
            }

            while (parentX != _lx || parentY != _ly)
            {
                if (parentX != _lx) parentX += offsX;
                else
                    if (parentY != _ly) parentY += offsY;
                length += _cellSize;
            }

            return length;
        }

        private void MarkCell(int parentX, int parentY, int kx, int ky)
        {
            int g = GCost(parentX, parentY, kx, ky);

            if (g != -1)
            {
                int h = HCost(parentX + kx, parentY + ky);

                if (_openList.Exists((point) => point.X == parentX + kx && point.Y == parentY + ky))
                {
                    APoint tmp = _openList.Find((point) => point.X == parentX + kx && point.Y == parentY + ky);
                    if (tmp.F > g + h) tmp.F = g + h;
                }
                else
                    if (!_closeList.Exists((point) => point.X == parentX + kx && point.Y == parentY + ky))
                    {
                        int parentGCost = 0;
                        if (_closeList.Exists((point) => point.X == parentX && point.Y == parentY))
                        {
                            APoint tmpNode = _closeList.First((point) => point.X == parentX && point.Y == parentY);
                            parentGCost = tmpNode.F - tmpNode.H;
                        }
                        var tmp = new APoint { F = parentGCost + g + h, H = h, X = parentX + kx, Y = parentY + ky };
                        _openList.Add(tmp);
                    }
            }
        }

        private bool FindNextCell(ref int x, ref int y)
        {
            if (_openList.Count == 0) return false;
            x = _openList[0].X;
            y = _openList[0].Y;
            int minF = _openList[0].F, minH = _openList[0].H;

            for (int i = 0; i < _openList.Count; i++)
                if (_openList[i].F < minF && _openList[i].H <= minH)
                {
                    x = _openList[i].X;
                    y = _openList[i].Y;
                    minF = _openList[i].F;
                    minH = _openList[i].H;
                }
            return true;
        }

        private void FindPath()
        {
            while (_fx != _lx || _fy != _ly)
            {
                MarkCell(_fx, _fy, 0, -1);
                MarkCell(_fx, _fy, 1, -1);
                MarkCell(_fx, _fy, 1, 0);
                MarkCell(_fx, _fy, 1, 1);
                MarkCell(_fx, _fy, 0, 1);
                MarkCell(_fx, _fy, -1, 1);
                MarkCell(_fx, _fy, -1, 0);
                MarkCell(_fx, _fy, -1, -1);

                int x = 0, y = 0;
                if (!FindNextCell(ref x, ref y)) break;
                if (x == -1 || y == -1) break;
                APoint tmp = _openList.Find(point => point.X == x && point.Y == y);
                APoint toCloseList = new APoint(tmp);
                _openList.Remove(tmp);
                _closeList.Add(toCloseList);
                _fx = x;
                _fy = y;
            }
        }

    }
}
