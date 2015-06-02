using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace MapMaker
{
    class MapLoader
    {
        private readonly List<EntityInfo> _entity;

        public MapLoader(string fileName)
        {
            _entity = new List<EntityInfo>();
            if (String.IsNullOrEmpty(fileName)) return;

            try
            {
                using (FileStream fs = File.Open(fileName, FileMode.Open))
                    using (XmlReader reader = XmlReader.Create(fs))
                    {
                        while (reader.ReadToFollowing("Entity"))
                        {
                            EntityInfo tmp = new EntityInfo();
                            reader.MoveToFirstAttribute();
                            tmp.Type = reader.GetAttribute("type");
                            tmp.TankType = reader.GetAttribute("class");

                            int x, y;
                            Int32.TryParse(reader.GetAttribute("x"), out x);
                            Int32.TryParse(reader.GetAttribute("y"), out y);

                            tmp.Position = new Point(x, y);
                            _entity.Add(tmp);
                        }
                    }
            }
            catch (IOException)
            {
                MessageBox.Show("Could not open file.", "Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        public void LoadMap(FieldGrid grid)
        {
            grid.ClearField();

            foreach (EntityInfo info in _entity)
            {
                switch (info.Type)
                {
                    case "Player":
                        grid.LoadPlayer(info.Position.X, info.Position.Y);
                        break;
                    case "AI":
                        grid.LoadAI(info.Position.X, info.Position.Y);
                        break;
                    case "Block":
                        grid.LoadBlock(info.Position.X, info.Position.Y);
                        break;
                }
            }
        }
    }
}
