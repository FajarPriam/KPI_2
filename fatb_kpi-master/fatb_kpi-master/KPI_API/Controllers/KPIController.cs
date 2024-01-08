using KPI_API.Models;
using KPI_API.Views_Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace KPI_API.Controllers
{
    public class KPIController : ApiController
    {
        DB_KPIDataContext db = new DB_KPIDataContext(ConfigurationManager.ConnectionStrings["DB_FATB_KPI_KPTConnectionString"].ConnectionString);

        [HttpPost]
        [Route("DeleteKPIDS")]
        public IHttpActionResult DeleteKPIDS(string id)
        {
            try
            {
                ClsKPI clsDS = new ClsKPI();
                clsDS.c_deleteDS(id);

                return Ok(new { Status = true, Message = "Delete DS Success" });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = e.Message.ToString() });
            }
        }


        [HttpPost]
        [Route("CreateOrUpdateON")]
        public IHttpActionResult CreateOrUpdateON(TBL_M_KPI[] param)
        {
            try
            {
                ClsKPI clsON = new ClsKPI();
                clsON.c_InsertOrUpdateON(param);

                return Ok(new { Status = true, Message = "Insert ON Success" });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = e.Message.ToString() });
            }
        }

        [HttpPost]
        [Route("CreateOrUpdateCash")]
        public IHttpActionResult CreateOrUpdateCash(TBL_M_KPI_CASHIER[] param)
        {
            try
            {
                ClsKPI clsON = new ClsKPI();
                clsON.c_InsertOrUpdateCash(param);

                return Ok(new { Status = true, Message = "Insert ON Success" });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = e.Message.ToString() });
            }
        }

        [HttpPost]
        [Route("UploadItemKP")]
        public IHttpActionResult UploadItemKP()
        {
            try
            {
                if (HttpContext.Current.Request.Files.Count > 0)
                {
                    var file = HttpContext.Current.Request.Files[0]; // Get the uploaded file
                    if (file != null && file.ContentLength > 0)
                    {
                        ClsKPI clsON = new ClsKPI();
                        clsON.c_uploadItemKPI(file);

                        return Ok(new { Status = true, Message = "Insert ON Success" });
                    }
                }

                return Ok(new { Status = false, Message = "No file uploaded." });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = e.Message.ToString() });
            }
        }

        [HttpPost]
        [Route("Create_ExcelKPI")]
        public IHttpActionResult Create_ExcelKPI([FromBody] ClsExcelKPI data)
        {
            try
            {
                //sheet officer
                ClsKPI clsKPI = new ClsKPI();
                bool sts_bobot = false;
                List<string> section = new List<string>();
                if (data.clsTemplates.Count() != 0)
                {
                    int total = 0;
                    foreach (ClsTemplate cek in data.clsTemplates)
                    {
                        if (cek.CODE_SECTION != "" && cek.KPI_ITEM != "" && cek.DESKRIPSI != "" && cek.BOBOT != "" && cek.TARGET != "")
                        {

                            if (section.Count() == 0)
                            {
                                section.Add(cek.CODE_SECTION);
                            }
                            else if (!section.Contains(cek.CODE_SECTION))
                            {
                                section.Add(cek.CODE_SECTION);
                            }

                            total = total + Convert.ToInt32(cek.BOBOT);
                        }
                        else
                        {
                            return Ok(new { Remarks = false, Message = "Terdapat data mandatory yang tidak diisi, silahkan di cek kembali" });
                        }
                    }

                    int n_sect = section.Count();
                    int total_avg = total / n_sect;
                    if(total == 100)
                    {
                        sts_bobot = true;
                    }
                }

                

                //sheet cash
                List<string> section2 = new List<string>();
                if (data.clsTemplatesCash.Count() != 0)
                {
                    int total = 0;
                    foreach (ClsTemplateCash cek in data.clsTemplatesCash)
                    {
                        if (cek.CODE_SECTION != "" && cek.KPI_ITEM != "" && cek.DESKRIPSI != "" && cek.BOBOT != "" && cek.TARGET != "")
                        {

                            if (section.Count() == 0)
                            {
                                section.Add(cek.CODE_SECTION);
                            }
                            else if (!section.Contains(cek.CODE_SECTION))
                            {
                                section.Add(cek.CODE_SECTION);
                            }

                            total = total + Convert.ToInt32(cek.BOBOT);
                        }
                        else
                        {
                            return Ok(new { Remarks = false, Message = "Terdapat data mandatory yang tidak diisi, silahkan di cek kembali" });
                        }
                    }

                    int n_sect = section.Count();
                    int total_avg = total / n_sect;
                    if (total_avg == 100)
                    {
                        sts_bobot = true;
                    }
                }

                if (sts_bobot == true)
                {
                    clsKPI.Create_ExcelKPI(data);
                }
                else
                {
                    return Ok(new { Remarks = false, Message = "Jumlah Bobot Tidak 100%" });
                }

                return Ok(new { Remarks = true, Message = "" });
            }
            catch (Exception ex)
            {
                return Ok(new { Remarks = false, Message = ex.Message.ToString() });
            }
        }

        [HttpPost]
        [Route("Create_ExcelMapping")]
        public IHttpActionResult Create_ExcelMapping(ClsTemplateMapping[] clsTemplateMappings)
        {
            try
            {
                ClsKPI clsKPI = new ClsKPI();
                clsKPI.Create_ExcelMapping(clsTemplateMappings);

                return Ok(new { Remarks = true, Message = "" });
            }
            catch (Exception ex)
            {
                return Ok(new { Remarks = false, Message = ex.Message.ToString() });
            }
        }

        [HttpPost]
        [Route("Update_ExcelKPI")]
        public IHttpActionResult Update_ExcelKPI([FromBody] ClsUpdate_Excel data)
        {
            try
            {
                ClsKPI clsKPI = new ClsKPI();
                bool sts_bobot = false;
                List<string> section = new List<string>();
                if (data.clsUpdateKPI.Count() != 0)
                {
                    int total = 0;
                    foreach (ClsUpdateKPI cek in data.clsUpdateKPI)
                    {
                        if (cek.CODE_SECTION != "" && cek.KPI_ITEM != "" && cek.DESKRIPSI != "" && cek.BOBOT != "" && cek.TARGET != "")
                        {

                            if (section.Count() == 0)
                            {
                                section.Add(cek.CODE_SECTION);
                            }
                            else if (!section.Contains(cek.CODE_SECTION))
                            {
                                section.Add(cek.CODE_SECTION);
                            }

                            if (cek.BOBOT.Contains("0.") && cek.BOBOT.Length > 1)
                            {
                                decimal bobot = decimal.Parse(cek.BOBOT);
                                string str_bobot = bobot.ToString();
                                if (str_bobot.Length == 1)
                                {
                                    bobot = bobot * 10;
                                }

                                total = total + Convert.ToInt32(bobot);
                            }
                            else
                            {
                                int bobot = Convert.ToInt32(cek.BOBOT) * 100;
                                total = total + bobot;
                            }
                            
                        }
                        else
                        {
                            return Ok(new { Remarks = false, Message = "Terdapat data mandatory yang tidak diisi, silahkan di cek kembali" });
                        }
                    }

                    int n_sect = section.Count();
                    int total_avg = (total / n_sect);
                    if (total_avg == 100)
                    {
                        sts_bobot = true;
                    }
                }

                if (sts_bobot == true)
                {
                    clsKPI.Update_ExcelKPI(data);
                }
                else
                {
                    return Ok(new { Remarks = false, Message = "Jumlah Bobot Tidak 100%" });
                }

                return Ok(new { Remarks = true, Message = "" });
            }
            catch (Exception ex)
            {
                return Ok(new { Remarks = false, Message = ex.Message.ToString() });
            }
        }

        [HttpPost]
        [Route("Update_ExcelCash")]
        public IHttpActionResult Update_ExcelCash([FromBody] ClsUpdate_Excel data)
        {
            try
            {
                ClsKPI clsKPI = new ClsKPI();
                bool sts_bobot = false;
                List<string> section = new List<string>();
                if (data.clsUpdateCashier.Count() != 0)
                {
                    int total = 0;
                    foreach (ClsUpdateCashier cek in data.clsUpdateCashier)
                    {
                        if (cek.CODE_SECTION != "" && cek.KPI_ITEM != "" && cek.DESKRIPSI != "" && cek.BOBOT != "" && cek.TARGET != "")
                        {

                            if (section.Count() == 0)
                            {
                                section.Add(cek.CODE_SECTION);
                            }
                            else if (!section.Contains(cek.CODE_SECTION))
                            {
                                section.Add(cek.CODE_SECTION);
                            }

                            if (cek.BOBOT.Contains("0.") && cek.BOBOT.Length > 1)
                            {
                                decimal bobot = decimal.Parse(cek.BOBOT);
                                string str_bobot = bobot.ToString();
                                if (str_bobot.Length == 1)
                                {
                                    bobot = bobot * 10;
                                }

                                total = total + Convert.ToInt32(bobot);
                            }
                            else
                            {
                                int bobot = Convert.ToInt32(cek.BOBOT) * 100;
                                total = total + bobot;
                            }

                        }
                        else
                        {
                            return Ok(new { Remarks = false, Message = "Terdapat data mandatory yang tidak diisi, silahkan di cek kembali" });
                        }
                    }

                    int n_sect = section.Count();
                    int total_avg = (total / n_sect);
                    if (total_avg == 100)
                    {
                        sts_bobot = true;
                    }
                }

                if (sts_bobot == true)
                {
                    clsKPI.Update_ExcelCash(data);
                }
                else
                {
                    return Ok(new { Remarks = false, Message = "Jumlah Bobot Tidak 100%" });
                }

                return Ok(new { Remarks = true, Message = "" });
            }
            catch (Exception ex)
            {
                return Ok(new { Remarks = false, Message = ex.Message.ToString() });
            }
        }

        [HttpPost]
        [Route("Update_ExcelDS")]
        public IHttpActionResult Update_ExcelDS([FromBody] ClsUpdate_Excel data)
        {
            try
            {
                DateTime dateTime = DateTime.Now;
                ClsKPI clsKPI = new ClsKPI();
                bool sts_bobot = false;
                List<string> section = new List<string>();
                if (data.clsUpdateDS.Count() != 0)
                {
                    List<TBL_M_KPI_DEPT_SECT> tbl2 = new List<TBL_M_KPI_DEPT_SECT>();
                    foreach (ClsUpdateDS cls2 in data.clsUpdateDS)
                    {
                        var DS = db.TBL_M_KPI_DEPT_SECTs.Where(a => a.KPI_DS_ID == cls2.KPI_DS_ID).FirstOrDefault();
                        if (DS != null)
                        {

                            DS.KPI_DS_DESC = cls2.KPI_DS_DESC;
                            DS.EDIT_BY = cls2.EDIT_BY;
                            DS.EDIT_DATE = dateTime;
                            db.SubmitChanges();
                        }
                        else
                        {
                            int? num = db.cufn_generateKPICodeDS();
                            var cek_kpi_code = "";
                            if (num == null)
                            {
                                cek_kpi_code = "H." + 1;
                            }
                            else
                            {
                                cek_kpi_code = "H." + Convert.ToString(num + 1);
                            }

                            tbl2.Add(new TBL_M_KPI_DEPT_SECT
                            {
                                KPI_DS_ID = cek_kpi_code,
                                KPI_DS_DESC = cls2.KPI_DS_DESC,
                                EDIT_BY = cls2.EDIT_BY,
                                EDIT_DATE = dateTime
                            });
                            db.TBL_M_KPI_DEPT_SECTs.InsertAllOnSubmit(tbl2);
                            db.SubmitChanges();
                        }
                    }
                }

                return Ok(new { Remarks = true, Message = "" });
            }
            catch (Exception ex)
            {
                return Ok(new { Remarks = false, Message = ex.Message.ToString() });
            }
        }

        [HttpPost]
        [Route("SubmitAllMapping")]
        public IHttpActionResult SubmitAllMapping(ClsEKITemp[] clsEKITemps)
        {
            //Console.WriteLine(clsEKITemps.Length);
            //var id = Convert.ToInt32(clsEKITemps[0].ID_KPI_DS);
            try
            {
                ClsKPI cls = new ClsKPI();
                cls.c_DeleteMappingAll(clsEKITemps[0].ID_KPI_DS);

                var data = db.TBL_M_MAPPING_KPIs.Where(x => x.ID_KPI_DS == clsEKITemps[0].ID_KPI_DS).SingleOrDefault();
                if (data == null)
                {
                    cls.c_SubmitAllMapping(clsEKITemps); ;
                }

                return Ok(new { Status = true, Message = "" });
            }
            catch (Exception ex)
            {
                return Ok(new { Status = false, Message = ex.Message.ToString() });
            }
        }

        [HttpPost]
        [Route("SubmitAllMappingON")]
        public IHttpActionResult SubmitAllMappingON(ClsMappingONTemp[] clsMappingONTemps)
        {
            //Console.WriteLine(clsEKITemps.Length);
            //var id = Convert.ToInt32(clsEKITemps[0].ID_KPI_DS);
            try
            {
                ClsKPI cls = new ClsKPI();
                
                string[] kpiCodesArray = clsMappingONTemps.Select(x => x.KPI_CODE).ToArray();
                var duplicateRecords = db.TBL_M_MAPPING_KPI_ONs
                        .Where(x => kpiCodesArray.Contains(x.KPI_CODE) && x.ID_KPI_ON != clsMappingONTemps[0].ID_KPI_ON)
                        .ToList();
                bool hasDuplicates = kpiCodesArray.Length != kpiCodesArray.Distinct().Count();
                if (duplicateRecords.Count == 0 && hasDuplicates == false)
                {
                    cls.c_DeleteMappingAllON(clsMappingONTemps[0].ID_KPI_ON);
                    cls.c_SubmitAllMappingON(clsMappingONTemps);
                    return Ok(new { Status = true, Message = "" });
                }
                else if (duplicateRecords.Count == 0 && hasDuplicates == true)
                {
                    return Ok(new { Status = false, Message = "Duplicate KPI CODE values found" });
                }
                else
                {
                    return Ok(new { Status = false, Message = "Duplicate KPI CODE values found." });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { Status = false, Message = ex.Message.ToString() });
            }
        }

        [HttpPost]
        [Route("AddLevelKPI")]
        public IHttpActionResult AddLevelKPI(ClsKPI clsKPI)
        {
            try
            {
                clsKPI.c_AddLevelKPI();
                return Ok(new { Remarks = true, Message = "" });
            }
            catch (Exception ex)
            {
                return Ok(new { Remarks = false, Message = ex.Message.ToString() });
            }
        }

        [HttpPost]
        [Route("AddLevelKPI_Cash")]
        public IHttpActionResult AddLevelKPI_Cash(ClsKPI clsKPI)
        {
            try
            {
                clsKPI.c_AddLevelKPI_Cash();
                return Ok(new { Remarks = true, Message = "" });
            }
            catch (Exception ex)
            {
                return Ok(new { Remarks = false, Message = ex.Message.ToString() });
            }
        }

        //============Upload Penilaian KPI
        [HttpPost]
        [Route("UploadPenilaianKPI")]
        public IHttpActionResult UploadPenilaianKPI()
        {
            try
            {
                if (HttpContext.Current.Request.Files.Count > 0)
                {
                    var file = HttpContext.Current.Request.Files[0]; // Get the uploaded file
                    if (file != null && file.ContentLength > 0)
                    {
                        ClsKPI clsON = new ClsKPI();
                        clsON.c_uploadItemKPI(file);

                        return Ok(new { Status = true, Message = "Insert ON Success" });
                    }
                }

                return Ok(new { Status = false, Message = "No file uploaded." });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = e.Message.ToString() });
            }
        }

        //insertPenilaianKPI
        [HttpPost]
        [Route("PenilaianKPI")]
        public IHttpActionResult PenilaianKPI(List<TBL_T_POINT_KPI_OFFICER_NONOM> param)
        {
            try
            {
                ClsKPI clsON = new ClsKPI();
                clsON.c_PenilaianKPI(param);

                return Ok(new { Status = true, Message = "Insert ON Success" });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = e.Message.ToString() });
            }
        }

        [HttpPost]
        [Route("AddMapping")]
        public IHttpActionResult AddMapping(string KPI_DS_ID)
        {
            try
            {
                ClsKPI cls = new ClsKPI();
                cls.c_AddMapping(KPI_DS_ID);
                return Ok(new { Status = true, Message = "Insert ON Success" });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = "Error: " + e.Message.ToString() });
            }
        }

        [HttpPost]
        [Route("DeleteMapping/{ID}")]
        public IHttpActionResult DeleteMapping(int ID)
        {
            try
            {
                ClsKPI cls = new ClsKPI();
                cls.c_DeleteMapping(ID);
                return Ok(new { Status = true, Message = "Delete Mapping Success" });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = "Error: " + e.Message.ToString() });
            }
        }

        [HttpPost]
        [Route("DeleteMappingON/{ID}")]
        public IHttpActionResult DeleteMappingON(int ID)
        {
            try
            {
                ClsKPI cls = new ClsKPI();
                cls.c_DeleteMappingON(ID);
                return Ok(new { Status = true, Message = "Delete Mapping Success" });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = "Error: " + e.Message.ToString() });
            }
        }

        [HttpPost]
        [Route("DeleteON/{id}")]
        public IHttpActionResult DeleteON(int id)
        {
            try
            {
                ClsKPI clsON = new ClsKPI();
                clsON.c_deleteON(id);

                return Ok(new { Status = true, Message = "Delete ON Success" });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = e.Message.ToString() });
            }
        }

        [HttpPost]
        [Route("Delete_DataKPI")]
        public IHttpActionResult Delete_DataKPI(string KPI_CODE)
        {
            try
            {
                ClsKPI cls = new ClsKPI();
                cls.Delete_DataKPI(KPI_CODE);

                return Ok(new { Remarks = true, Message = "" });
            }
            catch (Exception ex)
            {
                return Ok(new { Remarks = false, Message = ex.Message.ToString() });
            }
        }

        [HttpPost]
        [Route("Delete_DataKPICash")]
        public IHttpActionResult Delete_DataKPICash(string KPI_CODE)
        {
            try
            {
                ClsKPI cls = new ClsKPI();
                cls.Delete_DataKPICash(KPI_CODE);

                return Ok(new { Remarks = true, Message = "" });
            }
            catch (Exception ex)
            {
                return Ok(new { Remarks = false, Message = ex.Message.ToString() });
            }
        }

        [HttpPost]
        [Route("DeleteKPI/{KPI_CODE}")]
        public IHttpActionResult DeleteKPI(string KPI_CODE)
        {
            try
            {
                ClsKPI clsON = new ClsKPI();
                clsON.c_deleteKPI(KPI_CODE);

                return Ok(new { Status = true, Message = "Delete ON Success" });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = e.Message.ToString() });
            }
        }

        [HttpPost]
        [Route("AddKPION")]
        public IHttpActionResult AddKPION(TBL_M_KPI param)
        {
            try
            {
                ClsKPI clsDS = new ClsKPI();
                clsDS.c_AddKPION(param);

                return Ok(new { Status = true, Message = "Add KPI ON Success" });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = e.Message.ToString() });
            }
        }

        [HttpPost]
        [Route("AddKPICash")]
        public IHttpActionResult AddKPICash(TBL_M_KPI_CASHIER param)
        {
            try
            {
                ClsKPI clsDS = new ClsKPI();
                clsDS.c_AddKPICash(param);

                return Ok(new { Status = true, Message = "Add KPI ON Success" });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = e.Message.ToString() });
            }
        }

        [HttpPost]
        [Route("AddMappingKPION")]
        public IHttpActionResult AddMappingKPION(TBL_M_MAPPING_KPI_ON tbl)
        {
            try
            {
                ClsKPI cls = new ClsKPI();
                cls.c_AddMappingKPION(tbl);
                return Ok(new { Status = true, Message = "Insert ON Success" });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = "Error: " + e.Message.ToString() });
            }
        }

        [HttpGet]
        [Route("GetNilaiKPI_DS")]
        public IHttpActionResult GetNilaiKPI_DS()
        {
            try
            {
                ClsKPI clsON = new ClsKPI();

                var list = clsON.c_getNilaiKPI_DS();

                return Ok(new { Status = true, data = list });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = "Error: " + e.Message.ToString() });
            }
        }

        [HttpGet]
        [Route("GetTabelMappingAll")]
        public IHttpActionResult GetTabelMappingAll()
        {
            try
            {
                ClsKPI clsON = new ClsKPI();

                var list = clsON.c_getTabelMappingAll();

                return Ok(new { Status = true, data = list });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = "Error: " + e.Message.ToString() });
            }
        }
        
        [HttpGet]
        [Route("GetTabelKPION")]
        public IHttpActionResult GetTabelKPION()
        {
            try
            {
                ClsKPI clsON = new ClsKPI();

                var list = clsON.c_getTabelKPION();

                return Ok(new { Status = true, data = list });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = "Error: " + e.Message.ToString() });
            }
        }

        [HttpGet]
        [Route("GetTabelKPICash")]
        public IHttpActionResult GetTabelKPICash()
        {
            try
            {
                ClsKPI clsON = new ClsKPI();

                var list = clsON.c_getTabelKPICash();

                return Ok(new { Status = true, data = list });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = "Error: " + e.Message.ToString() });
            }
        }

        [HttpGet]
        [Route("GetTabelEditKPI/{CODE_SECTION}")]
        public IHttpActionResult GetTabelEditKPI(string CODE_SECTION)
        {
            try
            {
                ClsKPI clsON = new ClsKPI();

                var list = clsON.c_getTabelEditKPI(CODE_SECTION);

                return Ok(new { Status = true, data = list });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = "Error: " + e.Message.ToString() });
            }
        }

        [HttpGet]
        [Route("GetTabelEditKPI_Cash/{CODE_SECTION}")]
        public IHttpActionResult GetTabelEditKPI_Cash(string CODE_SECTION)
        {
            try
            {
                ClsKPI clsON = new ClsKPI();

                var list = clsON.c_getTabelEditKPI_Cash(CODE_SECTION);

                return Ok(new { Status = true, data = list });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = "Error: " + e.Message.ToString() });
            }
        }

        [HttpGet]
        [Route("GetKPIONMappingParent")]
        public IHttpActionResult GetKPIONMappingParent()
        {
            try
            {
                ClsKPI clsON = new ClsKPI();

                var list = clsON.c_getKPIONMappingParent();

                return Ok(new { Status = true, data = list });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = "Error: " + e.Message.ToString() });
            }
        }

        [HttpGet]
        [Route("GetKPIONMappingChild")]
        public IHttpActionResult GetKPIONMappingChild(string ID_KPI_ON)
        {
            try
            {
                ClsKPI clsON = new ClsKPI();

                var list = clsON.c_getKPIONMappingChild(ID_KPI_ON);

                return Ok(new { Status = true, data = list });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = "Error: " + e.Message.ToString() });
            }
        }

        [HttpGet]
        [Route("GetTabelMappingKPION")]
        public IHttpActionResult GetTabelMappingKPION()
        {
            try
            {
                ClsKPI clsON = new ClsKPI();

                var list = clsON.c_getTabelMappingKPION();

                return Ok(new { Status = true, data = list });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = "Error: " + e.Message.ToString() });
            }
        }

        [HttpGet]
        [Route("GetNilaiEvaluasi_ON")]
        public IHttpActionResult GetNilaiEvaluasi_ON()
        {
            try
            {
                ClsKPI clsON = new ClsKPI();

                var list = clsON.c_getNilaiEvaluasi_ON();

                return Ok(new { Status = true, data = list });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = "Error: " + e.Message.ToString() });
            }
        }

        [HttpGet]
        [Route("GetNilaiKPI_OF")]
        public IHttpActionResult GetNilaiKPI_OF()
        {
            try
            {
                ClsKPI clsON = new ClsKPI();

                var list = clsON.c_getNilaiKPI_OF();

                return Ok(new { Status = true, data = list });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = "Error: " + e.Message.ToString() });
            }
        }

        [HttpGet]
        [Route("GetItemKPI")]
        public IHttpActionResult GetItemKPI()
        {
            try
            {
                ClsKPI clsON = new ClsKPI();

                var list = clsON.c_getItemKPI();

                return Ok(new { Status = true, data = list });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = "Error: " + e.Message.ToString() });
            }
        }

        [HttpGet]
        [Route("GetItemKPIPeriode/{periode}/{year}")]
        public IHttpActionResult GetItemKPIPeriode(int periode, int year)
        {
            try
            {
                ClsKPI clsON = new ClsKPI();

                var list = clsON.c_getItemKPIPeriode(periode, year);

                return Ok(new { Status = true, data = list });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = "Error: " + e.Message.ToString() });
            }
        }

        [HttpGet]
        [Route("GetItemKPI/{id}")]
        public IHttpActionResult GetItemKPI(int id)
        {
            try
            {
                ClsKPI clsON = new ClsKPI();

                var list = clsON.c_getItemKPI(id);

                return Ok(new { Status = true, data = list });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = "Error: " + e.Message.ToString() });
            }
        }

        [HttpGet]
        [Route("GetItemKPIDept/{dept}/{year}/{periode}")]
        public IHttpActionResult GetItemKPIDept(string dept, int periode, int year)
        {
            try
            {
                ClsKPI clsON = new ClsKPI();

                var list = clsON.c_getItemKPIDept(dept, periode, year);

                return Ok(new { Status = true, data = list });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = "Error: " + e.Message.ToString() });
            }
        }

        [HttpGet]
        [Route("GetTabelMappingKPI")]
        public IHttpActionResult GetTabelMappingKPI(string ID_KPI_DS)
        {
            try
            {
                ClsKPI clsDS = new ClsKPI();

                var list = clsDS.c_getTabelMappingKPIDS(ID_KPI_DS);

                return Ok(new { Status = true, data = list });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = "Error: " + e.Message.ToString() });
            }
        }

        [HttpGet]
        [Route("GetTabelMappingKPIONByKPI")]
        public IHttpActionResult GetTabelMappingKPIONByKPI(string ID_KPI_ON)
        {
            try
            {
                ClsKPI clsDS = new ClsKPI();

                var list = clsDS.c_getTabelMappingKPIONByKPI(ID_KPI_ON);

                return Ok(new { Status = true, data = list });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = "Error: " + e.Message.ToString() });
            }
        }

        [HttpPost]
        [Route("CreateOrUpdateDS")]
        public IHttpActionResult CreateOrUpdateDS(TBL_M_KPI_DEPT_SECT param)
        {
            try
            {
                ClsKPI clsDS = new ClsKPI();
                clsDS.c_InsertOrUpdateDS(param);

                return Ok(new { Status = true, Message = "Insert DS Success" });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = e.Message.ToString() });
            }
        }

        [HttpGet]
        [Route("GetListKPICode")]
        public IHttpActionResult GetListKPICode()
        {
            try
            {
                ClsKPI clsDS = new ClsKPI();

                var list = clsDS.c_getListKPICode();

                return Ok(new { Status = true, data = list });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = "Error: " + e.Message.ToString() });
            }
        }

        [HttpGet]
        [Route("GetListPIC")]
        public IHttpActionResult GetListPIC()
        {
            try
            {
                ClsKPI clsDS = new ClsKPI();

                var list = clsDS.c_getListPIC();

                return Ok(new { Status = true, data = list });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = "Error: " + e.Message.ToString() });
            }
        }

        [HttpGet]
        [Route("GetItemDS")]
        public IHttpActionResult GetItemDS()
        {
            try
            {
                ClsKPI clsDS = new ClsKPI();

                var list = clsDS.c_getItemDS();

                return Ok(new { Status = true, data = list });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = "Error: " + e.Message.ToString() });
            }
        }

        //[HttpGet]
        //[Route("GetItemDS/{id}")]
        //public IHttpActionResult GetItemDS(int id)
        //{
        //    try
        //    {
        //        ClsKPI clsDS = new ClsKPI();

        //        var list = clsDS.c_getItemDS(id);

        //        return Ok(new { Status = true, data = list });
        //    }
        //    catch (Exception e)
        //    {
        //        return Ok(new { Status = false, Message = "Error: " + e.Message.ToString() });
        //    }
        //}

        [HttpPost]
        [Route("CreateOrUpdateOF")]
        public IHttpActionResult CreateOrUpdateOF(TBL_M_KPI_OFFICER param)
        {
            try
            {
                ClsKPI clsOF = new ClsKPI();
                clsOF.c_InsertOrUpdateOF(param);

                return Ok(new { Status = true, Message = "Insert OF Success" });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = e.Message.ToString() });
            }
        }

        [HttpPost]
        [Route("DeleteOF/{id}")]
        public IHttpActionResult DeleteOF(int id)
        {
            try
            {
                ClsKPI clsOF = new ClsKPI();
                clsOF.c_deleteOF(id);

                return Ok(new { Status = true, Message = "Delete OF Success" });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = e.Message.ToString() });
            }
        }

        [HttpPost]
        [Route("DeleteDS/{id}")]
        public IHttpActionResult DeleteDS(string id)
        {
            try
            {
                ClsKPI clsDS = new ClsKPI();
                clsDS.c_deleteDS(id);

                return Ok(new { Status = true, Message = "Delete DS Success" });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = e.Message.ToString() });
            }
        }

        [HttpGet]
        [Route("GetItemOFC")]
        public IHttpActionResult GetItemOFC()
        {
            try
            {
                ClsKPI clsOF = new ClsKPI();

                var list = clsOF.c_getItemOfc();

                return Ok(new { Status = true, data = list });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = "Error: " + e.Message.ToString() });
            }
        }

        [HttpGet]
        [Route("GetItemOFC/{id}")]
        public IHttpActionResult GetItemOFC(int id)
        {
            try
            {
                ClsKPI clsOF = new ClsKPI();

                var list = clsOF.c_getItemOfc(id);

                return Ok(new { Status = true, data = list });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = "Error: " + e.Message.ToString() });
            }
        }

        [HttpGet]
        [Route("GetPeriode")]
        public IHttpActionResult GetPeriode()
        {
            try
            {
                ClsKPI clsOF = new ClsKPI();

                var list = clsOF.c_getPeriode();

                return Ok(new { Status = true, data = list });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = "Error: " + e.Message.ToString() });
            }
        }

        [HttpGet]
        [Route("GetItemDSPeriode")]
        public IHttpActionResult GetItemDSPeriode()
        {
            try
            {
                ClsKPI clsOF = new ClsKPI();

                var list = clsOF.c_getItemDSPeriode();

                return Ok(new { Status = true, data = list });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = "Error: " + e.Message.ToString() });
            }
        }

        [HttpGet]
        [Route("GetItemDSPeriode_Cash")]
        public IHttpActionResult GetItemDSPeriode_Cash()
        {
            try
            {
                ClsKPI clsOF = new ClsKPI();

                var list = clsOF.c_getItemDSPeriode_Cash();

                return Ok(new { Status = true, data = list });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = "Error: " + e.Message.ToString() });
            }
        }

        [HttpGet]
        [Route("GetAllSection")]
        public IHttpActionResult GetAllSection(string section)
        {
            try
            {
                ClsKPI clsOF = new ClsKPI();

                var list = clsOF.GetAllSection(section);

                return Ok(new { Status = true, data = list });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = "Error: " + e.Message.ToString() });
            }
        }

        [HttpGet]
        [Route("GetPosition")]
        public IHttpActionResult GetPosition()
        {
            try
            {
                ClsKPI clsOF = new ClsKPI();

                var list = clsOF.c_getPosition();

                return Ok(new { Status = true, data = list });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = "Error: " + e.Message.ToString() });
            }
        }


        [HttpGet]
        [Route("GenerateTemplateItemKPI")]
        public IHttpActionResult GenerateTemplateItemKPI()
        {
            try
            {
                ClsKPI clsON = new ClsKPI();
                clsON.c_generateExcelItemKPI();

                return Ok(new { Status = true, Message = "Generate Template Success" });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = e.Message.ToString() });
            }
        }

        [HttpGet]
        [Route("GetAllDataKPI")]
        public IHttpActionResult GetAllDataKPI()
        {
            try
            {
                ClsKPI clsON = new ClsKPI();
                clsON.getAllDataKPI();

                return Ok(new { Status = true, Message = "Generate Template Success" });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = e.Message.ToString() });
            }
        }

        [HttpGet]
        [Route("GetAllDataKPI_Cash")]
        public IHttpActionResult GetAllDataKPI_Cash()
        {
            try
            {
                ClsKPI clsON = new ClsKPI();
                clsON.getAllDataKPI_Cash();

                return Ok(new { Status = true, Message = "Generate Template Success" });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = e.Message.ToString() });
            }
        }

        [HttpGet]
        [Route("GetAllDataKPI_DS")]
        public IHttpActionResult GetAllDataKPI_DS()
        {
            try
            {
                ClsKPI clsON = new ClsKPI();
                clsON.getAllDataKPI_DS();

                return Ok(new { Status = true, Message = "Generate Template Success" });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = e.Message.ToString() });
            }
        }

        [HttpGet]
        [Route("GenerateTemplateMappingKPI")]
        public IHttpActionResult GenerateTemplateMappingKPI()
        {
            try
            {
                ClsKPI clsON = new ClsKPI();
                clsON.c_generateExcelMappingKPI();

                return Ok(new { Status = true, Message = "Generate Template Success" });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = e.Message.ToString() });
            }
        }

        [HttpGet]
        [Route("GenerateTemplatePointKPI")]
        public IHttpActionResult GenerateTemplatePointKPI()
        {
            try
            {
                ClsKPI clsON = new ClsKPI();
                clsON.c_generateExcelPointKPI();

                return Ok(new { Status = true, Message = "Generate Template Success" });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = e.Message.ToString() });
            }
        }

        [HttpGet]
        [Route("getDistrict")]
        public IHttpActionResult getDistrict()
        {
            try
            {
                ClsKPI clsUser = new ClsKPI();

                var list = clsUser.c_getDistrict();

                return Ok(new { Status = true, data = list });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = "Error: " + e.Message.ToString() });
            }
        }

        [HttpGet]
        [Route("getNilaiLevelKPI")]
        public IHttpActionResult getNilaiLevelKPI(string kpi)
        {
            try
            {
                ClsKPI clsKPI = new ClsKPI();

                var list = clsKPI.c_getNilaiLevelKPI(kpi);

                return Ok(new { Status = true, data = list });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = "Error: " + e.Message.ToString() });
            }
        }

        [HttpGet]
        [Route("getNilaiLevelKPI_Cash")]
        public IHttpActionResult getNilaiLevelKPI_Cash(string kpi)
        {
            try
            {
                ClsKPI clsKPI = new ClsKPI();

                var list = clsKPI.c_getNilaiLevelKPI_Cash(kpi);

                return Ok(new { Status = true, data = list });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = "Error: " + e.Message.ToString() });
            }
        }


    }
}
