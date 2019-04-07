using System.Collections.Generic;
using StoreTransferKit.Models;

namespace StoreTransferKit.Repository {
	public interface IFactorRepository {
		List<Factor> GetItems(int id);
		bool DeleteFactor(int id);
		int Count();

	}
}