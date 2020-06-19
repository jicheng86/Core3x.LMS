using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AutoMapper;

using LMS.IService.IServices;
using LMS.Web.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LMS.Web.Controllers
{
    /// <summary>
    /// ajax辅助控制器
    /// </summary>
    public class APIController : Controller
    {
        public APIController(IMapper mapper, IAreaService areaService, ICorporationService corporationService)
        {
            AreaService = areaService ?? throw new ArgumentNullException(nameof(areaService));
            CorporationService = corporationService ?? throw new ArgumentNullException(nameof(corporationService));
            AutoMapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public IAreaService AreaService { get; set; }
        public ICorporationService CorporationService { get; set; }
        public IMapper AutoMapper { get; set; }

        /// <summary>
        /// 获取Area SelectListItem数据
        /// </summary>
        /// <param name="ParentID">父级主键ID</param>
        /// <param name="SelectedID">默认选中值</param>
        /// <param name="HadEmptyItem">是否包含：“请选择”空选项</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetAreasSelectOptions(int ParentID, int SelectedID, bool HadEmptyItem = false)
        {
            JSONData jSONData = new JSONData();
            List<SelectListItem> items = new List<SelectListItem>();
            if (HadEmptyItem)
            {
                items.Add(new SelectListItem { Value = string.Empty, Text = "请选择" });
            }
            if (ParentID == 1000 && SelectedID < 1)
            {
                SelectedID = 3689;//广东省
            }
            var area = AreaService.GetEntity(a => a.ID == ParentID);
            if (area == null)
            {
                return Ok(jSONData);
            }
            items.AddRange(AreaService.GetEntityListAsync(a => a.ParentCode == area.CityCode).Result.Select(a => new SelectListItem() { Value = a.ID.ToString(), Text = a.Name, Selected = a.ID == SelectedID }).ToList());
            StringBuilder stringBuilder = new StringBuilder();
            if (!items.Any())
            {
                return Ok(jSONData);
            }
            foreach (var item in items)
            {
                string option = $"<option{(item.Disabled ? " disabled" : string.Empty)}{(item.Selected ? " selected" : string.Empty)} value = \'{item.Value}\'> {item.Text}</option>";
                stringBuilder.Append(option);
            }
            jSONData.Code = Utility.EnumCollection.ResponseStatusCode.SUCCESS;
            jSONData.Data = stringBuilder.ToString();
            jSONData.Message = "successful.";

            return Ok(jSONData);
        }
    }
}
