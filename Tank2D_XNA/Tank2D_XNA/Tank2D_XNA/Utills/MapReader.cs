using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Tank2D_XNA.GameField;

namespace Tank2D_XNA.Utills
{
    class MapReader
    {
        private readonly List<EntityInfo> _entity; 
        public MapReader(string fileName)
        {
            _entity = new List<EntityInfo>();

            try
            {
                using (XmlReader reader = XmlReader.Create(File.Open(fileName, FileMode.Open)))
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

                        tmp.Position = new Vector2(x, y);
                        _entity.Add(tmp);
                    }
                }
            }
            catch (IOException)
            {
                BattleField.GetInstance().SafeExit();
            }
        }

        public void Map(Action<EntityInfo> proc)
        {
            foreach (EntityInfo info in _entity)
            {
                proc(info);
            }
        }
    }
}
