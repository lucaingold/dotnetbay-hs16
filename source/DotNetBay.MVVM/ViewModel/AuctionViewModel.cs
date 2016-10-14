using DotNetBay.Core;
using DotNetBay.Data.Entity;
using DotNetBay.MVVM.Command;
using DotNetBay.MVVM.View;
using System;
using System.Windows;
using System.Windows.Input;

namespace DotNetBay.MVVM.ViewModel
{
    public class AuctionViewModel : ViewModelBase
    {
        private IMemberService _memberService;
        private IAuctionService _auctionService;

        private readonly Auction _auctionCurrent;


        public Auction AuctionCurrent => _auctionCurrent;

        public string Title
        {
            get { return this._auctionCurrent.Title; }
        }

        public string Description
        {
            get { return this._auctionCurrent.Description; }
        }

        public byte[] Image
        {
            get { return this._auctionCurrent.Image; }
        }

        public double CurrentPrice
        {
            get { return this._auctionCurrent.CurrentPrice; }
        }

        public double StartPrice
        {
            get { return this._auctionCurrent.StartPrice; }
        }

        public DateTime StartDateTimeUtc
        {
            get { return this._auctionCurrent.StartDateTimeUtc.ToLocalTime(); }
        }

        public DateTime CloseDateTimeUtc
        {
            get { return this._auctionCurrent.CloseDateTimeUtc.ToLocalTime(); }
        }

        public DateTime EndDateTimeUtc
        {
            get { return this._auctionCurrent.EndDateTimeUtc.ToLocalTime(); }
        }

        public String Seller
        {
            get { return this._auctionCurrent.Seller.DisplayName; }
        }

        public String Winner
        {
            get { return this._auctionCurrent.Winner.DisplayName; }
        }

        public bool IsClosed
        {
            get { return this._auctionCurrent.IsClosed; }
        }

        public bool IsRunning
        {
            get { return this._auctionCurrent.IsRunning; }
        }

        public int Bids
        {
            get { return this._auctionCurrent.Bids.Count; }
        }

        public AuctionViewModel(Auction auction)
        {
            _auctionCurrent = auction;
        }

        void PlaceBidExecute()
        {
            var bidView = new BidView(AuctionCurrent);
            bidView.ShowDialog(); // Blocking
        }

        public ICommand PlaceBidCommand
        {
            get
            {
                return new RelayCommand<Window>(PlaceBidExecute);
            }
        }
    }
}
