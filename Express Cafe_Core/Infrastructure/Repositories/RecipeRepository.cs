using Express_Cafe_Core.Infrastructure.Base;
using Express_Cafe_Core.Infrastructure.INterfaces;
using Express_Cafe_Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Express_Cafe_Core.Infrastructure.Repositories
{
	public class RecipeRepository : GenericRepository<Recipe>, IRecipe
	{
		private readonly dbRestaurentContext db;
		public RecipeRepository(dbRestaurentContext context) : base(context)
		{
			this.db = context;
		}
		public System.Object GetAllRecipewithRawItemList()
		{
			var getRecipeItemList = from recipe in db.Recipes
									join recipeItems in db.RecipeItems
									on recipe.RecipeId equals recipeItems.RecipeId
									join item in db.Items
									on recipeItems.ItemId equals item.ItemId
									select new
									{
										recipe.RecipeId,
										recipe.RecipeName,
										item.Name,
										recipeItems.Quantity,
										recipeItems.Unit
									};

			return getRecipeItemList;
		}


	}
}