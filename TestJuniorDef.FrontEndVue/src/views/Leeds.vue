<template>
    <div>
        <b-container>
            <Header :name="name"/>
            <Leeds
                :list="list"
                :loaded="loaded"
                :error="error"
            />
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
                list: {},
                loaded: false,
                error: false,
            }
        },

        methods: {
            // async loadElements(){
            //     const {data} = await ProductsRepo.getallpaged(this.page)
            //     this.loaded = true
            //     this.list = data;
            // },
            OpenModal(){
                this.open = true
            },
            CloseModal(){
                this.open = false
            }
        },

        created() {
            //this.loadElements();
        },
    }
</script>