using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MinesweeperLib;

namespace wfaMinesweeper
{
    public partial class EndGameForm : Form
    {
        private Minesweeper _miner;
        public EndGameForm(Minesweeper miner)
        {
            InitializeComponent();
            _miner = miner;
            SetupForm();
        }
        private void SetupForm()
        {
            int playerNumbeerRefresh = 0;

            if (_miner.RoundCounter % 2 == 0) //Нечетный раунд
            {
                if (_miner.FightersFirstArmy <= 0)
                    playerNumbeerRefresh = 2; // Победил второй игрок
                else if (_miner.FightersSecondArmy <= 0)
                    playerNumbeerRefresh = 1; // Победил первый игрок
            }
            else  //Четный раунд
            {
                if (_miner.FightersFirstArmy <= 0)
                    playerNumbeerRefresh = 1; // Победил первый игрок
                else if (_miner.FightersSecondArmy <= 0)
                    playerNumbeerRefresh = 2; // Победил второй игрок
            }
            label1.Text = ($"Поздравляю с победой, Игрок {playerNumbeerRefresh}!");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            FormForStartGame formMain = new FormForStartGame();
            formMain.Show();
            this.DialogResult = DialogResult.OK;
        }
    }
}
