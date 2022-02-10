<template>
    <div>
        <b-container>
            <Header :name="name">
                <div class="btn-toolbar mb-2 mb-md-0">
                    <div class="btn-group me-2">
                        <button type="button" class="btn btn-sm btn-outline-secondary" @click="OpenNewBrand()">Crea nuovo brand</button>
                    </div>
                </div>
            </Header>
            <Brands
                :list="list"
                :loaded="loaded"
                :error="error"
                :page="page"
                :maxVisibleButtons="maxVisibleButtons"
                @page="pageChange"
                @openmodal="OpenModal"
                @closemodal="CloseModal"
                @openDetail="OpenDetail"
                @search="Search"
                @openedit="OpenEdit"
            />
            
            <Pagination
                :totalPages="totalpages"
                :maxVisibleButtons="maxVisibleButtons"
                :page="page"
                @pageChanged="pageChange"
            />
            <DeleteModal
                :open="open"
                @closemodal="CloseModal"
                @delete="Delete"
            />
        </b-container>
    </div>
</template>

<script>
    import Header from '@/components/Header.vue'
    import Brands from '@/components/Brands.vue'
    //import Table from '@/components/Table.vue'
    import Pagination from '@/components/Pagination.vue'
    import DeleteModal from '@/components/DeleteModal.vue'
    import {Factory} from './../wrappers/Factory'
    const BrandsRepo = Factory.get('brands')

    export default {
        
        components: {
            Brands,
            Header,
            Pagination,
            DeleteModal,
            //Table,
        },

        data(){
            return{
                name: 'Brand',
                page: 1,
                maxVisibleButtons: 7,
                pageSize: 10,
                list: {},
                loaded: false,
                error: false,
                open: false,
                idEl: 0,
                params: {},
                response: ""
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
                const {data} = await BrandsRepo.getallpagedsized(this.pageSize, this.page, this.params)
                this.loaded = true
                this.list = data;
            },
            OpenModal(id){
                this.idEl = id
                this.open = true
            },
            CloseModal(){
                this.open = false
            },
            OpenDetail(val){
                this.$router.push('/brands/'+val)
            },
            OpenNewBrand(){
                this.$router.push('/brands/new')
            },
            Delete(){
                const {data} = BrandsRepo.delete(this.idEl)
                this.open = false
                this.response = data
            },
            Search(filter){
                if(filter){
                    this.params.search = filter
                }else{
                    delete this.params.search
                }
                this.loadElements();
            },
            OpenEdit(id){
                this.$router.push('/brands/edit/' + id)
            },
        },

        created() {
            this.loadElements();
        }
    }
</script>