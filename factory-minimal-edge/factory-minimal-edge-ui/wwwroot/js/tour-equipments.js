
// Check if the user as already run the tour
let x = getCookie("tourHomeComplete");
let y = getCookie("tourNotificationsComplete");
let z = getCookie("tourEquipmentsComplete");

if (x == "true" && y== "true" && (z=="" || z=="false")) {
    introJs().setOptions({
        showProgress: true,
        steps: [{
            title: "Equipments tour",
            intro: 'Please take a few moments to have a quick tour on the main features regarding the equipments module. If your not interested in this tour please close in the X button'
        },
        {
            element: document.getElementById('tour-createdocument'),
            title: "Equipments tour",
            intro: 'This button will redirect you to the equipment creation page. Only the tenant owner or admins can add equipments',
            position: 'bottom'
        },
        {
            element: document.getElementById('tour-equipmentdocument'),
            title: "Equipments tour",
            intro: 'This button will redirect you to the equipment document creation page. Several docuemnts can be associated with the equipment. Schematics, operation manuals, procedures or videos. Only the tenant owner or admins can add equipment documents',
            position: 'bottom'
        },
        {
            element: document.getElementById('tour-documenttypes'),
            title: "Equipments tour",
            intro: 'This button will redirect you to the document type page. Here you will be able to define the types of documents that can be associated with the equipment. Only the tenant owner or admins can edit document types',
            position: 'bottom'
        },
        {
            //element: document.getElementById('tour-equipments'),
            title: "Equipments tour",
            intro: 'Thanks for the time spent in this quick tour. If you have more doubts or please feel free to contactus us <a href="mailto:geral@azordev.pt">geral@azordev.pt</a>',
            position: 'top'
        }
        ]
    }).start().oncomplete(function () {
        setCookie("tourEquipmentsComplete", true, 90);
    });
    
}

