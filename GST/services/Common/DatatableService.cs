using models.ViewModels;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GST.Controllers;

namespace services.Common
{
    public class DatatableService
    {
        public DataTable<T> GetDataTableResult<T>(string procedureName, DataTableSearch search, List<MySqlParameter> filters)
        {
            using (var context = new AppDb())
            {
                string connectionstring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                MySqlConnection sql = new MySqlConnection(connectionstring);
                MySqlCommand cmd = new MySqlCommand(procedureName, sql)
                {
                    CommandType = System.Data.CommandType.StoredProcedure,
                    // CommandTimeout=0
                };


                cmd.Parameters.Clear();

                cmd.Parameters.Add(CreateSqlParameter("@pStart", search.start, MySqlDbType.Int32));
                cmd.Parameters.Add(CreateSqlParameter("@pSize", search.length, MySqlDbType.Int32));

                foreach (var param in filters)
                {
                    cmd.Parameters.Add(param);
                }

                sql.Open();
                var reader = cmd.ExecuteReader();
                //Getting records
                var result = ((IObjectContextAdapter)context).ObjectContext.Translate<T>(reader).ToList();

                reader.NextResult();
                double? Count = 0;
                if (reader.HasRows)
                {
                    Count = ((IObjectContextAdapter)context).ObjectContext.Translate<double>(reader).FirstOrDefault();
                }

                sql.Close();
                cmd.Dispose();

                return new DataTable<T>
                {
                    draw = search.draw,
                    recordsTotal = result.Count(),
                    recordsFiltered = result.Count > 0 ? Count : result.Count,
                    data = result
                };

            }
        }

        public T GetSPFirstRecord<T>(string procedureName, List<MySqlParameter> filters)
        {
            using (var context = new AppDb())
            {
                string connectionstring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                MySqlConnection sql = new MySqlConnection(connectionstring);
                MySqlCommand cmd = new MySqlCommand(procedureName, sql)
                {
                    CommandType = System.Data.CommandType.StoredProcedure,
                    // CommandTimeout=0
                };


                cmd.Parameters.Clear();
                foreach (var param in filters)
                {
                    cmd.Parameters.Add(param);
                }

                sql.Open();
                var reader = cmd.ExecuteReader();
                //Getting records
                var result = ((IObjectContextAdapter)context).ObjectContext.Translate<T>(reader).FirstOrDefault();
                sql.Close();
                cmd.Dispose();
                return result;
            }
        }


        public MySqlParameter CreateSqlParameter(string name, object value, MySqlDbType type)
        {
            MySqlParameter sqlParameter = new MySqlParameter(name, type);
            if (value == null)
            {
                sqlParameter.Value = DBNull.Value;
            }
            else
            {
                sqlParameter.Value = value;
            }
            return sqlParameter;
        }

    }
}
