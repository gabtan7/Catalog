using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Entities;
namespace Catalog.Repositories

{
    public class InMemItemRepository : IItemsRepository
    {
        //private readonly List<Item> items = new()
        //{
        //    new Item { Id = Guid.NewGuid(), Name = "Potion", Price = 9, CreatedDate = DateTimeOffset.UtcNow },
        //    new Item { Id = Guid.NewGuid(), Name = "Iron Sword", Price = 20, CreatedDate = DateTimeOffset.UtcNow },
        //    new Item { Id = Guid.NewGuid(), Name = "Bronze Shield", Price = 18, CreatedDate = DateTimeOffset.UtcNow }
        //};

        //public IEnumerable<Item> GetItemAsync()
        //{
        //    return items;
        //}

        //public Item GetItemAsync(Guid id)
        //{
        //    return items.Where(item => item.Id == id).SingleOrDefault();
        //}

        //public void CreateItemAsync(Item item)
        //{
        //    items.Add(item);
        //}

        //public void UpateItemAsync(Item item)
        //{
        //    var index = items.FindIndex(existingItem => existingItem.Id == item.Id);
        //    items[index] = item;
        //}

        //public void DeleteItemAsync(Guid id)
        //{
        //    var index = items.FindIndex(existingItem => existingItem.Id == id);
        //    items.RemoveAt(index);
        //}
        public Task CreateItemAsync(Item item)
        {
            throw new NotImplementedException();
        }

        public Task DeleteItemAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Item> GetItemAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Item>> GetItemAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpateItemAsync(Item item)
        {
            throw new NotImplementedException();
        }
    }
}