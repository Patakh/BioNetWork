using System.ComponentModel.DataAnnotations;

namespace BioNetWork.Models.Users
{
    public class UsersModel
    {
        public class Person
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string Adres { get; set; }
            public DateTime DateRegistration { get; set; }
        }

        public Person person { get; set; }

        public List<Person> UserList { get; set; }

    }

}
