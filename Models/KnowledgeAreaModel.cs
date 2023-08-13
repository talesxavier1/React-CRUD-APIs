using MongoDB.Bson.Serialization.Attributes;

namespace SingularChatAPIs.Models;
public class KnowledgeAreaModel {

    [BsonId]
    public string codigo { get; set; }

    [BsonElement("area")]
    public string area { get; set; }

    [BsonElement("dataController")]
    public ControllerModel dataController;

    public KnowledgeAreaModel() {
        this.codigo = Guid.NewGuid().ToString();
    }
}
