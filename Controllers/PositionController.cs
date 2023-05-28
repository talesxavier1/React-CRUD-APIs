using Microsoft.AspNetCore.Mvc;
using SingularChatAPIs.Models;
using SingularChatAPIs.Repository.Implementations;
using SingularChatAPIs.ResponseModels;
using SingularChatAPIs.ResponseModels.ENUMs;

namespace SingularChatAPIs.Controllers;

[ApiController]
[Route("position")]
public class PositionController : Controller {

    [HttpPost]
    [Route("addPosition")]
    public ActionResult<OperationResponseModel> addPosition([FromHeader] string userToken, PositionModel positionModel) {
        UserRepository userRepository = new();
        OperationResponseModel response = new();

        if (!userRepository.validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }
        UserModel currentUser = userRepository.getUserByToken(userToken);


        Boolean result = new PositionRepository().addPosition(positionModel, currentUser);
        response.oparationStatus = result == true ? Status.OK : Status.NOK;
        response.data = positionModel;

        return Ok(response);
    }

    [HttpGet]
    [Route("countPositions")]
    public ActionResult<OperationResponseModel> countPositions([FromHeader] string userToken) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        long result = new PositionRepository().count();
        response.data = result;
        response.oparationStatus = Status.OK;

        return Ok(response);
    }

    [HttpPost]
    [Route("countPositionsByQuery")]
    public ActionResult<OperationResponseModel> countPositionsByQuery([FromHeader] string userToken, [FromBody] string query) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }
        try {
            byte[] valueBytes = System.Convert.FromBase64String(query);
            string stringFilter = Uri.UnescapeDataString(System.Text.Encoding.UTF8.GetString(valueBytes));

            long result = new PositionRepository().count(stringFilter);
            response.data = result;
            response.oparationStatus = Status.OK;

            return Ok(response);
        } catch (Exception e) {
            response.oparationStatus = Status.NOK;
            response.message = e.ToString();
            return StatusCode(500, response);
        }
    }

    [HttpGet]
    [Route("getPositionById")]
    public ActionResult<OperationResponseModel> getPositionById([FromHeader] string userToken, [FromQuery] string codigo) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        var result = new PositionRepository().getPositionById(codigo);
        response.data = result;
        response.oparationStatus = Status.OK;
        return Ok(response);
    }

    [HttpGet]
    [Route("getPositions")]
    public ActionResult<OperationResponseModel> getPositions([FromHeader] string userToken, [FromQuery] int skip, [FromQuery] int take) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        var result = new PositionRepository().getPositions(skip, take);
        response.data = result;
        response.oparationStatus = Status.OK;
        return Ok(response);
    }

    [HttpPost]
    [Route("getPositionsByQuery")]
    public ActionResult<OperationResponseModel> getPositionsByQuery([FromHeader] string userToken, [FromQuery] int skip, [FromQuery] int take, [FromBody] string query) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        try {
            byte[] valueBytes = System.Convert.FromBase64String(query);
            string stringFilter = Uri.UnescapeDataString(System.Text.Encoding.UTF8.GetString(valueBytes));

            var result = new PositionRepository().getPositions(skip, take, stringFilter);
            response.data = result;
            response.oparationStatus = Status.OK;
            return Ok(response);
        } catch (Exception e) {
            response.oparationStatus = Status.NOK;
            response.message = e.ToString();
            return StatusCode(500, response);
        }
    }

    [HttpPost]
    [Route("logicalDeletePosition")]
    public ActionResult<OperationResponseModel> logicalDeletePosition([FromHeader] string userToken, [FromQuery] string[] codigos) {
        UserRepository userRepository = new();
        OperationResponseModel response = new();

        if (!userRepository.validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }
        UserModel currentUser = userRepository.getUserByToken(userToken);

        Boolean result = new PositionRepository().logicalDeletePosition(codigos, currentUser);
        response.oparationStatus = result == true ? Status.OK : Status.NOK;

        return Ok(response);
    }

    [HttpPost]
    [Route("modifyPosition")]
    public ActionResult<OperationResponseModel> modifyPosition([FromHeader] string userToken, PositionModel positionModel) {
        UserRepository userRepository = new();
        OperationResponseModel response = new();

        if (!userRepository.validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }
        UserModel currentUser = userRepository.getUserByToken(userToken);

        Boolean result = new PositionRepository().updatePosition(positionModel, currentUser);
        response.oparationStatus = result == true ? Status.OK : Status.NOK;
        response.data = positionModel;
        return Ok(response);
    }

}

