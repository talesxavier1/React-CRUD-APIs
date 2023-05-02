using SingularChatAPIs.Models;

namespace SingularChatAPIs.Repository.Interfaces;
interface IContactPersonRepository {
    public Boolean addContact(ContactModel contact, UserModel user);
    public List<ContactModel> getContact(string codigoRef, int skip, int take);
    public long count(string? codigoRef);
    public Boolean updateContact(ContactModel contact, UserModel User);
    public Boolean deleteContact(String codigo);
    public Boolean logicalDeleteContact(String[] codigos, UserModel user);
    public ContactModel getContactById(String codigo);
}