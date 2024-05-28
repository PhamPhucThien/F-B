namespace FooDrink.Repository.Interface
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(Guid id, string fullName);
    }
}
