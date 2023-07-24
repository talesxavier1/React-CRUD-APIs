using MongoDB.Bson.Serialization.Attributes;

namespace SingularChatAPIs.Models;
public class ClassKnowledgeAreaModel {

    [BsonId]
    public string codigo { get; set; }

    [BsonElement("area")]
    public string area { get; set; }

    [BsonElement("dataController")]
    public ControllerModel dataController;

    public ClassKnowledgeAreaModel() {
        this.codigo = Guid.NewGuid().ToString();
    }
}
