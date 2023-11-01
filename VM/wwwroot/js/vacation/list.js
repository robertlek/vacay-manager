let dataTable;

const departmentId = $("#department-id").val();

$(document).ready(() => {
    loadDataTable(departmentId);
});

function loadDataTable(departmentId) {
    dataTable = $("#vacations-table").DataTable({
        "ajax": {
            "url": `/Public/Vacation/GetAllVacations?departmentId=${departmentId}`
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
            },
            {
                targets: 5,
                className: "dt-head-center dt-body-center"
            }
        ],
        "columns": [
            {
                "data": "fromDate",
                "render": function (data) {
                    var date = new Date(data);

                    var year = date.getFullYear();
                    var month = (date.getMonth() + 1).toString().padStart(2, '0');
                    var day = date.getDate().toString().padStart(2, '0');
                    var formattedDate = `${year}-${month}-${day}`;

                    return `${formattedDate}`;
                }
            },
            {
                "data": "toDate",
                "render": function (data) {
                    var date = new Date(data);

                    var year = date.getFullYear();
                    var month = (date.getMonth() + 1).toString().padStart(2, '0');
                    var day = date.getDate().toString().padStart(2, '0');
                    var formattedDate = `${year}-${month}-${day}`;

                    return `${formattedDate}`;
                }
            },
            {
                "render": (_data, _type, row) => {
                    var date = calculateDaysBetweenDates(row.fromDate, row.toDate);
                    return `${date}`
                }
            },
            {
                "data": "employee",
                "render": (data) => {
                    return `${data.firstName} ${data.lastName}`;
                }
            },
            {
                "data": "employee.email"
            },
            {
                "data": "createdOn",
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

function calculateDaysBetweenDates(startDate, endDate) {
    startDate = new Date(startDate);
    endDate = new Date(endDate);

    var difference = endDate - startDate;
    difference = difference / (1000 * 60 * 60 * 24);

    return Math.floor(difference);
}
