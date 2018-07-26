using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Contacts.Management.Api.Controllers;
using Contacts.Management.Api.DataAccess.Interfaces;
using Contacts.Management.Api.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Web.Http;
using System.Web.Http.Hosting;
using Contacts.Management.Api.Models.ErrorResponses;
using Moq;
using Contacts.Management.Api.Tests.Helper;

namespace Contacts.Management.Api.Tests.Controllers
{
    [TestClass]
    public class ContactsControllerTest
    {
        private Mock<IContactsRepository> _mockContactRepository;
        private ContactsController _contactsController;

        [TestInitialize]
        public void init()
        {
            _mockContactRepository = new Mock<IContactsRepository>();
            _contactsController = new ContactsController(_mockContactRepository.Object);
            _contactsController.Request = new HttpRequestMessage();
            _contactsController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
        }

        #region Test cases for Get
        [TestMethod]
        public void Get_Returns_AllContacts()
        {
            //Arrange
            _mockContactRepository.Setup(c => c.GetContacts()).Returns(Responses.ContactsResponse());

            //Act
            var response = _contactsController.Get();

            //Assert
            BaseResponse<List<Contact>> contacts;
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            Assert.IsTrue(response.TryGetContentValue<BaseResponse<List<Contact>>>(out contacts));
            Assert.IsNotNull(contacts.Data);
            Assert.AreEqual(1, contacts.Data[0].Id);
            Assert.IsTrue(contacts.Data.Count == 2);
        }

        [TestMethod]
        public void Get_Returns_Exception()
        {
            //Arrange
            _mockContactRepository.Setup(c => c.GetContacts()).Throws(new Exception());

            //Act
            var response = _contactsController.Get();

            //Assert
            BaseResponse<BasicError> error;
            Assert.AreEqual(response.StatusCode, HttpStatusCode.InternalServerError);
            Assert.IsTrue(response.TryGetContentValue<BaseResponse<BasicError>>(out error));
            Assert.IsTrue(error.IsError);
            Assert.AreEqual(error.Errors.ErrorCode, ErrorCodes.TechnicalError.ToString());
        }
        #endregion

        #region Test cases for Post
        [TestMethod]
        public void Post_Returns_Valid_Response()
        {
            //Arrange
            _mockContactRepository.Setup(c => c.AddContact(It.IsAny<Contact>())).Returns(true);

            //Act
            var response = _contactsController.Post(Requests.ContactRequest());

            //Assert
            BaseResponse<bool> contactAddedResponse;
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            Assert.IsTrue(response.TryGetContentValue<BaseResponse<bool>>(out contactAddedResponse));
            Assert.IsFalse(contactAddedResponse.IsError);
            Assert.IsTrue(contactAddedResponse.Data);
        }

        [TestMethod]
        public void Post_Returns_Exception()
        {
            //Arrange
            _mockContactRepository.Setup(c => c.AddContact(It.IsAny<Contact>())).Throws(new Exception());

            //Act
            var response = _contactsController.Post(Requests.ContactRequest());

            //Assert
            BaseResponse<BasicError> contactAddedResponse;
            Assert.AreEqual(response.StatusCode, HttpStatusCode.InternalServerError);
            Assert.IsTrue(response.TryGetContentValue<BaseResponse<BasicError>>(out contactAddedResponse));
            Assert.IsTrue(contactAddedResponse.IsError);
            Assert.AreEqual(contactAddedResponse.Errors.ErrorCode, ErrorCodes.TechnicalError.ToString());
        }

        #endregion

        #region Test cases for Put
        [TestMethod]
        public void Put_Returns_Valid_Response()
        {
            //Arrange
            _mockContactRepository.Setup(c => c.UpdateContact(It.IsAny<Contact>())).Returns(true);

            //Act
            var response = _contactsController.Put(Requests.ContactRequest());

            //Assert
            BaseResponse<bool> contactUpdatedResponse;
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            Assert.IsTrue(response.TryGetContentValue<BaseResponse<bool>>(out contactUpdatedResponse));
            Assert.IsFalse(contactUpdatedResponse.IsError);
            Assert.IsTrue(contactUpdatedResponse.Data);
        }

        [TestMethod]
        public void Put_Returns_Failure_Response()
        {
            //Arrange
            _mockContactRepository.Setup(c => c.UpdateContact(It.IsAny<Contact>())).Returns(false);

            //Act
            var response = _contactsController.Put(Requests.ContactRequest());

            //Assert
            BaseResponse<BasicError> contactUpdatedResponse;
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NotFound);
            Assert.IsTrue(response.TryGetContentValue<BaseResponse<BasicError>>(out contactUpdatedResponse));
            Assert.IsTrue(contactUpdatedResponse.IsError);
            Assert.AreEqual(contactUpdatedResponse.Errors.ErrorCode, ErrorCodes.DataNotFoundError.ToString());
        }

        [TestMethod]
        public void Put_Returns_Exception()
        {
            //Arrange
            _mockContactRepository.Setup(c => c.UpdateContact(It.IsAny<Contact>())).Throws(new Exception());

            //Act
            var response = _contactsController.Put(Requests.ContactRequest());

            //Assert
            BaseResponse<BasicError> contactUpdatedResponse;
            Assert.AreEqual(response.StatusCode, HttpStatusCode.InternalServerError);
            Assert.IsTrue(response.TryGetContentValue<BaseResponse<BasicError>>(out contactUpdatedResponse));
            Assert.IsTrue(contactUpdatedResponse.IsError);
            Assert.AreEqual(contactUpdatedResponse.Errors.ErrorCode, ErrorCodes.TechnicalError.ToString());
        }

        #endregion

        #region Test cases for Delete
        [TestMethod]
        public void Delete_Returns_Valid_Response()
        {
            //Arrange
            _mockContactRepository.Setup(c => c.ChangeContactStatus(It.IsAny<int>())).Returns(true);

            //Act
            var response = _contactsController.Delete(1);

            //Assert
            BaseResponse<bool> contactUpdatedResponse;
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            Assert.IsTrue(response.TryGetContentValue<BaseResponse<bool>>(out contactUpdatedResponse));
            Assert.IsFalse(contactUpdatedResponse.IsError);
            Assert.IsTrue(contactUpdatedResponse.Data);
        }

        [TestMethod]
        public void Delete_Returns_Failure_Response()
        {
            //Arrange
            _mockContactRepository.Setup(c => c.ChangeContactStatus(It.IsAny<int>())).Returns(false);

            //Act
            var response = _contactsController.Delete(1);

            //Assert
            BaseResponse<BasicError> contactUpdatedResponse;
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NotFound);
            Assert.IsTrue(response.TryGetContentValue<BaseResponse<BasicError>>(out contactUpdatedResponse));
            Assert.IsTrue(contactUpdatedResponse.IsError);
            Assert.AreEqual(contactUpdatedResponse.Errors.ErrorCode, ErrorCodes.DataNotFoundError.ToString());
        }

        [TestMethod]
        public void Delete_Returns_Exception()
        {
            //Arrange
            _mockContactRepository.Setup(c => c.ChangeContactStatus(It.IsAny<int>())).Throws(new Exception());

            //Act
            var response = _contactsController.Delete(1);

            //Assert
            BaseResponse<BasicError> contactUpdatedResponse;
            Assert.AreEqual(response.StatusCode, HttpStatusCode.InternalServerError);
            Assert.IsTrue(response.TryGetContentValue<BaseResponse<BasicError>>(out contactUpdatedResponse));
            Assert.IsTrue(contactUpdatedResponse.IsError);
            Assert.AreEqual(contactUpdatedResponse.Errors.ErrorCode, ErrorCodes.TechnicalError.ToString());
        }

        #endregion
    }
}
