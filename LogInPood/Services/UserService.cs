using LogInPood.Models;
using System.Collections.Generic;
using System.Linq;

public interface IUserService
{
    List<UserModel> GetUsers();
    void AddUser(UserModel user);
    UserModel GetUser(string username, string password);
    List<AdminModel> GetAdmins();
    void AddAdmin(AdminModel user);
    AdminModel GetAdmin(string username, string password);
}

public class UserService : IUserService
{
    private static List<UserModel> users = new List<UserModel>
    {
        new UserModel{id=1, username="Kiur", password="Kiur"},
        new UserModel{id=2, username="Marken", password="Marken"},
        new UserModel{id=3, username="Henri", password="Henri"},
    };

    private static List<AdminModel> admins = new List<AdminModel>
    {
        new AdminModel{id=111, username="KiurAdmin", password="KiurAdmin"},
        new AdminModel{id=222, username="MarkenAdmin", password="MarkenAdmin"},
        new AdminModel{id=333, username="HenriAdmin", password="HenriAdmin"},
    };

    // USERS
    public List<UserModel> GetUsers()
    {
        return users;
    }

    public void AddUser(UserModel user)
    {
        user.id = users.Max(u => u.id) + 1;
        users.Add(user);
    }

    public UserModel GetUser(string username, string password)
    {
        return users.FirstOrDefault(u => u.username == username && u.password == password);
    }

    // ADMINS
    public List<AdminModel> GetAdmins()
    {
        return admins;
    }

    public void AddAdmin(AdminModel admin)
    {
        admin.id = admins.Max(u => u.id) + 1;
        admins.Add(admin);
    }

    public AdminModel GetAdmin(string username, string password)
    {
        return admins.FirstOrDefault(u => u.username == username && u.password == password);
    }
}