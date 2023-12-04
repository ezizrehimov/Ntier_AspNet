using Business.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract
{
    public interface ICategoryService
    {
        Task<bool> CreateAsync(CategoryCreateVM model);
        Task<List<CategoryIndexVM>> GetAllAsync();

        Task<bool> UpdateAsync(CategoryUpdateVM model,int id);

        Task<bool> Delete(int id);
    }
}
