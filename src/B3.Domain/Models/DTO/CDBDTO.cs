using B3.Domain.Models.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace B3.Domain.Models.DTO;
public class CDBDTO : BaseClass
{
    public decimal GrossValue { get; set; }
    public decimal NetValue { get; set; }
    public decimal InitialValue { get; set; }
    public int DeadlineInMonths { get; set; }
}