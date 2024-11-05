using B3.Domain.Models.Common;
using B3.Domain.Models.DTO;
using System.Collections.Immutable;

namespace B3.Domain.Extensions;
public class ModelTransformExtensions
{
    public static CDBDTO ToCDBDTO(Models.Common.CDB cdb)
    {
        var cbdDTO = new CDBDTO
        {
            InitialValue = cdb.InitialValue,
            DeadlineInMonths = cdb.DeadlineInMonths
        };
        return cbdDTO;
    }
}