<template>
    <teleport to="#breadcrumb-wrapper" v-if="isReady">
      <router-link :to="{ name: 'adminDashboard' }"> Dashboard </router-link>
      <ChevronRightIcon class="breadcrumb__icon" />
      <a href="" class="breadcrumb--active">Users</a>
    </teleport>
    <div>
      <div class="intro-y flex flex-col sm:flex-row items-center mt-8">
        <h2 class="text-lg font-medium mr-auto">Users</h2>
        <div class="w-full sm:w-auto flex mt-4 sm:mt-0">
          <button
            class="btn btn-primary shadow-md mr-2"
            @click="openSlideoutPanel"
          >
            Add New User
          </button>
        </div>
      </div>
      <entitiesTable
        :controller="'users'"
        :columns="columns"
        :title="'Users'"
        :customParams="params"
        :overrideForm="'editUsersForm'"
      ></entitiesTable>
    </div>
  </template>
  
  <script>
  import entitiesTable from '@/components/entity-table/EntityTable.vue'
  import breadcrumbMixin from '@/mixins/BreadcrumbMixin'
  import { mapActions } from 'vuex'
  
  export default {
    components: {
      entitiesTable: entitiesTable
    },
    mixins: [breadcrumbMixin],
    data() {
      let _this = this
      return {
        filters: [
          { key: 'firstName', value: 'First Name' },
          { key: 'lastName', value: 'Last Name' }
        ],
        params: [
          {
            roles: 1,
            isActive: 1
          }
        ],
        columns: [
          {
            formatter: 'responsiveCollapse',
            width: 40,
            minWidth: 30,
            hozAlign: 'center',
            resizable: false,
            headerSort: false
          },
  
          // For HTML table
          {
            title: 'FULL NAME',
            minWidth: 200,
            responsive: 0,
            field: 'fullName',
            vertAlign: 'middle',
            print: false,
            download: false
          },
          {
            title: 'EMAIL',
            minWidth: 200,
            responsive: 0,
            field: 'email',
            vertAlign: 'middle',
            print: false,
            download: false
          },
          {
            title: 'CREATED AT',
            minWidth: 200,
            responsive: 1,
            field: 'createdAtString',
            vertAlign: 'middle',
            print: false,
            download: false
          },
          {
            title: '',
            width: 175,
            responsive: 0,
            hozAlign: 'center',
            vertAlign: 'middle',
            print: false,
            download: false,
            headerSort: false,
            formatter(cell) {
              const a = cash(`<div class="flex lg:justify-center items-center">
                  <a class="flex items-center text-theme-20" href="javascript:;">
                    <i data-feather="key" class="w-4 h-4 mr-1"></i> Change Password
                  </a>
                </div>`)
              cash(a).on('click', function () {
                _this.ChangeShownState({
                  isShown: true,
                  title: 'Change Password',
                  component: 'changeAdminPasswordForm',
                  id: cell.getRow().getData().id
                })
              })
  
              return a[0]
            }
          },
          // For print format
          {
            title: 'FULL NAME',
            field: 'fullname',
            visible: false,
            print: true,
            download: true
          },
          {
            title: 'EMAIL',
            field: 'email',
            visible: false,
            print: true,
            download: true
          },
          {
            title: 'CREATED AT',
            field: 'createdAtString',
            visible: false,
            print: true,
            download: true
          }
        ]
      }
    },
    methods: {
      ...mapActions('slideover', ['ChangeShownState']),
      openSlideoutPanel() {
        this.ChangeShownState({
          isShown: true,
          title: 'Add Users',
          component: 'adminsForm'
        })
      }
    }
  }
  </script>
  