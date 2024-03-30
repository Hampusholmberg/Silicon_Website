using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Dtos;

public class ApplicationUserDto
{
    public string Id { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public bool IsAdmin { get; set; }


    //    "id": "string",
    //"userName": "string",
    //"normalizedUserName": "string",
    //"email": "string",
    //"normalizedEmail": "string",
    //"emailConfirmed": true,
    //"passwordHash": "string",
    //"securityStamp": "string",
    //"concurrencyStamp": "string",
    //"phoneNumber": "string",
    //"phoneNumberConfirmed": true,
    //"twoFactorEnabled": true,
    //"lockoutEnd": "2024-03-30T08:04:36.027Z",
    //"lockoutEnabled": true,
    //"accessFailedCount": 0,
    //"firstName": "string",
    //"lastName": "string",
    //"isExternalAccount": true,
    //"isAdmin": true,
}
