using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.DTOs.Order
{
    public class OrderItemDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string Title { get; set; }
        public int GameId { get; set; }
        public decimal Price { get; set; }

    }
}
