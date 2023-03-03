<template>
  <div
    class="
      row-start-2
      md:row-start-auto
      col-span-12
      md:col-span-4
      py-6
      border-theme-11 border-t
      md:border-t-0 md:border-l md:border-r
      border-dashed
      px-10
      sm:px-28
      md:px-5
      -mx-5
    "
  >
    <div class="flex flex-wrap items-center">
      <div
        class="
          flex
          items-center
          w-full
          sm:w-auto
          justify-center
          sm:justify-start
          mr-auto
          mb-5
          2xl:mb-5
        "
      >
        <div
          class="
            text-base
            2xl:text-lg
            justify-center
            sm:justify-start
            flex
            items-center
            text-theme-34
            dark:text-gray-500
            leading-3
          "
        >
          Most Purchased Products
          <Tippy
            tag="div"
            content="Pie chart that shows you the most popular item"
          >
            <AlertCircleIcon class="w-5 h-5 ml-1.5 mt-0.5" />
          </Tippy>
        </div>
      </div>
    </div>
    <div
      class="
        boxed-tabs
        nav nav-tabs
        justify-center
        w-3/4
        2xl:w-4/6
        bg-theme-27
        text-white
        dark:bg-dark-2 dark:text-gray-500
        rounded-md
        p-1
        mx-auto
      "
      role="tablist"
    >
      <a
        @click="type = 0"
        id="active-users-tab"
        data-toggle="tab"
        data-target="#active-users"
        href="javascript:;"
        class="btn flex-1 border-0 shadow-none py-1.5 px-2 active"
        role="tab"
        aria-controls="active-users"
        aria-selected="true"
        >Monthly</a
      >
      <a
        @click="type = 1"
        id="inactive-users-tab"
        data-toggle="tab"
        data-target="#inactive-users"
        href="javascript:;"
        class="btn flex-1 border-0 shadow-none py-1.5 px-2"
        role="tab"
        aria-selected="false"
        >Weekly</a
      >
    </div>
    <Chart
      v-if="data"
      class="mt-10"
      type="doughnut"
      :width="0"
      :height="190"
      :data="data"
      :options="options"
    />
  </div>
</template>

<script>
import { MostPurchasedProducts } from '@/core/services/entities/dashboard.service'
export default {
  data() {
    return {
      options: {
        legend: {
          display: false
        }
      },
      data: null,
      type: 0
    }
  },
  watch: {
    type() {
      this.GetFavoriteProducts()
    }
  },
  methods: {
    async GetFavoriteProducts() {
      const response = await MostPurchasedProducts(this.type)
      if (response.status === 200 && response.data.isSuccess) {
        this.data = response.data.data
      }
    }
  },
  mounted() {
    this.GetFavoriteProducts()
  }
}
</script>

<style>
</style>
