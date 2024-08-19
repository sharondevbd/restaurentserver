using Express_Cafe_Core.Infrastructure.INterfaces;
using Express_Cafe_Core.Utility;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Express_Cafe_Core.Infrastructure.Base
{
    public interface IUnitOfWork:IDisposable
    {
         

#region property
        public ICategory? CategoryRepo { get; }
        public IItem? ItemRepo { get; }
        public IRecipe? RecipeRepo { get; }
        public IDailyMenu DailyMenuRepo { get; }
        public ILoginModelRepository? LoginModelRepository { get; }

		public IUnit? UnitRepo { get; }
		#endregion

		ModelMessage Save();
    }
}
