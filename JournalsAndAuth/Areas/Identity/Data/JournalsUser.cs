using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JournalsAndAuth.Models;
using Microsoft.AspNetCore.Identity;

namespace JournalsAndAuth.Areas.Identity.Data;

// Add profile data for application users by adding properties to the JournalsUser class
public class JournalsUser : IdentityUser
{
    public HashSet<Journal> Journals { get; set; } = new HashSet<Journal>();
}

