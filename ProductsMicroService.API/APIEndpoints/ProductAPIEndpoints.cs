using Basboosify.BusinessLogicLayer.DTO;
using Basboosify.BusinessLogicLayer.ServiceContracts;
using Basboosify.DataAccessLayer.Entities;
using FluentValidation;
using FluentValidation.Results;

namespace Basboosify.ProductsMicroService.API.APIEndpoints;

public static class ProductAPIEndpoints
{

    public static IEndpointRouteBuilder MapProductsAPIEndpoints(this IEndpointRouteBuilder app)
    {
        //GET /api/products
        app.MapGet("/api/products", async (IProductService productService) =>
        {
            List<ProductResponse?> products = await productService.GetProducts();

            return Results.Ok(products);
        });

        //GET /api/products/search/product-id/{productID}
        app.MapGet("/api/products/search/product-id/{ProductID:guid}", async (IProductService productService, Guid ProductID) =>
        {
            ProductResponse? product = await productService.GetProductByCondition(x => x.ProductID == ProductID);

            return Results.Ok(product);
        });

        //GET /api/products/search/{SearchString}
        app.MapGet("/api/products/search/{SearchString}", async (IProductService productService, string SearchString) =>
        {
            List<ProductResponse?> productsByName = await productService.GetProductsByCondition(x => x.ProductName != null && x.ProductName.Contains(SearchString, StringComparison.OrdinalIgnoreCase));

            List<ProductResponse?> productsByCategory = await productService.GetProductsByCondition(x => x.Category != null && x.Category.Contains(SearchString, StringComparison.OrdinalIgnoreCase));

            var products = productsByName.Union(productsByCategory);

            return Results.Ok(products);
        });

        //POST /api/products
        app.MapPost("/api/products", async (IProductService productService, IValidator<ProductAddRequest> productAddValidator, ProductAddRequest productAdd) =>
        {

            //validation
            ValidationResult validationResult = await productAddValidator.ValidateAsync(productAdd);

            //check validation res
            if (!validationResult.IsValid)
            {
                Dictionary<string, string[]> errors = validationResult.Errors
                                                            .GroupBy(x => x.PropertyName)
                                                            .ToDictionary(grp => grp.Key, 
                                                                grp => grp.Select(err => err.ErrorMessage)
                                                                .ToArray());

                 return Results.ValidationProblem(errors);
            }

            ProductResponse? product = await productService.AddProduct(productAdd);

            if (product != null)
            {
                return Results.Created($"/api/products/search/product-id/{product.ProductID}", product);
            }else
            {
                return Results.Problem("Error in adding product");
            }

        });

        //PUT /api/products
        app.MapPut("/api/products", async (IProductService productService, IValidator<ProductUpdateRequest> productUpdateValidator, ProductUpdateRequest productUpdate) =>
        {

            //validation
            ValidationResult validationResult = await productUpdateValidator.ValidateAsync(productUpdate);

            //check validation res
            if (!validationResult.IsValid)
            {
                Dictionary<string, string[]> errors = validationResult.Errors
                                                            .GroupBy(x => x.PropertyName)
                                                            .ToDictionary(grp => grp.Key,
                                                                grp => grp.Select(err => err.ErrorMessage)
                                                                .ToArray());

                return Results.ValidationProblem(errors);
            }

            ProductResponse? product = await productService.UpdateProduct(productUpdate);

            if (product != null)
            {
                return Results.Ok(product);
            }
            else
            {
                return Results.Problem("Error in updating product");
            }

        });

        //DELETE /api/products/{ProductID}
        app.MapDelete("/api/products/{ProductID:guid}", async (IProductService productService, Guid ProductID) =>
        {
          

            var isDeleted = await productService.DeleteProduct(ProductID);

            if (isDeleted)
            {
                return Results.Ok(true);
            }
            else
            {
                return Results.Problem("Error in deleting product");
            }

        });

        return app;
    } 
}
