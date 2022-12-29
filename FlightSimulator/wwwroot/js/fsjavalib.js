function DisplayDebug(message) {
    document.getElementById("debug").textContent = message;
}

String.prototype.inFeet = function () {
    var data = parseInt( this );
    return data.toString() + " ft";
}

String.prototype.inDegrees = function () {
    var data = parseInt(this);
    data = data % 360;
    return data.toString() + " º";
}

String.prototype.inKnots = function () {
    var data = parseInt(this);
    return data.toString() + " Kts" ;
}

String.prototype.inDegreeLat = function () {
    var data = parseFloat(this);
    var lat = toDegreesMinutesAndSeconds(data);
    var latitudeCardinal = data >= 0 ? "N" : "S";
    return lat + " " + latitudeCardinal;
}

String.prototype.inDegreeLong = function () {
    var data = parseFloat(this);
    var long = toDegreesMinutesAndSeconds(data);
    var longitudeCardinal = data >= 0 ? "E" : "W";
    return long + " " + longitudeCardinal
}

function toDegreesMinutesAndSeconds(coordinate) {
    var absolute = Math.abs(coordinate);
    var degrees = Math.floor(absolute);
    var minutesNotTruncated = (absolute - degrees) * 60;
    var minutes = Math.floor(minutesNotTruncated);
    var seconds = Math.floor((minutesNotTruncated - minutes) * 60);

    return degrees + "º " + minutes + "\" " + seconds + "\'";
}