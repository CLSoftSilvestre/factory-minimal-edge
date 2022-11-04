$(function() {
    "use strict";
    //get data

    fetch('/Notifications/GetLastDaysStats/30').then(function (resp) {
        return resp.json();
    }).then(function (mydata) {

        // Dashboard 1 Morris-chart
        Morris.Area({
            element: 'morris-area-chart',
            data: mydata,
            xkey: 'period',
            xLabels: 'day',
            ykeys: ['value','closed'],
            labels: ['Created notifications','Closed Notifications'],
            pointSize: 0,
            fillOpacity: 0,
            pointStrokeColors: ['#f62d51', '#7460ee', '#009efb'],
            behaveLikeLine: true,
            gridLineColor: '#f6f6f6',
            lineWidth: 1,
            hideHover: 'auto',
            lineColors: ['#eb8846', '#7460ee', '#009efb'],
            resize: true
        });

    })

    fetch('/Notifications/GetTypeStats').then(function (resp) {
        return resp.json();
    }).then(function (mydata) {
        Morris.Donut({
            element: 'morris-donut-chart-type',
            data: mydata,
            resize: true,
            colors: ['#586F6B', '#EB8846', '#0A0F0D', '#C4CBCA', '#3CBBB1'],
        });
    })

    fetch('/Notifications/GetLocationStats').then(function (resp) {
        return resp.json();
    }).then(function (mydata) {
        Morris.Donut({
            element: 'morris-donut-chart-location',
            data: mydata,
            resize: true,
            colors: ['#586F6B', '#EB8846', '#0A0F0D', '#C4CBCA', '#3CBBB1'],
        });
    })

    fetch('/Notifications/GetGlobalStats').then(function (resp) {
        return resp.json();
    }).then(function (mydata) {
        console.log(mydata);
        document.getElementById("openNotif").innerHTML = mydata[0].open;
        document.getElementById("closedNotif").innerHTML = mydata[0].closed;
        document.getElementById("totalNotif").innerHTML = mydata[0].open + mydata[0].closed;
    })

});