using MongoDB.Bson.Serialization.Attributes;

namespace SingularChatAPIs.Models;

public class ControllerModel {

    public ControllerModel() { }

    [BsonElement("active")]
    public Boolean active { get; set; }

    [BsonElement("postDate")]
    public DateTime postDate { get; set; }

    [BsonElement("updateDate")]
    public DateTime? updateDate { get; set; }

    [BsonElement("userPost")]
    public String userPost { get; set; }

    [BsonElement("userUpdate")]
    public String userUpdate { get; set; }
}

