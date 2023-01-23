function DrawBoard(boardlist, board) {
    var found = false
    var divs = boardlist.getElementsByTagName('div');
    for (var i = 0; i < divs.length; i += 1) {
        if (divs[i].id == board.Name) found = true
    }
    if (!found) {
        CreateBoard(boardlist, board)
    }
}

function CreateBoard(boardlist, board) {
    let div = document.createElement("div");
    div.id = board.Name;
    div.innerHTML = "<h3>" + board.Name + "</h3>"
    div.innerHTML += "<p>" + board.ConnectedAddress + "</p>"
    boardlist.appendChild(div);

}

function DeleteBoards(boardlist, boards) {
    var divs = boardlist.getElementsByTagName('div');

    for (var div = 0; div < divs.length; div++) {
        var found = false
        for (var bd = 0; bd < boards.length; bd++) {
            if (boards[bd].Name == divs[div].id) {
                found = true;
                break;
            }
        }
        if (!found) {
            divs[div].remove()
        }
    }
}

function CreateOutput(boardlist, key , value) {
    let div = document.createElement("div");
    div.id = key;
    div.innerHTML = "<h3>" + key + "</h3>"
    div.innerHTML += "<p>" + value + "</p>"
    boardlist.appendChild(div);

}

function DeleteOutputs(boardlist, jsonData) {
    var divs = boardlist.getElementsByTagName('div');
  
    for (var div = 0; div < divs.length; div++)
    {
        var found = false
        Object.keys(jsonData).forEach(function (key)
        {
            if (key == divs[div].id)
            {
                found = true;
            }
        })
        if(!found) {
            divs[div].remove()
        }
    }
}

function DrawOutputs(divList, jsonData) {
    var divs = divList.getElementsByTagName('div');

    Object.keys(jsonData).forEach(function (key) {
        var found = false
        for (var div = 0; div < divs.length; div++) {
            if (divs[div].id == key) {
                found = true;
                divs[div].innerHTML = "<h3>" + key + "</h3>"
                divs[div].innerHTML += "<p>" + jsonData[key] + "</p>"
                break;
            }
        }
        if (!found) CreateOutput(divList, key, jsonData[key]  )
    })

}