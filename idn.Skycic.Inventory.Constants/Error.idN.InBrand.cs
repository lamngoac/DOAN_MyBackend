using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;


namespace idn.Skycic.Inventory.Errors
{
    public partial class ErridnInventory
    {
		
        #region // Mst_PartType:
        // Mst_PartType_CheckDB:
        public const string Mst_PartType_CheckDB_PartTypeNotFound = "ErroriNOSInBrand.Mst_PartType_CheckDB_PartTypeNotFound"; //// //Mst_PartType_CheckDB_PartTypeNotFound
        public const string Mst_PartType_CheckDB_PartTypeExist = "ErroriNOSInBrand.Mst_PartType_CheckDB_PartTypeExist"; //// //Mst_PartType_CheckDB_PartTypeExist
        public const string Mst_PartType_CheckDB_FlagActiveNotMatched = "ErroriNOSInBrand.Mst_PartType_CheckDB_FlagActiveNotMatched"; //// //Mst_PartType_CheckDB_FlagActiveNotMatched

        // Mst_PartType_Get:
        public const string Mst_PartType_Get = "ErroriNOSInBrand.Mst_PartType_Get"; //// //Mst_PartType_Get

        // WAS_Mst_PartType_Get:
        public const string WAS_Mst_PartType_Get = "ErroriNOSInBrand.WAS_Mst_PartType_Get"; //// //WAS_Mst_PartType_Get


        // Mst_PartType_CreateX:
        public const string Mst_PartType_CreateX = "ErroriNOSInBrand.Mst_PartType_CreateX"; //// //Mst_PartType_CreateX
        public const string Mst_PartType_CreateX_InvalidPartType = "ErroriNOSInBrand.Mst_PartType_CreateX_InvalidPartType"; //// //Mst_PartType_CreateX_InvalidPartType
        public const string Mst_PartType_CreateX_InvalidPartTypeName = "ErroriNOSInBrand.Mst_PartType_CreateX_InvalidPartTypeName"; //// //Mst_PartType_CreateX_InvalidPartTypeName
        public const string WAS_Mst_PartType_Create = "ErroriNOSInBrand.WAS_Mst_PartType_Create"; //// //WAS_Mst_PartType_Create
        public const string Mst_PartType_Create = "ErroriNOSInBrand.Mst_PartType_Create"; //// //Mst_PartType_Create

        // Mst_PartType_UpdateX:
        public const string Mst_PartType_UpdateX = "ErroriNOSInBrand.Mst_PartType_UpdateX"; //// //Mst_PartType_UpdateX
        public const string Mst_PartType_Update = "ErroriNOSInBrand.Mst_PartType_Update"; //// //Mst_PartType_Update
        public const string WAS_Mst_PartType_Update = "ErroriNOSInBrand.WAS_Mst_PartType_Update"; //// //WAS_Mst_PartType_Update
        public const string Mst_PartType_UpdateX_InvalidPartTypeName = "ErroriNOSInBrand.Mst_PartType_UpdateX_InvalidPartTypeName"; //// //Mst_PartType_UpdateX_InvalidPartTypeName

        // Mst_PartType_DeleteX:
        public const string Mst_PartType_DeleteX = "ErroriNOSInBrand.Mst_PartType_DeleteX"; //// //Mst_PartType_DeleteX
        public const string Mst_PartType_Delete = "ErroriNOSInBrand.Mst_PartType_Delete"; //// //Mst_PartType_Delete
        public const string WAS_Mst_PartType_Delete = "ErroriNOSInBrand.WAS_Mst_PartType_Delete"; //// //WAS_Mst_PartType_Delete
        #endregion

        #region // Mst_PartMaterialType:
        // Mst_PartMaterialType_CheckDB:
        public const string Mst_PartMaterialType_CheckDB_PMTypeNotFound = "ErroriNOSInBrand.Mst_PartMaterialType_CheckDB_PMTypeNotFound"; //// //Mst_PartMaterialType_CheckDB_PMTypeNotFound
        public const string Mst_PartMaterialType_CheckDB_PMTypeExist = "ErroriNOSInBrand.Mst_PartMaterialType_CheckDB_PMTypeExist"; //// //Mst_PartMaterialType_CheckDB_PMTypeExist
        public const string Mst_PartMaterialType_CheckDB_FlagActiveNotMatched = "ErroriNOSInBrand.Mst_PartMaterialType_CheckDB_FlagActiveNotMatched"; //// //Mst_PartMaterialType_CheckDB_FlagActiveNotMatched

        // Mst_PartMaterialType_Get:
        public const string Mst_PartMaterialType_Get = "ErroriNOSInBrand.Mst_PartMaterialType_Get"; //// //Mst_PartMaterialType_Get

        // WAS_Mst_PartMaterialType_Get:
        public const string WAS_Mst_PartMaterialType_Get = "ErroriNOSInBrand.WAS_Mst_PartMaterialType_Get"; //// //WAS_Mst_PartMaterialType_Get

        // Mst_PartMaterialType_Create:
        public const string Mst_PartMaterialType_Create = "ErroriNOSInBrand.Mst_PartMaterialType_Create"; //// //Mst_PartMaterialType_Create

        // WAS_Mst_PartMaterialType_Create:
        public const string WAS_Mst_PartMaterialType_Create = "ErroriNOSInBrand.WAS_Mst_PartMaterialType_Create"; //// //WAS_Mst_PartMaterialType_Create
        public const string Mst_PartMaterialType_CreateX_InvalidPMType = "ErroriNOSInBrand.Mst_PartMaterialType_CreateX_InvalidPMType"; //// //Mst_PartMaterialType_CreateX_InvalidPMType
        public const string Mst_PartMaterialType_CreateX_InvalidPMTypeName = "ErroriNOSInBrand.Mst_PartMaterialType_CreateX_InvalidPMTypeName"; //// //Mst_PartMaterialType_CreateX_InvalidPMTypeName

        // WAS_Mst_PartMaterialType_Update:
        public const string Mst_PartMaterialType_Update = "ErroriNOSInBrand.Mst_PartMaterialType_Update"; //// //Mst_PartMaterialType_Update
        public const string WAS_Mst_PartMaterialType_Update = "ErroriNOSInBrand.WAS_Mst_PartMaterialType_Update"; //// //WAS_Mst_PartMaterialType_Update
        public const string Mst_PartMaterialType_Update_InvalidPMTypeName = "ErroriNOSInBrand.Mst_PartMaterialType_Update_InvalidPMTypeName"; //// //Mst_PartMaterialType_Update_InvalidPMTypeName

        // WAS_Mst_PartMaterialType_Delete:
        public const string WAS_Mst_PartMaterialType_Delete = "ErroriNOSInBrand.WAS_Mst_PartMaterialType_Delete"; //// //WAS_Mst_PartMaterialType_Delete
        public const string Mst_PartMaterialType_Delete = "ErroriNOSInBrand.Mst_PartMaterialType_Delete"; //// //Mst_PartMaterialType_Delete
        #endregion

        #region // Mst_BOMType:
        // Mst_BOMType_CheckDB:
        public const string Mst_BOMType_CheckDB_BOMTypeNotFound = "ErroriNOSInBrand.Mst_BOMType_CheckDB_BOMTypeNotFound"; //// //Mst_BOMType_CheckDB_BOMTypeNotFound
        public const string Mst_BOMType_CheckDB_BOMTypeExist = "ErroriNOSInBrand.Mst_BOMType_CheckDB_BOMTypeExist"; //// //Mst_BOMType_CheckDB_BOMTypeExist
        public const string Mst_BOMType_CheckDB_FlagActiveNotMatched = "ErroriNOSInBrand.Mst_BOMType_CheckDB_FlagActiveNotMatched"; //// //Mst_BOMType_CheckDB_FlagActiveNotMatched

        // Mst_BOMType_Get:
        public const string Mst_BOMType_Get = "ErroriNOSInBrand.Mst_BOMType_Get"; //// //Mst_BOMType_Get

        // WAS_Mst_BOMType_Get:
        public const string WAS_Mst_BOMType_Get = "ErroriNOSInBrand.WAS_Mst_BOMType_Get"; //// //WAS_Mst_BOMType_Get

        // Mst_BOMType_CreateX:
        public const string Mst_BOMType_Create = "ErroriNOSInBrand.Mst_BOMType_Create"; //// //Mst_BOMType_Create
        public const string Mst_BOMType_CreateX = "ErroriNOSInBrand.Mst_BOMType_CreateX"; //// //Mst_BOMType_CreateX
        public const string WAS_Mst_BOMType_Create = "ErroriNOSInBrand.WAS_Mst_BOMType_Create"; //// //WAS_Mst_BOMType_Create
        public const string Mst_BOMType_CreateX_InvalidBOMType = "ErroriNOSInBrand.Mst_BOMType_CreateX_InvalidBOMType"; //// //Mst_BOMType_CreateX_InvalidBOMType
        public const string Mst_BOMType_CreateX_InvalidBOMTypeDesc = "ErroriNOSInBrand.Mst_BOMType_CreateX_InvalidBOMTypeDesc"; //// //Mst_BOMType_CreateX_InvalidBOMTypeDesc

        // Mst_BOMType_UpdateX:
        public const string Mst_BOMType_UpdateX = "ErroriNOSInBrand.Mst_BOMType_UpdateX"; //// //Mst_BOMType_UpdateX
        public const string WAS_Mst_BOMType_Update = "ErroriNOSInBrand.WAS_Mst_BOMType_Update"; //// //WAS_Mst_BOMType_Update
        public const string Mst_BOMType_Update = "ErroriNOSInBrand.Mst_BOMType_Update"; //// //Mst_BOMType_Update
        public const string Mst_BOMType_UpdateX_InvalidBOMTypeDesc = "ErroriNOSInBrand.Mst_BOMType_UpdateX_InvalidBOMTypeDesc"; //// //Mst_BOMType_UpdateX_InvalidBOMTypeDesc

        // Mst_BOMType_Delete:
        public const string Mst_BOMType_Delete = "ErroriNOSInBrand.Mst_BOMType_Delete"; //// //Mst_BOMType_Delete
        public const string WAS_Mst_BOMType_Delete = "ErroriNOSInBrand.WAS_Mst_BOMType_Delete"; //// //WAS_Mst_BOMType_Delete
        #endregion

        #region // Mst_PartColor:
        // Mst_PartColor_CheckDB:
        public const string Mst_PartColor_CheckDB_PartColorCodeNotFound = "ErroriNOSInBrand.Mst_PartColor_CheckDB_PartColorCodeNotFound"; //// //Mst_PartColor_CheckDB_PartColorCodeNotFound
        public const string Mst_PartColor_CheckDB_PartColorCodeExist = "ErroriNOSInBrand.Mst_PartColor_CheckDB_PartColorCodeExist"; //// //Mst_PartColor_CheckDB_PartColorCodeExist
        public const string Mst_PartColor_CheckDB_FlagActiveNotMatched = "ErroriNOSInBrand.Mst_PartColor_CheckDB_FlagActiveNotMatched"; //// //Mst_PartColor_CheckDB_FlagActiveNotMatched

        // Mst_PartColor_Get:
        public const string Mst_PartColor_Get = "ErroriNOSInBrand.Mst_PartColor_Get"; //// //Mst_PartColor_Get

        // WAS_Mst_PartColor_Get:
        public const string WAS_Mst_PartColor_Get = "ErroriNOSInBrand.WAS_Mst_PartColor_Get"; //// //WAS_Mst_PartColor_Get

        // Mst_PartColor_Update:
        public const string Mst_PartColor_Update = "ErroriNOSInBrand.Mst_PartColor_Update"; //// //Mst_PartColor_Update
        public const string WAS_Mst_PartColor_Update = "ErroriNOSInBrand.WAS_Mst_PartColor_Update"; //// //WAS_Mst_PartColor_Update

        // Mst_PartColor_Create:
        public const string Mst_PartColor_Create = "ErroriNOSInBrand.Mst_PartColor_Create"; //// //Mst_PartColor_Create
        public const string Mst_PartColor_Create_InvalidPartColorCode = "ErroriNOSInBrand.Mst_PartColor_Create_InvalidPartColorCode"; //// //Mst_PartColor_Create_InvalidPartColorCode
        public const string Mst_PartColor_Create_InvalidPartColorName = "ErroriNOSInBrand.Mst_PartColor_Create_InvalidPartColorName"; //// //Mst_PartColor_Create_InvalidPartColorName
        public const string WAS_Mst_PartColor_Create = "ErroriNOSInBrand.WAS_Mst_PartColor_Create"; //// //WAS_Mst_PartColor_Create

        // Mst_PartColor_Delete:
        public const string Mst_PartColor_Delete = "ErroriNOSInBrand.Mst_PartColor_Delete"; //// //Mst_PartColor_Delete
        public const string WAS_Mst_PartColor_Delete = "ErroriNOSInBrand.WAS_Mst_PartColor_Delete"; //// //WAS_Mst_PartColor_Delete
        public const string Mst_PartColor_UpdateX_InvalidPartColorName = "ErroriNOSInBrand.Mst_PartColor_UpdateX_InvalidPartColorName"; //// //Mst_PartColor_UpdateX_InvalidPartColorName
        #endregion

        #region // Mst_PartUnit:
        // Mst_PartUnit_CheckDB:
        public const string Mst_PartUnit_CheckDB_PartUnitCodeNotFound = "ErroriNOSInBrand.Mst_PartUnit_CheckDB_PartUnitCodeNotFound"; //// //Mst_PartUnit_CheckDB_PartUnitCodeNotFound
        public const string Mst_PartUnit_CheckDB_PartUnitCodeExist = "ErroriNOSInBrand.Mst_PartUnit_CheckDB_PartUnitCodeExist"; //// //Mst_PartUnit_CheckDB_PartUnitCodeExist
        public const string Mst_PartUnit_CheckDB_FlagActiveNotMatched = "ErroriNOSInBrand.Mst_PartUnit_CheckDB_FlagActiveNotMatched"; //// //Mst_PartUnit_CheckDB_FlagActiveNotMatched
        public const string Mst_PartUnit_CheckDB_FlagUnitStdNotMatched = "ErroriNOSInBrand.Mst_PartUnit_CheckDB_FlagUnitStdNotMatched"; //// //Mst_PartUnit_CheckDB_FlagUnitStdNotMatched

        // Mst_PartUnit_Get:
        public const string Mst_PartUnit_Get = "ErroriNOSInBrand.Mst_PartUnit_Get"; //// //Mst_PartUnit_Get

        // WAS_Mst_PartUnit_Get:
        public const string WAS_Mst_PartUnit_Get = "ErroriNOSInBrand.WAS_Mst_PartUnit_Get"; //// //WAS_Mst_PartUnit_Get

        // Mst_PartUnit_Create:
        public const string Mst_PartUnit_Create = "ErroriNOSInBrand.Mst_PartUnit_Create"; //// //Mst_PartUnit_Create

        // WAS_Mst_PartUnit_Create:
        public const string WAS_Mst_PartUnit_Create = "ErroriNOSInBrand.WAS_Mst_PartUnit_Create"; //// //WAS_Mst_PartUnit_Create

        // Mst_PartUnit_Create:
        public const string Mst_PartUnit_CreateX_InvalidPartUnitCode = "ErroriNOSInBrand.Mst_PartUnit_CreateX_InvalidPartUnitCode"; //// //Mst_PartUnit_CreateX_InvalidPartUnitCode
        public const string Mst_PartUnit_CreateX_InvalidPartUnitName = "ErroriNOSInBrand.Mst_PartUnit_CreateX_InvalidPartUnitName"; //// //Mst_PartUnit_CreateX_InvalidPartUnitName

        // WAS_Mst_PartUnit_Update:
        public const string WAS_Mst_PartUnit_Update = "ErroriNOSInBrand.WAS_Mst_PartUnit_Update"; //// //WAS_Mst_PartUnit_Update

        // Mst_PartUnit_Update:
        public const string Mst_PartUnit_Update = "ErroriNOSInBrand.Mst_PartUnit_Update"; //// //Mst_PartUnit_Update
        public const string Mst_PartUnit_UpdateX_InvalidPartUnitName = "ErroriNOSInBrand.Mst_PartUnit_UpdateX_InvalidPartUnitName"; //// //Mst_PartUnit_UpdateX_InvalidPartUnitName

        // WAS_Mst_PartUnit_Delete:
        public const string WAS_Mst_PartUnit_Delete = "ErroriNOSInBrand.WAS_Mst_PartUnit_Delete"; //// //WAS_Mst_PartUnit_Delete
        public const string Mst_PartUnit_Delete = "ErroriNOSInBrand.Mst_PartUnit_Delete"; //// //Mst_PartUnit_Delete
        #endregion

        #region // Mst_Part:
        // Mst_Part_CheckDB:
        public const string Mst_Part_CheckDB_PartNotFound = "ErroriNOSInBrand.Mst_Part_CheckDB_PartNotFound"; //// //Mst_Part_CheckDB_PartNotFound
        public const string Mst_Part_CheckDB_PartExist = "ErroriNOSInBrand.Mst_Part_CheckDB_PartExist"; //// //Mst_Part_CheckDB_PartExist
        public const string Mst_Part_CheckDB_FlagActiveNotMatched = "ErroriNOSInBrand.Mst_Part_CheckDB_FlagActiveNotMatched"; //// //Mst_Part_CheckDB_FlagActiveNotMatched

        // Mst_Part_Get:
        public const string Mst_Part_Get = "ErroriNOSInBrand.Mst_Part_Get"; //// //Mst_Part_Get

        // WAS_Mst_Part_Get:
        public const string WAS_Mst_Part_Get = "ErroriNOSInBrand.WAS_Mst_Part_Get"; //// //WAS_Mst_Part_Get

        // Mst_Part_AddMulti:
        public const string Mst_Part_AddMulti = "ErroriNOSInBrand.Mst_Part_AddMulti"; //// //Mst_Part_AddMulti
        public const string Mst_Part_AddMulti_InputPartTblNotFound = "ErroriNOSInBrand.Mst_Part_AddMulti_InputPartTblNotFound"; //// //Mst_Part_AddMulti_InputPartTblNotFound
        public const string Mst_Part_AddMulti_InputPartTblInvalid = "ErroriNOSInBrand.Mst_Part_AddMulti_InputPartTblInvalid"; //// //Mst_Part_AddMulti_InputPartTblInvalid
        public const string Mst_Part_AddMulti_InputPartTbl_InvalidPartCode = "ErroriNOSInBrand.Mst_Part_AddMulti_InputPartTbl_InvalidPartCode"; //// //Mst_Part_AddMulti_InputPartTbl_InvalidPartCode
        public const string Mst_Part_AddMulti_InputPartTbl_InvalidPartName = "ErroriNOSInBrand.Mst_Part_AddMulti_InputPartTbl_InvalidPartName"; //// //Mst_Part_AddMulti_InputPartTbl_InvalidPartName
        public const string Mst_Part_AddMulti_InputPartTbl_InvalidPartUnitCodeStd = "ErroriNOSInBrand.Mst_Part_AddMulti_InputPartTbl_InvalidPartUnitCodeStd"; //// //Mst_Part_AddMulti_InputPartTbl_InvalidPartUnitCodeStd
        public const string Mst_Part_AddMulti_InputPartTbl_InvalidQty = "ErroriNOSInBrand.Mst_Part_AddMulti_InputPartTbl_InvalidQty"; //// //Mst_Part_AddMulti_InputPartTbl_InvalidQty
        public const string Mst_Part_AddMulti_InputPartTbl_InvalidActiveLotAndSerialAtOnce = "ErroriNOSInBrand.Mst_Part_AddMulti_InputPartTbl_InvalidActiveLotAndSerialAtOnce"; //// //Mst_Part_AddMulti_InputPartTbl_InvalidActiveLotAndSerialAtOnce
        public const string Mst_Part_AddMulti_InvalidActiveLotAndSerialAtOnce = "ErroriNOSInBrand.Mst_Part_AddMulti_InvalidActiveLotAndSerialAtOnce"; //// //Mst_Part_AddMulti_InvalidActiveLotAndSerialAtOnce

        // Mst_Part_Update:
        public const string Mst_Part_Update = "ErroriNOSInBrand.Mst_Part_Update"; //// //Mst_Part_Update
        public const string Mst_Part_Update_InvalidPartName = "ErroriNOSInBrand.Mst_Part_Update_InvalidPartName"; //// //Mst_Part_Update_InvalidPartName
        public const string Mst_Part_Update_InvalidQty = "ErroriNOSInBrand.Mst_Part_Update_InvalidQty"; //// //Mst_Part_Update_InvalidQty
        public const string Mst_Part_Update_InvalidActiveLotAndSerialAtOnce = "ErroriNOSInBrand.Mst_Part_Update_InvalidActiveLotAndSerialAtOnce"; //// //Mst_Part_Update_InvalidActiveLotAndSerialAtOnce

        // Mst_Part_Remove:
        public const string Mst_Part_Remove = "ErroriNOSInBrand.Mst_Part_Remove"; //// //Mst_Part_Remove

        // Mst_Part_CheckDBExistPartBarCode:
        public const string Mst_Part_CheckDBExistPartBarCode_InvalidExistPartBarCode = "ErroriNOSInBrand.Mst_Part_CheckDBExistPartBarCode_InvalidExistPartBarCode"; //// //Mst_Part_CheckDBExistPartBarCode_InvalidExistPartBarCode

        // Mst_Part_CheckDBLotORSerialActive:
        public const string Mst_Part_CheckDBLotORSerialActive_InvalidActiveLotAndSerialAtOnce = "ErroriNOSInBrand.Mst_Part_CheckDBLotORSerialActive_InvalidActiveLotAndSerialAtOnce"; //// //Mst_Part_CheckDBLotORSerialActive_InvalidActiveLotAndSerialAtOnce

        // Mst_Part_CheckDBQty:
        public const string Mst_Part_CheckDBQty_InvalidQty = "ErroriNOSInBrand.Mst_Part_CheckDBQty_InvalidQty"; //// //Mst_Part_CheckDBQty_InvalidQty

        // Mst_Part_Create:
        public const string Mst_Part_Create = "ErroriNOSInBrand.Mst_Part_Create"; //// //Mst_Part_Create

        // WAS_Mst_Part_Create:
        public const string WAS_Mst_Part_Create = "ErroriNOSInBrand.WAS_Mst_Part_Create"; //// //WAS_Mst_Part_Create

        public const string Mst_Part_CreateX_InvalidPartCode = "ErroriNOSInBrand.Mst_Part_CreateX_InvalidPartCode"; //// //Mst_Part_CreateX_InvalidPartCode
        public const string Mst_Part_CreateX_InvalidPartName = "ErroriNOSInBrand.Mst_Part_CreateX_InvalidPartName"; //// //Mst_Part_CreateX_InvalidPartName
        public const string Mst_Part_CreateX_InvalidPartNameFS = "ErroriNOSInBrand.Mst_Part_CreateX_InvalidPartNameFS"; //// //Mst_Part_CreateX_InvalidPartNameFS
        public const string Mst_Part_CreateX_InvalidQtyMaxSt = "ErroriNOSInBrand.Mst_Part_CreateX_InvalidQtyMaxSt"; //// //Mst_Part_CreateX_InvalidQtyMaxSt
        public const string Mst_Part_CreateX_InvalidQtyMinSt = "ErroriNOSInBrand.Mst_Part_CreateX_InvalidQtyMinSt"; //// //Mst_Part_CreateX_InvalidQtyMinSt
        public const string Mst_Part_CreateX_InvalidQtyEffSt = "ErroriNOSInBrand.Mst_Part_CreateX_InvalidQtyEffSt"; //// //Mst_Part_CreateX_InvalidQtyEffSt

        // Mst_Part_Delete:
        public const string Mst_Part_Delete = "ErroriNOSInBrand.Mst_Part_Delete"; //// //Mst_Part_Delete

        // WAS_Mst_Part_Delete:
        public const string WAS_Mst_Part_Delete = "ErroriNOSInBrand.WAS_Mst_Part_Delete"; //// //WAS_Mst_Part_Delete

        // WAS_Mst_Part_Update:
        public const string WAS_Mst_Part_Update = "ErroriNOSInBrand.WAS_Mst_Part_Update"; //// //WAS_Mst_Part_Update
        #endregion

        #region // Temp_PrintTemp:
        // Temp_PrintTemp_CheckDB:
        public const string Temp_PrintTemp_CheckDB_PrintTempNotFound = "ErroriNOSInBrand.Temp_PrintTemp_CheckDB_PrintTempNotFound"; //// //Temp_PrintTemp_CheckDB_PrintTempNotFound
        public const string Temp_PrintTemp_CheckDB_PrintTempExist = "ErroriNOSInBrand.Temp_PrintTemp_CheckDB_PrintTempExist"; //// //Temp_PrintTemp_CheckDB_PrintTempExist
        public const string Temp_PrintTemp_CheckDB_PrintTempStatusNotMatched = "ErroriNOSInBrand.Temp_PrintTemp_CheckDB_PrintTempStatusNotMatched"; //// //Temp_PrintTemp_CheckDB_PrintTempStatusNotMatched

        // Temp_PrintTemp_Get:
        public const string Temp_PrintTemp_Get = "ErroriNOSInBrand.Temp_PrintTemp_Get"; //// //Temp_PrintTemp_Get

        // WAS_Temp_PrintTemp_Get:
        public const string WAS_Temp_PrintTemp_Get = "ErroriNOSInBrand.WAS_Temp_PrintTemp_Get"; //// //WAS_Temp_PrintTemp_Get

        // Temp_PrintTemp_SaveX:
        public const string Temp_PrintTemp_Save = "ErroriNOSInBrand.Temp_PrintTemp_Save"; //// //Temp_PrintTemp_Save
        public const string Temp_PrintTemp_SaveX_InvalidPrintTempCode = "ErroriNOSInBrand.Temp_PrintTemp_SaveX_InvalidPrintTempCode"; //// //Temp_PrintTemp_SaveX_InvalidPrintTempCode
        public const string Temp_PrintTemp_SaveX_StatusNotMatched = "ErroriNOSInBrand.Temp_PrintTemp_SaveX_StatusNotMatched"; //// //Temp_PrintTemp_SaveX_StatusNotMatched 

        // WAS_Temp_PrintTemp_Save:
        public const string WAS_Temp_PrintTemp_Save = "ErroriNOSInBrand.WAS_Temp_PrintTemp_Save"; //// //WAS_Temp_PrintTemp_Save 

        // Temp_PrintTemp_Appr:
        public const string Temp_PrintTemp_Approved = "ErroriNOSInBrand.Temp_PrintTemp_Appr"; //// //Temp_PrintTemp_Appr 

        // WAS_Temp_PrintTemp_Appr:
        public const string WAS_Temp_PrintTemp_Approved = "ErroriNOSInBrand.WAS_Temp_PrintTemp_Approved"; //// //WAS_Temp_PrintTemp_Approved 

        // Temp_PrintTemp_Cacncel:
        public const string Temp_PrintTemp_Cancel = "ErroriNOSInBrand.Temp_PrintTemp_Cacncel"; //// //Temp_PrintTemp_Cacncel 

        // WAS_Temp_PrintTemp_Cancel:
        public const string WAS_Temp_PrintTemp_Cancel = "ErroriNOSInBrand.WAS_Temp_PrintTemp_Cancel"; //// //WAS_Temp_PrintTemp_Cancel 
        #endregion

        #region // Inv_GenTimes:
        // Inv_GenTimes_CheckDB:
        public const string Inv_GenTimes_CheckDB_GenTimesNoNotFound = "ErroriNOSInBrand.Inv_GenTimes_CheckDB_GenTimesNoNotFound"; //// //Inv_GenTimes_CheckDB_GenTimesNoNotFound
        public const string Inv_GenTimes_CheckDB_GenTimesNoExist = "ErroriNOSInBrand.Inv_GenTimes_CheckDB_GenTimesNoExist"; //// //Inv_GenTimes_CheckDB_GenTimesNoExist
        public const string Inv_GenTimes_CheckDB_PrintTempStatusNotMatched = "ErroriNOSInBrand.Inv_GenTimes_CheckDB_PrintTempStatusNotMatched"; //// //Inv_GenTimes_CheckDB_PrintTempStatusNotMatched

        // Inv_GenTimes_Get:
        public const string Inv_GenTimes_Get = "ErroriNOSInBrand.Inv_GenTimes_Get"; //// //Inv_GenTimes_Get

        // WAS_Inv_GenTimes_Get:
        public const string WAS_Inv_GenTimes_Get = "ErroriNOSInBrand.WAS_Inv_GenTimes_Get"; //// //WAS_Inv_GenTimes_Get

        // Inv_GenTimes_Add:
        public const string Inv_GenTimes_AddX = "ErroriNOSInBrand.Inv_GenTimes_AddX"; //// //Inv_GenTimes_AddX
        public const string Inv_GenTimes_AddX_InvalidGenTimesNo = "ErroriNOSInBrand.Inv_GenTimes_AddX_InvalidGenTimesNo"; //// //Inv_GenTimes_AddX_InvalidGenTimesNo
        public const string Inv_GenTimes_AddX_InvalidQty = "ErroriNOSInBrand.Inv_GenTimes_AddX_InvalidQty"; //// //Inv_GenTimes_AddX_InvalidQty

        // Inv_GenTimes_Add:
        public const string Inv_GenTimes_Add = "ErroriNOSInBrand.Inv_GenTimes_Add"; //// //Inv_GenTimes_Add
        public const string Inv_GenTimes_Add_InvalidConfigName = "ErroriNOSInBrand.Inv_GenTimes_Add_InvalidConfigName"; //// //Inv_GenTimes_Add_InvalidConfigName

        // WAS_Inv_GenTimes_Add:
        public const string WAS_Inv_GenTimes_Add = "ErroriNOSInBrand.WAS_Inv_GenTimes_Add"; //// //WAS_Inv_GenTimes_Add
        #endregion

        #region // Inv_GenTimesBox:
        // Inv_GenTimesBox_CheckDB:
        public const string Inv_GenTimesBox_CheckDB_GenTimesBoxNoNotFound = "ErroriNOSInBrand.Inv_GenTimesBox_CheckDB_GenTimesBoxNoNotFound"; //// //Inv_GenTimesBox_CheckDB_GenTimesBoxNoNotFound
        public const string Inv_GenTimesBox_CheckDB_GenTimesBoxNoExist = "ErroriNOSInBrand.Inv_GenTimesBox_CheckDB_GenTimesBoxNoExist"; //// //Inv_GenTimesBox_CheckDB_GenTimesBoxNoExist
        public const string Inv_GenTimesBox_CheckDB_GenTimesBoxNoStatusNotMatched = "ErroriNOSInBrand.Inv_GenTimesBox_CheckDB_GenTimesBoxNoStatusNotMatched"; //// //Inv_GenTimesBox_CheckDB_GenTimesBoxNoStatusNotMatched

        // Inv_GenTimesBox_Get:
        public const string Inv_GenTimesBox_Get = "ErroriNOSInBrand.Inv_GenTimesBox_Get"; //// //Inv_GenTimesBox_Get

        // WAS_Inv_GenTimesBox_Get:
        public const string WAS_Inv_GenTimesBox_Get = "ErroriNOSInBrand.WAS_Inv_GenTimesBox_Get"; //// //WAS_Inv_GenTimesBox_Get

        // Inv_GenTimesBox_Add:
        public const string Inv_GenTimesBox_AddX = "ErroriNOSInBrand.Inv_GenTimesBox_AddX"; //// //Inv_GenTimesBox_AddX
        public const string Inv_GenTimesBox_AddX_InvalidGenTimesBoxNo = "ErroriNOSInBrand.Inv_GenTimesBox_AddX_InvalidGenTimesBoxNo"; //// //Inv_GenTimesBox_AddX_InvalidGenTimesBoxNo
        public const string Inv_GenTimesBox_AddX_InvalidQty = "ErroriNOSInBrand.Inv_GenTimesBox_AddX_InvalidQty"; //// //Inv_GenTimesBox_AddX_InvalidQty

        // Inv_GenTimesBox_Add:
        public const string Inv_GenTimesBox_Add = "ErroriNOSInBrand.Inv_GenTimesBox_Add"; //// //Inv_GenTimesBox_Add
        public const string Inv_GenTimesBox_Add_InvalidConfigName = "ErroriNOSInBrand.Inv_GenTimesBox_Add_InvalidConfigName"; //// //Inv_GenTimesBox_Add_InvalidConfigName

        // WAS_Inv_GenTimesBox_Add:
        public const string WAS_Inv_GenTimesBox_Add = "ErroriNOSInBrand.WAS_Inv_GenTimesBox_Add"; //// //WAS_Inv_GenTimesBox_Add
        #endregion

        #region // Inv_GenTimesCarton:
        // Inv_GenTimesCarton_CheckDB:
        public const string Inv_GenTimesCarton_CheckDB_GenTimesCartonNoNotFound = "ErroriNOSInBrand.Inv_GenTimesCarton_CheckDB_GenTimesCartonNoNotFound"; //// //Inv_GenTimesCarton_CheckDB_GenTimesCartonNoNotFound
        public const string Inv_GenTimesCarton_CheckDB_GenTimesCartonNoExist = "ErroriNOSInBrand.Inv_GenTimesCarton_CheckDB_GenTimesCartonNoExist"; //// //Inv_GenTimesCarton_CheckDB_GenTimesCartonNoExist
        public const string Inv_GenTimesCarton_CheckDB_GenTimesCartonNoStatusNotMatched = "ErroriNOSInBrand.Inv_GenTimesCarton_CheckDB_GenTimesCartonNoStatusNotMatched"; //// //Inv_GenTimesCarton_CheckDB_GenTimesCartonNoStatusNotMatched

        // Inv_GenTimesCarton_Get:
        public const string Inv_GenTimesCarton_Get = "ErroriNOSInBrand.Inv_GenTimesCarton_Get"; //// //Inv_GenTimesCarton_Get

        // WAS_Inv_GenTimesCarton_Get:
        public const string WAS_Inv_GenTimesCarton_Get = "ErroriNOSInBrand.WAS_Inv_GenTimesCarton_Get"; //// //WAS_Inv_GenTimesCarton_Get

        // Inv_GenTimesCarton_Add:
        public const string Inv_GenTimesCarton_AddX = "ErroriNOSInBrand.Inv_GenTimesCarton_AddX"; //// //Inv_GenTimesCarton_AddX
        public const string Inv_GenTimesCarton_AddX_InvalidGenTimesCartonNo = "ErroriNOSInBrand.Inv_GenTimesCarton_AddX_InvalidGenTimesCartonNo"; //// //Inv_GenTimesCarton_AddX_InvalidGenTimesCartonNo
        public const string Inv_GenTimesCarton_AddX_InvalidQty = "ErroriNOSInBrand.Inv_GenTimesCarton_AddX_InvalidQty"; //// //Inv_GenTimesCarton_AddX_InvalidQty

        // Inv_GenTimesCarton_Add:
        public const string Inv_GenTimesCarton_Add = "ErroriNOSInBrand.Inv_GenTimesCarton_Add"; //// //Inv_GenTimesCarton_Add
        public const string Inv_GenTimesCarton_Add_InvalidConfigName = "ErroriNOSInBrand.Inv_GenTimesCarton_Add_InvalidConfigName"; //// //Inv_GenTimesCarton_Add_InvalidConfigName

        // WAS_Inv_GenTimesCarton_Add:
        public const string WAS_Inv_GenTimesCarton_Add = "ErroriNOSInBrand.WAS_Inv_GenTimesCarton_Add"; //// //WAS_Inv_GenTimesCarton_Add
        #endregion

        #region // Inv_InventoryBox:
        // Inv_InventoryBox_Get:
        public const string Inv_InventoryBox_Get = "ErroriNOSInBrand.Inv_InventoryBox_Get"; //// //Inv_InventoryBox_Get

        // WAS_Inv_InventoryBox_Get:
        public const string WAS_Inv_InventoryBox_Get = "ErroriNOSInBrand.WAS_Inv_InventoryBox_Get"; //// //WAS_Inv_InventoryBox_Get

        // Inv_InventoryBox_UpdateFlagUsed:
        public const string Inv_InventoryBox_UpdateFlagUsed = "ErroriNOSInBrand.Inv_InventoryBox_UpdateFlagUsed"; //// //Inv_InventoryBox_UpdateFlagUsed
        public const string Inv_InventoryBox_UpdateFlagUsed_Inv_InventoryBoxNotFound = "ErroriNOSInBrand.Inv_InventoryBox_UpdateFlagUsed_Inv_InventoryBoxNotFound"; //// //Inv_InventoryBox_UpdateFlagUsed_Inv_InventoryBoxNotFound
        public const string Inv_InventoryBox_UpdateFlagUsed_Inv_InventoryBoxInvalid = "ErroriNOSInBrand.Inv_InventoryBox_UpdateFlagUsed_Inv_InventoryBoxInvalid"; //// //Inv_InventoryBox_UpdateFlagUsed_Inv_InventoryBoxInvalid
        public const string Inv_InventoryBox_UpdateFlagUsed_InvalidSerial = "ErroriNOSInBrand.Inv_InventoryBox_UpdateFlagUsed_InvalidSerial"; //// //Inv_InventoryBox_UpdateFlagUsed_InvalidSerial
        public const string Inv_InventoryBox_UpdateFlagUsed_InvalidFlagUsed = "ErroriNOSInBrand.Inv_InventoryBox_UpdateFlagUsed_InvalidFlagUsed"; //// //Inv_InventoryBox_UpdateFlagUsed_InvalidFlagUsed
        public const string Inv_InventoryBox_UpdateFlagUsed_InvalidMST = "ErroriNOSInBrand.Inv_InventoryBox_UpdateFlagUsed_InvalidMST"; //// //Inv_InventoryBox_UpdateFlagUsed_InvalidMST

        // WAS_Inv_InventoryBox_UpdateFlagUsed:
        public const string WAS_Inv_InventoryBox_UpdateFlagUsed = "ErroriNOSInBrand.WAS_Inv_InventoryBox_UpdateFlagUsed"; //// //WAS_Inv_InventoryBox_UpdateFlagUsed
        #endregion

        #region // Inv_InventoryCarton:
        // Inv_InventoryCarton_Get:
        public const string Inv_InventoryCarton_Get = "ErroriNOSInBrand.Inv_InventoryCarton_Get"; //// //Inv_InventoryCarton_Get

        // WAS_Inv_InventoryCarton_Get:
        public const string WAS_Inv_InventoryCarton_Get = "ErroriNOSInBrand.WAS_Inv_InventoryCarton_Get"; //// //WAS_Inv_InventoryCarton_Get

        // Inv_InventoryCarton_UpdateFlagUsed:
        public const string Inv_InventoryCarton_UpdateFlagUsed = "ErroriNOSInBrand.Inv_InventoryCarton_UpdateFlagUsed"; //// //Inv_InventoryCarton_UpdateFlagUsed
        public const string Inv_InventoryCarton_UpdateFlagUsed_Inv_InventoryCartonNotFound = "ErroriNOSInBrand.Inv_InventoryCarton_UpdateFlagUsed_Inv_InventoryCartonNotFound"; //// //Inv_InventoryCarton_UpdateFlagUsed_Inv_InventoryCartonNotFound
        public const string Inv_InventoryCarton_UpdateFlagUsed_Inv_InventoryCartonInvalid = "ErroriNOSInBrand.Inv_InventoryCarton_UpdateFlagUsed_Inv_InventoryCartonInvalid"; //// //Inv_InventoryCarton_UpdateFlagUsed_Inv_InventoryCartonInvalid
        public const string Inv_InventoryCarton_UpdateFlagUsed_InvalidSerial = "ErroriNOSInBrand.Inv_InventoryCarton_UpdateFlagUsed_InvalidSerial"; //// //Inv_InventoryCarton_UpdateFlagUsed_InvalidSerial
        public const string Inv_InventoryCarton_UpdateFlagUsed_InvalidFlagUsed = "ErroriNOSInBrand.Inv_InventoryCarton_UpdateFlagUsed_InvalidFlagUsed"; //// //Inv_InventoryCarton_UpdateFlagUsed_InvalidFlagUsed
        public const string Inv_InventoryCarton_UpdateFlagUsed_InvalidMST = "ErroriNOSInBrand.Inv_InventoryCarton_UpdateFlagUsed_InvalidMST"; //// //Inv_InventoryCarton_UpdateFlagUsed_InvalidMST

        // WAS_Inv_InventoryCarton_UpdateFlagUsed:
        public const string WAS_Inv_InventoryCarton_UpdateFlagUsed = "ErroriNOSInBrand.WAS_Inv_InventoryCarton_UpdateFlagUsed"; //// //WAS_Inv_InventoryCarton_UpdateFlagUsed
        #endregion

        #region // Inv_InventorySecret:
        // Inv_InventorySecret_Get:
        public const string Inv_InventorySecret_Get = "ErroriNOSInBrand.Inv_InventorySecret_Get"; //// //Inv_InventorySecret_Get

        // WAS_Inv_InventorySecret_Get:
        public const string WAS_Inv_InventorySecret_Get = "ErroriNOSInBrand.WAS_Inv_InventorySecret_Get"; //// //WAS_Inv_InventorySecret_Get

        // Inv_InventorySecret_UpdateFlagUsed:
        public const string Inv_InventorySecret_UpdateFlagUsed = "ErroriNOSInBrand.Inv_InventorySecret_UpdateFlagUsed"; //// //Inv_InventorySecret_UpdateFlagUsed
        public const string Inv_InventorySecret_UpdateFlagUsed_Inv_InventorySecretNotFound = "ErroriNOSInBrand.Inv_InventorySecret_UpdateFlagUsed_Inv_InventorySecretNotFound"; //// //Inv_InventorySecret_UpdateFlagUsed_Inv_InventorySecretNotFound
        public const string Inv_InventorySecret_UpdateFlagUsed_Inv_InventorySecretInvalid = "ErroriNOSInBrand.Inv_InventorySecret_UpdateFlagUsed_Inv_InventorySecretInvalid"; //// //Inv_InventorySecret_UpdateFlagUsed_Inv_InventorySecretInvalid
        public const string Inv_InventorySecret_UpdateFlagUsed_InvalidSerial = "ErroriNOSInBrand.Inv_InventorySecret_UpdateFlagUsed_InvalidSerial"; //// //Inv_InventorySecret_UpdateFlagUsed_InvalidSerial
        public const string Inv_InventorySecret_UpdateFlagUsed_InvalidFlagUsed = "ErroriNOSInBrand.Inv_InventorySecret_UpdateFlagUsed_InvalidFlagUsed"; //// //Inv_InventorySecret_UpdateFlagUsed_InvalidFlagUsed
        public const string Inv_InventorySecret_UpdateFlagUsed_InvalidMST = "ErroriNOSInBrand.Inv_InventorySecret_UpdateFlagUsed_InvalidMST"; //// //Inv_InventorySecret_UpdateFlagUsed_InvalidMST

        // WAS_Inv_InventorySecret_UpdateFlagUsed:
        public const string WAS_Inv_InventorySecret_UpdateFlagUsed = "ErroriNOSInBrand.WAS_Inv_InventorySecret_UpdateFlagUsed"; //// //WAS_Inv_InventorySecret_UpdateFlagUsed
        #endregion

        #region // Inv_InventoryTransaction:
        // Inv_InventoryTransaction_Perform_QtyAllZero:
        public const string Inv_InventoryTransaction_Perform_QtyAllZero = "ErroriNOSInBrand.Inv_InventoryTransaction_Perform_QtyAllZero"; //// //Inv_InventoryTransaction_Perform_QtyAllZero
        public const string Inv_InventoryTransaction_Perform_InvalidQtyProductLot = "ErroriNOSInBrand.Inv_InventoryTransaction_Perform_InvalidQtyProductLot"; //// //Inv_InventoryTransaction_Perform_InvalidQtyProductLot
        public const string Inv_InventoryTransaction_Perform_QtyChangeOverThreshold = "ErroriNOSInBrand.Inv_InventoryTransaction_Perform_QtyChangeOverThreshold"; //// //Inv_InventoryTransaction_Perform_QtyChangeOverThreshold
        public const string Inv_InventoryTransaction_Perform_InvalidQtyPartLot = "ErroriNOSInBrand.Inv_InventoryTransaction_Perform_InvalidQtyPartLot"; //// //Inv_InventoryTransaction_Perform_InvalidQtyPartLot
        public const string Inv_InventoryTransaction_Perform_InvalidQtyProduct = "ErroriNOSInBrand.Inv_InventoryTransaction_Perform_InvalidQtyProduct"; //// //Inv_InventoryTransaction_Perform_InvalidQtyProduct
        #endregion

        #region // InvF_InventoryInFG:
        // InvF_InventoryInFG_CheckDB:
        public const string InvF_InventoryInFG_CheckDB_InvInFGNoNotFound = "ErroriNOSInBrand.InvF_InventoryInFG_CheckDB_InvInFGNoNotFound"; //// //InvF_InventoryInFG_CheckDB_InvInFGNoNotFound
        public const string InvF_InventoryInFG_CheckDB_InvInFGNoExist = "ErroriNOSInBrand.InvF_InventoryInFG_CheckDB_InvInFGNoExist"; //// //InvF_InventoryInFG_CheckDB_InvInFGNoExist
        public const string InvF_InventoryInFG_CheckDB_StatusNotMatched = "ErroriNOSInBrand.InvF_InventoryInFG_CheckDB_StatusNotMatched"; //// //InvF_InventoryInFG_CheckDB_StatusNotMatched

        // InvF_InventoryInFG_Get:
        public const string InvF_InventoryInFG_Get = "ErroriNOSInBrand.InvF_InventoryInFG_Get"; //// //InvF_InventoryInFG_Get

        // WAS_InvF_InventoryInFG_Get:
        public const string WAS_InvF_InventoryInFG_Get = "ErroriNOSInBrand.WAS_InvF_InventoryInFG_Get"; //// //WAS_InvF_InventoryInFG_Get

        // InvF_InventoryInFG_Save:
        public const string InvF_InventoryInFG_Save_InvalidIF_InvInFGNo = "ErroriNOSInBrand.InvF_InventoryInFG_Save_InvalidIF_InvInFGNo"; //// //InvF_InventoryInFG_Save_InvalidIF_InvInFGNo

        public const string InvF_InventoryInFG_Save_InvalidMST = "ErroriNOSInBrand.InvF_InventoryInFG_Save_InvalidMST"; //// //InvF_InventoryInFG_Save_InvalidMST

        public const string InvF_InventoryInFG_Save = "ErroriNOSInBrand.InvF_InventoryInFG_Save"; //// //InvF_InventoryInFG_Save
        public const string InvF_InventoryInFG_Save_InvalidStatus = "ErroriNOSInBrand.InvF_InventoryInFG_Save_InvalidStatus"; //// //InvF_InventoryInFG_Save_InvalidStatus
        public const string InvF_InventoryInFG_Save_InvalidPartUnitCode = "ErroriNOSInBrand.InvF_InventoryInFG_Save_InvalidPartUnitCode"; //// //InvF_InventoryInFG_Save_InvalidPartUnitCode

        public const string InvF_InventoryInFG_Save_InvFInventoryInFGDtlTblNotFound = "ErroriNOSInBrand.InvF_InventoryInFG_Save_InvFInventoryInFGDtlTblNotFound"; //// //InvF_InventoryInFG_Save_InvFInventoryInFGDtlTblNotFound
        public const string InvF_InventoryInFG_Save_InvFInventoryInFGDtlTblInvalid = "ErroriNOSInBrand.InvF_InventoryInFG_Save_InvFInventoryInFGDtlTblInvalid"; //// //InvF_InventoryInFG_Save_InvFInventoryInFGDtlTblInvalid
        public const string InvF_InventoryInFG_Save_InvF_InventoryInFGInstSerialNotFound = "ErroriNOSInBrand.InvF_InventoryInFG_Save_InvF_InventoryInFGInstSerialNotFound"; //// //InvF_InventoryInFG_Save_InvF_InventoryInFGInstSerialNotFound

        public const string InvF_InventoryInFG_Save_InvalidInputInvFInventoryInFGDtlTbl = "ErroriNOSInBrand.InvF_InventoryInFG_Save_InvalidInputInvFInventoryInFGDtlTbl"; //// //InvF_InventoryInFG_Save_InvalidInputInvFInventoryInFGDtlTbl
        public const string InvF_InventoryInFG_Save_InventoryInFGInputTbl_InvalidPMType = "ErroriNOSInBrand.InvF_InventoryInFG_Save_InventoryInFGInputTbl_InvalidPMType"; //// //InvF_InventoryInFG_Save_InventoryInFGInputTbl_InvalidPMType
        public const string InvF_InventoryInFG_Save_InventoryInFGInstSerialInvalidInputTbl = "ErroriNOSInBrand.InvF_InventoryInFG_Save_InventoryInFGInstSerialInvalidInputTbl"; //// //InvF_InventoryInFG_Save_InventoryInFGInstSerialInvalidInputTbl
        public const string WAS_InvF_InventoryInFG_Save = "ErroriNOSInBrand.WAS_InvF_InventoryInFG_Save"; //// //WAS_InvF_InventoryInFG_Save

        // InvF_InventoryInFG_Approve:
        public const string InvF_InventoryInFG_Approve = "ErroriNOSInBrand.InvF_InventoryInFG_Approve"; //// //InvF_InventoryInFG_Approve
        public const string WAS_InvF_InventoryInFG_Approve = "ErroriNOSInBrand.InvF_InventoryInFG_Save"; //// //InvF_InventoryInFG_Approve

        // InvF_InventoryInFG_UpdAfterAprr:
        public const string InvF_InventoryInFG_UpdAfterAprr = "ErroriNOSInBrand.InvF_InventoryInFG_UpdAfterAprr"; //// //InvF_InventoryInFG_UpdAfterAprr
        public const string InvF_InventoryInFG_UpdAfterAprr_InvalidFormInType = "ErroriNOSInBrand.InvF_InventoryInFG_UpdAfterAprr_InvalidFormInType"; //// //InvF_InventoryInFG_UpdAfterAprr_InvalidFormInType
        public const string InvF_InventoryInFG_UpdAfterAprr_InvalidInputInvFInventoryInFGDtlTbl = "ErroriNOSInBrand.InvF_InventoryInFG_UpdAfterAprr_InvalidInputInvFInventoryInFGDtlTbl"; //// //InvF_InventoryInFG_UpdAfterAprr_InvalidInputInvFInventoryInFGDtlTbl
        public const string InvF_InventoryInFG_UpdAfterAprr_InventoryInFGInputTbl_InvalidPMType = "ErroriNOSInBrand.InvF_InventoryInFG_UpdAfterAprr_InventoryInFGInputTbl_InvalidPMType"; //// //InvF_InventoryInFG_UpdAfterAprr_InventoryInFGInputTbl_InvalidPMType
        public const string InvF_InventoryInFG_UpdAfterAprr_InventoryInFGInstSerialInvalidInputTbl = "ErroriNOSInBrand.InvF_InventoryInFG_UpdAfterAprr_InventoryInFGInstSerialInvalidInputTbl"; //// //InvF_InventoryInFG_UpdAfterAprr_InventoryInFGInstSerialInvalidInputTbl

        //// InvF_InventoryInFG_Approve:
        public const string InvF_InventoryInFG_Approve_InvalidMST = "ErroriNOSInBrand.InvF_InventoryInFG_Approve_InvalidMST"; //// //InvF_InventoryInFG_Approve_InvalidMST
        #endregion

        #region // InvF_InventoryOutFG:
        // InvF_InventoryInFG_CheckDB:
        public const string InvF_InventoryOutFG_CheckDB_InvInFGNoNotFound = "ErroriNOSInBrand.InvF_InventoryOutFG_CheckDB_InvInFGNoNotFound"; //// //InvF_InventoryOutFG_CheckDB_InvInFGNoNotFound
        public const string InvF_InventoryOutFG_CheckDB_InvInFGNoExist = "ErroriNOSInBrand.InvF_InventoryOutFG_CheckDB_InvInFGNoExist"; //// //InvF_InventoryOutFG_CheckDB_InvInFGNoExist
        public const string InvF_InventoryOutFG_CheckDB_StatusNotMatched = "ErroriNOSInBrand.InvF_InventoryOutFG_CheckDB_StatusNotMatched"; //// //InvF_InventoryOutFG_CheckDB_StatusNotMatched

        // InvF_InventoryOutFG_Get:
        public const string InvF_InventoryOutFG_Get = "ErroriNOSInBrand.InvF_InventoryOutFG_Get"; //// //InvF_InventoryOutFG_Get

        // WAS_InvF_InventoryOutFG_Get:
        public const string WAS_InvF_InventoryOutFG_Get = "ErroriNOSInBrand.WAS_InvF_InventoryOutFG_Get"; //// //WAS_InvF_InventoryOutFG_Get

        // InvF_InventoryOutFG_Save:
        public const string InvF_InventoryOutFG_Save = "ErroriNOSInBrand.InvF_InventoryOutFG_Save"; //// //InvF_InventoryOutFG_Save
        public const string InvF_InventoryOutFG_Save_InvalidIF_InvOutFGNo = "ErroriNOSInBrand.InvF_InventoryOutFG_Save_InvalidIF_InvOutFGNo"; //// //InvF_InventoryOutFG_Save_InvalidIF_InvOutFGNo
        public const string InvF_InventoryOutFG_Save_InvalidStatus = "ErroriNOSInBrand.InvF_InventoryOutFG_Save_InvalidStatus"; //// //InvF_InventoryOutFG_Save_InvalidStatus
        public const string InvF_InventoryOutFG_Save_InvalidMST = "ErroriNOSInBrand.InvF_InventoryOutFG_Save_InvalidMST"; //// //InvF_InventoryOutFG_Save_InvalidMST
        public const string InvF_InventoryOutFG_Save_InvFInventoryOutFGDtlTblNotFound = "ErroriNOSInBrand.InvF_InventoryOutFG_Save_InvFInventoryOutFGDtlTblNotFound"; //// //InvF_InventoryOutFG_Save_InvFInventoryOutFGDtlTblNotFound
        public const string InvF_InventoryOutFG_Save_InvFInventoryOutFGDtlTblInvalid = "ErroriNOSInBrand.InvF_InventoryOutFG_Save_InvFInventoryOutFGDtlTblInvalid"; //// //InvF_InventoryOutFG_Save_InvFInventoryOutFGDtlTblInvalid
        public const string InvF_InventoryOutFG_Save_InvF_InventoryOutFGInstSerialNotFound = "ErroriNOSInBrand.InvF_InventoryOutFG_Save_InvF_InventoryOutFGInstSerialNotFound"; //// //InvF_InventoryOutFG_Save_InvF_InventoryOutFGInstSerialNotFound
        public const string InvF_InventoryOutFG_Save_InvFInventoryOutDtlTblInvalid = "ErroriNOSInBrand.InvF_InventoryOutFG_Save_InvFInventoryOutDtlTblInvalid"; //// //InvF_InventoryOutFG_Save_InvFInventoryOutDtlTblInvalid
        public const string InvF_InventoryOutFG_Save_InvalidInvFOutType = "ErroriNOSInBrand.InvF_InventoryOutFG_Save_InvalidInvFOutType"; //// //InvF_InventoryOutFG_Save_InvalidInvFOutType
        public const string InvF_InventoryOutFG_Save_SerialNoNotExistInInvOfMST = "ErroriNOSInBrand.InvF_InventoryOutFG_Save_SerialNoNotExistInInvOfMST"; //// //InvF_InventoryOutFG_Save_SerialNoNotExistInInvOfMST

        // WAS_InvF_InventoryOutFG_SaveSpecial:
        public const string WAS_InvF_InventoryOutFG_SaveSpecial = "ErroriNOSInBrand.WAS_InvF_InventoryOutFG_SaveSpecial"; //// //WAS_InvF_InventoryOutFG_SaveSpecial

        // InvF_InventoryOutFG_Approve
        public const string InvF_InventoryOutFG_Approve = "ErroriNOSInBrand.InvF_InventoryOutFG_Approve"; //// //InvF_InventoryOutFG_Approve
        public const string InvF_InventoryOutFG_Approve_InvalidMST = "ErroriNOSInBrand.InvF_InventoryOutFG_Approve_InvalidMST"; //// //InvF_InventoryOutFG_Approve_InvalidMST

        // WAS_InvF_InventoryOutFG_Approve
        public const string WAS_InvF_InventoryOutFG_Approve = "ErroriNOSInBrand.WAS_InvF_InventoryOutFG_Approve"; //// //WAS_InvF_InventoryOutFG_Approve
        #endregion

        #region // InvF_InventoryInFGInstSerial:
        // InvF_InventoryInFGInstSerial_Get:
        public const string InvF_InventoryInFGInstSerial_Get = "ErroriNOSInBrand.InvF_InventoryInFGInstSerial_Get"; //// //InvF_InventoryInFGInstSerial_Get
        public const string WAS_InvF_InventoryInFGInstSerial_Get = "ErroriNOSInBrand.WAS_InvF_InventoryInFGInstSerial_Get"; //// //WAS_InvF_InventoryInFGInstSerial_Get
        #endregion 

        #region // InvF_InventoryOutFGInstSerial:
        // InvF_InventoryOutFGInstSerial_Get:
        public const string InvF_InventoryOutFGInstSerial_Get = "ErroriNOSInBrand.InvF_InventoryOutFGInstSerial_Get"; //// //InvF_InventoryOutFGInstSerial_Get
        public const string WAS_InvF_InventoryOutFGInstSerial_Get = "ErroriNOSInBrand.WAS_InvF_InventoryOutFGInstSerial_Get"; //// //WAS_InvF_InventoryOutFGInstSerial_Get
        #endregion 

        #region // InvF_InventoryOutFGInstSerial:
        #endregion 

        #region // Inv_InventoryBalanceSerial:
        // Inv_InventoryBalanceSerial_Map:
        public const string Inv_InventoryBalanceSerial_Map = "ErroriNOSInBrand.Inv_InventoryBalanceSerial_Map"; //// //Inv_InventoryBalanceSerial_Map
        public const string Inv_InventoryBalanceSerial_Map_InventoryBalanceSerialTblNotFound = "ErroriNOSInBrand.Inv_InventoryBalanceSerial_Map_InventoryBalanceSerialTblNotFound"; //// //Inv_InventoryBalanceSerial_Map_InventoryBalanceSerialTblNotFound
        public const string Inv_InventoryBalanceSerial_Map_InventoryBalanceSerialTblInvalid = "ErroriNOSInBrand.Inv_InventoryBalanceSerial_Map_InventoryBalanceSerialTblInvalid"; //// //Inv_InventoryBalanceSerial_Map_InventoryBalanceSerialTblInvalid
        public const string Inv_InventoryBalanceSerial_Map_InvalidMST = "ErroriNOSInBrand.Inv_InventoryBalanceSerial_Map_InvalidMST"; //// //Inv_InventoryBalanceSerial_Map_InvalidMST
        public const string Inv_InventoryBalanceSerial_Map_ExistSerialInInv = "ErroriNOSInBrand.Inv_InventoryBalanceSerial_Map_ExistSerialInInv"; //// //Inv_InventoryBalanceSerial_Map_ExistSerialInInv
        public const string Inv_InventoryBalanceSerial_Map_SerialNoExistInInvGen = "ErroriNOSInBrand.Inv_InventoryBalanceSerial_Map_SerialNoExistInInvGen"; //// //Inv_InventoryBalanceSerial_Map_SerialNoExistInInvGen
        public const string Inv_InventoryBalanceSerial_Map_SerialNoNotExportExcel = "ErroriNOSInBrand.Inv_InventoryBalanceSerial_Map_SerialNoNotExportExcel"; //// //Inv_InventoryBalanceSerial_Map_SerialNoNotExportExcel
        public const string Inv_InventoryBalanceSerial_Map_SerialNoNotExistInInvGenOfMST = "ErroriNOSInBrand.Inv_InventoryBalanceSerial_Map_SerialNoNotExistInInvGenOfMST"; //// //Inv_InventoryBalanceSerial_Map_SerialNoNotExistInInvGenOfMST
        public const string Inv_InventoryBalanceSerial_Map_BoxNoNotExistInInvGen = "ErroriNOSInBrand.Inv_InventoryBalanceSerial_Map_BoxNoNotExistInInvGen"; //// //Inv_InventoryBalanceSerial_Map_BoxNoNotExistInInvGen
        public const string Inv_InventoryBalanceSerial_Map_BoxNoNoOutExcel = "ErroriNOSInBrand.Inv_InventoryBalanceSerial_Map_BoxNoNoOutExcel"; //// //Inv_InventoryBalanceSerial_Map_BoxNoNoOutExcel
        public const string Inv_InventoryBalanceSerial_Map_BoxNoNotExistInInvGenOfMST = "ErroriNOSInBrand.Inv_InventoryBalanceSerial_Map_BoxNoNotExistInInvGenOfMST"; //// //Inv_InventoryBalanceSerial_Map_BoxNoNotExistInInvGenOfMST
        public const string Inv_InventoryBalanceSerial_Map_BoxNoExistMap = "ErroriNOSInBrand.Inv_InventoryBalanceSerial_Map_BoxNoExistMap"; //// //Inv_InventoryBalanceSerial_Map_BoxNoExistMap
        public const string Inv_InventoryBalanceSerial_Map_CanNoNotExistInInvGen = "ErroriNOSInBrand.Inv_InventoryBalanceSerial_Map_CanNoNotExistInInvGen"; //// //Inv_InventoryBalanceSerial_Map_CanNoNotExistInInvGen
        public const string Inv_InventoryBalanceSerial_Map_CanNoNoExportExcel = "ErroriNOSInBrand.Inv_InventoryBalanceSerial_Map_CanNoNoExportExcel"; //// //Inv_InventoryBalanceSerial_Map_CanNoNoExportExcel
        public const string Inv_InventoryBalanceSerial_Map_CanNoNotExistInInvGenOfMST = "ErroriNOSInBrand.Inv_InventoryBalanceSerial_Map_CanNoNotExistInInvGenOfMST"; //// //Inv_InventoryBalanceSerial_Map_CanNoNotExistInInvGenOfMST
        public const string Inv_InventoryBalanceSerial_Map_CanNoExistedMap = "ErroriNOSInBrand.Inv_InventoryBalanceSerial_Map_CanNoExistedMap"; //// //Inv_InventoryBalanceSerial_Map_CanNoExistedMap
        public const string Inv_InventoryBalanceSerial_Map_OneBoxOneSerial = "ErroriNOSInBrand.Inv_InventoryBalanceSerial_Map_OneBoxOneSerial"; //// //Inv_InventoryBalanceSerial_Map_OneBoxOneSerial

        // WAS_Inv_InventoryBalanceSerial_Map:
        public const string WAS_Inv_InventoryBalanceSerial_Map = "ErroriNOSInBrand.WAS_Inv_InventoryBalanceSerial_Map"; //// //WAS_Inv_InventoryBalanceSerial_Map

        // Inv_InventoryBalanceSerial_Get:
        public const string Inv_InventoryBalanceSerial_Get = "ErroriNOSInBrand.Inv_InventoryBalanceSerial_Get"; //// //Inv_InventoryBalanceSerial_Get

        // WAS_Inv_InventoryBalanceSerial_Get:
        public const string WAS_Inv_InventoryBalanceSerial_Get = "ErroriNOSInBrand.WAS_Inv_InventoryBalanceSerial_Get"; //// //WAS_Inv_InventoryBalanceSerial_Get


        // Inv_InventoryBalanceSerial_UpdBoxX:
        public const string Inv_InventoryBalanceSerial_UpdBoxX = "ErroriNOSInBrand.Inv_InventoryBalanceSerial_UpdBoxX"; //// //Inv_InventoryBalanceSerial_UpdBoxX
        public const string Inv_InventoryBalanceSerial_UpdBoxX_InventoryBalanceSerialNotFound = "ErroriNOSInBrand.Inv_InventoryBalanceSerial_UpdBoxX_InventoryBalanceSerialNotFound"; //// //Inv_InventoryBalanceSerial_UpdBoxX_InventoryBalanceSerialNotFound
        public const string Inv_InventoryBalanceSerial_UpdBoxX_InventoryBalanceSerialInvalid = "ErroriNOSInBrand.Inv_InventoryBalanceSerial_UpdBoxX_InventoryBalanceSerialInvalid"; //// //Inv_InventoryBalanceSerial_UpdBoxX_InventoryBalanceSerialInvalid
        public const string Inv_InventoryBalanceSerial_UpdBoxX_InvalidSerial = "ErroriNOSInBrand.Inv_InventoryBalanceSerial_UpdBoxX_InvalidSerial"; //// //Inv_InventoryBalanceSerial_UpdBoxX_InvalidSerial
        public const string Inv_InventoryBalanceSerial_UpdBoxX_InvalidCanNo = "ErroriNOSInBrand.Inv_InventoryBalanceSerial_UpdBoxX_InvalidCanNo"; //// //Inv_InventoryBalanceSerial_UpdBoxX_InvalidCanNo
        public const string Inv_InventoryBalanceSerial_UpdBoxX_InvalidFlagCan = "ErroriNOSInBrand.Inv_InventoryBalanceSerial_UpdBoxX_InvalidFlagCan"; //// //Inv_InventoryBalanceSerial_UpdBoxX_InvalidFlagCan
        public const string Inv_InventoryBalanceSerial_UpdBoxX_InvalidMST = "ErroriNOSInBrand.Inv_InventoryBalanceSerial_UpdBoxX_InvalidMST"; //// //Inv_InventoryBalanceSerial_UpdBoxX_InvalidMST


        // Inv_InventoryBalanceSerial_UpdCan:
        public const string Inv_InventoryBalanceSerial_UpdCan = "ErroriNOSInBrand.Inv_InventoryBalanceSerial_UpdCan"; //// //Inv_InventoryBalanceSerial_UpdCan


        // WAS_Inv_InventoryBalanceSerial_UpdCan:
        public const string WAS_Inv_InventoryBalanceSerial_UpdCan = "ErroriNOSInBrand.WAS_Inv_InventoryBalanceSerial_UpdCan"; //// //WAS_Inv_InventoryBalanceSerial_UpdCan

        // Inv_InventoryBalanceSerial_UpdCanX:
        public const string Inv_InventoryBalanceSerial_UpdCanX = "ErroriNOSInBrand.Inv_InventoryBalanceSerial_UpdCanX"; //// //Inv_InventoryBalanceSerial_UpdCanX
        public const string Inv_InventoryBalanceSerial_UpdCanX_InventoryBalanceSerialNotFound = "ErroriNOSInBrand.Inv_InventoryBalanceSerial_UpdCanX_InventoryBalanceSerialNotFound"; //// //Inv_InventoryBalanceSerial_UpdCanX_InventoryBalanceSerialNotFound
        public const string Inv_InventoryBalanceSerial_UpdCanX_InventoryBalanceSerialInvalid = "ErroriNOSInBrand.Inv_InventoryBalanceSerial_UpdCanX_InventoryBalanceSerialInvalid"; //// //Inv_InventoryBalanceSerial_UpdCanX_InventoryBalanceSerialInvalid
        public const string Inv_InventoryBalanceSerial_UpdCanX_InvalidSerial = "ErroriNOSInBrand.Inv_InventoryBalanceSerial_UpdCanX_InvalidSerial"; //// //Inv_InventoryBalanceSerial_UpdCanX_InvalidSerial
        public const string Inv_InventoryBalanceSerial_UpdCanX_InvalidCanNo = "ErroriNOSInBrand.Inv_InventoryBalanceSerial_UpdCanX_InvalidCanNo"; //// //Inv_InventoryBalanceSerial_UpdCanX_InvalidCanNo
        public const string Inv_InventoryBalanceSerial_UpdCanX_InvalidFlagCan = "ErroriNOSInBrand.Inv_InventoryBalanceSerial_UpdCanX_InvalidFlagCan"; //// //Inv_InventoryBalanceSerial_UpdCanX_InvalidFlagCan
        public const string Inv_InventoryBalanceSerial_UpdCanX_InvalidMST = "ErroriNOSInBrand.Inv_InventoryBalanceSerial_UpdCanX_InvalidMST"; //// //Inv_InventoryBalanceSerial_UpdCanX_InvalidMST
        public const string Inv_InventoryBalanceSerial_UpdCanX_CanNoNotExistInInvGen = "ErroriNOSInBrand.Inv_InventoryBalanceSerial_UpdCanX_CanNoNotExistInInvGen"; //// //Inv_InventoryBalanceSerial_UpdCanX_CanNoNotExistInInvGen
        public const string Inv_InventoryBalanceSerial_UpdCanX_CanNoNoExportExcel = "ErroriNOSInBrand.Inv_InventoryBalanceSerial_UpdCanX_CanNoNoExportExcel"; //// //Inv_InventoryBalanceSerial_UpdCanX_CanNoNoExportExcel
        public const string Inv_InventoryBalanceSerial_UpdCanX_CanNoNotExistInInvGenOfMST = "ErroriNOSInBrand.Inv_InventoryBalanceSerial_UpdCanX_CanNoNotExistInInvGenOfMST"; //// //Inv_InventoryBalanceSerial_UpdCanX_CanNoNotExistInInvGenOfMST
        public const string Inv_InventoryBalanceSerial_UpdCanX_CanNoExistedMap = "ErroriNOSInBrand.Inv_InventoryBalanceSerial_UpdCanX_CanNoExistedMap"; //// //Inv_InventoryBalanceSerial_UpdCanX_CanNoExistedMap

        // Inv_InventoryBalanceSerial_UpdCanFromBox:
        public const string Inv_InventoryBalanceSerial_UpdCanFromBox = "ErroriNOSInBrand.Inv_InventoryBalanceSerial_UpdCanFromBox"; //// //Inv_InventoryBalanceSerial_UpdCanFromBox


        // WAS_Inv_InventoryBalanceSerial_UpdCanFromBox:
        public const string WAS_Inv_InventoryBalanceSerial_UpdCanFromBox = "ErroriNOSInBrand.WAS_Inv_InventoryBalanceSerial_UpdCanFromBox"; //// //WAS_Inv_InventoryBalanceSerial_UpdCanFromBox


        // Inv_InventoryBalance_Get:
        public const string Inv_InventoryBalance_Get = "ErroriNOSInBrand.Inv_InventoryBalance_Get"; //// //Inv_InventoryBalance_Get


        // Inv_InventoryBalanceSerial_OutInvX:
        public const string Inv_InventoryBalanceSerial_OutInvX_InventoryBalanceSerialTblNotFound = "ErroriNOSInBrand.Inv_InventoryBalanceSerial_OutInvX_InventoryBalanceSerialTblNotFound"; //// //Inv_InventoryBalanceSerial_OutInvX_InventoryBalanceSerialTblNotFound

        public const string Inv_InventoryBalanceSerial_OutInv = "ErroriNOSInBrand.Inv_InventoryBalanceSerial_OutInv"; //// //Inv_InventoryBalanceSerial_OutInv

        public const string WAS_Inv_InventoryBalanceSerial_OutInv = "ErroriNOSInBrand.WAS_Inv_InventoryBalanceSerial_OutInv"; //// //WAS_Inv_InventoryBalanceSerial_OutInv

        public const string Inv_InventoryBalanceSerial_OutInvX_InventoryBalanceSerialTblInvalid = "ErroriNOSInBrand.Inv_InventoryBalanceSerial_OutInvX_InventoryBalanceSerialTblInvalid"; //// //Inv_InventoryBalanceSerial_OutInvX_InventoryBalanceSerialTblInvalid
        public const string Inv_InventoryBalanceSerial_OutInvX_InventoryBalanceBoxTblNotFound = "ErroriNOSInBrand.Inv_InventoryBalanceSerial_OutInvX_InventoryBalanceBoxTblNotFound"; //// //Inv_InventoryBalanceSerial_OutInvX_InventoryBalanceBoxTblNotFound
        public const string Inv_InventoryBalanceSerial_OutInvX_InventoryBalanceBoxTblInvalid = "ErroriNOSInBrand.Inv_InventoryBalanceSerial_OutInvX_InventoryBalanceBoxTblInvalid"; //// //Inv_InventoryBalanceSerial_OutInvX_InventoryBalanceBoxTblInvalid
        public const string Inv_InventoryBalanceSerial_OutInvX_InventoryBalanceCanTblNotFound = "ErroriNOSInBrand.Inv_InventoryBalanceSerial_OutInvX_InventoryBalanceCanTblNotFound"; //// //Inv_InventoryBalanceSerial_OutInvX_InventoryBalanceCanTblNotFound
        public const string Inv_InventoryBalanceSerial_OutInvX_InventoryBalanceCanTblInvalid = "ErroriNOSInBrand.Inv_InventoryBalanceSerial_OutInvX_InventoryBalanceCanTblInvalid"; //// //Inv_InventoryBalanceSerial_OutInvX_InventoryBalanceCanTblInvalid
        public const string Inv_InventoryBalanceSerial_OutInvX_InvlidListSerialAndCanAndBox = "ErroriNOSInBrand.Inv_InventoryBalanceSerial_OutInvX_InvlidListSerialAndCanAndBox"; //// //Inv_InventoryBalanceSerial_OutInvX_InvlidListSerialAndCanAndBox
        public const string Inv_InventoryBalanceSerial_OutInvX_InvlidSerialNoBelongToNNTOrther = "ErroriNOSInBrand.Inv_InventoryBalanceSerial_OutInvX_InvlidSerialNoBelongToNNTOrther"; //// //Inv_InventoryBalanceSerial_OutInvX_InvlidSerialNoBelongToNNTOrther
        public const string Inv_InventoryBalanceSerial_OutInvX_InvlidFlagSales = "ErroriNOSInBrand.Inv_InventoryBalanceSerial_OutInvX_InvlidFlagSales"; //// //Inv_InventoryBalanceSerial_OutInvX_InvlidFlagSales
        public const string Inv_InventoryBalanceSerial_OutInvX_InvlidInvFOut = "ErroriNOSInBrand.Inv_InventoryBalanceSerial_OutInvX_InvlidInvFOut"; //// //Inv_InventoryBalanceSerial_OutInvX_InvlidInvFOut
        public const string Inv_InventoryBalanceSerial_OutInvX_CanNoNotExist = "ErroriNOSInBrand.Inv_InventoryBalanceSerial_OutInvX_CanNoNotExist"; //// //Inv_InventoryBalanceSerial_OutInvX_CanNoNotExist
        public const string Inv_InventoryBalanceSerial_OutInvX_BoxNoNotExist = "ErroriNOSInBrand.Inv_InventoryBalanceSerial_OutInvX_BoxNoNotExist"; //// //Inv_InventoryBalanceSerial_OutInvX_BoxNoNotExist
        public const string Inv_InventoryBalanceSerial_OutInvX_InvalidInvCode = "ErroriNOSInBrand.Inv_InventoryBalanceSerial_OutInvX_InvalidInvCode"; //// //Inv_InventoryBalanceSerial_OutInvX_InvalidInvCode
        public const string Inv_InventoryBalanceSerial_OutInvX_InvalidAgentCode_Current = "ErroriNOSInBrand.Inv_InventoryBalanceSerial_OutInvX_InvalidAgentCode_Current"; //// //Inv_InventoryBalanceSerial_OutInvX_InvalidAgentCode_Current
        #endregion

        #region // InvF_InventoryOutHist:
        // InvF_InventoryOutHist_CheckDB:
        public const string InvF_InventoryOutHist_CheckDB_InvOutHistNoNotFound = "ErroriNOSInBrand.InvF_InventoryOutHist_CheckDB_InvOutHistNoNotFound"; //// //InvF_InventoryOutHist_CheckDB_InvOutHistNoNotFound
        public const string InvF_InventoryOutHist_CheckDB_InvOutHistNoExist = "ErroriNOSInBrand.InvF_InventoryOutHist_CheckDB_InvOutHistNoExist"; //// //InvF_InventoryOutHist_CheckDB_InvOutHistNoExist
        public const string InvF_InventoryOutHist_CheckDB_StatusNotMatched = "ErroriNOSInBrand.InvF_InventoryOutHist_CheckDB_StatusNotMatched"; //// //InvF_InventoryOutHist_CheckDB_StatusNotMatched

        // InvF_InventoryOutHist_Get:
        public const string InvF_InventoryOutHist_Get = "ErroriNOSInBrand.InvF_InventoryOutHist_Get"; //// //InvF_InventoryOutHist_Get

        // WAS_InvF_InventoryOutHist_Get:
        public const string WAS_InvF_InventoryOutHist_Get = "ErroriNOSInBrand.WAS_InvF_InventoryOutHist_Get"; //// //WAS_InvF_InventoryOutHist_Get

        // InvF_InventoryOutHist_SaveX:
        public const string InvF_InventoryOutHist_SaveX = "ErroriNOSInBrand.InvF_InventoryOutHist_SaveX"; //// //InvF_InventoryOutHist_SaveX
        public const string InvF_InventoryOutHist_Save_InvalidIF_InvOutHistNo = "ErroriNOSInBrand.InvF_InventoryOutHist_Save_InvalidIF_InvOutHistNo"; //// //InvF_InventoryOutHist_Save_InvalidIF_InvOutHistNo
        public const string InvF_InventoryOutHist_Save_InvalidStatus = "ErroriNOSInBrand.InvF_InventoryOutHist_Save_InvalidStatus"; //// //InvF_InventoryOutHist_Save_InvalidStatus
        public const string InvF_InventoryOutHist_Save_InvFInventoryOutHistDtlTblNotFound = "ErroriNOSInBrand.InvF_InventoryOutHist_Save_InvFInventoryOutHistDtlTblNotFound"; //// //InvF_InventoryOutHist_Save_InvFInventoryOutHistDtlTblNotFound
        public const string InvF_InventoryOutHist_Save_InvFInventoryOutDtlTblInvalid = "ErroriNOSInBrand.InvF_InventoryOutHist_Save_InvFInventoryOutDtlTblInvalid"; //// //InvF_InventoryOutHist_Save_InvFInventoryOutDtlTblInvalid
        public const string InvF_InventoryOutHist_Save_InvF_InventoryOutHistInstSerialNotFound = "ErroriNOSInBrand.InvF_InventoryOutHist_Save_InvF_InventoryOutHistInstSerialNotFound"; //// //InvF_InventoryOutHist_Save_InvF_InventoryOutHistInstSerialNotFound
        public const string InvF_InventoryOutHist_Save_InvF_InvalidSerialNotEqualSource = "ErroriNOSInBrand.InvF_InventoryOutHist_Save_InvF_InvalidSerialNotEqualSource"; //// //InvF_InventoryOutHist_Save_InvF_InvalidSerialNotEqualSource
        public const string InvF_InventoryOutHist_Save_SerialFlagSalesNotMatch= "ErroriNOSInBrand.InvF_InventoryOutHist_Save_SerialFlagSalesNotMatch"; //// //InvF_InventoryOutHist_Save_SerialFlagSalesNotMatch
        public const string InvF_InventoryOutHist_Save_InvF_SerialNoExist = "ErroriNOSInBrand.InvF_InventoryOutHist_Save_InvF_SerialNoExist"; //// //InvF_InventoryOutHist_Save_InvF_SerialNoExist
        public const string InvF_InventoryOutHist_Save_InvF_AgentCodeNotEqual = "ErroriNOSInBrand.InvF_InventoryOutHist_Save_InvF_AgentCodeNotEqual"; //// //InvF_InventoryOutHist_Save_InvF_AgentCodeNotEqual
        public const string InvF_InventoryOutHist_Save_InvalidInvFOutType = "ErroriNOSInBrand.InvF_InventoryOutHist_Save_InvalidInvFOutType"; //// //InvF_InventoryOutHist_Save_InvalidInvFOutTypeInvF_InventoryOutHist_Save_InvF_AgentCodeNotEqual
        public const string InvF_InventoryOutHist_SaveSpecial = "ErroriNOSInBrand.InvF_InventoryOutHist_SaveSpecial"; //// //InvF_InventoryOutHist_SaveSpecial

        // WAS_InvF_InventoryOutHist_SaveSpecial:
        public const string WAS_InvF_InventoryOutHist_SaveSpecial = "ErroriNOSInBrand.WAS_InvF_InventoryOutHist_SaveSpecial"; //// //WAS_InvF_InventoryOutHist_SaveSpecial

        // InvF_InventoryOutHist_Approve:
        public const string InvF_InventoryOutHist_Approve = "ErroriNOSInBrand.InvF_InventoryOutHist_Approve"; //// //InvF_InventoryOutHist_Approve

        // WAS_InvF_InventoryOutHist_Approve:
        public const string WAS_InvF_InventoryOutHist_Approve = "ErroriNOSInBrand.WAS_InvF_InventoryOutHist_Approve"; //// //WAS_InvF_InventoryOutHist_Approve

        ////  Rpt_SearchHis_Get
        public const string WAS_Rpt_SearchHis_Add = "ErroriNOSInBrand.WAS_Rpt_SearchHis_Add"; //// //WAS_Rpt_SearchHis_Add
        public const string WAS_Rpt_SearchHis_Get = "ErroriNOSInBrand.WAS_Rpt_SearchHis_Get"; //// //WAS_Rpt_SearchHis_Get
        public const string Rpt_SearchHis_Get = "ErroriNOSInBrand.Rpt_SearchHis_Get"; //// //Rpt_SearchHis_Get
        #endregion

        #region // Report:
        // Rpt_Inv_InventoryBalanceSerialForSearch:
        public const string Rpt_Inv_InventoryBalanceSerialForSearch = "ErroriNOSInBrand.Rpt_Inv_InventoryBalanceSerialForSearch"; //// //Rpt_Inv_InventoryBalanceSerialForSearch
        // WAS_Rpt_Inv_InventoryBalanceSerialForSearch:
        public const string WAS_Rpt_Inv_InventoryBalanceSerialForSearch = "ErroriNOSInBrand.WAS_Rpt_Inv_InventoryBalanceSerialForSearch"; //// //WAS_Rpt_Inv_InventoryBalanceSerialForSearch
		#endregion

		#region // Mst_Spec:
		// Mst_Spec_CheckDB:
		public const string Mst_Spec_CheckDB_SpecNotFound = "ErridnInventory.Mst_Spec_CheckDB_SpecNotFound"; //// //Mst_Spec_CheckDB_SpecNotFound
		public const string Mst_Spec_CheckDB_SpecNotExist = "ErridnInventory.Mst_Spec_CheckDB_SpecNotExist"; //// //Mst_Spec_CheckDB_SpecNotExist
		public const string Mst_Spec_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_Spec_CheckDB_FlagActiveNotMatched"; //// //Mst_Spec_CheckDB_FlagActiveNotMatched
		public const string Mst_Spec_CheckDB_FlagHasSerialNotMatched = "ErridnInventory.Mst_Spec_CheckDB_FlagHasSerialNotMatched"; //// //Mst_Spec_CheckDB_FlagHasSerialNotMatched
		public const string Mst_Spec_CheckDB_FlagHasLOTNotMatched = "ErridnInventory.Mst_Spec_CheckDB_FlagHasLOTNotMatched"; //// //Mst_Spec_CheckDB_FlagHasLOTNotMatched

		// Mst_Spec_CheckDB_NetworkSpecCodeNoEqualSpecCode:
		public const string Mst_Spec_CheckDB_NetworkSpecCodeNoEqualSpecCode = "ErridnInventory.Mst_Spec_CheckDB_NetworkSpecCodeNoEqualSpecCode"; //// //Mst_Spec_CheckDB_NetworkSpecCodeNoEqualSpecCode

		// Mst_Spec_CheckDB_NotExistNetworkSpecCode:
		public const string Mst_Spec_CheckDB_NotExistNetworkSpecCode = "ErridnInventory.Mst_Spec_CheckDB_NotExistNetworkSpecCode"; //// //Mst_Spec_CheckDB_NotExistNetworkSpecCode

		// WAS_Mst_Spec_CheckListDB:
		public const string WAS_Mst_Spec_CheckListDB = "ErridnInventory.WAS_Mst_Spec_CheckListDB"; //// //WAS_Mst_Spec_CheckListDB

		// Mst_Spec_CheckListDB:
		public const string Mst_Spec_CheckListDB = "ErridnInventory.Mst_Spec_CheckListDB"; //// //Mst_Spec_CheckListDB
		public const string Mst_Spec_CheckListDB_Input_MstSpecNotFound = "ErridnInventory.Mst_Spec_CheckListDB_Input_MstSpecNotFound"; //// //Mst_Spec_CheckListDB_Input_MstSpecNotFound
		public const string Mst_Spec_CheckListDB_Input_MstSpecTblInvalid = "ErridnInventory.Mst_Spec_CheckListDB_Input_MstSpecTblInvalid"; //// //Mst_Spec_CheckListDB_Input_MstSpecTblInvalid

		// Mst_Spec_Get:
		public const string Mst_Spec_Get = "ErridnInventory.Mst_Spec_Get"; //// //Mst_Spec_Get
		public const string Mst_Spec_Exist_Active_Get = "ErridNTVAN.Mst_Spec_Exist_Active_Get"; //// //Mst_Spec_Exist_Active_Get

		// WAS_Mst_Spec_Get:
		public const string WAS_Mst_Spec_Get = "ErridnInventory.WAS_Mst_Spec_Get"; //// //WAS_Mst_Spec_Get
		public const string WAS_Mst_Spec_Exist_Active_Get = "ErridnInventory.WAS_Mst_Spec_Exist_Active_Get"; //// //WAS_Mst_Spec_Exist_Active_Get

		// WAS_Mst_Spec_Add:
		public const string WAS_Mst_Spec_Add = "ErridnInventory.WAS_Mst_Spec_Add"; //// //WAS_Mst_Spec_Add

		// Mst_Spec_Add:
		public const string Mst_Spec_Add = "ErridnInventory.Mst_Spec_Add"; //// //Mst_Spec_Add
		public const string Mst_Spec_Add_InvalidSpecCode = "ErridnInventory.Mst_Spec_Add_InvalidSpecCode"; //// //Mst_Spec_Add_InvalidSpecCode
		public const string Mst_Spec_Add_NetworkSpecCodeNoEqualSpecCode = "ErridnInventory.Mst_Spec_Add_NetworkSpecCodeNoEqualSpecCode"; //// //Mst_Spec_Add_NetworkSpecCodeNoEqualSpecCode
		public const string Mst_Spec_Add_InvalidModelName = "ErridnInventory.Mst_Spec_Add_InvalidModelName"; //// //Mst_Spec_Add_InvalidModelName
		public const string Mst_Spec_Add_Input_Mst_SpecImageTblNotFound = "ErridnInventory.Mst_Spec_Add_Input_Mst_SpecImageTblNotFound"; //// //Mst_Spec_Add_Input_Mst_SpecImageTblNotFound
		public const string Mst_Spec_Add_Input_Mst_SpecFilesTblNotFound = "ErridnInventory.Mst_Spec_Add_Input_Mst_SpecFilesTblNotFound"; //// //Mst_Spec_Add_Input_Mst_SpecFilesTblNotFound

		// WAS_Mst_Spec_Upd:
		public const string WAS_Mst_Spec_Upd = "ErridnInventory.WAS_Mst_Spec_Upd"; //// //WAS_Mst_Spec_Upd

		// Mst_Spec_Upd:
		public const string Mst_Spec_Upd = "ErridnInventory.Mst_Spec_Upd"; //// //Mst_Spec_Upd
		public const string Mst_Spec_Upd_InvalidSpecCode = "ErridnInventory.Mst_Spec_Upd_InvalidSpecCode"; //// //Mst_Spec_Upd_InvalidSpecCode
		public const string Mst_Spec_Upd_InvalidSpecName = "ErridnInventory.Mst_Spec_Upd_InvalidSpecName"; //// //Mst_Spec_Upd_InvalidSpecName
		public const string Mst_Spec_Upd_Input_Mst_SpecImageTblNotFound = "ErridnInventory.Mst_Spec_Upd_Input_Mst_SpecImageTblNotFound"; //// //Mst_Spec_Upd_Input_Mst_SpecImageTblNotFound
		public const string Mst_Spec_Upd_Input_Mst_SpecFilesTblNotFound = "ErridnInventory.Mst_Spec_Upd_Input_Mst_SpecFilesTblNotFound"; //// //Mst_Spec_Upd_Input_Mst_SpecFilesTblNotFound
		public const string Mst_Spec_Upd_NetworkSpecCodeNoEqualSpecCode = "ErridnInventory.Mst_Spec_Upd_NetworkSpecCodeNoEqualSpecCode"; //// //Mst_Spec_Upd_NetworkSpecCodeNoEqualSpecCode

		// Mst_Spec_Del:
		public const string Mst_Spec_Del = "ErridnInventory.Mst_Spec_Del"; //// //Mst_Spec_Del

		// WAS_Mst_Spec_Del:
		public const string WAS_Mst_Spec_Del = "ErridnInventory.WAS_Mst_Spec_Del"; //// //WAS_Mst_Spec_Del
		#endregion

		#region // Mst_Org:
		// Mst_Org_CheckDB:
		public const string Mst_Org_CheckDB_OrgIDNotFound = "ErridnInventory.Mst_Org_CheckDB_OrgIDNotFound"; //// //Mst_Org_CheckDB_OrgIDNotFound
		#endregion

		#region // Mst_Model:
		// Mst_Model_CheckDB:
		public const string Mst_Model_CheckDB_ModelCodeNotFound = "ErridnInventory.Mst_Model_CheckDB_ModelCodeNotFound"; //// //Mst_Model_CheckDB_ModelCodeNotFound
		public const string Mst_Model_CheckDB_ModelCodeExist = "ErridnInventory.Mst_Model_CheckDB_ModelCodeExist"; //// //Mst_Model_CheckDB_ModelCodeExist
		public const string Mst_Model_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_Model_CheckDB_FlagActiveNotMatched"; //// //Mst_Model_CheckDB_FlagActiveNotMatched

		#endregion

		#region // Mst_Unit:
		// Mst_Unit_CheckDB:
		public const string Mst_Unit_CheckDB_UnitCodeNotFound = "ErridnInventory.Mst_Unit_CheckDB_UnitCodeNotFound"; //// //Mst_Unit_CheckDB_UnitCodeNotFound
		public const string Mst_Unit_CheckDB_UnitCodeExist = "ErridnInventory.Mst_Unit_CheckDB_UnitCodeExist"; //// Mst_Unit_CheckDB_UnitCodeExist
		public const string Mst_Unit_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_Unit_CheckDB_FlagActiveNotMatched"; //// //Mst_Unit_CheckDB_FlagActiveNotMatched

		#endregion

		#region // Mst_SpecType1:
		// Mst_SpecType1_CheckDB:
		public const string Mst_SpecType1_CheckDB_SpecType1NotFound = "ErridnInventory.Mst_SpecType1_CheckDB_SpecType1NotFound"; //// //Mst_SpecType1_CheckDB_SpecType1NotFound
		public const string Mst_SpecType1_CheckDB_SpecType1Exist = "ErridnInventory.Mst_SpecType1_CheckDB_SpecType1Exist"; //// //Mst_SpecType1_CheckDB_SpecType1Exist
		public const string Mst_SpecType1_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_SpecType1_CheckDB_FlagActiveNotMatched"; //// //Mst_SpecType1_CheckDB_FlagActiveNotMatched

		#endregion

		#region // Mst_SpecType2:
		// Mst_SpecType2_CheckDB:
		public const string Mst_SpecType2_CheckDB_SpecType2NotFound = "ErridnInventory.Mst_SpecType2_CheckDB_SpecType2NotFound"; //// //Mst_SpecType2_CheckDB_SpecType2NotFound
		public const string Mst_SpecType2_CheckDB_SpecType2Exist = "ErridnInventory.Mst_SpecType2_CheckDB_SpecType2Exist"; //// //Mst_SpecType2_CheckDB_SpecType2Exist
		public const string Mst_SpecType2_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_SpecType2_CheckDB_FlagActiveNotMatched"; //// //Mst_SpecType2_CheckDB_FlagActiveNotMatched

		#endregion

		#region // Mst_SpecCustomField:
		// Mst_SpecCustomField_CheckDB:
		public const string Mst_SpecCustomField_CheckDB_CustomFieldNotFound = "ErridnInventory.Mst_SpecCustomField_CheckDB_CustomFieldNotFound"; //// //Mst_SpecCustomField_CheckDB_CustomFieldNotFound
		public const string Mst_SpecCustomField_CheckDB_CustomFieldExist = "ErridnInventory.Mst_SpecCustomField_CheckDB_CustomFieldExist"; //// //Mst_SpecCustomField_CheckDB_CustomFieldExist
		public const string Mst_SpecCustomField_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_SpecCustomField_CheckDB_FlagActiveNotMatched"; //// //Mst_SpecCustomField_CheckDB_FlagActiveNotMatched

        #endregion

        #region // Mst_Brand:
        // Mst_Brand_CheckDB:
        public const string Mst_Brand_CheckDB_BrandCodeNotFound = "ErridnInventory.Mst_Brand_CheckDB_BrandCodeNotFound"; //// //Mst_Brand_CheckDB_BrandCodeNotFound
        public const string Mst_Brand_CheckDB_BrandCodeExist = "ErridnInventory.Mst_Brand_CheckDB_BrandCodeExist"; //// //Mst_Brand_CheckDB_BrandCodeExist
        public const string Mst_Brand_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_Brand_CheckDB_FlagActiveNotMatched"; //// //Mst_Brand_CheckDB_FlagActiveNotMatched

        // Mst_Brand_Get:
        public const string Mst_Brand_Get = "ErridnInventory.Mst_Brand_Get"; //// //Mst_Brand_Get

        // Mst_Brand_Create:
        public const string Mst_Brand_Create = "ErridnInventory.Mst_Brand_Create"; //// //Mst_Brand_Create
        public const string Mst_Brand_Create_InvalidBrandCode = "ErridnInventory.Mst_Brand_Create_InvalidBrandCode"; //// //Mst_Brand_Create_InvalidBrandCode
        public const string Mst_Brand_Create_InvalidBrandName = "ErridnInventory.Mst_Brand_Create_InvalidBrandName"; //// //Mst_Brand_Create_InvalidBrandName

        // Mst_Brand_Update:
        public const string Mst_Brand_Update = "ErridnInventory.Mst_Brand_Update"; //// //Mst_Brand_Update
        public const string Mst_Brand_Update_InvalidBrandName = "ErridnInventory.Mst_Brand_Update_InvalidBrandName"; //// //Mst_Brand_Update_InvalidBrandName

        // Mst_Brand_Delete:
        public const string Mst_Brand_Delete = "ErridnInventory.Mst_Brand_Delete"; //// //Mst_Brand_Delete

        // WAS_Mst_Brand_Get:
        public const string WAS_Mst_Brand_Get = "ErridnInventory.WAS_Mst_Brand_Get"; //// //WAS_Mst_Brand_Get WAS_Mst_Brand_Create

        // WAS_Mst_Brand_Create:
        public const string WAS_Mst_Brand_Create = "ErridnInventory.WAS_Mst_Brand_Create"; //// //WAS_Mst_Brand_Create

        // WAS_Mst_Brand_Update:
        public const string WAS_Mst_Brand_Update = "ErridnInventory.WAS_Mst_Brand_Update"; //// //WAS_Mst_Brand_Update

        // WAS_Mst_Brand_Delete:
        public const string WAS_Mst_Brand_Delete = "ErridnInventory.WAS_Mst_Brand_Delete"; //// //WAS_Mst_Brand_Delete

        // Mst_Brand_CheckDB_ExistNetworkBrandCode:
        public const string Mst_Brand_CheckDB_NotExistNetworkBrandCode = "ErridnInventory.Mst_Brand_CheckDB_NotExistNetworkBrandCode"; //// //Mst_Brand_CheckDB_NotExistNetworkBrandCode

        // Mst_Brand_CheckDB_NetworkBrandCodeOfOrgParent:
        public const string Mst_Brand_CheckDB_NetworkBrandCodeNoEqualBrandCode = "ErridnInventory.Mst_Brand_CheckDB_NetworkBrandCodeNoEqualBrandCode"; //// //Mst_Brand_CheckDB_NetworkBrandCodeNoEqualBrandCode

        // Mst_Brand_SaveX_Input_InvoiceTblNotFound
        public const string Mst_Brand_SaveX_Input_BrandTblNotFound = "ErridnInventory.Mst_Brand_SaveX_Input_BrandTblNotFound"; //// // Mst_Brand_SaveX_Input_BrandTblNotFound

        public const string Mst_Brand_SaveX_Input_BrandTblInvalid = "ErridnInventory.Mst_Brand_SaveX_Input_BrandTblInvalid"; //// // Mst_Brand_SaveX_Input_BrandTblInvalid 

        public const string Mst_Brand_Save = "ErridnInventory.Mst_Brand_Save"; //// // Mst_Brand_Save
        public const string WAS_Mst_Brand_Save = "ErridnInventory.WAS_Mst_Brand_Save"; //// // WAS_Mst_Brand_Save
        #endregion

        #region // Mst_ProductGroup:
        // Mst_ProductGroup_CheckDB:
        public const string Mst_ProductGroup_CheckDB_ProductGroupNotFound = "ErridnInventory.Mst_ProductGroup_CheckDB_ProductGroupNotFound"; //// //Mst_ProductGroup_CheckDB_ProductGroupNotFound
        public const string Mst_ProductGroup_CheckDB_ProductGroupExist = "ErridnInventory.Mst_ProductGroup_CheckDB_ProductGroupExist"; //// //Mst_ProductGroup_CheckDB_ProductGroupExist
        public const string Mst_ProductGroup_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_ProductGroup_CheckDB_FlagActiveNotMatched"; //// //Mst_ProductGroup_CheckDB_FlagActiveNotMatched

        // Mst_ProductGroup_Get:
        public const string Mst_ProductGroup_Get = "ErridnInventory.Mst_ProductGroup_Get"; //// //Mst_ProductGroup_Get

        // WAS_Mst_ProductGroup_Get:
        public const string WAS_Mst_ProductGroup_Get = "ErridnInventory.WAS_Mst_ProductGroup_Get"; //// //WAS_Mst_ProductGroup_Get

        // WAS_Mst_ProductGroup_Create:
        public const string WAS_Mst_ProductGroup_Create = "ErridnInventory.WAS_Mst_ProductGroup_Create"; //// //WAS_Mst_ProductGroup_Create

        // Mst_ProductGroup_Create:
        public const string Mst_ProductGroup_Create = "ErridnInventory.Mst_ProductGroup_Create"; //// //Mst_ProductGroup_Create
        public const string Mst_ProductGroup_Create_InvalidProductGrpCode = "ErridnInventory.Mst_ProductGroup_Create_InvalidProductGrpCode"; //// //Mst_ProductGroup_Create_InvalidProductGrpCode
        public const string Mst_ProductGroup_Create_InvalidProductGrpName = "ErridnInventory.Mst_ProductGroup_Create_InvalidProductGrpName"; //// //Mst_ProductGroup_Create_InvalidProductGrpName

        // Mst_ProductGroup_Update:
        public const string Mst_ProductGroup_Update = "ErridnInventory.Mst_ProductGroup_Update"; //// //Mst_ProductGroup_Update

        // WAS_Mst_ProductGroup_Update:
        public const string WAS_Mst_ProductGroup_Update = "ErridnInventory.WAS_Mst_ProductGroup_Update"; //// //WAS_Mst_ProductGroup_Update

        // Mst_ProductGroup_UpdateX:
        public const string Mst_ProductGroup_UpdateX = "ErridnInventory.Mst_ProductGroup_UpdateX"; //// //Mst_ProductGroup_UpdateX
        public const string Mst_ProductGroup_UpdateX_InvalidProductGrpName = "ErridnInventory.Mst_ProductGroup_UpdateX_InvalidProductGrpName"; //// //Mst_ProductGroup_UpdateX_InvalidProductGrpName

        // WAS_Mst_ProductGroup_Delete:
        public const string WAS_Mst_ProductGroup_Delete = "ErridnInventory.WAS_Mst_ProductGroup_Delete"; //// //WAS_Mst_ProductGroup_Delete

        // Mst_ProductGroup_Delete:
        public const string Mst_ProductGroup_Delete = "ErridnInventory.Mst_ProductGroup_Delete"; //// //Mst_ProductGroup_Delete

        // Mst_Brand_SaveX_Input_InvoiceTblNotFound
        public const string Mst_ProductGroup_SaveX_Input_ProductGroupTblNotFound = "ErridnInventory.Mst_ProductGroup_SaveX_Input_ProductGroupTblNotFound"; //// // Mst_ProductGroup_SaveX_Input_ProductGroupTblNotFound

        public const string Mst_ProductGroup_SaveX_Input_ProductGroupTblInvalid = "ErridnInventory.Mst_ProductGroup_SaveX_Input_ProductGroupTblInvalid"; //// // Mst_ProductGroup_SaveX_Input_ProductGroupTblInvalid 

        public const string Mst_ProductGroup_Save = "ErridnInventory.Mst_ProductGroup_Save"; //// // Mst_ProductGroup_Save
        public const string WAS_Mst_ProductGroup_Save = "ErridnInventory.WAS_Mst_ProductGroup_Save"; //// // WAS_Mst_ProductGroup_Save
        #endregion

        #region // Prd_DynamicField:
        // Prd_DynamicField_CheckDB:
        public const string Prd_DynamicField_CheckDB_DynamicFieldNotFound = "ErridnInventory.Prd_DynamicField_CheckDB_DynamicFieldNotFound"; //// //Prd_DynamicField_CheckDB_DynamicFieldNotFound
        public const string Prd_DynamicField_CheckDB_DynamicFieldExist = "ErridnInventory.Prd_DynamicField_CheckDB_DynamicFieldExist"; //// //Prd_DynamicField_CheckDB_DynamicFieldExist
        public const string Prd_DynamicField_CheckDB_FlagActiveNotMatched = "ErridnInventory.Prd_DynamicField_CheckDB_FlagActiveNotMatched"; //// //Prd_DynamicField_CheckDB_FlagActiveNotMatched

        // Prd_DynamicField_Get:
        public const string Prd_DynamicField_Get = "ErridnInventory.Prd_DynamicField_Get"; //// //Prd_DynamicField_Get

        // WAS_Prd_DynamicField_Get:
        public const string WAS_Prd_DynamicField_Get = "ErridnInventory.WAS_Prd_DynamicField_Get"; //// //WAS_Prd_DynamicField_Get

        // WAS_Prd_DynamicField_Create:
        public const string WAS_Prd_DynamicField_Create = "ErridnInventory.WAS_Prd_DynamicField_Create"; //// //WAS_Prd_DynamicField_Create

        // Prd_DynamicField_Create:
        public const string Prd_DynamicField_Create = "ErridnInventory.Prd_DynamicField_Create"; //// //Prd_DynamicField_Create
        public const string Prd_DynamicField_Create_InvalidOrgID = "ErridnInventory.Prd_DynamicField_Create_InvalidOrgID"; //// //Prd_DynamicField_Create_InvalidOrgID
        public const string Prd_DynamicField_Create_InvalidDynamicFieldName = "ErridnInventory.Prd_DynamicField_Create_InvalidDynamicFieldName"; //// //Prd_DynamicField_Create_InvalidDynamicFieldName

        // Prd_DynamicField_Update:
        public const string Prd_DynamicField_Update = "ErridnInventory.Prd_DynamicField_Update"; //// //Prd_DynamicField_Update

        // WAS_Prd_DynamicField_Update:
        public const string WAS_Prd_DynamicField_Update = "ErridnInventory.WAS_Prd_DynamicField_Update"; //// //WAS_Prd_DynamicField_Update

        // Prd_DynamicField_UpdateX:
        public const string Prd_DynamicField_UpdateX = "ErridnInventory.Prd_DynamicField_UpdateX"; //// //Prd_DynamicField_UpdateX
        public const string Prd_DynamicField_UpdateX_InvalidDynamicFieldName = "ErridnInventory.Prd_DynamicField_UpdateX_InvalidDynamicFieldName"; //// //Prd_DynamicField_UpdateX_InvalidDynamicFieldName

        // WAS_Prd_DynamicField_Delete:
        public const string WAS_Prd_DynamicField_Delete = "ErridnInventory.WAS_Prd_DynamicField_Delete"; //// //WAS_Prd_DynamicField_Delete

        // Prd_DynamicField_Delete:
        public const string Prd_DynamicField_Delete = "ErridnInventory.Prd_DynamicField_Delete"; //// //Prd_DynamicField_Delete

        // Mst_Brand_SaveX_Input_InvoiceTblNotFound
        public const string Prd_DynamicField_SaveX_Input_DynamicFieldTblNotFound = "ErridnInventory.Prd_DynamicField_SaveX_Input_DynamicFieldTblNotFound"; //// // Prd_DynamicField_SaveX_Input_DynamicFieldTblNotFound

        public const string Prd_DynamicField_SaveX_Input_DynamicFieldTblInvalid = "ErridnInventory.Prd_DynamicField_SaveX_Input_DynamicFieldTblInvalid"; //// // Prd_DynamicField_SaveX_Input_DynamicFieldTblInvalid 

        public const string Prd_DynamicField_Save = "ErridnInventory.Prd_DynamicField_Save"; //// // Prd_DynamicField_Save
        public const string WAS_Prd_DynamicField_Save = "ErridnInventory.WAS_Prd_DynamicField_Save"; //// // WAS_Prd_DynamicField_Save
        #endregion

        #region // Mst_Attribute:
        public const string WAS_Mst_Attribute_Save = "ErridnInventory.WAS_Mst_Attribute_Save"; //// // WAS_Mst_Attribute_Save
        public const string Mst_Attribute_Save = "ErridnInventory.Mst_Attribute_Save"; //// // Mst_Attribute_Save
        public const string Mst_Attribute_SaveX_Input_AttributeTblNotFound = "ErridnInventory.Mst_Attribute_SaveX_Input_AttributeTblNotFound"; //// // Mst_Attribute_SaveX_Input_AttributeTblNotFound
        public const string Mst_Attribute_SaveX_Input_AttributeTblInvalid = "ErridnInventory.Mst_Attribute_SaveX_Input_AttributeTblInvalid"; //// // Mst_Attribute_SaveX_Input_AttributeTblInvalid
        #endregion

        #region // Common:
        // myCommon_CheckOrgParent:
        public const string myCommon_CheckNoOrgParent = "ErridnInventory.myCommon_CheckNoOrgParent"; //// //myCommon_CheckOrgParent
        #endregion

        #region // Mst_Area:
        // WAS_Mst_Area_Save:
        public const string WAS_Mst_Area_Save = "ErridnDMS.WAS_Mst_Area_Save"; //// //WAS_Mst_Area_Save 
        public const string Mst_Area_Save = "ErridnDMS.Mst_Area_Save"; //// //Mst_Area_Save
        public const string Mst_Area_SaveX_Input_BrandTblNotFound = "ErridnDMS.Mst_Area_SaveX_Input_BrandTblNotFound"; //// //Mst_Area_SaveX_Input_BrandTblNotFound
        public const string Mst_Area_SaveX_Input_BrandTblInvalid = "ErridnDMS.Mst_Area_SaveX_Input_BrandTblInvalid"; //// //Mst_Area_SaveX_Input_BrandTblInvalid
        // WAS_Mst_Area_Create:
        #endregion

        #region // WAS_Mst_CustomerInCustomerGroup_Get
        public const string WAS_Mst_CustomerInCustomerGroup_Get = "ErridnDMS.Mst_Area_CheckDB_AreaNotFound"; //// //Mst_Area_CheckDB_AreaNotFound
        public const string Mst_CustomerInCustomerGroup_CheckDB_AreaNotFound = "ErridnDMS.Mst_CustomerInCustomerGroup_CheckDB_AreaNotFound"; //// //Mst_CustomerInCustomerGroup_CheckDB_AreaNotFound
        public const string Mst_CustomerInCustomerGroup_CheckDB_AreaExist = "ErridnDMS.Mst_CustomerInCustomerGroup_CheckDB_AreaExist"; //// //Mst_CustomerInCustomerGroup_CheckDB_AreaExist
        public const string Mst_CustomerInCustomerGroup_CheckDB_FlagActiveNotMatched = "ErridnDMS.Mst_CustomerInCustomerGroup_CheckDB_FlagActiveNotMatched"; //// //Mst_CustomerInCustomerGroup_CheckDB_FlagActiveNotMatched
        public const string Mst_CustomerInCustomerGroup_Get = "ErridnDMS.Mst_CustomerInCustomerGroup_Get"; //// //Mst_CustomerInCustomerGroup_Get
        public const string Mst_CustomerInCustomerGroup_SaveX_Input_BrandTblNotFound = "ErridnDMS.Mst_CustomerInCustomerGroup_SaveX_Input_BrandTblNotFound"; //// //Mst_CustomerInCustomerGroup_SaveX_Input_BrandTblNotFound
        public const string Mst_CustomerInCustomerGroup_SaveX_Input_BrandTblInvalid = "ErridnDMS.Mst_CustomerInCustomerGroup_SaveX_Input_BrandTblInvalid"; //// //Mst_CustomerInCustomerGroup_SaveX_Input_BrandTblInvalid
        public const string Mst_CustomerInCustomerGroup_Save = "ErridnDMS.Mst_CustomerInCustomerGroup_Save"; //// //Mst_CustomerInCustomerGroup_Save
        public const string WAS_Mst_CustomerInCustomerGroup_Save = "ErridnDMS.WAS_Mst_CustomerInCustomerGroup_Save"; //// //WAS_Mst_CustomerInCustomerGroup_Save
        #endregion

        #region // Mst_Customer_Save:
        public const string Mst_CustomerType_Save = "ErridnDMS.Mst_CustomerType_Save"; //// //Mst_CustomerType_Save
        public const string Mst_CustomerType_Save_Input_Mst_CustomerTypeTblNotFound = "ErridnDMS.Mst_CustomerType_Save_Input_Mst_CustomerTypeTblNotFound"; //// //Mst_CustomerType_Save_Input_Mst_CustomerTypeTblNotFound
        public const string Mst_CustomerType_Update_Input_Mst_CustomerTypeImagesTblNotFound = "ErridnDMS.Mst_CustomerType_Update_Input_Mst_CustomerTypeImagesTblNotFound"; //// //Mst_CustomerType_Update_Input_Mst_CustomerTypeImagesTblNotFound
        public const string WAS_Mst_CustomerType_Save = "ErridnDMS.WAS_Mst_CustomerType_Save"; //// //WAS_Mst_CustomerType_Save
        #endregion

        #region // Mst_CustomerGroup:
        public const string Mst_CustomerGroup_Save = "ErridnDMS.Mst_CustomerGroup_Save"; //// //Mst_CustomerGroup_Save
        public const string Mst_CustomerGroup_Save_Input_Mst_CustomerGroupTblNotFound = "ErridnDMS.Mst_CustomerGroup_Save_Input_Mst_CustomerGroupTblNotFound"; //// //Mst_CustomerGroup_Save_Input_Mst_CustomerGroupTblNotFound
        public const string Mst_CustomerGroup_Update_Input_Mst_CustomerGroupImagesTblNotFound = "ErridnDMS.Mst_CustomerGroup_Update_Input_Mst_CustomerGroupImagesTblNotFound"; //// //Mst_CustomerGroup_Update_Input_Mst_CustomerGroupImagesTblNotFound
        public const string WAS_Mst_CustomerGroup_Save = "ErridnDMS.WAS_Mst_CustomerGroup_Save"; //// //WAS_Mst_CustomerGroup_Save
        #endregion

        #region // Mst_CustomerSource:
        public const string Mst_CustomerSource_Save = "ErridnDMS.Mst_CustomerSource_Save"; //// //Mst_CustomerSource_Save
        public const string Mst_CustomerSource_Save_Input_Mst_CustomerSourceTblNotFound = "ErridnDMS.Mst_CustomerSource_Save_Input_Mst_CustomerSourceTblNotFound"; //// //Mst_CustomerSource_Save_Input_Mst_CustomerSourceTblNotFound
        public const string Mst_CustomerSource_Update_Input_Mst_CustomerSourceImagesTblNotFound = "ErridnDMS.Mst_CustomerSource_Update_Input_Mst_CustomerSourceImagesTblNotFound"; //// //Mst_CustomerSource_Update_Input_Mst_CustomerSourceImagesTblNotFound
        public const string WAS_Mst_CustomerSource_Save = "ErridnDMS.WAS_Mst_CustomerSource_Save"; //// //WAS_Mst_CustomerSource_Save
        #endregion

        #region // Mst_CustomerInCustomerGroup:
        public const string Customer_DynamicField_SaveX_Input_TblNotFound = "ErridnDMS.Customer_DynamicField_SaveX_Input_TblNotFound"; //// //Customer_DynamicField_SaveX_Input_TblNotFound
        public const string Customer_DynamicField_SaveX_Input_TblInvalid = "ErridnDMS.Customer_DynamicField_SaveX_Input_TblInvalid"; //// //Customer_DynamicField_SaveX_Input_TblInvalid
        public const string Customer_DynamicField_Save = "ErridnDMS.Customer_DynamicField_Save"; //// //Customer_DynamicField_Save
        public const string WAS_Customer_DynamicField_Save = "ErridnDMS.WAS_Customer_DynamicField_Save"; //// //WAS_Customer_DynamicField_Save
        #endregion

        #region // Mst_Customer:
        public const string Mst_Customer_Save = "ErridnDMS.Mst_Customer_Save"; //// //Mst_Customer_Save
        public const string Mst_Customer_Save_Input_Mst_CustomerTblNotFound = "ErridnDMS.Mst_Customer_Save_Input_Mst_CustomerTblNotFound"; //// //Mst_Customer_Save_Input_Mst_CustomerTblNotFound
        public const string WAS_Mst_Customer_Save = "ErridnDMS.WAS_Mst_Customer_Save"; //// //WAS_Mst_Customer_Save
        #endregion

        #region // Mst_ColumnConfig:
        // Mst_ColumnConfig_CheckDB:
        public const string Mst_ColumnConfig_CheckDB_ColumnConfigNotFound = "ErridnInventory.Mst_ColumnConfig_CheckDB_ColumnConfigNotFound"; //// //Mst_ColumnConfig_CheckDB_ColumnConfigNotFound
        public const string Mst_ColumnConfig_CheckDB_ColumnConfigExist = "ErridnInventory.Mst_ColumnConfig_CheckDB_ColumnConfigExist"; //// //Mst_ColumnConfig_CheckDB_ColumnConfigExist
        public const string Mst_ColumnConfig_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_ColumnConfig_CheckDB_FlagActiveNotMatched"; //// //Mst_ColumnConfig_CheckDB_FlagActiveNotMatched

        // WAS_Mst_ColumnConfig_Get:
        public const string WAS_Mst_ColumnConfig_Get = "ErridnInventory.WAS_Mst_ColumnConfig_Get"; //// //WAS_Mst_ColumnConfig_Get
        public const string Mst_ColumnConfig_Get = "ErridnInventory.Mst_ColumnConfig_Get"; //// //Mst_ColumnConfig_Get

        // WAS_Mst_ColumnConfig_Update:
        public const string WAS_Mst_ColumnConfig_Update = "ErridnInventory.WAS_Mst_ColumnConfig_Update"; //// //WAS_Mst_ColumnConfig_Update
        public const string Mst_ColumnConfig_Update = "ErridnInventory.Mst_ColumnConfig_Update"; //// //Mst_ColumnConfig_Update
        public const string Mst_ColumnConfig_Update_InvalidTableName = "ErridnInventory.Mst_ColumnConfig_Update_InvalidTableName"; //// //Mst_ColumnConfig_Update_InvalidTableName
        public const string Mst_ColumnConfig_Update_InvalidColumnFormat = "ErridnInventory.Mst_ColumnConfig_Update_InvalidColumnFormat"; //// //Mst_ColumnConfig_Update_InvalidColumnFormat

        #endregion

        #region // Mst_ColumnConfigGroup:
        // Mst_ColumnConfigGroup_CheckDB:
        public const string Mst_ColumnConfigGroup_CheckDB_ColumnConfigGrpCodeNotFound = "ErridnInventory.Mst_ColumnConfigGroup_CheckDB_ColumnConfigGrpCodeNotFound"; //// //Mst_ColumnConfigGroup_CheckDB_ColumnConfigGrpCodeNotFound
        public const string Mst_ColumnConfigGroup_CheckDB_ColumnConfigGrpCodeExist = "ErridnInventory.Mst_ColumnConfigGroup_CheckDB_ColumnConfigGrpCodeExist"; //// //Mst_ColumnConfigGroup_CheckDB_ColumnConfigGrpCodeExist
        public const string Mst_ColumnConfigGroup_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_ColumnConfigGroup_CheckDB_FlagActiveNotMatched"; //// //Mst_ColumnConfigGroup_CheckDB_FlagActiveNotMatched

        // Mst_ColumnConfigGroup_Get:
        public const string WAS_Mst_ColumnConfigGroup_Get = "ErridnInventory.WAS_Mst_ColumnConfigGroup_Get"; //// //WAS_Mst_ColumnConfigGroup_Get
        public const string Mst_ColumnConfigGroup_Get = "ErridnInventory.Mst_ColumnConfigGroup_Get"; //// //Mst_ColumnConfigGroup_Get

        // Mst_ColumnConfigGroup_Create:
        public const string WAS_Mst_ColumnConfigGroup_Create = "ErridnInventory.WAS_Mst_ColumnConfigGroup_Create"; //// //WAS_Mst_ColumnConfigGroup_Create
        public const string Mst_ColumnConfigGroup_Create = "ErridnInventory.Mst_ColumnConfigGroup_Create"; //// //Mst_ColumnConfigGroup_Create
        public const string Mst_ColumnConfigGroup_Create_InvalidColumnConfigGrpCode = "ErridnInventory.Mst_ColumnConfigGroup_Create_InvalidColumnConfigGrpCode"; //// //Mst_ColumnConfigGroup_Create_InvalidColumnConfigGrpCode
        public const string Mst_ColumnConfigGroup_Create_Invalid_OrgID_NotNull = "ErridnInventory.Mst_ColumnConfigGroup_Create_Invalid_OrgID_NotNull"; //// //Mst_ColumnConfigGroup_Create_Invalid_OrgID_NotNull
        public const string Mst_ColumnConfigGroup_Create_Invalid_GrpFormat = "ErridnInventory.Mst_ColumnConfigGroup_Create_Invalid_GrpFormat"; //// //Mst_ColumnConfigGroup_Create_Invalid_GrpFormat

        // Mst_ColumnConfigGroup_Update:
        public const string WAS_Mst_ColumnConfigGroup_Update = "ErridnInventory.WAS_Mst_ColumnConfigGroup_Update"; //// //WAS_Mst_ColumnConfigGroup_Update
        public const string Mst_ColumnConfigGroup_Update = "ErridnInventory.Mst_ColumnConfigGroup_Update"; //// //Mst_ColumnConfigGroup_Update
        public const string Mst_ColumnConfigGroup_Update_Invalid_GrpFormat = "ErridnInventory.Mst_ColumnConfigGroup_Update_Invalid_GrpFormat"; //// //Mst_ColumnConfigGroup_Update_Invalid_GrpFormat

        // Mst_ColumnConfigGroup_Delete:
        public const string WAS_Mst_ColumnConfigGroup_Delete = "ErridnInventory.WAS_Mst_ColumnConfigGroup_Delete"; //// //WAS_Mst_ColumnConfigGroup_Delete
        public const string Mst_ColumnConfigGroup_Delete = "ErridnInventory.Mst_ColumnConfigGroup_Delete"; //// //Mst_ColumnConfigGroup_Delete

        #endregion

        #region // Mst_Sys_Config:
        // Mst_Sys_Config_CheckDB:
        public const string Mst_Sys_Config_CheckDB_SysConfigNotFound = "ErridnInventory.Mst_Sys_Config_CheckDB_SysConfigNotFound"; //// //Mst_Sys_Config_CheckDB_SysConfigNotFound
        public const string Mst_Sys_Config_CheckDB_SysConfigExist = "ErridnInventory.Mst_Sys_Config_CheckDB_SysConfigExist"; //// //Mst_Sys_Config_CheckDB_SysConfigExist
        public const string Mst_Sys_Config_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_Sys_Config_CheckDB_FlagActiveNotMatched"; //// //Mst_Sys_Config_CheckDB_FlagActiveNotMatched

        // Mst_Sys_Config_Update:
        public const string WAS_Mst_Sys_Config_Update = "ErridnInventory.WAS_Mst_Sys_Config_Update"; //// //WAS_Mst_Sys_Config_Update
        public const string Mst_Sys_Config_Update = "ErridnInventory.Mst_Sys_Config_Update"; //// //Mst_Sys_Config_Update
        public const string Mst_Sys_Config_Update_Input_Mst_Sys_ConfigTblNotFound = "ErridnInventory.Mst_Sys_Config_Update_Input_Mst_Sys_ConfigTblNotFound"; //// //Mst_Sys_Config_Update_Input_Mst_Sys_ConfigTblNotFound
        public const string Mst_Sys_Config_Update_Input_Mst_Sys_ConfigTblInvalid = "ErridnInventory.Mst_Sys_Config_Update_Input_Mst_Sys_ConfigTblInvalid"; //// //Mst_Sys_Config_Update_Input_Mst_Sys_ConfigTblInvalid

        // Mst_Sys_Config_Get:
        public const string WAS_Mst_Sys_Config_Get = "ErridnInventory.WAS_Mst_Sys_Config_Get"; //// //WAS_Mst_Sys_Config_Get
        public const string Mst_Sys_Config_Get = "ErridnInventory.Mst_Sys_Config_Get"; //// //Mst_Sys_Config_Get

        #endregion
    }
}
