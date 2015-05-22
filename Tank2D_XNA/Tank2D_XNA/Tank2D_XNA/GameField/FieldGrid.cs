using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Tank2D_XNA.Utills;

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

            public int ParentX, ParentY;

            public APoint(APoint point)
            {
                X = point.X;
                Y = point.Y;
                F = point.F;
                H = point.H;

                ParentX = point.ParentX;
                ParentY = point.ParentY;
            }
        }

        private readonly int _cellSize;
        private readonly int[,] _fieldGred;
        private int _firstX = -1, _firstY = -1, _currentX = -1, _currentY = -1, _lastX = -1, _lastY = -1;
        private readonly List<APoint> _openList = new List<APoint>();
        private readonly List<APoint> _closeList = new List<APoint>();
        private readonly List<APoint> _finalPath = new List<APoint>();

        public FieldGrid(int width, int height, int cellSize)
        {
            _fieldGred = new int[width / cellSize + 1, height / cellSize + 1];
            _cellSize = cellSize;
        }

        public void SetStart(Vector2 pos)
        {
            _firstX = (int)(pos.X / _cellSize);
            _firstY = (int)(pos.Y / _cellSize);

            _currentX = _firstX;
            _currentY = _firstY;
        }

        public bool SetFinish(Vector2 pos)
        {
            _lastX = (int)(pos.X / _cellSize);
            _lastY = (int)(pos.Y / _cellSize);

            if (_fieldGred[_lastX, _lastY] != 0) return false;

            _openList.Clear();
            _closeList.Clear();
            _finalPath.Clear();

            _closeList.Add(new APoint
            {
                H = HCost(_currentX, _currentY), 
                F = HCost(_currentX, _currentY), 
                X = _currentX, Y = _currentY,
                ParentX = -1,
                ParentY = -1
            });
            SearchPath();

            return true;
        }

        public void PlaceWall(Vector2 pos)
        {
            _fieldGred[(int)(pos.X / _cellSize), (int)(pos.Y / _cellSize)] = 1;
        }

        public List<Vector2> GetPath()
        {
            return _finalPath.Select(point => new Vector2() { X = point.X * _cellSize, Y = point.Y * _cellSize }).ToList();
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

            if (Math.Abs(_lastX - parentX) != 0)
                offsX = (_lastX - parentX) / Math.Abs(_lastX - parentX);
            if (Math.Abs(_lastY - parentY) != 0)
                offsY = (_lastY - parentY) / Math.Abs(_lastY - parentY);

            while (parentX != _lastX && parentY != _lastY)
            {
                parentX += offsX;
                parentY += offsY;
                length += (int)(_cellSize * 1.4142135623730950f);
            }

            while (parentX != _lastX || parentY != _lastY)
            {
                if (parentX != _lastX) parentX += offsX;
                else
                    if (parentY != _lastY) parentY += offsY;
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

                if (_openList.Exists(point => parentX == point.X && parentY == point.Y))
                {
                    APoint tmp = _openList.Find(point => parentX == point.X && parentY == point.Y);

                    if (tmp.F > g + h)
                    {
                        tmp.F = g + h;
                        tmp.ParentX = parentX;
                        tmp.ParentY = parentY;
                    }
                }
                else
                    if (!_closeList.Exists(point => parentX + kx == point.X && parentY + ky == point.Y))
                    {
                        APoint tmp = new APoint()
                        {
                            F = g + h, 
                            H = h, 
                            ParentX = parentX, ParentY = parentY, 
                            X = parentX + kx, Y = parentY + ky
                        };
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

        private void TraceBack()
        {
            if (_closeList.Last().X == _lastX && _closeList.Last().Y == _lastY)
            {
                _finalPath.Add(_closeList.Last());
                while ((_finalPath.Last().X != _firstX || _finalPath.Last().Y != _firstY) && 
                    (_finalPath.Last().ParentX != -1 || _finalPath.Last().ParentY != -1))
                {
                    foreach (APoint point in _closeList)
                        if (_finalPath.Last().ParentX == point.X && _finalPath.Last().ParentY == point.Y)
                        {
                            _finalPath.Add(point);
                            break;
                        }
                }
            }
        }

        // Corner cells not used
        private void SearchPath()
        {
            while (_currentX != _lastX || _currentY != _lastY)
            {
                MarkCell(_currentX, _currentY, 0, -1);
                //MarkCell(_currentX, _currentY, 1, -1);
                MarkCell(_currentX, _currentY, 1, 0);
                //MarkCell(_currentX, _currentY, 1, 1);
                MarkCell(_currentX, _currentY, 0, 1);
                //MarkCell(_currentX, _currentY, -1, 1);
                MarkCell(_currentX, _currentY, -1, 0);
                //MarkCell(_currentX, _currentY, -1, -1);

                int x = 0, y = 0;
                if (!FindNextCell(ref x, ref y)) break;
                if (x == -1 || y == -1) break;
                APoint tmp = _openList.Find(point => point.X == x && point.Y == y);
                var toCloseList = new APoint(tmp);
                _openList.Remove(tmp);
                _closeList.Add(toCloseList);
                _currentX = x;
                _currentY = y;
            }

            TraceBack();
        }

    }
}
