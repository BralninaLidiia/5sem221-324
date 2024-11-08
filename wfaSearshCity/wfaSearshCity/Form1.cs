namespace wfaSearshCity
{
    public partial class Form1 : Form
    {
        private readonly string[] cities;

        public Form1()
        {
            InitializeComponent();

            cities = Properties.Resources.goroda.Split('\n');
            edSearch.TextChanged += EdSearch_TextChanged;

        }

        private void EdSearch_TextChanged(object? sender, EventArgs e)
        {
            var r = cities
                .Where(i => i.ToUpper().Contains(edSearch.Text.ToUpper()))
                .ToArray();
            edResult.Text=string.Join(Environment.NewLine, r);
            this.Text=edSearch.Text;
        }
    }
}
