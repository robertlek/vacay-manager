(async function () {
    const data = await fetchVacationsData();

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
})();

async function fetchVacationsData() {
    const response = await fetch("/api/get-vacations-data");
    return response.json();
}
