using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using JournalsAndAuth.Models;
using Microsoft.AspNetCore.Identity;

namespace JournalsAndAuth.Areas.Identity.Data;

// Add profile data for application users by adding properties to the JournalsUser class
public class JournalsUser : IdentityUser
{
    [Required(AllowEmptyStrings = false)]
    [Display(Name = "First Name")]
    [MaxLength(100)]
    public string FirstName { get; set; } = default!;

    [Required(AllowEmptyStrings = false)]
    [Display(Name = "Last Name")]
    [MaxLength(100)]
    public string LastName { get; set; } = default!;

    public HashSet<UserBlog> BlogUsers { get; set; } = new HashSet<UserBlog>();
    public HashSet<Journal> Journals { get; set; } = new HashSet<Journal>();
}