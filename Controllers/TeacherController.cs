using Microsoft.AspNetCore.Mvc;
using SingularChatAPIs.Models;
using SingularChatAPIs.Repository.Implementations;
using SingularChatAPIs.ResponseModels;
using SingularChatAPIs.ResponseModels.ENUMs;

namespace SingularChatAPIs.Controllers;

[ApiController]
[Route("Teacher")]
public class TeacherController : Controller {

    [HttpPost]
    [Route("logicalDeleteTeacher")]
    public ActionResult<OperationResponseModel> logicalDeleteTeacher([FromHeader] String userToken, [FromQuery] string codigo) {

        OperationResponseModel response = new();
        UserRepository userRepository = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }
        UserModel user = userRepository.getUserByToken(userToken);

        Boolean operationResult = new TeacherRepository().logicalDeleteTeacher(codigo, user);

        response.oparationStatus = operationResult ? Status.OK : Status.NOK;

        return StatusCode(200, response);
    }

    [HttpPost]
    [Route("addTeacher")]
    public ActionResult<OperationResponseModel> addTeacher([FromHeader] String userToken, [FromBody] TeacherModel teacher) {
        OperationResponseModel response = new();
        UserRepository userRepository = new();

        if (!userRepository.validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        UserModel user = userRepository.getUserByToken(userToken);

        Boolean operationResult = new TeacherRepository().addTeacher(teacher, user);

        response.oparationStatus = operationResult ? Status.OK : Status.NOK;
        response.message = operationResult ? "Registro Criado." : "Não foi possível criar registro.";
        response.data = teacher;

        return StatusCode(200, response);
    }

    [HttpPost]
    [Route("modifyTeacher")]
    public ActionResult<OperationResponseModel> modifyTeacher([FromHeader] String userToken, [FromBody] TeacherModel teacher) {
        OperationResponseModel response = new();
        UserRepository userRepository = new();

        if (!userRepository.validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }
        UserModel user = userRepository.getUserByToken(userToken);

        new TeacherRepository().updateTeacher(teacher, user);
        return StatusCode(200, response);
    }

    [HttpGet]
    [Route("getTeacherList")]
    public ActionResult<OperationResponseModel> getTeacherList([FromHeader] String userToken, [FromQuery] int skip, int take) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        List<TeacherModel> result = new TeacherRepository().getTeacherList(skip, take);
        response.oparationStatus = Status.OK;
        response.message = "";
        response.data = result;

        return StatusCode(200, response);
    }

    [HttpPost]
    [Route("getTeacherByQuery")]
    public ActionResult<OperationResponseModel> getTeacherByQuery([FromHeader] String userToken, [FromBody] string queryB64, [FromQuery] int skip, [FromQuery] int take) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        try {
            byte[] valueBytes = System.Convert.FromBase64String(queryB64);
            string stringFilter = Uri.UnescapeDataString(System.Text.Encoding.UTF8.GetString(valueBytes));

            response.data = new TeacherRepository().getTeacherByStringQuery(stringFilter, skip, take);
            response.oparationStatus = Status.OK;

            return StatusCode(200, response);
        } catch (Exception e) {
            response.oparationStatus = Status.NOK;
            response.message = e.ToString();
            return StatusCode(500, response);
        }
    }

    [HttpPost]
    [Route("countTeachersByQuery")]
    public ActionResult<OperationResponseModel> countTeachersByQuery([FromHeader] String userToken, [FromBody] string queryB64) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        try {
            byte[] valueBytes = System.Convert.FromBase64String(queryB64);
            string stringFilter = Uri.UnescapeDataString(System.Text.Encoding.UTF8.GetString(valueBytes));

            response.data = new TeacherRepository().countTeacher(stringFilter);
            response.oparationStatus = Status.OK;

            return StatusCode(200, response);
        } catch (Exception e) {
            response.oparationStatus = Status.NOK;
            response.message = e.ToString();
            return StatusCode(500, response);
        }
    }

    [HttpGet]
    [Route("countTeachers")]
    public ActionResult<OperationResponseModel> countTeacher([FromHeader] String userToken) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }
        long result = new TeacherRepository().countTeacher();
        response.oparationStatus = Status.OK;
        response.data = result;
        return Ok(response);
    }

    [HttpGet]
    [Route("getTeacherById")]
    public ActionResult<OperationResponseModel> getTeacherById([FromHeader] String userToken, [FromQuery] string codigo) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        TeacherModel result = new TeacherRepository().getTeacherById(codigo);
        response.oparationStatus = Status.OK;
        response.data = result;

        return StatusCode(200, response);
    }


}
