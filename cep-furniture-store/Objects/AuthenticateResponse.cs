using cep_furniture_store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cep_furniture_store.Objects
{
    public class AuthenticateResponse
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public int roleId { get; set; }
        public string Token { get; set; }

        public AuthenticateResponse(User user, string token)
        {
            id = user.id;
            name = user.name;
            email = user.email;
            roleId = user.roleId;
            Token = token;
        }
    }
}
