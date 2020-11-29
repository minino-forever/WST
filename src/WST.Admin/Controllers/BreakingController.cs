using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WST.Admin.Infrastructure;
using WST.Admin.Models;
using WST.Admin.Models.Repositories;
using WST.Admin.Models.ViewModels;
using WST.Admin.Models.ViewModels.Breaking;

namespace WST.Admin.Controllers
{
    public class BreakingController : Controller
    {
        private readonly IMapper _mapper;
        
        private readonly IBreakingRepository _repository;
        
        private readonly IBreakingImageRepository _breakingImageRepository;

        public BreakingController(
            IMapper mapper,
            IBreakingRepository repository, 
            IBreakingImageRepository breakingImageRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _breakingImageRepository = breakingImageRepository;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var pageSize = PageInfoHelper.PageSize;
            
            var breakings = await _repository.Breakings
                .OrderBy(b => b.Description)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToArrayAsync();
            
            var pagingInfo = new PagingInfo
            {
                CurrentPage = page,
                ItemsPerPage = pageSize,
                TotalItems = await _repository.Breakings.CountAsync()
            };
            
            var breakingsListDto = new ListDto<Breaking>
            {
                Items = breakings,
                PagingInfo = pagingInfo
            };
            
            return View(breakingsListDto);
        }
        
        [HttpPost]
        public IActionResult Delete(Guid breakingId)
        {
            _repository.Delete(breakingId);

            TempData["message"] = "Запись была удалена";

            return RedirectToAction("Index");
        }      
        
        public async Task<IActionResult> Edit(Guid breakingId)
        {
            var breaking = _repository.Breakings.Single(p => p.Id == breakingId);


            var imageIds = await _breakingImageRepository.BreakingImages
                .Where(bi => bi.BreakingId == breakingId)
                .Select(bi => bi.Id)
                .ToArrayAsync();

            var imageUrls = imageIds.Select(id => $"get?breakingImageId={id}").ToArray();

            var formDto = _mapper.Map<BreakingFormDto>(breaking);
            
            formDto.ImageUrls = imageUrls;
            
            return View(formDto);
        }
        
        [HttpPost]
        public IActionResult Edit(BreakingFormDto breakingFormDto)
        {
            if (ModelState.IsValid)
            {
                var breaking = _mapper.Map<Breaking>(breakingFormDto);
                
                _repository.Save(breaking);
                
                if (breakingFormDto.Id == default)
                {
                    TempData["message"] = $"Запись была добавлена";    
                }
                else
                {
                    TempData["message"] = $"Запись была обновлена";
                }

                return RedirectToAction("Index");
            }
            
            return View(breakingFormDto);
        }

        public IActionResult Create()
        {
            return View("Edit", new BreakingFormDto { ImageUrls = new string[0]});
        }

        [HttpPost]
        public IActionResult Upload(Guid id, IFormFile[] files)
        {
            var breakingImages = new List<BreakingImage>();
            
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);

                        var newBreakingImage = new BreakingImage
                        {
                            BreakingId = id,
                            Image = ms.ToArray()
                        };
                        
                        breakingImages.Add(newBreakingImage);
                    }
                }
            }
            
            _breakingImageRepository.Save(breakingImages);

            return RedirectToAction("Edit", "Breaking", new { breakingId = id });
        }
        
        [HttpGet]
        [ResponseCache(Duration = 10 * 60 * 60)]
        public async Task<IActionResult> Get(Guid breakingImageId)
        {
            var imageBytes = await _breakingImageRepository
                .BreakingImages.Where(bi => bi.Id == breakingImageId)
                .Select(x => x.Image)
                .SingleAsync();

            using (var ms = new MemoryStream(imageBytes))
            {
                return File(imageBytes, "image/*");
            }
        }
    }
}