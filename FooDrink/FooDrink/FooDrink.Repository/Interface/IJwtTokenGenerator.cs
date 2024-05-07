using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FooDrink.Repository.Interface
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(Guid id, string fullName);
    }
}
