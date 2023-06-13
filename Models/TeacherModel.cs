using MongoDB.Bson.Serialization.Attributes;

namespace SingularChatAPIs.Models;
public class TeacherModel {
    [BsonId]
    public string codigo { get; set; }

    [BsonElement("pessoa")]
    public string pessoa { get; set; }

    [BsonElement("pessoaCPF")]
    public string pessoaCPF { get; set; }

    [BsonElement("pessoaID")]
    public string pessoaID { get; set; }

    [BsonElement("funcao")]
    public string funcao { get; set; }

    [BsonElement("funcaoID")]
    public string funcaoID { get; set; }

    [BsonElement("cargaHoraria")]
    public double cargaHoraria { get; set; }

    [BsonElement("formacaoAcademica")]
    public string formacaoAcademica { get; set; }

    [BsonElement("formacaoAcademicaID")]
    public string formacaoAcademicaID { get; set; }

    [BsonElement("dataInicioContratacao")]
    public DateTime dataInicioContratacao { get; set; }

    [BsonElement("nivelEnsinoQueMinistra")]
    public string nivelEnsinoQueMinistra { get; set; }

    [BsonElement("tipoContrato")]
    public string tipoContrato { get; set; }

    [BsonElement("areaAtuacao")]
    public string areaAtuacao { get; set; }

    [BsonElement("areaAtuacaoID")]
    public string areaAtuacaoID { get; set; }

    [BsonElement("valorHoraAula")]
    public double valorHoraAula { get; set; }

    [BsonElement("dataController")]
    public ControllerModel dataController;

    public TeacherModel() {
        this.codigo = Guid.NewGuid().ToString();
    }
}
