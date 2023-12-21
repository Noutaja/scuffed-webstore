using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScuffedWebstore.Core.src.Parameters;
public class GetAllAddressesParams : GetAllParams
{
    public Guid? UserID { get; set; } = null;
}