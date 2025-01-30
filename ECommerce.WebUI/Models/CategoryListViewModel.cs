using ECommerce.Domain.Entities;

namespace ECommerce.WebUI;

public class CategoryListViewModel
{
    public List<Category> Categories { get; set; }
    public int CurrentCategory { get; set; }
}