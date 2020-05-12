<template>
  <div id="app" class="container">
    <index-page />  
    <app-modal />
    <notifications group="main" position="bottom right" :max="2" />
  </div>
</template>

<script>
import AppModal from './components/AppModal.vue';
import IndexPage from './components/IndexPage.vue';

export default {
  name: 'App',
  components: {
    IndexPage,
    AppModal,
  },
}
</script>

<style lang="scss">
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
