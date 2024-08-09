using Shaghaf.Core.Entities.HomeEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaghaf.Core.Specifications.Home_Specs
{
    public class BirthdaySpecs : BaseSpecifications<Birthday>
    {
        public BirthdaySpecs() : base()
        {
            Includes.Add(B => B.Cakes);
            Includes.Add(B => B.Decorations);
            Includes.Add(B => B.Room);
            Includes.Add(B => B.Room.Location);
            Includes.Add(B => B.Room.PhotoSessions);
      
        }
        public BirthdaySpecs(int id) : base(b=> b.Id == id)
        {
            Includes.Add(B => B.Cakes);
            Includes.Add(B => B.Decorations);
            Includes.Add(B => B.Room);
            Includes.Add(B => B.Room.Location);
            Includes.Add(B => B.Room.PhotoSessions);
        }
        
    }
}
