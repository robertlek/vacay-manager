const select = $("#select-list");
const vacationsChart = $("#vacations-chart");
const employeesChart = $("#employees-chart");

(async function () {
    $(vacationsChart).hide();
    $(employeesChart).hide();

    $(select).on("change", () => {
        const option = $(select).find(":selected").val();

        switch (option) {
            case "0":
                vacationsChart.show();
                employeesChart.hide();
                break;
            case "1":
                employeesChart.show();
                vacationsChart.hide();
                break;
            default:
                break;
        }
    });

    $(select).trigger("change");

    const vacationsData = await fetchVacationsData();
    const employeesData = await fetchEmployeesData();

    new Chart(
        document.getElementById("vacations-chart"),
        {
            type: "bar",
            data: {
                labels: vacationsData.map(row => row.month),
                datasets: [
                    {
                        label: "Vacations planned for 2024",
                        data: vacationsData.map(row => row.count)
                    }
                ]
            }
        }
    );

    new Chart(
        document.getElementById("employees-chart"),
        {
            type: "doughnut",
            data: {
                labels: employeesData.map(row => row.year),
                datasets: [{
                    label: "Yearly Employment Rate",
                    data: employeesData.map(row => row.count),
                    backgroundColor: [
                        "#4E6580",
                        "#101491",
                        "#4E0F0F",
                        "#E5E5E5",
                        "#303E4E",
                        "#2E9117"
                    ],
                    hoverOffset: 4
                }]
            },
        }
    );
})();

async function fetchEmployeesData() {
    const response = await fetch("/api/get-employees-data");
    return response.json();
}

async function fetchVacationsData() {
    const response = await fetch("/api/get-vacations-data");
    return response.json();
}
