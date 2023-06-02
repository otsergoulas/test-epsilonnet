namespace BlazorApp.Server.Misc
{
    public interface IRole
    {
        string Name { get; set; }
    }

    public class Employee : IRole
    {
        public string Name { get; set; } = string.Empty;
    }

    public class Manager : IRole
    {
        public string Name { get; set; } = string.Empty;
    }

    public class RoleUtils
    {
        public void PrintName<T>(T role) where T : IRole
        {
            Console.WriteLine(role.Name);
        }
    }
}
