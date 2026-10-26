// Chart styling only. Dataset values, labels and tooltip callbacks stay untouched.
(function () {
    function styleChart(chart) {
        const dark = document.documentElement.dataset.bsTheme === 'dark';
        const text = dark ? '#a6b5c9' : '#64748b';
        const grid = dark ? '#314055' : '#e5eaf1';
        chart.options.color = text;
        if (chart.options.plugins.legend) chart.options.plugins.legend.labels.color = text;
        if (chart.config.type === 'line') {
            ['x', 'y'].forEach(function (axis) {
                chart.options.scales[axis].ticks.color = text;
                chart.options.scales[axis].grid.color = grid;
                chart.options.scales[axis].title.color = text;
                chart.options.scales[axis].border.color = grid;
            });
            chart.data.datasets.forEach(function (dataset, index) {
                dataset.borderColor = index === 0 ? (dark ? '#85b1ff' : '#245bc4') : (dark ? '#edb080' : '#ae6b36');
                dataset.backgroundColor = dataset.borderColor;
                dataset.borderWidth = 2;
                dataset.pointRadius = 2;
                dataset.pointHoverRadius = 5;
            });
        } else {
            chart.data.datasets.forEach(function (dataset) {
                dataset.backgroundColor = ['#4774cf', '#4c9a91', '#bd9159', '#8580bb', '#ba7188', '#6c92a6', '#9aab6a', '#c28e73'];
                dataset.borderColor = dark ? '#192332' : '#ffffff';
                dataset.borderWidth = 3;
            });
        }
    }
    Chart.register({ id: 'studioPresentation', beforeInit: styleChart });
    document.addEventListener('studio:theme', function () {
        Object.values(Chart.instances).forEach(function (chart) { styleChart(chart); chart.update('none'); });
    });
})();
