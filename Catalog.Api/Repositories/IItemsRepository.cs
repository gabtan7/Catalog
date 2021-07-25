using Catalog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Repositories
{
    public interface IItemsRepository 
    {
        Task<Item> GetItemAsync(Guid id);

        Task<IEnumerable<Item>> GetItemAsync();

        Task CreateItemAsync(Item item);

        Task UpateItemAsync(Item item);
        Task DeleteItemAsync(Guid id);
    }
}
