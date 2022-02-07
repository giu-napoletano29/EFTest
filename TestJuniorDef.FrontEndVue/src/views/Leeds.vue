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
    import {Factory} from './../wrappers/Factory'
    const LeedsRepo = Factory.get('leeds')

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
            async loadElements(){
                const {data} = await LeedsRepo.get()
                this.loaded = true
                this.list = data;
            },
            OpenModal(){
                this.open = true
            },
            CloseModal(){
                this.open = false
            }
        },

        created() {
            this.loadElements();
        },
    }
</script>