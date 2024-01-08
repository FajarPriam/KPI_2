using KPI_API.Models;
using KPI_API.Repositories;
using KPI_API.Views_Models;
using KPIAPI.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Web;

namespace KPI_API.Services
{
    public class KPIService
    {
        private readonly ClsKPI _clsKPI = new ClsKPI();
        private readonly MappingKPIRepository _mappingKPIRepository = new MappingKPIRepository();
        private readonly MappingKPIONRepository _mappingKPIONRepository = new MappingKPIONRepository();

        public List<MappingKPIDSDto> GetTableMappingKPIDS(string ID_KPI_DS)
        {
            try
            {
                var mappingKPIDS = _clsKPI.c_getTabelMappingKPIDS(ID_KPI_DS);
                var kPICodes = _clsKPI.c_getListKPICode();
                var pICs = _clsKPI.c_getListPIC().Where(f => f.ID != 4);
                var results = mappingKPIDS.Select(item => new MappingKPIDSDto
                {
                    ID = item.ID,
                    KPI_DS_ID = item.KPI_DS_ID,
                    KPI_DS_DESC = item.KPI_DS_DESC,
                    KPI_CODE = item.KPI_CODE,
                    KPI_ITEM = item.KPI_ITEM,
                    BOBOT = item.BOBOT,
                    ID_PIC_OFFICER_NONOM = item.ID_PIC_OFFICER_NONOM,
                    INITIALS = item.INITIALS,
                    KPICodes = kPICodes,
                    PICS = pICs
                }).ToList();
                return results ?? new List<MappingKPIDSDto>();
            }
            catch
            {
                return new List<MappingKPIDSDto>();
            }
        }
        
        public List<MappingKPIONDto> GetTableMappingKPION(string ID_KPI_ON)
        {
            try
            {
                var mappingKPIDS = _clsKPI.c_getTabelMappingKPIONByKPI(ID_KPI_ON);
                var kPICodes = _clsKPI.c_getKPIONMappingChild(ID_KPI_ON);
                var pICs = _clsKPI.c_getListPIC().Where(f => f.ID == 4);
                var results = mappingKPIDS.Select(item => new MappingKPIONDto
                {
                    ID = item.ID,
                    ID_KPI_ON = item.ID_KPI_ON,
                    KPI_DESC = item.KPI_DESC,
                    KPI_CODE = item.KPI_CODE,
                    KPI_ITEM = item.KPI_ITEM,
                    BOBOT = item.BOBOT,
                    ID_PIC = item.ID_PIC,
                    INITIALS = item.INITIALS,
                    KPICodes = kPICodes,
                    PICS = pICs
                }).ToList();
                return results ?? new List<MappingKPIONDto>();
            }
            catch
            {
                return new List<MappingKPIONDto>();
            }
        }

        public MappingKPIDSDto CreateDOMTableMappingKPIDS(string ID_KPI_DS)
        {
            try
            {
                var kPICodes = _clsKPI.c_getListKPICode();
                var pICs = _clsKPI.c_getListPIC().Where(f => f.ID != 4);
                var result = new MappingKPIDSDto()
                {
                    ID = 0,
                    KPI_DS_ID = ID_KPI_DS,
                    KPI_DS_DESC = "",
                    KPI_CODE = "",
                    KPI_ITEM = "",
                    BOBOT = 0,
                    ID_PIC_OFFICER_NONOM = 0,
                    INITIALS = "",
                    KPICodes = kPICodes,
                    PICS = pICs
                };
                return result ?? new MappingKPIDSDto();
            }
            catch
            {
                return new MappingKPIDSDto();
            }
        }
        public MappingKPIONDto CreateDOMTableMappingKPION(string ID_KPI_ON)
        {
            try
            {
                var kPICodes = _clsKPI.c_getKPIONMappingChild(ID_KPI_ON);
                var pICs = _clsKPI.c_getListPIC().Where(f => f.ID == 4);
                var result = new MappingKPIONDto()
                {
                    ID = 0,
                    ID_KPI_ON = ID_KPI_ON,
                    KPI_DESC = "",
                    KPI_CODE = "",
                    KPI_ITEM = "",
                    BOBOT = 0,
                    ID_PIC = 0,
                    INITIALS = "",
                    KPICodes = kPICodes,
                    PICS = pICs
                };
                return result ?? new MappingKPIONDto();
            }
            catch
            {
                return new MappingKPIONDto();
            }
        }

        public int SubmitKPIDS(IEnumerable<TBL_M_MAPPING_KPI> dataMappingKPIDS)
        {
            try
            {
                //remove KPI that dont have in dataMappingKPIDS
                var existingDatas = _mappingKPIRepository.GetsMappingKPIByKPIDS(dataMappingKPIDS.FirstOrDefault().ID_KPI_DS);
                var deleteData = existingDatas.Where(f => !dataMappingKPIDS.Any(item => item.ID == f.ID)).ToList();
                foreach(var deleteItem in deleteData) {
                    _mappingKPIRepository.DeleteMappingKPI(deleteItem.ID);
                }

                foreach (var item in dataMappingKPIDS)
                {
                    //updatedata
                    if(item.ID != 0)
                    {
                        _mappingKPIRepository.UpdateMappingKPI(item);
                    }
                    //add data
                    else
                    {
                        _mappingKPIRepository.AddMappingKPI(item);
                    }
                }
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        
        public int SubmitKPION(IEnumerable<TBL_M_MAPPING_KPI_ON> dataMappingKPIDS)
        {
            try
            {
                //remove KPI that dont have in dataMappingKPIDS
                var existingDatas = _mappingKPIONRepository.GetsMappingKPIByKPION(dataMappingKPIDS.FirstOrDefault().ID_KPI_ON);
                var deleteData = existingDatas.Where(f => !dataMappingKPIDS.Any(item => item.ID == f.ID)).ToList();
                foreach(var deleteItem in deleteData) {
                    _mappingKPIONRepository.DeleteMappingKPI(deleteItem.ID);
                }

                foreach (var item in dataMappingKPIDS)
                {
                    //updatedata
                    if(item.ID != 0)
                    {
                        _mappingKPIONRepository.UpdateMappingKPI(item);
                    }
                    //add data
                    else
                    {
                        _mappingKPIONRepository.AddMappingKPI(item);
                    }
                }
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

    }
}