<template>
    <div>
        <b-container>
            <Header :name="name"/>
            <InsertNewbrand

            />
        </b-container>
    </div>
</template>

<script>
    import Header from '@/components/Header.vue'
    import InsertNewbrand from '@/components/InsertNewbrand.vue'
    //import {Factory} from './../wrappers/Factory'
    //const BrandsRepo = Factory.get('brands')

    export default {
        
        components: {
            InsertNewbrand,
            Header,
        },

        data(){
            return{
                name: 'Nuovo Brand',

            }
        },

        methods: {

        },

    }
</script>