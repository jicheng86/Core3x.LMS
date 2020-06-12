using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using LMS.IService.IServices;
using LMS.Web.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LMS.Web.Controllers
{
    public class SharedController : Controller
    {

        public SharedController(IMapper mapper, IAreaService areaService, ICorporationService corporationService)
        {
            AreaService = areaService ?? throw new ArgumentNullException(nameof(areaService));
            CorporationService = corporationService ?? throw new ArgumentNullException(nameof(corporationService));
            AutoMapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public IAreaService AreaService { get; }
        public ICorporationService CorporationService { get; }
        public IMapper AutoMapper { get; }

        /// <summary>
        /// 获取Area SelectListItem数据
        /// </summary>
        /// <param name="AreaLevel">父级code</param>
        /// <param name="SelectedID">默认选中值</param>
        /// <param name="HadEmptyItem">是否包含：“请选择”空选项</param>
        /// <returns></returns>
        [HttpGet]
        public List<SelectListItem> AreasSelectListItem(string AreaLevel, int SelectedID, bool HadEmptyItem = false)
        {
            if (string.IsNullOrWhiteSpace(AreaLevel))
            {
                AreaLevel = EnumCollection.AreaLevel.country.ToString();
            }
            List<SelectListItem> items = new List<SelectListItem>();
            if (HadEmptyItem)
            {
                items.Add(new SelectListItem { Value = string.Empty, Text = "请选择" });
            }
            if (SelectedID < 1)
            {
                SelectedID = 3689;//广东省
            }
            items.AddRange(AreaService.GetEntityListAsync(a => a.AreaLevel == AreaLevel).Result.Select(a => new SelectListItem() { Value = a.ID.ToString(), Text = a.Name, Selected = a.ID == SelectedID }).ToList());
            return items;
        }


        /// <summary>
        /// 获取Area SelectListItem数据
        /// </summary>
        /// <param name="ParentCode">父级code</param>
        /// <param name="SelectedID">默认选中值</param>
        /// <param name="HadEmptyItem">是否包含：“请选择”空选项</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<SelectListItem>> GetAreasSelectOptions(string ParentCode, int SelectedID, bool HadEmptyItem = false)
        {
            return AreasSelectListItem(ParentCode, SelectedID, HadEmptyItem);
        }
    }
}
