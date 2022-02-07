<template>
    <div>
        <b-container>
            <Header :name="name"/>
            <Leeds
                :list="list"
                :loaded="loaded"
                :error="error"
                @openDetail="OpenDetail"
            />
            <Pagination
                :totalPages="totalpages"
                :maxVisibleButtons="maxVisibleButtons"
                :page="page"
                @pageChanged="pageChange"
            />
        </b-container>
    </div>
</template>

<script>
    import Header from '@/components/Header.vue'
    import Leeds from '@/components/Leeds.vue'
    import Pagination from '@/components/Pagination.vue'
    import {Factory} from './../wrappers/Factory'
    const LeedsRepo = Factory.get('leeds')

    export default {
        
        components: {
            Leeds,
            Header,
            Pagination
        },

        data(){
            return{
                name: 'Richieste info',
                list: {},
                maxVisibleButtons: 7,
                page: 1,
                pageSize: 10,
                loaded: false,
                error: false,
            }
        },

        computed:{
            totalpages(){
                return Math.ceil(this.list.totalElements/this.list.pageSize)
            }
        },

        methods: {
            pageChange(page) {
                this.page = page
                this.loadElements();
            },

            async loadElements(){
                const {data} = await LeedsRepo.getallpagedsized(this.pageSize, this.page)
                this.loaded = true
                this.list = data
            },
            OpenModal(){
                this.open = true
            },
            CloseModal(){
                this.open = false
            },
            OpenDetail(val){
                this.$router.push('/leeds/'+val)
            }
        },

        created() {
            this.loadElements();
        },
    }
</script>