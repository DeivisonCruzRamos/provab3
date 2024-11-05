using System.ComponentModel.DataAnnotations.Schema;

namespace B3.Domain.Models.Common;
public class CDB 
{
    public decimal InitialValue { get; set; }
    public int DeadlineInMonths { get; set; }
}