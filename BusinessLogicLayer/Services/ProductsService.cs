using AutoMapper;
using Basboosify.BusinessLogicLayer.DTO;
using Basboosify.BusinessLogicLayer.ServiceContracts;
using Basboosify.DataAccessLayer.Entities;
using Basboosify.DataAccessLayer.RepositoryContracts;
using FluentValidation;
using FluentValidation.Results;
using System.Linq.Expressions;


namespace Basboosify.BusinessLogicLayer.Services;

public class ProductsService : IProductService
{
    private readonly IValidator<ProductAddRequest> _productAddRequestValidator;
    private readonly IValidator<ProductUpdateRequest> _productUpdateRequestValidator;
    private readonly IMapper _mapper;
    private readonly IProductsRepository _productsRepository;

    public ProductsService(IValidator<ProductAddRequest> productAddRequestValidator, IValidator<ProductUpdateRequest> productUpdateRequestValidator, IMapper mapper, IProductsRepository productsRepository)
    {
        _productAddRequestValidator = productAddRequestValidator;
        _productUpdateRequestValidator = productUpdateRequestValidator;
        _mapper = mapper;
        _productsRepository = productsRepository;
    }

    public async Task<ProductResponse?> AddProduct(ProductAddRequest productAddRequest)
    {
        if (productAddRequest == null) {
            throw new ArgumentNullException(nameof(productAddRequest));
        }

        //validate the product using fluent validations
        ValidationResult validationResult = await _productAddRequestValidator.ValidateAsync(productAddRequest);

        // check validation result
        if (!validationResult.IsValid)
        {
            string errors = string.Join(", ", validationResult.Errors.Select(x => x.ErrorMessage));
            throw new ArgumentException(errors);
        } 

        //add product
        Product product = _mapper.Map<Product>(productAddRequest); //map productAddRequest to Product
        Product? addedProduct = await _productsRepository.AddProduct(product);

        if ( addedProduct == null)
        {
            return null;
        }

        ProductResponse productResponse = _mapper.Map<ProductResponse>(addedProduct); //map Product to ProductResponse

        return productResponse;

    }

    public async Task<bool> DeleteProduct(Guid productID)
    {
        Product? productToDelete = await _productsRepository.GetProductByCondition(x => x.ProductID == productID);

        if (productToDelete == null) 
        { 
            return false;
        }

        bool isDeleted = await _productsRepository.DeleteProduct(productID);
        return isDeleted;
    }

    public async Task<ProductResponse?> GetProductByCondition(Expression<Func<Product, bool>> conditionExpression)
    {
       Product? product =  await _productsRepository.GetProductByCondition(conditionExpression);

       if (product == null) 
       {
            return null;
       }

        ProductResponse productResponse = _mapper.Map<ProductResponse>(product);
        return productResponse;
    }

    public async Task<List<ProductResponse?>> GetProducts()
    {
        IEnumerable<Product?> product = await _productsRepository.GetProducts();

        IEnumerable<ProductResponse?> productResponse = _mapper.Map<IEnumerable<ProductResponse>>(product);

        return productResponse.ToList();
    }

    public async Task<List<ProductResponse?>> GetProductsByCondition(Expression<Func<Product, bool>> conditionExpression)
    {
        IEnumerable<Product?> product = await _productsRepository.GetProductsByCondition(conditionExpression);

        IEnumerable<ProductResponse?> productResponse = _mapper.Map<IEnumerable<ProductResponse>>(product);

        return productResponse.ToList();
    }

    public async Task<ProductResponse?> UpdateProduct(ProductUpdateRequest productUpdateRequest)
    {

        Product? productToUpdate = await _productsRepository.GetProductByCondition(x => x.ProductID == productUpdateRequest.ProductID);

        if (productToUpdate == null)
        {
            throw new ArgumentException("Invalid Product ID");
        }


        //validate the product using fluent validations
        ValidationResult validationResult = await _productUpdateRequestValidator.ValidateAsync(productUpdateRequest);

        // check validation result
        if (!validationResult.IsValid)
        {
            string errors = string.Join(", ", validationResult.Errors.Select(x => x.ErrorMessage));
            throw new ArgumentException(errors);
        }

        //add product
        Product product = _mapper.Map<Product>(productUpdateRequest); //map productUpdateRequest to Product
        Product? updatedProduct = await _productsRepository.UpdateProduct(product);

        ProductResponse? productResponse = _mapper.Map<ProductResponse>(updatedProduct); //map Product to ProductResponse

        return productResponse;
    }
}
