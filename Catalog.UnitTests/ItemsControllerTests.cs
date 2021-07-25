using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.Controller;
using Catalog.Dtos;
using Catalog.Entities;
using Catalog.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FluentAssertions;
using Moq;
using Xunit;
namespace Catalog.UnitTests
{
    public class UnitTest1
    {
        private readonly Mock<IItemsRepository> repositoryStub = new();
        private readonly Mock<ILogger<ItemController>> loggerStub = new();
        private readonly Random rand = new();

        [Fact]
        public async Task GetItemAsync_WithNonExistingItem_ReturnsNotFound()
        {
            //Arrange
            repositoryStub.Setup(repo => repo.GetItemAsync(It.IsAny<Guid>())).ReturnsAsync((Item)null);

            var controller = new ItemController(repositoryStub.Object, loggerStub.Object);

            //Act
            var result = await controller.GetItemAsync(Guid.NewGuid());
            
            //Asert
            result.Result.Should().BeOfType<NotFoundResult>();
        }        
        
        [Fact]
        public async Task GetItemAsync_WithExistingItem_ReturnsExpectedItem()
        {
            //Arrange
            var expectedItem = CreateRandomItem();

            repositoryStub.Setup(repo => repo.GetItemAsync(It.IsAny<Guid>())).ReturnsAsync(expectedItem);

            var controller = new ItemController(repositoryStub.Object, loggerStub.Object);

            //Act
            var result = await controller.GetItemAsync(Guid.NewGuid());
            
            //Asert
            result.Value.Should().BeEquivalentTo(expectedItem);
        }

        [Fact]
        public async Task GetItemAsync_WithExistingItem_ReturnsAllItems()
        {
            //Arrange
            var expectedItems = new[]{ CreateRandomItem(), CreateRandomItem(), CreateRandomItem() };

            repositoryStub.Setup(repo => repo.GetItemAsync()).ReturnsAsync(expectedItems);

            var controller = new ItemController(repositoryStub.Object, loggerStub.Object);

            //Act
            var result = await controller.GetItemAsync();
            
            //Asert
            result.Should().BeEquivalentTo(expectedItems);
        } 
        
        [Fact]
        public async Task GetItemAsync_WithMatchingItem_ReturnsMatchingItems()
        {
            //Arrange
            var allItems = new[]
            { 
                new Item(){ Name = "Potion"},
                new Item(){ Name = "Antidote"},
                new Item(){ Name = "Hi-Potion"}
            };

            var nameToMatch = "Potion";

            repositoryStub.Setup(repo => repo.GetItemAsync()).ReturnsAsync(allItems);

            var controller = new ItemController(repositoryStub.Object, loggerStub.Object);

            //Act
            IEnumerable<ItemDto> foundItems = await controller.GetItemAsync(nameToMatch);
            
            //Asert
            foundItems.Should().OnlyContain(
                item => item.Name == allItems[0].Name || item.Name == allItems[2].Name
            );
        }


        [Fact]
        public async Task CreateItemAsync_WithItemToCreate_ReturnsCreatedItems()
        {
            //Arrange
            var itemToCreate = new CreateItemDto(
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                rand.Next(1000)
            );

            var controller = new ItemController(repositoryStub.Object, loggerStub.Object);

            //Act
            var result = await controller.CreateItem(itemToCreate);

            //Asert
            var createdItem = (result.Result as CreatedAtActionResult).Value as ItemDto;
            itemToCreate.Should().BeEquivalentTo(createdItem, options => options.ComparingByMembers<ItemDto>().ExcludingMissingMembers());
            createdItem.Id.Should().NotBeEmpty();
            createdItem.CreatedDate.Should().BeCloseTo(DateTimeOffset.UtcNow, 1000);
        }

        [Fact]
        public async Task UpdateItemAsync_WithExistingItem_ReturnsNoContent()
        {
          Item existingItem = CreateRandomItem();
            repositoryStub.Setup(repo => repo.GetItemAsync(It.IsAny<Guid>()))
                .ReturnsAsync(existingItem);

            var itemId = existingItem.Id;

            var itemToUpdate = new UpdateItemDto(
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                existingItem.Price + 3
            );

            var controller = new ItemController(repositoryStub.Object, loggerStub.Object);

            // Act
            var result = await controller.UpdateItem(itemId, itemToUpdate);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task DeleteItemAsync_WithExistingItem_ReturnsNoContent()
        {
          Item existingItem = CreateRandomItem();
            repositoryStub.Setup(repo => repo.GetItemAsync(It.IsAny<Guid>()))
                .ReturnsAsync(existingItem);

            var controller = new ItemController(repositoryStub.Object, loggerStub.Object);

            // Act
            var result = await controller.DeleteItem(existingItem.Id);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        private Item CreateRandomItem()
        {
            return new()
            {
                Id = Guid.NewGuid(),
                Name = Guid.NewGuid().ToString(),
                Price = rand.Next(1000),
                CreatedDate = DateTimeOffset.UtcNow
            };
        }
    }
}
