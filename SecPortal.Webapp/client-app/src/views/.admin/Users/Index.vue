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
            title: 'ORGANIZATION NAME',
            minWidth: 200,
            responsive: 0,
            field: 'organizationName',
            vertAlign: 'middle',
            print: false,
            download: false
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
          },
          {
            title: 'ORGANIZATION NAME',
            field: 'organizationName',
            visible: false,
            print: true,
            download: true
          },
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
  