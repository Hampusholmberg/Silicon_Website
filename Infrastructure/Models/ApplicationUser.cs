﻿using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models;

public class ApplicationUser : IdentityUser
{
    [Required]
    [Display(Name = "First name")]
    [ProtectedPersonalData]
    public string FirstName { get; set; } = null!;

    [Required]
    [Display(Name = "Last name")]
    [ProtectedPersonalData]
    public string LastName { get; set; } = null!;


    public virtual UserProfileEntity UserProfile { get; set; }
}