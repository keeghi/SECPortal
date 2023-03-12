import { createApp } from 'vue'
import App from './App.vue'
import router from './router/index'
import store from './store'
import globalComponents from './global-components'
import utils from './utils'
import './libs'
import i18n from '@/core/plugins/vue-i18n'
import axios from 'axios'
import VueAxios from 'vue-axios'
import Toast from 'vue-toastification'
import { createMetaManager } from 'vue-meta'
import { library } from '@fortawesome/fontawesome-svg-core'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'
import Icons from '@/libs/font-awesome'
import VueFormGenerator from 'vfg-next'
import VFGFields from '@/libs/vfg'
import { VERIFY_AUTH } from '@/store/auth.module'
import { BASE_URL } from '@/core/constant/env'
import mitt from 'mitt'
import VueNumerals from 'vue-numerals'

import 'vfg-next/dist/vfg-next.css'
import 'vue-toastification/dist/index.css'
library.add(Icons)

window.axios = require('axios')
window.moment = require('moment')
router.beforeEach((to, from) => {
  Promise.all([store.dispatch(VERIFY_AUTH)]).then(() => {
    return true
  })

  // reset config to initial state
  window.axios.defaults.baseURL = BASE_URL

  // Scroll page to top on every route change
  setTimeout(() => {
    window.scrollTo(0, 0)
  }, 100)
})
require('moment/locale/es')

const emitter = mitt()
const app = createApp(App)
  .use(i18n)
  .use(store)
  .use(router)
  .use(VueAxios, axios)
  .use(Toast)
  .use(VueNumerals)
  .use(VueFormGenerator, {
    components: { ...VFGFields }
  })
  .use(createMetaManager())
  .component('font-awesome-icon', FontAwesomeIcon)
  .provide('emitter', emitter)

globalComponents(app)
utils(app)

app.mount('#app')
