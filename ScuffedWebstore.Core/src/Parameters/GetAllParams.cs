using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScuffedWebstore.Core.src.Parameters;
public class GetAllParams
{
    public int Limit { get; set; } = 20;
    public int Offset { get; set; } = 0;
}