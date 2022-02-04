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
    import {Factory} from './../wrappers/Factory'
    const BrandsRepo = Factory.get('brands')

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
            const {data} = await BrandsRepo.getById(6)
            this.loaded = true
            this.brand = data;
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

