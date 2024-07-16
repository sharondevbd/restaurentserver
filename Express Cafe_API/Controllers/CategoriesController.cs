using Express_Cafe_Core.Infrastructure.Base;
using Express_Cafe_Core.Models;
using Express_Cafe_Core.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Express_Cafe_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("express")]
   // [Authorize]
    public class CategoriesController : ControllerBase
    {
        private IUnitOfWork unitofWork;
        ModelMessage modelsMessage;
        public CategoriesController(IUnitOfWork unitofWork)
        {
            this.unitofWork = unitofWork;
            modelsMessage = new ModelMessage();
        }
        [HttpGet]
        public async Task<IEnumerable<Category>> GetAll()
        {
            IEnumerable<Category> all = new List<Category>();
            try
            {
                all = await this.unitofWork.CategoryRepo.GetAll(null, null);
            }
            catch (Exception ex)
            {
                all = new List<Category>();
            }

            return all;
        }
        [HttpPost]
        public async Task<IActionResult> PostCategory(Category Category)
        {
            //var isExist =await this.unitofWork.CategoryRepo.GetAll(null, null).Result.Any(c => c.CategoryName == Category.CategoryName);
            //if (!isExist)
            //{
            await this.unitofWork.CategoryRepo.Add(Category);
            var m = this.unitofWork.Save();

            if (m.IsSuccess)
            {
                return Ok(new { Data = Category, result = m });
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
        public Category GetByID(int id)
        {
            return this.unitofWork.CategoryRepo.GetT(i => i.Id == id);
        }
        [HttpPut]
        public void PutCategory(Category Category)
        {
            this.unitofWork.CategoryRepo.Update(Category);
            this.unitofWork.Save();

        }
        [HttpDelete]
        [Route("{id:int}")]
        public void Delete(int id)
        {
            this.unitofWork.CategoryRepo.DeletebyID(i => i.Id == id);
            this.unitofWork.Save();

        }
        [HttpDelete]
        public void Delete(Category Category)
        {
            this.unitofWork.CategoryRepo.Delete(Category);
            this.unitofWork.Save();

        }
        [HttpDelete]
        [Route("DeleteRange")]
        public void DeleteRangs(IEnumerable<Category> Categorys)
        {
            this.unitofWork.CategoryRepo.DeleteRange(Categorys);
            this.unitofWork.Save();

        }
    }
}