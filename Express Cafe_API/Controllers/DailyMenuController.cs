using Express_Cafe_Core.Infrastructure.Base;
using Express_Cafe_Core.Models;
using Express_Cafe_Core.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Express_Cafe_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DailyMenuController : ControllerBase
	{
		private IUnitOfWork unitofWork;
		ModelMessage modelsMessage;
		public DailyMenuController(IUnitOfWork unitofWork)
		{
			this.unitofWork = unitofWork;
			modelsMessage = new ModelMessage();
		}
		[HttpGet]
		public async Task<IEnumerable<DailyMenu>> GetAll()
		{
			IEnumerable<DailyMenu> all = new List<DailyMenu>();
			try
			{
				all = await this.unitofWork.DailyMenuRepo.GetAll(null, "Recipe");
			}

			catch (Exception ex)
			{
				all = new List<DailyMenu>();
			}

			return all;
		}
		[HttpPost]
		public async Task<IActionResult> PostDailyMenu(DailyMenu DailyMenu)
		{
			//var isExist =await this.unitofWork.DailyMenuRepo.GetAll(null, null).Result.Any(c => c.DailyMenuName == DailyMenu.DailyMenuName);
			//if (!isExist)
			//{
			await this.unitofWork.DailyMenuRepo.Add(DailyMenu);
			var m = this.unitofWork.Save();

			if (m.IsSuccess)
			{
				return Ok(new { Data = DailyMenu, result = m });
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
		public DailyMenu GetByID(int id)
		{
			return this.unitofWork.DailyMenuRepo.GetT(i => i.DailyMenuId == id);
		}
		[HttpPut]
		public void PutDailyMenu(DailyMenu DailyMenu)
		{
			this.unitofWork.DailyMenuRepo.Update(DailyMenu);
			this.unitofWork.Save();

		}
		[HttpDelete]
		[Route("{id:int}")]
		public void Delete(int id)
		{
			this.unitofWork.DailyMenuRepo.DeletebyID(i => i.DailyMenuId == id);
			this.unitofWork.Save();

		}
		[HttpDelete]
		public void Delete(DailyMenu DailyMenu)
		{
			this.unitofWork.DailyMenuRepo.Delete(DailyMenu);
			this.unitofWork.Save();

		}
		[HttpDelete]
		[Route("DeleteRange")]
		public void DeleteRangs(IEnumerable<DailyMenu> DailyMenus)
		{
			this.unitofWork.DailyMenuRepo.DeleteRange(DailyMenus);
			this.unitofWork.Save();

		}
	}
}