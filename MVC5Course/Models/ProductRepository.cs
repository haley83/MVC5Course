using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVC5Course.Models
{   
	public  class ProductRepository : EFRepository<Product>, IProductRepository
	{
        public override IQueryable<Product> All()
        {
            return base.All().Where(p => p.Active == true);
        }

        public IQueryable<Product> All(bool IsGetAll)
        {
            if (IsGetAll)
                return base.All();
            else
                return this.All();
        } 

        public IQueryable<Product> GetBySearchKey(string search)
        {
            if (!string.IsNullOrEmpty(search))
                return this.All(true).Where(p => p.ProductName.Contains(search));
            else
                return this.Get取得前面n筆範例資料(10);
            //throw new NotImplementedException();
        }

        public Product GetById(int? id)
        {
            return this.All().FirstOrDefault(p => p.ProductId == id.Value);
            //throw new NotImplementedException();
        }

        public IQueryable<Product> Get取得前面n筆範例資料(int n)
        {
            //return this.All().Where(p => p.ProductId < 10);
            return this.All().OrderBy(p => p.ProductId).Take(n);
            //throw new NotImplementedException();
        }

        public override void Delete(Product entity)
        {
            var db = ((FabricsEntities)base.UnitOfWork.Context);
            db.OrderLine.RemoveRange(entity.OrderLine);
            base.Delete(entity);
        }
    }

	public  interface IProductRepository : IRepository<Product>
	{

	}
}