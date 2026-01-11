using System;
using dating_app.Entities;

namespace dating_app.Interfaces;

public interface ITokenService
{
    string CreateToken(AppUser user);
}
