var home = {
    init: function () {
        home.callAjaxReport();
    },
    loadchartline: function (lstMonth, lstPrice) {
        var options = {
            series: [{
                name: "Tổng tiền",
                data: lstPrice
            }],
            chart: {
                height: 350,
                type: 'line',
                zoom: {
                    enabled: false
                }
            },
            dataLabels: {
                enabled: false
            },
            stroke: {
                curve: 'straight'
            },
            title: {
                text: 'Báo cáo doanh thu theo năm',
                align: 'left'
            },
            grid: {
                row: {
                    colors: ['#f3f3f3', 'transparent'],
                    opacity: 0.5
                },
            },
            xaxis: {
                categories: lstMonth,
            }
        };
        $('#column_chart').empty();
        var chart = new ApexCharts(document.querySelector("#column_chart"), options);
        chart.render();
    },
    callAjaxReport: function () {
        $.ajax({
            type: 'get',
            url: '/Admin/Home/GetChartReportBill',
            data: {
                
            },
            success: function (rp) {
                home.loadchartline(rp.lstMonth, rp.lstPrice);
            },
            error: function (err) {
                notif.DANGER("Lỗi hệ thống!");
            }
        });
    },
}
$(document).ready(function () {
    home.init();
});