google.charts.load('current', {'packages': ['corechart']});

google.charts.setOnLoadCallback(generateTotalSpendingChart);

google.charts.setOnLoadCallback(generateDemographicChart); //#3366CC
google.charts.setOnLoadCallback(generateTypeChart); //#DC3912
google.charts.setOnLoadCallback(generateReadingChart);// #FF9900
google.charts.setOnLoadCallback(generateCollectionChart);// #109618
google.charts.setOnLoadCallback(generatePublishingChart);// #990099


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
        colors: ['#3366CC'],
        legend: "none"
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
        colors: ['#DC3912'],
        legend: "none"
    };

    const chart = new google.visualization.BarChart(document.getElementById(chartId));
    chart.draw(data, options);
}

function generateReadingChart(readingStatistics, chartId) {
    console.log(readingStatistics);

    let data = new google.visualization.DataTable();
    data.addColumn('string', 'Reading Status');
    data.addColumn('number', 'Count');

    readingStatistics.forEach(statistic => {
        data.addRow([statistic.readingStatus, statistic.count]);
    });

    console.log(data);

    const options = {
        colors: ['#FF9900'],
        legend: "none"
    };

    const chart = new google.visualization.BarChart(document.getElementById(chartId));
    chart.draw(data, options);
}

function generateCollectionChart(collectionStatistics, chartId) {
    console.log(collectionStatistics);

    let data = new google.visualization.DataTable();
    data.addColumn('string', 'Collection Status');
    data.addColumn('number', 'Count');

    collectionStatistics.forEach(statistic => {
        data.addRow([statistic.collectionStatus, statistic.count]);
    });

    console.log(data);

    const options = {
        colors: ['#109618'],
        legend: "none"
    };

    const chart = new google.visualization.BarChart(document.getElementById(chartId));
    chart.draw(data, options);
}

function generatePublishingChart(publishingStatistics, chartId) {
    console.log(publishingStatistics);

    let data = new google.visualization.DataTable();
    data.addColumn('string', 'Publishing Status');
    data.addColumn('number', 'Count');

    publishingStatistics.forEach(statistic => {
        data.addRow([statistic.publishingStatus, statistic.count]);
    });

    console.log(data);

    const options = {
        colors: ['#990099'],
        legend: "none"
    };

    const chart = new google.visualization.BarChart(document.getElementById(chartId));
    chart.draw(data, options);
}


function generateTotalSpendingChart(mangas, chartId) {
    console.log(mangas);

    let data = new google.visualization.DataTable();
    data.addColumn('string', 'Title');
    data.addColumn('number', 'Price paid');

    mangas.forEach(manga => {
        data.addRow([manga.title, Number(manga.price)]);
    });

    console.log(data);

    const options = {
        legend: {
            position: 'labeled',
        },
        chartArea: {
            width: '75%',
            height: '75%'
        },
        pieSliceText: 'value',
    };

    const chart = new google.visualization.PieChart(document.getElementById(chartId));
    chart.draw(data, options);
}