using dataApi.Database;
using dataApi.Models;


namespace dataApi.Repositories
{
    public class UserRepositories
    {
public static async Task<object> FindUserByEmail(Databasecontext context, User user)
        {
            
            try
            {
                var userData = await context.Users.FirstOrDefaultAsync<User>(User => User.UserEmail == user.UserEmail);

                if (userData != null)
                {
                    return userData;
                }
            }catch (Exception ex) {
                if (ex.InnerException != null)
                {
                    var innerExceptionMessage = ex.InnerException.Message;
                    // Log or print the inner exception message for details
                    return new {
                    error = innerExceptionMessage,
                    };
                }
               // _logger.LogError(ex.ToString());
                return new
                {
                    error = ex.Message,
                };
            }
            return null;
        }



        public static async Task<object> FindUserById(Databasecontext context, string Id)
        {
            try
            {
                var userData = await context.Users.FindAsync(Id);

                if (userData != null)
                {
                    return userData;
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    var innerExceptionMessage = ex.InnerException.Message;
                    // Log or print the inner exception message for details
                    return new
                    {
                        error = innerExceptionMessage,
                    };
                }
                // _logger.LogError(ex.ToString());
                return new
                {
                    error = ex.Message,
                };
            }
            return null;
        }

    }
}
/*"name": "miracle",
  "age": 23,
  "mobile": "09032672139",
  "userName": "mimi",
  "userEmail": "mimi@gmail.com",
  "password": "string1234"
*/