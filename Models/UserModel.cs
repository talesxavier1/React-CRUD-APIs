using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace SingularChatAPIs.Models {
    public class UserModel : PersonUserModel {

        [Required]
        [BsonElement("email")]
        [MaxLength(50)]
        public string email { get; set; }

        [Required]
        [BsonElement("password")]
        [MaxLength(30)]
        public string password { get; set; }

        [BsonElement("active")]
        public bool active { get; set; } = true;

        [Required]
        [BsonElement("userToken")]
        public string userToken { get; private set; }

        [Required]
        [BsonElement("profileToken")]
        [MaxLength(36)]
        public string profileToken { get; set; }

        public UserModel() {
            this.userToken = Guid.NewGuid().ToString();
        }
    }
}
