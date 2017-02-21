using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using DogeNews.Data.Contracts;
using DogeNews.Data.Models;
using DogeNews.Web.Models;
using DogeNews.Web.Mvp.UserControls.AdminActionAudit;
using DogeNews.Web.Mvp.UserControls.AdminActionAudit.EventArguments;
using Moq;
using NUnit.Framework;

namespace DogeNews.Web.Mvp.Tests.PresenterTests.AdminActionAudit
{
    [TestFixture]
    public class AdminActionAuditPresenterTests
    {
        private Mock<IAdminActionAuditView> mockedView;
        private Mock<IProjectableRepository<AdminActionLog>> mockedRepository;

        [SetUp]
        public void Init()
        {
            this.mockedView = new Mock<IAdminActionAuditView>();
            this.mockedView.Setup(x => x.Model).Returns(new AdminActionAuditViewModel());

            this.mockedRepository = new Mock<IProjectableRepository<AdminActionLog>>();
            this.mockedRepository.Setup(
                x => x.GetAllMapped<AdminActionLogWebModel>()).Returns(() => new List<AdminActionLogWebModel>());
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullExepction_WhenRepositoryIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new AdminActionAuditPresenter(this.mockedView.Object, null));
        }

        [Test]
        public void Constructor_ShouldNotThrowArgumentNullExepction_WhenAllParametersAreNotNull()
        {
            Assert.DoesNotThrow(() => new AdminActionAuditPresenter(this.mockedView.Object, this.mockedRepository.Object));
        }

        [Test]
        public void LoadLogs_ShouldCallRepositoryGetMappedToAdminActionLogWebModelOnce()
        {
            AdminActionAuditPresenter presenter = new AdminActionAuditPresenter(this.mockedView.Object, this.mockedRepository.Object);

            presenter.LoadLogs(new Button(), new PageLoadEventArgs());

            this.mockedRepository.Verify(x => x.GetAllMapped<AdminActionLogWebModel>(), Times.Once);
        }

        [Test]
        public void LoadLogs_ShouldCallRepositoryGetMappedToAdminActionLogWebModelOnce_WhenEventIsRaisedByTheView()
        {
            AdminActionAuditPresenter presenter = new AdminActionAuditPresenter(this.mockedView.Object, this.mockedRepository.Object);

            this.mockedView.Raise(x => x.PageLoad += presenter.LoadLogs, null, new PageLoadEventArgs());

            this.mockedRepository.Verify(x => x.GetAllMapped<AdminActionLogWebModel>(), Times.Once);
        }
    }
}