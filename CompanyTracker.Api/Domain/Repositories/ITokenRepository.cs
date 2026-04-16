using Microsoft.IdentityModel.Tokens;

namespace CompanyTracker.Api.Domain.Repositories;

public interface ITokenRepository
{
    string CreateTestToken(TokenType tokenType);
    JsonWebKey PublishKey();
}
