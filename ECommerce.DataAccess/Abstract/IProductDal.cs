﻿using ECommerce.Domain.Entities;
using ECommerce.Repository;

namespace ECommerce.DataAccess.Abstract;

public interface IProductDal:IEntityRepository<Product>
{

}