using SingularChatAPIs.Models;

namespace SingularChatAPIs.Repository.Interfaces;
interface IPersonAdressRepository {
    public Boolean addAddress(AddressModel addressModel, UserModel user);
    public List<AddressModel> getAddress(string codigoRef, int skip, int take);
    public AddressModel getAddressById(string codigo);
    public Boolean updateAddress(AddressModel addressModel, UserModel User);
    public Boolean deleteAddress(String codigo);
    public Boolean logicalDeleteAddress(String[] codigos, UserModel user);
    public long count(String? codigoRef);
}
