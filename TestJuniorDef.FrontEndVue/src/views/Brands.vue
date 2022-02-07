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
            />
        </b-container>
    </div>
</template>

<script>
    import Header from '@/components/Header.vue'
    import Brands from '@/components/Brands.vue'
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
        },

        data(){
            return{
                name: 'Brand',
                page: 1,
                maxVisibleButtons: 7,
                list: {},
                loaded: false,
                error: false,
                open: false,
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
                const {data} = await BrandsRepo.getallpaged(this.page)
                this.loaded = true
                this.list = data;
            },
            OpenModal(){
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
            }
        },

        created() {
            this.loadElements();
        }
    }
</script>