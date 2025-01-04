using CanddelsBackEnd.Models;

namespace CanddelsBackEnd.Specifications
{
    public class OrderSpecification:BaseSpecification<Order>
    {
        public OrderSpecification() {
            AddInclude(sd => sd.ShippingDetail);
            AddInclude(oi=>oi.OrderItems);
        }
        public OrderSpecification(int id):base(x=>x.Id==id)
        {
            AddInclude(sd => sd.ShippingDetail);
            AddInclude(oi => oi.OrderItems);


        }




    }
}
