<template>
    <div>
        <b-container>
            <Header :name="name"/>
            <Leeds
                :list="list"
                :loaded="loaded"
                :error="error"
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
                //listpaginated: [],
                maxVisibleButtons: 7,
                page: 1,
                pageSize: 10,
                loaded: false,
                error: false,
            }
        },

        computed:{
            totalpages(){
                //return Math.ceil(this.list.length/this.pageSize)
                return Math.ceil(this.list.totalElements/this.list.pageSize)
            }
        },

        methods: {
            pageChange(page) {
                this.page = page
                this.loadElements();
                //var skip = (this.page - 1 ) * this.pageSize
                //var take = skip + this.pageSize
                //this.listpaginated = this.list.slice(skip, take)
            },

            async loadElements(){
                const {data} = await LeedsRepo.getallpaged(this.page)
                this.loaded = true
                this.list = data
                //this.pageChange(1);
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