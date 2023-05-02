using MongoDB.Driver;
using SingularChatAPIs.BD;
using SingularChatAPIs.Models;
using SingularChatAPIs.Repository.Interfaces;

namespace SingularChatAPIs.Repository.Implementations;

public class CountryRepository : ICountryRepository {
    private readonly IMongoCollection<CountryModel> collection;
    public CountryRepository() {
        IMongoDatabase database = MongoDBConnection.getMongoDatabase();
        collection = database.GetCollection<CountryModel>("Localities");
    }

    public List<CountryModel> get(string? codigo) {
        if(codigo == null) {
            return collection.Find(DOC => DOC.localitieType.Equals("country")).ToList<CountryModel>();
        } else {
            return collection.Find<CountryModel>(DOC => DOC.codigo.Equals(codigo) && DOC.localitieType.Equals("country")).ToList<CountryModel>();
        }
    }
}

public class StateRepository : IStateRepository {
    private readonly IMongoCollection<StateModel> collection;
    public StateRepository() {
        IMongoDatabase database = MongoDBConnection.getMongoDatabase();
        collection = database.GetCollection<StateModel>("Localities");
    }

    public List<StateModel> get(string? codigo, string? codigoRef) {
        if (codigo != null) {
            return collection.Find<StateModel>(DOC => DOC.localitieType.Equals("state") && DOC.codigo.Equals(codigo)).ToList<StateModel>();
        }

        if (codigoRef != null) {
            return collection.Find<StateModel>(DOC => DOC.localitieType.Equals("state") && DOC.codigoRef.Equals(codigoRef)).ToList<StateModel>();

        }

        return collection.Find<StateModel>(DOC => DOC.localitieType.Equals("state")).ToList<StateModel>();
    }
}

public class CityRepository : ICityRepository {
    private readonly IMongoCollection<CityModel> collection;
    public CityRepository() {
        IMongoDatabase database = MongoDBConnection.getMongoDatabase();
        collection = database.GetCollection<CityModel>("Localities");
    }

    public List<CityModel> get(string? codigo, string? codigoRef) {
        if (codigo != null) {
            return collection.Find<CityModel>(DOC => DOC.localitieType.Equals("city") && DOC.codigo.Equals(codigo)).ToList<CityModel>();
        }

        if (codigoRef != null) {
            return collection.Find<CityModel>(DOC => DOC.localitieType.Equals("city") && DOC.codigoRef.Equals(codigoRef)).ToList<CityModel>();

        }

        return collection.Find<CityModel>(DOC => DOC.localitieType.Equals("city")).ToList<CityModel>();
    }
}
