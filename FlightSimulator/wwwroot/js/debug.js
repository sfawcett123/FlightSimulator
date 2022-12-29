"use strict";

var debug_connection = new signalR.HubConnectionBuilder().withUrl("/FlightSimulator").build();
debug_connection.start();

debug_connection.on("FlightSimulator", function (message) {

    const res = JSON.parse(message);
    var table = document.getElementById('PlaneDebug-table-body');
    for (const key in res) {
        var cell = document.getElementById(key);
        if (cell) {
            cell.innerHTML = res[key];
        }
        else {
            var row = table.insertRow();
            var cell = row.insertCell();
            cell.innerHTML = key;

            cell = row.insertCell();
            cell.innerHTML = res[key];
            cell.id = key;
        }
    }

});

debug_connection.on("FlightSimulatorTrack", function (message) {

    const res = JSON.parse(message);
    var table = document.getElementById('TrackDebug-table-body');

    for (const key in res) {
        var row = document.getElementById("row_" + key);
        if (!row) {
            var row = table.insertRow();
            var cell = row.insertCell();
            cell.innerHTML = res[key].Latitude;
            cell = row.insertCell();
            cell.innerHTML = res[key].Longitude;
            row.id = "row_" + key;
        }
    }

});

function CreateStatusBar() {
    var statusBar = document.createElement('div');
    boardDiv.className = "statebar ";
    return statusBar;
}

debug_connection.on("Boards", function (message) {
    const res = JSON.parse(message);
    //console.log(message);
    var board_count = document.getElementById('board_count');
    board_count.textContent = res.length;

    var row = document.getElementById("board-list");

    for (const key in res) {
        var boardDiv = document.getElementById(res[key].Hash);
        if (!boardDiv) {
            boardDiv = document.createElement('div');
            boardDiv.id = res[key].Hash;
            boardDiv.className = "board " + res[key].OS;
            row.appendChild(boardDiv);
            boardDiv.appendChild(CreateStatusBar()); 
        }
        else {
            //boardDiv.innerText = res[key].Timeout + " " + res[key].Ip_address;
        }
    }

    for (const child of document.getElementById("board-list").childNodes) {
        var _delete = true;
        for (const key in res) {
            if (child.id == res[key].Hash) _delete = false;
        }
        if (_delete) child.remove();
    }

});