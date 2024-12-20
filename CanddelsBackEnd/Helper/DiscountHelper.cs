namespace CanddelsBackEnd.Helper
{
    public static class DiscountHelper
    {
        public static bool CheckDiscountExpired(DateTime? endDate)
        {
            return endDate == null || endDate < DateTime.Now;
        }
    }
}
