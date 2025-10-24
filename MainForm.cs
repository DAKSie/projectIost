using projectIost.Services;
using projectIost.Views;

namespace projectIost
{
    public partial class MainForm : Form
    {
        private readonly IIostService _service;
        private readonly InventoryView _inventoryView;

        // InventoryView is injected by DI; it itself receives IIostService via DI.
        public MainForm(IIostService service, InventoryView inventoryView)
        {
            _service = service;
            _inventoryView = inventoryView;
            InitializeComponent();
        }

        // Show the inventory view resolved from DI. InventoryView.OnShown will load items.
        private void btnLoad_Click(object sender, EventArgs e)
        {
            // Show will reuse the DI-constructed InventoryView instance.
            // If you prefer a new instance each time, register InventoryView as Transient and resolve a new one.
            _inventoryView.Show();
        }
    }
}