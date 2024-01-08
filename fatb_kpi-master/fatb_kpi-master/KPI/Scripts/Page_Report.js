var tableON = $("#tbl_kpi").DataTable({
    ajax: {
        url: $("#hd_path").val() + "GetNilaiEvaluasi_ON",
        type: "GET",
        datatype: "json",
        dataSrc: "data"
    },
    scrollX: true,
    autoWidth: false,
    responsive: false,
    columns: [
        { data: "DSTRCT" },
        { data: "ITEM_ON" },
        { data: "nilai_evaluasi" },
        { data: "POINT" },
        { data: "BOBOT" },
        { data: "PERIODE" },
        { data: "YEAR" },
        { data: "DEPT" },
        { data: "ITEM_DS" },
        { data: "ITEM_OF" },

    ],
});

var tableDS = $("#tbl_kpiDS").DataTable({
    ajax: {
        url: $("#hd_path").val() + "GetNilaiKPI_DS",
        type: "GET",
        datatype: "json",
        dataSrc: "data"
    },
    scrollX: true,
    autoWidth: false,
    responsive: false,
    columns: [
        { data: "ITEM" },
        { data: "NILAI_KPI" },
        { data: "PERIODE" },
        { data: "YEAR" },
        { data: "DSTRCT" },

    ],
});

var tableOF = $("#tbl_kpiOF").DataTable({
    ajax: {
        url: $("#hd_path").val() + "GetNilaiKPI_OF",
        type: "GET",
        datatype: "json",
        dataSrc: "data"
    },
    scrollX: true,
    autoWidth: false,
    responsive: false,
    columns: [
        { data: "ITEM" },
        { data: "ITEM_DS" },
        { data: "NILAI_KPI" },
        { data: "PERIODE" },
        { data: "YEAR" },
        { data: "DSTRCT" },

    ],
});