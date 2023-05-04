using MongoDB.Bson.Serialization.Attributes;

namespace SingularChatAPIs.Models;
public class PositionModel {

    [BsonId]
    public string codigo { get; set; }

    [BsonElement("position")]
    public string position { get; set; }

    public PositionModel() {
        this.codigo = Guid.NewGuid().ToString();
    }

}
