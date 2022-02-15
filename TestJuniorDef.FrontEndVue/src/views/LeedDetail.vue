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
    import Commonutils from '@/utilities/Commonutils.js'
    import Header from '@/components/Header.vue'
    import LeedDetail from '@/components/LeedDetail.vue'
    import Pagination from '@/components/Pagination.vue'
    import {Factory} from './../wrappers/Factory'
    const LeedsRepo = Factory.get('leeds')

    export default {

        mixins: [DetailPagesUtils, Commonutils],
        
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
                this.listpaginated = this.list.replies.slice(skip, take)    //Client side pagination handler
            },

            async loadElements(){
                const {data} = await LeedsRepo.getById(this.$route.params.id)
                this.loadElementsComponent(data)    //Common request response handler (DetailPagesUtils.js)
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