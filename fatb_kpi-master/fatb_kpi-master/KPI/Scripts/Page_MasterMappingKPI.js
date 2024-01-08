var excelMapping;
/*var tableMappingON;*/

const tableDS = $("#tbl_kpi_ds").DataTable({
    ajax: {
        url: $("#hd_path").val() + "GetTabelMappingAll",
        type: "GET",
        datatype: "json",
        dataSrc: "data"
    },
    //scrollX: true,
    autoWidth: false,
    //responsive: false,
    order: [],
    columns: [
        {
            data: null,
            render: function (data, type, row) {
                var html = `<div class="btn-group mb-3" role="group" aria-label="Basic example">
                                        <button type="button" class="btn btn-info" onclick="EditKPIDS('${data.KPI_DS_ID}','${data.KPI_DS_DESC}')">Mapping</i></button>
                                    </div>`;
                if (data.KPI_DS_ID == null) {
                    return null;
                } else {
                    return html;
                }
            }
        },
        { data: "KPI_DS_ID" },
        { data: "KPI_DS_DESC" },
        { data: "KPI_CODE" },
        { data: "KPI_ITEM" },
        //{ data: "BOBOT" },
        {
            data: null,
            render: function (data, type, row, meta) {
                if (data.BOBOT == null) {
                    return null;
                } else {
                    return (data.BOBOT * 100) + '%';
                }

            }
        },
        { data: "INITIALS" }
    ],
});

const tableON = $("#tbl_kpi_on").DataTable({
    ajax: {
        url: $("#hd_path").val() + "GetTabelMappingKPION",
        type: "GET",
        datatype: "json",
        dataSrc: "data"
    },
    //scrollX: true,
    autoWidth: false,
    //responsive: false,
    order: [],
    columns: [
        {
            data: null,
            render: function (data, type, row) {
                var html = `<div class="btn-group mb-3" role="group" aria-label="Basic example">
                                        <button type="button" class="btn btn-info" onclick="EditKPION('${data.ID_KPI_ON}','${data.KPI_ITEM}')">Mapping</button>
                                    </div>`;
                if (data.ID_KPI_ON == null) {
                    return null;
                } else {
                    return html;
                }
            }
        },
        { data: "ID_KPI_ON"},
        { data: "KPI_ITEM" },
        { data: "KPI_CODE" },
        { data: "KPI_DESC" },
        //{ data: "BOBOT" },
        {
            data: null,
            render: function (data, type, row, meta) {
                if (data.BOBOT == null) {
                    return null;
                } else {
                    return (data.BOBOT * 100) + '%';
                }

            }
        },
        { data: "INITIALS" }
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
                if (workbook.SheetNames[i] == "Mapping_KPI") {
                    var sheet = workbook.SheetNames[i];
                    var XL_row_object = XLSX.utils.sheet_to_json(workbook.Sheets[sheet], { defval: "" });
                    var json_object = JSON.stringify(XL_row_object);
                    excelMapping = JSON.parse(json_object);
                    console.log(excelMapping);
                    break; // Exit the loop once the sheet is found
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

document.getElementById('excel_mapping').addEventListener('change', handleFileSelect, false);


$(document).ready(function () {
    $("#btn_closeMappingKPI").click(function () {
        tableDS.ajax.reload();
    });

    $("#btn_closeMappingKPION").click(function () {
        tableON.ajax.reload();
    });

    $("#formUploadMapping").submit(function (e) {
        e.preventDefault(); // Prevent the default form submission

        // Perform AJAX form submission
        $.ajax({
            url: $(this).attr("action"),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            data: JSON.stringify(excelMapping),
            type: 'POST',
            success: function (data) {
                if (data.Remarks === true) {
                    // Call SweetAlert2 when Status is true
                    Swal.fire({
                        icon: "success",
                        title: "Success!",
                        text: "Form submitted successfully."
                    });
                    $("#formUploadMapping")[0].reset();
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

    // Form in Mapping Edit Submit Mapping KPI Dept/Sect Head
    $("#FormKPI_DE").submit(function (e) {
        e.preventDefault(); // Prevent the default form submission

        let resultMap = [];
        $("#tbl_MappingKPI").DataTable().rows().every(function () {
            let row = this.node();
            let rowData = this.data();
            let obj = {
                ID: rowData.ID || 0,
                ID_KPI_DS: rowData.KPI_DS_ID || "",
                KPI_CODE: $(row).find('select[name="editKPI"]').val() || "",
                ID_PIC_OFFICER_NONOM: parseInt($(row).find('select[name="editPIC"]').val()) || 0
            };

            resultMap.push(obj);
        });

        $.ajax({
            url: `${$("#hd_path").val()}api/v2/kpi/TabelMappingKPIDS`,
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            data: JSON.stringify(resultMap),
            type: 'POST',
            success: function (data) {
                $('#modalAdd_DS').modal('hide');
                // Call SweetAlert2 when Status is true
                Swal.fire({
                    icon: "success",
                    title: "Success!",
                    text: "Form submitted successfully."
                });
                tableDS.ajax.reload();
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

    $("#FormKPI_AddON").submit(function (e) {
        e.preventDefault(); // Prevent the default form submission

        console.log($(this).serialize())
        //var object_AddKPION = {};
        //objectItem.ID_KPI_ON = $('#onItem').val();

        // Perform AJAX form submission
        $.ajax({
            url: $(this).attr("action"),
            //contentType: 'application/json; charset=utf-8',
            //dataType: 'json',
            data: $(this).serialize(),
            type: 'POST',
            success: function (data) {
                $('#modalAdd_MasterON').modal('hide');
                if (data.Status == true) {
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

    // Form in Mapping Edit Submit Mapping KPI Officer/Cashier
    $("#FormKPI_MappingON").submit(function (e) {
        e.preventDefault(); // Prevent the default form submission

        let resultMapON = [];

        $("#tbl_MappingKPION").DataTable().rows().every(function () {
            let row = this.node();
            let rowData = this.data();
            let obj = {
                ID: rowData.ID || 0,
                ID_KPI_ON: rowData.ID_KPI_ON || "",
                KPI_CODE: $(row).find('select[name="editKPION"]').val() || "",
                ID_PIC: parseInt($(row).find('select[name="editPICON"]').val()) || 0
            };

            resultMapON.push(obj);
        });

        $.ajax({
            url: `${$("#hd_path").val()}api/v2/kpi/TabelMappingKPION`,
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            data: JSON.stringify(resultMapON),
            type: 'POST',
            success: function (data) {
                $('#modalAdd_MappingON').modal('hide');
                // Call SweetAlert2 when Status is true
                Swal.fire({
                    icon: "success",
                    title: "Success!",
                    text: "Form submitted successfully."
                });
                tableON.ajax.reload();
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
});

function EditKPION(ID_KPI_ON, KPI_ITEM) {
    $('#modalAdd_MappingON').modal('show');
    $('#FormKPI_MappingON').find("input[name='ID_KPI_ON']").val(ID_KPI_ON);
    $('#FormKPI_MappingON').find("input[name='NAME_KPI_ON_edit']").val(ID_KPI_ON + " - " + KPI_ITEM);

    $('#tbl_MappingKPION').DataTable().destroy();
    tableMappingON = $("#tbl_MappingKPION").DataTable({
        ajax: {
            url: `${$("#hd_path").val()}api/v2/kpi/TabelMappingKPION?ID_KPI_ON=${ID_KPI_ON}`,
            type: "GET",
            datatype: "json",
            dataSrc: "Data"
        },
        autoWidth: false,
        ordering: false, // Disable sorting
        order: [],
        columns: [
            { data: 'ID', visible: false },
            {
                data: null,
                render: function (data, type, row) {
                    var html = `<div class="btn-group mb-3" role="group" aria-label="Basic example">
                                        <button type="button" class="btn btn-danger" onclick="DeleteMappingON(this)"><i class="bi bi-trash"></i></button>
                                    </div>`;
                    return html;
                }
            },
            // "KPI_CODE"
            {
                data: null,
                render: function (data, type, row, meta) {
                    let kpi = '<option value=""></option>';
                    data.KPICodes.forEach(item => {
                        kpi += `<option value="${item.KPI_CODE}" ${item.KPI_CODE == data.KPI_CODE ? "selected" : ""}>${item.KPI_CODE} - ${item.KPI_ITEM}</option>`;
                    });
                    return `<select ${data.ID != 0 ? `id="editKPION${data.ID}"` : ''} name="editKPION" class="select2-js-kpi" required>${kpi}</select> `;
                }
            },
            { data: "KPI_DESC" },
            //"BOBOT"
            {
                data: null,
                render: function (data, type, row, meta) {
                    if (data.BOBOT == null) {
                        return null;
                    } else {
                        return (data.BOBOT * 100) + '%';
                    }

                }
            },
            //"INITIALS"
            {
                data: null,
                render: function (data, type, row, meta) {
                    var level = '';//'<option value=""></option>';
                    data.PICS.forEach(item => {
                        level += `<option value="${item.ID}" ${item.ID == data.ID_PIC ? "selected" : ""}>${item.INITIALS}</option>`
                    })
                    return `<select ${data.ID != 0 ? `id="editPICON${data.ID}"` : ''} name="editPICON" class="select2-js-kpi form-control" required>${level}</select>`;

                }
            },
        ],
        
        columnDefs: [
            { targets: 3, className: 'description-kpi' },
            { targets: 4, className: 'bobot-kpi' },
        ],
        drawCallback: function (settings) {
            // Reinitialize Select2 on draw
            $('.select2-js-kpi').select2({
                width: "100%",
                placeholder: "Pilih..."
            });
        }
    });

    tableMappingON.on('change', 'select[name="editKPION"]', function () {
        // Get the selected value
        let selectedValue = $(this).val();
        // Get the corresponding row data
        let rowData = tableMappingON.row($(this).closest('tr')).data();
        let descriptionKpiTd = $(this).closest('tr').find('.description-kpi');
        let bobotKpiTd = $(this).closest('tr').find('.bobot-kpi');
        let kpidata = rowData.KPICodes.find(item => item.KPI_CODE === selectedValue)

        if (descriptionKpiTd.length > 0 && bobotKpiTd.length > 0) {
            descriptionKpiTd.text(kpidata ? kpidata.KPI_ITEM : '');
            bobotKpiTd.text(kpidata ? `${kpidata.BOBOT * 100}%` : '0%');
        }

    });

    let buttonAddMappingON = "";
    buttonAddMappingON = `<button type="button" class="btn btn-primary" id="EKI_btn" onclick="AddMappingON('${ID_KPI_ON}')">ADD</button>`;
    $("#btn_AddMappingON").html(buttonAddMappingON);
} 

function EditKPIDS(KPI_DS_ID, KPI_DS_DESC) {
    $("#KPI_DS_Head").val(KPI_DS_DESC);
    $("#ID_KPI_DS_Head").val(KPI_DS_ID);
    $('#modalAdd_DS').modal('show');

    $('#tbl_MappingKPI').DataTable().destroy();
    tabelMapping = $("#tbl_MappingKPI").DataTable({
        ajax: {
            url: `${$("#hd_path").val()}api/v2/kpi/TabelMappingKPIDS?ID_KPI_DS=${KPI_DS_ID}`,
            type: "GET",
            dataSrc: "Data"
        },
        autoWidth: false,
        ordering: false, // Disable sorting
        columns: [
            { data: 'ID', visible: false },
            {
                data: null,
                render: function (data, type, row) {
                    var html = `<div class="btn-group mb-3" role="group" aria-label="Basic example">
                                        <button type="button" class="btn btn-danger" onclick="DeleteMapping(this)"><i class="bi bi-trash"></i></button>
                                    </div>`;
                    return html;
                }
            },
            //"KPI_CODE"
            {
                data: null,
                render: function (data, type, row, meta) {
                    let kpi = '<option value=""></option>';
                    data.KPICodes.forEach(item => {
                        kpi += `<option value="${item.KPI_CODE}" ${item.KPI_CODE == data.KPI_CODE ? "selected" : ""}>${item.KPI_CODE} - ${item.KPI_ITEM}</option>`;
                    });
                    return `<select ${data.ID != 0 ? `id="editKPI${data.ID}"` : ''} name="editKPI" class="select2-js-kpi" required>${kpi}</select> `;
                }
            },
            { data: "KPI_ITEM" },
            //"BOBOT"
            {
                data: null,
                render: function (data, type, row, meta) {
                    if (data.BOBOT == null) {
                        return null;
                    } else {
                        return (data.BOBOT * 100) + '%';
                    }
                }
            },
            //"INITIALS"
            {
                data: null,
                render: function (data, type, row, meta) {
                    let level = '<option value=""></option>';
                    data.PICS.forEach(item => {
                        level += `<option value="${item.ID}" ${item.ID == data.ID_PIC_OFFICER_NONOM ? "selected" : ""}>${item.INITIALS}</option>`
                    })
                    return `<select ${data.ID != 0 ? `id="editPIC${data.ID}"` : ''} name="editPIC" class="select2-js-kpi form-control" required>${level}</select>`;
                }
            },
        ],
        columnDefs: [
            { targets: 2, width: "30%" },
            { targets: 3, className: 'description-kpi' },
            { targets: 4, className: 'bobot-kpi' },
        ],
        drawCallback: function (settings) {
            // Reinitialize Select2 on draw
            $('.select2-js-kpi').select2({
                width: "100%",
                placeholder: "Pilih..."
            });
        }
    })
    tabelMapping.on('change', 'select[name="editKPI"]', function () {
        // Get the selected value
        let selectedValue = $(this).val();
        // Get the corresponding row data
        let rowData = tabelMapping.row($(this).closest('tr')).data();
        let descriptionKpiTd = $(this).closest('tr').find('.description-kpi');
        let bobotKpiTd = $(this).closest('tr').find('.bobot-kpi');
        let kpidata = rowData.KPICodes.find(item => item.KPI_CODE === selectedValue)

        if (descriptionKpiTd.length > 0 && bobotKpiTd.length > 0) {
            descriptionKpiTd.text(kpidata ? kpidata.KPI_ITEM : '');
            bobotKpiTd.text(kpidata ? `${kpidata.BOBOT * 100}%`  : '0%');
        }

    });

    let buttonAddMapping = "";
    buttonAddMapping = `<button type="button" class="btn btn-primary" id="KPIDS_btn" onclick="AddMapping('${KPI_DS_ID}')">ADD</button>`;
    $("#btn_AddMapping").html(buttonAddMapping);
}


const getKPIONParent = () => {
    $.ajax({
        type: "GET",
        dataType: "json",
        contentType: "application/json",
        url: $("#hd_path").val() + "GetKPIONMappingParent",
        success: function (result) {
            if (result.Status) {
                var l = result.data;// Remove all options using jQuery
                var parentKPI = '<option value=""></option>';
                $.each(l, function (key, val) {
                    //console.log(val.LEVEL_TRAINING);
                    parentKPI += '<option value="' + val.KPI_CODE + '">' + val.KPI_CODE + " - " + val.KPI_ITEM + '</option>';
                });
                $("#onItem").html(parentKPI);
                $("#onItem").select2({
                    width: '100%',
                    dropdownParent: $("#modalAdd_MasterON")
                });
            }
            else {
                alert(result.Error);
            }
        }
    })
}

function DeleteMapping(e) {
    let tabelMapping = $("#tbl_MappingKPI").DataTable();
    let row = $(e).closest("tr");
    //remove row DOM
    tabelMapping.row(row).remove().draw();
}

function DeleteMappingON(e) {
    let tabelMapping = $("#tbl_MappingKPION").DataTable();
    let row = $(e).closest("tr");
    //remove row DOM
    tabelMapping.row(row).remove().draw();
}

function AddMapping(ID_KPI_DS) {
    $.ajax({
        url: `${$("#hd_path").val()}api/v2/kpi/DOMTableMappingKPIDS?ID_KPI_DS=${ID_KPI_DS}`,
        type: "GET",
        success: function (response) {
            let newRow = response.Data;
            let tabelMapping = $("#tbl_MappingKPI").DataTable();

            // Add the new row to the underlying data
            tabelMapping.row.add(newRow).draw();
        },
        errorr: function (error) {
            alert("Failed to add KPI")
        }
    })
}

function AddMappingON(ID_KPI_ON) {
    $.ajax({
        url: `${$("#hd_path").val()}api/v2/kpi/DOMTableMappingKPION?ID_KPI_ON=${ID_KPI_ON}`,
        type: "GET",
        success: function (response) {
            let newRow = response.Data;
            let tabelMapping = $("#tbl_MappingKPION").DataTable();

            // Add the new row to the underlying data
            tabelMapping.row.add(newRow).draw();
        },
        errorr: function (error) {
            alert("Failed to add KPI")
        }
    })
}

const DownloadTemplate = () => {
    $.ajax({
        type: "GET",
        url: $("#hd_path").val() + "GenerateTemplateMappingKPI",
        success: function (result) {
            if (result.Status == true) {
                var path = $("#hd_path").val() + "Files/TemplateMappingKPI.xlsx"
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

function resetModal_DS() {
    $("#FormKPI_MappingON")[0].reset();
    $(".select2").val("").trigger("change");
    getKPIONParent();
}