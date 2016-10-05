using DotNetBay.Core;
using DotNetBay.Data.Entity;
using System.Collections.ObjectModel;
using System.Windows;

namespace DotNetBay.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Auction> auctions = new ObservableCollection<Auction>();
        private AuctionService auctionService;

        public ObservableCollection<Auction> Auctions
        {
            get
            {
                return this.auctions;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            var app = Application.Current as App;

            this.auctionService = new AuctionService(app.MainRepository, new SimpleMemberService(app.MainRepository));

            this.auctions = new ObservableCollection<Auction>(this.auctionService.GetAll());
        }

        private void SellButtonClick(object sender, RoutedEventArgs e)
        {
            var sellView = new SellView();
            sellView.ShowDialog(); // Blocking

        }

        private void PlaceBidButtonClick(object sender, RoutedEventArgs e)
        {
            var currentAuction = (Auction)this.AuctionsDataGrid.SelectedItem;

            var bidView = new BidView(currentAuction);
            bidView.ShowDialog(); // Blocking


        }


    }
}
