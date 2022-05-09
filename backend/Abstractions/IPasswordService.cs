namespace Abstractions
{
    public interface IPasswordService
    {
        string CalculateHash(string password);
    }
}
