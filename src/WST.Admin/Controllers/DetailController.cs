using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WST.Admin.Infrastructure;
using WST.Admin.Models;
using WST.Admin.Models.Repositories;
using WST.Admin.Models.ViewModels;

namespace WST.Admin.Controllers
{
    public class DetailController : Controller
    {
        private readonly IDetailRepository _repository;

        public DetailController(IDetailRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var pageSize = PageInfoHelper.PageSize;
            
            var details = await _repository.Details
                .OrderBy(d => d.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToArrayAsync();
            
            var pagingInfo = new PagingInfo
            {
                CurrentPage = page,
                ItemsPerPage = pageSize,
                TotalItems = await _repository.Details.CountAsync()
            };
            
            var detailListDto = new ListDto<Detail>
            {
                Items = details,
                PagingInfo = pagingInfo
            };
            
            return View(detailListDto);
        }

        [HttpPost]
        public IActionResult Delete(Guid detailId)
        {
            _repository.Delete(detailId);

            TempData["message"] = "Запись была удалена";

            return RedirectToAction("Index");
        }      
        
        public IActionResult Edit(Guid detailId)
        {
            var detail = _repository.Details.FirstOrDefault(d => d.Id == detailId);

            return View(detail);
        }
        
        [HttpPost]
        public IActionResult Edit(Detail detail)
        {
            if (ModelState.IsValid)
            {
                if (detail.Id == default)
                {
                    TempData["message"] = "Запись была добавлена";    
                }
                else
                {
                    TempData["message"] = "Запись была обновлена";
                }
                
                _repository.Save(detail);

                return RedirectToAction("Index");
            }

            return View(detail);
        }

        public IActionResult Create()
        {
            return View("Edit", new Detail());
        }
    }
}