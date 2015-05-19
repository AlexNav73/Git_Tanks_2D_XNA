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

            _grid = new FieldGrid(20, 1920, 1080);
            Zoom.SelectedIndex = 10;
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
                        else if (control.Tag.ToString() == "3")
                            _grid.ClearCell(e.X, e.Y);
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
            int screenWidth = 1920, screenHeigth = 1080;

            if (!String.IsNullOrEmpty(SW.Text)) Int32.TryParse(SW.Text, out screenWidth);
            if (!String.IsNullOrEmpty(SH.Text)) Int32.TryParse(SH.Text, out screenHeigth);

            _grid = new FieldGrid(20, screenWidth, screenHeigth);
            Map.Invalidate();
        }

        private void Zoom_SelectedItemChanged(object sender, EventArgs e)
        {
            int zoom;

            Int32.TryParse(Zoom.Items[Zoom.SelectedIndex].ToString(), out zoom);
            
            _grid.Zoom(zoom);
            Map.Invalidate();
        }
    }
}
