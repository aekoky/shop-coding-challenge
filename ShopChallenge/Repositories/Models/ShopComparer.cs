using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopChallenge.Repositories.Models
{
    public class ShopComparer : IEqualityComparer<ShopModel>
    {
      
        public bool Equals(ShopModel x, ShopModel y)
        {
            return x?.Id.Equals(y.Id) ?? false;
        }

        public int GetHashCode(ShopModel obj)
        {
            return base.GetHashCode();
        }
    }
}
