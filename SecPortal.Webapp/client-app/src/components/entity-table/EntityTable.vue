<template>
  <!-- BEGIN: HTML Table Data -->
  <div class="intro-y box p-5 mt-5">
    <div class="flex flex-col sm:flex-row sm:items-end xl:items-start">
      <form
        id="tabulator-html-filter-form"
        class="xl:flex sm:mr-auto"
        v-if="filters.length > 0"
      >
        <div class="sm:flex items-center sm:mr-4"></div>
        <div class="sm:flex items-center sm:mr-4">
          <label class="w-12 flex-none xl:w-auto xl:flex-initial mr-2"
            >Field</label
          >
          <select
            id="tabulator-html-filter-field"
            v-model="filter.field"
            class="form-select w-full sm:w-32 2xl:w-full mt-2 sm:mt-0 sm:w-auto"
          >
            <option v-for="item in filters" :value="item.key" :key="item.key">
              {{ item.value }}
            </option>
          </select>
        </div>
        <div class="sm:flex items-center sm:mr-4 mt-2 xl:mt-0">
          <label class="w-12 flex-none xl:w-auto xl:flex-initial mr-2"
            >Value</label
          >
          <input
            id="tabulator-html-filter-value"
            v-model="filter.value"
            @keypress.enter.prevent="onFilter"
            type="text"
            class="form-control sm:w-40 2xl:w-full mt-2 sm:mt-0"
            placeholder="Search..."
          />
        </div>
        <div class="mt-2 xl:mt-0">
          <button
            id="tabulator-html-filter-go"
            type="button"
            class="btn btn-primary w-full sm:w-16"
            @click="onFilter"
          >
            Go
          </button>
          <button
            id="tabulator-html-filter-reset"
            type="button"
            class="btn btn-secondary w-full sm:w-16 mt-2 sm:mt-0 sm:ml-1"
            @click="onResetFilter"
          >
            Reset
          </button>
        </div>
      </form>
      <form id="tabulator-html-filter-form" class="xl:flex sm:mr-auto">
        &nbsp;
      </form>
      <div class="flex mt-5 sm:mt-0">
        <button
          id="tabulator-print"
          class="btn btn-outline-secondary w-1/2 sm:w-auto mr-2"
          @click="onPrint"
        >
          <PrinterIcon class="w-4 h-4 mr-2" /> Print
        </button>
        <div class="dropdown w-1/2 sm:w-auto">
          <button
            class="dropdown-toggle btn btn-outline-secondary w-full sm:w-auto"
            aria-expanded="false"
          >
            <FileTextIcon class="w-4 h-4 mr-2" /> Export
            <ChevronDownIcon class="w-4 h-4 ml-auto sm:ml-2" />
          </button>
          <div class="dropdown-menu w-40">
            <div class="dropdown-menu__content box dark:bg-dark-1 p-2">
              <a
                id="tabulator-export-csv"
                href="javascript:;"
                class="
                  flex
                  items-center
                  block
                  p-2
                  transition
                  duration-300
                  ease-in-out
                  bg-white
                  dark:bg-dark-1
                  hover:bg-gray-200
                  dark:hover:bg-dark-2
                  rounded-md
                "
                @click="onExportCsv"
              >
                <FileTextIcon class="w-4 h-4 mr-2" /> Export CSV
              </a>
              <a
                id="tabulator-export-json"
                href="javascript:;"
                class="
                  flex
                  items-center
                  block
                  p-2
                  transition
                  duration-300
                  ease-in-out
                  bg-white
                  dark:bg-dark-1
                  hover:bg-gray-200
                  dark:hover:bg-dark-2
                  rounded-md
                "
                @click="onExportJson"
              >
                <FileTextIcon class="w-4 h-4 mr-2" /> Export JSON
              </a>
              <a
                id="tabulator-export-html"
                href="javascript:;"
                class="
                  flex
                  items-center
                  block
                  p-2
                  transition
                  duration-300
                  ease-in-out
                  bg-white
                  dark:bg-dark-1
                  hover:bg-gray-200
                  dark:hover:bg-dark-2
                  rounded-md
                "
                @click="onExportHtml"
              >
                <FileTextIcon class="w-4 h-4 mr-2" /> Export HTML
              </a>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="overflow-x-auto scrollbar-hidden">
      <div
        id="tabulator"
        ref="tableRef"
        class="mt-5 table-report table-report--tabulator"
      ></div>
    </div>
  </div>
  <!-- END: HTML Table Data -->
</template>

<script>
import { useRouter } from 'vue-router'
import { defineComponent, ref, reactive, onMounted, inject } from 'vue'
import feather from 'feather-icons'
import { TabulatorFull as Tabulator } from 'tabulator-tables'
import { useStore } from 'vuex'
import { BASE_URL } from '@/core/constant/env'
import { OperatorEnum } from '@/core/services/helper.service'
import { useToast } from 'vue-toastification'
import Swal from 'sweetalert2'

export default defineComponent({
  props: {
    editRoute: {
      type: String,
      required: false,
      default: null
    },
    controller: {
      type: String,
      required: true
    },
    columns: {
      type: Array,
      required: true
    },
    isEditable: {
      type: Boolean,
      default: true
    },
    isDeletable: {
      type: Boolean,
      default: true
    },
    isViewable: {
      type: Boolean,
      default: false
    },
    title: {
      type: String,
      required: true
    },
    onEdit: {
      type: Function,
      default: null
    },
    onView: {
      type: Function,
      default: null
    },
    customParams: {
      type: Array,
      required: false,
      default: () => {
        return []
      }
    },
    overrideForm: {
      type: String,
      required: false
    },
    filters: {
      type: Array,
      default: () => {
        return []
      }
    }
  },
  setup(props) {
    const router = useRouter()
    const toast = useToast()
    const store = useStore()
    const tableRef = ref()
    const tabulator = ref()
    const filter = reactive({
      field: 'Name',
      type: 'like',
      value: ''
    })

    const token = store.getters.currentUser.accessToken

    const constructURL = params => {
      let uri = ''
      for (const key in params) {
        if (Object.prototype.hasOwnProperty.call(params, key)) {
          if (key === 'sort') {
            params[key].forEach((item, index) => {
              if (uri.length > 0) {
                uri += '&'
              }

              uri += `sort[${index}][field]=${item.field}&sort[${index}][dir]=${item.dir}`
            })
          } else if (params[key]) {
            if (uri.length > 0) {
              uri += '&'
            }

            uri += `${key}=${encodeURI(params[key])}`
          }
        }
      }

      return uri
    }

    const clearPreviousFilter = params => {
      const removedParams = []
      for (const key in params) {
        if (Object.prototype.hasOwnProperty.call(params, key)) {
          if (key.includes('.')) {
            removedParams.push(key)
          }
        }
      }

      removedParams.forEach(param => {
        delete params[param]
      })
    }

    const initTabulator = () => {
      // const columns = JSON.parse(JSON.stringify(props.columns))
      const columns = props.columns
      if (props.isEditable) {
        columns.push({
          title: '',
          width: 75,
          responsive: 0,
          hozAlign: 'center',
          vertAlign: 'middle',
          print: false,
          headerSort: false,
          download: false,
          formatter(cell) {
            const a = cash(`<div class="flex lg:justify-center items-center">
                <a class="flex items-center mr-3 btn-edit" href="javascript:;">
                  <i data-feather="check-square" class="w-4 h-4 mr-1"></i> Edit
                </a>
              </div>`)
            cash(a).on('click', function() {
              if (props.onEdit) {
                props.onEdit(cell)
              } else if (props.editRoute) {
                router.push({
                  name: props.editRoute,
                  params: { id: cell.getRow().getData().id }
                })
              } else {
                store.dispatch('slideover/ChangeShownState', {
                  isShown: true,
                  title: `Edit ${props.title}`,
                  component: props.overrideForm
                    ? props.overrideForm
                    : `${props.controller}Form`,
                  id: cell.getRow().getData().id
                })
              }
            })

            return a[0]
          }
        })
      }

      if (props.isDeletable) {
        columns.push({
          title: '',
          width: 75,
          responsive: 0,
          hozAlign: 'center',
          vertAlign: 'middle',
          print: false,
          download: false,
          headerSort: false,
          formatter(cell) {
            const a = cash(`<div class="flex lg:justify-center items-center">
                <a class="flex items-center text-theme-21" href="javascript:;">
                  <i data-feather="trash-2" class="w-4 h-4 mr-1"></i> Delete
                </a>
              </div>`)
            cash(a).on('click', function() {
              Swal.fire({
                title: 'Hey!',
                text: 'Are you sure you want to remove this item?',
                confirmButtonColor: '#2F5AD8',
                cancelButtonColor: '#E5EAF1',
                showCancelButton: true,
                confirmButtonText: 'Delete'
              }).then(async result => {
                /* Read more about isConfirmed, isDenied below */
                if (result.isConfirmed) {
                  const rowId = cell.getRow().getData().id
                  const response = await window.axios.delete(
                    `${BASE_URL}api/${props.controller}/${rowId}`
                  )
                  if (response.status === 200) {
                    if (response.data.isSuccess) {
                      toast.success(response.data.message, {
                        timeout: 2000
                      })
                    } else {
                      toast.error(response.data.message, {
                        timeout: 2000
                      })
                    }

                    tabulator.value.setPage(tabulator.value.getPage())
                  }
                }
              })
            })

            return a[0]
          }
        })
      }

      if (props.isViewable) {
        columns.push({
          title: '',
          width: 75,
          responsive: 0,
          hozAlign: 'center',
          vertAlign: 'middle',
          print: false,
          download: false,
          headerSort: false,
          formatter(cell) {
            const a = cash(`<div class="flex lg:justify-center items-center">
                <a class="flex items-center text-theme-20" href="javascript:;">
                  <i data-feather="eye" class="w-4 h-4 mr-1"></i> View
                </a>
              </div>`)
            cash(a).on('click', function() {
              props.onView(cell.getRow().getData().id)
            })

            return a[0]
          }
        })
      }

      tabulator.value = new Tabulator(tableRef.value, {
        ajaxURL: `${BASE_URL}api/${props.controller}`,
        filterMode: 'remote',
        ajaxConfig: {
          headers: {
            // eslint-disable-next-line
            Authorization: `Bearer ${token}`
          }
        },
        ajaxURLGenerator: function(url, config, params) {
          clearPreviousFilter(params)
          if (params.filter) {
            params.filter.forEach(element => {
              params[`${element.field}.Operator`] = OperatorEnum(
                element.type.toUpperCase()
              )
              params[`${element.field}.Value`] = element.value
            })

            delete params.filter
          }

          params['isActive.Operator'] = 4
          params['isActive.Value'] = true

          if (props.customParams) {
            props.customParams.forEach(element => {
              for (const key in element) {
                if (Object.prototype.hasOwnProperty.call(element, key)) {
                  params[key] = element[key]
                }
              }
            })
          }

          const result = url + '?' + constructURL(params)
          return result
        },
        ajaxResponse: function(url, params, response) {
          const pageSize = tabulator.value.getPageSize()
          const lastPage = Math.ceil(response.totalRows / pageSize)
          response.last_page = lastPage === 0 ? 1 : lastPage
          return response
        },
        sortMode: 'remote',
        printAsHtml: true,
        printStyled: true,
        pagination: true,
        paginationMode: 'remote',
        paginationSize: 10,
        paginationSizeSelector: [10, 25, 50, 100],
        dataSendParams: {
          page: 'pageNo',
          size: 'recordsPerPage'
        },
        layout: 'fitColumns',
        responsiveLayout: 'collapse',
        placeholder: 'No matching records found',
        columns: columns,
        movableColumns: true,
        resizableRows: true
      })

      tabulator.value.on('renderComplete', function() {
        feather.replace({
          'stroke-width': 1.5
        })
      })
    }

    // Redraw table onresize
    const reInitOnResizeWindow = () => {
      window.addEventListener('resize', () => {
        tabulator.value.redraw()
        feather.replace({
          'stroke-width': 1.5
        })
      })
    }

    // Filter function
    const onFilter = () => {
      if (filter.value) {
        tabulator.value.setFilter(filter.field, filter.type, filter.value)
      }
    }

    // On reset filter
    const onResetFilter = () => {
      filter.field = 'name'
      filter.type = 'like'
      filter.value = ''
      tabulator.value.clearFilter()
    }

    // Export
    const onExportCsv = () => {
      tabulator.value.download('csv', 'data.csv')
    }

    const onExportJson = () => {
      tabulator.value.download('json', 'data.json')
    }

    const onExportHtml = () => {
      tabulator.value.download('html', 'data.html', {
        style: true
      })
    }

    // Print
    const onPrint = () => {
      tabulator.value.print()
    }

    const emitter = inject('emitter')
    emitter.on('refresh_table', () => {
      tabulator.value.setPage(tabulator.value.getPage())
    })

    onMounted(() => {
      initTabulator()
      reInitOnResizeWindow()
    })

    return {
      tableRef,
      filter,
      onFilter,
      onResetFilter,
      onExportCsv,
      onExportJson,
      onExportHtml,
      onPrint
    }
  }
})
</script>

<style></style>
