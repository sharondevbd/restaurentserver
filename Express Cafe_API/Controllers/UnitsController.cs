using Express_Cafe_Core.Infrastructure.Base;
using Express_Cafe_Core.Models;
using Express_Cafe_Core.Utility;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Express_Cafe_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[EnableCors("express")]
	public class UnitsController : ControllerBase
	{
		private IUnitOfWork unitofWork;
		ModelMessage modelsMessage;
		public UnitsController(IUnitOfWork unitofWork)
		{
			this.unitofWork = unitofWork;
			modelsMessage = new ModelMessage();
		}
		[HttpGet]
		public async Task<IEnumerable<Unit>> GetAll()
		{
			IEnumerable<Unit> all = new List<Unit>();
			try
			{
				all = await this.unitofWork.UnitRepo.GetAll(null, null);
			}
			catch (Exception ex)
			{
				all = new List<Unit>();
			}

			return all;
		}
		[HttpPost]
		public async Task<IActionResult> PostUnit(Unit Unit)
		{
			//var isExist =await this.unitofWork.UnitRepo.GetAll(null, null).Result.Any(c => c.UnitName == Unit.UnitName);
			//if (!isExist)
			//{
			this.unitofWork.UnitRepo.Add(Unit);
			var m = this.unitofWork.Save();

			if (m.IsSuccess)
			{
				return Ok(new { Data = Unit, result = m });
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
		public Unit GetByID(int id)
		{
			return this.unitofWork.UnitRepo.GetT(i => i.Id == id);
		}
		[HttpPut]
		public void Update(Unit Unit)
		{
			this.unitofWork.UnitRepo.Update(Unit);
			this.unitofWork.Save();

		}
		[HttpDelete]
		[Route("{id:int}")]
		public void Delete(int id)
		{
			this.unitofWork.UnitRepo.DeletebyID(i => i.Id == id);
			this.unitofWork.Save();

		}
		[HttpDelete]
		public void Delete(Unit Unit)
		{
			this.unitofWork.UnitRepo.Delete(Unit);
			this.unitofWork.Save();

		}
		[HttpDelete]
		[Route("DeleteRange")]
		public void DeleteRangs(IEnumerable<Unit> Units)
		{
			this.unitofWork.UnitRepo.DeleteRange(Units);
			this.unitofWork.Save();

		}
	}
}

