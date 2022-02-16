<template>
    <div>
        <Header :name="name">
            <div class="btn-toolbar mb-2 mb-md-0">
                <div class="btn-group me-2">
                    <button type="button" class="btn btn-sm btn-outline-primary" @click="OpenNewBrand()">Crea nuovo brand</button>
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
        >
            <Table
                :loaded="loaded"
                :error="error"
                :nrow="nrow"
                :ntab="ntab"
            />
        </Brands>
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
        <RedirectModal
            :success="successModalOpen"
            :OpError="OpError"
            :ErrMsg="ErrMsg"
            @closemodal="CloseModal"
        />  
        <ToastMessage
            :open="showToast"
            @closetoast="CloseToast"
        />
    </div>
</template>

<script>
    import MainPagesUtils from '@/utilities/MainPagesUtils.js' 
    import BrandsProductsUtils from '@/utilities/BrandsProductsUtils.js'
    import Header from '@/components/Header.vue'
    import ToastMessage from '@/components/ToastMessage.vue'
    import Brands from '@/components/Brands.vue'
    import Table from '@/components/Table.vue'
    import Pagination from '@/components/Pagination.vue'
    import DeleteModal from '@/components/DeleteModal.vue'
    import RedirectModal from '@/components/RedirectModal.vue'
    import {Factory} from './../wrappers/Factory'
    const BrandsRepo = Factory.get('brands')

    export default {
        mixins: [MainPagesUtils, BrandsProductsUtils],
        
        components: {
            Brands,
            Header,
            Pagination,
            DeleteModal,
            Table,
            RedirectModal,
            ToastMessage
        },

        data(){
            return{
                name: 'Brand',
                nrow: 6,
                ntab: 1,
                response: "",
            }
        },

        methods: {
            async loadElements(){
                const {data} = await BrandsRepo.getallpagedsized(this.pageSize, this.page, this.params)
                this.loaded = true
                this.list = data;
            },

            OpenDetail(val){
                this.$router.push('/brands/'+val)
            },
            OpenNewBrand(){
                this.$router.push('/brands/new')
            },

            SpecRedirect(){
                if(this.$route.name!='Brands'){
                    this.$router.push({name: 'Brands'})
                }
            },
            
            Delete(){
                const data = BrandsRepo.delete(this.idEl)
                this.open = false
                this.DeleteSpecComponent(data)  //delete handling generic for the components (BrandsProductsUtils.js)
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
        },

        mounted(){
            if(this.$route.name==='BrandsSuccess'){
                // this.successModalOpen = true
                this.OpenToast()
            }else if(this.$route.name==='BrandsError'){
                this.OpError = true
                this.successModalOpen = true
                this.ErrMsg = this.$route.params.Error
            }
        }
    }
</script>