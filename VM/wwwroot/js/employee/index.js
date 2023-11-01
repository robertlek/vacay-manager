let dataTable;

$(document).ready(() => {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $("#employee-table").DataTable({
        "ajax": {
            "url": "/Admin/Employee/GetAllEmployees"
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
            },
            {
                targets: 6,
                className: "dt-head-center dt-body-center"
            }
        ],
        "columns": [
            {
                "data": "department.name"
            },
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
            },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <a href="/Admin/Employee/Update/${data}" class="btn btn-warning btn-sm shadow-none mx-1">
                            <i class="bi bi-pencil-square mx-1"></i>
                        </a>
                        <button onclick=askForDeleteEmployee('${data}') class="btn btn-danger btn-sm shadow-none mx-1">
                            <i class="bi bi-trash mx-1"></i>
                        </button>
                    `;
                }
            }
        ]
    });
}

async function askForDeleteEmployee(id) {
    const result = await Swal.fire({
        title: "Are you sure?",
        text: "This employee will be permanently deleted!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#a93a3a",
        cancelButtonColor: "#a5a5a5",
        confirmButtonText: "Delete",
        cancelButtonText: "Cancel",
        customClass: {
            confirmButton: "shadow-none",
            cancelButton: "shadow-none"
        }
    });

    if (result.isConfirmed) {
        deleteEmployee(id);
    }
}

function deleteEmployee(id) {
    $.ajax({
        url: `/Admin/Employee/Delete/${id}`,
        type: "DELETE",
        success: function (data) {
            if (data.success) {
                dataTable.ajax.reload();
                deleteEmployeeConfirmation();
            }
        },
        error: function (error) {
            console.error(error);
        }
    });
}

function deleteEmployeeConfirmation() {
    Swal.fire({
        "title": "Employee deleted.",
        "text": "The employee has been successfully deleted.",
        "icon": "success",
        "confirmButtonText": "Back",
        "confirmButtonColor": "#a5a5a5",
        "customClass": {
            "confirmButton": "shadow-none"
        }
    });
}
