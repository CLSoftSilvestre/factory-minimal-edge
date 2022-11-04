
// Check if the user as already run the tour
let x = getCookie("tourHomeComplete");

if ( (x == "" || x == "false")) {
    introJs().setOptions({
        showProgress: true,
        steps: [{
            title: "Factory Floor",
            intro: 'Please take a few moments to have a quick tour on the main features of this platform. If your not interested in this tour please close in the X button. You can desactive tours in the user menu in top right corner'
        },
        {
            element: document.getElementById('tour-home'),
            title: "Base tour",
            intro: 'This item bring you the main dashboard. in this page you can check some statistics regarding the notifications creation',
            position: 'right'
        },
        {
            element: document.getElementById('morris-area-chart'),
            title: "Base tour",
            intro: 'Here you can check the evaluation of the notifications created and closed during the last 30 days',
            position: 'bottom'
        },
        {
            element: document.getElementById('tour-totals'),
            title: "Base tour",
            intro: 'Bellow you have the global amount of notifications created, opened and closed',
            position: 'top'
        },
        {
            element: document.getElementById('morris-donut-chart-type'),
            title: "Base tour",
            intro: 'This charts shows the amount of notifications per type of notification',
            position: 'bottom'
        },
        {
            element: document.getElementById('morris-donut-chart-location'),
            title: "Base tour",
            intro: 'And this one will give you the amount of notifications per location',
            position: 'bottom'
        },
        {
            element: document.getElementById('tour-user'),
            title: "Base tour",
            intro: 'Here you have access to user and tenant properties. You can change the default language, create a free tenant account, associate you email to other tenants or change actual tenant',
            position: 'left'
        },
        {
            element: document.querySelector('.footer'),
            title: "Base tour",
            intro: 'If you have more doubts or please feel free to contactus us <a href="mailto:geral@azordev.pt">geral@azordev.pt</a>',
            position: 'top'
        },
        {
            element: document.getElementById('tour-notifications'),
            title: "Base tour",
            intro: 'Are you ready for the notification tour? Click the item to access it',
            position: 'right'
        }
        ]
    }).start().oncomplete(function () {
        setCookie("tourHomeComplete", true, 90);
    });  
}



