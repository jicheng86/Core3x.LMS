using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using LMS.IService.IServices;
using LMS.Web.Models;
using LMS.Model.Dto;
using LMS.Model;
using LMS.Model.Entities;
using LMS.Model.Extend;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LMS.Web.Utility;

namespace LMS.Web.Controllers
{
    public class CorporationController : SharedController
    {

        //public CorporationController(IMapper mapper,
        //                             IAreaService areaService,
        //                             ICorporationService corporationService) : base(mapper, areaService, corporationService)
        //{
        //}
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult CorporationList()
        {

            return View();
        }
        [HttpPost]
        public ActionResult<PageData<CorporationDto>> CorporationListData([FromBody] CorporationSearchParamsDto paramsDto)
        {
            if (paramsDto is null)
            {
                throw new ArgumentNullException(nameof(paramsDto));
            }
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "");
            }
            Expression<Func<Corporation, bool>> whereLambda = w => true;
            Expression<Func<Corporation, object>> orderLambda = w => true;
            if (!string.IsNullOrWhiteSpace(paramsDto.Name))
            {
                whereLambda.And(s => s.Name.Contains(paramsDto.Name));
            }
            if (string.IsNullOrWhiteSpace(paramsDto.CorporationAddress))
            {
                whereLambda.And(s => s.CorporationAddress.Contains(paramsDto.CorporationAddress));
            }
            //LoadingPageData 加载分页数据
            PageData<CorporationDto> Data = CorporationService.LoadPageDataList(whereLambda, orderLambda,
                                                                                paramsDto.PageNumber, paramsDto.PageSize,
                                                                                paramsDto.SortOrder.ToLower() == "desc");
            PageData<CorporationDto> pageData = null;
            pageData.Total = Data.Total;
            return pageData;
        }
        [HttpGet]
        public IActionResult Creation(int ID)
        {
            if (ID > 0)
            {
                Task<List<CorporationDto>> CorporationDtos = CorporationService.GetEntityListAsync(s => true).Result
                       .ProjectTo<CorporationDto>(AutoMapper.ConfigurationProvider)
                       .ToListAsync();
            }

            List<SelectListItem> listItems = AreasSelectListItem("100000", 0, false);
            ViewBag.AreaSelectListItem = listItems;
            return View();
        }
        /// <summary>
        /// 创建公司数据提交
        /// </summary>
        /// <param name="corporationDto">提交公司信息</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Creation(CorporationDto corporationDto)
        {
            if (ModelState.IsValid)
            {
                // return ModelExtensions ()
            }
            JSONData Jsondata = new JSONData();
            Corporation corporation = AutoMapper.Map<Corporation>(corporationDto);

            CorporationService.Create(corporation);
            var result = CorporationService.SaveChangeAsync();
            if (result.IsCompleted && result.Result > 0)
            {
                Jsondata.Code = EnumCollection.ResponseStatusCode.SUCCESS;
                Jsondata.Message = "操作成功";
                return Json(Jsondata);
            }
            return Json(Jsondata);
        }

    }
}