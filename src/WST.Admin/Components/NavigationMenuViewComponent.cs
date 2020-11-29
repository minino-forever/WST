using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WST.Admin.Models.ViewModels;

namespace WST.Admin.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedMenuItem = RouteData?.Values["controller"];

            return View(GetMenuItems());
        }

        private IEnumerable<MenuItemViewModel> GetMenuItems()
        {
            return new[]
            {
                new MenuItemViewModel
                {
                    Controller = "ElectricLocomotive",
                    Name = "Электровоз"
                },
                new MenuItemViewModel
                {
                    Controller = "Breaking",
                    Name = "Неисправности"
                },
                new MenuItemViewModel
                {
                    Controller = "Detail",
                    Name = "Детали"
                }
            };
        }
    }
}