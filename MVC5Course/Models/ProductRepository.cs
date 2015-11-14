using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVC5Course.Models
{   
	public  class ProductRepository : EFRepository<Product>, IProductRepository
	{

        public IQueryable<Product> GetBySearchKey(string search)
        {
            if (!string.IsNullOrEmpty(search))
                return this.All().Where(p => p.ProductName.Contains(search));
            else
                return null;
            //throw new NotImplementedException();
        }

        public Product GetById(int? id)
        {
            return this.All().FirstOrDefault(p => p.ProductId == id.Value);
            //throw new NotImplementedException();
        }

        public IQueryable<Product> Get���o�e��10���d�Ҹ��()
        {
            return this.All().Where(p => p.ProductId < 10);
            //throw new NotImplementedException();
        }
    }

	public  interface IProductRepository : IRepository<Product>
	{

	}
}