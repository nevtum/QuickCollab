using QuickCollab.Models;

namespace QuickCollab.Services
{
    public interface IManageAccounts
    {
        void CreateNewAccount(MemberLoginDetails details);
        void AuthenticateUser(MemberLoginDetails details);
    }
}
