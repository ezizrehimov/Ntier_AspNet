using Business.Services.Abstract;
using Business.ViewModels;
using Common.Entities;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Base;
using DataAccess.UnitofWork;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly IActionContextAccessor accessor;
        private readonly ICategoryRepository repository;
        private readonly IUnitofWork unitofWork;
        private ModelStateDictionary modelState;
        public CategoryService(IActionContextAccessor accessor, ICategoryRepository repository, IUnitofWork unitofWork)
        {
            this.accessor = accessor;
            this.repository = repository;
            this.unitofWork = unitofWork;
            modelState = accessor.ActionContext.ModelState;
        }
        public async Task<bool> CreateAsync(CategoryCreateVM model)
        {
            if (!modelState.IsValid)
                return false;
            Category category = new Category
            {
                Title = model.Title,
                CreatedAt = DateTime.Now,
            };
            await repository.Create(category);
            await unitofWork.CommitAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var category = await repository.GetAsync(id);
            if (category is null)
                return false;

            repository.Delete(category);
            await unitofWork.CommitAsync();
            return true;
        }

        public async Task<List<CategoryIndexVM>> GetAllAsync()
        {
            var categories = await repository.GetAllAsync();
            var categoriesVM = new List<CategoryIndexVM>();

            foreach (var item in categories)
            {
                categoriesVM.Add(new CategoryIndexVM { Title = item.Title, CreateAt = item.CreatedAt, Id = item.Id });
            }
            return categoriesVM;
        }

        public async Task<bool> UpdateAsync(CategoryUpdateVM model,int id)
        {
          

            if (!modelState.IsValid) 
                return false;

            var category = await repository.GetAsync(id);

            if (category is null)
                return false;

            category.Title = model.Title;

            await unitofWork.CommitAsync();
            return true; 


           
        }
    }
}
