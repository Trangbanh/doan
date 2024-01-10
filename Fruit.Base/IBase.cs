using Fruit.Base.Interface;

namespace FruitKha.Base
{
    public interface IBase
    {
        IBill bills { get; }
        IBlog blogs { get; }
        IProduct products { get; }
        IProductType ProductType { get; }
        IAdmin admins { get; }
        IUser users { get; }
        IComment comments { get; }
        IDetailedInvoice detailedInvoice { get; }
        IContacts contacts { get; }
        void Commit();
    }
}
