<template>
    <div>
        <b-container>
            <Header :name="name"/>
            <Leeds/>
        </b-container>
    </div>
</template>

<script>
    import Header from '@/components/Header.vue'
    import Leeds from '@/components/Leeds.vue'
    export default {
        
        components: {
            Leeds,
            Header
        },

        data(){
            return{
                name: 'Richieste info',
            }
        }
    }
</script>