using KPI_API.Models;
using KPI_API.Services;
using KPI_API.Views_Models;
using KPIAPI.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KPI_API.Controllers
{
    [RoutePrefix("api/v2/kpi")]
    public class KPIV2Controller : ApiController
    {
        KPIService _kpiService = new KPIService();

        [Route("TabelMappingKPIDS")]
        [HttpGet]
        public IHttpActionResult GetTableMappingKPIDS(string ID_KPI_DS = "")
        {
            try
            {
                var results = _kpiService.GetTableMappingKPIDS(ID_KPI_DS);

                return Content(HttpStatusCode.OK, new { Status = HttpStatusCode.OK, Data = results, Message = "Success to Get TabelMappingKPI" });
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.BadRequest, new { Status = HttpStatusCode.BadRequest, Data = "", Message = "Failed to Get TabelMappingKPI" });
            }
        }

        [Route("TabelMappingKPION")]
        [HttpGet]
        public IHttpActionResult GetTableMappingKPION(string ID_KPI_ON = "")
        {
            try
            {
                var results = _kpiService.GetTableMappingKPION(ID_KPI_ON);

                return Content(HttpStatusCode.OK, new { Status = HttpStatusCode.OK, Data = results, Message = "Success to Get TabelMappingKPI" });
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.BadRequest, new { Status = HttpStatusCode.BadRequest, Data = "", Message = "Failed to Get TabelMappingKPI" });
            }
        }

        [Route("DOMTableMappingKPIDS")]
        [HttpGet]
        public IHttpActionResult CreateDOMTableMappingKPIDS(string ID_KPI_DS = "")
        {
            try
            {
                var results = _kpiService.CreateDOMTableMappingKPIDS(ID_KPI_DS);

                return Content(HttpStatusCode.OK, new { Status = HttpStatusCode.OK, Data = results, Message = "Success to Create DOM TabelMappingKPI" });
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.BadRequest, new { Status = HttpStatusCode.BadRequest, Data = "", Message = "Failed to Create DOM TabelMappingKPI" });
            }
        }
        
        [Route("DOMTableMappingKPION")]
        [HttpGet]
        public IHttpActionResult CreateDOMTableMappingKPION(string ID_KPI_ON = "")
        {
            try
            {
                var results = _kpiService.CreateDOMTableMappingKPION(ID_KPI_ON);

                return Content(HttpStatusCode.OK, new { Status = HttpStatusCode.OK, Data = results, Message = "Success to Create DOM TabelMappingKPI" });
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.BadRequest, new { Status = HttpStatusCode.BadRequest, Data = "", Message = "Failed to Create DOM TabelMappingKPI" });
            }
        }

        [HttpPost]
        [Route("TabelMappingKPIDS")]
        public IHttpActionResult TabelMappingKPIDS(IEnumerable<TBL_M_MAPPING_KPI> dataMappingKPIDS)
        {
            try
            {
                var isSuccess = _kpiService.SubmitKPIDS(dataMappingKPIDS);
                if (isSuccess == 0) return Content(HttpStatusCode.BadRequest, new { Status = HttpStatusCode.BadRequest, Data = "", Message = "Failed to Submit TabelMappingKPI" });
                return Content(HttpStatusCode.OK, new { Status = HttpStatusCode.OK, Data = "", Message = "Success to Submit TabelMappingKPI" });
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.BadRequest, new { Status = HttpStatusCode.BadRequest, Data = "", Message = "Failed to Submit TabelMappingKPI" });
            }
        }
        
        [HttpPost]
        [Route("TabelMappingKPION")]
        public IHttpActionResult TabelMappingKPION(IEnumerable<TBL_M_MAPPING_KPI_ON> dataMappingKPIDS)
        {
            try
            {
                var isSuccess = _kpiService.SubmitKPION(dataMappingKPIDS);
                if (isSuccess == 0) return Content(HttpStatusCode.BadRequest, new { Status = HttpStatusCode.BadRequest, Data = "", Message = "Failed to Submit TabelMappingKPI" });
                return Content(HttpStatusCode.OK, new { Status = HttpStatusCode.OK, Data = "", Message = "Success to Submit TabelMappingKPI" });
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.BadRequest, new { Status = HttpStatusCode.BadRequest, Data = "", Message = "Failed to Submit TabelMappingKPI" });
            }
        }

    }
}
