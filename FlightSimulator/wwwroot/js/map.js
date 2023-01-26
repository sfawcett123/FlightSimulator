"use strict";

const InitialPostition = { lat: 54.84414, lng: -1.50106 };

var FirstCall = true;

var map;
var zoom = 12;
var aircraft;
var angle = 0;


var LockToCentre = false;

function DrawMap(json) {

    if (json["Connected"] == "True") {
        Move(json );
        FirstCall = false;
    }
    else {
        FirstCall = true;
    }
}

function Move(SimData) {

    console.log( "In Move")
    console.log(SimData);

    if (SimData["Connected"] == "False") return;

    var image = aircraft.getIcon();
    
    image.rotation = parseInt(SimData["PLANE HEADING DEGREES TRUE"] );
    aircraft.setIcon(image);
  

    moveAircraft(SimData["PLANE LATITUDE"], SimData["PLANE LONGITUDE"]);

    if (LockToCentre || FirstCall) panToAircraft(SimData["PLANE LATITUDE"], SimData["PLANE LONGITUDE"]);
}

function panToAircraft(Lat, Lng) {
    map.panTo(new google.maps.LatLng(Lat, Lng));
}

function moveAircraft(Lat, Lng) {
    aircraft.setPosition(new google.maps.LatLng(Lat, Lng));
};

// Initialize and add the map
function initMap() {

    map = new google.maps.Map(document.getElementById("map"), {
        zoom: zoom,
        center: InitialPostition,
        streetViewControl: false,
        zoomControl: false,
        mapTypeControl: false,
        fullscreenControl: false,
    });

    const image = {
        path: "M14 8.947L22 14v2l-8-2.526v5.36l3 1.666V22l-4.5-1L8 22v-1.5l3-1.667v-5.36L3 16v-2l8-5.053V3.5a1.5 1.5 0 0 1 3 0v5.447z",
        fillColor: "#ffd400",
        fillOpacity: 1,
        strokeColor: "000",
        strokeOpacity: 0.4,
        scale: 1,
        rotation: 0,
        anchor: new google.maps.Point(13, 13)
    };

    // The Aircraft
    aircraft = new google.maps.Marker({
       position: InitialPostition,
        icon: image,
        map: map,
    });

    //google.maps.event.addListenerOnce(map, 'idle', function () {
    //    setInterval(Move, 1000);
    //});
}

window.initMap = initMap;