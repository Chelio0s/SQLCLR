using System;
using System.Collections.Generic;
using Microsoft.SqlServer.Server;
using System.Data.SqlTypes;
using System.IO;
using System.Text;

[Serializable]
[SqlUserDefinedAggregate(
    Format.UserDefined, //use clr serialization to serialize the intermediate result  
    IsInvariantToNulls = true, //optimizer property  
    IsInvariantToDuplicates = false, //optimizer property  
    IsInvariantToOrder = false, //optimizer property  
    MaxByteSize = 8000)] //maximum size in bytes of persisted value  

public struct MySUM:IBinarySerialize
{
    public decimal _accumulate;

    public void Init()
    {
        _accumulate = 0;
      
    }

    public void Accumulate(SqlDecimal Value)
    {
        _accumulate += Value.Value;
    }

    public void Merge (MySUM Group)
    {
        //_accumulate += Group._accumulate;
    }

    public SqlDecimal Terminate ()
    {
        // Введите здесь код
        return new SqlDecimal(_accumulate);
    }

    public void Read(BinaryReader r)
    {
        _accumulate = r.ReadDecimal();
    }

    public void Write(BinaryWriter w)
    {
        w.Write(_accumulate);
    }
}
