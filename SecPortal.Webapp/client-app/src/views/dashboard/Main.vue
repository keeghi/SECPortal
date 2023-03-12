<template>
  <div>
    <teleport to="#breadcrumb-wrapper" v-if="isReady">
      <a href="" class="breadcrumb--active">Dashboard</a>
    </teleport>
    <div class="relative"></div>
  </div>
</template>

<script>
import { mapGetters } from 'vuex'
import { inject } from 'vue'
export default {
  computed: {
    ...mapGetters(['currentUser'])
  },
  data() {
    return {
      isReady: false,
      revenueByMonth: null,
      yearlyRevenue: 0,
      totalRevenue: 0
    }
  },
  methods: {
  },
  mounted() {
    const setDashboardLayout = inject('setDashboardLayout')
    setDashboardLayout(true)
    this.isReady = true
  }
}
</script>
