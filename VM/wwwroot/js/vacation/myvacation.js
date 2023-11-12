$(document).ready(() => {
    $("#cancel-button").click(function () {
        const id = this.dataset.id;
        askToCancelVacation(id);
    });
});


async function askToCancelVacation(id) {
    const result = await Swal.fire({
        title: "Are you sure?",
        text: "Your vacation will be cancelled.",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#a93a3a",
        cancelButtonColor: "#a5a5a5",
        confirmButtonText: "Cancel",
        cancelButtonText: "Back",
        customClass: {
            confirmButton: "shadow-none",
            cancelButton: "shadow-none"
        }
    });

    if (result.isConfirmed) {
        cancelVacation(id);
    }
}


function cancelVacation(id) {
    $.ajax({
        url: `/Public/Vacation/CancelVacation/${id}`,
        type: "DELETE",
        success: function (data) {
            if (data.success) {
                displayAlertThenReload("Vacation cancelled", "Your vacation has been successfully cancelled on your request.");
            }
            else {
                toastr.error(data.message);
            }
        },
        error: function (error) {
            toastr.error("A server exception has been thrown.");
            console.error(error);
        }
    });
}


async function displayAlertThenReload(title, text) {
    await Swal.fire({
        title,
        text,
        icon: "success",
        confirmButtonColor: "#13a71e",
        confirmButtonText: "Alright",
        customClass: {
            confirmButton: "shadow-none"
        }
    });

    location.href = "/public/home/index";
}
