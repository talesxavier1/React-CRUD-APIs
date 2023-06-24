using Microsoft.AspNetCore.Mvc;
using SingularChatAPIs.Models;
using SingularChatAPIs.Repository.Implementations;
using SingularChatAPIs.ResponseModels;
using SingularChatAPIs.ResponseModels.ENUMs;

namespace SingularChatAPIs.Controllers;

[ApiController]
[Route("Course")]
public class CourseController : Controller {

    [HttpPost]
    [Route("logicalDeleteCourse")]
    public ActionResult<OperationResponseModel> logicalDeleteCourse([FromHeader] String userToken, [FromQuery] string codigo) {

        OperationResponseModel response = new();
        UserRepository userRepository = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }
        UserModel user = userRepository.getUserByToken(userToken);

        Boolean operationResult = new CourseRepository().logicalDeleteCourse(codigo, user);

        response.oparationStatus = operationResult ? Status.OK : Status.NOK;

        return StatusCode(200, response);
    }

    [HttpPost]
    [Route("addCourse")]
    public ActionResult<OperationResponseModel> addCourse([FromHeader] String userToken, CourseModel course) {
        OperationResponseModel response = new();
        UserRepository userRepository = new();

        if (!userRepository.validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        UserModel user = userRepository.getUserByToken(userToken);

        Boolean operationResult = new CourseRepository().addCourse(course, user);

        response.oparationStatus = operationResult ? Status.OK : Status.NOK;
        response.message = operationResult ? "Registro Criado." : "Não foi possível criar registro.";
        response.data = course;

        return StatusCode(200, response);
    }

    [HttpPost]
    [Route("modifyCourse")]
    public ActionResult<OperationResponseModel> modifyCourse([FromHeader] String userToken, CourseModel course) {
        OperationResponseModel response = new();
        UserRepository userRepository = new();

        if (!userRepository.validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }
        UserModel user = userRepository.getUserByToken(userToken);

        bool result = new CourseRepository().updateCourse(course, user);
        response.oparationStatus = result == true ? Status.OK : Status.NOK;
        response.message = result == true ? "Registro Atualizado." : "Não foi posível atualizar o registro.";

        return StatusCode(200, response);
    }

    [HttpGet]
    [Route("getCourseList")]
    public ActionResult<OperationResponseModel> getCourseList([FromHeader] String userToken, [FromQuery] int skip, [FromQuery] int take, [FromQuery] string? codigoRef) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        var result = new CourseRepository().getCoursesList(skip, take, codigoRef);
        response.oparationStatus = Status.OK;
        response.message = "";
        response.data = result;

        return StatusCode(200, response);
    }

    [HttpPost]
    [Route("getCourseByStringQuery")]
    public ActionResult<OperationResponseModel> getCourseByStringQuery([FromHeader] String userToken, [FromBody] string queryB64, [FromQuery] int skip, [FromQuery] int take, [FromQuery] string? codigoRef) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        try {
            byte[] valueBytes = System.Convert.FromBase64String(queryB64);
            string stringFilter = Uri.UnescapeDataString(System.Text.Encoding.UTF8.GetString(valueBytes));

            response.data = new CourseRepository().getCoursesByStringQuery(stringFilter, skip, take, codigoRef);
            response.oparationStatus = Status.OK;

            return StatusCode(200, response);
        } catch (Exception e) {
            response.oparationStatus = Status.NOK;
            response.message = e.ToString();
            return StatusCode(500, response);
        }

    }

    [HttpPost]
    [Route("countCourseByQuery")]
    public ActionResult<OperationResponseModel> countCourseByQuery([FromHeader] String userToken, [FromBody] string queryB64) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        try {
            byte[] valueBytes = System.Convert.FromBase64String(queryB64);
            string stringFilter = Uri.UnescapeDataString(System.Text.Encoding.UTF8.GetString(valueBytes));

            response.data = new CourseRepository().countCoursesByQuery(stringFilter);
            response.oparationStatus = Status.OK;

            return StatusCode(200, response);
        } catch (Exception e) {
            response.oparationStatus = Status.NOK;
            response.message = e.ToString();
            return StatusCode(500, response);
        }
    }

    [HttpGet]
    [Route("countCourse")]
    public ActionResult<OperationResponseModel> countCourse([FromHeader] String userToken, [FromQuery] string? codigoRef) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }
        long result = new CourseRepository().countCourses(codigoRef);
        response.oparationStatus = Status.OK;
        response.data = result;
        return Ok(response);
    }

    [HttpGet]
    [Route("getCourseById")]
    public ActionResult<OperationResponseModel> getCourseById([FromHeader] String userToken, [FromQuery] string codigo) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        var result = new CourseRepository().getCourseById(codigo);
        response.oparationStatus = Status.OK;
        response.data = result;

        return StatusCode(200, response);
    }

}
