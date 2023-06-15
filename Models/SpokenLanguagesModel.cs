using MongoDB.Bson.Serialization.Attributes;

namespace SingularChatAPIs.Models;
public class SpokenLanguagesModel {

    [BsonId]
    [BsonElement("codigo")]
    public string id { get; set; }

    [BsonElement("codigoRef")]
    public string codigoRef { get; set; }

    [BsonElement("nomeIdioma")]
    public string languageName { get; set; }

    [BsonElement("nivelProeficiencia")]
    public string proficiencyLevel { get; set; }

    [BsonElement("aplicacoesPraticas")]
    public string practicalApplications { get; set; }

    [BsonElement("dataController")]
    public ControllerModel dataController;

    public SpokenLanguagesModel() {
        this.id = Guid.NewGuid().ToString();
    }

}

