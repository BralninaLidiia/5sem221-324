using MinesweeperLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wfaMinesweeper
{
    public partial class PlayerReadyForm : Form
    {
        private Minesweeper _miner;
        public PlayerReadyForm(Minesweeper miner)
        {
            InitializeComponent();
            _miner = miner;
            SetupForm();
        }
        private void SetupForm()
        {
            int playerNumbeerRefresh = 0;
            if (_miner.RoundCounter % 2 == 1)
                playerNumbeerRefresh = _miner.PayerNumber;
            else if (_miner.RoundCounter % 2 != 1 && _miner.PayerNumber == 1)
                playerNumbeerRefresh = 2;
            else if (_miner.RoundCounter % 2 != 1 && _miner.PayerNumber == 2)
                playerNumbeerRefresh = 1;
            label1.Text = ($"Игрок {playerNumbeerRefresh}, приготовиться!\n Раунд {_miner.RoundCounter}");
            label1.Location = new Point((ClientSize.Width - label1.Width) / 2, (ClientSize.Height - label1.Height) / 2);
            buttonOK.Location = new Point((ClientSize.Width - buttonOK.Width) / 2, (ClientSize.Height - buttonOK.Height) / 4*3);
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            //FormForGame formMain = new FormForGame(_miner);
            this.Close();
            //formMain.Show();
        }
    }
}
