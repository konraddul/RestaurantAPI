using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAPI.Authorization
{
    public class MinimumCreatedRestaurantsRequirements : IAuthorizationRequirement
    {
        public int MinimumCreatedRestaurants { get; }
        public MinimumCreatedRestaurantsRequirements(int minumumCreatedRestaurants)
        {
            MinimumCreatedRestaurants = minumumCreatedRestaurants;
        }
    }
}
