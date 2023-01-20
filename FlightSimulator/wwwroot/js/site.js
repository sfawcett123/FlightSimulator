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
        console.log("Flight Simualtor Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

site_connection.on("/FlightSimulator", function (message) {
    var obj = JSON.parse(message);
    setConnected(obj["Connected"]);
});

site_connection.onclose(async () => {
    await start();
});

// Start the connection, when the document has loaded.
$(document).ready(function () {
    setConnected(false);
    start();
}); 


