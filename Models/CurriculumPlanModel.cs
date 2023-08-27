using MongoDB.Bson.Serialization.Attributes;

namespace SingularChatAPIs.Models;
public class CurriculumPlanModel {

    [BsonId]
    public string codigo { get; set; }

    [BsonElement("codigoRef")]
    public string codigoRef { get; set; }

    [BsonElement("supplementary")]
    public string supplementary { get; set; }

    [BsonElement("knowledgeArea")]
    public string knowledgeArea { get; set; }

    [BsonElement("knowledgeAreaId")]
    public string knowledgeAreaId { get; set; }

    [BsonElement("curricularComponent")]
    public string curricularComponent { get; set; }

    [BsonElement("curricularComponentId")]
    public string curricularComponentId { get; set; }

    [BsonElement("weeklyClasses")]
    public int weeklyClasses { get; set; }

    [BsonElement("hourLoad")]
    public int hourLoad { get; set; }

    [BsonElement("dataController")]
    public ControllerModel dataController;

    public CurriculumPlanModel() {
        this.codigo = Guid.NewGuid().ToString();
    }
}
