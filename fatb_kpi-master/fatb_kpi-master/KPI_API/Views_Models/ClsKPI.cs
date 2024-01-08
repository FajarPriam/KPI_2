using KPI_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Web;
using System.Data;
using ExcelDataReader;
using System.Data.Linq;
using System.IO;
using OfficeOpenXml;
using System.Globalization;
using System.Web.Http;
using System.Text;
using System.Runtime.InteropServices.ComTypes;
using OfficeOpenXml.Style;
using System.Runtime.ConstrainedExecution;

namespace KPI_API.Views_Models
{
    public class ClsKPI
    {
        DB_KPIDataContext db = new DB_KPIDataContext(ConfigurationManager.ConnectionStrings["DB_FATB_KPI_KPTConnectionString"].ConnectionString);
        public string KPI_CODE { get; set; }
        public string EDIT_BY { get; set; }
        public string KPI_ITEM { get; set; }
        public string CODE_SECTION { get; set; }
        public string DESKRIPSI { get; set; }
        public string BOBOT { get; set; }
        public string TARGET { get; set; }
        public string[] DeskripsiLevel { get; set; }
        public string[] NilaiLevel { get; set; }
        

        public IEnumerable<VW_NILAI_KPI_D> c_getNilaiKPI_DS()
        {
            var data = db.VW_NILAI_KPI_Ds.ToList();
            return data;
        }

        public IEnumerable<cufn_GetTabelMappingKPIResult> c_getTabelMappingAll()
        {
            var data = db.cufn_GetTabelMappingKPI().ToList();
            return data;
        }

        public IEnumerable<cufn_GetTabelKPIONResult> c_getTabelKPION()
        {
            var data = db.cufn_GetTabelKPION().ToList();
            return data;
        }

        public IEnumerable<cufn_GetTabelKPICashResult> c_getTabelKPICash()
        {
            var data = db.cufn_GetTabelKPICash().ToList();
            return data;
        }

        public IEnumerable<cufn_GetTabelEditKPIResult> c_getTabelEditKPI(string CODE_SECTION)
        {
            var data = db.cufn_GetTabelEditKPI(CODE_SECTION).ToList();
            return data;
        }

        public IEnumerable<cufn_GetTabelEditKPI_CashResult> c_getTabelEditKPI_Cash(string CODE_SECTION)
        {
            var data = db.cufn_GetTabelEditKPI_Cash(CODE_SECTION).ToList();
            return data;
        }

        public IEnumerable<cufn_GetKPIONMappingParentResult> c_getKPIONMappingParent()
        {
            var data = db.cufn_GetKPIONMappingParent().ToList();
            return data;
        }

        public IEnumerable<cufn_GetKPIONMappingChildResult> c_getKPIONMappingChild(string ID_KPI_ON)
        {
            var data = db.cufn_GetKPIONMappingChild(ID_KPI_ON).ToList();
            return data;
        }

        public IEnumerable<cufn_GetTabelMappingKPIONResult> c_getTabelMappingKPION()
        {
            var data = db.cufn_GetTabelMappingKPION().ToList();
            return data;
        }

        public IEnumerable<VW_NILAI_EVALUASI_OFFICER_NONOM> c_getNilaiEvaluasi_ON()
        {
            var data = db.VW_NILAI_EVALUASI_OFFICER_NONOMs.ToList();
            return data;
        }

        public IEnumerable<VW_NILAI_KPI_O> c_getNilaiKPI_OF()
        {
            var data = db.VW_NILAI_KPI_Os.ToList();
            return data;
        }
        //public IEnumerable<VW_ITEM_KPI> c_getItemKPI()
        //{
        //    var data = db.VW_ITEM_KPIs.ToList();
        //    return data;
        //}
        public IEnumerable<VW_M_KPI> c_getItemKPI()
        {
            var data = db.VW_M_KPIs.ToList();
            return data;
        }
        public IEnumerable<VW_ITEM_KPI> c_getItemKPIPeriode(int periode, int year)
        {
            var data = db.VW_ITEM_KPIs.Where(a => a.PERIODE == periode && a.YEAR == year).ToList();
            return data;
        }

        public VW_ITEM_KPI c_getItemKPI(int id)
        {
            var data = db.VW_ITEM_KPIs.Where(a => a.ID_ON == id).FirstOrDefault();
            return data;
        }

        public List<VW_ITEM_KPI> c_getItemKPIDept(string dept, int periode, int year)
        {
            var data = db.VW_ITEM_KPIs.Where(a => a.DEPT_CODE == dept && a.PERIODE == periode && a.YEAR == year).ToList();
            return data;
        }

        public List<VW_M_MAPPING_KPI_DS_HEAD> c_getTabelMappingKPIDS(string ID_KPI_DS)
        {
            var data = db.VW_M_MAPPING_KPI_DS_HEADs.Where(a => a.KPI_DS_ID == ID_KPI_DS).ToList();
            return data;
        }

        public List<VW_M_MAPPING_KPI_ON_BY_CODE> c_getTabelMappingKPIONByKPI(string ID_KPI_ON)
        {
            var data = db.VW_M_MAPPING_KPI_ON_BY_CODEs.Where(a => a.ID_KPI_ON == ID_KPI_ON).ToList();
            return data;
        }

        public IEnumerable<TBL_M_KPI> c_getListKPICode()
        {
            var data = db.TBL_M_KPIs.ToList();
            return data;
        }

        public IEnumerable<TBL_M_KPI_CASHIER > c_getListKPICashier()
        {
            var data = db.TBL_M_KPI_CASHIERs.ToList();
            return data;
        }

        public IEnumerable<TBL_M_PCI_OFFICER_NONOM> c_getListPIC()
        {
            var data = db.TBL_M_PCI_OFFICER_NONOMs.ToList();
            return data;
        }

        public IEnumerable<TBL_M_KPI_DEPT_SECT> c_getItemDS()
        {
            var data = db.TBL_M_KPI_DEPT_SECTs.ToList();
            return data;
        }

        public IEnumerable<TBL_M_POS_EVALUATEE> c_getPosition()
        {
            var data = db.TBL_M_POS_EVALUATEEs.ToList();
            return data;
        }
        
        public IEnumerable<VW_SECTION> c_getItemDSPeriode()
        {
            var data = db.VW_SECTIONs.ToList();
            return data;
        }

        public IEnumerable<VW_SECTIONS_CASHIER> c_getItemDSPeriode_Cash()
        {
            var data = db.VW_SECTIONS_CASHIERs.ToList();
            return data;
        }

        public IEnumerable<TBL_M_SECTION> GetAllSection(string section)
        {
            var data = db.TBL_M_SECTIONs.Where(a => a.CODE == section).ToList();
            return data;
        }

        //public TBL_M_KPI_DEPT_SECT c_getItemDS(int id)
        //{
        //    var data = db.TBL_M_KPI_DEPT_SECTs.Where(a => a.ID == id).FirstOrDefault();
        //    return data;
        //}

        public IEnumerable<TBL_M_KPI_OFFICER> c_getItemOfc()
        {
            var data = db.TBL_M_KPI_OFFICERs.ToList();
            return data;
        }

        public IEnumerable<TBL_R_PERIODE> c_getPeriode()
        {
            var data = db.TBL_R_PERIODEs.ToList();
            return data;
        }

        public TBL_M_KPI_OFFICER c_getItemOfc(int id)
        {
            DateTime dateTime = DateTime.Now;
            var data = db.TBL_M_KPI_OFFICERs.Where(a => a.ID == id).FirstOrDefault();
            return data;
        }

        public void c_deleteDS(string id)
        {
            var data = db.TBL_M_KPI_DEPT_SECTs.Where(a => a.KPI_DS_ID == id).FirstOrDefault();
            db.TBL_M_KPI_DEPT_SECTs.DeleteOnSubmit(data);
            db.SubmitChanges();
        }

        public void c_InsertOrUpdateDS(TBL_M_KPI_DEPT_SECT param)
        {
            DateTime dateTime = DateTime.Now;
            var data = db.TBL_M_KPI_DEPT_SECTs.Where(a => a.KPI_DS_ID == param.KPI_DS_ID).FirstOrDefault();
            if (data != null)
            {
                data.KPI_DS_DESC = param.KPI_DS_DESC;
                data.EDIT_BY = param.EDIT_BY;
                data.EDIT_DATE = dateTime;
            }
            else
            {
                int? num = db.cufn_generateKPICodeDS();
                //var cek_kpi_code = "H." + Convert.ToString(num + 1);
                var cek_kpi_code = "";
                if (num == null)
                {
                    cek_kpi_code = "H." + 1;
                }
                else
                {
                    cek_kpi_code = "H." + Convert.ToString(num + 1);
                }

                TBL_M_KPI_DEPT_SECT item = new TBL_M_KPI_DEPT_SECT();

                item.KPI_DS_ID = cek_kpi_code;
                item.KPI_DS_DESC = param.KPI_DS_DESC;
                item.EDIT_BY = param.EDIT_BY;
                item.EDIT_DATE = dateTime;

                db.TBL_M_KPI_DEPT_SECTs.InsertOnSubmit(item);
            }
            db.SubmitChanges();
        }

        public void c_deleteON(int id)
        {
            var data = db.TBL_M_KPI_OFFICER_NONOMs.Where(a => a.ID == id).FirstOrDefault();
            db.TBL_M_KPI_OFFICER_NONOMs.DeleteOnSubmit(data);
            db.SubmitChanges();
        }

        public void c_deleteKPI(string KPI_CODE)
        {
            var data = db.TBL_M_KPIs.Where(a => a.KPI_CODE == KPI_CODE).FirstOrDefault();
            db.TBL_M_KPIs.DeleteOnSubmit(data);
            db.SubmitChanges();
        }

        public void Delete_DataKPI(string KPI_CODE)
        {
            var data = db.TBL_M_KPIs.Where(x => x.KPI_CODE == KPI_CODE).SingleOrDefault();
            var nilai = db.TBL_M_NILAI_LEVEL_KPIs.Where(x => x.KPI_CODE == KPI_CODE).ToList();

            db.TBL_M_KPIs.DeleteOnSubmit(data);
            db.TBL_M_NILAI_LEVEL_KPIs.DeleteAllOnSubmit(nilai);
            db.SubmitChanges();
            //TBL_M_KPI data = db.TBL_M_KPIs.Where(a => a.KPI_CODE == KPI_CODE).FirstOrDefault();
            //db.TBL_M_KPIs.DeleteOnSubmit(data);
            //db.SubmitChanges();
        }

        public void Delete_DataKPICash(string KPI_CODE)
        {
            var data = db.TBL_M_KPI_CASHIERs.Where(x => x.KPI_CODE == KPI_CODE).SingleOrDefault();
            var nilai = db.TBL_M_NILAI_LEVEL_KPICASHIERs.Where(x => x.KPI_CODE == KPI_CODE).ToList();

            db.TBL_M_KPI_CASHIERs.DeleteOnSubmit(data);
            db.TBL_M_NILAI_LEVEL_KPICASHIERs.DeleteAllOnSubmit(nilai);
            db.SubmitChanges();
            //TBL_M_KPI data = db.TBL_M_KPIs.Where(a => a.KPI_CODE == KPI_CODE).FirstOrDefault();
            //db.TBL_M_KPIs.DeleteOnSubmit(data);
            //db.SubmitChanges();
        }

        public void c_InsertOrUpdateON(TBL_M_KPI[] param)
        {
            List<TBL_M_KPI> tbl = new List<TBL_M_KPI>();
            DateTime dateTime = DateTime.Now;
            foreach (TBL_M_KPI cls in param)
            {
                var data = db.TBL_M_KPIs.Where(a => a.KPI_CODE == cls.KPI_CODE).FirstOrDefault();
                if (data != null)
                {
                    data.KPI_ITEM = cls.KPI_ITEM == "" ? null : cls.KPI_ITEM;
                    data.DESKRIPSI = cls.DESKRIPSI == "" ? null : cls.DESKRIPSI;
                    data.BOBOT = cls.BOBOT == 0 ? null : cls.BOBOT / 100;
                    //data.BOBOT = (decimal?)decimal.Parse(BOBOT, CultureInfo.InvariantCulture);
                    data.TARGET = cls.TARGET == "" ? null : cls.TARGET;
                    data.YEAR = dateTime.Year;
                    data.EDIT_BY = cls.EDIT_BY;
                    data.EDIT_DATE = dateTime;
                    db.SubmitChanges();
                }
                else
                {
                    int? num = db.cufn_generateKPICode(cls.CODE_SECTION);
                    //var cek_kpi_code = cls.CODE_SECTION + "." + Convert.ToString(num + 1);
                    var cek_kpi_code = "";
                    if (num == null)
                    {
                        cek_kpi_code = cls.CODE_SECTION + "." + 1;
                    }
                    else
                    {
                        cek_kpi_code = cls.CODE_SECTION + "." + Convert.ToString(num + 1);
                    }

                    TBL_M_KPI item = new TBL_M_KPI();

                    item.CODE_SECTION = cls.CODE_SECTION;
                    item.KPI_CODE = cek_kpi_code;
                    item.KPI_ITEM = cls.KPI_ITEM == "" ? null : cls.KPI_ITEM;
                    item.DESKRIPSI = cls.DESKRIPSI == "" ? null : cls.DESKRIPSI;
                    item.BOBOT = cls.BOBOT == 0 ? null : cls.BOBOT / 100;
                    item.TARGET = cls.TARGET == "" ? null : cls.TARGET;
                    item.YEAR = dateTime.Year;
                    item.EDIT_BY = cls.EDIT_BY;
                    item.EDIT_DATE = dateTime;

                    db.TBL_M_KPIs.InsertOnSubmit(item);
                    db.SubmitChanges();
                }
            }

            
        }

        public void c_InsertOrUpdateCash(TBL_M_KPI_CASHIER[] param)
        {
            List<TBL_M_KPI_CASHIER> tbl = new List<TBL_M_KPI_CASHIER>();
            DateTime dateTime = DateTime.Now;
            foreach (TBL_M_KPI_CASHIER cls in param)
            {
                var data = db.TBL_M_KPI_CASHIERs.Where(a => a.KPI_CODE == cls.KPI_CODE).FirstOrDefault();
                if (data != null)
                {
                    data.KPI_ITEM = cls.KPI_ITEM == "" ? null : cls.KPI_ITEM;
                    data.DESKRIPSI = cls.DESKRIPSI == "" ? null : cls.DESKRIPSI;
                    data.BOBOT = cls.BOBOT == 0 ? null : cls.BOBOT / 100;
                    //data.BOBOT = (decimal?)decimal.Parse(BOBOT, CultureInfo.InvariantCulture);
                    data.TARGET = cls.TARGET == "" ? null : cls.TARGET;
                    data.YEAR = dateTime.Year;
                    data.EDIT_BY = cls.EDIT_BY;
                    data.EDIT_DATE = dateTime;
                    db.SubmitChanges();
                }
                else
                {
                    int? num = db.cufn_generateKPICash_Code(cls.CODE_SECTION);
                    //var cek_kpi_code = cls.CODE_SECTION + "." + Convert.ToString(num + 1);
                    var cek_kpi_code = "";
                    if (num == null)
                    {
                        cek_kpi_code = cls.CODE_SECTION + "." + 0 + 1;
                    }
                    else
                    {
                        cek_kpi_code = cls.CODE_SECTION + "." + 0 + Convert.ToString(num + 1);
                    }

                    TBL_M_KPI_CASHIER item = new TBL_M_KPI_CASHIER();

                    item.CODE_SECTION = cls.CODE_SECTION;
                    item.KPI_CODE = cek_kpi_code;
                    item.KPI_ITEM = cls.KPI_ITEM == "" ? null : cls.KPI_ITEM;
                    item.DESKRIPSI = cls.DESKRIPSI == "" ? null : cls.DESKRIPSI;
                    item.BOBOT = cls.BOBOT == 0 ? null : cls.BOBOT / 100;
                    item.TARGET = cls.TARGET == "" ? null : cls.TARGET;
                    item.YEAR = dateTime.Year;
                    item.EDIT_BY = cls.EDIT_BY;
                    item.EDIT_DATE = dateTime;

                    db.TBL_M_KPI_CASHIERs.InsertOnSubmit(item);
                    db.SubmitChanges();
                }
            }


        }

        public void c_AddKPION(TBL_M_KPI param)
        {
            DateTime dateTime = DateTime.Now;
            int? num = db.cufn_generateKPICode(param.CODE_SECTION);
            //var cek_kpi_code = param.CODE_SECTION + "." + Convert.ToString(num + 1);
            var cek_kpi_code = "";
            if (num == null)
            {
                cek_kpi_code = param.CODE_SECTION + "." + 1;
            }
            else
            {
                cek_kpi_code = param.CODE_SECTION + "." + Convert.ToString(num + 1);
            }

            TBL_M_KPI item = new TBL_M_KPI();

            item.CODE_SECTION = param.CODE_SECTION;
            item.KPI_CODE = cek_kpi_code;
            item.KPI_ITEM = null;
            item.DESKRIPSI = null;
            item.BOBOT = null;
            item.TARGET = null;
            item.YEAR = dateTime.Year;
            item.EDIT_BY = null;
            item.EDIT_DATE = null;

            db.TBL_M_KPIs.InsertOnSubmit(item);
            db.SubmitChanges();

        }

        public void c_AddKPICash(TBL_M_KPI_CASHIER param)
        {
            DateTime dateTime = DateTime.Now;
            int? num = db.cufn_generateKPICash_Code(param.CODE_SECTION);
            //var cek_kpi_code = param.CODE_SECTION + "." + Convert.ToString(num + 1);
            var cek_kpi_code = "";
            if (num == null)
            {
                cek_kpi_code = param.CODE_SECTION + "." + 0 + 1;
            }
            else
            {
                cek_kpi_code = param.CODE_SECTION + "." + 0 + Convert.ToString(num + 1);
            }

            TBL_M_KPI_CASHIER item = new TBL_M_KPI_CASHIER();

            item.CODE_SECTION = param.CODE_SECTION;
            item.KPI_CODE = cek_kpi_code;
            item.KPI_ITEM = null;
            item.DESKRIPSI = null;
            item.BOBOT = null;
            item.TARGET = null;
            item.YEAR = dateTime.Year;
            item.EDIT_BY = null;
            item.EDIT_DATE = null;

            db.TBL_M_KPI_CASHIERs.InsertOnSubmit(item);
            db.SubmitChanges();

        }

        public void c_AddLevelKPI()
        {
            var deleteNilaiLevel = db.TBL_M_NILAI_LEVEL_KPIs.Where(x => x.KPI_CODE == KPI_CODE).ToList();
            db.TBL_M_NILAI_LEVEL_KPIs.DeleteAllOnSubmit(deleteNilaiLevel);

            for (int i = 0; i < 5; i++)
            {
                TBL_M_NILAI_LEVEL_KPI tbl = new TBL_M_NILAI_LEVEL_KPI();
                tbl.NILAI = i + 1; //Convert.ToInt32(data.ID_COMPETENCY);
                tbl.KPI_CODE = KPI_CODE;
                tbl.NILAI_LEVEL = NilaiLevel[i];
                tbl.NILAI_DESC = DeskripsiLevel[i];

                db.TBL_M_NILAI_LEVEL_KPIs.InsertOnSubmit(tbl);
            }
            db.SubmitChanges();
        }

        public void c_AddLevelKPI_Cash()
        {
            var deleteNilaiLevel = db.TBL_M_NILAI_LEVEL_KPICASHIERs.Where(x => x.KPI_CODE == KPI_CODE).ToList();
            db.TBL_M_NILAI_LEVEL_KPICASHIERs.DeleteAllOnSubmit(deleteNilaiLevel);

            for (int i = 0; i < 5; i++)
            {
                TBL_M_NILAI_LEVEL_KPICASHIER tbl = new TBL_M_NILAI_LEVEL_KPICASHIER();
                tbl.NILAI = i + 1; //Convert.ToInt32(data.ID_COMPETENCY);
                tbl.KPI_CODE = KPI_CODE;
                tbl.NILAI_LEVEL = NilaiLevel[i];
                tbl.NILAI_DESC = DeskripsiLevel[i];

                db.TBL_M_NILAI_LEVEL_KPICASHIERs.InsertOnSubmit(tbl);
            }
            db.SubmitChanges();
        }


        public void c_deleteOF(int id)
        {
            var data = db.TBL_M_KPI_OFFICERs.Where(a => a.ID == id).FirstOrDefault();
            db.TBL_M_KPI_OFFICERs.DeleteOnSubmit(data);
            db.SubmitChanges();
        }

        public void c_InsertOrUpdateOF(TBL_M_KPI_OFFICER param)
        {
            DateTime dateTime = DateTime.Now;
            var data = db.TBL_M_KPI_OFFICERs.Where(a => a.ID == param.ID).FirstOrDefault();
            if (data != null)
            {
                data.ITEM = param.ITEM;
                data.INPUT_DATE = dateTime;
                data.INPUT_BY = param.INPUT_BY;
                data.ID_KPI_DS = param.ID_KPI_DS;
            }
            else
            {
                TBL_M_KPI_OFFICER item = new TBL_M_KPI_OFFICER();

                item.ITEM = param.ITEM;
                item.ID_KPI_DS = param.ID_KPI_DS;
                item.INPUT_DATE = dateTime;
                item.INPUT_BY = param.INPUT_BY;
                item.PERIODE = dateTime.Month;
                item.YEAR = dateTime.Year;

                db.TBL_M_KPI_OFFICERs.InsertOnSubmit(item);
            }
            db.SubmitChanges();
        }

        public void c_PenilaianKPI(List<TBL_T_POINT_KPI_OFFICER_NONOM> param)
        {
            List<TBL_T_POINT_KPI_OFFICER_NONOM> newList = new List<TBL_T_POINT_KPI_OFFICER_NONOM>();
            foreach (var item in param)
            {
                var cekKPI = db.TBL_T_POINT_KPI_OFFICER_NONOMs.Where(a => a.DSTRCT == item.DSTRCT && a.ID_KPI_ON == item.ID_KPI_ON).FirstOrDefault();
                if (cekKPI != null)
                {
                    cekKPI.POINT = item.POINT;
                }
                else
                {
                    TBL_T_POINT_KPI_OFFICER_NONOM newData = new TBL_T_POINT_KPI_OFFICER_NONOM
                    {
                        ID_KPI_ON = item.ID_KPI_ON,
                        INPUT_BY = item.INPUT_BY,
                        INPUT_DATE = DateTime.Now,
                        DSTRCT = item.DSTRCT,
                        POINT = item.POINT
                    };
                    newList.Add(newData);
                }
            }

            db.TBL_T_POINT_KPI_OFFICER_NONOMs.InsertAllOnSubmit(newList);
            db.SubmitChanges();
        }

        public void c_AddMapping(string KPI_DS_ID)
        {
            TBL_M_MAPPING_KPI data = new TBL_M_MAPPING_KPI();
            data.ID_KPI_DS = KPI_DS_ID;
            db.TBL_M_MAPPING_KPIs.InsertOnSubmit(data);
            db.SubmitChanges();
        }

        public void c_DeleteMapping(int ID)
        {
            TBL_M_MAPPING_KPI data = db.TBL_M_MAPPING_KPIs.Where(a => a.ID == ID).FirstOrDefault();
            db.TBL_M_MAPPING_KPIs.DeleteOnSubmit(data);
            db.SubmitChanges();
        }

        public void c_DeleteMappingON(int ID)
        {
            TBL_M_MAPPING_KPI_ON data = db.TBL_M_MAPPING_KPI_ONs.Where(a => a.ID == ID).FirstOrDefault();
            db.TBL_M_MAPPING_KPI_ONs.DeleteOnSubmit(data);
            db.SubmitChanges();
        }

        public void c_DeleteMappingAll(string ID_KPI)
        {
            var deleteMapping = db.TBL_M_MAPPING_KPIs.Where(x => x.ID_KPI_DS == ID_KPI).ToList();
            db.TBL_M_MAPPING_KPIs.DeleteAllOnSubmit(deleteMapping);
            db.SubmitChanges();
        }

        public void c_DeleteMappingAllON(string ID_KPI)
        {
            var deleteMappingON = db.TBL_M_MAPPING_KPI_ONs.Where(x => x.ID_KPI_ON == ID_KPI).ToList();
            db.TBL_M_MAPPING_KPI_ONs.DeleteAllOnSubmit(deleteMappingON);
            db.SubmitChanges();
        }

        public void c_SubmitAllMapping(ClsEKITemp[] clsEKITemps)
        {
            

            List<TBL_M_MAPPING_KPI> tbl = new List<TBL_M_MAPPING_KPI>();
            Console.WriteLine(clsEKITemps.Length);
            foreach (ClsEKITemp cls in clsEKITemps)
            {
                tbl.Add(new TBL_M_MAPPING_KPI
                {
                    ID_KPI_DS = cls.ID_KPI_DS,
                    KPI_CODE = cls.KPI_CODE,
                    ID_PIC_OFFICER_NONOM = Convert.ToInt32(cls.ID_PIC_OFFICER_NONOM)
                });
                
            }
            db.TBL_M_MAPPING_KPIs.InsertAllOnSubmit(tbl);
            db.SubmitChanges();

        }

        public void c_SubmitAllMappingON(ClsMappingONTemp[] clsMappingONTemps)
        {


            List<TBL_M_MAPPING_KPI_ON> tbl = new List<TBL_M_MAPPING_KPI_ON>();
            foreach (ClsMappingONTemp cls in clsMappingONTemps)
            {
                tbl.Add(new TBL_M_MAPPING_KPI_ON
                {
                    ID_KPI_ON = cls.ID_KPI_ON,
                    KPI_CODE = cls.KPI_CODE,
                    ID_PIC = Convert.ToInt32(cls.ID_PIC)
                });

            }
            db.TBL_M_MAPPING_KPI_ONs.InsertAllOnSubmit(tbl);
            db.SubmitChanges();

        }

        public void c_AddMappingKPION(TBL_M_MAPPING_KPI_ON tbl)
        {
            TBL_M_MAPPING_KPI_ON data = new TBL_M_MAPPING_KPI_ON();
            data.ID_KPI_ON = tbl.ID_KPI_ON;
            db.TBL_M_MAPPING_KPI_ONs.InsertOnSubmit(data);
            db.SubmitChanges();
        }

        //================================================================================================================================
        //Upload item KPI
        public void c_uploadItemKPI(HttpPostedFile file)
        {
            List<TBL_M_KPI_OFFICER_NONOM> listItem = new List<TBL_M_KPI_OFFICER_NONOM>();
            DateTime dateTime = DateTime.Now;
            using (var stream = file.InputStream)
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    // Assuming the first sheet is the one you want to read from
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration
                        {
                            UseHeaderRow = true // Treat the first row as column headers
                        }
                    });

                    var dataTable = result.Tables[0]; // Assuming the first table in the DataSet
                    //bool isFirstRow = true;

                    // Iterate through the rows and populate the DataMaintenance objects
                    foreach (DataRow row in dataTable.Rows)
                    {

                        //// Skip the first row (header row)
                        //if (isFirstRow)
                        //{
                        //    isFirstRow = false;
                        //    continue;
                        //}

                        int? ID_KPI_O = null;
                        if (row[2] != DBNull.Value)
                        {
                            ID_KPI_O = Convert.ToInt32(row[2].ToString());
                        }

                        TBL_M_KPI_OFFICER_NONOM data = new TBL_M_KPI_OFFICER_NONOM
                        {
                            ID_KPI_DS = Convert.ToInt32(row[1].ToString()),
                            ID_KPI_O = ID_KPI_O,
                            ID_DEPT = row[3].ToString(),
                            ID_POS_EVALUATE = Convert.ToInt32(row[4].ToString()),
                            ITEM = row[5].ToString(),
                            BOBOT = Convert.ToInt32(row[6].ToString()),
                            PERIODE = dateTime.Month,
                            YEAR = dateTime.Year,
                            INPUT_DATE = dateTime,
                        };
                        listItem.Add(data);
                    }
                }
            }
            db.TBL_M_KPI_OFFICER_NONOMs.InsertAllOnSubmit(listItem);
            db.SubmitChanges();
        }
        
        public void c_uploadPointKPI(HttpPostedFile file)
        {
            List<TBL_T_POINT_KPI_OFFICER_NONOM> listItem = new List<TBL_T_POINT_KPI_OFFICER_NONOM>();
            DateTime dateTime = DateTime.Now;
            using (var stream = file.InputStream)
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    // Assuming the first sheet is the one you want to read from
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration
                        {
                            UseHeaderRow = true // Treat the first row as column headers
                        }
                    });

                    var dataTable = result.Tables[0]; // Assuming the first table in the DataSet
                    //bool isFirstRow = true;

                    // Iterate through the rows and populate the DataMaintenance objects
                    foreach (DataRow row in dataTable.Rows)
                    {

                        // Skip the first row (header row)
                        //if (isFirstRow)
                        //{
                        //    isFirstRow = false;
                        //    continue;
                        //}

                        TBL_T_POINT_KPI_OFFICER_NONOM data = new TBL_T_POINT_KPI_OFFICER_NONOM
                        {
                            ID_KPI_ON = Convert.ToInt32(row[1].ToString()),
                            POINT = Convert.ToInt32(row[2].ToString()),
                            DSTRCT = row[3].ToString(),
                            INPUT_BY = row[4].ToString(),
                            INPUT_DATE = dateTime,
                        };
                        listItem.Add(data);
                    }

                    c_PenilaianKPI(listItem);
                }
            }
        }

        public void c_generateExcelItemKPI()
        {
            DateTime dateTime = DateTime.Now;
            int Periode = dateTime.Month;
            int year = dateTime.Year;

            List<TBL_M_SECTION> sectionData = db.GetTable<TBL_M_SECTION>().ToList();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            // Specify the path to the existing template file
            string fileName = "TemplateItemKPI.xlsx";
            string filePath = System.Web.Hosting.HostingEnvironment.MapPath("/Files/" + fileName);

            using (ExcelPackage templatePackage = new ExcelPackage(new FileInfo(filePath)))
            {
                using (ExcelPackage excelPackage = new ExcelPackage())
                {
                    ExcelWorksheet worksheetNonom1 = excelPackage.Workbook.Worksheets.Add("Data Master KPI Officer");
                    ExcelWorksheet worksheetNonom2 = excelPackage.Workbook.Worksheets.Add("Data Master KPI Cashier");
                    ExcelWorksheet worksheetNonom3 = excelPackage.Workbook.Worksheets.Add("Data Master KPI Dept Sect");

                    foreach (var templateWorksheet in templatePackage.Workbook.Worksheets)
                    {

                        int sheetIndex = templateWorksheet.Index;
                        if (sheetIndex == 0)
                        {
                    
                            // Copy the header row from the template to the new worksheet
                            for (int col = 1; col <= templateWorksheet.Dimension.End.Column; col++)
                            {
                                worksheetNonom1.Cells[1, col].Value = templateWorksheet.Cells[1, col].Value;
                                ExcelRange cellnotes = worksheetNonom1.Cells[1, col];

                                // Add a comment to the cell
                                cellnotes.AddComment("Mandatory!", "KPISystem");
                                // Set the background color of the entire column
                                cellnotes.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                cellnotes.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Red);


                            }
                        }
                        else if (sheetIndex == 1)
                        {
                            // Copy the header row from the template to the new worksheet
                            for (int col = 1; col <= templateWorksheet.Dimension.End.Column; col++)
                            {
                                worksheetNonom2.Cells[1, col].Value = templateWorksheet.Cells[1, col].Value;
                                ExcelRange cellnotes = worksheetNonom2.Cells[1, col];

                                // Add a comment to the cell
                                cellnotes.AddComment("Mandatory!", "KPISystem");
                                cellnotes.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                cellnotes.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Red);



                            }
                        }
                        else if (sheetIndex == 2)
                        {
                            // Copy the header row from the template to the new worksheet
                            for (int col = 1; col <= templateWorksheet.Dimension.End.Column; col++)
                            {
                                worksheetNonom3.Cells[1, col].Value = templateWorksheet.Cells[1, col].Value;
                                ExcelRange cellnotes = worksheetNonom3.Cells[1, col];

                                // Add a comment to the cell
                                cellnotes.AddComment("Mandatory!", "KPISystem");
                                cellnotes.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                cellnotes.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Red);
                            }
                        }


                    }

                    AddSheetWithData(excelPackage, sectionData, "Section");

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        excelPackage.SaveAs(memoryStream);

                        File.WriteAllBytes(filePath, memoryStream.ToArray());
                    }


                }
            }
        }

        public void c_generateExcelMappingKPI()
        {
            DateTime dateTime = DateTime.Now;
            int Periode = dateTime.Month;
            int year = dateTime.Year;

            List<TBL_M_KPI> dataKPI_ON = db.GetTable<TBL_M_KPI>().ToList();
            List<TBL_M_KPI_DEPT_SECT> dataKPI_DS = db.GetTable<TBL_M_KPI_DEPT_SECT>().ToList();
            List<TBL_M_PCI_OFFICER_NONOM> dataPIC = db.GetTable<TBL_M_PCI_OFFICER_NONOM>().ToList();

            //var dataKPI = db.GetTable<TBL_M_KPI>().Select(a => new { a.BOBOT, a.DESKRIPSI}).ToList();

            //List<TBL_M_KPI_DEPT_SECT> kpiDeptSectData = db.GetTable<TBL_M_KPI_DEPT_SECT>().Where(a => a.PERIOD == Periode && a.YEAR == year).ToList();
            //List<TBL_M_KPI_OFFICER> kpiOfficerData = db.GetTable<TBL_M_KPI_OFFICER>().Where(a => a.PERIODE == Periode && a.YEAR == year).ToList();
            //List<TBL_M_DEPT_IN_CHARGE> deptTable = db.GetTable<TBL_M_DEPT_IN_CHARGE>().ToList();
            //List<TBL_M_POS_EVALUATEE> posEvaluateTable = db.GetTable<TBL_M_POS_EVALUATEE>().ToList();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet worksheetNonom2 = excelPackage.Workbook.Worksheets.Add("Mapping_KPI");
                AddHeaderRow(worksheetNonom2, typeof(ClsTemplateMapping));

                AddSheetWithData(excelPackage, dataKPI_ON, "TBL_M_KPI");
                AddSheetWithData(excelPackage, dataKPI_DS, "TBL_M_KPI_DEPT_SECT");
                AddSheetWithData(excelPackage, dataPIC, "TBL_M_PIC_OFFICER_NONOM");

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    excelPackage.SaveAs(memoryStream);

                    memoryStream.Position = 0;

                    string fileName = "TemplateMappingKPI.xlsx";
                    string filePath = System.Web.Hosting.HostingEnvironment.MapPath("/Files/" + fileName);
                    File.WriteAllBytes(filePath, memoryStream.ToArray());
                }
            }
        }

        public void c_generateExcelPointKPI()
        {
            DateTime dateTime = DateTime.Now;
            int Periode = dateTime.Month;
            int year = dateTime.Year;

            List<VW_ITEM_KPI> kpiData = db.GetTable<VW_ITEM_KPI>().Where(a => a.PERIODE == Periode && a.YEAR == year).ToList();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet worksheetNonom = excelPackage.Workbook.Worksheets.Add("TBL_T_POINT_KPI_OFFICER_NONOM");
                AddHeaderRow(worksheetNonom, typeof(TBL_T_POINT_KPI_OFFICER_NONOM));

                AddSheetWithData(excelPackage, kpiData, "kpiData");

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    excelPackage.SaveAs(memoryStream);

                    string fileName = "TemplatePointKPI.xlsx";
                    string filePath = System.Web.Hosting.HostingEnvironment.MapPath("/Files/" + fileName);
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
        //End Upload Item KPI
        //==================================================================================================

        public IEnumerable<VW_DSTRCT_ELP> c_getDistrict()
        {
            var list = db.VW_DSTRCT_ELPs.ToList();
            return list;
        }

        public IEnumerable<VW_M_NILAI_LEVEL_KPI> c_getNilaiLevelKPI(string kpi)
        {
            var list = db.VW_M_NILAI_LEVEL_KPIs.Where(a => a.KPI_CODE == kpi).OrderBy(a => a.NILAI).ToList();
            return list;
        }

        public IEnumerable<VW_M_NILAI_LEVEL_KPICASHIER> c_getNilaiLevelKPI_Cash(string kpi)
        {
            var list = db.VW_M_NILAI_LEVEL_KPICASHIERs.Where(a => a.KPI_CODE == kpi).OrderBy(a => a.NILAI).ToList();
            return list;
        }

        public void Create_ExcelKPI([FromBody] ClsExcelKPI data)
        {
            DateTime dateTime = DateTime.Now;



            List<TBL_M_KPI> tbl1 = new List<TBL_M_KPI>();
            foreach (ClsTemplate cls1 in data.clsTemplates)
            {
                int? num = db.cufn_generateKPICode(cls1.CODE_SECTION);
                var cek_kpi_code = "";
                if (num == null)
                {
                    cek_kpi_code = cls1.CODE_SECTION + "." + 1;
                }
                else
                {
                    cek_kpi_code = cls1.CODE_SECTION + "." + Convert.ToString(num + 1);
                }
                

                TBL_M_KPI item = new TBL_M_KPI();

                item.CODE_SECTION = cls1.CODE_SECTION;
                item.KPI_CODE = cek_kpi_code;
                item.KPI_ITEM = cls1.KPI_ITEM == "" ? null : cls1.KPI_ITEM;
                item.DESKRIPSI = cls1.DESKRIPSI == "" ? null : cls1.DESKRIPSI;
                item.BOBOT = (decimal?)decimal.Parse(cls1.BOBOT, CultureInfo.InvariantCulture) /100;
                item.TARGET = cls1.TARGET == "" ? null : cls1.TARGET;
                item.YEAR = dateTime.Year;
                item.EDIT_BY = cls1.EDIT_BY;
                item.EDIT_DATE = dateTime;

                db.TBL_M_KPIs.InsertOnSubmit(item);
                db.SubmitChanges();

            }

            List<TBL_M_KPI_CASHIER> tbl3 = new List<TBL_M_KPI_CASHIER>();
            foreach (ClsTemplateCash cls3 in data.clsTemplatesCash)
            {
                int? num = db.cufn_generateKPICash_Code(cls3.CODE_SECTION);
                var cek_kpi_code = "";
                if (num == null)
                {
                    cek_kpi_code = cls3.CODE_SECTION + "." + 0 + 1;
                }
                else
                {
                    cek_kpi_code = cls3.CODE_SECTION + "." + 0 + Convert.ToString(num + 1);
                }


                TBL_M_KPI_CASHIER item = new TBL_M_KPI_CASHIER();

                item.CODE_SECTION = cls3.CODE_SECTION;
                item.KPI_CODE = cek_kpi_code;
                item.KPI_ITEM = cls3.KPI_ITEM == "" ? null : cls3.KPI_ITEM;
                item.DESKRIPSI = cls3.DESKRIPSI == "" ? null : cls3.DESKRIPSI;
                item.BOBOT = (decimal?)decimal.Parse(cls3.BOBOT, CultureInfo.InvariantCulture) / 100;
                item.TARGET = cls3.TARGET == "" ? null : cls3.TARGET;
                item.YEAR = dateTime.Year;
                item.EDIT_BY = cls3.EDIT_BY;
                item.EDIT_DATE = dateTime;

                db.TBL_M_KPI_CASHIERs.InsertOnSubmit(item);
                db.SubmitChanges();

            }

            List<TBL_M_KPI_DEPT_SECT> tbl2 = new List<TBL_M_KPI_DEPT_SECT>();
            foreach (ClsTemplateDS cls2 in data.clsTemplateDs)
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
            }

            db.SubmitChanges();
        }

        public void Create_ExcelMapping(ClsTemplateMapping[] clsTemplateMappings)
        {
            List<TBL_M_MAPPING_KPI> tbl = new List<TBL_M_MAPPING_KPI>();
            foreach (ClsTemplateMapping cls in clsTemplateMappings)
            {
                tbl.Add(new TBL_M_MAPPING_KPI
                {
                    ID_KPI_DS = cls.ID_KPI_DS,
                    //CATEGORY = Convert.ToInt32(cls.KATEGORI_ID),
                    KPI_CODE = cls.KPI_CODE,
                    ID_PIC_OFFICER_NONOM = Convert.ToInt32(cls.ID_PIC_OFFICER_NONOM),
                });
                    
            }
            db.TBL_M_MAPPING_KPIs.InsertAllOnSubmit(tbl);
            db.SubmitChanges();
        }

        public void Update_ExcelKPI([FromBody] ClsUpdate_Excel data)
        {
            DateTime dateTime = DateTime.Now;
            List<TBL_M_KPI> tbl1 = new List<TBL_M_KPI>();
                
            foreach (ClsUpdateKPI cls1 in data.clsUpdateKPI)
            {
                var KPI = db.TBL_M_KPIs.Where(a => a.KPI_CODE == cls1.KPI_CODE).FirstOrDefault();
                int bobot = 0;
                if (cls1.BOBOT.Contains("0.") && cls1.BOBOT.Length > 1)
                {
                    decimal bobotdec = decimal.Parse(cls1.BOBOT);
                    string str_bobot = bobotdec.ToString();
                    if (str_bobot.Length == 1)
                    {
                        bobot = Convert.ToInt32(bobotdec) * 10;
                    }
                    else
                    {
                        bobot = Convert.ToInt32(bobotdec);
                    }
                }
                else
                {
                    bobot = Convert.ToInt32(cls1.BOBOT) * 100;
                }
                if (KPI != null)
                {
                    

                    KPI.KPI_ITEM = cls1.KPI_ITEM;
                    KPI.DESKRIPSI = cls1.DESKRIPSI;
                    KPI.BOBOT = (decimal?)decimal.Parse(bobot.ToString(), CultureInfo.InvariantCulture) / 100;
                    KPI.TARGET = cls1.TARGET;
                    KPI.YEAR = dateTime.Year;
                    KPI.EDIT_BY = cls1.EDIT_BY;
                    KPI.EDIT_DATE = dateTime;
                    db.SubmitChanges();
                }
                else
                {

                    int? num = db.cufn_generateKPICode(cls1.CODE_SECTION);
                    var cek_kpi_code = "";
                    if (num == null)
                    {
                        cek_kpi_code = cls1.CODE_SECTION + "." + 1;
                    }
                    else
                    {
                        cek_kpi_code = cls1.CODE_SECTION + "." + Convert.ToString(num + 1);
                    }


                    TBL_M_KPI item = new TBL_M_KPI();

                    item.CODE_SECTION = cls1.CODE_SECTION;
                    item.KPI_CODE = cek_kpi_code;
                    item.KPI_ITEM = cls1.KPI_ITEM == "" ? null : cls1.KPI_ITEM;
                    item.DESKRIPSI = cls1.DESKRIPSI == "" ? null : cls1.DESKRIPSI;
                    item.BOBOT = (decimal?)decimal.Parse(bobot.ToString(), CultureInfo.InvariantCulture) / 100;
                    item.TARGET = cls1.TARGET == "" ? null : cls1.TARGET;
                    item.YEAR = dateTime.Year;
                    item.EDIT_BY = cls1.EDIT_BY;
                    item.EDIT_DATE = dateTime;

                    db.TBL_M_KPIs.InsertOnSubmit(item);
                    db.SubmitChanges();
                }

            }
  
        }

        public void Update_ExcelCash([FromBody] ClsUpdate_Excel data)
        {
            DateTime dateTime = DateTime.Now;
            List<TBL_M_KPI> tbl1 = new List<TBL_M_KPI>();

            foreach (ClsUpdateCashier cls1 in data.clsUpdateCashier)
            {
                var KPI = db.TBL_M_KPI_CASHIERs.Where(a => a.KPI_CODE == cls1.KPI_CODE).FirstOrDefault();
                int bobot = 0;
                if (cls1.BOBOT.Contains("0.") && cls1.BOBOT.Length > 1)
                {
                    decimal bobotdec = decimal.Parse(cls1.BOBOT);
                    string str_bobot = bobotdec.ToString();
                    if (str_bobot.Length == 1)
                    {
                        bobot = Convert.ToInt32(bobotdec) * 10;
                    }
                    else
                    {
                        bobot = Convert.ToInt32(bobotdec);
                    }
                }
                else
                {
                    bobot = Convert.ToInt32(cls1.BOBOT) * 100;
                }
                if (KPI != null)
                {


                    KPI.KPI_ITEM = cls1.KPI_ITEM;
                    KPI.DESKRIPSI = cls1.DESKRIPSI;
                    KPI.BOBOT = (decimal?)decimal.Parse(bobot.ToString(), CultureInfo.InvariantCulture) / 100;
                    KPI.TARGET = cls1.TARGET;
                    KPI.YEAR = dateTime.Year;
                    KPI.EDIT_BY = cls1.EDIT_BY;
                    KPI.EDIT_DATE = dateTime;
                    db.SubmitChanges();
                }
                else
                {

                    int? num = db.cufn_generateKPICash_Code(cls1.CODE_SECTION);
                    var cek_kpi_code = "";
                    if (num == null)
                    {
                        cek_kpi_code = cls1.CODE_SECTION + "." + "0" + 1;
                    }
                    else
                    {
                        cek_kpi_code = cls1.CODE_SECTION + "."+ "0" + Convert.ToString(num + 1);
                    }


                    TBL_M_KPI_CASHIER item = new TBL_M_KPI_CASHIER();

                    item.CODE_SECTION = cls1.CODE_SECTION;
                    item.KPI_CODE = cek_kpi_code;
                    item.KPI_ITEM = cls1.KPI_ITEM == "" ? null : cls1.KPI_ITEM;
                    item.DESKRIPSI = cls1.DESKRIPSI == "" ? null : cls1.DESKRIPSI;
                    item.BOBOT = (decimal?)decimal.Parse(bobot.ToString(), CultureInfo.InvariantCulture) / 100;
                    item.TARGET = cls1.TARGET == "" ? null : cls1.TARGET;
                    item.YEAR = dateTime.Year;
                    item.EDIT_BY = cls1.EDIT_BY;
                    item.EDIT_DATE = dateTime;

                    db.TBL_M_KPI_CASHIERs.InsertOnSubmit(item);
                    db.SubmitChanges();
                }

            }

        }

        public void getAllDataKPI()
        {
            DateTime dateTime = DateTime.Now;
            int Periode = dateTime.Month;
            int year = dateTime.Year;

            List<TBL_M_SECTION> sectionData = db.GetTable<TBL_M_SECTION>().ToList();
            List<TBL_M_KPI> dataKPI_ON = db.GetTable<TBL_M_KPI>().ToList();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            // Specify the path to the existing template file
            string fileName = "UpdateAllDataOfficer.xlsx";
            string filePath = System.Web.Hosting.HostingEnvironment.MapPath("/Files/" + fileName);

            using (ExcelPackage templatePackage = new ExcelPackage(new FileInfo(filePath)))
            {
                using (ExcelPackage excelPackage = new ExcelPackage())
                {
                    ExcelWorksheet worksheetNonom1 = excelPackage.Workbook.Worksheets.Add("Data Master KPI");

                    foreach (var templateWorksheet in templatePackage.Workbook.Worksheets)
                    {

                        int sheetIndex = templateWorksheet.Index;
                        if (sheetIndex == 0)
                        {
                            for (int col = 1; col <= templateWorksheet.Dimension.End.Column; col++)
                            {
                                if (col == 2) { // Specify the column indices you want to make read-only

                                    worksheetNonom1.Column(col).Style.Locked = true;
                                    worksheetNonom1.Cells[1, col].Value = templateWorksheet.Cells[1, col].Value;
                                    ExcelRange cellnotes = worksheetNonom1.Cells[1, col];
                                    cellnotes.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    cellnotes.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.ForestGreen);

                                }
                                else
                                {
                                    worksheetNonom1.Cells[1, col].Value = templateWorksheet.Cells[1, col].Value;
                                    ExcelRange cellnotes = worksheetNonom1.Cells[1, col];
                                    cellnotes.AddComment("Wajib diisi!", "KPISystem");
                                    cellnotes.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    cellnotes.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Red);
                                }

                            }
                        }



                    }

                    AddSheetWithData(excelPackage, sectionData, "Section");
                    for (int row = 0; row < dataKPI_ON.Count; row++)
                    {
                        var properties = dataKPI_ON[row].GetType().GetProperties();
                        for (int col = 0; col < properties.Length - 2; col++)
                        {
                            worksheetNonom1.Cells[row + 2, col + 1].Value = properties[col].GetValue(dataKPI_ON[row]);
                            
                        }
                    }

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        memoryStream.Seek(0, SeekOrigin.Begin);
                        excelPackage.SaveAs(memoryStream);

                        memoryStream.Position = 0;

                        File.WriteAllBytes(filePath, memoryStream.ToArray());
                    }

                }
            }
        }

        public void getAllDataKPI_Cash()
        {
            DateTime dateTime = DateTime.Now;
            int Periode = dateTime.Month;
            int year = dateTime.Year;

            List<TBL_M_SECTION> sectionData = db.GetTable<TBL_M_SECTION>().ToList();
            List<TBL_M_KPI_CASHIER> dataKPI_Cash = db.GetTable<TBL_M_KPI_CASHIER>().ToList();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            // Specify the path to the existing template file
            string fileName = "UpdateAllDataCashier.xlsx";
            string filePath = System.Web.Hosting.HostingEnvironment.MapPath("/Files/" + fileName);

            using (ExcelPackage templatePackage = new ExcelPackage(new FileInfo(filePath)))
            {
                using (ExcelPackage excelPackage = new ExcelPackage())
                {
                    ExcelWorksheet worksheetNonom1 = excelPackage.Workbook.Worksheets.Add("Data Master Cashier");

                    foreach (var templateWorksheet in templatePackage.Workbook.Worksheets)
                    {

                        int sheetIndex = templateWorksheet.Index;
                        if (sheetIndex == 0)
                        {
                            for (int col = 1; col <= templateWorksheet.Dimension.End.Column; col++)
                            {
                                if (col == 2)
                                { // Specify the column indices you want to make read-only

                                    worksheetNonom1.Column(col).Style.Locked = true;
                                    worksheetNonom1.Cells[1, col].Value = templateWorksheet.Cells[1, col].Value;
                                    ExcelRange cellnotes = worksheetNonom1.Cells[1, col];
                                    cellnotes.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    cellnotes.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.ForestGreen);

                                }
                                else
                                {
                                    worksheetNonom1.Cells[1, col].Value = templateWorksheet.Cells[1, col].Value;
                                    ExcelRange cellnotes = worksheetNonom1.Cells[1, col];
                                    cellnotes.AddComment("Wajib diisi!", "KPISystem");
                                    cellnotes.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    cellnotes.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Red);
                                }

                            }
                        }



                    }

                    AddSheetWithData(excelPackage, sectionData, "Section");
                    for (int row = 0; row < dataKPI_Cash.Count; row++)
                    {
                        var properties = dataKPI_Cash[row].GetType().GetProperties();
                        for (int col = 0; col < properties.Length - 2; col++)
                        {
                            worksheetNonom1.Cells[row + 2, col + 1].Value = properties[col].GetValue(dataKPI_Cash[row]);

                        }
                    }

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        memoryStream.Seek(0, SeekOrigin.Begin);
                        excelPackage.SaveAs(memoryStream);

                        memoryStream.Position = 0;

                        File.WriteAllBytes(filePath, memoryStream.ToArray());
                    }

                }
            }
        }

        public void getAllDataKPI_DS()
        {
            DateTime dateTime = DateTime.Now;
            int Periode = dateTime.Month;
            int year = dateTime.Year;

            List<TBL_M_SECTION> sectionData = db.GetTable<TBL_M_SECTION>().ToList();
            List<TBL_M_KPI_DEPT_SECT> dataKPI_DS = db.GetTable<TBL_M_KPI_DEPT_SECT>().ToList();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            // Specify the path to the existing template file
            string fileName = "UpdateAllData_DeptSect.xlsx";
            string filePath = System.Web.Hosting.HostingEnvironment.MapPath("/Files/" + fileName);

            using (ExcelPackage templatePackage = new ExcelPackage(new FileInfo(filePath)))
            {
                using (ExcelPackage excelPackage = new ExcelPackage())
                {
                    ExcelWorksheet worksheetNonom1 = excelPackage.Workbook.Worksheets.Add("Data Master KPI Dept Sect");

                    foreach (var templateWorksheet in templatePackage.Workbook.Worksheets)
                    {

                        int sheetIndex = templateWorksheet.Index;
                        if (sheetIndex == 0)
                        {
                            for (int col = 1; col <= templateWorksheet.Dimension.End.Column; col++)
                            {
                                if (col == 1)
                                { // Specify the column indices you want to make read-only

                                    worksheetNonom1.Column(col).Style.Locked = true;
                                    worksheetNonom1.Cells[1, col].Value = templateWorksheet.Cells[1, col].Value;
                                    ExcelRange cellnotes = worksheetNonom1.Cells[1, col];
                                    cellnotes.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    cellnotes.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.ForestGreen);

                                }
                                else
                                {
                                    worksheetNonom1.Cells[1, col].Value = templateWorksheet.Cells[1, col].Value;
                                    ExcelRange cellnotes = worksheetNonom1.Cells[1, col];
                                    cellnotes.AddComment("Wajib diisi!", "KPISystem");
                                    cellnotes.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    cellnotes.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Red);
                                }

                            }
                        }



                    }

                    AddSheetWithData(excelPackage, sectionData, "Section");
                    for (int row = 0; row < dataKPI_DS.Count; row++)
                    {
                        var properties = dataKPI_DS[row].GetType().GetProperties();
                        for (int col = 0; col < properties.Length - 2; col++)
                        {
                            worksheetNonom1.Cells[row + 2, col + 1].Value = properties[col].GetValue(dataKPI_DS[row]);

                        }
                    }

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        memoryStream.Seek(0, SeekOrigin.Begin);
                        excelPackage.SaveAs(memoryStream);

                        memoryStream.Position = 0;

                        File.WriteAllBytes(filePath, memoryStream.ToArray());
                    }

                }
            }
        }

    }
}