using DotNetBay.Core;
using DotNetBay.MVVM.Command;
using DotNetBay.MVVM.View;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using DotNetBay.Data.Entity;

namespace DotNetBay.MVVM.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private IMemberService _memberService;
        private IAuctionService _auctionService;

        //private ObservableCollection<Auction> _auctions;

        private ObservableCollection<AuctionViewModel> auctions = new ObservableCollection<AuctionViewModel>();

        public ObservableCollection<AuctionViewModel> Auctions
        {
            get
            {
                return this.auctions;
            }

            private set
            {
                this.auctions = value;
                base.OnPropertyChanged();
            }
        }

        public MainViewModel(IMemberService memberService, IAuctionService auctionService)
        {
            this._auctionService = auctionService;
            this._memberService = memberService;

            foreach (var auction in this._auctionService.GetAll())
            {
                auctions.Add(new AuctionViewModel(auction));
            }

        }

        void SellExecute()
        {
            var sellView = new SellView();
            sellView.ShowDialog(); // Blocking
            //Update auctionlist
            this.Auctions = new ObservableCollection<AuctionViewModel>();
            foreach (var auction in this._auctionService.GetAll())
            {
                auctions.Add(new AuctionViewModel(auction));
            }
        }

        public ICommand SellCommand
        {
            get
            {
                return new RelayCommand<Window>(SellExecute);
            }
        }


    }
}
