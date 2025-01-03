using CanddelsBackEnd.Models;

namespace CanddelsBackEnd.Specifications
{
    public class OrderSpecification:BaseSpecification<Order>
    {
        public OrderSpecification() {
            AddInclude(sd => sd.ShippingDetail);
               
        
        }
        public OrderSpecification(int id):base(x=>x.Id==id)
        {
            AddInclude(sd => sd.ShippingDetail);

        }
        
            
        

    }
}
