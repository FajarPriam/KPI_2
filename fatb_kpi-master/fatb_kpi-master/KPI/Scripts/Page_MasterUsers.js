$(document).ready(function () {
    getRole();
    getUserELP();
    $(".select2").select2({
        placeholder: "Select an option"
    });
});

var table = $("#tbl_users").DataTable({
    ajax: {
        url: $("#hd_path").val() + "getAllUser",
        type: "GET",
        datatype: "json",
        dataSrc: "data"
    },
    scrollX: true,
    autoWidth: false,
    responsive: false,
    columns: [
        {
            data: "USER",
            render: function (data, type, row) {
                var html = `<div class="btn-group mb-3" role="group" aria-label="Basic example">
                                        <button type="button" class="btn btn-info" value="${row.ID}" data-role="${row.ID_ROLE}" data-user="${row.USER}" onclick="GetEditUser(this)"><i class="bi bi-pencil"></i></button>
                                        <button type="button" class="btn btn-danger" onclick="DeleteUser(${row.ID})"><i class="bi bi-trash"></i></button>
                                    </div>`;
                return html;
            }
        },
        { data: "USER" },
        { data: "NAME" },
        { data: "ROLE" },
        { data: "POS_TITLE" },
        { data: "DSTRCT_CODE" }, 
    ],
});

function getRole() {
    $.ajax({
        type: "GET",
        dataType: "json",
        contentType: "application/json",
        url: $("#hd_path").val() + "getRole",
        success: function (result) {
            if (result.Status) {
                var l = result.Data;
                $.each(l, function (key, item) {
                    $("#role").append(new Option(item.ROLE, item.ID));
                })
            }
            else {
                alert(result.Error);
            }
        }
    })
}

function getUserELP() {
    $.ajax({
        type: "GET",
        dataType: "json",
        contentType: "application/json",
        url: $("#hd_path").val() + "getUserELP",
        success: function (result) {
            if (result.Status) {
                var data = result.data;
                $.each(data, function (key, item) {
                    var name = item.EMPLOYEE_ID + '-' + item.NAME;
                    $("#username").append(new Option(name, item.EMPLOYEE_ID));
                })
            }
            else {
                alert(result.Error);
            }
        }
    })
}

function DeleteUser(id) {

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
                url: $("#hd_path").val() + "DeleteUser/" + id,
                success: function (result) {
                    if (result.Status == true) {
                        Swal.fire(
                            'Deleted!',
                            'User has been deleted.',
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

function InsertOrUpdateUser() {
    var obj = {
        ID: $("#id_user").val()
        , ID_ROLE: $("#role").val()
        , NRP: $("#username").val()
    }


    var spinner = `<span class="spinner-border spinner-border-md" role="status" aria-hidden="true"></span> Loading...`;
    $('#btn_submit').prop('disabled', true);
    $('#btn_submit').html(spinner);

    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json",
        url: $("#hd_path").val() + "CreateOrUpdateUser",
        data: JSON.stringify(obj),
        success: function (result) {
            $('#modalAdd').modal('hide');
            if (result.Status == true) {
                Swal.fire(
                    'Inserted!',
                    'User has been insert.',
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

function GetEditUser(button) {
    var role = button.getAttribute("data-role");
    var user = button.getAttribute("data-user");
    var id = $(button).val();

    console.log(id)

    $('#role').val(role).trigger('change');
    $('#username').val(user).trigger('change');
    $('#id_user').val(id);
    $('#modalAdd').modal('show');
}

function resetModal() {
    $('#role').val("").trigger('change');
    $('#username').val("").trigger('change');
    $('#id_user').val("");
}