using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace TaskWebAPI
{
    public interface IDapper
    {
        Task<T> ExecuteGetAllAsync<T>(string query, DynamicParameters sp_parames, CommandType commandType = CommandType.StoredProcedure);
        Task<List<int>> QueryAsync<T>(string spname, DynamicParameters param);
    }
}
