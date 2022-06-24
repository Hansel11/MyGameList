using Microsoft.AspNetCore.Mvc.RazorPages;
using MyGameList.Domain.Request;
using System.ComponentModel.DataAnnotations;

namespace MyGameList.Models
{
    public class LoginViewModel
    {
        public Guid? Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
