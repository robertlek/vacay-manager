let dataTable;

const departmentId = $("#department-id").val();

$(document).ready(() => {
    loadDataTable(departmentId);
});

function loadDataTable(departmentId) {
    dataTable = $("#employee-table").DataTable({
        "ajax": {
            "url": `/Public/Vacation/GetAllEmployees?departmentId=${departmentId}`
        },
        "columnDefs": [
            {
                targets: 0,
                className: "dt-head-center dt-body-center"
            },
            {
                targets: 1,
                className: "dt-head-center dt-body-center"
            },
            {
                targets: 2,
                className: "dt-head-center dt-body-center"
            },
            {
                targets: 3,
                className: "dt-head-center dt-body-center"
            },
            {
                targets: 4,
                className: "dt-head-center dt-body-center"
            }
        ],
        "columns": [
            {
                "data": "firstName"
            },
            {
                "data": "lastName"
            },
            {
                "data": "email"
            },
            {
                "data": "phoneNumber"
            },
            {
                "data": "employedOn",
                "render": function (data) {
                    var date = new Date(data);

                    var year = date.getFullYear();
                    var month = (date.getMonth() + 1).toString().padStart(2, '0');
                    var day = date.getDate().toString().padStart(2, '0');
                    var formattedDate = `${year}-${month}-${day}`;

                    return `${formattedDate}`;
                }
            }
        ]
    });
}
