"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/tagsHub").build();

connection.on("UpdatedTagValue", function (tagId, tagName, tagValue) {
    console.log(tagId + " - " + tagName + " - " + tagValue);
    updateTagField(tagId, tagName, tagValue);
});

connection.start().then(function () {
    console.log("ConnectionStarted");
}).catch(function (err) {
    return console.error(err.toString());
});

function updateTagField(tagId, tagName, tagValue) {
    document.getElementById(tagName).innerHTML = tagValue;
}