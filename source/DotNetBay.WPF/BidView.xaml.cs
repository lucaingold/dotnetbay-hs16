using DotNetBay.Core;
using DotNetBay.Data.Entity;
using System.Collections.ObjectModel;
using System.Windows;

namespace DotNetBay.WPF
{
    /// <summary>
    /// Interaction logic for BidView.xaml
    /// </summary>
    public partial class BidView : Window
    {
        public Auction Auction { get; }

        public BidView(Auction auction)
        {
            this.Auction = auction;
            this.DataContext = this;
            InitializeComponent();
            this.DataContext = this;
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void PlaceBidButtonClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
