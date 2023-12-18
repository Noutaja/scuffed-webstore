using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Core.src.Parameters;
using ScuffedWebstore.Service.src.DTOs;

namespace ScuffedWebstore.Service.src.Abstractions;
public interface IOrderService : IBaseService<Order, OrderReadDTO, OrderCreateDTO, OrderUpdateDTO>
{

}