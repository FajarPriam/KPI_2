var kpi;
var tabelMapping;
var excelKPI_Off;
var excelKPI_Cash;
var excelKPI_DS;
var excelKPI_OffUpdate;
var excelKPI_CashUpdate;
var excelKPI_DSUpdate;
var arrayAddKPI = [];
var tabelKPIbySection;
var arrayAddKPI_Cash = [];
var tabelKPIbySection_Cash;
var tabelLevel;

var tableON = $("#tbl_kpi").DataTable({
    ajax: {
        url: $("#hd_path").val() + "GetTabelKPION",
        type: "GET",
        datatype: "json",
        dataSrc: "data"
    },

    order: [],
    columns: [
        {
            data: null,
            render: function (data, type, row) {
                var html = `<div class="btn-group mb-3" role="group" aria-label="Basic example">
                                        <button type="button" class="btn btn-info" onclick="GetEditON('${data.CODE_SECTION}')"><i class="bi bi-pencil"></i></button>
                                    </div>`;
                if (data.CODE_SECTION == null) {
                    return null;
                } else {
                    return html;
                }
            }
        },
        { data: "CODE_SECTION" },
        { data: "CODE_DESC" },
        {
            data: null,
            render: function (data, type, row) {
                var html = `<div class="btn-group mb-3" role="group" aria-label="Basic example">
                                        <button type="button" class="btn btn-info" onclick="AddLevelKPI('${data.KPI_CODE}','${data.KPI_ITEM}')"><i class="bi bi-plus-lg"></i></button>
                                    </div>`;
                return html;
            }
        },
        { data: "KPI_CODE" },
        { data: "KPI_ITEM" },
        { data: "DESKRIPSI" },
        //{ data: "BOBOT" },
        {
            data: null,
            render: function (data, type, row, meta) {


                return (data.BOBOT * 100) + '%';
            }
        },
        { data: "TARGET" },
        

    ],
});

var tableCash = $("#tbl_kpi_cash").DataTable({
    ajax: {
        url: $("#hd_path").val() + "GetTabelKPICash",
        type: "GET",
        datatype: "json",
        dataSrc: "data"
    },

    order: [],
    columns: [
        {
            data: null,
            render: function (data, type, row) {
                var html = `<div class="btn-group mb-3" role="group" aria-label="Basic example">
                                        <button type="button" class="btn btn-info" onclick="GetEditCash('${data.CODE_SECTION}')"><i class="bi bi-pencil"></i></button>
                                    </div>`;
                if (data.CODE_SECTION == null) {
                    return null;
                } else {
                    return html;
                }
            }
        },
        { data: "CODE_SECTION" },
        { data: "CODE_DESC" },
        {
            data: null,
            render: function (data, type, row) {
                var html = `<div class="btn-group mb-3" role="group" aria-label="Basic example">
                                        <button type="button" class="btn btn-info" onclick="AddLevelKPI_Cash('${data.KPI_CODE}','${data.KPI_ITEM}')"><i class="bi bi-plus-lg"></i></button>
                                    </div>`;
                return html;
            }
        },
        { data: "KPI_CODE" },
        { data: "KPI_ITEM" },
        { data: "DESKRIPSI" },
        //{ data: "BOBOT" },
        {
            data: null,
            render: function (data, type, row, meta) {


                return (data.BOBOT * 100) + '%';
            }
        },
        { data: "TARGET" },


    ],
});

var tableMasterDS = $("#tbl_kpi_MasterDS").DataTable({
    ajax: {
        url: $("#hd_path").val() + "GetItemDS",
        type: "GET",
        datatype: "json",
        dataSrc: "data"
    },
    //scrollX: true,
    autoWidth: false,
    //responsive: false,
    columns: [
        {
            data: "KPI_DS_ID",
            render: function (data, type, row) {
                var html = `<div class="btn-group mb-3" role="group" aria-label="Basic example">
                                        <button type="button" class="btn btn-info" value="${row.KPI_DS_ID}" data-M-ds="${row.KPI_DS_DESC}" onclick="GetEditMasterDS(this)"><i class="bi bi-pencil"></i></button>
                                        <button type="button" class="btn btn-danger" value="${row.KPI_DS_ID}" onclick="DeleteKPIDS(this.value)"><i class="bi bi-trash"></i></button>
                                    </div>`;
                return html;
            }
        },
        { data: "KPI_DS_ID" },
        { data: "KPI_DS_DESC" },

    ],
});



var ExcelToJSON = function () {
    this.parseExcel = function (file) {
        var reader = new FileReader();
        reader.onload = function (e) {
            var data = e.target.result;
            var workbook = XLSX.read(data, {
                type: 'binary'
            });

            for (var i = 0; i < workbook.SheetNames.length; i++) {
                if (workbook.SheetNames[i] == "Data Master KPI Officer") {
                    var sheet = workbook.SheetNames[i];
                    var XL_row_object = XLSX.utils.sheet_to_json(workbook.Sheets[sheet], { defval: "" });
                    var json_object = JSON.stringify(XL_row_object);
                    excelKPI_Off = JSON.parse(json_object);
                    for (let i = 0; i < excelKPI_Off.length; i++) {
                        delete excelKPI_Off[i].EDIT_BY;
                    }
                    console.log(excelKPI_Off);
                    //break; // Exit the loop once the sheet is found
                } else if (workbook.SheetNames[i] == "Data Master KPI Cashier") {
                    var sheet = workbook.SheetNames[i];
                    var XL_row_object = XLSX.utils.sheet_to_json(workbook.Sheets[sheet], { defval: "" });
                    var json_object = JSON.stringify(XL_row_object);
                    excelKPI_Cash = JSON.parse(json_object);
                    for (let i = 0; i < excelKPI_Cash.length; i++) {
                        delete excelKPI_Cash[i].EDIT_BY;
                    }
                    console.log(excelKPI_Cash);
                    //break; // Exit the loop once the sheet is found
                } else if (workbook.SheetNames[i] == "Data Master KPI Dept Sect") {
                    var sheet = workbook.SheetNames[i];
                    var XL_row_object = XLSX.utils.sheet_to_json(workbook.Sheets[sheet], { defval: "" });
                    var json_object = JSON.stringify(XL_row_object);
                    excelKPI_DS = JSON.parse(json_object);
                    for (let i = 0; i < excelKPI_DS.length; i++) {
                        delete excelKPI_DS[i].EDIT_BY;
                    }
                    console.log(excelKPI_DS);
                    //break; // Exit the loop once the sheet is found
                }
            }

        };

        reader.onerror = function (ex) {
            console.log(ex);
        };

        reader.readAsBinaryString(file);
    };
};

function handleFileSelect(evt) {
    var files = evt.target.files; // FileList object
    var xl2json = new ExcelToJSON();
    xl2json.parseExcel(files[0]);
}

var ExcelToJSON_Update = function () {
    this.parseExcel = function (file) {
        var reader = new FileReader();
        reader.onload = function (e) {
            var data = e.target.result;
            var workbook = XLSX.read(data, {
                type: 'binary'
            });

            for (var i = 0; i < workbook.SheetNames.length; i++) {
                if (workbook.SheetNames[i] == "Data Master KPI") {
                    var sheet = workbook.SheetNames[i];
                    var XL_row_object = XLSX.utils.sheet_to_json(workbook.Sheets[sheet], { defval: "" });
                    var json_object = JSON.stringify(XL_row_object);
                    excelKPI_OffUpdate = JSON.parse(json_object);
                    for (let i = 0; i < excelKPI_OffUpdate.length; i++) {
                        delete excelKPI_OffUpdate[i].EDIT_BY;
                    }
                    console.log(excelKPI_OffUpdate);
                    //break; // Exit the loop once the sheet is found
                } else if (workbook.SheetNames[i] == "Data Master Cashier") {

                    var sheet = workbook.SheetNames[i];
                    var XL_row_object = XLSX.utils.sheet_to_json(workbook.Sheets[sheet], { defval: "" });
                    var json_object = JSON.stringify(XL_row_object);
                    excelKPI_CashUpdate = JSON.parse(json_object);
                    for (let i = 0; i < excelKPI_CashUpdate.length; i++) {
                        delete excelKPI_CashUpdate[i].EDIT_BY;
                    }
                    console.log(excelKPI_CashUpdate);
                    //break; // Exit the loop once the sheet is found
                } else if (workbook.SheetNames[i] == "Data Master KPI Dept Sect") {
                    var sheet = workbook.SheetNames[i];
                    var XL_row_object = XLSX.utils.sheet_to_json(workbook.Sheets[sheet], { defval: "" });
                    var json_object = JSON.stringify(XL_row_object);
                    excelKPI_DSUpdate = JSON.parse(json_object);
                    for (let i = 0; i < excelKPI_DSUpdate.length; i++) {
                        delete excelKPI_DSUpdate[i].EDIT_BY;
                    }
                    console.log(excelKPI_DSUpdate);
                    //break; // Exit the loop once the sheet is found
                }
            }

        };

        reader.onerror = function (ex) {
            console.log(ex);
        };

        reader.readAsBinaryString(file);
    };
};

var ExcelToJSON_CashUpdate = function () {
    this.parseExcel = function (file) {
        var reader = new FileReader();
        reader.onload = function (e) {
            var data = e.target.result;
            var workbook = XLSX.read(data, {
                type: 'binary'
            });

            for (var i = 0; i < workbook.SheetNames.length; i++) {
                if (workbook.SheetNames[i] == "Data Master KPI") {
                    var sheet = workbook.SheetNames[i];
                    var XL_row_object = XLSX.utils.sheet_to_json(workbook.Sheets[sheet], { defval: "" });
                    var json_object = JSON.stringify(XL_row_object);
                    excelKPI_OffUpdate = JSON.parse(json_object);
                    for (let i = 0; i < excelKPI_OffUpdate.length; i++) {
                        delete excelKPI_OffUpdate[i].EDIT_BY;
                    }
                    console.log(excelKPI_OffUpdate);
                    //break; // Exit the loop once the sheet is found
                } else if (workbook.SheetNames[i] == "Data Master Cashier") {

                    var sheet = workbook.SheetNames[i];
                    var XL_row_object = XLSX.utils.sheet_to_json(workbook.Sheets[sheet], { defval: "" });
                    var json_object = JSON.stringify(XL_row_object);
                    excelKPI_CashUpdate = JSON.parse(json_object);
                    for (let i = 0; i < excelKPI_CashUpdate.length; i++) {
                        delete excelKPI_CashUpdate[i].EDIT_BY;
                    }
                    console.log(excelKPI_CashUpdate);
                    //break; // Exit the loop once the sheet is found
                } else if (workbook.SheetNames[i] == "Data Master KPI Dept Sect") {
                    var sheet = workbook.SheetNames[i];
                    var XL_row_object = XLSX.utils.sheet_to_json(workbook.Sheets[sheet], { defval: "" });
                    var json_object = JSON.stringify(XL_row_object);
                    excelKPI_DSUpdate = JSON.parse(json_object);
                    for (let i = 0; i < excelKPI_DSUpdate.length; i++) {
                        delete excelKPI_DSUpdate[i].EDIT_BY;
                    }
                    console.log(excelKPI_DSUpdate);
                    //break; // Exit the loop once the sheet is found
                }
            }

        };

        reader.onerror = function (ex) {
            console.log(ex);
        };

        reader.readAsBinaryString(file);
    };
};

var ExcelToJSON_DSUpdate = function () {
    this.parseExcel = function (file) {
        var reader = new FileReader();
        reader.onload = function (e) {
            var data = e.target.result;
            var workbook = XLSX.read(data, {
                type: 'binary'
            });

            for (var i = 0; i < workbook.SheetNames.length; i++) {
                if (workbook.SheetNames[i] == "Data Master KPI") {
                    var sheet = workbook.SheetNames[i];
                    var XL_row_object = XLSX.utils.sheet_to_json(workbook.Sheets[sheet], { defval: "" });
                    var json_object = JSON.stringify(XL_row_object);
                    excelKPI_OffUpdate = JSON.parse(json_object);
                    for (let i = 0; i < excelKPI_OffUpdate.length; i++) {
                        delete excelKPI_OffUpdate[i].EDIT_BY;
                    }
                    console.log(excelKPI_OffUpdate);
                    //break; // Exit the loop once the sheet is found
                } else if (workbook.SheetNames[i] == "Data Master Cashier") {

                    var sheet = workbook.SheetNames[i];
                    var XL_row_object = XLSX.utils.sheet_to_json(workbook.Sheets[sheet], { defval: "" });
                    var json_object = JSON.stringify(XL_row_object);
                    excelKPI_CashUpdate = JSON.parse(json_object);
                    for (let i = 0; i < excelKPI_CashUpdate.length; i++) {
                        delete excelKPI_CashUpdate[i].EDIT_BY;
                    }
                    console.log(excelKPI_CashUpdate);
                    //break; // Exit the loop once the sheet is found
                } else if (workbook.SheetNames[i] == "Data Master KPI Dept Sect") {
                    var sheet = workbook.SheetNames[i];
                    var XL_row_object = XLSX.utils.sheet_to_json(workbook.Sheets[sheet], { defval: "" });
                    var json_object = JSON.stringify(XL_row_object);
                    excelKPI_DSUpdate = JSON.parse(json_object);
                    for (let i = 0; i < excelKPI_DSUpdate.length; i++) {
                        delete excelKPI_DSUpdate[i].EDIT_BY;
                    }
                    console.log(excelKPI_DSUpdate);
                    //break; // Exit the loop once the sheet is found
                }
            }

        };

        reader.onerror = function (ex) {
            console.log(ex);
        };

        reader.readAsBinaryString(file);
    };
};

function handleFileSelect_Update(evt) {
    var files = evt.target.files; // FileList object
    var xl2json = new ExcelToJSON_Update();
    xl2json.parseExcel(files[0]);
}

function handleFileSelect_CashUpdate(evt) {
    var files = evt.target.files; // FileList object
    var xl2json = new ExcelToJSON_CashUpdate();
    xl2json.parseExcel(files[0]);
}

function handleFileSelect_DSUpdate(evt) {
    var files = evt.target.files; // FileList object
    var xl2json = new ExcelToJSON_DSUpdate();
    xl2json.parseExcel(files[0]);
}


document.getElementById('excel_kpi').addEventListener('change', handleFileSelect, false);
document.getElementById('excelUp_kpi').addEventListener('change', handleFileSelect_Update, false);
document.getElementById('excelUp_kpiCash').addEventListener('change', handleFileSelect_CashUpdate, false);
document.getElementById('excelUp_kpiDS').addEventListener('change', handleFileSelect_DSUpdate, false);

$(document).ready(function () {
    $('#modalAddON').on('hidden.bs.modal', function () {
        arrayAddKPI = [];
        console.log('hilang');
        $("#tbl_kpiSection tbody").empty();
    });

    $('#modalAddCash').on('hidden.bs.modal', function () {
        arrayAddKPI = [];
        console.log('hilang');
        $("#tbl_kpiCash tbody").empty();
    });

    $('#modalUpdateExcel').on('hidden.bs.modal', function () {
       
        console.log('hilang update');
    });

    $('#modalUpdateExcel_Cash').on('hidden.bs.modal', function () {

        console.log('hilang update');
    });

    $("#btn_closeEditKPI").click(function () {
        tableON.ajax.reload();
    });

    $("#btn_closeEditKPI_Cash").click(function () {
        tableON.ajax.reload();
    });


    //getDataSection();


    $("#formUploadItem").submit(function (e) {
        e.preventDefault(); // Prevent the default form submission
        console.log({
            clsTemplates: excelKPI_Off,
            clsTemplatesCash: excelKPI_Cash,
            clsTemplateDs: excelKPI_DS
        });
        console.log('masuk data');
        $.ajax({
            url: $("#hd_path").val() + "Create_ExcelKPI",
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            data: JSON.stringify({
                clsTemplates: excelKPI_Off,
                clsTemplatesCash: excelKPI_Cash,
                clsTemplateDs: excelKPI_DS
            }),
            success: function (data) {
                if (data.Remarks == true) {
                    Swal.fire({
                        icon: "success",
                        title: "Success!",
                        text: "Form submitted successfully."
                    });
                    $("#formUploadItem")[0].reset();
                    tableON.ajax.reload();
                    tableCash.ajax.reload();
                    tableMasterDS.ajax.reload();
                } if (data.Remarks == false) {
                    Swal.fire(
                        'Can\'t be Added!',
                        'Message : ' + data.Message,
                        'error'
                    );
                }
            },
            error: function () {
                Swal.fire({
                    icon: 'error',
                    title: 'Failed!',
                    text: 'Something went wrong!'
                });
            },
        });


    });

    $("#formUpdateItem_Cash").submit(function (e) {
        e.preventDefault(); // Prevent the default form submission


        for (i = 0; i < excelKPI_CashUpdate.length; i++) {
            excelKPI_CashUpdate[i].EDIT_BY = $('#hd_userLogin').val();
        }

        console.log({
            clsUpdateKPI: excelKPI_OffUpdate,
            clsUpdateCashier: excelKPI_CashUpdate,
            clsUpdateDS: excelKPI_DSUpdate
        });
        console.log('masuk data');
        //console.log(clsUpdateCash);
        $.ajax({
            url: $("#hd_path").val() + "Update_ExcelCash",
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            data: JSON.stringify({
                clsUpdateKPI: excelKPI_OffUpdate,
                clsUpdateCashier: excelKPI_CashUpdate,
                clsUpdateDS: excelKPI_DSUpdate
            }),
            success: function (data) {
                $('#modalUpdateExcel_Cash').modal('hide');
                if (data.Remarks == true) {
                    Swal.fire({
                        icon: "success",
                        title: "Success!",
                        text: "Form submitted successfully."
                    });
                    $("#formUpdateItem_Cash")[0].reset();
                    //tableON.ajax.reload();
                    tableCash.ajax.reload();
                    //tableMasterDS.ajax.reload();
                } if (data.Remarks == false) {
                    Swal.fire(
                        'Can\'t be Added!',
                        'Message : ' + data.Message,
                        'error'
                    );
                }
            },
            error: function () {
                Swal.fire({
                    icon: 'error',
                    title: 'Failed!',
                    text: 'Something went wrong!'
                });
            },
        });

    });

    $("#formUpdateItem").submit(function (e) {
        e.preventDefault(); // Prevent the default form submission
        for (i = 0; i < excelKPI_OffUpdate.length; i++) {
            excelKPI_OffUpdate[i].EDIT_BY = $('#hd_userLogin').val();
        }

        console.log({
            clsUpdateKPI: excelKPI_OffUpdate,
            clsUpdateCash: excelKPI_CashUpdate,
            clsUpdateDS: excelKPI_DSUpdate
        });
        console.log('masuk data');
        $.ajax({
            url: $("#hd_path").val() + "Update_ExcelKPI",
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            data: JSON.stringify({
                clsUpdateKPI: excelKPI_OffUpdate,
                clsUpdateCash: excelKPI_CashUpdate,
                clsUpdateDS: excelKPI_DSUpdate
            }),
            success: function (data) {
                $('#modalUpdateExcel').modal('hide');
                if (data.Remarks == true) {
                    Swal.fire({
                        icon: "success",
                        title: "Success!",
                        text: "Form submitted successfully."
                    });
                    $("#formUpdateItem")[0].reset();
                    tableON.ajax.reload();
                    //tableCash.ajax.reload();
                    //tableMasterDS.ajax.reload();
                } if (data.Remarks == false) {
                    Swal.fire(
                        'Can\'t be Added!',
                        'Message : ' + data.Message,
                        'error'
                    );
                }
            },
            error: function () {
                Swal.fire({
                    icon: 'error',
                    title: 'Failed!',
                    text: 'Something went wrong!'
                });
            },
        });

    });

    $("#formUpdateItem_DS").submit(function (e) {
        e.preventDefault(); // Prevent the default form submission
        for (i = 0; i < excelKPI_DSUpdate.length; i++) {
            excelKPI_DSUpdate[i].EDIT_BY = $('#hd_userLogin').val();
        }

        console.log({
            clsUpdateKPI: excelKPI_OffUpdate,
            clsUpdateCash: excelKPI_CashUpdate,
            clsUpdateDS: excelKPI_DSUpdate
        });
        console.log('masuk data');
        $.ajax({
            url: $("#hd_path").val() + "Update_ExcelDS",
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            data: JSON.stringify({
                clsUpdateKPI: excelKPI_OffUpdate,
                clsUpdateCash: excelKPI_CashUpdate,
                clsUpdateDS: excelKPI_DSUpdate
            }),
            success: function (data) {
                $('#modalUpdateExcel_DS').modal('hide');
                if (data.Remarks == true) {
                    Swal.fire({
                        icon: "success",
                        title: "Success!",
                        text: "Form submitted successfully."
                    });
                    $("#formUpdateItem_DS")[0].reset();
                    //tableON.ajax.reload();
                    //tableCash.ajax.reload();
                    tableMasterDS.ajax.reload();
                } if (data.Remarks == false) {
                    Swal.fire(
                        'Can\'t be Added!',
                        'Message : ' + data.Message,
                        'error'
                    );
                }
            },
            error: function () {
                Swal.fire({
                    icon: 'error',
                    title: 'Failed!',
                    text: 'Something went wrong!'
                });
            },
        });

    });

    $("#FormDS").submit(function (e) {
        e.preventDefault(); // Prevent the default form submission

        // Perform AJAX form submission
        $.ajax({
            url: $(this).attr("action"),
            type: "POST",
            data: $(this).serialize(),
            success: function (data) {
                $('#modalAddOF').modal('hide');
                if (data.Status === true) {
                    // Call SweetAlert2 when Status is true
                    Swal.fire({
                        icon: "success",
                        title: "Success!",
                        text: "Form submitted successfully."
                    });
                    tableDS.ajax.reload();
                } else {
                    // Handle the case when Status is false
                    Swal.fire({
                        icon: "error",
                        title: "Error!",
                        text: "An error occurred while processing the form."
                    });
                }
            },
            error: function () {
                // Handle AJAX error
                Swal.fire({
                    icon: "error",
                    title: "Error!",
                    text: "An error occurred while submitting the form."
                });
            }
        });
    });

    $("#FormOF").submit(function (e) {
        e.preventDefault(); // Prevent the default form submission

        // Perform AJAX form submission
        $.ajax({
            url: $(this).attr("action"),
            type: "POST",
            data: $(this).serialize(),
            success: function (data) {
                $('#modalAddOF').modal('hide');
                if (data.Status === true) {
                    // Call SweetAlert2 when Status is true
                    Swal.fire({
                        icon: "success",
                        title: "Success!",
                        text: "Form submitted successfully."
                    });
                    tableOF.ajax.reload();
                } else {
                    // Handle the case when Status is false
                    Swal.fire({
                        icon: "error",
                        title: "Error!",
                        text: "An error occurred while processing the form."
                    });
                }
            },
            error: function () {
                // Handle AJAX error
                Swal.fire({
                    icon: "error",
                    title: "Error!",
                    text: "An error occurred while submitting the form."
                });
            }
        });
    });

    $("#FormKPI_MasterDE").submit(function (e) {
        e.preventDefault(); // Prevent the default form submission

        console.log($(this).serialize())

        // Perform AJAX form submission
        $.ajax({
            url: $(this).attr("action"),
            type: "POST",
            data: $(this).serialize(),
            success: function (data) {
                $('#modalAdd_MasterDS').modal('hide');
                if (data.Status === true) {
                    // Call SweetAlert2 when Status is true
                    Swal.fire({
                        icon: "success",
                        title: "Success!",
                        text: "Form submitted successfully."
                    });
                    tableMasterDS.ajax.reload();
                } else {
                    // Handle the case when Status is false
                    Swal.fire({
                        icon: "error",
                        title: "Error!",
                        text: "An error occurred while processing the form."
                    });
                }
            },
            error: function () {
                // Handle AJAX error
                Swal.fire({
                    icon: "error",
                    title: "Error!",
                    text: "An error occurred while submitting the form."
                });
            }
        });
    });

    $("#FormKPI").submit(function (e) {
        e.preventDefault(); // Prevent the default form submission
        
        //var objectKPI_Section = {}
        var arrayKPI_Section = [];
        var jumlah = 0;
        console.log($('#dsItem').val());
       

        if ($('#dsItem').val() != "") {

            for (i = 1; i <= arrayAddKPI.length; i++) {
                jumlah += parseInt($(`#bobot_Section_${i}`).val(), 10);
                var objectKPI_Section = {
                    CODE_SECTION: $('#dsItem').val(),
                    KPI_ITEM: $(`#kpiItem_Section_${i}`).val(),
                    DESKRIPSI: $(`#desc_Section_${i}`).val(),
                    BOBOT: isNaN(parseInt($(`#bobot_Section_${i}`).val(), 10)) ? 0 : parseInt($(`#bobot_Section_${i}`).val(), 10),
                    TARGET: $(`#plan_Section_${i}`).val(),
                    EDIT_BY: $('#editBy_ON').val()
                }
                arrayKPI_Section.push(objectKPI_Section)
            }

            console.log(jumlah);

            if (jumlah == 100) {
                // Perform AJAX form submission
                $.ajax({
                    url: $(this).attr("action"),
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    data: JSON.stringify(arrayKPI_Section),
                    type: "POST",
                    success: function (data) {
                        $('#modalAddON').modal('hide');
                        if (data.Status === true) {
                            // Call SweetAlert2 when Status is true
                            Swal.fire({
                                icon: "success",
                                title: "Success!",
                                text: "Form submitted successfully."
                            });
                            tableON.ajax.reload();
                        } else {
                            // Handle the case when Status is false
                            Swal.fire({
                                icon: "error",
                                title: "Error!",
                                text: "Message" + data.Message
                            });
                        }
                    },
                    error: function () {
                        // Handle AJAX error
                        Swal.fire({
                            icon: "error",
                            title: "Error!",
                            text: "An error occurred while submitting the form."
                        });
                    }
                });
            } else {
                Swal.fire({
                    icon: "error",
                    title: "Error!",
                    text: "Jumlah Bobot Tidak 100%"
                });
            }


        }if ($('#dsItem').val() == "") {
            Swal.fire({
                icon: "error",
                title: "Error!",
                text: "Code Section Harus di isi !"
            });
        }
    });

    $("#FormKPI_Edit").submit(function (e) {
        e.preventDefault(); // Prevent the default form submission
        selectElement = document.querySelector('#dsItem_Edit');
        output = selectElement.options[selectElement.selectedIndex].value;
        console.log(output);

        var arrayKPIEdit_Section = [];
        var jumlahEdit = 0;
        var id_kpi = tabelKPIbySection.column(0).data();
        var kode_kpi = tabelKPIbySection.column(2).data();
        console.log(kode_kpi);
        for (s = 0; s < id_kpi.length; s++) {
            jumlahEdit += parseInt($(`#editBobotKPI_${id_kpi[s]}`).val(), 10);
            var objectKPIEdit_Section = {
                CODE_SECTION : output,
                KPI_CODE: kode_kpi[s],
                KPI_ITEM: $(`#editItemKPI_${id_kpi[s]}`).val(),
                DESKRIPSI: $(`#editDescKPI_${id_kpi[s]}`).val(),
                BOBOT: isNaN(parseInt($(`#editBobotKPI_${id_kpi[s]}`).val(), 10)) ? 0 : parseInt($(`#editBobotKPI_${id_kpi[s]}`).val(), 10),
                TARGET: $(`#editPlanKPI_${id_kpi[s]}`).val(),
                EDIT_BY: $('#editBy_ON_edit').val()
            }
            arrayKPIEdit_Section.push(objectKPIEdit_Section);
        }
        console.log(jumlahEdit);
        console.log(arrayKPIEdit_Section)

        if (jumlahEdit == 100) {
            // Perform AJAX form submission
            $.ajax({
                url: $(this).attr("action"),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                data: JSON.stringify(arrayKPIEdit_Section),
                type: "POST",
                success: function (data) {
                    $('#modalEditON').modal('hide');
                    if (data.Status === true) {
                        // Call SweetAlert2 when Status is true
                        Swal.fire({
                            icon: "success",
                            title: "Success!",
                            text: "Form submitted successfully."
                        });
                        tableON.ajax.reload();
                    } else {
                        // Handle the case when Status is false
                        Swal.fire({
                            icon: "error",
                            title: "Error!",
                            text: "Message" + data.Message
                        });
                    }
                },
                error: function () {
                    // Handle AJAX error
                    Swal.fire({
                        icon: "error",
                        title: "Error!",
                        text: "An error occurred while submitting the form."
                    });
                }
            });
        } else {
            Swal.fire({
                icon: "error",
                title: "Error!",
                text: "Jumlah Bobot Tidak 100%"
            });
        }
    });

    $("#FormKPICash").submit(function (e) {

        e.preventDefault(); // Prevent the default form submission
        console.log("Submit Cash")
        //var objectKPI_Section = {}
        var arrayKPI_SectionCash = [];
        var jumlah = 0;
        console.log($('#dsItem_Cash').val());


        if ($('#dsItem_Cash').val() != "") {

            for (i = 1; i <= arrayAddKPI_Cash.length; i++) {
                jumlah += parseInt($(`#bobot_Section_${i}`).val(), 10);
                var objectKPI_SectionCash = {
                    CODE_SECTION: $('#dsItem_Cash').val(),
                    KPI_ITEM: $(`#kpiItem_Section_${i}`).val(),
                    DESKRIPSI: $(`#desc_Section_${i}`).val(),
                    BOBOT: isNaN(parseInt($(`#bobot_Section_${i}`).val(), 10)) ? 0 : parseInt($(`#bobot_Section_${i}`).val(), 10),
                    TARGET: $(`#plan_Section_${i}`).val(),
                    EDIT_BY: $('#editBy_Cash').val()
                }
                arrayKPI_SectionCash.push(objectKPI_SectionCash)
            }

            console.log($(this).attr("action"));
            console.log(jumlah);

            if (jumlah == 100) {
                // Perform AJAX form submission
                $.ajax({
                    url: $(this).attr("action"),
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    data: JSON.stringify(arrayKPI_SectionCash),
                    type: "POST",
                    success: function (data) {
                        $('#modalAddCash').modal('hide');
                        if (data.Status === true) {
                            // Call SweetAlert2 when Status is true
                            Swal.fire({
                                icon: "success",
                                title: "Success!",
                                text: "Form submitted successfully."
                            });
                            tableCash.ajax.reload();
                        } else {
                            // Handle the case when Status is false
                            Swal.fire({
                                icon: "error",
                                title: "Error!",
                                text: "Message" + data.Message
                            });
                        }
                    },
                    error: function () {
                        // Handle AJAX error
                        Swal.fire({
                            icon: "error",
                            title: "Error!",
                            text: "An error occurred while submitting the form."
                        });
                    }
                });
            } else {
                Swal.fire({
                    icon: "error",
                    title: "Error!",
                    text: "Jumlah Bobot Tidak 100%"
                });
            }


        } if ($('#dsItemCash').val() == "") {
            Swal.fire({
                icon: "error",
                title: "Error!",
                text: "Code Section Harus di isi !"
            });
        }
    });

    $("#FormKPI_EditCash").submit(function (e) {
        e.preventDefault(); // Prevent the default form submission
        selectElement = document.querySelector('#dsItem_CashEdit');
        output = selectElement.options[selectElement.selectedIndex].value;
        console.log(output);

        var arrayKPIEditCash_Section = [];
        var jumlahEdit = 0;
        var id_kpi = tabelKPIbySection_Cash.column(0).data();
        var kode_kpi = tabelKPIbySection_Cash.column(2).data();
        console.log(kode_kpi);
        for (s = 0; s < id_kpi.length; s++) {
            jumlahEdit += parseInt($(`#editBobotKPICash_${id_kpi[s]}`).val(), 10);
            var objectKPIEdit_CashSection = {
                CODE_SECTION : output,
                KPI_CODE: kode_kpi[s],
                KPI_ITEM: $(`#editItemKPICash_${id_kpi[s]}`).val(),
                DESKRIPSI: $(`#editDescKPICash_${id_kpi[s]}`).val(),
                BOBOT: isNaN(parseInt($(`#editBobotKPICash_${id_kpi[s]}`).val(), 10)) ? 0 : parseInt($(`#editBobotKPICash_${id_kpi[s]}`).val(), 10),
                TARGET: $(`#editPlanKPICash_${id_kpi[s]}`).val(),
                EDIT_BY: $('#editBy_Cash_edit').val()
            }
            arrayKPIEditCash_Section.push(objectKPIEdit_CashSection);
        }
        console.log(jumlahEdit);
        console.log(arrayKPIEditCash_Section)

        if (jumlahEdit == 100) {
            // Perform AJAX form submission
            $.ajax({
                url: $(this).attr("action"),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                data: JSON.stringify(arrayKPIEditCash_Section),
                type: "POST",
                success: function (data) {
                    $('#modalEditCash').modal('hide');
                    if (data.Status === true) {
                        // Call SweetAlert2 when Status is true
                        Swal.fire({
                            icon: "success",
                            title: "Success!",
                            text: "Form submitted successfully."
                        });
                        tableCash.ajax.reload();
                    } else {
                        // Handle the case when Status is false
                        Swal.fire({
                            icon: "error",
                            title: "Error!",
                            text: "Message" + data.Message
                        });
                    }
                },
                error: function () {
                    // Handle AJAX error
                    Swal.fire({
                        icon: "error",
                        title: "Error!",
                        text: "An error occurred while submitting the form."
                    });
                }
            });
        } else {
            Swal.fire({
                icon: "error",
                title: "Error!",
                text: "Jumlah Bobot Tidak 100%"
            });
        }
    });

    $("#FormAddLevel").submit(function (e) {
        e.preventDefault(); // Prevent the default form submission

        var objectAddLevelKPI = {};
        var arr_nilaiLevel = ['KR', 'CU', 'BA', 'BS', 'IST']
        var levelDeskripsi = [];
        levelDeskripsi.push($('#nilaiDesc_1').val(), $('#nilaiDesc_2').val(), $('#nilaiDesc_3').val(), $('#nilaiDesc_4').val(), $('#nilaiDesc_5').val());
        objectAddLevelKPI.KPI_CODE = $('#kpiCode_lvlModal').val()
        objectAddLevelKPI.DeskripsiLevel = levelDeskripsi;
        objectAddLevelKPI.NilaiLevel = arr_nilaiLevel;

        console.log(objectAddLevelKPI);

        // Perform AJAX form submission
        $.ajax({
            url: $(this).attr("action"),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            data: JSON.stringify(objectAddLevelKPI),
            type: "POST",
            success: function (data) {
                $('#modalAddLevel').modal('hide');
                if (data.Remarks === true) {
                    // Call SweetAlert2 when Status is true
                    Swal.fire({
                        icon: "success",
                        title: "Success!",
                        text: "Form submitted successfully."
                    });
                    tableON.ajax.reload();
                } else {
                    // Handle the case when Status is false
                    Swal.fire({
                        icon: "error",
                        title: "Error!",
                        text: "Message" + data.Message
                    });
                }
            },
            error: function () {
                // Handle AJAX error
                Swal.fire({
                    icon: "error",
                    title: "Error!",
                    text: "An error occurred while submitting the form."
                });
            }
        });
    });

    $("#FormAddLevel_Cash").submit(function (e) {
        e.preventDefault(); // Prevent the default form submission

        var objectAddLevelKPI_Cash = {};
        var arr_nilaiLevel = ['KR', 'CU', 'BA', 'BS', 'IST']
        var levelDeskripsi = [];
        levelDeskripsi.push($('#nilaiDesc_1').val(), $('#nilaiDesc_2').val(), $('#nilaiDesc_3').val(), $('#nilaiDesc_4').val(), $('#nilaiDesc_5').val());
        objectAddLevelKPI_Cash.KPI_CODE = $('#kpiCode_lvlModal_Cash').val()
        objectAddLevelKPI_Cash.DeskripsiLevel = levelDeskripsi;
        objectAddLevelKPI_Cash.NilaiLevel = arr_nilaiLevel;

        console.log(objectAddLevelKPI_Cash);

        // Perform AJAX form submission
        $.ajax({
            url: $(this).attr("action"),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            data: JSON.stringify(objectAddLevelKPI_Cash),
            type: "POST",
            success: function (data) {
                $('#modalAddLevel_Cash').modal('hide');
                if (data.Remarks === true) {
                    // Call SweetAlert2 when Status is true
                    Swal.fire({
                        icon: "success",
                        title: "Success!",
                        text: "Form submitted successfully."
                    });
                    tableCash.ajax.reload();
                } else {
                    // Handle the case when Status is false
                    Swal.fire({
                        icon: "error",
                        title: "Error!",
                        text: "Message" + data.Message
                    });
                }
            },
            error: function () {
                // Handle AJAX error
                Swal.fire({
                    icon: "error",
                    title: "Error!",
                    text: "An error occurred while submitting the form."
                });
            }
        });
    });

    $("#FormKPI_DE").submit(function (e) {
        e.preventDefault(); // Prevent the default form submission

        var hasilMappingAll = [];
        var id_Mapping = tabelMapping.column(0).data();
        console.log(id_Mapping);
        for (s = 0; s < id_Mapping.length; s++) {
            var objectMappingAll = {
                ID_KPI_DS: $('#ID_KPI_DS_Head').val(),
                KPI_CODE: $(`#editKPI${id_Mapping[s]}`).val(),
                ID_PIC_OFFICER_NONOM: $(`#editPIC${id_Mapping[s]}`).val()
            }

            hasilMappingAll.push(objectMappingAll);
        }
        console.log(hasilMappingAll);


        $.ajax({
            url: $("#hd_path").val() + "SubmitAllMapping",
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            data: JSON.stringify(hasilMappingAll),
            type: 'POST',
            success: function (data) {
                $('#modalAdd_DS').modal('hide');
                if (data.Status === true) {
                    // Call SweetAlert2 when Status is true
                    Swal.fire({
                        icon: "success",
                        title: "Success!",
                        text: "Form submitted successfully."
                    });
                    //tableDS.ajax.reload();
                } else {
                    // Handle the case when Status is false
                    Swal.fire({
                        icon: "error",
                        title: "Error!",
                        text: "An error occurred while processing the form."
                    });
                }
            },
            error: function () {
                Swal.fire({
                    icon: 'error',
                    title: 'Failed!',
                    text: 'Something went wrong!'
                });
            }
        });
    });

    $('#tbl_kpi_ds tbody').on('click', 'td.details-control', function () {
        var tr = $(this).closest('tr');
        var row = tbl_kpi_ds.row(tr);

        if (row.child.isShown()) {
            // This row is already open; close it.
            row.child.hide();
            tr.removeClass('shown');
        }
        else {
            // Open a child row with a child table.
            row.child('<table class="child-table" cellpadding="5" cellspacing="0" border="0"><tr><th>Child Name</th><th>Child Age</th></tr><tr><td>Child 1</td><td>5</td></tr><tr><td>Child 2</td><td>8</td></tr></table>').show();
            tr.addClass('shown');
        }
    });

});


function resetModalON() {
    $('#FormKPI').find("select[name='CODE_SECTION']").prop('disabled', false);
    $("#FormKPI")[0].reset();
    $(".select2").val("").trigger("change");
    getDataSection();

}

function resetModalCash() {
    $('#FormKPICash').find("select[name='CODE_SECTION_Cash']").prop('disabled', false);
    $("#FormKPICash")[0].reset();
    $(".select2").val("").trigger("change");
    getDataSection_Cash();

}


function resetModalUP_ON() {
    //$('#formUpdateItem').find("select[name='CODE_SECTION']").prop('disabled', false);
    $("#formUpdateItem")[0].reset();
}

function resetModalUP_Cash() {
    //$('#formUpdateItem').find("select[name='CODE_SECTION']").prop('disabled', false);
    $("#formUpdateItem_Cash")[0].reset();
}

function resetModal_MasterDS() {
    $("#FormKPI_MasterDE")[0].reset();
}

const getDataSection = () => {
    $.ajax({
        type: "GET",
        dataType: "json",
        contentType: "application/json",
        url: $("#hd_path").val() + "GetItemDSPeriode",
        success: function (result) {
            if (result.Status) {
                var l = result.data;// Remove all options using jQuery
                $("select[name='CODE_SECTION']").each(function () {
                    var selectElement = $(this);
                    selectElement.empty();
                    selectElement.append(new Option("", ""));
                    $.each(l, function (key, item) {
                        selectElement.append(new Option(item.SECTION_DETAIL, item.CODE));
                    });
                });
            }
            else {
                alert(result.Error);
            }
        }
    })
}

const getDataSection_Cash = () => {
    $.ajax({
        type: "GET",
        dataType: "json",
        contentType: "application/json",
        url: $("#hd_path").val() + "GetItemDSPeriode_Cash",
        success: function (result) {
            if (result.Status) {
                var l = result.data;// Remove all options using jQuery
                $("select[name='CODE_SECTION_Cash']").each(function () {
                    var selectElement = $(this);
                    selectElement.empty();
                    selectElement.append(new Option("", ""));
                    $.each(l, function (key, item) {
                        selectElement.append(new Option(item.SECTION_DETAIL, item.CODE));
                    });
                });
            }
            else {
                alert(result.Error);
            }
        }
    })
}


function tabelKPISection() {

}

function EditKPIDS(KPI_DS_ID, KPI_DS_DESC) {
    $("#KPI_DS_Head").val(KPI_DS_DESC);
    $("#ID_KPI_DS_Head").val(KPI_DS_ID);
    $('#modalAdd_DS').modal('show');

    getTabelMapping(KPI_DS_ID)
}

function GetEditON(CODE_SECTION) {
    /*$('#FormKPI_Edit').find("select[name='CODE_SECTION']").val(CODE_SECTION).trigger('change');*/
    $('#modalEditON').modal('show');

    /*$('#FormKPI_Edit').find("select[name='CODE_SECTION']").prop('disabled', 'disabled');*/

    $.ajax({
        type: "GET",
        dataType: "json",
        contentType: "application/json",
        url: $("#hd_path").val() + "GetAllSection?section=" + CODE_SECTION,
        success: function (result) {
            console.log(result.Status)
            console.log(result.data)
            if (result.Status) {
                $('#dsItem_Edit').empty();
                text = '<option></option>';
                $.each(result.data, function (key, val) {
                    text += '<option value="' + val.CODE + '">' + val.CODE + ". " + val.CODE_DESC + '</option>';
                });
                $("#dsItem_Edit").append(text);

                if (result.data.length > 0) {
                    $('#dsItem_Edit').val(result.data[0].CODE).trigger('change');
                }

            }
        }
    });
    $('#dsItem_Edit').prop('disabled', true);

    $('#tbl_kpiSection_Edit').DataTable().destroy();
    tabelKPIbySection = $("#tbl_kpiSection_Edit").DataTable({
        ajax: {
            url: $("#hd_path").val() + "GetTabelEditKPI/" + CODE_SECTION,
            type: "GET",
            datatype: "json",
            dataSrc: "data"
        },
        //scrollX: true,
        autoWidth: false,
        //responsive: false,
        order: [],
        columns: [
            { data: "ID_EDIT" },
            {
                data: null,
                render: function (data, type, row) {
                    var html = `<div class="btn-group mb-3" role="group" aria-label="Basic example">
                                        <button type="button" class="btn btn-danger" value="${row.KPI_CODE}" onclick="DeleteON(this)"><i class="bi bi-trash"></i></button>
                                    </div>`;
                    if (data.KPI_CODE == null) {
                        return null;
                    } else {
                        return html;
                    }
                }
            },
            { data: "KPI_CODE" },
            //{ data: "KPI_ITEM" },
            {
                data: null,
                render: function (data, type, row) {
                    return `<input type="text" id="editItemKPI_${data.ID_EDIT}" class="form-control" value="${data.KPI_ITEM == null ? "" : data.KPI_ITEM}">`
                }
            },
            //{ data: "DESKRIPSI" },
            {
                data: null,
                render: function (data, type, row) {
                    return `<input type="text" id="editDescKPI_${data.ID_EDIT}" class="form-control" value="${data.DESKRIPSI == null ? "" : data.DESKRIPSI}">`
                }
            },
            //{ data: "BOBOT" },
            {
                data: null,
                render: function (data, type, row, meta) {
                    return `<input type="number" id="editBobotKPI_${data.ID_EDIT}" class="form-control" value="${data.BOBOT == null ? "" : data.BOBOT * 100}">`
                }
            },
            //{ data: "TARGET" },
            {
                data: null,
                render: function (data, type, row) {
                    return `<input type="text" id="editPlanKPI_${data.ID_EDIT}" class="form-control" value="${data.TARGET == null ? "" : data.TARGET}">`
                }
            },


        ],
        searching: false,
        paging: false,
        info: false
    });
    tabelKPIbySection.column(0).visible(false);
}

function GetEditCash(CODE_SECTION) {
    console.log(CODE_SECTION)
    /*$('#FormKPI_EditCash').find("select[name='CODE_SECTION_Cash']").val(CODE_SECTION).trigger('change');*/
    $('#modalEditCash').modal('show');

    $.ajax({
        type: "GET",
        dataType: "json",
        contentType: "application/json",
        url: $("#hd_path").val() + "GetAllSection?section=" + CODE_SECTION,
        success: function (result) {
            console.log(result.Status)
            console.log(result.data)
            if (result.Status) {
                $('#dsItem_CashEdit').empty();
                text = '<option></option>';
                $.each(result.data, function (key, val) {
                    text += '<option value="' + val.CODE + '">' + val.CODE + ". " + val.CODE_DESC+ '</option>';
                });
                $("#dsItem_CashEdit").append(text);

                if (result.data.length > 0) {
                    $('#dsItem_CashEdit').val(result.data[0].CODE).trigger('change');
                }
                
            }
        }
    });
    $('#dsItem_CashEdit').prop('disabled', true);


    $('#tbl_kpiCash_Edit').DataTable().destroy();
    tabelKPIbySection_Cash = $("#tbl_kpiCash_Edit").DataTable({
        ajax: {
            url: $("#hd_path").val() + "GetTabelEditKPI_Cash/" + CODE_SECTION,
            type: "GET",
            datatype: "json",
            dataSrc: "data"
        },
        //scrollX: true,
        autoWidth: false,
        //responsive: false,
        order: [],
        columns: [
            { data: "ID_EDIT" },
            {
                data: null,
                render: function (data, type, row) {
                    var html = `<div class="btn-group mb-3" role="group" aria-label="Basic example">
                                        <button type="button" class="btn btn-danger" value="${row.KPI_CODE}" onclick="DeleteCash(this)"><i class="bi bi-trash"></i></button>
                                    </div>`;
                    if (data.KPI_CODE == null) {
                        return null;
                    } else {
                        return html;
                    }
                }
            },
            { data: "KPI_CODE" },
            //{ data: "KPI_ITEM" },
            {
                data: null,
                render: function (data, type, row) {
                    return `<input type="text" id="editItemKPICash_${data.ID_EDIT}" class="form-control" value="${data.KPI_ITEM == null ? "" : data.KPI_ITEM}">`
                }
            },
            //{ data: "DESKRIPSI" },
            {
                data: null,
                render: function (data, type, row) {
                    return `<input type="text" id="editDescKPICash_${data.ID_EDIT}" class="form-control" value="${data.DESKRIPSI == null ? "" : data.DESKRIPSI}">`
                }
            },
            //{ data: "BOBOT" },
            {
                data: null,
                render: function (data, type, row, meta) {
                    return `<input type="number" id="editBobotKPICash_${data.ID_EDIT}" class="form-control" value="${data.BOBOT == null ? "" : data.BOBOT * 100}">`
                }
            },
            //{ data: "TARGET" },
            {
                data: null,
                render: function (data, type, row) {
                    return `<input type="text" id="editPlanKPICash_${data.ID_EDIT}" class="form-control" value="${data.TARGET == null ? "" : data.TARGET}">`
                }
            },


        ],
        searching: false,
        paging: false,
        info: false
    });
    tabelKPIbySection_Cash.column(0).visible(false);
}

function GetEditMasterDS(button) {
    var KPI_DS_ID = button.getAttribute("value");
    var KPI_DS_DESC = button.getAttribute("data-M-ds");


    $('#FormKPI_MasterDE').find("input[name='KPI_DS_ID']").val(KPI_DS_ID);
    $('#FormKPI_MasterDE').find("input[name='KPI_DS_DESC']").val(KPI_DS_DESC);
    $('#modalAdd_MasterDS').modal('show');

}

function GetEditDS(button) {
    var ITEM = button.getAttribute("data-item");
    var ID = $(button).val();

    var ITEM = button.getAttribute("data-item");
    var ID = $(button).val();

    $('#FormDS').find("input[name='ID']").val(ID);
    $('#FormDS').find("input[name='ITEM']").val(ITEM);
    $('#modalAddDS').modal('show');
}

function GetEditOF(button) {
    var ITEM = button.getAttribute("data-item");
    var ID = $(button).val();
    var CODE_SECTION = button.getAttribute("data-ds");

    $('#FormOF').find("select[name='CODE_SECTION']").val(CODE_SECTION).trigger('change');
    $('#FormOF').find("input[name='ID']").val(ID);
    $('#FormOF').find("input[name='ITEM']").val(ITEM);
    $('#modalAddOF').modal('show');
}

function DeleteKPIDS(KPI_DS_ID) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "POST",
                dataType: "json",
                contentType: "application/json",
                url: $("#hd_path").val() + "DeleteKPIDS?id=" + KPI_DS_ID,
                success: function (result) {
                    if (result.Status == true) {
                        Swal.fire(
                            'Deleted!',
                            'Item has been deleted.',
                            'success'
                        );
                        tableMasterDS.ajax.reload();
                    } else {
                        Swal.fire(
                            'Error!',
                            result.Message,
                            'error'
                        )
                    }
                }
            })
        }
    })
}

function DeleteON(KPI_CODE) {
    var KPI_CODE_val = KPI_CODE.getAttribute("value");
    console.log(KPI_CODE_val);
    console.log(typeof (KPI_CODE_val));
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "POST",
                //dataType: "json",
                //contentType: "application/json",
                url: $("#hd_path").val() + "Delete_DataKPI?KPI_CODE=" + KPI_CODE_val,
                success: function (result) {
                    if (result.Remarks == true) {
                        tabelKPIbySection.ajax.reload();
                        //Swal.fire(
                        //    'Deleted!',
                        //    'Item has been deleted.',
                        //    'success'
                        //);
                        //tableON.ajax.reload();
                    } else {
                        Swal.fire(
                            'Error!',
                            result.Message,
                            'error'
                        )
                    }
                }
            })
        }
    })


}

function DeleteCash(KPI_CODE) {
    var KPI_CODE_val = KPI_CODE.getAttribute("value");
    console.log(KPI_CODE_val);
    console.log(typeof (KPI_CODE_val));
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "POST",
                //dataType: "json",
                //contentType: "application/json",
                url: $("#hd_path").val() + "Delete_DataKPICash?KPI_CODE=" + KPI_CODE_val,
                success: function (result) {
                    if (result.Remarks == true) {
                        tbl_kpiCash_Edit.ajax.reload();
                        //Swal.fire(
                        //    'Deleted!',
                        //    'Item has been deleted.',
                        //    'success'
                        //);
                        //tableON.ajax.reload();
                    } else {
                        Swal.fire(
                            'Error!',
                            result.Message,
                            'error'
                        )
                    }
                }
            })
        }
    })


}

function DeleteOF(id) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "POST",
                dataType: "json",
                contentType: "application/json",
                url: $("#hd_path").val() + "DeleteOF/" + id,
                success: function (result) {
                    if (result.Status == true) {
                        Swal.fire(
                            'Deleted!',
                            'Item has been deleted.',
                            'success'
                        );
                        tableOF.ajax.reload();
                    } else {
                        Swal.fire(
                            'Error!',
                            result.Message,
                            'error'
                        )
                    }
                }
            })
        }
    })


}

$('#dsItem').on('change', function () {
    var selectedValue = $(this).val();

    $('#ofItem option').each(function () {
        if ($(this).data('ds') == selectedValue) {
            $(this).show();
        } else {
            $(this).hide();
        }
    });

    $('#ofItem').val($('#ofItem option:visible:first').val());
});

$('#dsItem_Cash').on('change', function () {
    var selectedValue = $(this).val();

    $('#ofItem option').each(function () {
        if ($(this).data('ds') == selectedValue) {
            $(this).show();
        } else {
            $(this).hide();
        }
    });

    $('#ofItem').val($('#ofItem option:visible:first').val());
});



const DownloadTemplate = () => {
    $.ajax({
        type: "GET",
        url: $("#hd_path").val() + "GenerateTemplateItemKPI",
        success: function (result) {
            if (result.Status == true) {
                var path = $("#hd_path").val() + "Files/TemplateItemKPI.xlsx"
                console.log(path);
                window.open(path);
            } else {
                Swal.fire(
                    'Error!',
                    result.Message,
                    'error'
                )
            }
        }
    })
}

function getTabelMapping(KPI_DS_ID) {
    $('#tbl_MappingKPI').DataTable().destroy();
    tabelMapping = $("#tbl_MappingKPI").DataTable({
        ajax: {
            url: $("#hd_path").val() + `GetTabelMappingKPI/${KPI_DS_ID}`,
            type: "GET",
            datatype: "json",
            dataSrc: "data"
        },
        scrollX: true,
        autoWidth: false,
        responsive: false,
        columns: [
            { data: 'ID' },
            {
                data: null,
                render: function (data, type, row) {
                    var html = `<div class="btn-group mb-3" role="group" aria-label="Basic example">
                                        <button type="button" class="btn btn-danger" onclick="DeleteMapping('${data.ID}')"><i class="bi bi-trash"></i></button>
                                    </div>`;
                    return html;
                }
            },
            //{ data: "KPI_CODE" },
            {
                data: null,
                render: function (data, type, row, meta) {
                    //if (data.ACTUAL == null) {
                    $.ajax({
                        type: "GET",
                        dataType: "json",
                        contentType: "application/json",
                        url: $("#hd_path").val() + "GetListKPICode",
                        success: function (result) {
                            if (result.Status) {
                                //var l = result.data;
                                //dataID = {};
                                //for (i = 1; i <= tabelMapping.rows().data().length; i++) {
                                //    dataID.ID = data.ID;
                                //}
                                ////$(`#editNilai${data.ID}`).append(new Option('Role', 0));
                                //$.each(l, function (key, item) {
                                //    $(`#editKPI${data.ID}`).append(new Option(item.KPI_CODE + '-' + item.KPI_ITEM, item.KPI_CODE));
                                //})
                                //$(`#editKPI${data.ID}`).val(data.KPI_CODE).change();
                                var kpi = '<option value=""></option>';
                                $.each(result.data, function (key, val) {
                                    //console.log(val.LEVEL_TRAINING);
                                    kpi += '<option value="' + val.KPI_CODE + '">' + val.KPI_CODE + ' - ' + val.KPI_ITEM + '</option>';
                                });
                                dataID = {};
                                for (i = 1; i <= tabelMapping.rows().data().length; i++) {
                                    dataID.ID = data.ID;
                                }

                                $(`#editKPI${data.ID}`).html(kpi);
                                $(`#editKPI${data.ID}`).select2({
                                    width: "100%",
                                    //allowClear: true,
                                    placeholder: "Pilih..."
                                });
                                $(`#editKPI${data.ID}`).val(data.KPI_CODE).change();
                            }
                            else {
                                alert(result.Error);
                            }
                        }
                    })
                    return `<select id="editKPI${data.ID}" class="form-control" name="training" required></select > `;
                }
            },
            { data: "KPI_ITEM" },
            //{ data: "BOBOT" },
            {
                data: null,
                render: function (data, type, row, meta) {
                   

                    return (data.BOBOT * 100) + '%';
                }
            },
            //{ data: "INITIALS" },
            {
                data: null,
                render: function (data, type, row, meta) {
                    //if (data.ACTUAL == null) {
                    $.ajax({
                        type: "GET",
                        dataType: "json",
                        contentType: "application/json",
                        url: $("#hd_path").val() + "GetListPIC",
                        success: function (result) {
                            if (result.Status) {
                                //var l = result.data;
                                //dataID = {};
                                //for (i = 1; i <= tabelMapping.rows().data().length; i++) {
                                //    dataID.ID = data.ID;
                                //}
                                ////$(`#editNilai${data.ID}`).append(new Option('Role', 0));
                                //$.each(l, function (key, item) {
                                //    $(`#editPIC${data.ID}`).append(new Option(item.INITIALS, item.ID));
                                //})
                                //$(`#editPIC${data.ID}`).val(data.ID_PIC_OFFICER_NONOM).change();
                                var level = '<option value=""></option>';
                                $.each(result.data, function (key, val) {
                                    //console.log(val.LEVEL_TRAINING);
                                    level += '<option value="' + val.ID + '">' + val.INITIALS + '</option>';
                                });
                                dataID = {};
                                for (i = 1; i <= tabelMapping.rows().data().length; i++) {
                                    dataID.ID = data.ID;
                                }

                                $(`#editPIC${data.ID}`).html(level);
                                $(`#editPIC${data.ID}`).select2({
                                    width: "100%",
                                    //allowClear: true,
                                    placeholder: "Pilih..."
                                });
                                $(`#editPIC${data.ID}`).val(data.ID_PIC_OFFICER_NONOM).change();
                            }
                            else {
                                alert(result.Error);
                            }
                        }
                    })
                    return `<select id="editPIC${data.ID}" class="form-control" name="training" required></select > `;
                }
            },
        ],
    });

    tabelMapping.column(0).visible(false);

    let buttonAddMapping = "";
    buttonAddMapping = `<button type="button" class="btn btn-primary" id="EKI_btn" onclick="AddMapping(${KPI_DS_ID})">ADD</button>`;
    $("#btn_AddMapping").html(buttonAddMapping);
}

function AddKPI() {
    
    arrayAddKPI.push('kpi');
    console.log(arrayAddKPI.length);
    var urutan = arrayAddKPI.length;
    var objectAddKPI = {
        SECTION: $("#dsItem").val()
    }

    console.log(objectAddKPI);
    $("#tbl_kpiSection").find('tbody').append(
        `<tr>
                <td class="text-center"></td>
                <td class="text-center"></td>
                <td class="text-center"><input type="text" id="kpiItem_Section_${urutan}" class="form-control"></td>
                <td class="text-center"><input type="text" id="desc_Section_${urutan}" class="form-control"></td>
                <td class="text-center"><input type="number" id="bobot_Section_${urutan}" class="form-control"></td>
                <td class="text-center"><input type="text" id="plan_Section_${urutan}" class="form-control"></td>
            </tr>`
    );
}

function AddKPI_Cash() {

    arrayAddKPI_Cash.push('kpi');
    console.log(arrayAddKPI_Cash.length);
    var urutan = arrayAddKPI_Cash.length;
    var objectAddKPI_Cash = {
        SECTION: $("#dsItem_Cash").val()
    }

    console.log(objectAddKPI_Cash);
    $("#tbl_kpiCash").find('tbody').append(
        `<tr>
                <td class="text-center"></td>
                <td class="text-center"></td>
                <td class="text-center"><input type="text" id="kpiItem_Section_${urutan}" class="form-control"></td>
                <td class="text-center"><input type="text" id="desc_Section_${urutan}" class="form-control"></td>
                <td class="text-center"><input type="number" id="bobot_Section_${urutan}" class="form-control"></td>
                <td class="text-center"><input type="text" id="plan_Section_${urutan}" class="form-control"></td>
            </tr>`
    );
}

function DeleteMapping(ID) {
    console.log(ID);
    $.ajax({
        url: $("#hd_path").val() + `DeleteMapping/${ID}`,
        type: "POST",
        success: function (data) {
            if (data.Status == true) {
                tabelMapping.ajax.reload();
            } if (data.Status == false) {
                Swal.fire(
                    'Can\'t be Added!',
                    'Message : ' + data.Message,
                    'error'
                );
            }
        },
        error: function (xhr) {
            alert(xhr.responseText);
        },
    });
}

function AddMapping(KPI_DS_ID) {
    console.log(KPI_DS_ID);
    $.ajax({
        url: $("#hd_path").val() + `AddMapping/${KPI_DS_ID}`,
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: null,
        type: 'POST',
        success: function (data) {
            if (data.Status == true) {
                tabelMapping.ajax.reload();
            } if (data.Status == false) {
                Swal.fire(
                    'Can\'t be Added!',
                    'Message : ' + data.Message,
                    'error'
                );
            }
        },
        error: function () {
            Swal.fire({
                icon: 'error',
                title: 'Failed!',
                text: 'Something went wrong!'
            });
        },
    });
}

//function SubmitMappingAll() {
//    var hasilMappingAll = [];
//    var id_Mapping = tabelMapping.column(0).data();
//    console.log(id_Mapping);
//    for (s = 0; s < id_T_EKI.length; s++) {
//        var objectMappingAll = {
//            ID_KPI_DS: $('#ID_KPI_DS_Head').val(),
//            KPI_CODE: $(`#editKPI${id_Mapping[s]}`).val(),
//            ID_PIC_OFFICER_NONOM: $(`#editPIC${id_Mapping[s]}`).val()
//        }
        
//        hasilMappingAll.push(objectMappingAll);
//    }
//    console.log(hasilMappingAll);
//}

function getNilaiLevelDesc(code_section) {
    kpi = "";
    if (code_section == "new") {
        console.log(code_section);
        console.log("tabel biasa")
        //tabellAdd();
    }
    else {
        console.log(code_section);
        kpi = code_section;
        console.log("datatable")
        //tabelEdit(code_section);
        ////tableNilaiLevelKPI.ajax.reload();
    }
}
console.log(kpi)
function tabelEdit(kpi_id) {
    $('#tbl_levelKPI').DataTable().destroy();
    if (kpi_id == "new") {
        $('#tbl_levelKPI').DataTable().clear().destroy();
        //$('#tbl_levelKPI').DataTable().row.add([
        //    '1', '1', '1', '1', '1'
        //]).draw();
        var arr_nilaiLevel = ['KR','CU','BA','BS','IST']
        for (let i = 1; i <= 5; i++) {
            $("#tbl_levelKPI").find('tbody').append(
                `<tr>
                <td class="text-center">${i}</td>
                <td class="text-center"></td>
                <td class="text-center">${arr_nilaiLevel[i - 1]}</td>
                <td><input type="text" id="nilaiDesc_${i}" class="form-control"></td>
            </tr>`
            );
        }
    } else {
        var tableNilaiLevelKPI = $("#tbl_levelKPI").DataTable({
            ajax: {
                url: $("#hd_path").val() + "getNilaiLevelKPI?kpi=" + kpi_id,
                type: "GET",
                datatype: "json",
                dataSrc: "data"
            },
            scrollX: true,
            autoWidth: false,
            responsive: false,
            columns: [
                { data: "NILAI" },
                { data: "KPI_CODE" },
                { data: "NILAI_LEVEL" },
                {
                    data: null,
                    render: function (data, type, row, meta) {
                        return `<input type="text" id="nilaiDesc_${row.NILAI}" class="form-control" value="${data.NILAI_DESC}">`
                        //if (data.NILAI_DESC == null) {
                        //    return `<input type="text" id="nilaiDesc_${row.NILAI}" class="form-control" value="">`
                        //} else {
                        //    return `<input type="text" id="nilaiDesc_${row.NILAI}" class="form-control" value="${data.NILAI_DESC}">`
                        //}
                        //}

                        //}

                        //else {
                        //    return data.ACTUAL;
                        //}
                    }
                },

            ],
        });
    }
};

function AddLevelKPI(KPI_CODE, KPI_ITEM) {
    //console.log($("#hd_path").val() + "getNilaiLevelKPI?kpi=" + KPI_CODE)
    $('#FormAddLevel').find("input[name='KPI_ITEM']").val(KPI_ITEM);
    $('#FormAddLevel').find("input[name='KPI_CODE']").val(KPI_CODE);
    $('#modalAddLevel').modal('show');
    $.ajax({
        url: $("#hd_path").val() + "getNilaiLevelKPI?kpi=" + KPI_CODE,
        type: "GET",
        cache: false,
        success: function (result) {
            console.log(result.data.length)
            if (result.data.length == 0) {
                console.log('kosong');
                $('#tbl_levelKPI').DataTable().clear().destroy();
                //$('#tbl_levelKPI').DataTable().row.add([
                //    '1', '1', '1', '1', '1'
                //]).draw();
                var arr_nilaiLevel = ['KR', 'CU', 'BA', 'BS', 'IST']
                for (let i = 1; i <= 5; i++) {
                    $("#tbl_levelKPI").find('tbody').append(
                        `<tr>
                            <td class="text-center">${i}</td>
                            <td class="text-center">${KPI_CODE}</td>
                            <td class="text-center">${arr_nilaiLevel[i - 1]}</td>
                            <td><input type="text" id="nilaiDesc_${i}" class="form-control"></td>
                        </tr>`
                    );
                }
            } else {
                console.log('ada')
                $('#tbl_levelKPI').DataTable().destroy();
                tabelLevel =  $("#tbl_levelKPI").DataTable({
                    ajax: {
                        url: $("#hd_path").val() + "getNilaiLevelKPI?kpi=" + KPI_CODE,
                        type: "GET",
                        datatype: "json",
                        dataSrc: "data"
                    },
                    //scrollX: true,
                    autoWidth: false,
                    //responsive: false,
                    order: [],
                    columns: [
                        { data: "NILAI" },
                        { data: "KPI_CODE" },
                        { data: "NILAI_LEVEL" },
                        {
                            data: null,
                            render: function (data, type, row, meta) {
                                return `<input type="text" id="nilaiDesc_${row.NILAI}" class="form-control" value="${data.NILAI_DESC}">`
                            }
                        },

                    ],
                    columnDefs: [
                        { targets: 0, className: "dt-center", width: "20px" },
                        { targets: 1, className: "dt-center" },
                        { targets: 2, className: "dt-center" },
                    ],
                    searching: false,
                    paging: false,
                    info: false,
                    ordering: false
                });

            }

        }
    });
}

function AddLevelKPI_Cash(KPI_CODE, KPI_ITEM) {
    //console.log($("#hd_path").val() + "getNilaiLevelKPI?kpi=" + KPI_CODE)
    $('#FormAddLevel_Cash').find("input[name='KPI_ITEM_Cash']").val(KPI_ITEM);
    $('#FormAddLevel_Cash').find("input[name='KPI_CODE_Cash']").val(KPI_CODE);
    $('#modalAddLevel_Cash').modal('show');
    $.ajax({
        url: $("#hd_path").val() + "getNilaiLevelKPI_Cash?kpi=" + KPI_CODE,
        type: "GET",
        cache: false,
        success: function (result) {
            console.log(result.data.length)
            if (result.data.length == 0) {
                console.log('kosong');
                $('#tbl_levelKPI_Cash').DataTable().clear().destroy();
                //$('#tbl_levelKPI').DataTable().row.add([
                //    '1', '1', '1', '1', '1'
                //]).draw();
                var arr_nilaiLevel = ['KR', 'CU', 'BA', 'BS', 'IST']
                for (let i = 1; i <= 5; i++) {
                    $("#tbl_levelKPI_Cash").find('tbody').append(
                        `<tr>
                            <td class="text-center">${i}</td>
                            <td class="text-center">${KPI_CODE}</td>
                            <td class="text-center">${arr_nilaiLevel[i - 1]}</td>
                            <td><input type="text" id="nilaiDesc_${i}" class="form-control"></td>
                        </tr>`
                    );
                }
            } else {
                console.log('ada')
                $('#tbl_levelKPI_Cash').DataTable().destroy();
                tabelLevel = $("#tbl_levelKPI_Cash").DataTable({
                    ajax: {
                        url: $("#hd_path").val() + "getNilaiLevelKPI_Cash?kpi=" + KPI_CODE,
                        type: "GET",
                        datatype: "json",
                        dataSrc: "data"
                    },
                    //scrollX: true,
                    autoWidth: false,
                    //responsive: false,
                    order: [],
                    columns: [
                        { data: "NILAI" },
                        { data: "KPI_CODE" },
                        { data: "NILAI_LEVEL" },
                        {
                            data: null,
                            render: function (data, type, row, meta) {
                                return `<input type="text" id="nilaiDesc_${row.NILAI}" class="form-control" value="${data.NILAI_DESC}">`
                            }
                        },

                    ],
                    columnDefs: [
                        { targets: 0, className: "dt-center", width: "20px" },
                        { targets: 1, className: "dt-center" },
                        { targets: 2, className: "dt-center" },
                    ],
                    searching: false,
                    paging: false,
                    info: false,
                    ordering: false
                });

            }

        }
    });
}

function AddKPI_Edit() {
    arrayAddKPI.push('kpi');
    console.log(arrayAddKPI.length);
    var urutan = arrayAddKPI.length;
    var objectAddKPI = {
        SECTION: $("#dsItem").val()
    }

    console.log(objectAddKPI);
    $("#tbl_kpiSection").find('tbody').append(
        `<tr>
                <td class="text-center"></td>
                <td class="text-center"></td>
                <td class="text-center"><input type="text" id="kpiItem_Section_${urutan}" class="form-control"></td>
                <td class="text-center"><input type="text" id="desc_Section_${urutan}" class="form-control"></td>
                <td class="text-center"><input type="number" id="bobot_Section_${urutan}" class="form-control"></td>
                <td class="text-center"><input type="text" id="plan_Section_${urutan}" class="form-control"></td>
            </tr>`
    );

    //var objectAddKPI = {
    //    CODE_SECTION: $("#dsItem_Edit").val()
    //}
    //console.log(objectAddKPI);
    //$.ajax({
    //    url: $("#hd_path").val() + "AddKPION",
    //    contentType: 'application/json; charset=utf-8',
    //    dataType: 'json',
    //    data: JSON.stringify(objectAddKPI),
    //    type: 'POST',
    //    success: function (data) {
    //        if (data.Status == true) {
    //            tabelKPIbySection.ajax.reload();
    //        } else {
    //            Swal.fire(
    //                'Can\'t be Added!',
    //                'Message : ' + data.Message,
    //                'error'
    //            );
    //        }
    //    },
    //    error: function () {
    //        Swal.fire({
    //            icon: 'error',
    //            title: 'Failed!',
    //            text: 'Something went wrong!'
    //        });
    //    },
    //});
}

function AddKPI_CashEdit() {
    var objectAddKPI_Cash = {
        CODE_SECTION: $("#dsItem_CashEdit").val()
    }
    console.log(objectAddKPI_Cash);
    $.ajax({
        url: $("#hd_path").val() + "AddKPICash",
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: JSON.stringify(objectAddKPI_Cash),
        type: 'POST',
        success: function (data) {
            if (data.Status == true) {
                tabelKPIbySection_Cash.ajax.reload();
            } else {
                Swal.fire(
                    'Can\'t be Added!',
                    'Message : ' + data.Message,
                    'error'
                );
            }
        },
        error: function () {
            Swal.fire({
                icon: 'error',
                title: 'Failed!',
                text: 'Something went wrong!'
            });
        },
    });
}

const DownloadAllData = () => {
    $.ajax({
        type: "GET",
        url: $("#hd_path").val() + "GetAllDataKPI",
        success: function (result) {
            if (result.Status == true) {
                var path = $("#hd_path").val() + "Files/UpdateAllDataOfficer.xlsx"
                console.log(path);
                window.open(path);
            } else {
                Swal.fire(
                    'Error!',
                    result.Message,
                    'error'
                )
            }
        }
    })
}

const DownloadAllData_Cash = () => {
    $.ajax({
        type: "GET",
        url: $("#hd_path").val() + "GetAllDataKPI_Cash",
        success: function (result) {
            if (result.Status == true) {
                var path = $("#hd_path").val() + "Files/UpdateAllDataCashier.xlsx"
                console.log(path);
                window.open(path);
            } else {
                Swal.fire(
                    'Error!',
                    result.Message,
                    'error'
                )
            }
        }
    })
}

const DownloadAllData_DS = () => {
    $.ajax({
        type: "GET",
        url: $("#hd_path").val() + "GetAllDataKPI_DS",
        success: function (result) {
            if (result.Status == true) {
                var path = $("#hd_path").val() + "Files/UpdateAllData_DeptSect.xlsx"
                console.log(path);
                window.open(path);
            } else {
                Swal.fire(
                    'Error!',
                    result.Message,
                    'error'
                )
            }
        }
    })
}

