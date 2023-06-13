using Microsoft.AspNetCore.Mvc;
using SingularChatAPIs.Models;
using SingularChatAPIs.Repository.Implementations;
using SingularChatAPIs.ResponseModels;
using SingularChatAPIs.ResponseModels.ENUMs;

namespace SingularChatAPIs.Controllers;

[ApiController]
[Route("WorkExperience")]
public class WorkExperienceController : Controller {

    [HttpPost]
    [Route("logicalDeleteWorkEperience")]
    public ActionResult<OperationResponseModel> logicalDeleteWorkEperience([FromHeader] String userToken, [FromQuery] string codigo) {

        OperationResponseModel response = new();
        UserRepository userRepository = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }
        UserModel user = userRepository.getUserByToken(userToken);

        Boolean operationResult = new WorkExperienceRepository().logicalDeleteWorkExperience(codigo, user);

        response.oparationStatus = operationResult ? Status.OK : Status.NOK;

        return StatusCode(200, response);
    }

    [HttpPost]
    [Route("addWorkExperience")]
    public ActionResult<OperationResponseModel> addWorkExperience([FromHeader] String userToken, WorkExperienceModel workExperience) {
        OperationResponseModel response = new();
        UserRepository userRepository = new();

        if (!userRepository.validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        UserModel user = userRepository.getUserByToken(userToken);

        Boolean operationResult = new WorkExperienceRepository().addExperience(workExperience, user);

        response.oparationStatus = operationResult ? Status.OK : Status.NOK;
        response.message = operationResult ? "Registro Criado." : "Não foi possível criar registro.";
        response.data = workExperience;

        return StatusCode(200, response);
    }

    [HttpPost]
    [Route("modifyWorkExperience")]
    public ActionResult<OperationResponseModel> modifyWorkExperience([FromHeader] String userToken, WorkExperienceModel workExperience) {
        OperationResponseModel response = new();
        UserRepository userRepository = new();

        if (!userRepository.validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }
        UserModel user = userRepository.getUserByToken(userToken);

        bool result = new WorkExperienceRepository().updateWorkExperience(workExperience, user);
        response.oparationStatus = result == true ? Status.OK : Status.NOK;
        response.message = result == true ? "Registro Atualizado." : "Não foi posível atualizar o registro.";

        return StatusCode(200, response);
    }

    [HttpGet]
    [Route("getWorkExperienceList")]
    public ActionResult<OperationResponseModel> getWorkExperienceList([FromHeader] String userToken, [FromQuery] int skip, [FromQuery] int take, [FromQuery] string? codigoRef) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        List<WorkExperienceModel> result = new WorkExperienceRepository().getWorkExperienceList(skip, take, codigoRef);
        response.oparationStatus = Status.OK;
        response.message = "";
        response.data = result;

        return StatusCode(200, response);
    }

    [HttpPost]
    [Route("getWorkExperienceByStringQuery")]
    public ActionResult<OperationResponseModel> getWorkExperienceByStringQuery([FromHeader] String userToken, [FromBody] string queryB64, [FromQuery] int skip, [FromQuery] int take, [FromQuery] string? codigoRef) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        try {
            byte[] valueBytes = System.Convert.FromBase64String(queryB64);
            string stringFilter = Uri.UnescapeDataString(System.Text.Encoding.UTF8.GetString(valueBytes));

            response.data = new WorkExperienceRepository().getWorkExperienceByStringQuery(stringFilter, skip, take, codigoRef);
            response.oparationStatus = Status.OK;

            return StatusCode(200, response);
        } catch (Exception e) {
            response.oparationStatus = Status.NOK;
            response.message = e.ToString();
            return StatusCode(500, response);
        }

    }

    [HttpPost]
    [Route("countWorkExperiencesByQuery")]
    public ActionResult<OperationResponseModel> countWorkExperiencesByQuery([FromHeader] String userToken, [FromBody] string queryB64) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        try {
            byte[] valueBytes = System.Convert.FromBase64String(queryB64);
            string stringFilter = Uri.UnescapeDataString(System.Text.Encoding.UTF8.GetString(valueBytes));

            response.data = new WorkExperienceRepository().countWorkExperiencesByQuery(stringFilter);
            response.oparationStatus = Status.OK;

            return StatusCode(200, response);
        } catch (Exception e) {
            response.oparationStatus = Status.NOK;
            response.message = e.ToString();
            return StatusCode(500, response);
        }
    }

    [HttpGet]
    [Route("countWorkExperiences")]
    public ActionResult<OperationResponseModel> countWorkExperiences([FromHeader] String userToken, [FromQuery] string? codigoRef) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }
        long result = new WorkExperienceRepository().countWorkExperiences(codigoRef);
        response.oparationStatus = Status.OK;
        response.data = result;
        return Ok(response);
    }

    [HttpGet]
    [Route("getWorkExperienceById")]
    public ActionResult<OperationResponseModel> getWorkExperienceById([FromHeader] String userToken, [FromQuery] string codigo) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        WorkExperienceModel result = new WorkExperienceRepository().getWorkExperienceById(codigo);
        response.oparationStatus = Status.OK;
        response.data = result;

        return StatusCode(200, response);
    }
}
