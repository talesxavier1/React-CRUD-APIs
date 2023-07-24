using MongoDB.Bson.Serialization.Attributes;

namespace SingularChatAPIs.Models;
public class CurricularComponentModel {

    [BsonId]
    public string codigo { get; set; }

    [BsonElement("component")]
    public string component { get; set; }

    [BsonElement("dataController")]
    public ControllerModel dataController;


    public CurricularComponentModel() {
        this.codigo = Guid.NewGuid().ToString();
    }
}

