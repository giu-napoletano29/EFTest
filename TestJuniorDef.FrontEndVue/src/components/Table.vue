<template>
	<tbody v-show="!loaded && !error" cellpadding="0" cellspacing="0" width="100%" class="table_loader">
		<tr v-for="index in this.nrow" :key="index">
			<td>
				<span></span>
			</td>
			<td>
				<span></span>
				<span class="sub-temp"></span>
				<span class="sub-temp sub-temp-three"></span>			
			</td>
			<td v-for="i in ntab" :key="i">
				<span></span>
			</td>
		</tr>		
	</tbody>
</template>


<script>
    export default {
        props: {
            loaded: Boolean,
            error: Boolean,
            nrow: Number,
            ntab: Number,
        },
    }
</script>



<style>

.table_loader {
    background-color: #FFF;
    color: #666;
    width: 100%;
    padding: 0;
}
.table_loader th{
    background: #F7F9FA;
    border-top: 1px solid #E5E5E5;
    border-bottom: 1px solid #E5E5E5;
    padding: 12px 10px;
	text-align: left;
}
.table_loader td{
    border-top: 0px solid #E5E5E5;
    border-bottom: 1px solid #E5E5E5;
    padding: 12px 10px;
    vertical-align: top;
}
.table_loader span{
width: calc(100% - 5px);
    height: 15px;
    border-radius: 3px;
    background-image: linear-gradient(90deg, #ddd 0px, #e8e8e8 40px, #ddd 80px);
    background-size: 600px;
    animation: shine-lines-table 1.6s infinite linear;
    display: inline-block;
    min-width:15px;
}
.table_loader span.sub-temp{
	width: calc(100% - 50%);
	margin-top: 5px;
}
.table_loader span.sub-temp-three{
	width:calc(100% - 70%);
	display:block
}



@keyframes shine-lines-table {
  0% {
	opacity:0.4;
  }
  40%{
	opacity:0.8;
  }
  100% {
	opacity:0.4;
  }
}

</style>