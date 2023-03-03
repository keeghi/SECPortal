import { createStore } from 'vuex'
import main from './main'
import sideMenu from './side-menu'
import simpleMenu from './simple-menu'
import topMenu from './top-menu'
import app from './app.module'
import auth from './auth.module'
import slideover from './slideover.module'
import systems from './systems.module'

import VuexPersistence from 'vuex-persist'
const vuexLocal = new VuexPersistence({
  storage: window.localStorage,
  reducer: state => ({ cart: state.cart })
})

const store = createStore({
  modules: {
    main,
    sideMenu,
    simpleMenu,
    topMenu,
    app,
    auth,
    slideover,
    systems
  },
  plugins: [vuexLocal.plugin]
})

export function useStore() {
  return store
}

export default store
