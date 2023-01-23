"use strict";

const site_connection = new signalR.HubConnectionBuilder()
    .withUrl("/FlightSimulator")
    .configureLogging(signalR.LogLevel.Information)
    .build();

function setConnected(Connected) {
     if ( Connected == "True") 
        document.getElementById("connection").className = "connected fa-solid fa-handshake";
    else
        document.getElementById("connection").className = "disconnected fa-solid fa-handshake";
}

async function start() {
    try {
        await site_connection.start();
        console.log("Flight Simulator SignalR hub connection made.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

site_connection.on("SimData", function (message) {
 
    var obj = JSON.parse(message);
    setConnected(obj["Connected"]);

    var map = document.getElementById("map")
    if (map) DrawMap(obj);

    var outputs = document.getElementById("outputList")
    if (outputs) DrawOutputs(outputs, obj);
    if (outputs) DeleteOutputs(outputs, obj);

});


site_connection.on("BoardData", function (message) {
    var boardlist = document.getElementById("boardList")
    if (boardlist == null) return;

    var obj = JSON.parse(message);

    for (let bd = 0; bd < obj.length; bd++) {
        DrawBoard( boardlist, obj[bd] )
    }
    // Check for any boards that have gone off line
    DeleteBoards(boardlist, obj)
});

site_connection.on("InputList", function (message) {
    var inputs = document.getElementById("inputList")
    var obj = JSON.parse(message);

    if (inputs) DrawOutputs(inputs, obj);
    if (inputs) DeleteOutputs(inputs, obj);
});

site_connection.onclose(async () => {
    await start();
});

// Start the connection, when the document has loaded.
$(document).ready(function () {
    setConnected(false);
    start();
}); 


