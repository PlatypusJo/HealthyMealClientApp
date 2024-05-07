using HealthyMeal.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthyMeal.Services.BLL
{
    public class GetShopListPageResponseModel
    {
        public int Count { get; set; }
        public List<ProductToBuyModel> ShopList { get; set; }
    }
}
