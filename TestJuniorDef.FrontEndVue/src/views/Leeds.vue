<template>
    <div>
        <b-container>
            <Header :name="name"/>
            <Leeds
                :list="listpaginated"
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
                list: [],
                listpaginated: [],
                maxVisibleButtons: 7,
                page: 1,
                pageSize: 10,
                loaded: false,
                error: false,
            }
        },

        computed:{
            totalpages(){
                return Math.ceil(this.list.length/this.pageSize)
            }
        },

        methods: {
            pageChange(page) {
                this.page = page
                var skip = (this.page - 1 ) * this.pageSize
                var take = skip + this.pageSize
                //this.listpaginated = this.list.Skip(skip).Take(this.pageSize)
                this.listpaginated = this.list.slice(skip, take)
                // this.loadElements();
            },

            async loadElements(){
                const {data} = await LeedsRepo.get()
                this.loaded = true
                this.list = data
                this.pageChange(1);
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