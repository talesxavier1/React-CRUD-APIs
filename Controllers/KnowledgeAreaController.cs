using Microsoft.AspNetCore.Mvc;
using SingularChatAPIs.Models;
using SingularChatAPIs.Repository.Implementations;
using SingularChatAPIs.ResponseModels;
using SingularChatAPIs.ResponseModels.ENUMs;

namespace SingularChatAPIs.Controllers;
[ApiController]
[Route("KnowledgeArea")]
public class KnowledgeAreaController : Controller {

    [HttpPost]
    [Route("addKnowledgeArea")]
    public ActionResult<OperationResponseModel> addKnowledgeArea([FromHeader] string userToken, [FromBody] KnowledgeAreaModel classKnowledgeAreaModel) {
        UserRepository userRepository = new();
        OperationResponseModel response = new();

        if (!userRepository.validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }
        UserModel currentUser = userRepository.getUserByToken(userToken);


        Boolean result = new KnowledgeAreaRepository().addKnowledgeArea(classKnowledgeAreaModel, currentUser);
        response.oparationStatus = result == true ? Status.OK : Status.NOK;
        response.data = classKnowledgeAreaModel;

        return Ok(response);
    }

    [HttpGet]
    [Route("countKnowledgeAreas")]
    public ActionResult<OperationResponseModel> countKnowledgeAreas([FromHeader] string userToken) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        long result = new KnowledgeAreaRepository().count();
        response.data = result;
        response.oparationStatus = Status.OK;

        return Ok(response);
    }

    [HttpPost]
    [Route("countKnowledgeAreasByQuery")]
    public ActionResult<OperationResponseModel> countKnowledgeAreasByQuery([FromHeader] string userToken, [FromBody] string query) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        try {
            byte[] valueBytes = System.Convert.FromBase64String(query);
            string stringFilter = Uri.UnescapeDataString(System.Text.Encoding.UTF8.GetString(valueBytes));

            long result = new KnowledgeAreaRepository().count(stringFilter);
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
    [Route("getKnowledgeAreaById")]
    public ActionResult<OperationResponseModel> getKnowledgeAreaById([FromHeader] string userToken, [FromQuery] string codigo) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        var result = new KnowledgeAreaRepository().getKnowledgeAreaById(codigo);
        response.data = result;
        response.oparationStatus = Status.OK;
        return Ok(response);
    }

    [HttpGet]
    [Route("getKnowledgeAreas")]
    public ActionResult<OperationResponseModel> getKnowledgeAreas([FromHeader] string userToken, [FromQuery] int skip, [FromQuery] int take) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        List<KnowledgeAreaModel> result = new KnowledgeAreaRepository().getKnowledgeAreas(skip, take);
        response.data = result;
        response.oparationStatus = Status.OK;
        return Ok(response);
    }

    [HttpPost]
    [Route("getKnowledgeAreasByQuery")]
    public ActionResult<OperationResponseModel> getKnowledgeAreasByQuery([FromHeader] string userToken, [FromQuery] int skip, [FromQuery] int take, [FromBody] string query) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        try {
            byte[] valueBytes = System.Convert.FromBase64String(query);
            string stringFilter = Uri.UnescapeDataString(System.Text.Encoding.UTF8.GetString(valueBytes));

            List<KnowledgeAreaModel> result = new KnowledgeAreaRepository().getKnowledgeAreas(skip, take, stringFilter);
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
    [Route("logicalDeleteKnowledgeArea")]
    public ActionResult<OperationResponseModel> logicalDeleteKnowledgeArea([FromHeader] string userToken, [FromQuery] string[] codigos) {
        UserRepository userRepository = new();
        OperationResponseModel response = new();

        if (!userRepository.validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }
        UserModel currentUser = userRepository.getUserByToken(userToken);

        Boolean result = new KnowledgeAreaRepository().logicalDeleteKnowledgeArea(codigos, currentUser);
        response.oparationStatus = result == true ? Status.OK : Status.NOK;

        return Ok(response);
    }

    [HttpPost]
    [Route("modifyKnowledgeArea")]
    public ActionResult<OperationResponseModel> modifyKnowledgeArea([FromHeader] string userToken, [FromBody] KnowledgeAreaModel classKnowledgeAreaModel) {
        UserRepository userRepository = new();
        OperationResponseModel response = new();

        if (!userRepository.validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }
        UserModel currentUser = userRepository.getUserByToken(userToken);

        Boolean result = new KnowledgeAreaRepository().updateKnowledgeArea(classKnowledgeAreaModel, currentUser);
        response.oparationStatus = result == true ? Status.OK : Status.NOK;
        response.data = classKnowledgeAreaModel;
        return Ok(response);
    }
}
