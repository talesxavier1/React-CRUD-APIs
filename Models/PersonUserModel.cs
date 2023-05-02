using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace SingularChatAPIs.Models;
public abstract class PersonUserModel {

    [BsonId]
    public String id { get; private set; }

    [Required]
    [BsonElement("name")]
    [MaxLength(100)]
    public String name { get; set; }

    [BsonElement("lastName")]
    [MaxLength(100)]
    public String lastName { get; set; }

    [BsonElement("CPF")]
    [MaxLength(11)]
    public String CPF { get; set; } = "00000000000";

    [BsonElement("RG")]
    [MaxLength(9)]
    public String RG { get; set; } = "000000000";

    public PersonUserModel() {
        this.id = Guid.NewGuid().ToString();
    }
}

