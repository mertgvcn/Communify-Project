using CommunifyLibrary.Models;
using CommunifyLibrary.Repository.Interfaces;

namespace CommunifyLibrary.Repository;
public class PasswordTokenRepository(CommunifyContext context) : BaseRepository<PasswordToken>(context), IPasswordTokenRepository
{

}
