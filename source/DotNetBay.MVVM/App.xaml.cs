using DotNetBay.Core;
using DotNetBay.Core.Execution;
using DotNetBay.Data.Entity;
using DotNetBay.Data.Provider.FileStorage;
using DotNetBay.Interfaces;
using System;
using System.IO;
using System.Linq;
using System.Windows;

namespace DotNetBay.MVVM
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IMainRepository MainRepository { get; private set; }
        public IAuctionRunner AuctionRunner { get; private set; }

        public App()
        {


            String path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\\..\\"));

            this.MainRepository = new FileSystemMainRepository(path + @"data\data.json");
            this.MainRepository.SaveChanges();

            var memberService = new SimpleMemberService(this.MainRepository);
            var service = new AuctionService(this.MainRepository, memberService);

            if (!service.GetAll().Any())
            {
                var me = memberService.GetCurrentMember();

                service.Save(new Auction
                {
                    Title = "Kaputtes Auto",
                    Description = "This is a description",
                    StartDateTimeUtc = DateTime.UtcNow.AddSeconds(10),
                    EndDateTimeUtc = DateTime.UtcNow.AddDays(14),
                    StartPrice = 72,
                    Image = File.ReadAllBytes(path + @"data\img\auto.jpg"),
                    Seller = me,
                    IsClosed = true,
                    IsRunning = false
                });
                service.Save(new Auction
                {
                    Title = "Haufen Schrott",
                    Description = "This is a description",
                    StartDateTimeUtc = DateTime.UtcNow.AddSeconds(10),
                    EndDateTimeUtc = DateTime.UtcNow.AddDays(14),
                    StartPrice = 3000,
                    Image = File.ReadAllBytes(path + @"data\img\schrott.jpg"),
                    Seller = me,
                    IsClosed = false,
                    IsRunning = true
                });
            }


            this.AuctionRunner = new AuctionRunner(this.MainRepository);
            this.AuctionRunner.Start();
        }
    }
}
