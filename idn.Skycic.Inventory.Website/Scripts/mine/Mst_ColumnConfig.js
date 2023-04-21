var objMst_ColumnConfig = new function () {
    this.searchColumnConfig = function (myArray, objSearch) {
        for (var i = 0; i < myArray.length; i++) {
            var objCur = myArray[i];
            if (objCur.TableName === objSearch.TableName && objCur.ColumnName === objSearch.ColumnName) {
                return myArray[i];
            }
        }
    };
    this.returnValueColumnFormat_V1 = function (myArray, tableName, columnName) {
        var columnformat = 0;
        var objSearch = {
            TableName: tableName,
            ColumnName: columnName,
        };
        var objColumnConfig = this.searchColumnConfig(myArray, objSearch)
        if (objColumnConfig != null) {
            var strColumnFormat = commonUtils.returnValue(objColumnConfig.ColumnFormat);
            if (!commonUtils.isNullOrEmpty(strColumnFormat)) {
                columnformat = parseInt(strColumnFormat);
            }
        }
        return columnformat;
    }
    this.returnValueColumnFormat_V2 = function (tableName, columnName) {
        var columnformat = 0;
        var listMst_ColumnConfig = commonInventory.ListMst_ColumnConfig;
        if (listMst_ColumnConfig === undefined || listMst_ColumnConfig === null) {
            listMst_ColumnConfig = [];
        }
        var objSearch = {
            TableName: tableName,
            ColumnName: columnName,
        };
        var objColumnConfig = this.searchColumnConfig(listMst_ColumnConfig, objSearch)
        if (objColumnConfig != null) {
            var strColumnFormat = commonUtils.returnValue(objColumnConfig.ColumnFormat);
            if (!commonUtils.isNullOrEmpty(strColumnFormat)) {
                columnformat = parseInt(strColumnFormat);
            }
        }
        return columnformat;
    }
    this.returnValueColumnFormat = function (myArray, objSearch) {
        var columnformat = 0;
        var objColumnConfig = this.searchColumnConfig(myArray, objSearch)
        if (objColumnConfig != null) {
            var strColumnFormat = commonUtils.returnValue(objColumnConfig.ColumnFormat);
            if (!commonUtils.isNullOrEmpty(strColumnFormat)) {
                columnformat = parseInt(strColumnFormat);
            }
        }
        return columnformat;
    }
    this.jqueryFormatNumber_Class = function (idorclass, columnformat) {
        if ($(idorclass).length) {
            debugger;
            $('.' + idorclass).number(true, columnformat);
        }
    }
    this.jqueryFormatNumber_Id = function (idorclass, columnformat) {
        if ($(idorclass).length) {
            debugger;
            $('#' + idorclass).number(true, columnformat);
        }
    }
    this.jqueryFormatNumber_ListColumnConfig_Class = function (myArray, objSearch, idorclass) {
        debugger;
        var columnformat = this.returnValueColumnFormat(myArray, objSearch);
        this.jqueryFormatNumber_Class(idorclass, columnformat);
    }
    this.jqueryFormatNumber_ListColumnConfig_Id = function (myArray, objSearch, idorclass) {
        debugger;
        var columnformat = this.returnValueColumnFormat(myArray, objSearch);
        this.jqueryFormatNumber_Id(idorclass, columnformat);
    }
};