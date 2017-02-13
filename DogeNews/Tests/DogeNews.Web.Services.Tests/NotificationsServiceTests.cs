using System.Collections.Generic;
using DogeNews.Common.Enums;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Moq;
using NUnit.Framework;

namespace DogeNews.Web.Services.Tests
{
    [TestFixture]
    public class NotificationsServiceTests
    {
        [Test]
        public void Sleep_ShouldReturnNotificationsServiceSoMethodsOfTheServiceAreChainable()
        {
            var service = new NotificationsService();

            Assert.AreEqual(typeof(NotificationsService), service.Sleep(1).GetType());
        }

        [Test]
        public void Clients_Get_ShouldReturnSetClients_WhenClientsAreNotNull()
        {
            var mockClients = new Mock<IClientProxy>();
            var service = new NotificationsService();

            service.Clients = mockClients.Object;

            var clients = service.Clients;

            Assert.AreSame(mockClients.Object, clients);
        }

        [Test]
        public void Clients_Set_ShouldSetPassedValueWhenItIsNotNull()
        {
            var mockClients = new Mock<IClientProxy>();
            var service = new NotificationsService();

            service.Clients = mockClients.Object;

            var clients = service.Clients;

            Assert.AreSame(mockClients.Object, clients);
        }
    }
}