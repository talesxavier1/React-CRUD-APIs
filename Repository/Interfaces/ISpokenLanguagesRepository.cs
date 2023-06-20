using SingularChatAPIs.Models;

namespace SingularChatAPIs.Repository.Interfaces;
public interface ISpokenLanguagesRepository {

    public Boolean addSpokenLanguage(SpokenLanguagesModel spokenLanguages, UserModel user);
    public SpokenLanguagesModel getSpokenLanguageById(String id);
    public List<SpokenLanguagesModel> getSpokenLanguagesByStringQuery(string query, int skip, int take, string? codigoRef);
    public List<SpokenLanguagesModel> getSpokenLanguagesList(int skip, int take, string? codigoRef);
    public long countSpokenLanguagesByQuery(string query);
    public long countSpokenLanguages(string? codigoRef);
    public Boolean updateSpokenLanguage(SpokenLanguagesModel spokenLanguages, UserModel user);
    public Boolean deleteSpokenLanguage(String id);
    public Boolean logicalDeleteSpokenLanguage(String id, UserModel user);
}
