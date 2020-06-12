using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        //public SharedController(IMapper mapper, IAreaService areaService, ICorporationService corporationService)
        //{
        //    AreaService = areaService ?? throw new ArgumentNullException(nameof(areaService));
        //    CorporationService = corporationService ?? throw new ArgumentNullException(nameof(corporationService));
        //    AutoMapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        //}

        public IAreaService AreaService { get; }
        public ICorporationService CorporationService { get; }
        public IMapper AutoMapper { get; }

        /// <summary>
        /// 获取Area SelectListItem数据
        /// </summary>
        /// <param name="ParentCode">父级code</param>
        /// <param name="SelectedID">默认选中值</param>
        /// <param name="HadEmptyItem">是否包含：“请选择”空选项</param>
        /// <returns></returns>
        [HttpGet]
        public List<SelectListItem> AreasSelectListItem(string ParentCode = "100000", int SelectedID = 0, bool HadEmptyItem = false)
        {

            List<SelectListItem> items = new List<SelectListItem>();
            if (HadEmptyItem)
            {
                items.Add(new SelectListItem { Value = string.Empty, Text = "请选择" });
            }
            if (ParentCode == "100000" && SelectedID < 1)
            {
                SelectedID = 3689;//广东省
            }
            items.AddRange(AreaService.GetEntityListAsync(a => a.ParentCode == ParentCode).Result.Select(a => new SelectListItem() { Value = a.ID.ToString(), Text = a.Name, Selected = a.ID == SelectedID }).ToList());
            return items;
        }


        /// <summary>
        /// 获取Area SelectListItem数据
        /// </summary>
        /// <param name="ParentCode">父级code</param>
        /// <param name="SelectedID">默认选中值</param>
        /// <param name="HadEmptyItem">是否包含：“请选择”空选项</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetAreasSelectOptions(string ParentCode, int SelectedID, bool HadEmptyItem = false)
        {
            var items = AreasSelectListItem(ParentCode, SelectedID, HadEmptyItem);
            StringBuilder stringBuilder = new StringBuilder();
            if (items.Any())
            {
                foreach (var item in items)
                {
                    string option = $"<option{(item.Disabled ? " disabled" : "")}{(item.Selected ? " selected" : "")} value = \'{item.Value}\'> {item.Text}</option>";
                    stringBuilder.Append(option);
                }
            }
            return Json(stringBuilder.ToString());
        }
    }
}
