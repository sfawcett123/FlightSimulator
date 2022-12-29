"use strict";

var site_connection = new signalR.HubConnectionBuilder().withUrl("/FlightSimulator").build();

site_connection.on("FlightSimulator", function (message) {

    var obj = JSON.parse(message);
    if (obj["Connected"] == "True") {
        document.getElementById("connection").className = "connected fa-solid fa-handshake";

        document.getElementById("Heading").textContent = obj["PLANE HEADING DEGREES TRUE"].inDegrees();
        document.getElementById("Altitude").textContent = obj["PLANE ALTITUDE"].inFeet();
        document.getElementById("Latitude").textContent = obj["PLANE LATITUDE"].inDegreeLat();
        document.getElementById("Longitude").textContent = obj["PLANE LONGITUDE"].inDegreeLong();
        document.getElementById("AirSpeed").textContent = obj["AIRSPEED TRUE"].inKnots();
    }
    else {
        document.getElementById("connection").className = "disconnected fa-solid fa-handshake";
    }
});

site_connection.start();

