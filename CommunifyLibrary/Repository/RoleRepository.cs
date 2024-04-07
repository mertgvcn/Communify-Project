using CommunifyLibrary.Models;

namespace CommunifyLibrary.Repository
{
    public class RoleRepository(CommunifyContext context) : BaseRepository<Role>(context), IRoleRepository
    {

    }
}
