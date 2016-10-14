using DotNetBay.Core;
using DotNetBay.Data.Entity;
using DotNetBay.MVVM.ViewModel;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace DotNetBay.MVVM.View
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<Auction> _auctions;
        private readonly AuctionService _auctionService;

        public MainWindow()
        {
            InitializeComponent();
            //this.DataContext = this;
            var app = Application.Current as App;
            this._auctionService = new AuctionService(app.MainRepository, new SimpleMemberService(app.MainRepository));
            var memberService = new SimpleMemberService(app.MainRepository);

            this.DataContext = new MainViewModel(memberService, _auctionService);
        }
    }
}
