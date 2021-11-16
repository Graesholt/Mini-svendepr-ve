<template>
  <h1>Temperature</h1>
  <div v-for="Room in Rooms" :key="Room.id">
    <div class="TemperatureText">
      <b>{{ Room.roomName }}</b>
    </div>
    <div class="TemperatureText">{{ Room.temperaturelabel }}</div>
    <line-chart :data="Room.chartData"></line-chart>
  </div>
</template>

<script async setup>
import axios from "axios";
import { ref } from "vue";
var Rooms = ref([]); // Databinded i HTML'en

axios
  .get("https://api.temp.computerx.dk/Rooms")
  .then(function (response) {
    Rooms.value = response.data;

    executeAndsetInterval(sendlatestRequest, 1000); // Opdater temperaturelabels hvert sekund
    executeAndsetInterval(sendlatestchartRequest, 60000); // Opdater chartData hvert minut
  });

async function sendlatestRequest() { // Opdater temperaturelabels
  Rooms.value.forEach(async (room) => {
    await axios
      .get("https://api.temp.computerx.dk/temperatures/latest/" + room.roomName)
      .then(function (response) {
        room.temperaturelabel = response.data + " °C";
        console.log(response.data);
      });
  });
}

async function sendlatestchartRequest() { // Opdater chartData
  Rooms.value.forEach(async (room) => {
    await axios
      .get("https://api.temp.computerx.dk/temperatures/latestchart/" + room.roomName)
      .then(function (response) {
        room.chartData = [];
        var i1 = -response.data.length;
        response.data.forEach((element) => {
          room.chartData.push([i1, element]);
          i1++;
        });
      });
  });
}

function executeAndsetInterval(func, interval) { // Lille function til at starte en metode og sætte den på timer.
    func();
    return(setInterval(func, interval));
}
</script>

<style>
.TemperatureText {
  font-size: 25px;
  margin-bottom: 10px;
}
</style>