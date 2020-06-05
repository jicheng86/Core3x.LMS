using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using LMS.IService.IServices;
using LMS.Model;
using LMS.Model.Dto;
using LMS.Model.Entities;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Org.BouncyCastle.Asn1.Ocsp;

using static LMS.Model.Enums.EnumCollection;

namespace LMS.Web.Controllers
{
    public class CorporationController : Controller
    {
        public IMapper AuroMapper { get; }
        public ICorporationService CorporationService { get; }

        public CorporationController(IMapper mapper, ICorporationService corporationService)
        {
            AuroMapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            CorporationService = corporationService ?? throw new ArgumentNullException(nameof(corporationService));
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
        public ActionResult<PageData<CorporationDto>> CorporationListData()
        {
            PageData<CorporationDto> Data = CorporationService.LoadPageDataList(s => true, s => true, 1, 10);
            PageData<CorporationDto> pageData = null;
            pageData.Total = Data.Total;
            //pageData.Data = AuroMapper.ProjectTo<List<CorporationDto>>(await Data);
            return pageData;
        }
        [HttpGet]
        public IActionResult Creation(int ID)
        {
            if (ID > 0)
            {
                Task<List<CorporationDto>> CorporationDtos = CorporationService.GetEntityListAsync(s => true).Result
                       .ProjectTo<CorporationDto>(AuroMapper.ConfigurationProvider)
                       .ToListAsync();
            }
            return View();
        }
        /// <summary>
        /// 创建公司数据提交
        /// </summary>
        /// <param name="corporationDto">提交公司信息</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Creation(CorporationDto corporationDto)
        {
            if (ModelState.IsValid)
            {
               // return ModelExtensions ()
            }
            JsonData json = new JsonData();
            Corporation corporation = AuroMapper.Map<Corporation>(corporationDto);

            CorporationService.Create(corporation);
            var result = CorporationService.SaveChangeAsync();
            if (result.IsCompleted && result.Result > 0)
            {
                json.Code = RespondStatusCode.Success;
                json.Message = "操作成功";
                return Json(json);
            }
            return Json(json);
        }

    }
}