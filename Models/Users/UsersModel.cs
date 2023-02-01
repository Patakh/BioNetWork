namespace BioNetWork.Models.Users
{
    public class UsersModel
    {
        public class Person
        {
            public String Id { get; set; }
            public String Name { get; set; }
            public String Email { get; set; }
            public String Phone { get; set; }
            public String Adres { get; set; }
            public String DateRegistration { get; set; }
        }
        public Person person { get; set; }
        public List<Person> UserList { get; set; }

    }

}
