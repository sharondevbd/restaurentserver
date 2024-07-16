using Express_Cafe_Core.Infrastructure.Base;
using Express_Cafe_Core.Models;
using Express_Cafe_Core.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Express_Cafe_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RecipesController : ControllerBase
	{
		private IUnitOfWork unitofWork;
		ModelMessage modelsMessage;
		public RecipesController(IUnitOfWork unitofWork)
		{
			this.unitofWork = unitofWork;
			modelsMessage = new ModelMessage();
		}
		[HttpGet]
		public async Task<IEnumerable<Recipe>> GetAll()
		{
			IEnumerable<Recipe> all = new List<Recipe>();
			try
			{
				all = await this.unitofWork.RecipeRepo.GetAll(null, "RecipeItems");
			}

			catch (Exception ex)
			{
				all = new List<Recipe>();
			}

			return all;
		}
		[HttpPost]
		public async Task<IActionResult> PostRecipe(Recipe Recipe)
		{
			//var isExist =await this.unitofWork.RecipeRepo.GetAll(null, null).Result.Any(c => c.RecipeName == Recipe.RecipeName);
			//if (!isExist)
			//{
			await this.unitofWork.RecipeRepo.Add(Recipe);
			//void Add(List<T> entity);
			//if (Recipe.RecipeItems is not null)
			//{
			//	this.unitofWork.RecipeRepo.Add((List<RecipeItem>)Recipe.RecipeItems);
			//}
			var m = this.unitofWork.Save();

			if (m.IsSuccess)
			{
				return Ok(new { Data = Recipe, result = m });
			}
			else
			{
				return Problem(m.Message);

			}
			//}
			//else
			//{
			//    return Problem("Class name already exist.Try another one");
			//}
		}
		[HttpGet]
		[Route("{id:int}")]
		public Recipe GetByID(int id)
		{
			return this.unitofWork.RecipeRepo.GetT(i => i.RecipeId == id);
		}
		[HttpPut]
		public void PutRecipe(Recipe Recipe)
		{
			this.unitofWork.RecipeRepo.Update(Recipe);
			this.unitofWork.Save();

		}
		[HttpDelete]
		[Route("{id:int}")]
		public void Delete(int id)
		{
			this.unitofWork.RecipeRepo.DeletebyID(i => i.RecipeId == id);
			this.unitofWork.Save();

		}
		[HttpDelete]
		public void Delete(Recipe Recipe)
		{
			this.unitofWork.RecipeRepo.Delete(Recipe);
			this.unitofWork.Save();

		}
		[HttpDelete]
		[Route("DeleteRange")]
		public void DeleteRangs(IEnumerable<Recipe> Recipes)
		{
			this.unitofWork.RecipeRepo.DeleteRange(Recipes);
			this.unitofWork.Save();

		}
	}
}

//Ready