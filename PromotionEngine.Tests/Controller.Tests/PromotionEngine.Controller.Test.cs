using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using PromotionEngineAPI.Controllers;
using PromotionEngineAPI.Models;
using PromotionEngineAPI.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine.Tests.Controller.Tests
{
    [TestFixture]
    public class PromotionEngine
    {
        private PromotionEngineController _promoController;
        private Mock<IPromotionDataService> _mockPromoService;

        [SetUp]
        public void Init()
        {
            this._mockPromoService = new Mock<IPromotionDataService>();

            this._mockPromoService.Setup(c => c.GetActiveCurrentOffers()).Returns(It.IsAny<List<SKUOfferDetails>>());
            this._mockPromoService.Setup(c => c.AddToCart(It.IsAny<List<PurchaseDetail>>())).Returns(It.IsAny<FinalCart>());
            this._mockPromoService.Setup(c => c.AddIndividualOffer(It.IsAny<IndividualSKUOffer>()));
            this._mockPromoService.Setup(c => c.AddComboOffer(It.IsAny<ComboOffer>()));
            this._mockPromoService.Setup(c => c.ApplyComboOffer(It.IsAny<SKUComboRelation>()));
            this._mockPromoService.Setup(c => c.ApplyIndividualOffer(It.IsAny<SKUIndividualOfferRelation>()));

            this._promoController = new PromotionEngineController(this._mockPromoService.Object);
        }

        [Test]
        public void GetAllOfferTest() => this._promoController.GetCurrentOffers().Should().BeOfType<OkObjectResult>();

        [Test]
        public void AddToCartTest() => this._promoController.AddToCart(new List<PurchaseDetail>()).Should().BeOfType<OkObjectResult>();

        [Test]
        public void AddIndividualOfferTest() => this._promoController.AddIndividualOffer(new IndividualSKUOffer()).Should().BeOfType<OkResult>();

        [Test]
        public void AddComboOfferTest() => this._promoController.AddComboOffer(new ComboOffer()).Should().BeOfType<OkResult>();

        [Test]
        public void ApplyIndividualOfferTest() => this._promoController.ApplyIndividualOffer(new SKUIndividualOfferRelation()).Should().BeOfType<OkResult>();

        [Test]
        public void ApplyComboOfferTest() => this._promoController.ApplyComboOffer(new SKUComboRelation()).Should().BeOfType<OkResult>();

       
    }
}
