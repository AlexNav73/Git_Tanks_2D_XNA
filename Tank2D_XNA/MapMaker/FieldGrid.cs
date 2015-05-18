using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace MapMaker
{

    public struct EntityInfo
    {
        public string Type;
        public string TankType;
        public Point Position;
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    class FieldGrid
    {
        private readonly int[,] _grid;
        private bool _isPlayerSpoted;
        private readonly List<EntityInfo> _entity;
        private readonly int _width;
        private readonly int _height;
        private readonly int _cellSize;

        private readonly int _cellS; // cell size for game
        private readonly int _tankWidth; // half of tank width
        private readonly int _tankHeight; // half of tank height

        private readonly SolidBrush _emptyPlace = new SolidBrush(Color.White);
        private readonly SolidBrush _wallBrush = new SolidBrush(Color.Black);
        private readonly SolidBrush _aiBrush = new SolidBrush(Color.Red);
        private readonly SolidBrush _playerBrush = new SolidBrush(Color.Green);

        private readonly Pen _blackPen = new Pen(Color.Black);

        public FieldGrid(int width, int height, int cellSize, 
            int tankWidth = 30, 
            int tankHeight = 24)
        {
            _cellSize = cellSize;
            _tankWidth = tankWidth;
            _tankHeight = tankHeight;
            _width = width / _cellSize;
            _height = height / _cellSize;
            _cellS = tankWidth * 2;
            _entity = new List<EntityInfo>();

            _grid = new int[width / _cellSize, height / _cellSize];
        }

        public void SetBlock(int x, int y)
        {
            _grid[x/_cellSize, y/_cellSize] = 1;
        }

        public void SetAI(int x, int y)
        {
            _grid[x/_cellSize, y/_cellSize] = 2;
        }

        public void SetPlayer(int x, int y)
        {
            if (!_isPlayerSpoted)
            {
                _grid[x/_cellSize, y/_cellSize] = 3;
                _isPlayerSpoted = true;
            }
        }

        public void Draw(Graphics g)
        {
            for (int i = 0; i < _width; i++)
                for (int j = 0; j < _height; j++)
                {
                    SolidBrush brush;
                    if (_grid[i, j] == 0) brush = _emptyPlace;
                    else if (_grid[i, j] == 1) brush = _wallBrush;
                    else if (_grid[i, j] == 2) brush = _aiBrush;
                    else brush = _playerBrush;

                    g.FillRectangle(brush, i*_cellSize, j*_cellSize, _cellSize, _cellSize);
                    g.DrawRectangle(_blackPen, i * _cellSize, j * _cellSize, _cellSize, _cellSize);
                }
        }

        private void PrepareData()
        {
            for (int i = 0; i < _width; i++)
                for (int j = 0; j < _height; j++)
                {
                    if (_grid[i, j] == 1)
                    {
                        EntityInfo tmp = new EntityInfo
                        {
                            Type = "Block",
                            TankType = "",
                            Position = new Point(i * _cellS, j * _cellS)
                        };
                        _entity.Add(tmp);
                    }
                    else if (_grid[i, j] == 2)
                    {
                        EntityInfo tmp = new EntityInfo
                        {
                            Type = "AI",
                            TankType = "MT",
                            Position = new Point(i * _cellS + _tankWidth, j * _cellS + _tankHeight)
                        };
                        _entity.Add(tmp);
                    }
                    else if (_grid[i, j] == 3)
                    {
                        EntityInfo tmp = new EntityInfo
                        {
                            Type = "Player",
                            TankType = "MT",
                            Position = new Point(i * _cellS + _tankWidth, j * _cellS + _tankHeight)
                        };
                        _entity.Add(tmp);
                    }
                }
        }

        private void SetTag(XmlWriter writer, string type, string _class, int x, int y)
        {
            writer.WriteStartElement("Entity");
            writer.WriteAttributeString("type", type);
            writer.WriteAttributeString("class", _class);
            writer.WriteAttributeString("x", x.ToString());
            writer.WriteAttributeString("y", y.ToString());
            writer.WriteEndElement();
        }

        public void SaveMap(string fileName)
        {
            PrepareData();

            using (FileStream fs = File.Create(fileName))
            {
                using (XmlWriter writer = XmlWriter.Create(fs))
                {
                    writer.WriteStartElement("root");

                    EntityInfo tmp = _entity.Find(n => n.Type == "Player");
                    if (tmp.Type == null) return;
                    SetTag(writer, "Player", "MT", tmp.Position.X, tmp.Position.Y);

                    foreach (EntityInfo info in _entity)
                    {
                        if (info.Type == "AI")
                            SetTag(writer, "AI", "MT", info.Position.X, info.Position.Y);
                        else if (info.Type == "Block")
                            SetTag(writer, "Block", "", info.Position.X, info.Position.Y);
                    }

                    writer.WriteEndElement();
                }
            }

            MessageBox.Show("Map saved!", "Save");
        }

    }
}
