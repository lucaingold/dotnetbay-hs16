using DotNetBay.Core;
using DotNetBay.Core.Execution;
using DotNetBay.Data.Entity;
using DotNetBay.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace DotNetBay.WPF.Test
{
    [TestClass]
    public class UnitTest1
    {
        public IMainRepository MainRepository { get; private set; }
        public IAuctionRunner AuctionRunner { get; private set; }
        IMemberService memberService;
        IAuctionService service;
        [TestInitialize]
        public void SetUp()
        {
            memberService = new SimpleMemberService(MainRepository);
            service = new AuctionService(MainRepository, memberService);

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
