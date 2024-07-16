using Microsoft.EntityFrameworkCore;

using Express_Cafe_Core.Models;
using Express_Cafe_Core.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Express_Cafe_Core.Infrastructure.INterfaces;
using Express_Cafe_Core.Infrastructure.Repositories;

namespace Express_Cafe_Core.Infrastructure.Base
{
    public class UnitOFWork : IUnitOfWork
    {
        private readonly dbRestaurentContext _context;
        public UnitOFWork(dbRestaurentContext context)
        {
            this._context = context;
        }
        #region property

        public ICategory? categoryRepo;
        public ICategory CategoryRepo
        {
            get
            {
                if (categoryRepo == null)
                {
                    categoryRepo = new CategoryRepository(_context);
                }
                return categoryRepo;
            }
        }

		public IItem? itemRepo;
		public IItem ItemRepo
		{
			get
			{
				if (itemRepo == null)
				{
					itemRepo = new ItemRepository(_context);
				}
				return itemRepo;
			}
		}
		public IRecipe? recipeRepo;
		public IRecipe RecipeRepo
		{
			get
			{
				if (recipeRepo == null)
				{
					recipeRepo = new RecipeRepository(_context);
				}
				return recipeRepo;
			}
		}

	
		public ILoginModelRepository? loginModelRepository;
        public ILoginModelRepository? LoginModelRepository
        {
            get
            {
                if (loginModelRepository == null)
                {
                    loginModelRepository = new LoginModelRepository(_context);
                }
                return loginModelRepository;
            }
        }

        public IUnit unitRepo;
        public  IUnit UnitRepo
		{
            get
            {
                if(unitRepo == null)
                {
                    unitRepo = new UnitRepository(_context);
                }
                return unitRepo;
            }
        }

		#endregion


		public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }

        public ModelMessage Save()
        {
            ModelMessage modelMessage = new ModelMessage();
            //string msg = "";
            try
            {
                if (_context.SaveChanges() > 0)
                {
                    modelMessage.Message = $"Action committed Successfully ";
                    modelMessage.IsSuccess = true;
                }
                else
                {
                    modelMessage.Message = "Action Failed";
                    modelMessage.IsSuccess = false;
                }
            }

            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    //modelMessage.Message = ex.InnerException.InnerException.Message;
                    modelMessage.Message = ex.InnerException.Message;
                    modelMessage.IsSuccess = false;
                }
                else
                {
                    modelMessage.Message = ex.Message;
                    modelMessage.IsSuccess = false;
                }
            }
            return modelMessage;
        }
    }
}
