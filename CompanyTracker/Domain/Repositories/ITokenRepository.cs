using Microsoft.IdentityModel.Tokens;

namespace CompanyTracker.Domain.Repositories;

public interface ITokenRepository
{
    string CreateTestToken(TokenType tokenType);
    JsonWebKey PublishKey();
}
