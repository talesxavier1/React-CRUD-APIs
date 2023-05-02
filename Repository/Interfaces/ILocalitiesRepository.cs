using SingularChatAPIs.Models;

namespace SingularChatAPIs.Repository.Interfaces;

interface ICountryRepository {
    public List<CountryModel> get(string? codigo);
}

interface IStateRepository {
    public List<StateModel> get(string? codigo, string? codigoRef);
}

interface ICityRepository {
    public List<CityModel> get(string? codigo, string? codigoRef);
}

