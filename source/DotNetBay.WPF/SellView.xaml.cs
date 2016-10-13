using DotNetBay.Core;
using DotNetBay.Data.Entity;
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
namespace DotNetBay.WPF
{
    /// <summary>
    /// Interaction logic for SellView.xaml
    /// </summary>
    public partial class SellView : Window
    {

        private String _imagePath;

        private readonly AuctionService _auctionService;

        private readonly Auction _auctionNew;



        public Auction AuctionNew => _auctionNew;

        public SellView()
        {
            InitializeComponent();
            this.DataContext = this;

            var app = Application.Current as App;
            var memberService = new SimpleMemberService(app.MainRepository);
            this._auctionService = new AuctionService(app.MainRepository, memberService);
            this._auctionNew = new Auction();
            this._auctionNew.StartDateTimeUtc = DateTime.Now;
            this._auctionNew.EndDateTimeUtc = DateTime.Now;
            this._auctionNew.Seller = memberService.GetCurrentMember();
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddAuctionButtonClick(object sender, RoutedEventArgs e)
        {
            this._auctionService.Save(this.AuctionNew);
            this.Close();
        }

        private void SelectImageButtonClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                _imagePath = openFileDialog.FileName;
                this.TxtImagePath.Text = _imagePath;
                _auctionNew.Image = File.ReadAllBytes(_imagePath);
            }

        }
    }
}
