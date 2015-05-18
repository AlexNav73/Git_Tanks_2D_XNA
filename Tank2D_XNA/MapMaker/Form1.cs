using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapMaker
{
    public partial class Form1 : Form
    {

        private FieldGrid _grid;
        private bool _isDrawWall;

        public Form1()
        {
            InitializeComponent();

            _grid = new FieldGrid(Map.Width, Map.Height, 20);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(FileName.Text))
            {
                _grid.SaveMap(FileName.Text);
            }
        }

        private void Map_MouseClick(object sender, MouseEventArgs e)
        {
            foreach (Control control in EntityType.Controls)
                if (control is RadioButton)
                    if (((RadioButton)control).Checked)
                        if (control.Tag.ToString() == "0")
                            _grid.SetPlayer(e.X, e.Y);
                        else if (control.Tag.ToString() == "1")
                            _grid.SetAI(e.X, e.Y);
                        else if (control.Tag.ToString() == "2")
                            _grid.SetBlock(e.X, e.Y);
            Map.Invalidate();
        }

        private void Map_Paint(object sender, PaintEventArgs e)
        {
            _grid.Draw(e.Graphics);
        }

        private void Map_MouseDown(object sender, MouseEventArgs e)
        {
            foreach (Control control in EntityType.Controls)
                if (control is RadioButton)
                    if (((RadioButton)control).Checked)
                        if (control.Tag.ToString() == "2")
                        {
                            _isDrawWall = true;
                            _grid.SetBlock(e.X, e.Y);
                            Map.Invalidate();
                        }

        }

        private void Map_MouseUp(object sender, MouseEventArgs e)
        {
            foreach (Control control in EntityType.Controls)
                if (control is RadioButton)
                    if (((RadioButton)control).Checked)
                        if (control.Tag.ToString() == "2")
                        {
                            _isDrawWall = false;
                            _grid.SetBlock(e.X, e.Y);
                            Map.Invalidate();
                        }
        }

        private void Map_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDrawWall)
            {
                _grid.SetBlock(e.X, e.Y);
                Map.Invalidate();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int tankWidth = 30, tankHeigth = 24;

            if (!String.IsNullOrEmpty(TW.Text)) Int32.TryParse(TW.Text, out tankWidth);
            if (!String.IsNullOrEmpty(TH.Text)) Int32.TryParse(TH.Text, out tankHeigth);

            _grid = new FieldGrid(Map.Width, Map.Height, 20, tankWidth, tankHeigth);
            Map.Invalidate();
        }
    }
}
