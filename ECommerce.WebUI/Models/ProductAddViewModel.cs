using ECommerce.Domain.Entities;

namespace ECommerce.WebUI.Models;

public class ProductAddViewModel
{
    public Product Product { get; set; }
    public List<Category> Categories { get; set; }
}
