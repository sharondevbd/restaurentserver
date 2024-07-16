using Express_Cafe_Core.Infrastructure.Base;
using Express_Cafe_Core.Models;
using Express_Cafe_Core.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Express_Cafe_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ItemsController : ControllerBase
	{
		private IUnitOfWork unitofWork;
		ModelMessage modelsMessage;
		public ItemsController(IUnitOfWork unitofWork)
		{
			this.unitofWork = unitofWork;
			modelsMessage = new ModelMessage();
		}
		[HttpGet]
		public async Task<IEnumerable<Item>> GetAll()
		{
			IEnumerable<Item> all = new List<Item>();
			try
			{
				all = await this.unitofWork.ItemRepo.GetAll(null, null);
			}
			
			catch (Exception ex)
			{
				all = new List<Item>();
			}

			return all;
		}
		[HttpPost]
		public async Task<IActionResult> PostItem(Item Item)
		{
			//var isExist =await this.unitofWork.ItemRepo.GetAll(null, null).Result.Any(c => c.ItemName == Item.ItemName);
			//if (!isExist)
			//{
			await this.unitofWork.ItemRepo.Add(Item);
			var m = this.unitofWork.Save();

			if (m.IsSuccess)
			{
				return Ok(new { Data = Item, result = m });
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
		public Item GetByID(int id)
		{
			return this.unitofWork.ItemRepo.GetT(i => i.ItemId == id);
		}
		[HttpPut]
		public void PutItem(Item Item)
		{
			this.unitofWork.ItemRepo.Update(Item);
			this.unitofWork.Save();

		}
		[HttpDelete]
		[Route("{id:int}")]
		public void Delete(int id)
		{
			this.unitofWork.ItemRepo.DeletebyID(i => i.ItemId == id);
			this.unitofWork.Save();

		}
		[HttpDelete]
		public void Delete(Item Item)
		{
			this.unitofWork.ItemRepo.Delete(Item);
			this.unitofWork.Save();

		}
		[HttpDelete]
		[Route("DeleteRange")]
		public void DeleteRangs(IEnumerable<Item> Items)
		{
			this.unitofWork.ItemRepo.DeleteRange(Items);
			this.unitofWork.Save();

		}
	}
}