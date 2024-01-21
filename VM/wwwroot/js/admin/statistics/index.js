(async function () {
    const data = [
        {
            month: "January",
            count: 1
        },
        {
            month: "February",
            count: 2
        },
        {
            month: "March",
            count: 3
        },
        {
            month: "April",
            count: 4
        },
        {
            month: "May",
            count: 5
        },
        {
            month: "June",
            count: 6
        },
        {
            month: "July",
            count: 7
        },
        {
            month: "August",
            count: 8
        },
        {
            month: "September",
            count: 9
        },
        {
            month: "October",
            count: 10
        },
        {
            month: "November",
            count: 11
        },
        {
            month: "December",
            count: 12
        }
    ];

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
