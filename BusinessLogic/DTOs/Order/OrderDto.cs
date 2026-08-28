using DataAccess.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.DTOs.Order
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderItemDto> OrderItems { get; set; } = new();

    }
}
