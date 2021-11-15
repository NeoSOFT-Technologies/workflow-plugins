using Entities;
using IRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class UserRepository : BaseRepository<UserDetails>, IUserRepository
    {
        public UserRepository(UserDbContext dbContext) : base(dbContext)
        {
        }
    }
}
