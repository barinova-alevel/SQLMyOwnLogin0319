using PhoneBookApi.Models;

namespace PhoneBookApi.Repositories
{
    public class ContactRepository : RepositoryBase<Contact>
    {
        public override string ServerPath => "~/Contacts";
    }
}