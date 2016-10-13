using DotNetBay.Core;
using DotNetBay.Data.Entity;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;

namespace DotNetBay.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private ObservableCollection<Auction> _auctions;
        private readonly AuctionService _auctionService;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Auction> Auctions
        {
            get
            {
                return this._auctions;
            }

            private set
            {
                this._auctions = value;
                this.OnPropertyChanged();
            }
        }

        public void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            var app = Application.Current as App;

            this._auctionService = new AuctionService(app.MainRepository, new SimpleMemberService(app.MainRepository));
            this._auctions = new ObservableCollection<Auction>(this._auctionService.GetAll());
        }

        private void SellButtonClick(object sender, RoutedEventArgs e)
        {
            var sellView = new SellView();
            sellView.ShowDialog(); // Blocking
            //Update auctionlist
            this.Auctions = new ObservableCollection<Auction>(_auctionService.GetAll());
        }

        private void PlaceBidButtonClick(object sender, RoutedEventArgs e)
        {
            var currentAuction = (Auction)this.AuctionsDataGrid.SelectedItem;
            var bidView = new BidView(currentAuction);
            bidView.ShowDialog(); // Blocking
        }


    }

    public class BooleanToStatusTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            switch ((Boolean)value)
            {
                case true:
                    return "Abgeschlossen";
                case false:
                    return "Offen";
            }
            return "Offen";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is String)
            {
                if (value.ToString().ToLower() == "Abgeschlossen")
                    return true;
                else
                    return false;
            }
            return false;
        }
    }
}
