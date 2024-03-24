﻿using ITS_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITS_Library.Interfaces
{
    public interface IOrderService
    {
        bool CompletePurchase(int userId);
        ICollection<Order> GetMyOrders(int userId);
        Order GetOrder(int OrderId);
    }
}
