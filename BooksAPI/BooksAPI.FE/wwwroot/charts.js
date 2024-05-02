﻿google.charts.load('current', {'packages':['corechart']});
google.charts.setOnLoadCallback(drawChart);

function test() {
// Set Data
    const data = google.visualization.arrayToDataTable([
        ['Contry', 'Mhl'],
        ['Italy', 60],
        ['France', 49],
        ['Spain', 44],
        ['USA', 24],
        ['Argentina', 15]
    ]);

// Set Options
    const options = {
        title: 'World Wide Wine Production'
    };

// Draw
    const chart = new google.visualization.BarChart(document.getElementById('myChart'));
    chart.draw(data, options);
}