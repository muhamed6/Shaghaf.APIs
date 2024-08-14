using Shaghaf.Core.Entities.HomeEntities;
using Shaghaf.Core.Entities.RoomEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaghaf.Core.Specifications.Home_Specs
{
    public class CategorySpecs : BaseSpecifications<Category>
    {
        public CategorySpecs() : base()
        {
            Includes.Add(B => B.RoomCategories);
        }
        public CategorySpecs(int id) : base(b => b.Id == id)
        {
            Includes.Add(B => B.RoomCategories);
        }
    }
}
