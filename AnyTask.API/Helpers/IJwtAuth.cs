using AnyTask.API.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnyTask.API.Helpers
{
    public interface IJwtAuth
    {
        string GenerateToken(User user);
    }
}
