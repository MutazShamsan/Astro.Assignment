using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Astro.Assignment.Web.FavoriteChannelManagement;
using Astro.Assignment.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Astro.Assignment.Web.Test
{
    [TestClass]
    public class FavoriteRepoDatabaseTest
    {
        [TestMethod]
        public void AddNewFavorite()
        {
            var mockSet = new Mock<DbSet<Models.UserFavoriteChannelsModels>>();


            var list = new List<UserFavoriteChannelsModels>();
            var queryable = list.AsQueryable();
            mockSet.As<IQueryable<UserFavoriteChannelsModels>>().Setup(m => m.Provider).Returns(queryable.Provider);
            mockSet.As<IQueryable<UserFavoriteChannelsModels>>().Setup(m => m.Expression).Returns(queryable.Expression);
            mockSet.As<IQueryable<UserFavoriteChannelsModels>>().Setup(m => m.ElementType)
                .Returns(queryable.ElementType);
            mockSet.As<IQueryable<UserFavoriteChannelsModels>>().Setup(m => m.GetEnumerator())
                .Returns(() => queryable.GetEnumerator());


            var mockContext = new Mock<ApplicationDbContext>();

            mockContext.Setup(m => m.UserFavoriteChannels).Returns(mockSet.Object);

            var service = new FavoriteRepositoryDatabase(mockContext.Object);
            service.UpdateFavoriteRepo("mutaz.shamsan", 4);

            mockSet.Verify(m => m.Add(It.IsAny<UserFavoriteChannelsModels>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }
    }
}
