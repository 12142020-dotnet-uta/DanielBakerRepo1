using System;
using Microsoft.EntityFrameworkCore;
using StoreApp;
using Xunit;

namespace StoreAppMain.Tests
{
    public class UnitTest1
    {
        [Theory]
        [InlineData("test123", "test desc", 33.00, true)]
        [InlineData("Test Product 2", "This is the second test", 17.00, false)]
        public void CreateProductSavesAndAddsProductToDb(string name, string desc, decimal price, bool restricted)
        {
            var options = new DbContextOptionsBuilder<StoreDbContext>().UseInMemoryDatabase(databaseName: "TestDb").Options;

            Product p = new Product();
            using(var context = new StoreDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                StoreDataLayer repo = new StoreDataLayer(context);
                p = repo.CreateProduct(name, desc, price, restricted);
            }

            using(var context = new StoreDbContext(options))
            {
                StoreDataLayer repo = new StoreDataLayer(context);
                Product result = repo.CreateProduct(name, desc, price, restricted);
                Assert.Equal(p.ProductID, result.ProductID);
            }

        }

        [Theory]
        [InlineData("test123", "test desc", 33.00, true)]
        [InlineData("Test Product 2", "This is the second test", 17.00, false)]
        public void CreatesNewProductEachTime(string name, string desc, decimal price, bool restricted)
        {
            var options = new DbContextOptionsBuilder<StoreDbContext>().UseInMemoryDatabase(databaseName: "TestDb").Options;

            Product p = new Product();
            using(var context = new StoreDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                StoreDataLayer repo = new StoreDataLayer(context);
                p = repo.CreateProduct(name, desc, price, restricted);
            }

            using(var context = new StoreDbContext(options))
            {
                StoreDataLayer repo = new StoreDataLayer(context);
                Product result = repo.CreateProduct("name", "desc", 26.00M, false);
                Assert.NotEqual(p.ProductID, result.ProductID);
            }

        }

        [Theory]
        [InlineData("Test Store", "Dallas")]
        [InlineData("WalTarg", "Plano")]
        public void CreateStoreSavesAndAddsProductToDb(string name, string address)
        {
            var options = new DbContextOptionsBuilder<StoreDbContext>().UseInMemoryDatabase(databaseName: "TestDb").Options;

            StoreLocation s = new StoreLocation();
            using(var context = new StoreDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                StoreDataLayer repo = new StoreDataLayer(context);

                s = repo.CreateStore(name, address);
            }

            using(var context = new StoreDbContext(options))
            {
                StoreDataLayer repo = new StoreDataLayer(context);
                StoreLocation result = repo.CreateStore(name, address);
                Assert.Equal(s.StoreLocationId, result.StoreLocationId);
            }

        }

        [Theory]
        [InlineData("Test Store", "Dallas")]
        [InlineData("WalTarg", "Plano")]
        public void CreatesNewStoreEachTime(string name, string address)
        {
            var options = new DbContextOptionsBuilder<StoreDbContext>().UseInMemoryDatabase(databaseName: "TestDb").Options;

            StoreLocation s = new StoreLocation();
            using(var context = new StoreDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                StoreDataLayer repo = new StoreDataLayer(context);

                s = repo.CreateStore(name, address);
            }

            using(var context = new StoreDbContext(options))
            {
                StoreDataLayer repo = new StoreDataLayer(context);
                StoreLocation result = repo.CreateStore("TargWal", "San Diego");
                Assert.NotEqual(s.StoreLocationId, result.StoreLocationId);
            }

        }

        [Theory]
        [InlineData(10)]
        [InlineData(20)]
        public void AddsQuantityToInventory(int quantity)
        {
            var options = new DbContextOptionsBuilder<StoreDbContext>().UseInMemoryDatabase(databaseName: "TestDb").Options;

            Inventory inventory = new Inventory();
            StoreLocation store = new StoreLocation();
            Product product = new Product();
            using(var context = new StoreDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                StoreDataLayer repo = new StoreDataLayer(context);

                store = repo.CreateStore("TargWal", "San Diego");
                product = repo.CreateProduct("name", "desc", 26.00M, false);

                inventory = repo.AssignInventory( product, store, quantity);

            }

            using(var context = new StoreDbContext(options))
            {
                StoreDataLayer repo = new StoreDataLayer(context);
                Inventory invResult = repo.AssignInventory( product, store, 20);
                int result = quantity + 20;

                Assert.Equal(invResult.ProductQuantity, result);
            }

        }


    }
}
