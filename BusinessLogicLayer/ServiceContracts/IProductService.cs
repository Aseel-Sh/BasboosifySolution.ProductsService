
using Basboosify.BusinessLogicLayer.DTO;
using Basboosify.DataAccessLayer.Entities;
using System.Linq.Expressions;

namespace Basboosify.BusinessLogicLayer.ServiceContracts;

public interface IProductService
{
    /// <summary>
    /// retrieves the list of products from the products repo
    /// </summary>
    /// <returns>returns list of product response obj</returns>
    Task<List<ProductResponse?>> GetProducts();

    /// <summary>
    /// retrieves the list of products from the products repo based on conditions
    /// </summary>
    /// <param name="conditionExpression">expression that represents the condition to check</param>
    /// <returns>returns matching products</returns>
    Task<List<ProductResponse?>> GetProductsByCondition(Expression<Func<Product, bool>> conditionExpression);

    /// <summary>
    /// returns product from the products repo based on conditions
    /// </summary>
    /// <param name="conditionExpression">expression that represents the condition to check</param>
    /// <returns>returns matching product or null</returns>
    Task<ProductResponse?> GetProductByCondition(Expression<Func<Product, bool>> conditionExpression);

    /// <summary>
    /// adds product into table using repo
    /// </summary>
    /// <param name="productAddRequest"> product to insert</param>
    /// <returns> product after inserting or null if failed</returns>
    Task<ProductResponse?> AddProduct(ProductAddRequest productAddRequest);

    /// <summary>
    /// updates existing product based on product id
    /// </summary>
    /// <param name="productAddRequest"> product data to pdate</param>
    /// <returns> product after update or null if failed</returns>
    Task<ProductResponse?> UpdateProduct(ProductUpdateRequest productUpdateRequest);

    /// <summary>
    /// delets an existing product based on id
    /// </summary>
    /// <param name="productID"> product id to search and delete</param>
    /// <returns>true if deleted false if not</returns>
    Task<bool> DeleteProduct(Guid productID);
}
