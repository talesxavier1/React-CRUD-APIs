using MongoDB.Bson.Serialization.Attributes;

namespace SingularChatAPIs.Models;
public class CountryModel {
    [BsonId]
    public string codigo { get; set; }

    [BsonElement("localitieType")]
    public string localitieType { get; set; }

    [BsonElement("countryAcronym")]
    public string countryAcronym { get; set; }

    [BsonElement("countryName")]
    public string countryName { get; set; }

    [BsonElement("countryIBGECode")]
    public string countryIBGECode { get; set; }

    public CountryModel() {
        this.codigo = Guid.NewGuid().ToString();
    }
}

public class StateModel {
    [BsonId]
    public string codigo { get; set; }

    [BsonElement("codigoRef")]
    public string codigoRef { get; set; }

    [BsonElement("localitieType")]
    public string localitieType { get; set; }

    [BsonElement("stateAcronym")]
    public string stateAcronym { get; set; }

    [BsonElement("stateName")]
    public string stateName { get; set; }

    [BsonElement("stateIBGECode")]
    public string stateIBGECode { get; set; }

    public StateModel() {
        this.codigo = Guid.NewGuid().ToString();
    }
}

public class CityModel {
    [BsonId]
    public string codigo { get; set; }

    [BsonElement("codigoRef")]
    public string codigoRef { get; set; }

    [BsonElement("localitieType")]
    public string localitieType { get; set; }

    [BsonElement("cityName")]
    public string cityName { get; set; }

    [BsonElement("cityIBGECode")]
    public string cityIBGECode { get; set; }

    public CityModel() {
        this.codigo = Guid.NewGuid().ToString();
    }
}
