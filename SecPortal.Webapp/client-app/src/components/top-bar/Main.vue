<template>
  <!-- BEGIN: Top Bar -->
  <div class="top-bar -mx-4 px-4 md:mx-0 md:px-0">
    <!-- BEGIN: Breadcrumb -->
    <div
      class="-intro-x breadcrumb mr-auto hidden sm:flex"
      id="breadcrumb-wrapper"
    ></div>
    <!-- END: Breadcrumb -->
    <!-- BEGIN: Search -->
    <div class="intro-x relative mr-3 sm:mr-6">
      <!-- <div class="search hidden sm:block">
        <input
          type="text"
          class="
            search__input
            form-control
            border-transparent
            placeholder-theme-13
          "
          placeholder="Search..."
          @focus="showSearchDropdown"
          @blur="hideSearchDropdown"
        />
        <SearchIcon class="search__icon dark:text-gray-300" />
      </div>
      <a class="notification sm:hidden" href="">
        <SearchIcon class="notification__icon dark:text-gray-300" />
      </a>
      <div class="search-result" :class="{ show: searchDropdown }">
        <div class="search-result__content">
          <div class="search-result__content__title">Pages</div>
          <div class="mb-5">
            <a href="" class="flex items-center">
              <div
                class="
                  w-8
                  h-8
                  bg-theme-17
                  text-theme-20
                  flex
                  items-center
                  justify-center
                  rounded-full
                "
              >
                <InboxIcon class="w-4 h-4" />
              </div>
              <div class="ml-3">Mail Settings</div>
            </a>
            <a href="" class="flex items-center mt-2">
              <div
                class="
                  w-8
                  h-8
                  bg-theme-18
                  text-theme-21
                  flex
                  items-center
                  justify-center
                  rounded-full
                "
              >
                <UsersIcon class="w-4 h-4" />
              </div>
              <div class="ml-3">Users & Permissions</div>
            </a>
            <a href="" class="flex items-center mt-2">
              <div
                class="
                  w-8
                  h-8
                  bg-theme-19
                  text-theme-22
                  flex
                  items-center
                  justify-center
                  rounded-full
                "
              >
                <CreditCardIcon class="w-4 h-4" />
              </div>
              <div class="ml-3">Transactions Report</div>
            </a>
          </div>
          <div class="search-result__content__title">Users</div>
          <div class="mb-5">
            <a
              v-for="(faker, fakerKey) in $_.take($f(), 4)"
              :key="fakerKey"
              href
              class="flex items-center mt-2"
            >
              <div class="w-8 h-8 image-fit">
                <img
                  alt="Tinker Tailwind HTML Admin Template"
                  class="rounded-full"
                  :src="require(`@/assets/images/${faker.photos[0]}`)"
                />
              </div>
              <div class="ml-3">{{ faker.users[0].name }}</div>
              <div
                class="ml-auto w-48 truncate text-gray-600 text-xs text-right"
              >
                {{ faker.users[0].email }}
              </div>
            </a>
          </div>
          <div class="search-result__content__title">Products</div>
          <a
            v-for="(faker, fakerKey) in $_.take($f(), 4)"
            :key="fakerKey"
            href
            class="flex items-center mt-2"
          >
            <div class="w-8 h-8 image-fit">
              <img
                alt="Tinker Tailwind HTML Admin Template"
                class="rounded-full"
                :src="require(`@/assets/images/${faker.images[0]}`)"
              />
            </div>
            <div class="ml-3">{{ faker.products[0].name }}</div>
            <div class="ml-auto w-48 truncate text-gray-600 text-xs text-right">
              {{ faker.products[0].category }}
            </div>
          </a>
        </div>
      </div> -->
    </div>
    <!-- END: Search -->
    <!-- BEGIN: Notifications -->
    <!-- <div class="intro-x dropdown mr-auto sm:mr-6">
      <div
        class="dropdown-toggle notification notification--bullet cursor-pointer"
        role="button"
        aria-expanded="false"
      >
        <BellIcon class="notification__icon dark:text-gray-300" />
      </div>
      <div class="notification-content pt-2 dropdown-menu">
        <div
          class="
            notification-content__box
            dropdown-menu__content
            box
            dark:bg-dark-6
          "
        >
          <div class="notification-content__title">Notifications</div>
          <div
            v-for="(faker, fakerKey) in $_.take($f(), 5)"
            :key="fakerKey"
            class="cursor-pointer relative flex items-center"
            :class="{ 'mt-5': fakerKey }"
          >
            <div class="w-12 h-12 flex-none image-fit mr-1">
              <img
                alt="Tinker Tailwind HTML Admin Template"
                class="rounded-full"
                :src="require(`@/assets/images/${faker.photos[0]}`)"
              />
              <div
                class="
                  w-3
                  h-3
                  bg-theme-20
                  absolute
                  right-0
                  bottom-0
                  rounded-full
                  border-2 border-white
                "
              ></div>
            </div>
            <div class="ml-2 overflow-hidden">
              <div class="flex items-center">
                <a href="javascript:;" class="font-medium truncate mr-5">{{
                  faker.users[0].name
                }}</a>
                <div class="text-xs text-gray-500 ml-auto whitespace-nowrap">
                  {{ faker.times[0] }}
                </div>
              </div>
              <div class="w-full truncate text-gray-600 mt-0.5">
                {{ faker.news[0].shortContent }}
              </div>
            </div>
          </div>
        </div>
      </div>
    </div> -->
    <!-- END: Notifications -->
    <!-- BEGIN: Account Menu -->
    <div class="intro-x dropdown w-8 h-8">
      <div
        class="
          dropdown-toggle
          w-8
          h-8
          rounded-full
          overflow-hidden
          shadow-lg
          image-fit
          zoom-in
        "
        style="background: white"
        role="button"
        aria-expanded="false"
      >
        <SettingsIcon class="pt-2 pl-2 notification__icon dark:text-gray-300" />
      </div>
      <div class="dropdown-menu w-56">
        <div class="dropdown-menu__content box dark:bg-dark-6">
          <div
            class="
              p-4
              border-b border-black border-opacity-5
              dark:border-dark-3
            "
          >
            <div class="font-medium">{{ currentUser.firstName }}</div>
            <div class="text-xs text-gray-600 mt-0.5 dark:text-gray-600">
              {{currentUser.role}}
            </div>
          </div>
          <!-- <div class="p-2">
            <a
              href=""
              class="
                flex
                items-center
                block
                p-2
                transition
                duration-300
                ease-in-out
                hover:bg-gray-200
                dark:hover:bg-dark-3
                rounded-md
              "
            >
              <UserIcon class="w-4 h-4 mr-2" /> Profile
            </a>
            <a
              href=""
              class="
                flex
                items-center
                block
                p-2
                transition
                duration-300
                ease-in-out
                hover:bg-gray-200
                dark:hover:bg-dark-3
                rounded-md
              "
            >
              <EditIcon class="w-4 h-4 mr-2" /> Add Account
            </a>
            <a
              href=""
              class="
                flex
                items-center
                block
                p-2
                transition
                duration-300
                ease-in-out
                hover:bg-gray-200
                dark:hover:bg-dark-3
                rounded-md
              "
            >
              <LockIcon class="w-4 h-4 mr-2" /> Reset Password
            </a>
            <a
              href=""
              class="
                flex
                items-center
                block
                p-2
                transition
                duration-300
                ease-in-out
                hover:bg-gray-200
                dark:hover:bg-dark-3
                rounded-md
              "
            >
              <HelpCircleIcon class="w-4 h-4 mr-2" /> Help
            </a>
          </div> -->
          <div
            class="
              p-2
              border-t border-black border-opacity-5
              dark:border-dark-3
            "
          >
            <a
              @click="adminLogout"
              href="javascript:;"
              class="
                flex
                items-center
                block
                p-2
                transition
                duration-300
                ease-in-out
                hover:bg-gray-200
                dark:hover:bg-dark-3
                rounded-md
              "
            >
              <ToggleRightIcon class="w-4 h-4 mr-2" /> Logout
            </a>
          </div>
        </div>
      </div>
    </div>
    <!-- END: Account Menu -->
  </div>
  <!-- END: Top Bar -->
</template>

<script>
import { Logout } from '@/core/services/entities/authentication.service'
import { defineComponent, ref, computed } from 'vue'
import { useRouter } from 'vue-router'
import { useStore } from 'vuex'

export default defineComponent({
  setup() {
    const store = useStore()
    const router = useRouter()
    const searchDropdown = ref(false)
    const currentUser = computed(() => store.state.auth.user)

    const showSearchDropdown = () => {
      searchDropdown.value = true
    }

    const hideSearchDropdown = () => {
      searchDropdown.value = false
    }

    const adminLogout = () => {
      Logout()
      router.go({ name: 'adminLogin' })
    }

    return {
      searchDropdown,
      showSearchDropdown,
      hideSearchDropdown,
      adminLogout,
      currentUser
    }
  }
})
</script>
