<template>
  <img alt="Vue logo" src="./assets/logo.png">
  <Temperature msg="Welcome to Your Vue.js App"/>
  <Geolocation/>
</template>

<script>
import Temperature from './components/Temperature.vue'
import Geolocation from './components/Geolocation.vue'

export default {
  name: 'App',
  components: {
    Temperature,
    Geolocation
  }
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
