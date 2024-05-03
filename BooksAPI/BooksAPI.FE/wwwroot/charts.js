google.charts.load('current', {'packages': ['corechart']});
google.charts.setOnLoadCallback(generateDemographicChart);
google.charts.setOnLoadCallback(generateTypeChart);



function generateDemographicChart(demographicStatistic, chartId) {
    console.log(demographicStatistic);
    
    let data = new google.visualization.DataTable();
    data.addColumn('string', 'Demographic');
    data.addColumn('number', 'Count');
    
    demographicStatistic.forEach(statistic => {
        data.addRow([statistic.demographicType, statistic.count]);
    });

    console.log(data);

    const options = {
        colors: ['#3366CC']
    };

    const chart = new google.visualization.BarChart(document.getElementById(chartId));
    chart.draw(data, options);
}

function generateTypeChart(typeStatistics, chartId) {
    console.log(typeStatistics);
    
    let data = new google.visualization.DataTable();
    data.addColumn('string', 'Type');
    data.addColumn('number', 'Count');

    typeStatistics.forEach(statistic => {
        data.addRow([statistic.type, statistic.count]);
    });

    console.log(data);

    const options = {
        colors: ['#DC3912']
    };

    const chart = new google.visualization.BarChart(document.getElementById(chartId));
    chart.draw(data, options);
}