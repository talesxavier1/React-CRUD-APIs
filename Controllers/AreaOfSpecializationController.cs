using Microsoft.AspNetCore.Mvc;
using SingularChatAPIs.Models;
using SingularChatAPIs.Repository.Implementations;
using SingularChatAPIs.ResponseModels;
using SingularChatAPIs.ResponseModels.ENUMs;

namespace SingularChatAPIs.Controllers;

[ApiController]
[Route("areaOfSpecialization")]
public class AreaOfSpecializationController : Controller {

    [HttpPost]
    [Route("addAreaOfSpecialization")]
    public ActionResult<OperationResponseModel> addAreaOfSpecialization([FromHeader] string userToken, AreaOfSpecializationModel areaOfSpecializationModel) {
        UserRepository userRepository = new();
        OperationResponseModel response = new();

        if (!userRepository.validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }
        UserModel currentUser = userRepository.getUserByToken(userToken);


        Boolean result = new AreaOfSpecializationRepository().addAreaOfSpecialization(areaOfSpecializationModel, currentUser);
        response.oparationStatus = result == true ? Status.OK : Status.NOK;
        response.data = areaOfSpecializationModel;

        return Ok(response);
    }

    [HttpGet]
    [Route("countAreaOfSpecialization")]
    public ActionResult<OperationResponseModel> countAreaOfSpecialization([FromHeader] string userToken) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        long result = new AreaOfSpecializationRepository().count();
        response.data = result;
        response.oparationStatus = Status.OK;

        return Ok(response);
    }

    [HttpPost]
    [Route("countAreaOfSpecializationByQuery")]
    public ActionResult<OperationResponseModel> countAreaOfSpecializationByQuery([FromHeader] string userToken, [FromBody] string query) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        long result = new AreaOfSpecializationRepository().count(query);
        response.data = result;
        response.oparationStatus = Status.OK;

        return Ok(response);
    }

    [HttpGet]
    [Route("getAreaOfSpecializationById")]
    public ActionResult<OperationResponseModel> getAreaOfSpecializationById([FromHeader] string userToken, [FromQuery] string codigo) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        var result = new AreaOfSpecializationRepository().getAreasOfSpecializationById(codigo);
        response.data = result;
        response.oparationStatus = Status.OK;
        return Ok(response);
    }

    [HttpGet]
    [Route("getAreasOfSpecialization")]
    public ActionResult<OperationResponseModel> getAreasOfSpecialization([FromHeader] string userToken, [FromQuery] int skip, [FromQuery] int take) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        List<AreaOfSpecializationModel> result = new AreaOfSpecializationRepository().getAreasOfSpecialization(skip, take);
        response.data = result;
        response.oparationStatus = Status.OK;
        return Ok(response);
    }

    [HttpPost]
    [Route("getAreasOfSpecializationByQuery")]
    public ActionResult<OperationResponseModel> getAreasOfSpecializationByQuery([FromHeader] string userToken, [FromQuery] int skip, [FromQuery] int take, [FromBody] string query) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        List<AreaOfSpecializationModel> result = new AreaOfSpecializationRepository().getAreasOfSpecialization(skip, take, query);
        response.data = result;
        response.oparationStatus = Status.OK;
        return Ok(response);
    }


    [HttpPost]
    [Route("logicalDeleteAreaOfSpecialization")]
    public ActionResult<OperationResponseModel> logicalDeleteAreaOfSpecialization([FromHeader] string userToken, [FromQuery] string[] codigos) {
        UserRepository userRepository = new();
        OperationResponseModel response = new();

        if (!userRepository.validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }
        UserModel currentUser = userRepository.getUserByToken(userToken);

        Boolean result = new AreaOfSpecializationRepository().logicalDeleteAreaOfSpecialization(codigos, currentUser);
        response.oparationStatus = result == true ? Status.OK : Status.NOK;

        return Ok(response);
    }

    [HttpPost]
    [Route("modifyAreaOfSpecialization")]
    public ActionResult<OperationResponseModel> modifyAreaOfSpecialization([FromHeader] string userToken, AreaOfSpecializationModel areaOfSpecializationModel) {
        UserRepository userRepository = new();
        OperationResponseModel response = new();

        if (!userRepository.validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }
        UserModel currentUser = userRepository.getUserByToken(userToken);

        Boolean result = new AreaOfSpecializationRepository().updateAreaOfSpecialization(areaOfSpecializationModel, currentUser);
        response.oparationStatus = result == true ? Status.OK : Status.NOK;
        response.data = areaOfSpecializationModel;
        return Ok(response);
    }
}

