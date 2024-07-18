using Express_Cafe_Core.Infrastructure.Base;
using Express_Cafe_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Express_Cafe_Core.Infrastructure.INterfaces
{
	public interface IRecipe:IGenericRepository<Recipe>
	{
		public System.Object GetAllRecipewithRawItemList();
	}
}
