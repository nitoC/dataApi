
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace dataApi.Models
{
    public class UserDto
    {
            public string Name { get; set; }
            public int Age { get; set; }
            public string Mobile { get; set; }
            public string UserName { get; set; }
            public string UserEmail { get; set; }
            public string Password { get; set; }

      
    }

}
