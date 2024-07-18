using Express_Cafe_Core.Models;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Express_Cafe_Core.Infrastructure.Base
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly dbRestaurentContext _context;
        private DbSet<T> _dbset;
        public GenericRepository(dbRestaurentContext context)
        {
            _context = context;
            _dbset = _context.Set<T>();
        }
        public virtual async Task  Add(T entity)
        {
          await  _dbset.AddAsync(entity);
        }

        public async void Add(List<T> entity)
        {
            await _dbset.AddRangeAsync(entity);
        }

        public void Delete(T entity)
        {
            _dbset.Remove(entity);
        }

        public void DeletebyID(Expression<Func<T, bool>> predicate)
        {
            var entity = _dbset.Where(predicate).FirstOrDefault();
            if(entity!=null)
            { _dbset.Remove(entity); }
            
        }

        public void DeleteRange(IEnumerable<T> entitylist)
        {
            _dbset.RemoveRange(entitylist);
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await _dbset.ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? predicate, string? includeProperties)
        {
            IQueryable<T> query = _dbset;
            try
            {
                if (predicate != null)
                {
                    query = query.Where(predicate);
                }
                if (includeProperties != null)
                {
                    foreach (var item in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(item);
                    }
                }
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                return await query.ToListAsync();

            }
        }

		//public object GetAllRecipewithRawItemList()
		//{
		//    var getRecipeItemList = from recipe in _context.Recipes
		//                            join recipeItems in _context.RecipeItems
		//                            on recipe.RecipeId equals recipeItems.RecipeId
		//                            join item in _context.Items
		//                            on recipeItems.ItemId equals item.ItemId
		//                            select new
		//                            {
		//                                recipe.RecipeId,
		//                                recipe.RecipeName,
		//                                item.Name,
		//                                recipeItems.Quantity,
		//                                recipeItems.Unit
		//                            };

		//    return getRecipeItemList;
		//}
		public async Task<IEnumerable<object>> GetAllRecipewithRawItemList()
		{
			var getRecipeItemList = await (from recipe in _context.Recipes
									join recipeItems in _context.RecipeItems
									on recipe.RecipeId equals recipeItems.RecipeId
									join item in _context.Items
									on recipeItems.ItemId equals item.ItemId
									select new
									{
										recipe.RecipeId,
										recipe.RecipeName,
										item.Name,
										recipeItems.Quantity,
										recipeItems.Unit
									}).ToListAsync();
          //  var groupRecipes = getRecipeItemList.GroupBy(r => r.RecipeName)
          //                        .Select(group => new
          //                        {
									 // RecipeName=group.Key,
									 // Ingredients = group
          //                            .Select(item => new { item.Name, item.Quantity })
								  //});

			return getRecipeItemList;
		}
		

		public Task<T> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public T GetT(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return _dbset.Where(predicate).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void Update(T entity)
        {
            _context.Update(entity);
        }

		//public object GetAllRecipewithRawItemList()
		//{
		//	var getRecipeItemList = from recipe in _context.Recipes
		//							join recipeItems in _context.RecipeItems
		//							on recipe.RecipeId equals recipeItems.RecipeId
		//							join item in _context.Items
		//							on recipeItems.ItemId equals item.ItemId
		//							select new
		//							{
		//								recipe.RecipeId,
		//								recipe.RecipeName,
		//								item.Name,
		//								recipeItems.Quantity,
		//								recipeItems.Unit
		//							};

		//	return getRecipeItemList;
		//}
	}
	//Custom Quries


}

