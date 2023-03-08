<template>
    <div>
        <div class="intro-y flex flex-col sm:flex-row items-center mt-8">
            <h2 class="text-lg font-medium mr-auto">Organizations</h2>
            <div class="w-full sm:w-auto flex mt-4 sm:mt-0">
                <button class="btn btn-primary shadow-md mr-2">Add New Organization</button>
            </div>
        </div>
        <!-- BEGIN: HTML Table Data -->
        <div class="intro-y box p-5 mt-5">
            <div class="overflow-x-auto scrollbar-hidden">
                <div id="tabulator" ref="tableRef" class="mt-5 table-report table-report--tabulator"></div>
            </div>
        </div>
        <!-- END: HTML Table Data -->
    </div>
</template>

<script>
import Tabulator from 'tabulator-tables';
import { defineComponent, onMounted } from 'vue';
import { GetAll } from '@/core/services/entities/organizations.service';

export default defineComponent({
    setup() {
        const pageSize = ref(20)
        const pageSizeSelector = ref([20, 40, 60])
        const tableRef = ref()
        const tabulator = ref()
        const message = ref('')
        const records = ref([])

        const initTabulator = async () => {
            const response = await GetAll();
            
            if (response.status === 200) {
                records = response.data;
            }

            tabulator.value = new Tabulator(tableRef.value, {
                data: records,
                placeholder: 'No matching records found',
                selectable: false,
                pagination: 'remote',
                paginationSize: pageSize.value,
                paginationSizeSelector: pageSizeSelector.value,
                paginationDataSent: {
                    page: 'pageNo',
                    sorters: 'tabulatorSorter'
                },
                paginationDataReceived: {
                    last_page: 'totalRows'
                },
                ajaxSorting: true,
                layout: 'fitData',
                columns: [
                    // For HTML table
                    {
                        title: 'Organization Name',
                        minWidth: 200,
                        field: 'name',
                        hozAlign: 'left',
                        vertAlign: 'middle'
                    },
                    {
                        title: 'Description',
                        minWidth: 400,
                        field: 'description',
                        hozAlign: 'left',
                        vertAlign: 'middle'
                    },
                    {
                        title: 'Primary Contact Email',
                        minWidth: 150,
                        field: 'primaryContactEmail',
                        hozAlign: 'left',
                        vertAlign: 'middle'
                    },
                ],
                renderComplete() {
                    feather.replace({
                        'stroke-width': 1.5
                    })
                }
            })
        }

        onMounted(() => {
            initTabulator()
        })

        return {
            tableRef,
            tabulator,
            message,
            records,
        }
    }
})
</script>