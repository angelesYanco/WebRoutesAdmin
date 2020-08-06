'use strict'

class Localizacion{
    constructor(callback){
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition((position) => {
                this.logitude = position.coords.latitude;
                this.longitude = position.coords.longitude;
            });
        } else {
            alert("Tu navegador no soporta geolocalización! :(");
        }
    }
}