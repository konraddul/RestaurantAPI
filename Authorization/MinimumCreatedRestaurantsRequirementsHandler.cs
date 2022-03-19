using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using RestaurantAPI.Entities;
using RestaurantAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RestaurantAPI.Authorization
{
    public class MinimumCreatedRestaurantsRequirementsHandler : AuthorizationHandler<MinimumCreatedRestaurantsRequirements>
    {
        private readonly ILogger<MinimumCreatedRestaurantsRequirementsHandler> _logger;
        private readonly RestaurantDbContext _dbContext;
        public MinimumCreatedRestaurantsRequirementsHandler(ILogger<MinimumCreatedRestaurantsRequirementsHandler> logger, RestaurantDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumCreatedRestaurantsRequirements requirement)
        {
            var userId = int.Parse(context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
            int created = _dbContext.Restaurants
                .Count(r => r.CreatedById == userId);
            _logger.LogInformation($"User: {userId} with created restaurants: [{created}]");
            if (created >= requirement.MinimumCreatedRestaurants)
            {
                _logger.LogInformation("Authorization succedded");
                context.Succeed(requirement);
            }
            else
            {
                _logger.LogInformation("Authorization failed");
            }
            return Task.CompletedTask;
        }
        /*protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumCreatedRestaurantsRequirements requirement)
{

   var userId = int.Parse(context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
   int created = _dbContext.Restaurants
       .Count(r => r.CreatedById == userId);
   _logger.LogInformation($"User: {userId} with date of birth: [{created}]");
   if (created >= requirement.MinimumCreatedRestaurants)
   {
       _logger.LogInformation("Authorization succedded");
       context.Succeed(requirement);
   }
   else
   {
       _logger.LogInformation("Authorization failed");
   }
   return Task.CompletedTask;
}*/
    }
}
