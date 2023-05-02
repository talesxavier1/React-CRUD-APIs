using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace SingularChatAPIs.Models;
public class ProfileModel {

    [BsonId]
    public string id { get; private set; }

    [BsonElement("ProfileName")]
    public string profileName { get; private set; }

    [BsonElement("ProfileRate")]
    public int profileRate { get; private set; }

    [BsonElement("ProfileToken")]
    public string profileToken { get; private set; }

    [BsonElement("CanCreateUser")]
    public bool canCreateUser { get; private set; }

}

