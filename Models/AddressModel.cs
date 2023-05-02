using MongoDB.Bson.Serialization.Attributes;

namespace SingularChatAPIs.Models;
public class AddressModel {

    [BsonId]
    public string codigo { get; set; }

    [BsonElement("codigoRef")]
    public string codigoRef { get; set; }

    [BsonElement("CEP")]
    public string CEP { get; set; }

    [BsonElement("tipoEndereco")]
    public string tipoEndereco { get; set; }

    [BsonElement("rua")]
    public string rua { get; set; }

    [BsonElement("numero")]
    public string numero { get; set; }

    [BsonElement("bairro")]
    public string bairro { get; set; }

    [BsonElement("cidade")]
    public string cidade { get; set; }

    [BsonElement("codigoIBGECidade")]
    public string codigoIBGECidade { get; set; }

    [BsonElement("estado")]
    public string estado { get; set; }

    [BsonElement("codigoIBGEEstado")]
    public string codigoIBGEEstado { get; set; }

    [BsonElement("dataController")]
    public ControllerModel dataController;

    public AddressModel() {
        this.codigo = Guid.NewGuid().ToString();
    }
}

