using System.Data;
using Core.DbCustomTypes;
using Dapper;

namespace Core.DbTypeHandlers;

public class NormalizedStringHandler : SqlMapper.TypeHandler<NormalizedString>
{
    public override void SetValue(IDbDataParameter parameter, NormalizedString value)
    {
        parameter.Value = value.Value;
    }

    public override NormalizedString Parse(object value)
    {
        return new NormalizedString(value.ToString());
    }
}