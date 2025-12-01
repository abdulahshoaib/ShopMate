using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ShopMate.DTOs
{
#pragma warning disable CsWinRT1028
    internal class BillItemVm : INotifyPropertyChanged
#pragma warning restore CsWinRT1028
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public decimal UnitPrice { get; set; }

        private int qty;
        public int Quantity
        {
            get => qty;
            set
            {
                if (qty != value)
                {
                    qty = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Total));
                }
            }
        }

        public decimal Total => UnitPrice * Quantity;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}