<template>
  <div>
    <DarkModeSwitcher />
    <MobileMenu :dashboard-layout="dashboardLayout" layout="top-menu" />
    <!-- BEGIN: Top Bar -->
    <div
      class="top-bar-boxed border-b border-theme-3 px-4 md:px-6 mb-14 md:mb-8"
    >
      <div class="h-full flex items-center">
        <!-- BEGIN: Logo -->
        <a href="" class="-intro-x hidden md:flex">
          <img
            alt="Tinker Tailwind HTML Admin Template"
            class="w-6"
            :src="require(`@/assets/images/logo.svg`)"
          />
          <span class="text-white text-lg ml-3">
            Tink<span class="font-medium">er</span>
          </span>
        </a>
        <!-- END: Logo -->
        <!-- BEGIN: Breadcrumb -->
        <div
          class="-intro-x breadcrumb breadcrumb--light mr-auto"
          id="breadcrumb-wrapper"
        >
          SecPortal Stock Summary
        </div>
        <!-- END: Breadcrumb -->
      </div>
    </div>
    <!-- END: Top Bar -->
    <!-- BEGIN: Content -->
    <div
      :class="{ 'content--dashboard': dashboardLayout }"
      class="content content--top-nav"
    >
      <div>
        <div class="intro-y flex flex-col sm:flex-row items-center mt-8">
          <h2 class="text-lg font-medium mr-auto">Stock Summaries</h2>
        </div>
        <entitiesTable
          :controller="'Products/Details'"
          :columns="columns"
          :title="'Admins'"
          :customParams="params"
          :overrideForm="'editAdminsForm'"
          :isDeletable="false"
          :isEditable="false"
        ></entitiesTable>
      </div>
    </div>
    <!-- END: Content -->
  </div>
</template>

<script>
import entitiesTable from '@/components/entity-table/EntityTable.vue'
import { defineComponent, provide, onMounted, ref } from 'vue'
import { useRouter } from 'vue-router'
import MobileMenu from '@/components/mobile-menu/Main.vue'
import DarkModeSwitcher from '@/components/dark-mode-switcher/Main.vue'
import { linkTo } from '@/layouts/side-menu'

export default defineComponent({
  components: {
    MobileMenu,
    DarkModeSwitcher,
    entitiesTable
  },
  setup() {
    const dashboardLayout = ref(false)
    const router = useRouter()
    const params = ref([
      {
        roles: 1,
        isActive: 1,
        'sort[0][field]': 'product',
        'sort[0][dir]': 'asc',
        'sort[1][field]': 'variant',
        'sort[1][dir]': 'asc',
        'sort[2][field]': 'color',
        'sort[2][dir]': 'asc',
        'sort[3][field]': 'size',
        'sort[3][dir]': 'asc'
      }
    ])
    const columns = ref([
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
        title: 'PRODUCT',
        minWidth: 200,
        responsive: 0,
        field: 'product',
        print: false,
        download: false
      },
      {
        title: 'VARIANT',
        minWidth: 200,
        responsive: 0,
        field: 'variant',
        print: false,
        download: false
      },
      {
        title: 'COLOR',
        minWidth: 200,
        responsive: 0,
        field: 'color',
        print: false,
        download: false
      },
      {
        title: 'SIZE',
        minWidth: 200,
        responsive: 0,
        field: 'size',
        print: false,
        download: false
      },
      {
        title: 'STOCK',
        minWidth: 200,
        responsive: 0,
        field: 'stock',
        print: false,
        download: false
      },
      // For print format
      {
        title: 'PRODUCT',
        field: 'product',
        visible: false,
        print: true,
        download: true
      },
      {
        title: 'VARIANT',
        field: 'variant',
        visible: false,
        print: true,
        download: true
      },
      {
        title: 'COLOR',
        field: 'color',
        visible: false,
        print: true,
        download: true
      },
      {
        title: 'SIZE',
        field: 'size',
        visible: false,
        print: true,
        download: true
      },
      {
        title: 'STOCK',
        field: 'stock',
        visible: false,
        print: true,
        download: true
      }
    ])

    provide('setDashboardLayout', (newVal) => {
      dashboardLayout.value = newVal
    })

    onMounted(() => {
      cash('body')
        .removeClass('error-page')
        .removeClass('login')
        .addClass('main')
    })

    return {
      dashboardLayout,
      router,
      linkTo,
      columns,
      params
    }
  }
})
</script>
