using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DiegoWebAPI.Domain.Models
{
    public class ApplicationUser : IdentityUser
    {
        [JsonIgnore]
        List<Post> Posts { get; set; }

    }
}
