using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dating_app.DTOs
{
    
    public class UserDto 
    {
        public required string Id {get; set;}

        public required string Email{get; set;}

        public required string DisplayName {get; set;}

        public  string? ImageUrl {get; set;}

        public required string Token {get; set;}
    }
}
