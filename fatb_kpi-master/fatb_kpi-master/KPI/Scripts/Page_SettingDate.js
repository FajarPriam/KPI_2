var table = $("#tbl_date").DataTable({
    ajax: {
        url: $("#hd_path").val() + "getSettingDate",
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
                                        <button type="button" class="btn btn-info" value="${row.PERIODE}" data-role="${row.PERIODE}" onclick="GetEditRole(this)"><i class="bi bi-pencil"></i></button>
                                        <button type="button" class="btn btn-danger" value="${row.PERIODE}" onclick="DeleteRole(this.value)"><i class="bi bi-trash"></i></button>
                                    </div>`;
                return html;
            }
        },
        { data: "PERIODE" },
        { data: "START_DATE" },
        { data: "FINISH_DATE" }
    ],
});