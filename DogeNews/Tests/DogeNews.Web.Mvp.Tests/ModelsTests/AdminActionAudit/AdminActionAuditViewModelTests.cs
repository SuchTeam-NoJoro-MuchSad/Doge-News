using System.Collections.Generic;
using DogeNews.Web.Models;
using DogeNews.Web.Mvp.UserControls.AdminActionAudit;
using NUnit.Framework;

namespace DogeNews.Web.Mvp.Tests.ModelsTests.AdminActionAudit
{
    [TestFixture]
    public class AdminActionAuditViewModelTests
    {
        [Test]
        public void Logs_Get_ShouldReturnSetValue()
        {
            AdminActionAuditViewModel model = new AdminActionAuditViewModel();
            List<AdminActionLogWebModel> expectedCollection = new List<AdminActionLogWebModel>();
            model.Logs = expectedCollection;

            Assert.NotNull(model.Logs);
            Assert.AreSame(expectedCollection,model.Logs);
        }
    }
}