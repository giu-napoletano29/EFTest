<template>
    <div>
        <nav aria-label="Page navigation example">
            <ul class="pagination justify-content-center">
                <li class="page-item" :class="{ disabled: IsFirstPage }">
                    <a class="page-link" @click="changePage(prevPage)">Indietro</a>
                </li>
                <li v-for="i in pages" :key="i.num" class="page-item" :class="{ active: IsCurrentPage(i), disabled: i.isDisabled }"> 
                    <a class="page-link" href="#" @click="changePage(i.num)">{{i.num}}</a>
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


    computed: {
        IsFirstPage(){
            return this.page === 1
        },
        IsLastPage(){
            return this.page === this.totalPages
        },
        prevPage(){
            return this.page-1 > 0 ? this.page-1 : this.page
        },

        nextPage(){
            return this.page+1 <= this.totalPages ? this.page+1 : this.page
        },

        startPage() {
            // When on the first page
            if (this.page === 1) {
                return 1;
            }

            // When on the last page
            if (this.page === this.totalPages) {
                return this.totalPages - this.maxVisibleButtons;
            }

            // When inbetween
            return this.page - 1;
        },

        pages() {
            const range = [];

            for (let i = this.startPage; i <= Math.min(this.startPage + this.maxVisibleButtons - 1, this.totalPages); i++ ) 
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
