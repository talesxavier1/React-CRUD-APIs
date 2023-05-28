using Microsoft.AspNetCore.Mvc;
using SingularChatAPIs.Models;
using SingularChatAPIs.Repository.Implementations;
using SingularChatAPIs.ResponseModels;
using SingularChatAPIs.ResponseModels.ENUMs;
using SingularChatAPIs.utils;

namespace SingularChatAPIs.Controllers;
[ApiController]
[Route("contactPerson")]
public class ContactPersonController : Controller {

    [HttpGet]
    [Route("getContacts")]
    public ActionResult<OperationResponseModel> getContacts([FromHeader] string userToken, [FromQuery] string? codigoRef, [FromQuery] int skip, [FromQuery] int take) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        List<ContactModel> contacts = new ContactPersonRepository().getContact(codigoRef!, skip, take);
        response.oparationStatus = Status.OK;
        response.data = contacts;
        return Ok(response);
    }

    [HttpPost]
    [Route("addContact")]
    public ActionResult<OperationResponseModel> addAddress([FromHeader] string userToken, [FromBody] ContactModel contact) {
        UserRepository userRepository = new();
        OperationResponseModel response = new();

        if (!userRepository.validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }
        UserModel currentUser = userRepository.getUserByToken(userToken);

        Boolean result = new ContactPersonRepository().addContact(contact, currentUser);
        response.oparationStatus = result == true ? Status.OK : Status.NOK;
        response.data = contact;

        return Ok(response);
    }

    [HttpPost]
    [Route("logicallyDeleteContact")]
    public ActionResult<OperationResponseModel> logicallyDeleteContact([FromHeader] string userToken, [FromQuery] string[] codigos) {
        UserRepository userRepository = new();
        OperationResponseModel response = new();

        if (!userRepository.validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }
        UserModel currentUser = userRepository.getUserByToken(userToken);

        Boolean result = new ContactPersonRepository().logicalDeleteContact(codigos, currentUser);
        response.oparationStatus = result == true ? Status.OK : Status.NOK;

        return Ok(response);
    }

    [HttpPost]
    [Route("deleteContact")]
    public ActionResult<OperationResponseModel> deleteContact([FromHeader] string AdminAccessToken, [FromQuery] string codigos) {
        OperationResponseModel response = new();

        if (AppSettings.appSetting["Admin_AccessToken"] != AdminAccessToken) {
            response.oparationStatus = Status.NOK;
            response.message = "Admin Access Token Inválido.";
            return StatusCode(401, response);
        }

        Boolean result = new ContactPersonRepository().deleteContact(codigos);
        response.oparationStatus = result == true ? Status.OK : Status.NOK;
        return Ok(response);
    }

    [HttpPost]
    [Route("modifyContact")]
    public ActionResult<OperationResponseModel> modifyContact([FromHeader] string userToken, [FromBody] ContactModel contact) {
        UserRepository userRepository = new();
        OperationResponseModel response = new();

        if (!userRepository.validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }
        UserModel currentUser = userRepository.getUserByToken(userToken);

        Boolean result = new ContactPersonRepository().updateContact(contact, currentUser);
        response.oparationStatus = result == true ? Status.OK : Status.NOK;
        response.data = contact;
        return Ok(response);
    }

    [HttpGet]
    [Route("getContactById")]
    public ActionResult<OperationResponseModel> getContactById([FromHeader] string userToken, [FromQuery] string codigo) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        ContactModel result = new ContactPersonRepository().getContactById(codigo);
        response.data = result;
        response.oparationStatus = result != null ? Status.OK : Status.NOK;

        return Ok(response);
    }

    [HttpGet]
    [Route("countContacts")]
    public ActionResult<OperationResponseModel> countContacts([FromHeader] string userToken, [FromQuery] string? codigoRef) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        long result = new ContactPersonRepository().count(codigoRef);
        response.data = result;
        response.oparationStatus = Status.OK;

        return Ok(response);
    }
}

