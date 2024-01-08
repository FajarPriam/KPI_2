$(document).ready(function () {
    getDeptELP();
    $(".select2").select2({
        placeholder: "Select an option"
    });
});

var table = $("#tbl_dept").DataTable({
    ajax: {
        url: $("#hd_path").val() + "getAllDept",
        type: "GET",
        datatype: "json",
        dataSrc: "data"
    },
    scrollX: true,
    autoWidth: false,
    responsive: false,
    columns: [
        {
            data: "DEPT",
            render: function (data, type, row) {
                var html = `<div class="btn-group mb-3" role="group" aria-label="Basic example">
                                        <button type="button" class="btn btn-info" value="${row.ID}" data-dept="${row.DEPT}" data-code="${row.DEPT_CODE}" onclick="GetEditDept(this)"><i class="bi bi-pencil"></i></button>
                                        <button type="button" class="btn btn-danger" value="${row.ID}" onclick="DeleteDept(this.value)"><i class="bi bi-trash"></i></button>
                                    </div>`;
                return html;
            }
        },
        { data: "ID" },
        { data: "DEPT" },
        { data: "DEPT_CODE" },
    ],
});

function getDeptELP() {
    $.ajax({
        type: "GET",
        dataType: "json",
        contentType: "application/json",
        url: $("#hd_path").val() + "getDeptELP",
        success: function (result) {
            if (result.Status) {
                var l = result.data;
                $.each(l, function (key, item) {
                    $("#dept_code").append(new Option(item.DEPT_DESC, item.DEPT_CODE));
                })
            }
            else {
                alert(result.Error);
            }
        }
    })
}

function DeleteDept(id) {
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
                url: $("#hd_path").val() + "DeleteDept/" + id,
                success: function (result) {
                    if (result.Status == true) {
                        Swal.fire(
                            'Deleted!',
                            'Dept has been deleted.',
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
    })


}

function InsertOrUpdateDept() {
    var obj = {
        DEPT_CODE: $("#dept_code").val()
        , DEPT: $("#dept").val()
        , ID: $("#id_dept").val()
    }


    var spinner = `<span class="spinner-border spinner-border-md" role="status" aria-hidden="true"></span> Loading...`;
    $('#btn_submit').prop('disabled', true);
    $('#btn_submit').html(spinner);

    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json",
        url: $("#hd_path").val() + "CreateOrUpdateDept",
        data: JSON.stringify(obj),
        success: function (result) {
            $('#modalAdd').modal('hide');
            if (result.Status == true) {
                Swal.fire(
                    'Inserted!',
                    'Dept has been insert.',
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


            var spinDisable = `<i class="bx bx-check d-block d-sm-none"></i><span class="d-none d-sm-block">Submit</span>`;
            $('#btn_submit').prop('disabled', false);
            $('#btn_submit').html(spinDisable);
        }
    })
}

function GetEditDept(button) {
    var dept = button.getAttribute("data-dept");
    var code = button.getAttribute("data-code");
    var id = $(button).val();

    console.log(id)

    $('#dept_code').val(code).trigger('change');
    $('#id_dept').val(id);
    $('#dept').val(dept);
    $('#modalAdd').modal('show');
}

function resetModal() {
    $('#dept_code').val("").trigger('change');
    $('#id_dept').val("");
    $('#dept').val("");
}