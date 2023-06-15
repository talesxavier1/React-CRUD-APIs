using MongoDB.Bson.Serialization.Attributes;

namespace SingularChatAPIs.Models;
public class WorkExperienceModel {

    [BsonId]
    public string codigo { get; set; }

    [BsonElement("codigoRef")]
    public string codigoRef { get; set; }

    [BsonElement("nomeInstituicao")]
    public string nomeInstituicao { get; set; }

    [BsonElement("cargo")]
    public string cargo { get; set; }

    [BsonElement("cargoID")]
    public string cargoID { get; set; }

    [BsonElement("areaAtuacao")]
    public string areaAtuacao { get; set; }

    [BsonElement("areaAtuacaoID")]
    public string areaAtuacaoID { get; set; }

    [BsonElement("dataInicio")]
    public DateTime? dataInicio { get; set; } = null;

    [BsonElement("dataFim")]
    public DateTime? dataFim { get; set; } = null;

    [BsonElement("descricao")]
    public string descricao { get; set; }

    [BsonElement("regimeContratacao")]
    public string regimeContratacao { get; set; }

    [BsonElement("cargaHoraria")]
    public double cargaHoraria { get; set; }

    [BsonElement("salario")]
    public double salario { get; set; }

    [BsonElement("dataController")]
    public ControllerModel dataController;

    public WorkExperienceModel() {
        this.codigo = Guid.NewGuid().ToString();
    }
}
