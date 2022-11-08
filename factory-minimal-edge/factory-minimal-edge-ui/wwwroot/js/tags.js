"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/tagsHub").build();

//Disable send button until connection is established
//document.getElementById("sendButton").disabled = true;

connection.on("UpdatedTag", function (tag) {
    console.log(tag.Value);
});

connection.start().then(function () {
    //document.getElementById("sendButton").disabled = false;
    console.log("Teste");
}).catch(function (err) {
    return console.error(err.toString());
});