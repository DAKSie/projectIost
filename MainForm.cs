using projectIost.Views;

namespace projectIost
{
    public partial class MainForm : Form
    {
        private readonly IostController _controller;
        public MainForm(IostController controller)
        {
            _controller = controller;
            InitializeComponent();
        }

        private async void btnLoad_Click(object sender, EventArgs e)
        {
            InventoryView invView = new InventoryView();
            var items = await _controller.GetAllItemsAsync();
            invView.Show();
        }
    }
}
