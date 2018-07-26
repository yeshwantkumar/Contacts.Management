using Contacts.Management.Api.DataAccess.Interfaces;
using Contacts.Management.Api.Models;
using Contacts.Management.Api.Models.ErrorResponses;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Contacts.Management.Api.Controllers
{
    public class ContactsController : ApiController
    {
        private readonly IContactsRepository _contactRepository;

        public ContactsController(IContactsRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        //GET api/contacts
        public HttpResponseMessage Get()
        {
            try
            {
                BaseResponse<List<Contact>> response = new BaseResponse<List<Contact>>();
                response.Data = _contactRepository.GetContacts();
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                BaseResponse<BasicError> response = new BaseResponse<BasicError>();
                response.Errors = new BasicError(ErrorCodes.TechnicalError.ToString(), ex.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, response);
            }
        }

        //POST api/contacts
        public HttpResponseMessage Post([FromBody]Contact contact)
        {
            try
            {
                BaseResponse<bool> response = new BaseResponse<bool>();
                response.Data = _contactRepository.AddContact(contact);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                BaseResponse<BasicError> response = new BaseResponse<BasicError>();
                response.Errors = new BasicError(ErrorCodes.TechnicalError.ToString(), ex.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, response);
            }
        }


        //PUT api/contacts
        public HttpResponseMessage Put([FromBody]Contact contact)
        {
            try
            {
                bool isContactUpdated = _contactRepository.UpdateContact(contact);
                if (isContactUpdated)
                {
                    BaseResponse<bool> response = new BaseResponse<bool>();
                    response.Data = isContactUpdated;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                else
                {
                    BaseResponse<BasicError> response = new BaseResponse<BasicError>();
                    response.Errors = new BasicError(ErrorCodes.DataNotFoundError.ToString(), "Contact not found");
                    return Request.CreateResponse(HttpStatusCode.NotFound, response);
                }
            }
            catch (Exception ex)
            {
                BaseResponse<BasicError> response = new BaseResponse<BasicError>();
                response.Errors = new BasicError(ErrorCodes.TechnicalError.ToString(), ex.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, response);
            }
        }

        //DELETE api/contacts/2
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                bool isContactUpdated = _contactRepository.ChangeContactStatus(id);
                if (isContactUpdated)
                {
                    BaseResponse<bool> response = new BaseResponse<bool>();
                    response.Data = isContactUpdated;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                else
                {
                    BaseResponse<BasicError> response = new BaseResponse<BasicError>();
                    response.Errors = new BasicError(ErrorCodes.DataNotFoundError.ToString(), "Contact not found");
                    return Request.CreateResponse(HttpStatusCode.NotFound, response);
                }
            }
            catch (Exception ex)
            {
                BaseResponse<BasicError> response = new BaseResponse<BasicError>();
                response.Errors = new BasicError(ErrorCodes.TechnicalError.ToString(), ex.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, response);
            }
        }

    }
}
