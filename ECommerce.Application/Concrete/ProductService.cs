using ECommerce.Application.Abstract;
using ECommerce.DataAccess.Abstract;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Concrete;

public class ProductService(IProductDal productDal) : IProductService
{

    private readonly IProductDal _productDal = productDal;

    public void Add(Product product)
    {
        _productDal.Add(product);
    }

    public void Delete(int id)
    {
        var product = _productDal.Get(x=>x.ProductId==id);
        _productDal.Delete(product);
    }

    public List<Product> GetAll()
    {
        return _productDal.GetList();
    }

    public List<Product> GetAllByCategory(int categoryId=0)
    {
        return _productDal.GetList(x => x.CategoryId == categoryId || categoryId==0);
    }

    public Product GetById(int id)
    {
        return _productDal.Get(x => x.ProductId == id);
    }

    public void Update(Product product)
    {
        _productDal.Update(product);
    }
}
