using DotNetBay.Core;
using DotNetBay.Data.Entity;
using DotNetBay.MVVM.ViewModel;
using System.Windows;

namespace DotNetBay.MVVM.View
{

    public partial class BidView : Window
    {
        public BidView(Auction auction)
        {
            InitializeComponent();
            var app = Application.Current as App;
            var memberService = new SimpleMemberService(app.MainRepository);
            var auctionService = new AuctionService(app.MainRepository, memberService);
            this.DataContext = new BidViewModel(auction, memberService, auctionService);
        }
    }
}
