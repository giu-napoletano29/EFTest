<template>
    <div>
        <b-container>
            <Header :name="name"/>
            <Home :test="prova"
                  :axi="brand"
                  :inc="numInc"
                  :dec="numDec"
                  :bset="bset"
                  :loaded="loaded"
                  @val="bset" />
        </b-container>
    </div>
</template>

<script>
    import Home from '@/components/Home.vue'
    import Header from '@/components/Header.vue'
    import axios from 'axios';

    export default {
        
        components: {
            Home,
            Header
        },

        data() {
            return {
                name: 'Home',
                prova: 34,
                brand: '',
                loaded: false
            }
        },

        async created() {
            try {
               await axios.get('https://localhost:5001/Brand/6').then(response => (this.brand = response.data))
               this.loaded = true
            } catch (e) {
                console.error(e)
            }
        },

        methods: {
            numInc() {
                this.prova++
            },
            numDec() {
                this.prova--
            },
            bset(val) {
                this.prova = val
            }
        }
    }
</script>

<style>
/*    #app {
        font-family: Avenir, Helvetica, Arial, sans-serif;
        -webkit-font-smoothing: antialiased;
        -moz-osx-font-smoothing: grayscale;
        text-align: center;
        color: #2c3e50;
        margin-top: 60px;
    }*/
</style>
