namespace Infrastructure.Repositorys
{
    public interface IPasswordService
    {
        string HashPassword(string plainTextPassword);
        bool VerifyPassword(string hashedPassword, string plainTextPassword);
    }
}