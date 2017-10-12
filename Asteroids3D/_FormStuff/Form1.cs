/***********************************************************************************
 * Galen Cochrane 12 DEC 2014
 **********************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Asteroids3D
{
    public partial class Form1 : Form
    {
        #region Fields

        GameHub game;

        #endregion

        #region Constructor

        public Form1()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;            

            game = new GameHub(this.CreateGraphics());  // love how simple this is right now.
        }

        #endregion
    }
}
