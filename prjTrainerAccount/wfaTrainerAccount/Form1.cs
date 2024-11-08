using libCore;

namespace wfaTrainerAccount
{
    public partial class Form1 : Form
    {
        private readonly Game g;

        public Form1()
        {
            InitializeComponent();

            g = new Game();
            g.ChangeQuestion += () => laQuestion.Text = g.QuestionLine;
            g.ChangeStat += G_ChangeStat;

            g.Restart();
            buYes.Click += (s, e) => g.DoAnswer(true);
            buNo.Click += (s, e) => g.DoAnswer(false);

        }

        private void G_ChangeStat()
        {
            throw new NotImplementedException();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
