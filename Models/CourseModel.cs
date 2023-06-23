using MongoDB.Bson.Serialization.Attributes;

namespace SingularChatAPIs.Models;
public class CourseModel {

    [BsonId]
    [BsonElement("codigo")]
    public string codigo { get; set; }

    [BsonElement("codigoRef")]
    public string codigoRef { get; set; }

    [BsonElement("cursoNome")]
    public string courseName { get; set; }

    [BsonElement("instituicaoEducacional")]
    public string educationalInstitution { get; set; }

    [BsonElement("cargaHoraria")]
    public double courseLoad { get; set; }

    [BsonElement("dataInicio")]
    public DateTime? startDate { get; set; } = null;

    [BsonElement("dataFim")]
    public DateTime? endDate { get; set; } = null;

    [BsonElement("modalidade")]
    public string modality { get; set; }

    [BsonElement("investimento")]
    public double financialInvestment { get; set; }

    [BsonElement("descricao")]
    public string descriptions { get; set; }

    [BsonElement("dataController")]
    public ControllerModel dataController;

    public CourseModel() {
        this.codigo = Guid.NewGuid().ToString();
    }

}

