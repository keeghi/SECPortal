<template>
  <div class="col-span-12 xl:col-span-4 mt-6">
    <div class="intro-y flex items-center h-10">
      <h2 class="text-lg font-medium truncate mr-5">
        New Customer Registration
      </h2>
    </div>
    <div class="mt-5">
      <div v-for="item in customers" :key="item.id" class="intro-y">
        <div
          @click="OpenData(item.id)"
          class="box px-4 py-4 mb-3 flex items-center zoom-in"
        >
          <div class="ml-4 mr-auto">
            <div class="font-medium">
              {{ item.fullname }}
            </div>
            <div class="text-gray-600 text-xs mt-0.5">
              {{ item.email }}
            </div>
          </div>
          <div
            class="
              py-1
              px-2
              rounded-full
              text-xs
              bg-theme-6
              dark:bg-theme-20
              text-white
              cursor-pointer
              font-medium
            "
          >
            {{ item.createdAtString }}
          </div>
        </div>
      </div>
      <router-link
        :to="{ name: 'adminCustomers' }"
        class="
          intro-y
          w-full
          block
          text-center
          rounded-md
          py-4
          border border-dotted border-theme-32
          dark:border-dark-5
          text-theme-33
          dark:text-gray-600
        "
        >View More</router-link
      >
    </div>
  </div>
</template>

<script>
import { NewCustomer } from '@/core/services/entities/dashboard.service'
import { mapActions } from 'vuex'
export default {
  data() {
    return {
      customers: []
    }
  },
  methods: {
    ...mapActions('slideover', ['ChangeShownState']),
    async DoGetCustomers() {
      const response = await NewCustomer()
      if (response.status === 200 && response.data.isSuccess) {
        this.customers = response.data.data
      }
    },
    OpenData(id) {
      this.ChangeShownState({
        isShown: true,
        title: 'Edit Customer',
        component: 'customersForm',
        id: id
      })
    }
  },
  mounted() {
    this.DoGetCustomers()
  }
}
</script>

<style>
</style>
