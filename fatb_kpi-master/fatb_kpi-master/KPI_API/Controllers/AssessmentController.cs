using KPI_API.Models;
using KPI_API.Views_Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KPI_API.Controllers
{
    public class AssessmentController : ApiController
    {
        DB_KPIDataContext db = new DB_KPIDataContext(ConfigurationManager.ConnectionStrings["DB_FATB_KPI_KPTConnectionString"].ConnectionString);
        ClsAssessment cls = new ClsAssessment();

        [HttpGet]
        [Route("GetTabelAssessmentDS_ByFilter")]
        public IHttpActionResult GetTabelAssessmentDS_ByFilter(string district, int periode)
        {
            try
            {
                var cekExist = db.TBL_T_ASSESSMENTs.Where(x => x.DISTRICT == district && x.PERIODE == periode && x.YEAR == DateTime.Now.Year).ToList();

                if (cekExist.Count == 0)
                {
                    var list = cls.c_getTabelAssessmentDS();

                    return Ok(new { Status = true, data = list });
                }
                else
                {
                    var list = cls.c_getTabelAssessmentDS_ByFilter(district, periode);

                    return Ok(new { Status = true, data = list });
                }
                
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = "Error: " + e.Message.ToString() });
            }
        }

        [HttpGet]
        [Route("GetTabelAssessmentON_ByFilter")]
        public IHttpActionResult GetTabelAssessmentON_ByFilter(string district, int periode)
        {
            try
            {
                var cekExist = db.TBL_T_ASSESSMENT_ONs.Where(x => x.DISTRICT == district && x.PERIODE == periode && x.YEAR == DateTime.Now.Year).ToList();

                if (cekExist.Count == 0)
                {
                    var list = cls.c_getTabelAssessmentON();

                    return Ok(new { Status = true, data = list });
                }
                else
                {
                    var list = cls.c_getTabelAssessmentON_ByFilter(district, periode);

                    return Ok(new { Status = true, data = list });
                }

            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = "Error: " + e.Message.ToString() });
            }
        }

        [HttpGet]
        [Route("GenerateExcelAssessment")]
        public IHttpActionResult GenerateExcelAssessment()
        {
            try
            {
                cls.c_generateExcelAssessment();

                return Ok(new { Status = true, Message = "Generate Template Success" });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = e.Message.ToString() });
            }
        }

        [HttpGet]
        [Route("GetAssessmentDS")]
        public IHttpActionResult GetAssessmentDS(string district, int periode)
        {
            try
            {
                var list = cls.c_getAssessmentDS(district, periode);

                return Ok(new { Status = true, data = list });
                
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = "Error: " + e.Message.ToString() });
            }
        }

        [HttpGet]
        [Route("GetAssessmentON")]
        public IHttpActionResult GetAssessmentON(string district, int periode)
        {
            try
            {
                var list = cls.c_getAssessmentON(district, periode);

                return Ok(new { Status = true, data = list });

            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = "Error: " + e.Message.ToString() });
            }
        }

        [HttpPost]
        [Route("Create_Assessment_DS")]
        public IHttpActionResult Create_Assessment_DS(TBL_T_ASSESSMENT_TEMP[] tBL_T_ASSESSMENT_TEMPs)
        {
            try
            {
                cls.Create_AssessmentDS(tBL_T_ASSESSMENT_TEMPs);

                return Ok(new { Remarks = true, Message = "" });
            }
            catch (Exception ex)
            {
                return Ok(new { Remarks = false, Message = ex.Message.ToString() });
            }
        }

        [HttpPost]
        [Route("Create_Assessment_ON")]
        public IHttpActionResult Create_Assessment_ON(TBL_T_ASSESSMENT_ON_TEMP[] tBL_T_ASSESSMENT_ON_TEMPs)
        {
            try
            {
                cls.Create_AssessmentON(tBL_T_ASSESSMENT_ON_TEMPs);

                return Ok(new { Remarks = true, Message = "" });
            }
            catch (Exception ex)
            {
                return Ok(new { Remarks = false, Message = ex.Message.ToString() });
            }
        }

        [HttpPost]
        [Route("Create_ExcelAssessment")]
        public IHttpActionResult Create_ExcelAssessment([FromBody] ClsExcelAssessment data)
        {
            try
            {
                cls.Create_ExcelAssessment(data);

                return Ok(new { Remarks = true, Message = "" });
            }
            catch (Exception ex)
            {
                return Ok(new { Remarks = false, Message = ex.Message.ToString() });
            }
        }

    }
}
