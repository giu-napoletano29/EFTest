export default{
    data(){
        return{
            open: false,
            idEl: 0,
            successModalOpen: false,
            OpError: false,
        }
    },

    computed:{
        totalpages(){
            return Math.ceil(this.list.totalElements/this.list.pageSize)
        }
    },

    methods:{
        OpenModal(id){
            this.idEl = id
            this.open = true
        },

        CloseModal(){
            this.open = false
            this.successModalOpen = false //To close success modal
        },

        DeleteSpecComponent(data){
            let self = this;
            data.then(function (response) {
                    if(response.status >= 200 && response.status <= 208){
                        self.OpError = false
                        self.successModalOpen = true
                    }else{
                        self.OpError = true
                        self.successModalOpen = true
                    }
                    self.loadElements();
                });
        },
    },
}