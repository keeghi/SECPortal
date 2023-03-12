<template>
  <loading :active="isLoading" :can-cancel="false" :is-full-page="true" />
  <Header></Header>
  <router-view v-slot="{ Component }">
    <transition name="fade" mode="out-in">
      <component :is="Component" />
    </transition>
  </router-view>
  <Footer></Footer>
</template>

<script>
import Loading from 'vue-loading-overlay'
import { mapGetters, mapActions } from 'vuex'
import 'vue-loading-overlay/dist/vue-loading.css'
import Header from '@/views/.customer/components/Header'
import Footer from '@/views/.customer/components/Footer'
import '../assets/sass.customer/app.scss'
import '../../node_modules/bootstrap/dist/js/bootstrap'
import '../../node_modules/@fortawesome/fontawesome-free/js/all.min'
import {
  Logout,
  Validate
} from '@/core/services/entities/authentication.service'

export default {
  components: {
    Loading,
    Header,
    Footer
  },
  computed: {
    ...mapGetters('app', ['isLoading']),
    ...mapGetters(['currentUser', 'isAuthenticated'])
  },
  methods: {
    ...mapActions(['logout'])
  },
  async mounted() {
    if (this.isAuthenticated && this.currentUser.role === 'Customer') {
      try {
        await Validate()
      } catch (ex) {
        // validate returns 401 which is unauthorized
        Logout()
        this.$router.push({ name: 'homepage' })
      }
    } else if (this.isAuthenticated) {
      Logout()
      this.$router.push({ name: 'homepage' })
    }
  }
}
</script>
