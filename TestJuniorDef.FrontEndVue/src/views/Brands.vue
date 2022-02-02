<template>
    <div>
        <b-container>
            <Header :name="name"/>
            <Brands/>
        </b-container>
    </div>
</template>

<script>
    import Header from '@/components/Header.vue'
    import Brands from '@/components/Brands.vue'
    export default {
        
        components: {
            Brands,
            Header
        },

        data(){
            return{
                name: 'Brand',
            }
        }
    }
</script>