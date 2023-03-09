<template>
  <div>
    <Loading :active="isLoading" :can-cancel="false" :is-full-page="true" />
    <DarkModeSwitcher />
    <MobileMenu :dashboard-layout="dashboardLayout" layout="side-menu" />
    <div class="flex overflow-hidden">
      <!-- BEGIN: Side Menu -->
      <nav class="side-nav">
        <a href="" class="intro-x flex items-center pl-5 pt-4 mt-3">
          <img
            alt="Admin Template"
            class="w-6"
            :src="require(`@/assets/images/logo.svg`)"
          />
          <span class="hidden xl:block text-white text-lg ml-3">
            Tink<span class="font-medium">er</span>
          </span>
        </a>
        <div class="side-nav__devider my-6"></div>
        <ul>
          <!-- BEGIN: First Child -->
          <template v-for="(menu, menuKey) in formattedMenu">
            <li
              v-if="menu == 'devider'"
              :key="menu + menuKey"
              class="side-nav__devider my-6"
            ></li>
            <li v-else :key="menu + menuKey">
              <SideMenuTooltip
                tag="a"
                :content="menu.title"
                :href="
                  menu.subMenu
                    ? 'javascript:;'
                    : router.resolve({ name: menu.pageName }).path
                "
                class="side-menu"
                :class="{
                  'side-menu--active': menu.active,
                  'side-menu--open': menu.activeDropdown
                }"
                @click="linkTo(menu, router, $event)"
              >
                <div class="side-menu__icon">
                  <component :is="menu.icon" />
                </div>
                <div class="side-menu__title">
                  {{ menu.title }}
                  <div
                    v-if="menu.subMenu"
                    class="side-menu__sub-icon"
                    :class="{ 'transform rotate-180': menu.activeDropdown }"
                  >
                    <ChevronDownIcon />
                  </div>
                </div>
              </SideMenuTooltip>
              <!-- BEGIN: Second Child -->
              <transition @enter="enter" @leave="leave">
                <ul v-if="menu.subMenu && menu.activeDropdown">
                  <li
                    v-for="(subMenu, subMenuKey) in menu.subMenu"
                    :key="subMenuKey"
                  >
                    <SideMenuTooltip
                      tag="a"
                      :content="subMenu.title"
                      :href="
                        subMenu.subMenu
                          ? 'javascript:;'
                          : router.resolve({ name: subMenu.pageName }).path
                      "
                      class="side-menu"
                      :class="{ 'side-menu--active': subMenu.active }"
                      @click="linkTo(subMenu, router, $event)"
                    >
                      <div class="side-menu__icon">
                        <ActivityIcon />
                      </div>
                      <div class="side-menu__title">
                        {{ subMenu.title }}
                        <div
                          v-if="subMenu.subMenu"
                          class="side-menu__sub-icon"
                          :class="{
                            'transform rotate-180': subMenu.activeDropdown
                          }"
                        >
                          <ChevronDownIcon />
                        </div>
                      </div>
                    </SideMenuTooltip>
                    <!-- BEGIN: Third Child -->
                    <transition @enter="enter" @leave="leave">
                      <ul v-if="subMenu.subMenu && subMenu.activeDropdown">
                        <li
                          v-for="(lastSubMenu,
                          lastSubMenuKey) in subMenu.subMenu"
                          :key="lastSubMenuKey"
                        >
                          <SideMenuTooltip
                            tag="a"
                            :content="lastSubMenu.title"
                            :href="
                              lastSubMenu.subMenu
                                ? 'javascript:;'
                                : router.resolve({
                                    name: lastSubMenu.pageName
                                  }).path
                            "
                            class="side-menu"
                            :class="{
                              'side-menu--active': lastSubMenu.active
                            }"
                            @click="linkTo(lastSubMenu, router, $event)"
                          >
                            <div class="side-menu__icon">
                              <ZapIcon />
                            </div>
                            <div class="side-menu__title">
                              {{ lastSubMenu.title }}
                            </div>
                          </SideMenuTooltip>
                        </li>
                      </ul>
                    </transition>
                    <!-- END: Third Child -->
                  </li>
                </ul>
              </transition>
              <!-- END: Second Child -->
            </li>
          </template>
          <!-- END: First Child -->
        </ul>
      </nav>
      <!-- END: Side Menu -->
      <!-- BEGIN: Content -->
      <div :class="{ 'content--dashboard': dashboardLayout }" class="content">
        <TopBar />
        <router-view />
        <CrudSlideover />
      </div>
      <!-- END: Content -->
    </div>
  </div>
</template>

<script>
import {
  defineComponent,
  computed,
  provide,
  onMounted,
  ref,
  watch,
  reactive
} from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { useStore } from '@/store'
import { helper as $h } from '@/utils/helper'
import TopBar from '@/components/top-bar/Main.vue'
import MobileMenu from '@/components/mobile-menu/Main.vue'
import DarkModeSwitcher from '@/components/dark-mode-switcher/Main.vue'
import SideMenuTooltip from '@/components/side-menu-tooltip/Main.vue'
import CrudSlideover from '@/components/crud-slideover/CrudSlideover.vue'
import { linkTo, nestedMenu, enter, leave } from './index'
import {
  Logout,
  Validate
} from '@/core/services/entities/authentication.service'
import Loading from 'vue-loading-overlay'
import 'vue-loading-overlay/dist/vue-loading.css'

export default defineComponent({
  components: {
    TopBar,
    MobileMenu,
    DarkModeSwitcher,
    SideMenuTooltip,
    CrudSlideover,
    Loading
  },
  setup() {
    const slideoverClass = reactive([])
    const dashboardLayout = ref(false)
    const route = useRoute()
    const router = useRouter()
    const store = useStore()
    const formattedMenu = ref([])
    const sideMenu = computed(() =>
      nestedMenu(store.state.sideMenu.menu, route)
    )
    provide('setDashboardLayout', newVal => {
      dashboardLayout.value = newVal
    })

    watch(
      computed(() => route.path),
      () => {
        formattedMenu.value = $h.toRaw(sideMenu.value)
        dashboardLayout.value = false
      }
    )

    onMounted(async () => {
      const isAuthenticated = store.getters.isAuthenticated
      const currentUser = store.getters.currentUser

      if (!isAuthenticated) {
        Logout()
        router.push({ name: 'adminLogin' })
      } else {
        if (currentUser.role === 'Customer') {
          Logout()
        }
        try {
          await Validate()
        } catch (ex) {
          // validate returns 401 which is unauthorized
          Logout()
          router.push({ name: 'adminLogin' })
        }
      }

      cash('body')
        .removeClass('error-page')
        .removeClass('login')
        .addClass('main')
      formattedMenu.value = $h.toRaw(sideMenu.value)
    })

    return {
      isLoading: computed(() => store.getters['app/isLoading']),
      isShown: computed(() => store.getters['slideover/isShown']),
      dashboardLayout,
      formattedMenu,
      router,
      linkTo,
      enter,
      leave,
      slideoverClass
    }
  }
})
</script>

<style lang="scss" src="@/assets/sass/app.scss"></style>
