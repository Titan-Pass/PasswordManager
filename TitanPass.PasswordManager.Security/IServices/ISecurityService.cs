﻿using TitanPass.PasswordManager.Security.Models;

namespace TitanPass.PasswordManager.Security.IServices
{
    public interface ISecurityService
    {
        JwtToken GenerateJwtToken(string email, string password);
    }
}