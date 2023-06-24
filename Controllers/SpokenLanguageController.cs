using Microsoft.AspNetCore.Mvc;
using SingularChatAPIs.Models;
using SingularChatAPIs.Repository.Implementations;
using SingularChatAPIs.ResponseModels;
using SingularChatAPIs.ResponseModels.ENUMs;

namespace SingularChatAPIs.Controllers;


[ApiController]
[Route("SpokenLanguage")]
public class SpokenLanguageController : Controller {
    [HttpPost]
    [Route("logicalDeleteSpokenLanguage")]
    public ActionResult<OperationResponseModel> logicalDeleteSpokenLanguage([FromHeader] String userToken, [FromQuery] string codigo) {

        OperationResponseModel response = new();
        UserRepository userRepository = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }
        UserModel user = userRepository.getUserByToken(userToken);

        Boolean operationResult = new SpokenLanguagesRepository().logicalDeleteSpokenLanguage(codigo, user);

        response.oparationStatus = operationResult ? Status.OK : Status.NOK;

        return StatusCode(200, response);
    }

    [HttpPost]
    [Route("addSpokenLanguage")]
    public ActionResult<OperationResponseModel> addSpokenLanguage([FromHeader] String userToken, SpokenLanguagesModel spokenLanguage) {
        OperationResponseModel response = new();
        UserRepository userRepository = new();

        if (!userRepository.validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        UserModel user = userRepository.getUserByToken(userToken);

        Boolean operationResult = new SpokenLanguagesRepository().addSpokenLanguage(spokenLanguage, user);

        response.oparationStatus = operationResult ? Status.OK : Status.NOK;
        response.message = operationResult ? "Registro Criado." : "Não foi possível criar registro.";
        response.data = spokenLanguage;

        return StatusCode(200, response);
    }

    [HttpPost]
    [Route("modifySpokenLanguage")]
    public ActionResult<OperationResponseModel> modifySpokenLanguage([FromHeader] String userToken, SpokenLanguagesModel spokenLanguage) {
        OperationResponseModel response = new();
        UserRepository userRepository = new();

        if (!userRepository.validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }
        UserModel user = userRepository.getUserByToken(userToken);

        bool result = new SpokenLanguagesRepository().updateSpokenLanguage(spokenLanguage, user);
        response.oparationStatus = result == true ? Status.OK : Status.NOK;
        response.message = result == true ? "Registro Atualizado." : "Não foi posível atualizar o registro.";

        return StatusCode(200, response);
    }

    [HttpGet]
    [Route("getSpokenLanguageList")]
    public ActionResult<OperationResponseModel> getSpokenLanguageList([FromHeader] String userToken, [FromQuery] int skip, [FromQuery] int take, [FromQuery] string? codigoRef) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        List<SpokenLanguagesModel> result = new SpokenLanguagesRepository().getSpokenLanguagesList(skip, take, codigoRef);
        response.oparationStatus = Status.OK;
        response.message = "";
        response.data = result;

        return StatusCode(200, response);
    }

    [HttpPost]
    [Route("getSpokenLanguageByQuery")]
    public ActionResult<OperationResponseModel> getSpokenLanguageByQuery([FromHeader] String userToken, [FromBody] string queryB64, [FromQuery] int skip, [FromQuery] int take, [FromQuery] string? codigoRef) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        try {
            byte[] valueBytes = System.Convert.FromBase64String(queryB64);
            string stringFilter = Uri.UnescapeDataString(System.Text.Encoding.UTF8.GetString(valueBytes));

            response.data = new SpokenLanguagesRepository().getSpokenLanguagesByStringQuery(stringFilter, skip, take, codigoRef);
            response.oparationStatus = Status.OK;

            return StatusCode(200, response);
        } catch (Exception e) {
            response.oparationStatus = Status.NOK;
            response.message = e.ToString();
            return StatusCode(500, response);
        }

    }

    [HttpPost]
    [Route("countSpokenLanguageByQuery")]
    public ActionResult<OperationResponseModel> countSpokenLanguageByQuery([FromHeader] String userToken, [FromBody] string queryB64) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        try {
            byte[] valueBytes = System.Convert.FromBase64String(queryB64);
            string stringFilter = Uri.UnescapeDataString(System.Text.Encoding.UTF8.GetString(valueBytes));

            response.data = new SpokenLanguagesRepository().countSpokenLanguagesByQuery(stringFilter);
            response.oparationStatus = Status.OK;

            return StatusCode(200, response);
        } catch (Exception e) {
            response.oparationStatus = Status.NOK;
            response.message = e.ToString();
            return StatusCode(500, response);
        }
    }

    [HttpGet]
    [Route("countSpokenLanguage")]
    public ActionResult<OperationResponseModel> countSpokenLanguage([FromHeader] String userToken, [FromQuery] string? codigoRef) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }
        long result = new SpokenLanguagesRepository().countSpokenLanguages(codigoRef);
        response.oparationStatus = Status.OK;
        response.data = result;
        return Ok(response);
    }

    [HttpGet]
    [Route("getSpokenLanguageById")]
    public ActionResult<OperationResponseModel> getSpokenLanguageById([FromHeader] String userToken, [FromQuery] string codigo) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        SpokenLanguagesModel result = new SpokenLanguagesRepository().getSpokenLanguageById(codigo);
        response.oparationStatus = Status.OK;
        response.data = result;

        return StatusCode(200, response);
    }
}
