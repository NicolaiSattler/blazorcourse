export function extractLatLong(objectRef) {
    alert("Hi!");
    //TOOD: retrieve latlong from info from Exif.js
    objectRef.invokeMethodAsync("CallBack", "lat", "long");
}