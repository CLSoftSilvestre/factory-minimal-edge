
// Check if the user as already run the tour
let x = getCookie("tourHomeComplete");
let y = getCookie("tourNotificationsComplete");

if (x == "true" && (y=="" || y=="false")) {
    introJs().setOptions({
        showProgress: true,
        steps: [{
            title: "Notifications tour",
            intro: 'Please take a few moments to have a quick tour on the main features regarding the notifications module. If your not interested in this tour please close in the X button'
        },
        {
            element: document.getElementById('tour-createnotif'),
            title: "Notifications tour",
            intro: 'This button will redirect you to the notification creation page. When a notifications is created one email is sent to the contact list of the location were the notification was created',
            position: 'bottom'
        },
        {
            element: document.getElementById('tour-edittypes'),
            title: "Notifications tour",
            intro: 'This button redirects you to the types of notifications table. Here you can define the types of notifications that users can select during the creation of new notification. This button is only accessible to the owner of tenant admins',
            position: 'bottom'
        },
        {
            element: document.getElementById('tour-editstatus'),
            title: "Notifications tour",
            intro: 'This button redirects you to the status of notifications table. Here you can define the status that users can select during the notification management. This button is only accessible to the owner of tenant admins',
            position: 'bottom'
        },
        {
            element: document.getElementById('tour-editrisks'),
            title: "Notifications tour",
            intro: 'This button reditects you to the notification risks table. Here you can define the types risks that users can select during the creation of new notification. This button is only accessible to the owner of teanant admins',
            position: 'bottom'
        },
        {
            element: document.getElementById('tour-table'),
            title: "Notifications tour",
            intro: 'Here you can find the notifications created in this tenant',
            position: 'top'
        },
        {
            element: document.getElementById('tour-searchbox'),
            title: "Notifications tour",
            intro: 'To seach one notification by description content, you can fill here the text that you want to seacrh and press enter',
            position: 'bottom'
        },
        {
            element: document.getElementById('tour-backtofulllist'),
            title: "Notifications tour",
            intro: 'To get back to the full notifications list, select this item',
            position: 'bottom'
        },
        {
            element: document.getElementById('tour-equipments'),
            title: "Notifications tour",
            intro: 'Are you ready for the equipments tour? Click the item to access it',
            position: 'right'
        }
        ]
    }).start().oncomplete(function () {
        setCookie("tourNotificationsComplete", true, 90);
    });
    
}

