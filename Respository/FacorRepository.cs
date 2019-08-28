using System.Collections.Generic;
using System.Data;
using Microsoft.Data.Sqlite;
using System.Linq;
using Dapper;
using StoreTransferKit.Models;
using StoreTransferKit.Helper;

namespace StoreTransferKit.Repository
{
	public class FactorRespository : IFactorRepository
	{
		private readonly IDbConnection _db;

		public FactorRespository()
		{
			// _db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
			  _db = new SqliteConnection(ConfigurationHelper.ConnectionString);
		}

		public List<Factor> GetItems(int FactorId)
		{
			string sql =@"
			SELECT f.id as refID, f.amountPaid as amountPaid, f.code as code, f.totalPrice totalPrice, strftime('%Y-%m-%dT%H:%M:%S.0Z',f.salesDate) as salesDate,
			c.id as customer_id, c.lastName, c.code, c.mobile
				FROM factor f
			INNER JOIN customer c ON f.customer_id = c.id
			where f.id >  " + @FactorId.ToString() ;
			var items=_db.Query<Factor, Customer, Factor>(sql
				,(f, c) => {
                         f.customer = c;
                         return f;
                     },
					 splitOn : "customer_id"
                     ).AsQueryable().ToList();
			return items;
		}

		public int Count(int lessThan)
		{
			string lessCondition = lessThan > 0 ? " where id>" + lessThan : "";
			string sql = "SELECT count(*) FROM factor " + lessCondition;
			int count = _db.ExecuteScalar<int>(sql);
			return count;
		}
		
		public bool DeleteFactor(int FactorId)
		{
			int rowsAffected = this._db.Execute(@"DELETE FROM [Factor] WHERE FactorID = @FactorID",
				new { FactorID = FactorId });

			if (rowsAffected > 0)
			{
				return true;
			}

			return false;
		}

	}
}