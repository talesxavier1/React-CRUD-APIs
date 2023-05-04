using MongoDB.Bson.Serialization.Attributes;

namespace SingularChatAPIs.Models;
public class AcademicBackgroundModel {

    [BsonId]
    public string codigo { get; set; }

    [BsonElement("education")]
    public string education { get; set; }

    public AcademicBackgroundModel() {
        this.codigo = Guid.NewGuid().ToString();
    }
}

