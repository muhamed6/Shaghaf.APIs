using Shaghaf.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaghaf.Core.Specifications.Booking_Spec
{
    public class BookingWithRoomsSpecs : BaseSpecifications<Booking>
    {
        public BookingWithRoomsSpecs() : base()
        {
            Includes.Add(B => B.Room);
        }
        public BookingWithRoomsSpecs(int bookId) : base(B => B.Id == bookId)
        {
            Includes.Add(B => B.Room);
        }

    }
}
