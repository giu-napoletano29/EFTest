<template>
    <div>
        <b-container>
            <Header :name="name">
                <div class="btn-toolbar mb-2 mb-md-0">
                    <div class="btn-group me-2">
                        <button type="button" class="btn btn-sm btn-outline-secondary">Crea nuovo brand</button>
                    </div>
                </div>
            </Header>
            <Brands
                :page="page"
                :maxVisibleButtons="maxVisibleButtons"
                @page="pageChange"
            />
        </b-container>
    </div>
</template>

<script>
    import Header from '@/components/Header.vue'
    import Brands from '@/components/Brands.vue'

    export default {
        
        components: {
            Brands,
            Header
        },

        data(){
            return{
                name: 'Brand',
                page: 1,
                maxVisibleButtons: 7
            }
        },

        methods: {
            pageChange(page) {
                this.page = page
            }
        },
    }
</script>