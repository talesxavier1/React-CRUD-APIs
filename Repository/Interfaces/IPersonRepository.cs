using SingularChatAPIs.Models;

namespace SingularChatAPIs.Repository.Interfaces;
public interface IPersonRepository {
    public Boolean addPerson(PersonModel person, UserModel user);
    public PersonModel getPersonById(String id);
    public List<PersonModel> getPersonsByStringQuery(string query);
    public List<PersonModel> getPersonList(int skip, int take);
    public long countPersons();
    public Boolean updatePerson(PersonModel person, UserModel user);
    public Boolean deletePerson(String id);
    public Boolean logicalDeletePerson(String id, UserModel user);
}

