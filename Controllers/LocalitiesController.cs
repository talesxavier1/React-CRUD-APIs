namespace SingularChatAPIs.Controllers;
using Microsoft.AspNetCore.Mvc;
using SingularChatAPIs.Repository.Implementations;
using SingularChatAPIs.ResponseModels;
using SingularChatAPIs.ResponseModels.ENUMs;

[ApiController]
[Route("localities")]
public class LocalitiesController : Controller {
    
    [HttpGet]
    [Route("getCountries")]
    public ActionResult<OperationResponseModel> getCountries([FromHeader] string userToken, [FromQuery] string? codigo) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        response.data = new CountryRepository().get(codigo);

        return Ok(response);
    }

    [HttpGet]
    [Route("getStates")]
    public ActionResult<OperationResponseModel> getStates([FromHeader] string userToken, [FromQuery] string? codigo, [FromQuery] string? codigoRef) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        response.data = new StateRepository().get(codigo, codigoRef);

        return Ok(response);
    }

    [HttpGet]
    [Route("getCities")]
    public ActionResult<OperationResponseModel> getCities([FromHeader] string userToken, [FromQuery] string? codigo, [FromQuery] string? codigoRef) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        response.data = new CityRepository().get(codigo, codigoRef);

        return Ok(response);
    }

}
