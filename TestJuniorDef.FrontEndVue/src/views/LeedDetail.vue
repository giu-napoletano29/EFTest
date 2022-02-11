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
    import DetailPagesUtils from '@/utilities/DetailPagesUtils.js' 
    import Header from '@/components/Header.vue'
    import LeedDetail from '@/components/LeedDetail.vue'
    import Pagination from '@/components/Pagination.vue'
    import {Factory} from './../wrappers/Factory'
    const LeedsRepo = Factory.get('leeds')

    export default {

        mixins: [DetailPagesUtils],
        
        components: {
            Header,
            LeedDetail,
            Pagination
        },

        data(){
            return{
                pageSize: 2,
            }
        },

        methods: {
            pageChangeComponent(skip, take){
                this.listpaginated = this.list.replies.slice(skip, take)
            },

            async loadElements(){
                const {data} = await LeedsRepo.getById(this.$route.params.id)
                this.loadElementsComponent(data)
            },
        },

        computed:{
            name(){
                return 'Dettagli InfoRequest ' + this.list.id
            },

            totalpages(){
                return Math.ceil(this.list.replies?.length/this.pageSize)
            }
        },
    }
</script>