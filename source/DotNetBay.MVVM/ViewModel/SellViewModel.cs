using DotNetBay.Core;
using DotNetBay.Data.Entity;
using DotNetBay.MVVM.Command;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace DotNetBay.MVVM.ViewModel
{
    public class SellViewModel : ViewModelBase
    {

        public RelayCommand<Window> CloseWindowCommand { get; private set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public long StartPrice { get; set; }

        public DateTime StartDateTimeUtc { get; set; }

        public DateTime EndDateTimeUtc { get; set; }

        public string ImagePath { get; set; }

        private byte[] _image;

        private readonly IAuctionService _auctionService;

        private readonly Auction _auctionNew;
        private IMemberService memberService;

        public Auction AuctionNew => _auctionNew;

        public SellViewModel(IMemberService memberService, IAuctionService auctionService)
        {
            var app = Application.Current as App;
            this._auctionService = auctionService;
            this._auctionNew = new Auction();
            StartDateTimeUtc = DateTime.Now;
            EndDateTimeUtc = DateTime.Now;
            this.memberService = memberService;
            Title = "";
            Description = "";



            //            this.CloseWindowCommand = new RelayCommand<Window>(this.CloseWindow());|

        }

        #region Commands
        void AddAuctionExecute()
        {
            this._auctionService.Save(new Auction
            {
                Title = this.Title,
                Description = this.Description,
                StartDateTimeUtc = this.StartDateTimeUtc,
                EndDateTimeUtc = this.EndDateTimeUtc,
                StartPrice = this.StartPrice,
                Image = this._image,
                Seller = memberService.GetCurrentMember()
            });
            //this.Close();
        }

        public bool CanUpdateAddAuctionExecute()
        {
            if (!Title.Equals("") && !Description.Equals(""))
                return true;
            return false;
        }

        public ICommand AddAuctionCommand
        {
            get
            {
                return new RelayCommand<Window>(AddAuctionExecute, CanUpdateAddAuctionExecute);
            }
        }

        void SelectImageExecute()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                ImagePath = openFileDialog.FileName;
                _image = File.ReadAllBytes(ImagePath);
            }
        }

        public ICommand SelectImageCommand
        {
            get
            {
                return new RelayCommand<Window>(SelectImageExecute);
            }
        }

        private void CloseWindow(Window window)
        {
            if (window != null)
            {
                window.Close();
            }
        }
        #endregion


    }
}
