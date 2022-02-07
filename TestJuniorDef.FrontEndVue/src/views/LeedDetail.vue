<template>
    <div>
        <b-container>
            <Header :name="name"/>
            <LeedDetail
                :list="list"
                :error="error"
                :loaded="loaded"
            />
        </b-container>
    </div>
</template>

<script>
    import Header from '@/components/Header.vue'
    import LeedDetail from '@/components/LeedDetail.vue'
    import {Factory} from './../wrappers/Factory'
    const LeedsRepo = Factory.get('leeds')

    export default {
        
        components: {
            Header,
            LeedDetail,
        },

        data(){
            return{
                list: {},
                loaded: false,
                error: false,
            }
        },

        methods: {
            async loadElements(){
                const {data} = await LeedsRepo.getById(this.$route.params.id)
                this.loaded = true
                this.list = data;
            },
        },

        computed:{
            name(){
                return 'Dettagli InfoRequest ' + this.list.id
            },
        },

        created() {
            this.loadElements();
        }
    }
</script>