using Microsoft.AspNetCore.Mvc;
using SingularChatAPIs.Models;
using SingularChatAPIs.Repository.Implementations;
using SingularChatAPIs.ResponseModels;
using SingularChatAPIs.ResponseModels.ENUMs;

namespace SingularChatAPIs.Controllers;

[ApiController]
[Route("CurriculumPlan")]
public class CurriculumPlanController : Controller {

    [HttpPost]
    [Route("addCurriculumPlan")]
    public ActionResult<OperationResponseModel> addCurriculumPlan([FromHeader] string userToken, [FromBody] CurriculumPlanModel curriculumPlanModel) {
        UserRepository userRepository = new();
        OperationResponseModel response = new();

        if (!userRepository.validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }
        UserModel currentUser = userRepository.getUserByToken(userToken);


        Boolean result = new CurriculumPlanRepository().AddCurriculumPlan(curriculumPlanModel, currentUser);
        response.oparationStatus = result == true ? Status.OK : Status.NOK;
        response.data = curriculumPlanModel;

        return Ok(response);
    }

    [HttpGet]
    [Route("countCurriculumPlans")]
    public ActionResult<OperationResponseModel> countCurriculumPlans([FromHeader] string userToken, [FromQuery] string? codigoRef) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        long result = new CurriculumPlanRepository().count(codigoRef);
        response.data = result;
        response.oparationStatus = Status.OK;

        return Ok(response);
    }

    [HttpPost]
    [Route("countCurriculumPlansByQuery")]
    public ActionResult<OperationResponseModel> countCurriculumPlansByQuery([FromHeader] string userToken, [FromBody] string query, [FromQuery] string? codigoRef) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        try {
            byte[] valueBytes = System.Convert.FromBase64String(query);
            string stringFilter = Uri.UnescapeDataString(System.Text.Encoding.UTF8.GetString(valueBytes));

            long result = new CurriculumPlanRepository().count(stringFilter, codigoRef);
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
    [Route("getCurriculumPlanById")]
    public ActionResult<OperationResponseModel> getCurriculumPlanById([FromHeader] string userToken, [FromQuery] string codigo) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        var result = new CurriculumPlanRepository().getCurriculumPlanById(codigo);
        response.data = result;
        response.oparationStatus = Status.OK;
        return Ok(response);
    }

    [HttpGet]
    [Route("getCurriculumPlans")]
    public ActionResult<OperationResponseModel> getCurriculumPlans([FromHeader] string userToken, [FromQuery] int skip, [FromQuery] int take, [FromQuery] string? codigoRef) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        List<CurriculumPlanModel> result = new CurriculumPlanRepository().getCurriculumPlans(skip, take, codigoRef);
        response.data = result;
        response.oparationStatus = Status.OK;
        return Ok(response);
    }

    [HttpPost]
    [Route("getCurriculumPlansByQuery")]
    public ActionResult<OperationResponseModel> getCurriculumPlansByQuery([FromHeader] string userToken, [FromQuery] int skip, [FromQuery] int take, [FromBody] string query, [FromQuery] string? codigoRef) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        try {
            byte[] valueBytes = System.Convert.FromBase64String(query);
            string stringFilter = Uri.UnescapeDataString(System.Text.Encoding.UTF8.GetString(valueBytes));

            List<CurriculumPlanModel> result = new CurriculumPlanRepository().getCurriculumPlans(skip, take, stringFilter, codigoRef);
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
    [Route("logicalDeleteCurriculumPlan")]
    public ActionResult<OperationResponseModel> logicalDeleteCurriculumPlan([FromHeader] string userToken, [FromQuery] string[] codigos) {
        UserRepository userRepository = new();
        OperationResponseModel response = new();

        if (!userRepository.validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }
        UserModel currentUser = userRepository.getUserByToken(userToken);

        Boolean result = new CurriculumPlanRepository().logicalDeleteCurriculumPlan(codigos, currentUser);
        response.oparationStatus = result == true ? Status.OK : Status.NOK;

        return Ok(response);
    }

    [HttpPost]
    [Route("modifyCurriculumPlan")]
    public ActionResult<OperationResponseModel> modifyCurriculumPlan([FromHeader] string userToken, [FromBody] CurriculumPlanModel curriculumPlanModel) {
        UserRepository userRepository = new();
        OperationResponseModel response = new();

        if (!userRepository.validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }
        UserModel currentUser = userRepository.getUserByToken(userToken);

        Boolean result = new CurriculumPlanRepository().updateCurriculumPlan(curriculumPlanModel, currentUser);
        response.oparationStatus = result == true ? Status.OK : Status.NOK;
        response.data = curriculumPlanModel;
        return Ok(response);
    }
}
