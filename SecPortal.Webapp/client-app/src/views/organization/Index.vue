<template>
  <div>
    <div class="intro-y flex flex-col sm:flex-row items-center mt-8">
      <h2 class="text-lg font-medium mr-auto">Organizations</h2>
      <div class="w-full sm:w-auto flex mt-4 sm:mt-0">
        <button class="btn btn-primary shadow-md mr-2" @click="showFormModal()">
          Add New Organization
        </button>
      </div>
    </div>
    <!-- BEGIN: HTML Table Data -->
    <div class="intro-y box p-5 mt-5">
      <div class="overflow-x-auto scrollbar-hidden">
        <div
          id="tabulator"
          ref="tableRef"
          class="mt-5 table-report table-report--tabulator"
        ></div>
      </div>
    </div>
    <!-- END: HTML Table Data -->
    <OrganizationForm
      :formTitle="formTitle"
      :model="model"
      :saveData="saveData"
    >
    </OrganizationForm>
  </div>
</template>

<script>
import { TabulatorFull as Tabulator } from 'tabulator-tables'
import { defineComponent, ref, onMounted } from 'vue'
import {
  GetAll,
  GetData,
  CreateData,
  UpdateData
} from '@/core/services/entities/organizations.service'
import OrganizationForm from './Form.vue'

export default defineComponent({
  components: {
    OrganizationForm
  },
  setup() {
    const pageSize = ref(20)
    const pageSizeSelector = ref([20, 40, 60])
    const tableRef = ref()
    const tabulator = ref()
    const message = ref('')
    const records = ref([])
    const totalRows = ref(0)
    const formTitle = ref('Form Add New Organization')

    const initTabulator = async () => {
      const response = await GetAll()

      if (response.status === 200) {
        if (response.data.isSuccess) {
          records.value = response.data.data
          totalRows.value = response.data.totalRows
        } else {
          alert(response.data.message)
        }
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
          {
            title: 'ACTIONS',
            minWidth: 200,
            field: 'actions',
            responsive: 1,
            hozAlign: 'center',
            vertAlign: 'middle',
            print: false,
            download: false,
            formatter(cell) {
              const a = cash(`<div class="flex lg:justify-center items-center">
                      <a class="btn-edit flex items-center mr-3" href="javascript:;" data-id="${
                        cell.getData().id
                      }">
                      <i data-feather="check-square" class="w-4 h-4 mr-1"></i> Edit
                      </a>
                      </div>`)
              cash(a).on('click', function(e) {
                if (e.target.className == 'btn-edit flex items-center mr-3') {
                  showFormModal(cell.getData().id)
                }
              })
              return a[0]
            }
          }
        ],
        renderComplete() {
          feather.replace({
            'stroke-width': 1.5
          })
        }
      })
    }

    const showFormModal = async id => {
      await clearForm()

      if (id > 0) {
        formTitle.value = 'Form Edit Data'
        model.value = await GetData(id)
      }

      cash('#add-edit-modal').modal('show')
    }

    const clearForm = async () => {
      model.value = {
        name: '',
        description: '',
        primaryContactEmail: '',
        id: 0
      }
    }

    const saveData = async () => {
      if (model.value.name == '') {
        message.value += 'name must be filled, '
      }

      if (model.value.primaryContactEmail == '') {
        message.value += 'Primary Contact Email must be filled, '
      }

      const isUpdate = model.value.id != ''
      const formData = new FormData()
      for (const key in model.value) {
        formData.append(key, model.value[key])
      }

      let submitResponse

      if (isUpdate) {
        submitResponse = await UpdateData(formData)
      } else {
        submitResponse = await CreateData(formData)
      }

      if (submitResponse) {
        try {
          await initTabulator()
        } finally {
          hideFormModal()
        }
      }
    }

    const hideFormModal = () => {
      cash('#add-edit-modal').modal('hide')
    }

    onMounted(async () => {
      await initTabulator()
    })

    return {
      tableRef,
      tabulator,
      message,
      records,
      showFormModal,
      hideFormModal,
      saveData
    }
  }
})
</script>
