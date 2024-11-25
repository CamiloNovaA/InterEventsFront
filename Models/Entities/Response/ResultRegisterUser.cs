using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities.Response
{
    public class ResultRegisterUser
    {
        public bool Result { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
        public int IdUser { get; set; }
    }
}
