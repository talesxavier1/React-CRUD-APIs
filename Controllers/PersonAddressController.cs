using Microsoft.AspNetCore.Mvc;
using SingularChatAPIs.Models;
using SingularChatAPIs.Repository.Implementations;
using SingularChatAPIs.ResponseModels;
using SingularChatAPIs.ResponseModels.ENUMs;
using SingularChatAPIs.utils;

namespace SingularChatAPIs.Controllers;
[ApiController]
[Route("personAddress")]
public class PersonAddressController : Controller {

    [HttpGet]
    [Route("getAddresses")]
    public ActionResult<OperationResponseModel> getAddressList([FromHeader] string userToken, [FromQuery] string? codigoRef, [FromQuery] int skip, [FromQuery] int take) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        List<AddressModel> addresses = new PersonAddressRepository().getAddress(codigoRef!, skip, take);
        response.oparationStatus = Status.OK;
        response.data = addresses;
        return Ok(response);
    }

    [HttpPost]
    [Route("addAddress")]
    public ActionResult<OperationResponseModel> addAddress([FromHeader] string userToken, [FromBody] AddressModel address) {
        UserRepository userRepository = new();
        OperationResponseModel response = new();

        if (!userRepository.validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }
        UserModel currentUser = userRepository.getUserByToken(userToken);

        Boolean result = new PersonAddressRepository().addAddress(address, currentUser);
        response.oparationStatus = result == true ? Status.OK : Status.NOK;
        response.data = address;

        return Ok(response);
    }

    [HttpPost]
    [Route("logicallyDeleteAddress")]
    public ActionResult<OperationResponseModel> logicallyDeleteAddress([FromHeader] string userToken, [FromQuery] string[] codigo) {
        UserRepository userRepository = new();
        OperationResponseModel response = new();

        if (!userRepository.validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }
        UserModel currentUser = userRepository.getUserByToken(userToken);

        Boolean result = new PersonAddressRepository().logicalDeleteAddress(codigo, currentUser);
        response.oparationStatus = result == true ? Status.OK : Status.NOK;

        return Ok(response);
    }

    [HttpPost]
    [Route("deleteAddress")]
    public ActionResult<OperationResponseModel> deleteAddress([FromHeader] string AdminAccessToken, [FromQuery] string codigo) {
        OperationResponseModel response = new();

        if (AppSettings.appSetting["Admin_AccessToken"] != AdminAccessToken) {
            response.oparationStatus = Status.NOK;
            response.message = "Admin Access Token Inválido.";
            return StatusCode(401, response);
        }

        Boolean result = new PersonAddressRepository().deleteAddress(codigo);
        response.oparationStatus = result == true ? Status.OK : Status.NOK;
        return Ok(response);
    }

    [HttpPost]
    [Route("modifyAddress")]
    public ActionResult<OperationResponseModel> modifyAddress([FromHeader] string userToken, [FromBody] AddressModel address) {
        UserRepository userRepository = new();
        OperationResponseModel response = new();

        if (!userRepository.validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }
        UserModel currentUser = userRepository.getUserByToken(userToken);

        Boolean result = new PersonAddressRepository().updateAddress(address, currentUser);
        response.oparationStatus = result == true ? Status.OK : Status.NOK;
        response.data = address;
        return Ok(response);
    }

    [HttpGet]
    [Route("getAddressById")]
    public ActionResult<OperationResponseModel> getAddressById([FromHeader] string userToken, [FromQuery] string codigo) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        AddressModel result = new PersonAddressRepository().getAddressById(codigo);
        response.oparationStatus = result != null ? Status.OK : Status.NOK;
        response.data = result!;
        return Ok(response);
    }

    [HttpGet]
    [Route("countAddresses")]
    public ActionResult<OperationResponseModel> countAddresses([FromHeader] string userToken, [FromQuery] string? codigoRef) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        long count = new PersonAddressRepository().count(codigoRef!);
        response.oparationStatus = Status.OK;
        response.data = count;
        return Ok(response);
    }
}

