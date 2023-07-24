using Microsoft.AspNetCore.Mvc;
using SingularChatAPIs.Models;
using SingularChatAPIs.Repository.Implementations;
using SingularChatAPIs.ResponseModels;
using SingularChatAPIs.ResponseModels.ENUMs;

namespace SingularChatAPIs.Controllers;
[ApiController]
[Route("ClassKnowledgeArea")]
public class ClassKnowledgeAreaController : Controller {

    [HttpPost]
    [Route("addClassKnowledgeArea")]
    public ActionResult<OperationResponseModel> addClassKnowledgeArea([FromHeader] string userToken, [FromBody] ClassKnowledgeAreaModel classKnowledgeAreaModel) {
        UserRepository userRepository = new();
        OperationResponseModel response = new();

        if (!userRepository.validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }
        UserModel currentUser = userRepository.getUserByToken(userToken);


        Boolean result = new ClassKnowledgeAreaRepository().addClassKnowledgeArea(classKnowledgeAreaModel, currentUser);
        response.oparationStatus = result == true ? Status.OK : Status.NOK;
        response.data = classKnowledgeAreaModel;

        return Ok(response);
    }

    [HttpGet]
    [Route("countClassKnowledgeAreas")]
    public ActionResult<OperationResponseModel> countClassKnowledgeAreas([FromHeader] string userToken) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        long result = new ClassKnowledgeAreaRepository().count();
        response.data = result;
        response.oparationStatus = Status.OK;

        return Ok(response);
    }

    [HttpPost]
    [Route("countClassKnowledgeAreasByQuery")]
    public ActionResult<OperationResponseModel> countClassKnowledgeAreasByQuery([FromHeader] string userToken, [FromBody] string query) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        try {
            byte[] valueBytes = System.Convert.FromBase64String(query);
            string stringFilter = Uri.UnescapeDataString(System.Text.Encoding.UTF8.GetString(valueBytes));

            long result = new ClassKnowledgeAreaRepository().count(stringFilter);
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
    [Route("getClassKnowledgeAreaById")]
    public ActionResult<OperationResponseModel> getClassKnowledgeAreaById([FromHeader] string userToken, [FromQuery] string codigo) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        var result = new ClassKnowledgeAreaRepository().getClassKnowledgeAreaById(codigo);
        response.data = result;
        response.oparationStatus = Status.OK;
        return Ok(response);
    }

    [HttpGet]
    [Route("getClassKnowledgeAreas")]
    public ActionResult<OperationResponseModel> getClassKnowledgeAreas([FromHeader] string userToken, [FromQuery] int skip, [FromQuery] int take) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        List<ClassKnowledgeAreaModel> result = new ClassKnowledgeAreaRepository().getClassKnowledgeAreas(skip, take);
        response.data = result;
        response.oparationStatus = Status.OK;
        return Ok(response);
    }

    [HttpPost]
    [Route("getClassKnowledgeAreasByQuery")]
    public ActionResult<OperationResponseModel> getClassKnowledgeAreasByQuery([FromHeader] string userToken, [FromQuery] int skip, [FromQuery] int take, [FromBody] string query) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        try {
            byte[] valueBytes = System.Convert.FromBase64String(query);
            string stringFilter = Uri.UnescapeDataString(System.Text.Encoding.UTF8.GetString(valueBytes));

            List<ClassKnowledgeAreaModel> result = new ClassKnowledgeAreaRepository().getClassKnowledgeAreas(skip, take, stringFilter);
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
    [Route("logicalDeleteClassKnowledgeArea")]
    public ActionResult<OperationResponseModel> logicalDeleteClassKnowledgeArea([FromHeader] string userToken, [FromQuery] string[] codigos) {
        UserRepository userRepository = new();
        OperationResponseModel response = new();

        if (!userRepository.validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }
        UserModel currentUser = userRepository.getUserByToken(userToken);

        Boolean result = new ClassKnowledgeAreaRepository().logicalDeleteClassKnowledgeArea(codigos, currentUser);
        response.oparationStatus = result == true ? Status.OK : Status.NOK;

        return Ok(response);
    }

    [HttpPost]
    [Route("modifyClassKnowledgeArea")]
    public ActionResult<OperationResponseModel> modifyClassKnowledgeArea([FromHeader] string userToken, [FromBody] ClassKnowledgeAreaModel classKnowledgeAreaModel) {
        UserRepository userRepository = new();
        OperationResponseModel response = new();

        if (!userRepository.validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }
        UserModel currentUser = userRepository.getUserByToken(userToken);

        Boolean result = new ClassKnowledgeAreaRepository().updateClassKnowledgeArea(classKnowledgeAreaModel, currentUser);
        response.oparationStatus = result == true ? Status.OK : Status.NOK;
        response.data = classKnowledgeAreaModel;
        return Ok(response);
    }
}
