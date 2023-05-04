using MongoDB.Bson.Serialization.Attributes;

namespace SingularChatAPIs.Models;
public class AreaOfSpecializationModel {

    [BsonId]
    public string codigo { get; set; }

    [BsonElement("area")]
    public string area { get; set; }

    public AreaOfSpecializationModel() {
        this.codigo = Guid.NewGuid().ToString();
    }
}

