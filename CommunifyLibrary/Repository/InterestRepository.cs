using CommunifyLibrary.Models;

namespace CommunifyLibrary.Repository
{
    public class InterestRepository(CommunifyContext context) : BaseRepository<Interest>(context), IInterestRepository
    {
    }
}
