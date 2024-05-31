using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KingAlbert
{
    public partial class Player : Form
    {
        public string PlayerName { get; private set; }

        public Player()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            PlayerName = txtPlayerName.Text.Trim();
            if (!string.IsNullOrEmpty(PlayerName))
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Введите корректное имя.");
            }
        }

        private void Player_Load(object sender, EventArgs e)
        {

        }
    }
}
