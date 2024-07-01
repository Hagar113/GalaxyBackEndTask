using DataAccess.IRepo;
using DataAccess.Repo;
using DataProvider.IProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataAccess.ApplicationContext.ApplicationDBContext;
using static DataProvider.Provider.AuthProvider;

namespace DataProvider.Provider
{
    public class AuthProvider : IAuthProvider
    {
        private readonly ApplicationDbContext _dbContext;
        public IAuthenticationRepo AuthenticationRepo { get; private set;}

        public AuthProvider(ApplicationDbContext context, IAuthenticationRepo authenticationRepo)
        {
            _dbContext = context;
            AuthenticationRepo = authenticationRepo;
        }
    }


}
