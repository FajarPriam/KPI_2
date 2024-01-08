var tableDS = $("#tbl_kpi_ds").DataTable({
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