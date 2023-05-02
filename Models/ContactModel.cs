using MongoDB.Bson.Serialization.Attributes;

namespace SingularChatAPIs.Models;
public class ContactModel {

    [BsonId]
    public string codigo { get; set; }

    [BsonElement("codigoRef")]
    public string codigoRef { get; set; }

    [BsonElement("nomeContato")]
    public string nomeContato { get; set; }

    [BsonElement("email")]
    public string email { get; set; }

    [BsonElement("telefoneCelular")]
    public string telefoneCelular { get; set; }

    [BsonElement("telefoneFixo")]
    public string telefoneFixo { get; set; }

    [BsonElement("dataController")]
    public ControllerModel dataController;

    public ContactModel() {
        this.codigo = Guid.NewGuid().ToString();
    }
}