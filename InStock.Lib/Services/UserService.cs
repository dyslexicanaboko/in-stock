using InStock.Lib.DataAccess;
using InStock.Lib.Entities;
using InStock.Lib.Exceptions;
using InStock.Lib.Validation;
using Crypto = BCrypt.Net.BCrypt; //Naming it so it's clear what is 3rd party

namespace InStock.Lib.Services
{
  public class UserService : IUserService
  {
    private readonly IUserRepository _repoUser;

    public UserService(
      IUserRepository repoUser)
      => _repoUser = repoUser;

    public UserEntity? GetUser(int id)
    {
      var dbEntity = _repoUser.Using(x => x.Select(id));

      return dbEntity;
    }

    public IList<UserEntity> GetAllUsers()
    {
      var lst = _repoUser.Using(x => x.SelectAll()).ToList();

      return lst;
    }

    public UserEntity Add(UserEntity? user)
    {
      Validations.IsNotNull(user, nameof(user));

      user.UserId = _repoUser.Using(x => x.Insert(user));

      return user;
    }

    //I do not like this, but I am using it for now
    public UserEntity Authenticate(string username, string password)
    {
      var entity = _repoUser.Using(x => x.Select(username));

      //If user isn't found
      if (entity == null)
      {
        //throw exception about user not being found
        throw NotFoundExceptions.UserCredentials();
      }

      //TODO: Need to change this to a 401
      //If user is found, but the account is disabled
      Validations.IsUserAllowed(entity.IsAllowed);

      Validations.DoesPasswordMatch(password, entity.Password);
      
      //There is no conceivable scenario where the encrypted password is needed, just blank it out
      entity.Password = string.Empty;

      return entity;
    }

    //Will need this later for creating users
    private static string HashPassword(string plainTextPassword)
    {
      var salt = Crypto.GenerateSalt(12);

      var hashedPassword = Crypto.HashPassword(plainTextPassword, salt);

      return hashedPassword;
    }

    public static bool IsPasswordValid(string password, string correctHash)
    {
      var isValid = Crypto.Verify(password, correctHash);

      return isValid;
    }
  }
}
