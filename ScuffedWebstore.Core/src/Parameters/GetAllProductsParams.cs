using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScuffedWebstore.Core.src.Parameters;
public class GetAllProductsParams : GetAllParams
{
    public string Query { get; set; } = string.Empty;
}