using MongoDB.Bson.Serialization.Attributes;

namespace SingularChatAPIs.Models;
public class PersonModel {

    [BsonId]
    public string codigo { get; set; }

    [BsonElement("nome")]
    public string nome { get; set; }

    [BsonElement("cpf")]
    public string cpf { get; set; }

    [BsonElement("rg")]
    public string rg { get; set; }

    [BsonElement("dataNascimento")]
    public string dataNascimento { get; set; }

    [BsonElement("nacionalidadePais")]
    public string nacionalidadePais { get; set; }

    [BsonElement("nacionalidadePaisID")]
    public string nacionalidadePaisID { get; set; }

    [BsonElement("nacionalidadeEstado")]
    public string nacionalidadeEstado { get; set; }

    [BsonElement("nacionalidadeEstadoID")]
    public string nacionalidadeEstadoID { get; set; }

    [BsonElement("nacionalidadeMunicipio")]
    public string nacionalidadeMunicipio { get; set; }

    [BsonElement("nacionalidadeMunicipioID")]
    public string nacionalidadeMunicipioID { get; set; }

    [BsonElement("sexo")]
    public string sexo { get; set; }

    [BsonElement("estadoCivil")]
    public string estadoCivil { get; set; }

    [BsonElement("tituloEleitorNumero")]
    public string tituloEleitorNumero { get; set; }

    [BsonElement("tituloEleitorZona")]
    public string tituloEleitorZona { get; set; }

    [BsonElement("tituloEleitorEstado")]
    public string tituloEleitorEstado { get; set; }

    [BsonElement("tituloEleitorExpedicao")]
    public string tituloEleitorExpedicao { get; set; }

    [BsonElement("tituloEleitorSecao")]
    public string tituloEleitorSecao { get; set; }

    [BsonElement("nomePai")]
    public string nomePai { get; set; }

    [BsonElement("nomeMae")]
    public string nomeMae { get; set; }

    [BsonElement("dataController")]
    public ControllerModel dataController;

    public PersonModel() {
        this.codigo = Guid.NewGuid().ToString();
    }
}
