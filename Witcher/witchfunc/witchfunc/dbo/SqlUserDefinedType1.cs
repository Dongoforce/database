using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Text.RegularExpressions;



[Serializable]
[SqlUserDefinedType(Format.UserDefined, MaxByteSize = 8000)]
public struct Threat : INullable, IBinarySerialize
{
    private string _Threat;
    private bool _null;
    public override string ToString()
    {
        return _Threat;
    }
    public bool IsNull
    {
        get
        {
            return _null;
        }
    }
    public static Threat Null
    {
        get
        {
            Threat h = new Threat();
            h._null = true;
            return h;
        }
    }
    public static Threat Parse(SqlString s)
    {//Преобразование строки из SQL запроса в тип 
        if (s.IsNull)
            return Null;
        Threat u = new Threat();
        string str = s.ToString();
        if (isValidThreat(str))
        {
            u._Threat = str;
        }
        else
        {
            throw new Exception("Invalid data format");
        };
        return u;
    }
    private static bool isValidThreat(string Threat)
    {
        if (Threat == "low" || Threat == "medium" || Threat == "high" || Threat == "infernal")
        {
            return true;
        }
        else
            return false;
    }
    void IBinarySerialize.Read(System.IO.BinaryReader r)
    {
        this._Threat = r.ReadString();
    }
    void IBinarySerialize.Write(System.IO.BinaryWriter w)
    {
        w.Write(this._Threat);
    }
}
