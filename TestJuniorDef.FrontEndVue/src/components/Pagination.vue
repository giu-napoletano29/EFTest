<template>
    <div>
        <nav aria-label="Page navigation example">
            <ul class="pagination justify-content-center">
                <li class="page-item" :class="{ disabled: IsFirstPage }">
                    <a class="page-link" href="#" @click="changePage(prevPage)">Indietro</a>
                </li>
                <li class="page-item">
                    <a class="page-link" href="#" @click="changePage(1)">Prima pagina</a>
                </li>
                <li v-for="i in pages" :key="i.num" class="page-item" :class="{ active: IsCurrentPage(i), disabled: i.isDisabled }"> 
                    <a class="page-link" href="#" @click="changePage(i.num)">{{i.num}}</a>
                </li>
                <li class="page-item">
                    <a class="page-link" href="#" @click="changePage(computedTotalPages)">Ultima pagina</a>
                </li>
                <li class="page-item" :class="{ disabled: IsLastPage }">
                    <a class="page-link" href="#" @click="changePage(nextPage)" >Avanti</a>
                </li>
            </ul>
        </nav>
    </div>
</template>

<script>
export default {
    props: {
        maxVisibleButtons: {
        type: Number,
        required: false,
        default: 3
        },    
        totalPages: {
        type: Number,
        required: true
        },
        page: {
        type: Number,
        required: true
        }
    },

    data(){
        return{
            paginationPageMarginBefore: 3
        }
    },

    computed: {
        computedTotalPages(){
            return this.totalPages > 0 ? this.totalPages:1
        },

        IsFirstPage(){
            return this.page === 1
        },
        IsLastPage(){
            return this.page === this.computedTotalPages
        },
        prevPage(){
            return this.page-1 > 0 ? this.page-1 : this.page
        },

        nextPage(){
            return this.page+1 <= this.computedTotalPages ? this.page+1 : this.page
        },

        startPage() {
            // When on the first page
            if (this.page === 1) {
                return 1;
            }

            // When on the last page
            if (this.page === this.computedTotalPages) {
                const startpage = this.computedTotalPages - this.maxVisibleButtons + 1
                return startpage > 1 ? startpage:1;
            }

            const pageDifference = this.page - this.paginationPageMarginBefore + this.maxVisibleButtons
            //8 - 3 + 7 = 12
            //9 - 3 + 7 = 13

            if(pageDifference > (this.computedTotalPages+1)){
                //return this.page - this.paginationPageMarginBefore + (pageDifference - this.totalPages - 1)
                return this.page - (this.paginationPageMarginBefore + (pageDifference - this.computedTotalPages - 1))
                //9 - (3 + (13 - 11 - 1)) = 
            }

            // When inbetween
            return this.page - this.paginationPageMarginBefore > 1 ? this.page - this.paginationPageMarginBefore:1;
        },

        pages() {
            const range = [];

            for (let i = this.startPage; i <= Math.min(this.startPage + this.maxVisibleButtons - 1, this.computedTotalPages); i++ ) 
            {
                range.push({
                num: i,
                isDisabled: i === this.page
                });
            }

            return range;
        },
    },

    methods: {
        async changePage(pageNum) {
            await this.$emit('pageChanged', pageNum)

        },
        IsCurrentPage(cPage){
            return this.page === cPage
        },
    },
}
</script>
