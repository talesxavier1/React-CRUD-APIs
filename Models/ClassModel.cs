using MongoDB.Bson.Serialization.Attributes;

namespace SingularChatAPIs.Models;
public class ClassModel {

    [BsonId]
    public string codigo { get; set; }

    [BsonElement("classCode")]
    public string classCode { get; set; }

    [BsonElement("schoolYear")]
    public int schoolYear { get; set; }

    [BsonElement("classStatus")]
    public string classStatus { get; set; }

    [BsonElement("gradeLevel")]
    public string gradeLevel { get; set; }

    [BsonElement("classShift")]
    public string classShift { get; set; }

    [BsonElement("responsibleTeacher")]
    public string responsibleTeacher { get; set; }

    [BsonElement("responsibleTeacherID")]
    public string responsibleTeacherID { get; set; }

    [BsonElement("observations")]
    public string observations { get; set; }

    [BsonElement("classStartDate")]
    public DateTime classStartDate { get; set; }

    [BsonElement("classEndtDate")]
    public DateTime classEndtDate { get; set; }

    [BsonElement("totalWorkload")]
    public double totalWorkload { get; set; }

    [BsonElement("supplementaryWorkload")]
    public double supplementaryWorkload { get; set; }

    [BsonElement("dataController")]
    public ControllerModel dataController;

    public ClassModel() {
        this.codigo = Guid.NewGuid().ToString();
    }
}
