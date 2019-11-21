using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SqlClient;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;

namespace Laba7
{
    public class UserDataContext
    {
        public class UserDataContext1 : DataContext
        {
            public UserDataContext1(string connectionString) : base(connectionString)
            {

            }

            [Function(Name = "Getfactor")]
            [return: Parameter(DbType = "Int")]
            public int Getfactor(
                [Parameter(Name = "num", DbType = "Int")] ref int _num)
            {
                IExecuteResult result = this.ExecuteMethodCall(this, (MethodInfo)MethodInfo.GetCurrentMethod(), _num);
                _num = (int)result.GetParameterValue(0);

                return (int)result.ReturnValue;
            }
        }
    }
}