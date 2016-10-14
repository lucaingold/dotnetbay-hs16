using DotNetBay.Core;
using DotNetBay.MVVM.ViewModel;
using System.Windows;

namespace DotNetBay.MVVM.View
{
    /// <summary>
    /// Interaction logic for SellView.xaml
    /// </summary>
    public partial class SellView : Window
    {
        public SellView()
        {
            InitializeComponent();
            //this.DataContext = this;
            var app = Application.Current as App;
            var memberService = new SimpleMemberService(app.MainRepository);
            var auctionService = new AuctionService(app.MainRepository, memberService);
            this.DataContext = new SellViewModel(memberService, auctionService);
        }
    }
}
