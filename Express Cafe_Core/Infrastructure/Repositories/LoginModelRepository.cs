using Express_Cafe_Core.Infrastructure.Base;
using Express_Cafe_Core.Infrastructure.INterfaces;
using Express_Cafe_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Express_Cafe_Core.Infrastructure.Repositories
{
    public class LoginModelRepository:GenericRepository<LoginModel>,ILoginModelRepository
    {
        public LoginModelRepository(dbRestaurentContext context):base(context)
        {
            
        }
    }
}
