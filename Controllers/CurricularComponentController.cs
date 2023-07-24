using Microsoft.AspNetCore.Mvc;
using SingularChatAPIs.Models;
using SingularChatAPIs.Repository.Implementations;
using SingularChatAPIs.ResponseModels;
using SingularChatAPIs.ResponseModels.ENUMs;

namespace SingularChatAPIs.Controllers;

[ApiController]
[Route("CurricularComponent")]
public class CurricularComponentController : Controller {

    [HttpPost]
    [Route("addCurricularComponent")]
    public ActionResult<OperationResponseModel> addCurricularComponent([FromHeader] string userToken, [FromBody] CurricularComponentModel curricularComponentModel) {
        UserRepository userRepository = new();
        OperationResponseModel response = new();

        if (!userRepository.validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }
        UserModel currentUser = userRepository.getUserByToken(userToken);


        Boolean result = new CurricularComponentRepository().addCurricularComponent(curricularComponentModel, currentUser);
        response.oparationStatus = result == true ? Status.OK : Status.NOK;
        response.data = curricularComponentModel;

        return Ok(response);
    }

    [HttpGet]
    [Route("countCurricularComponents")]
    public ActionResult<OperationResponseModel> countCurricularComponents([FromHeader] string userToken) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        long result = new CurricularComponentRepository().count();
        response.data = result;
        response.oparationStatus = Status.OK;

        return Ok(response);
    }

    [HttpPost]
    [Route("countCurricularComponentsByQuery")]
    public ActionResult<OperationResponseModel> countCurricularComponentsByQuery([FromHeader] string userToken, [FromBody] string query) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        try {
            byte[] valueBytes = System.Convert.FromBase64String(query);
            string stringFilter = Uri.UnescapeDataString(System.Text.Encoding.UTF8.GetString(valueBytes));

            long result = new CurricularComponentRepository().count(stringFilter);
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
    [Route("getCurricularComponentById")]
    public ActionResult<OperationResponseModel> getCurricularComponentById([FromHeader] string userToken, [FromQuery] string codigo) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        var result = new CurricularComponentRepository().getCurricularComponentById(codigo);
        response.data = result;
        response.oparationStatus = Status.OK;
        return Ok(response);
    }

    [HttpGet]
    [Route("getCurricularComponents")]
    public ActionResult<OperationResponseModel> getCurricularComponents([FromHeader] string userToken, [FromQuery] int skip, [FromQuery] int take) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        List<CurricularComponentModel> result = new CurricularComponentRepository().getCurricularComponents(skip, take);
        response.data = result;
        response.oparationStatus = Status.OK;
        return Ok(response);
    }

    [HttpPost]
    [Route("getCurricularComponentsByQuery")]
    public ActionResult<OperationResponseModel> getCurricularComponentsByQuery([FromHeader] string userToken, [FromQuery] int skip, [FromQuery] int take, [FromBody] string query) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        try {
            byte[] valueBytes = System.Convert.FromBase64String(query);
            string stringFilter = Uri.UnescapeDataString(System.Text.Encoding.UTF8.GetString(valueBytes));

            List<CurricularComponentModel> result = new CurricularComponentRepository().getCurricularComponents(skip, take, stringFilter);
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
    [Route("logicalDeleteCurricularComponent")]
    public ActionResult<OperationResponseModel> logicalDeleteCurricularComponent([FromHeader] string userToken, [FromQuery] string[] codigos) {
        UserRepository userRepository = new();
        OperationResponseModel response = new();

        if (!userRepository.validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }
        UserModel currentUser = userRepository.getUserByToken(userToken);

        Boolean result = new CurricularComponentRepository().logicalDeleteCurricularComponent(codigos, currentUser);
        response.oparationStatus = result == true ? Status.OK : Status.NOK;

        return Ok(response);
    }

    [HttpPost]
    [Route("modifyCurricularComponent")]
    public ActionResult<OperationResponseModel> modifyCurricularComponent([FromHeader] string userToken, [FromBody] CurricularComponentModel curricularComponentModel) {
        UserRepository userRepository = new();
        OperationResponseModel response = new();

        if (!userRepository.validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }
        UserModel currentUser = userRepository.getUserByToken(userToken);

        Boolean result = new CurricularComponentRepository().updateCurricularComponent(curricularComponentModel, currentUser);
        response.oparationStatus = result == true ? Status.OK : Status.NOK;
        response.data = curricularComponentModel;
        return Ok(response);
    }
}
