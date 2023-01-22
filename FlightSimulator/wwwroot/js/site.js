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

function DrawBoard(boardlist , board ) {
    var found = false
    var divs = boardlist.getElementsByTagName('div');
    for (var i = 0; i < divs.length; i += 1) {
        if (divs[i].id == board.Name ) found = true
    }
    if (!found) {
        CreateBoard(boardlist, board )
    }
}

function CreateBoard(boardlist , board) {
    let div = document.createElement("div");
    div.id = board.Name;
    div.innerHTML = "<h3>" + board.Name + "</h3>" 
    div.innerHTML += "<p>" + board["ConnectedAddress"] + "</p>"
    boardlist.appendChild(div);

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
});


site_connection.on("BoardData", function (message) {
    var obj = JSON.parse(message);

    var boardlist = document.getElementById("boardList")
    if (boardlist == null) return;

    for (let bd = 0; bd < obj.length; bd++) {
        DrawBoard( boardlist, obj[bd] )
    }

    var boardlist = document.getElementById("boardList")
    var divs  = boardlist.getElementsByTagName('div');
    for (let div = 0; div < divs.length; div++) {
        var found = false
        for (let bd = 0; bd < obj.length; bd++) {
            console.log("Inspecting " + obj[bd].Name + " against " + divs[div].id )
            if (obj[bd].Name == divs[div].id) {
                found = true;
                break;
            }
        }
        if (!found) {
            console.log("Removing " + divs[div].id)
            boardlist.removeChild(divs[div])
        }
            
    }

});


site_connection.onclose(async () => {
    await start();
});

// Start the connection, when the document has loaded.
$(document).ready(function () {
    setConnected(false);
    start();
}); 


