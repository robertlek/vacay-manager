(async function () {
    const data = await fetchVacationsData();
    const employeesData = await fetchEmployeesData();

    new Chart(
        document.getElementById("vacations-chart"),
        {
            type: "bar",
            data: {
                labels: data.map(row => row.month),
                datasets: [
                    {
                        label: "Vacations planned for 2024",
                        data: data.map(row => row.count)
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
