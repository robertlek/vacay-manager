$(document).ready(() => {
    $(".delete-button").each((_, element) => {
        $(element).on("click", function () {
            const id = $(this).attr("data-id");
            askToDeleteDepartment(id);
        });
    });
});


async function askToDeleteDepartment(id) {
    const result = await Swal.fire({
        "title": "Are you sure?",
        "text": "This department will be permanently deleted!",
        "icon": "warning",
        "confirmButtonText": "Delete",
        "confirmButtonColor": "#a93a3a",
        "showCancelButton": true,
        "cancelButtonText": "Cancel",
        "cancelButtonColor": "a5a5a5",
        "customClass": {
            "confirmButton": "shadow-none",
            "cancelButton": "shadow-none"
        }
    });

    if (result.isConfirmed) {
        deleteDepartment(id);
    }
}

function deleteDepartment(id) {
    $.ajax({
        "url": `/Admin/Department/Delete/${id}`,
        "method": "DELETE",
        "success": (data) => {
            if (data.success) {
                deleteDepartmentConfirmation();
            }
        },
        "error": (error) => {
            console.error(error);
        }
    })
}

async function deleteDepartmentConfirmation() {
    const result = await Swal.fire({
        "title": "Department deleted.",
        "text": "The department has been successfully deleted.",
        "icon": "success",
        "confirmButtonText": "Back",
        "confirmButtonColor": "#a5a5a5",
        "customClass": {
            "confirmButton": "shadow-none"
        }
    });

    window.location.reload();
}
