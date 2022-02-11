<template>
    <div>
        <b-container>
            <Header :name="name"/>
            <LeedDetail
                :list="list"
                :replies="listpaginated"
                :error="error"
                :loaded="loaded"
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
    import utils from '@/utilities/utils.js' 
    import Header from '@/components/Header.vue'
    import LeedDetail from '@/components/LeedDetail.vue'
    import Pagination from '@/components/Pagination.vue'
    import {Factory} from './../wrappers/Factory'
    const LeedsRepo = Factory.get('leeds')

    export default {

        mixins: [utils],
        
        components: {
            Header,
            LeedDetail,
            Pagination
        },

        data(){
            return{
                list: {},
                listpaginated: [],
                loaded: false,
                error: false,
                maxVisibleButtons: 7,
                page: 1,
                pageSize: 2,
            }
        },

        methods: {
            pageChange(page) {
                this.page = page
                var skip = (this.page - 1 ) * this.pageSize
                var take = skip + this.pageSize
                this.listpaginated = this.list.replies.slice(skip, take)
            },
            async loadElements(){
                const {data} = await LeedsRepo.getById(this.$route.params.id)
                if(!data){
                    this.RedirectIfNotFound()
                }
                this.loaded = true
                this.list = data;
                this.pageChange(1);
            },

            // RedirectIfNotFound(){
            //     this.$router.push({name: 'NotFound', params: { 0: "" } })     
            // },
        },

        computed:{
            name(){
                return 'Dettagli InfoRequest ' + this.list.id
            },

            totalpages(){
                return Math.ceil(this.list.replies?.length/this.pageSize)
            }
        },

        created() {
            this.loadElements();
        }
    }
</script>