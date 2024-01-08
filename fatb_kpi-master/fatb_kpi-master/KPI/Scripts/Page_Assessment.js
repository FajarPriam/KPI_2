var tableAssessment;
var tableAssessmentON;
var tableAssessmentON_baru;
var excelAssessment_DS; 
var excelAssessment_ON;


var ExcelToJSON = function () {
    this.parseExcel = function (file) {
        var reader = new FileReader();
        reader.onload = function (e) {
            var data = e.target.result;
            var workbook = XLSX.read(data, {
                type: 'binary'
            });

            for (var i = 0; i < workbook.SheetNames.length; i++) {
                if (workbook.SheetNames[i] == "ASSESSMENT_DS") {
                    var sheet = workbook.SheetNames[i];
                    var XL_row_object = XLSX.utils.sheet_to_json(workbook.Sheets[sheet], { defval: "" });
                    var json_object = JSON.stringify(XL_row_object);
                    excelAssessment_DS = JSON.parse(json_object);
                    for (let i = 0; i < excelAssessment_DS.length; i++) {
                        delete excelAssessment_DS[i].ID_TEMP;
                        delete excelAssessment_DS[i].ID;
                        delete excelAssessment_DS[i].CABANG;
                    }
                    console.log(excelAssessment_DS);
                    //break; // Exit the loop once the sheet is found
                } else if (workbook.SheetNames[i] == "ASSESSMENT_ON") {
                    var sheet = workbook.SheetNames[i];
                    var XL_row_object = XLSX.utils.sheet_to_json(workbook.Sheets[sheet], { defval: "" });
                    var json_object = JSON.stringify(XL_row_object);
                    excelAssessment_ON = JSON.parse(json_object);
                    //for (let i = 0; i < excelKPI_DS.length; i++) {
                    //    delete excelKPI_DS[i].EDIT_BY;
                    //}
                    console.log(excelAssessment_ON);
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

document.getElementById('excel_Assessment').addEventListener('change', handleFileSelect, false);


const SaveKPI = () => {
    var pointKPI = $("input[name='nilai']");

    var kpiObjects = [];

    var INPUT_BY = $("#hd_nrp").val();
    var DSTRCT = $("#district").val();//$("#hd_dstrct").val();

    pointKPI.each(function () {

        var input = $(this);
        var ID_KPI_ON = input.data("id");
        var POINT = input.val();

        var kpiObject = {
            ID_KPI_ON: ID_KPI_ON,
            INPUT_BY: INPUT_BY,
            DSTRCT: DSTRCT,
            POINT: POINT
        };

        kpiObjects.push(kpiObject);
    });

    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json",
        url: $("#hd_path").val() + "PenilaianKPI",
        data: JSON.stringify(kpiObjects),
        success: function (result) {
            if (result.Status == true) {
                Swal.fire(
                    'Inserted!',
                    'Point has been saved.',
                    'success'
                );
                table.ajax.reload();
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

const DownloadTemplate = () => {
    $.ajax({
        type: "GET",
        url: $("#hd_path").val() + "GenerateExcelAssessment",
        success: function (result) {
            if (result.Status == true) {
                var path = $("#hd_path").val() + "Files/TemplateAssessmentKPI.xlsx"
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

$("#formUploadNilai").submit(function (e) {
    e.preventDefault(); // Prevent the default form submission

    var formData = new FormData($("#formUploadNilai")[0]); // Create FormData object from the form

    $.ajax({
        url: $(this).attr("action"),
        type: "POST",
        data: formData, // Use FormData object
        processData: false, // Important! Prevent jQuery from processing the data
        contentType: false, // Let jQuery set the correct content type
        success: function (data) {
            if (data.Status === true) {
                // Call SweetAlert2 when Status is true
                Swal.fire({
                    icon: "success",
                    title: "Success!",
                    text: "Form submitted successfully."
                });
            } else {
                // Handle the case when Status is false
                Swal.fire({
                    icon: "error",
                    title: "Error!",
                    text: data.Message
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

function getDistrict() {
    $.ajax({
        type: "GET",
        dataType: "json",
        contentType: "application/json",
        url: $("#hd_path").val() + "getDistrict",
        success: function (result) {
            if (result.Status) {
                var data = result.data;
                $.each(data, function (key, item) {
                    var name = item.DSTRCT_CODE;
                    $("#district").append(new Option(name, item.DSTRCT_CODE));
                })
            }
            else {
                alert(result.Error);
            }
        }
    })
}

function setDistrict() {
    var d = $("#district").val();
    //document.getElementById('district').innerHTML = d;
    console.log(d);
    $("[name='dstrct']").empty();
    $("[name='dstrct']").append(d);
}

function FilterKPI() {
    if ($("#district").val() == '' && $("#periode").val() == '') {
        document.getElementById('warningDistrik').style.visibility = "visible";
        document.getElementById('warningPeriode').style.visibility = "visible";
    } else if (($("#periode").val() == '' || $("#periode").val() < 1 || $("#periode").val() > 12) && $("#district").val() != '') {
        document.getElementById('warningDistrik').style.visibility = "hidden";
        document.getElementById('warningPeriode').style.visibility = "visible";
    } else if ($("#periode").val() >= 1 && $("#periode").val() <= 12 && $("#district").val() == '') {
        document.getElementById('warningDistrik').style.visibility = "visible";
        document.getElementById('warningPeriode').style.visibility = "hidden";
    } else {
        document.getElementById('warningDistrik').style.visibility = "hidden";
        document.getElementById('warningPeriode').style.visibility = "hidden";
        document.getElementById('cardUpload').style.display = "inline";
        document.getElementById('cardTabelDS').style.display = "inline";
        document.getElementById('cardTabelON').style.display = "inline";
        console.log($("#district").val());
        console.log($("#periode").val());
        //$('#tbl_assessment_DS').DataTable().destroy();
        //tableAssessment = $("#tbl_assessment_DS").DataTable({
        //    ajax: {
        //        url: $("#hd_path").val() + "GetTabelAssessmentDS_ByFilter?district=" + $("#district").val() + "&periode=" + $("#periode").val(),
        //        type: "GET",
        //        datatype: "json",
        //        dataSrc: "data"
        //    },
        //    //scrollX: true,
        //    autoWidth: false,
        //    //responsive: false,
        //    order: [],
        //    columns: [
        //        { data: "ID" },
        //        { data: "KPI_DS_DESC" },
        //        { data: "KPI_CODE" },
        //        { data: "KPI_ITEM" },
        //        {
        //            data: null,
        //            render: function (data, type, row, meta) {
        //                if (data.BOBOT == null) {
        //                    return null;
        //                } else {
        //                    return (data.BOBOT * 100) + '%';
        //                }

        //            }
        //        },
        //        {
        //            data: null,
        //            render: function (data, type, row, meta) {
        //                if (data.JUMLAH_BOBOT == null) {
        //                    return null;
        //                } else {
        //                    return (data.JUMLAH_BOBOT * 100) + '%';
        //                }

        //            }
        //        },
        //        { data: "TARGET" },
        //        {
        //            data: null,
        //            render: function (data, type, row) {
        //                if (data.CABANG == 1) {
        //                    return `<input type="number" id="planAch_${data.ID}" value="${data.PLAN_ACH == null ? 100 : (data.PLAN_ACH * 100).toFixed(0)}" class="form-control" readonly>`
        //                } else {
        //                    return `<input type="number" id="planAch_${data.ID}" value="${data.PLAN_ACH == null ? 100 : (data.PLAN_ACH * 100).toFixed(0)}" class="form-control">`
        //                }
        //            }
        //        },
        //        {
        //            data: null,
        //            render: function (data, type, row) {
        //                if (data.CABANG == 1) {
        //                    return `<input type="text" id="actual_${data.ID}" value="${data.ACTUAL == null ? "" : data.ACTUAL}" class="form-control" readonly>`
        //                } else {
        //                    return `<input type="text" id="actual_${data.ID}" value="${data.ACTUAL == null ? "" : data.ACTUAL}" class="form-control">`
        //                }
        //            }
        //        },
        //        {
        //            data: null,
        //            render: function (data, type, row) {
        //                if (data.CABANG == 1) {
        //                    return `<input type="number" id="achievment_${data.ID}" value="${data.ACHIEVMENT == null ? "" : (data.ACHIEVMENT * 100).toFixed(0)}" class="form-control" readonly>`
        //                } else {
        //                    return `<input type="number" id="achievment_${data.ID}" value="${data.ACHIEVMENT == null ? "" : (data.ACHIEVMENT * 100).toFixed(0)}" class="form-control">`
        //                }
        //            }
        //        },
        //        { data: "NILAI" },
        //        { data: "NILAI_FOR_KABAG" },
        //        { data: "JUMLAH_NILAI_FOR_KABAG" },
        //        {
        //            data: null,
        //            render: function (data, type, row, meta) {
        //                if (data.ID == null) {
        //                    return null;
        //                } else {
        //                    return `<textarea id="remarks_${data.ID}">${data.REMARK == null ? "" : data.REMARK}</textarea>`;
        //                }
        //            }
        //        },
        //    ],
        //});
        //tableAssessment.column(0).visible(false);

        //let buttonSubmitAssessmentDS = "";
        //buttonSubmitAssessmentDS = '<button type="button" class="btn btn-primary w-100" id="EKI_btn" onclick="SubmitAssessmentDS()">Submit</button>';
        //$("#btn_SubmitAssessmentDS").html(buttonSubmitAssessmentDS);

        $('#tbl_assessment_DS_baru').DataTable().destroy();
        tableAssessment = $("#tbl_assessment_DS_baru").DataTable({
            ajax: {
                url: $("#hd_path").val() + "GetAssessmentDS?district=" + $("#district").val() + "&periode=" + $("#periode").val(),
                type: "GET",
                datatype: "json",
                dataSrc: "data"
            },
            //scrollX: true,
            autoWidth: false,
            //responsive: false,
            order: [],
            columns: [
                { data: "ID" },
                { data: "KPI_CODE" },
                { data: "KPI_ITEM" },
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
                { data: "TARGET" },
                {
                    data: null,
                    render: function (data, type, row) {
                        if (data.ID == null) {
                            return null;
                        } else {
                            //if (data.CABANG == 1) {
                            //    return `<input type="number" id="planAch_${data.ID}" value="${data.PLAN_ACH == null ? 100 : (data.PLAN_ACH * 100).toFixed(0)}" class="form-control" readonly>`
                            //} else {
                                return `<input type="number" id="planAch_${data.ID}" value="${data.PLAN_ACH == null ? 100 : (data.PLAN_ACH * 100).toFixed(0)}" class="form-control">`
                            //}
                        }
                    }
                },
                {
                    data: null,
                    render: function (data, type, row) {
                        if (data.ID == null) {
                            return null;
                        } else {
                            if (data.CABANG == 1) {
                                return `<input type="text" id="actual_${data.ID}" value="${data.ACTUAL == null ? "" : data.ACTUAL}" class="form-control" readonly>`
                            } else {
                                return `<input type="text" id="actual_${data.ID}" value="${data.ACTUAL == null ? "" : data.ACTUAL}" class="form-control">`
                            }
                        }
                    }
                },
                {
                    data: null,
                    render: function (data, type, row) {
                        if (data.ID == null) {
                            return null;
                        } else {
                            if (data.CABANG == 1) {
                                return `<input type="number" id="achievment_${data.ID}" value="${data.ACHIEVMENT == null ? "" : (data.ACHIEVMENT * 100).toFixed(0)}" class="form-control" readonly>`
                            } else {
                                return `<input type="number" id="achievment_${data.ID}" value="${data.ACHIEVMENT == null ? "" : (data.ACHIEVMENT * 100).toFixed(0)}" class="form-control">`
                            }
                        }
                    }
                },
                { data: "NILAI" },
                { data: "NILAI_FOR_KABAG" },
                {
                    data: null,
                    render: function (data, type, row, meta) {
                        if (data.ID == null) {
                            return null;
                        } else {
                            return `<textarea id="remarks_${data.ID}">${data.REMARK == null ? "" : data.REMARK}</textarea>`;
                        }
                    }
                },
            ],
            rowCallback: function (row, data) {
                // Assuming you want to make a row bold based on a specific condition (replace with your own condition)
                if (data.ID === null) {
                    $(row).addClass('bold-row');
                    $(row).css('background-color', '#ADD8E6')
                }
            }
        });
        tableAssessment.column(0).visible(false);

        let buttonSubmitAssessmentDS = "";
        buttonSubmitAssessmentDS = '<button type="button" class="btn btn-primary w-100" id="EKI_btn" onclick="SubmitAssessmentDS()">Submit</button>';
        $("#btn_SubmitAssessmentDS").html(buttonSubmitAssessmentDS);


        //$('#tbl_assessment_ON').DataTable().destroy();
        //tableAssessmentON = $("#tbl_assessment_ON").DataTable({
        //    ajax: {
        //        url: $("#hd_path").val() + "GetTabelAssessmentON_ByFilter?district=" + $("#district").val() + "&periode=" + $("#periode").val(),
        //        type: "GET",
        //        datatype: "json",
        //        dataSrc: "data"
        //    },
        //    //scrollX: true,
        //    autoWidth: false,
        //    //responsive: false,
        //    order: [],
        //    columns: [
        //        { data: "ID" },
        //        { data: "ID_KPI_ON" },
        //        { data: "KPI_ITEM" },
        //        { data: "KPI_CODE" },
        //        { data: "KPI_DESC" },
        //        {
        //            data: null,
        //            render: function (data, type, row, meta) {
        //                if (data.BOBOT == null) {
        //                    return null;
        //                } else {
        //                    return (data.BOBOT * 100) + '%';
        //                }

        //            }
        //        },
        //        {
        //            data: null,
        //            render: function (data, type, row, meta) {
        //                if (data.JUMLAH_BOBOT == null) {
        //                    return null;
        //                } else {
        //                    return (data.JUMLAH_BOBOT * 100) + '%';
        //                }

        //            }
        //        },
        //        { data: "TARGET" },
        //        {
        //            data: null,
        //            render: function (data, type, row) {
        //                    return `<input type="text" id="actualON_${data.ID}" value="${data.ACTUAL == null ? "" : data.ACTUAL}" class="form-control">`
        //            }
        //        },
        //        {
        //            data: null,
        //            render: function (data, type, row) {
        //                    return `<input type="number" id="achievmentON_${data.ID}" value="${data.ACHIEVMENT == null ? "" : (data.ACHIEVMENT * 100).toFixed(0)}" class="form-control">`
        //            }
        //        },
        //        { data: "NILAI" },
        //        { data: "NILAI_FOR_KABAG" },
        //        { data: "JUMLAH_NILAI_FOR_KABAG" },
        //        {
        //            data: null,
        //            render: function (data, type, row, meta) {
        //                if (data.ID == null) {
        //                    return null;
        //                } else {
        //                    return `<textarea id="remarksON_${data.ID}">${data.REMARK == null ? "" : data.REMARK}</textarea>`;
        //                }
        //            }
        //        },
        //    ],
        //});
        //tableAssessmentON.column(0).visible(false);

        //let buttonSubmitAssessmentON = "";
        //buttonSubmitAssessmentON = '<button type="button" class="btn btn-primary w-100" id="EKI_btn" onclick="SubmitAssessmentON()">Submit</button>';
        //$("#btn_SubmitAssessmentON").html(buttonSubmitAssessmentON);


        $('#tbl_assessment_ON_baru').DataTable().destroy();
        tableAssessmentON_baru = $("#tbl_assessment_ON_baru").DataTable({
            ajax: {
                url: $("#hd_path").val() + "GetAssessmentON?district=" + $("#district").val() + "&periode=" + $("#periode").val(),
                type: "GET",
                datatype: "json",
                dataSrc: "data"
            },
            //scrollX: true,
            autoWidth: false,
            //responsive: false,
            order: [],
            columns: [
                { data: "ID" },
                //{ data: "ID_KPI_ON" },
                //{ data: "KPI_ITEM" },
                { data: "KPI_CODE" },
                { data: "KPI_DESC" },
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
                { data: "TARGET" },
                {
                    data: null,
                    render: function (data, type, row) {
                        if (data.ID == null) {
                            return null;
                        } else {
                            return `<input type="text" id="actualON_${data.ID}" value="${data.ACTUAL == null ? "" : data.ACTUAL}" class="form-control">`
                        }
                    }
                },
                {
                    data: null,
                    render: function (data, type, row) {
                        if (data.ID == null) {
                            return null;
                        } else {
                            return `<input type="number" id="achievmentON_${data.ID}" value="${data.ACHIEVMENT == null ? "" : (data.ACHIEVMENT * 100).toFixed(0)}" class="form-control">`
                        }
                    }
                },
                { data: "NILAI" },
                { data: "NILAI_FOR_KABAG" },
                {
                    data: null,
                    render: function (data, type, row, meta) {
                        if (data.ID == null) {
                            return null;
                        } else {
                            return `<textarea id="remarksON_${data.ID}">${data.REMARK == null ? "" : data.REMARK}</textarea>`;
                        }
                    }
                },
            ],
            rowCallback: function (row, data) {
                // Assuming you want to make a row bold based on a specific condition (replace with your own condition)
                if (data.ID === null) {
                    $(row).addClass('bold-row');
                    $(row).css('background-color', '#ADD8E6')
                }
            }
        });
        tableAssessmentON_baru.column(0).visible(false);
        let buttonSubmitAssessmentON = "";
        buttonSubmitAssessmentON = '<button type="button" class="btn btn-primary w-100" id="EKI_btn" onclick="SubmitAssessmentON()">Submit</button>';
        $("#btn_SubmitAssessmentON_baru").html(buttonSubmitAssessmentON);
    }
}

$(document).ready(function () {
    $("#periode").val((new Date()).getMonth());
    getDistrict();

    $("#formUploadNilai").submit(function (e) {
        e.preventDefault(); // Prevent the default form submission

        for (i = 0; i < excelAssessment_DS.length; i++) {
            excelAssessment_DS[i].DISTRICT = $('#district').val();
            excelAssessment_DS[i].PERIODE = $('#periode').val();
        }

        for (i = 0; i < excelAssessment_ON.length; i++) {
            excelAssessment_ON[i].DISTRICT = $('#district').val();
            excelAssessment_ON[i].PERIODE = $('#periode').val();
        }

        console.log({
            clsAssessment_Ds: excelAssessment_DS,
            clsAssessment_On: excelAssessment_ON
        });
        console.log('masuk data');
        $.ajax({
            url: $("#hd_path").val() + "Create_ExcelAssessment",
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            data: JSON.stringify({
                clsAssessment_Ds: excelAssessment_DS,
                clsAssessment_On: excelAssessment_ON
            }),
            success: function (data) {
                if (data.Remarks == true) {
                    Swal.fire({
                        icon: "success",
                        title: "Success!",
                        text: "Form submitted successfully."
                    });
                    $("#formUploadNilai")[0].reset();
                    tableAssessment.ajax.reload();
                    tableAssessmentON.ajax.reload();
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
});

function SubmitAssessmentDS() {
    var AssessmentDS = [];
    var id_AssessmentDS = tableAssessment.column(0).data();
    var kpiCode_AssessmentDS = tableAssessment.column(1).data();
    console.log(id_AssessmentDS);
    for (s = 0; s < id_AssessmentDS.length; s++) {
        if (id_AssessmentDS[s] != null) {
            var objectAssessmentDS = {
                KPI_CODE: kpiCode_AssessmentDS[s],
                DISTRICT: $('#district').val(),
                PERIODE: $('#periode').val(),
                PLAN_ACH: isNaN(parseInt($(`#planAch_${id_AssessmentDS[s]}`).val(), 10)) ? 0 : parseInt($(`#planAch_${id_AssessmentDS[s]}`).val(), 10),
                ACTUAL: $(`#actual_${id_AssessmentDS[s]}`).val(),
                ACHIEVMENT: isNaN(parseInt($(`#achievment_${id_AssessmentDS[s]}`).val(), 10)) ? 0 : parseInt($(`#achievment_${id_AssessmentDS[s]}`).val(), 10),
                REMARK: $(`#remarks_${id_AssessmentDS[s]}`).val(),
            }

            AssessmentDS.push(objectAssessmentDS);
        }
    }
    console.log(AssessmentDS);

    $.ajax({
        url: $("#hd_path").val() + "Create_Assessment_DS",
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: JSON.stringify(AssessmentDS),
        type: "POST",
        success: function (data) {
            if (data.Remarks === true) {
                // Call SweetAlert2 when Status is true
                Swal.fire({
                    icon: "success",
                    title: "Success!",
                    text: "Form submitted successfully."
                });
                tableAssessment.ajax.reload();
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
}

function SubmitAssessmentON() {
    //var AssessmentON = [];
    //var id_AssessmentON = tableAssessmentON.column(0).data();
    //var kpiCode_AssessmentON = tableAssessmentON.column(3).data();
    //console.log(id_AssessmentON);
    //for (s = 0; s < id_AssessmentON.length; s++) {
    //    var objectAssessmentON = {
    //        KPI_CODE: kpiCode_AssessmentON[s],
    //        DISTRICT: $('#district').val(),
    //        PERIODE: $('#periode').val(),
    //        ACTUAL: $(`#actualON_${id_AssessmentON[s]}`).val(),
    //        ACHIEVMENT: isNaN(parseInt($(`#achievmentON_${id_AssessmentON[s]}`).val(), 10)) ? 0 : parseInt($(`#achievmentON_${id_AssessmentON[s]}`).val(), 10),
    //        REMARK: $(`#remarksON_${id_AssessmentON[s]}`).val(),
    //    }

    //    AssessmentON.push(objectAssessmentON);
    //}
    //console.log(AssessmentON);

    var AssessmentON = [];
    var id_AssessmentON = tableAssessmentON_baru.column(0).data();
    var kpiCode_AssessmentON = tableAssessmentON_baru.column(1).data();
    console.log(id_AssessmentON);
    console.log(kpiCode_AssessmentON);
    for (s = 0; s < id_AssessmentON.length; s++) {
        if (id_AssessmentON[s] != null) {
            var objectAssessmentON = {
                KPI_CODE: kpiCode_AssessmentON[s],
                DISTRICT: $('#district').val(),
                PERIODE: $('#periode').val(),
                ACTUAL: $(`#actualON_${id_AssessmentON[s]}`).val(),
                ACHIEVMENT: isNaN(parseInt($(`#achievmentON_${id_AssessmentON[s]}`).val(), 10)) ? 0 : parseInt($(`#achievmentON_${id_AssessmentON[s]}`).val(), 10),
                REMARK: $(`#remarksON_${id_AssessmentON[s]}`).val(),
            }
            AssessmentON.push(objectAssessmentON);
        }
        
    }
    console.log(AssessmentON);

    $.ajax({
        url: $("#hd_path").val() + "Create_Assessment_ON",
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: JSON.stringify(AssessmentON),
        type: "POST",
        success: function (data) {
            if (data.Remarks === true) {
                // Call SweetAlert2 when Status is true
                Swal.fire({
                    icon: "success",
                    title: "Success!",
                    text: "Form submitted successfully."
                });
                tableAssessmentON_baru.ajax.reload();
                tableAssessment.ajax.reload();
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
}