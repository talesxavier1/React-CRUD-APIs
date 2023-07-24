using Microsoft.AspNetCore.Mvc;
using SingularChatAPIs.Models;
using SingularChatAPIs.Repository.Implementations;
using SingularChatAPIs.ResponseModels;
using SingularChatAPIs.ResponseModels.ENUMs;

namespace SingularChatAPIs.Controllers;

[ApiController]
[Route("Class")]
public class ClassController : Controller {

    [HttpPost]
    [Route("logicalDeleteClass")]
    public ActionResult<OperationResponseModel> logicalDeleteClass([FromHeader] String userToken, [FromQuery] string codigo) {

        OperationResponseModel response = new();
        UserRepository userRepository = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }
        UserModel user = userRepository.getUserByToken(userToken);

        Boolean operationResult = new ClassRepository().logicalDeleteClass(codigo, user);

        response.oparationStatus = operationResult ? Status.OK : Status.NOK;

        return StatusCode(200, response);
    }

    [HttpPost]
    [Route("addClass")]
    public ActionResult<OperationResponseModel> addClass([FromHeader] String userToken, [FromBody] ClassModel classModel) {
        OperationResponseModel response = new();
        UserRepository userRepository = new();

        if (!userRepository.validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        UserModel user = userRepository.getUserByToken(userToken);

        Boolean operationResult = new ClassRepository().addClass(classModel, user);

        response.oparationStatus = operationResult ? Status.OK : Status.NOK;
        response.message = operationResult ? "Registro Criado." : "Não foi possível criar registro.";
        response.data = classModel;

        return StatusCode(200, response);
    }

    [HttpPost]
    [Route("modifyClass")]
    public ActionResult<OperationResponseModel> modifyClass([FromHeader] String userToken, [FromBody] ClassModel classModel) {
        OperationResponseModel response = new();
        UserRepository userRepository = new();

        if (!userRepository.validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }
        UserModel user = userRepository.getUserByToken(userToken);

        new ClassRepository().updateClass(classModel, user);
        return StatusCode(200, response);
    }

    [HttpGet]
    [Route("getClassList")]
    public ActionResult<OperationResponseModel> getClassList([FromHeader] String userToken, [FromQuery] int skip, int take) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        List<ClassModel> result = new ClassRepository().getClassesList(skip, take);
        response.oparationStatus = Status.OK;
        response.message = "";
        response.data = result;

        return StatusCode(200, response);
    }

    [HttpPost]
    [Route("getClassByQuery")]
    public ActionResult<OperationResponseModel> getClassByQuery([FromHeader] String userToken, [FromBody] string queryB64, [FromQuery] int skip, [FromQuery] int take) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        try {
            byte[] valueBytes = System.Convert.FromBase64String(queryB64);
            string stringFilter = Uri.UnescapeDataString(System.Text.Encoding.UTF8.GetString(valueBytes));

            response.data = new ClassRepository().getClassByStringQuery(stringFilter, skip, take);
            response.oparationStatus = Status.OK;

            return StatusCode(200, response);
        } catch (Exception e) {
            response.oparationStatus = Status.NOK;
            response.message = e.ToString();
            return StatusCode(500, response);
        }
    }

    [HttpPost]
    [Route("countClassesByQuery")]
    public ActionResult<OperationResponseModel> countClassesByQuery([FromHeader] String userToken, [FromBody] string queryB64) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        try {
            byte[] valueBytes = System.Convert.FromBase64String(queryB64);
            string stringFilter = Uri.UnescapeDataString(System.Text.Encoding.UTF8.GetString(valueBytes));

            response.data = new ClassRepository().countClassesByQuery(stringFilter);
            response.oparationStatus = Status.OK;

            return StatusCode(200, response);
        } catch (Exception e) {
            response.oparationStatus = Status.NOK;
            response.message = e.ToString();
            return StatusCode(500, response);
        }
    }

    [HttpGet]
    [Route("countClasses")]
    public ActionResult<OperationResponseModel> countClasses([FromHeader] String userToken) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }
        long result = new ClassRepository().countClasses();
        response.oparationStatus = Status.OK;
        response.data = result;
        return Ok(response);
    }

    [HttpGet]
    [Route("getClssById")]
    public ActionResult<OperationResponseModel> getClassById([FromHeader] String userToken, [FromQuery] string codigo) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        ClassModel result = new ClassRepository().getClassById(codigo);
        response.oparationStatus = Status.OK;
        response.data = result;

        return StatusCode(200, response);
    }

}
