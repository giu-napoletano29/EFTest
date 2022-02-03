<template>
  <div>
    <b-table striped hover :items="items" :fields="fields" thead-class="prova">
        <template slot="thead-top">
            <b-tr role='row'>
                <th role='columnheader' scope='col' colspan='3' tabindex='4' aria-colindex='4' aria-sort='none' class=''><div>Test</div><span class='sr-only'></span></th>
            </b-tr>
        </template>
    </b-table>
  </div>
</template>

<script>
  export default {
    data() {
      return {
        // Note 'isActive' is left out and will not appear in the rendered table
        fields: [
          {
            key: 'last_name',
            sortable: true
          },
          {
            key: 'first_name',
            sortable: false
          },
          {
            key: 'age',
            label: 'Person age',
            sortable: true,
            // Variant applies to the whole column, including the header and footer
            variant: 'danger'
          }
        ],
        items: [
          { isActive: true, age: 40, first_name: 'Dickerson', last_name: 'Macdonald' },
          { isActive: false, age: 21, first_name: 'Larsen', last_name: 'Shaw' },
          { isActive: false, age: 89, first_name: 'Geneva', last_name: 'Wilson' },
          { isActive: true, age: 38, first_name: 'Jami', last_name: 'Carney' }
        ]
      }
    },
  }
</script>