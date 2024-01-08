$(document).ready(function () {

});

var table = $("#tbl_role").DataTable({
    ajax: {
        url: $("#hd_path").val() + "getRole",
        type: "GET",
        datatype: "json",
        dataSrc: "Data"
    },
    scrollX: true,
    autoWidth: false,
    responsive: false,
    columns: [
        {
            data: "ID",
            render: function (data, type, row) {
                var html = `<div class="btn-group mb-3" role="group" aria-label="Basic example">
                                        <button type="button" class="btn btn-info" value="${row.ID}" data-role="${row.ROLE}" onclick="GetEditRole(this)"><i class="bi bi-pencil"></i></button>
                                        <button type="button" class="btn btn-danger" value="${row.ID}" onclick="DeleteRole(this.value)"><i class="bi bi-trash"></i></button>
                                    </div>`;
                return html;
            }
        },
        { data: "ID" },
        { data: "ROLE" }
    ],
});

function DeleteRole(id) {

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
                url: $("#hd_path").val() + "DeleteRole/" + id,
                success: function (result) {
                    if (result.Status == true) {
                        Swal.fire(
                            'Deleted!',
                            'Role has been deleted.',
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

function InsertOrUpdateRole() {
    var obj = {
        ID: $("#id_role").val()
        , ROLE: $("#role").val()
    }


    var spinner = `<span class="spinner-border spinner-border-md" role="status" aria-hidden="true"></span> Loading...`;
    $('#btn_submit').prop('disabled', true);
    $('#btn_submit').html(spinner);

    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json",
        url: $("#hd_path").val() + "CreateOrUpdateRole",
        data: JSON.stringify(obj),
        success: function (result) {
            $('#modalAdd').modal('hide');
            if (result.Status == true) {
                Swal.fire(
                    'Inserted!',
                    'Role has been insert.',
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

function GetEditRole(button) {
    var role = button.getAttribute("data-role");
    var id = $(button).val();

    console.log(id)

    $('#role').val(role);
    $('#id_role').val(id);
    $('#modalAdd').modal('show');
}

function resetModal() {
    $('#role').val("");
    $('#id_role').val("");
}