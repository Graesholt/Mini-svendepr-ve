<template>
  <h1>Geo</h1>
  <button @click="showGeoLocation">Geo</button>
</template>

<script setup>
import {ref} from "vue";
let GeoLocation = ref();

navigator.geolocation.watchPosition((position) => {
    GeoLocation.value = position.coords;
});

const showGeoLocation = () => {
    let geo = {
        altitude: GeoLocation.value.altitude,
        altitudeAccuracy: GeoLocation.value.altitudeAccuracy,
        heading: GeoLocation.value.heading,
        latitude: GeoLocation.value.latitude,
        longitude: GeoLocation.value.longitude,
        speed: GeoLocation.value.speed
    };
    alert(JSON.stringify(geo));
};
</script>

<style>
.TemperatureText {
  font-size: 25px;
  margin-bottom: 10px;
}
</style>