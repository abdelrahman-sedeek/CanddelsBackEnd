namespace CanddelsBackEnd.Helper
{
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using CanddelsBackEnd.Contexts;

    public class CartCleanupService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public CartCleanupService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<CandelContext>();

                    // Find expired carts
                    var expiredCarts = context.Carts
                        .Where(c => c.ExpiresAt <= DateTime.UtcNow)
                        .ToList();

                    if (expiredCarts.Any())
                    {
                        // Remove cart items and carts
                        context.CartItems.RemoveRange(expiredCarts.SelectMany(c => c.CartItems));
                        context.Carts.RemoveRange(expiredCarts);

                        await context.SaveChangesAsync(stoppingToken);
                    }
                }

               
                await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken);
            }
        }
    }

}
