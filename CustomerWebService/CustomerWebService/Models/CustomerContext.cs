using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteDB;
using Microsoft.Extensions.Options;

namespace CustomerWebService.Models
{
    public class CustomerContext
    {
        public readonly LiteDatabase Context;
        public CustomerContext(IOptions<LiteDbConfig> configs)
        {
            try
            {
                var db = new LiteDatabase(configs.Value.DatabasePath);
                if (db != null)
                    Context = db;
            }
            catch (Exception ex)
            {
                throw new Exception("Can find or create LiteDb database.", ex);
            }
        }
    }
}
