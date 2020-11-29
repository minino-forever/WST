using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WST.Admin.Infrastructure;
using WST.Admin.Models;
using WST.Admin.Models.Repositories;
using WST.Admin.Models.ViewModels;
using WST.Admin.Services;

namespace WST.Admin.Controllers
{
    public class ElectricLocomotiveController : Controller
    {
        private readonly IElectricLocomotiveRepository _repository;

        public ElectricLocomotiveController(IElectricLocomotiveRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var pageSize = PageInfoHelper.PageSize;
            
            var locomotives = await _repository.ElectricLocomotives
                .OrderBy(el => el.SerialNumber)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToArrayAsync();
            
            var pagingInfo = new PagingInfo
            {
                CurrentPage = page,
                ItemsPerPage = pageSize,
                TotalItems = await _repository.ElectricLocomotives.CountAsync()
            };
            
            var electricLocomotiveListDto = new ListDto<ElectricLocomotive>
            {
                Items = locomotives,
                PagingInfo = pagingInfo
            };
            
            return View(electricLocomotiveListDto);
        }

        [HttpPost]
        public IActionResult Delete(Guid electricLocomotivesId)
        {
            _repository.Delete(electricLocomotivesId);

            TempData["message"] = $"Запись была удалена";

            return RedirectToAction("Index");
        }      
        
        public IActionResult Edit(Guid electricLocomotivesId)
        {
            var product = _repository.ElectricLocomotives.FirstOrDefault(p => p.Id == electricLocomotivesId);

            return View(product);
        }
        
        [HttpPost]
        public IActionResult Edit(ElectricLocomotive electricLocomotive)
        {
            if (ModelState.IsValid)
            {
                _repository.Save(electricLocomotive);
                
                if (electricLocomotive.Id == default)
                {
                    TempData["message"] = $"{electricLocomotive.SerialNumber} был добавлен";    
                }
                else
                {
                    TempData["message"] = $"{electricLocomotive.SerialNumber} был обновлен";
                }

                return RedirectToAction("Index");
            }

            return View(electricLocomotive);
        }

        public IActionResult Create()
        {
            return View("Edit", new ElectricLocomotive());
        }
    }
}