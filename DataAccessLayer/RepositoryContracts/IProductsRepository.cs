using Basboosify.DataAccessLayer.Entities;
using System.Linq.Expressions;

namespace Basboosify.DataAccessLayer.RepositoryContracts;

/// <summary>
/// Represents a repo for managing products table
/// </summary>
public interface IProductsRepository
{
    /// <summary>
    /// Retrieves all products asynchronously
    /// </summary>
    /// <returns>returns all products from the table</returns>
    Task<IEnumerable<Product>> GetProducts();
    /// <summary>
    /// retrieves all products based on the specified condition asynchronously
    /// </summary>
    /// <param name="conditionExpression"> the condition to filter products</param>
    /// <returns>returning a collection of matching products</returns>
    Task<IEnumerable<Product?>> GetProductsByCondition(Expression<Func<Product, bool>> conditionExpression);

    /// <summary>
    /// retrieves a single product based on the specified condition asynchronously 
    /// </summary>
    /// <param name="conditionExpression">the condition to filer product</param>
    /// <returns>returns a single product or null if not found</returns>
    Task<Product?> GetProductByCondition(Expression<Func<Product, bool>> conditionExpression);

    /// <summary>
    /// adds a new product into the products table asynchronously
    /// </summary>
    /// <param name="product">the product to be added</param>
    /// <returns>returns the added product object or null if unsuccessful</returns>
    Task<Product> AddProduct(Product product);

    /// <summary>
    /// updates an existing product asynchronously
    /// </summary>
    /// <param name="product">the product to be updated</param>
    /// <returns>returns the updated product or null if not found</returns>
    Task<Product?> UpdateProduct(Product product);

    /// <summary>
    /// deletes the product async
    /// </summary>
    /// <param name="product">the product id to be deleted</param>
    /// <returns>returns true if the deletion is sucessful, false otherwise</returns>
    Task<bool> DeleteProduct(Guid productID);
}
