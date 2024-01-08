using KPI_API.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Windows;

namespace KPI_API.Views_Models
{
    public class ClsAssessment
    {
        DB_KPIDataContext db = new DB_KPIDataContext(ConfigurationManager.ConnectionStrings["DB_FATB_KPI_KPTConnectionString"].ConnectionString);

        public IEnumerable<cufn_GetTabelAssessmentDSResult> c_getTabelAssessmentDS()
        {
            var data = db.cufn_GetTabelAssessmentDS().ToList();
            return data;
        }

        public IEnumerable<cufn_GetTabelAssessmentDS_ByFilterResult> c_getTabelAssessmentDS_ByFilter(string district, int periode)
        {
            var data = db.cufn_GetTabelAssessmentDS_ByFilter(district, periode).ToList();
            return data;
        }

        public IEnumerable<cufn_GetTabelAssessmentONResult> c_getTabelAssessmentON()
        {
            var data = db.cufn_GetTabelAssessmentON().ToList();
            return data;
        }

        public IEnumerable<cufn_GetTabelAssessmentON_ByFilterResult> c_getTabelAssessmentON_ByFilter(string district, int periode)
        {
            var data = db.cufn_GetTabelAssessmentON_ByFilter(district, periode).ToList();
            return data;
        }

        public IEnumerable<cusp_GetAssessmentDSResult> c_getAssessmentDS(string district, int periode)
        {
            var data = db.cusp_GetAssessmentDS(district, periode).ToList();
            return data;
        }

        public IEnumerable<cusp_GetAssessmentONResult> c_getAssessmentON(string district, int periode)
        {
            var data = db.cusp_GetAssessmentON(district, periode).ToList();
            return data;
        }

        public void Create_AssessmentDS(TBL_T_ASSESSMENT_TEMP[] tBL_T_ASSESSMENT_TEMPs)
        {
            var dataAssessmentDS = db.TBL_T_ASSESSMENTs.Where(x => x.DISTRICT == tBL_T_ASSESSMENT_TEMPs[0].DISTRICT && x.PERIODE == tBL_T_ASSESSMENT_TEMPs[0].PERIODE && x.YEAR == DateTime.Now.Year).ToList();
            db.TBL_T_ASSESSMENTs.DeleteAllOnSubmit(dataAssessmentDS);

            List<TBL_T_ASSESSMENT_TEMP> tbl = new List<TBL_T_ASSESSMENT_TEMP>();
            foreach (TBL_T_ASSESSMENT_TEMP tblCls in tBL_T_ASSESSMENT_TEMPs)
            {
                tbl.Add(new TBL_T_ASSESSMENT_TEMP
                {
                    KPI_CODE = tblCls.KPI_CODE,
                    DISTRICT = tblCls.DISTRICT,
                    PERIODE = tblCls.PERIODE,
                    PLAN_ACH = tblCls.PLAN_ACH/100,
                    ACTUAL = tblCls.ACTUAL,
                    ACHIEVMENT = tblCls.ACHIEVMENT/100, //(decimal?)decimal.Parse(tblCls.ACHIEVMENT, CultureInfo.InvariantCulture) / 100,
                    REMARK = tblCls.REMARK
                });
            }
            db.TBL_T_ASSESSMENT_TEMPs.InsertAllOnSubmit(tbl);
            db.SubmitChanges();

            db.cusp_InsertAssessmentDS();
        }

        public void Create_AssessmentON(TBL_T_ASSESSMENT_ON_TEMP[] tBL_T_ASSESSMENT_ON_TEMPs)
        {
            var dataAssessmentON = db.TBL_T_ASSESSMENT_ONs.Where(x => x.DISTRICT == tBL_T_ASSESSMENT_ON_TEMPs[0].DISTRICT && x.PERIODE == tBL_T_ASSESSMENT_ON_TEMPs[0].PERIODE && x.YEAR == DateTime.Now.Year).ToList();
            db.TBL_T_ASSESSMENT_ONs.DeleteAllOnSubmit(dataAssessmentON);

            List<TBL_T_ASSESSMENT_ON_TEMP> tbl = new List<TBL_T_ASSESSMENT_ON_TEMP>();
            foreach (TBL_T_ASSESSMENT_ON_TEMP tblClsON in tBL_T_ASSESSMENT_ON_TEMPs)
            {
                tbl.Add(new TBL_T_ASSESSMENT_ON_TEMP
                {
                    KPI_CODE = tblClsON.KPI_CODE,
                    DISTRICT = tblClsON.DISTRICT,
                    PERIODE = tblClsON.PERIODE,
                    ACTUAL = tblClsON.ACTUAL,
                    ACHIEVMENT = tblClsON.ACHIEVMENT / 100, //(decimal?)decimal.Parse(tblCls.ACHIEVMENT, CultureInfo.InvariantCulture) / 100,
                    REMARK = tblClsON.REMARK
                });
            }
            db.TBL_T_ASSESSMENT_ON_TEMPs.InsertAllOnSubmit(tbl);
            db.SubmitChanges();

            db.cusp_InsertAssessmentON();
        }

        public void c_generateExcelAssessment()
        {

            var dataAssessmentDS = db.GetTable<VW_EXCEL_ASSESSMENT_D>().Select(x => new VW_EXCEL_ASSESSMENT_D
            {
                KPI_KABAG_CODE = x.KPI_KABAG_CODE,
                KPI_KABAG = x.KPI_KABAG,
                KPI_CODE = x.KPI_CODE,
                KPI_ITEM = x.KPI_ITEM,
                BOBOT = x.BOBOT,
                PLAN = x.PLAN,
                ACTUAL = Convert.ToInt32(x.ACTUAL),
                ACHIEVMENT = Convert.ToInt32(x.ACHIEVMENT),
                REMARK = Convert.ToInt32(x.REMARK)
            }).ToList();

            var dataAssessmentON = db.GetTable<VW_EXCEL_ASSESSMENT_ON>().Select(x => new VW_EXCEL_ASSESSMENT_ON
            {
                KPI = x.KPI,
                KPI_ITEM = x.KPI_ITEM,
                KPI_CODE = x.KPI_CODE,
                KPI_DESC = x.KPI_DESC,
                BOBOT = x.BOBOT,
                PLAN = x.PLAN,
                ACTUAL = x.ACTUAL,
                ACHIEVMENT = x.ACHIEVMENT,
                REMARK = x.REMARK
            }).ToList();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage excelPackage = new ExcelPackage())
            {

                AddSheetWithData(excelPackage, dataAssessmentDS, "ASSESSMENT_OFFICER");
                AddSheetWithData(excelPackage, dataAssessmentON, "ASSESSMENT_KASIR");

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    excelPackage.SaveAs(memoryStream);

                    string fileName = "TemplateAssessmentKPI.xlsx";
                    string filePath = System.Web.Hosting.HostingEnvironment.MapPath("/Files/" + fileName);
                    //string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "~/Files", fileName);
                    //string filePath = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["fileLocation"].ToString() + "/fileEvidence/");

                    File.WriteAllBytes(filePath, memoryStream.ToArray());
                }
            }
        }

        static void AddHeaderRow(ExcelWorksheet worksheet, Type type)
        {
            var properties = type.GetProperties();
            for (int i = 0; i < properties.Length; i++)
            {
                worksheet.Cells[1, i + 1].Value = properties[i].Name;
            }
        }
        static void AddSheetWithData<T>(ExcelPackage excelPackage, List<T> data, string sheetName) where T : class
        {
            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add(sheetName);

            // Create header row
            AddHeaderRow(worksheet, typeof(T));

            // Add data rows
            for (int row = 0; row < data.Count; row++)
            {
                var properties = typeof(T).GetProperties();
                for (int col = 0; col < properties.Length; col++)
                {
                    worksheet.Cells[row + 2, col + 1].Value = properties[col].GetValue(data[row]);
                }
            }
        }

        public void Create_ExcelAssessment([FromBody] ClsExcelAssessment data)
        {
            var dataAssessmentDS = db.TBL_T_ASSESSMENTs.Where(x => x.DISTRICT == data.clsAssessment_Ds[0].DISTRICT && x.PERIODE == Convert.ToInt32(data.clsAssessment_Ds[0].PERIODE) && x.YEAR == DateTime.Now.Year).ToList();
            var dataAssessmentON = db.TBL_T_ASSESSMENT_ONs.Where(x => x.DISTRICT == data.clsAssessment_On[0].DISTRICT && x.PERIODE == Convert.ToInt32(data.clsAssessment_On[0].PERIODE) && x.YEAR == DateTime.Now.Year).ToList();
            db.TBL_T_ASSESSMENTs.DeleteAllOnSubmit(dataAssessmentDS);
            db.TBL_T_ASSESSMENT_ONs.DeleteAllOnSubmit(dataAssessmentON);


            List<TBL_T_ASSESSMENT_TEMP> tbl1 = new List<TBL_T_ASSESSMENT_TEMP>();
            foreach (ClsAssessment_DS cls1 in data.clsAssessment_Ds)
            {
                tbl1.Add(new TBL_T_ASSESSMENT_TEMP
                {
                    KPI_CODE = cls1.KPI_CODE,
                    DISTRICT = cls1.DISTRICT,
                    PERIODE = Convert.ToInt32(cls1.PERIODE),
                    ACTUAL = cls1.ACTUAL,
                    ACHIEVMENT = (decimal?)decimal.Parse(cls1.ACHIEVMENT, CultureInfo.InvariantCulture) / 100, //(decimal?)decimal.Parse(tblCls.ACHIEVMENT, CultureInfo.InvariantCulture) / 100,
                    REMARK = cls1.REMARK
                });
            }
            db.TBL_T_ASSESSMENT_TEMPs.InsertAllOnSubmit(tbl1);
            //db.SubmitChanges();

            List<TBL_T_ASSESSMENT_ON_TEMP> tbl2 = new List<TBL_T_ASSESSMENT_ON_TEMP>();
            foreach (ClsAssessment_ON cls2 in data.clsAssessment_On)
            {
                tbl2.Add(new TBL_T_ASSESSMENT_ON_TEMP
                {
                    KPI_CODE = cls2.KPI_CODE,
                    DISTRICT = cls2.DISTRICT,
                    PERIODE = Convert.ToInt32(cls2.PERIODE),
                    ACTUAL = cls2.ACTUAL,
                    ACHIEVMENT = (decimal?)decimal.Parse(cls2.ACHIEVMENT, CultureInfo.InvariantCulture) / 100, //(decimal?)decimal.Parse(tblCls.ACHIEVMENT, CultureInfo.InvariantCulture) / 100,
                    REMARK = cls2.REMARK
                });
            }
            db.TBL_T_ASSESSMENT_ON_TEMPs.InsertAllOnSubmit(tbl2);
            db.SubmitChanges();

            db.cusp_InsertAssessmentDS();
            db.cusp_InsertAssessmentON();

        }
    }
}