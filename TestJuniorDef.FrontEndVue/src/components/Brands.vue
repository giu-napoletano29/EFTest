<template>
    <div>
        <b-overlay :show="!loaded && !error" rounded="sm">
        <div class="brand">
            <div v-if="loaded">
                <table class="table table-striped table-hover">
                    <thead>
                        <th> ID </th>
                        <th> Nome brand </th>
                    </thead>
                    <tbody>
                        <tr v-for="(l,i) in list.elements" :key="i"> 
                            <td>{{ l.id }}</td> 
                            <td>{{ l.brandName }}</td>
                            <td>
                                <div class="container">
                                    <button type="button" class="btn btn-primary">Modifica</button>
                                    <!-- <DeleteModal/> -->
                                    <button type="button" class="btn btn-danger" @click="modal.show()">Elimina</button>
                                </div>
                            </td> 
                        </tr>
                    </tbody>
                </table>
            </div>
            <nav aria-label="Page navigation example">
                <ul class="pagination justify-content-center">
                    <li class="page-item" :class="{ disabled: IsFirstPage }">
                        <a class="page-link" @click="changePage(prevPage)">Previous</a>
                    </li>
                    <li v-for="i in pages" :key="i.num" class="page-item" :class="{ active: IsCurrentPage(i), disabled: i.isDisabled }"> 
                        <a class="page-link" href="#" @click="changePage(i.num)">{{i.num}}</a>
                    </li>
                    <!-- <li class="page-item" :class="{ active: IsCurrentPage(1) }"><a class="page-link" href="#" @click="changePage(1)">1</a></li>
                    <li class="page-item"><a class="page-link" href="#" @click="changePage(2)">2</a></li>
                    <li class="page-item"><a class="page-link" href="#" @click="changePage(3)">3</a></li> -->

                    <li class="page-item" :class="{ disabled: IsLastPage }">
                    <a class="page-link" href="#" @click="changePage(nextPage)" >Next</a>
                    </li>
                </ul>
            </nav>
        </div>

        <div class="modal fade" ref="exampleModal" tabindex="-1" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLabel">Sei sicuro?</h5>
                        <button type="button" class="btn-close" @click="modal.hide()" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        L'azione è irreversibile.
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" @click="modal.hide()">Chiudi</button>
                        <button type="button" class="btn btn-danger">Elimina</button>
                    </div>
                </div>
            </div>
        </div>

        </b-overlay>
        <div v-if="error">
            <h2>An error has occourred!</h2>
        </div>
    </div>
</template>

<script>
import {Factory} from './../wrappers/Factory'
import { Modal } from 'bootstrap'
// import DeleteModal from '@/components/DeleteModal.vue'
const BrandsRepo = Factory.get('brands')

export default {
    // components:{
    //     DeleteModal
    // },
    computed: {
        LastPage(){
            return Math.ceil(this.list.totalElements/this.list.pageSize)
        },
        IsFirstPage(){
            return this.page === 1
        },
        IsLastPage(){
            return this.page === this.LastPage
        },
        prevPage(){
            return this.page-1 > 0 ? this.page-1 : this.page
        },

        nextPage(){
            return this.page+1 <= this.LastPage ? this.page+1 : this.page
        },

        startPage() {
            // When on the first page
            if (this.page === 1) {
                return 1;
            }

            // When on the last page
            if (this.page === this.LastPage) {
                return this.LastPage - this.maxVisibleButtons;
            }

            // When inbetween
            return this.page - 1;
        },

        pages() {
            const range = [];

            for (let i = this.startPage; i <= Math.min(this.startPage + this.maxVisibleButtons - 1, this.LastPage); i++ ) 
            {
                range.push({
                num: i,
                isDisabled: i === this.page
                });
            }

            return range;
        },
    },

    props: {
        page: Number,
        maxVisibleButtons: Number
    },

    data(){
        return{
            list: Object,
            loaded: false,
            error: false,
            modal: null
        }
    },

    methods: {
        async changePage(pageNum) {
            await this.$emit('page', pageNum)
            this.loadElements();

        },
        async loadElements(){
            const {data} = await BrandsRepo.getallpaged(this.page)
            this.loaded = true
            this.list = data;
        },
        IsCurrentPage(cPage){
            return this.page === cPage
        },
    },

    mounted() {
        this.modal = new Modal(this.$refs.exampleModal)
    },

    created() {
        this.loadElements();
    }
}
</script>
