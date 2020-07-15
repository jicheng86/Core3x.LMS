using AutoMapper;
using AutoMapper.QueryableExtensions;

using LMS.IService.IServices;
using LMS.Model;
using LMS.Model.Dto;
using LMS.Model.Entities;
using LMS.Model.Extend;
using LMS.Web.Models;
using LMS.Web.Utility;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LMS.Web.Controllers
{
    /// <summary>
    /// 企业控制器
    /// </summary>
    public class CorporationController : SharedController
    {

        public CorporationController(IMapper mapper,
                                     IAreaService areaService,
                                     ICorporationService corporationService) : base(mapper, areaService, corporationService)
        {
        }
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

            List<SelectListItem> listItems = AreasSelectListItem();
            ViewBag.AreaSelectListItem = listItems;
            return View();
        }
        /// <summary>
        /// 创建公司数据提交
        /// </summary>
        /// <param name="corporationDto">提交公司信息</param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Creation(CorporationDtoCreation corporationDto)
        {
            JSONData jsondata = new JSONData();
            if (!ModelState.IsValid)
            {
                //ModelState.AddModelError(string.Empty, "自定义描述错误");
                //ModelState.Remove("AreaID");
                //ViewBag.ModelState = ModelState;
                //ViewData["ModelState"] = ModelState;
                //var values = ModelState.Values.Where(s => s.Errors.Any());
                ViewBag.AreaIDs = string.Join(",", corporationDto.AreaID);
                Dictionary<string, string> ModelErrors = new Dictionary<string, string>();

                var keys = ModelState.Keys.Where(k => ModelState[k].Errors.Count > 0);
                foreach (var key in keys)
                {
                    var ErrorMessages = "";
                    if (key == "AreaID")
                    {
                        ErrorMessages = "请正确选择区划地址！";
                    }
                    else
                    {
                        foreach (var error in ModelState[key].Errors)
                        {
                            ErrorMessages += ' ' + error.ErrorMessage;
                        }
                    }
                    ModelErrors.Add(key, ErrorMessages);
                }
                jsondata.Code = EnumCollection.ResponseStatusCode.MODELSTATE;
                jsondata.Data = new { ModelErrors, AreaIDs = string.Join(",", corporationDto.AreaID) };
                jsondata.Message = "数据校验非法，请核实！";
                return Json(jsondata);
            }

            var firstArerID = corporationDto.AreaID.FirstOrDefault();
            if (Constant.SpecialAdministrativeRegionAreaID.Contains(firstArerID))
            {
                if (corporationDto.AreaID.Count < 2)
                {
                    jsondata.Code = EnumCollection.ResponseStatusCode.ARGUMENTSLOSE;
                    jsondata.Message = "请选择完整的区划地址";
                    return Json(jsondata);
                }
            }
            if (corporationDto.AreaID.Count < 4)
            {
                jsondata.Code = EnumCollection.ResponseStatusCode.ARGUMENTSLOSE;
                jsondata.Message = "请选择完整的区划地址";
                return Json(jsondata);
            }

            Corporation corporation = AutoMapper.Map<Corporation>(corporationDto);
            corporation.CreatorUserId = 1000;
            if (CorporationService.IsExisted(corporation))
            {
                jsondata.Code = EnumCollection.ResponseStatusCode.FAIL;
                jsondata.Message = "操作失败：分公司信息已存在，请核实！";
                return Json(jsondata);
            }
            CorporationService.Create(corporation);
            var result = await CorporationService.SaveChangeAsync();
            if (result > 0)
            {
                jsondata.Code = EnumCollection.ResponseStatusCode.SUCCESS;
                jsondata.Message = "操作成功";
                return Json(jsondata);
            }
            return Json(jsondata);
        }

    }
}