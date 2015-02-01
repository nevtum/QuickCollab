using QuickCollab.Collaboration.Domain.Models;

namespace QuickCollab.Collaboration.Domain.Services
{
    public interface IOpenNewParty
    {
        void OpenNewParty(PartyDetails details, string clearPassword);
    }
}
