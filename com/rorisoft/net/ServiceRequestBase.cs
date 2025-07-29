namespace com.rorisoft.net
{
    public abstract class ServiceRequestBase
    {
        public string? Username { get; set; }
        public string? Domain { get; set; }
        public UserViewModel? UserViewModel { get; set; }

    }

    public class UserViewModel
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Domain { get; set; }
        public string? Name { get; set; }
        public string? FirstName { get; set; }
        public string? SecondName { get; set; }
        public string? Email { get; set; }
        public string? Office { get; set; }
        public string? OfficeSettle { get; set; }
        public List<string>? UserPrivileges { get; set; }
        public List<string>? UserApps { get; set; }
    }
}
