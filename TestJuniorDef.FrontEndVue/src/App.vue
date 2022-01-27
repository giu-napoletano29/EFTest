<template>
    <div id="app">
        <img alt="Vue logo" src="./assets/logo.png">
        <b-container>
            <Header />
            <Home :test="prova"
                  :inc="numInc"
                  :dec="numDec"
                  />
        </b-container>
    </div>
</template>

<script>
    import Home from '@/components/Home.vue'
    import Header from '@/components/Header.vue'

export default {
  name: 'App',
  components: {
      Home,
      Header
  },

  data() {
      return {
          prova: 34
      }
  },

  methods: {
      numInc() {
          this.prova++
      },
      numDec() {
          this.prova--
      }
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
