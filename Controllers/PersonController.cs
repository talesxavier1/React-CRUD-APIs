using Microsoft.AspNetCore.Mvc;
using SingularChatAPIs.Models;
using SingularChatAPIs.Repository.Implementations;
using SingularChatAPIs.ResponseModels;
using SingularChatAPIs.ResponseModels.ENUMs;

namespace SingularChatAPIs.Controllers;

[ApiController]
[Route("Person")]
public class PersonCOntroller : Controller {

    [HttpPost]
    [Route("logicalDeletePerson")]
    public ActionResult<OperationResponseModel> logicalDeletePerson([FromHeader] String userToken, [FromQuery] string codigo) {

        OperationResponseModel response = new();
        UserRepository userRepository = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }
        UserModel user = userRepository.getUserByToken(userToken);

        Boolean operationResult = new PersonRepository().logicalDeletePerson(codigo, user);

        response.oparationStatus = operationResult ? Status.OK : Status.NOK;

        return StatusCode(200, response);
    }

    [HttpPost]
    [Route("addPerson")]
    public ActionResult<OperationResponseModel> addPerosn([FromHeader] String userToken, PersonModel person) {
        OperationResponseModel response = new();
        UserRepository userRepository = new();

        if (!userRepository.validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        UserModel user = userRepository.getUserByToken(userToken);

        Boolean operationResult = new PersonRepository().addPerson(person, user);

        response.oparationStatus = operationResult ? Status.OK : Status.NOK;
        response.message = operationResult ? "Usuário Criado." : "Não foi possível criar usuário.";
        response.data = person;

        return StatusCode(200, response);
    }

    [HttpPost]
    [Route("modifyPerson")]
    public ActionResult<OperationResponseModel> modifyPerson([FromHeader] String userToken, PersonModel person) {
        OperationResponseModel response = new();
        UserRepository userRepository = new();

        if (!userRepository.validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }
        UserModel user = userRepository.getUserByToken(userToken);

        new PersonRepository().updatePerson(person, user);
        return StatusCode(200, response);
    }

    [HttpGet]
    [Route("getPersonList")]
    public ActionResult<OperationResponseModel> getPersonList([FromHeader] String userToken, [FromQuery] int skip, int take) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        List<PersonModel> result = new PersonRepository().getPersonList(skip, take);
        response.oparationStatus = Status.OK;
        response.message = "";
        response.data = result;

        return StatusCode(200, response);
    }

    [HttpPost]
    [Route("getPersonsByStringQuery")]
    public ActionResult<OperationResponseModel> getPersonsByStringQuery([FromHeader] String userToken, [FromBody] string queryB64, [FromQuery] int skip, [FromQuery] int take) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        try {
            byte[] valueBytes = System.Convert.FromBase64String(queryB64);
            string stringFilter = Uri.UnescapeDataString(System.Text.Encoding.UTF8.GetString(valueBytes));

            response.data = new PersonRepository().getPersonsByStringQuery(stringFilter, skip, take);
            response.oparationStatus = Status.OK;

            return StatusCode(200, response);
        } catch (Exception e) {
            response.oparationStatus = Status.NOK;
            response.message = e.ToString();
            return StatusCode(500, response);
        }

    }

    [HttpPost]
    [Route("countPersonsByQuery")]
    public ActionResult<OperationResponseModel> countPersons([FromHeader] String userToken, [FromBody] string queryB64) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        try {
            byte[] valueBytes = System.Convert.FromBase64String(queryB64);
            string stringFilter = Uri.UnescapeDataString(System.Text.Encoding.UTF8.GetString(valueBytes));

            response.data = new PersonRepository().countPersons(stringFilter);
            response.oparationStatus = Status.OK;

            return StatusCode(200, response);
        } catch (Exception e) {
            response.oparationStatus = Status.NOK;
            response.message = e.ToString();
            return StatusCode(500, response);
        }
    }


    [HttpGet]
    [Route("countPersons")]
    public ActionResult<OperationResponseModel> countPersons([FromHeader] String userToken) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }
        long result = new PersonRepository().countPersons();
        response.oparationStatus = Status.OK;
        response.data = result;
        return Ok(response);
    }

    [HttpGet]
    [Route("getPersonById")]
    public ActionResult<OperationResponseModel> getPersonById([FromHeader] String userToken, [FromQuery] string codigo) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        PersonModel result = new PersonRepository().getPersonById(codigo);
        response.oparationStatus = Status.OK;
        response.data = result;

        return StatusCode(200, response);
    }

}
