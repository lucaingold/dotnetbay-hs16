using DotNetBay.Core;
using DotNetBay.Data.Entity;
using DotNetBay.MVVM.Command;
using System;
using System.Windows;
using System.Windows.Input;

namespace DotNetBay.MVVM.ViewModel
{
    public class BidViewModel : ViewModelBase
    {

        private readonly Auction _auctionCurrent;
        private IMemberService _memberService;

        public Auction AuctionCurrent => _auctionCurrent;

        public double CurrentBid { get; set; }

        public string Title
        {
            get { return this._auctionCurrent.Title; }
        }

        public string Description
        {
            get { return this._auctionCurrent.Description; }
        }

        public byte[] AuctionImage
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

        public DateTime CloseDateTimeLocal
        {
            get { return this._auctionCurrent.CloseDateTimeUtc.ToLocalTime(); }
        }

        public BidViewModel(Auction auction, IMemberService memberService, IAuctionService auctionService)
        {
            this._auctionCurrent = auction;
            this._memberService = memberService;
        }

        void PlaceBidExecute()
        {
            Bid bid = new Bid
            {
                Auction = AuctionCurrent,
                Bidder = _memberService.GetCurrentMember(),
                Amount = CurrentBid,

            };
            _auctionCurrent.Bids.Add(bid);
        }

        bool CanUpdatePlaceBidExecute()
        {
            return true;
        }

        public ICommand PlaceBidCommand
        {
            get
            {
                return new RelayCommand<Window>(PlaceBidExecute, CanUpdatePlaceBidExecute);
            }
        }
    }
}
