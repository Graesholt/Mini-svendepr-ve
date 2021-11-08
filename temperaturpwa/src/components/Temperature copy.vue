<template>
  <h1>Dashboard - container</h1>
  <div>{{ temperaturelabel }}</div>
  <line-chart :data="chartData"></line-chart>
</template>

<script async setup>
import axios from "axios";
import { ref } from "vue";
var temperaturelabel = ref("Reading Temperature");
var chartData = ref([]);

setInterval(sendlatestRequest, 1000);
setInterval(sendlatestchartRequest, 60000);
sendlatestchartRequest();

async function sendlatestRequest() {
  await axios
    .get("http://localhost:8444/temperatures/latest/H5PD091121")
    //.then(response => temperaturelabel.value = response.data.temperatureCentigrade)
    .then(function (response) {
      temperaturelabel.value = response.data + " °C";
      console.log(response.data);
    });

  /*
    var postTemp = Math.random() * 30
axios
.post("http://localhost:8444/Temperatures", {
    "TemperatureCentigrade": postTemp
})
*/
}

async function sendlatestchartRequest() {
  await axios
    .get("http://localhost:8444/temperatures/latestchart/H5PD091121")
    .then(function (response) {
      chartData.value = [];
      var i1 = -response.data.length;
      response.data.forEach((element) => {
        //console.log(element)
        chartData.value.push([i1, element]);
        i1++;
      });
    });
}
</script>

<style>
</style>