<template>
  <div>
    <DarkModeSwitcher />
    <div class="container sm:px-10">
      <div class="block xl:grid grid-cols-2 gap-4">
        <!-- BEGIN: Login Info -->
        <div class="hidden xl:flex flex-col min-h-screen">
          <a href="" class="-intro-x flex items-center pt-5">
            <img
              alt="Icewall Tailwind HTML Admin Template"
              class="w-6"
              src="@/assets/images/logo.svg"
            />
            <span class="text-white text-lg ml-3">
              Ru<span class="font-medium">bick</span>
            </span>
          </a>
          <div class="my-auto">
            <img
              alt="Icewall Tailwind HTML Admin Template"
              class="-intro-x w-1/2 -mt-16"
              src="@/assets/images/illustration.svg"
            />
            <div
              class="
                -intro-x
                text-white
                font-medium
                text-4xl
                leading-tight
                mt-10
              "
            >
              A few more clicks to <br />
              sign in to your account.
            </div>
            <div
              class="
                -intro-x
                mt-5
                text-lg text-white text-opacity-70
                dark:text-gray-500
              "
            >
              Manage all your e-commerce accounts in one place
            </div>
          </div>
        </div>
        <!-- END: Login Info -->
        <!-- BEGIN: Login Form -->
        <div class="h-screen xl:h-auto flex py-5 xl:py-0 my-10 xl:my-0">
          <div
            class="
              my-auto
              mx-auto
              xl:ml-20
              bg-white
              dark:bg-dark-1
              xl:bg-transparent
              px-5
              sm:px-8
              py-8
              xl:p-0
              rounded-md
              shadow-md
              xl:shadow-none
              w-full
              sm:w-3/4
              lg:w-2/4
              xl:w-auto
            "
          >
            <h2
              class="
                intro-x
                font-bold
                text-2xl
                xl:text-3xl
                text-center
                xl:text-left
              "
            >
              {{ this.$t('AUTHENTICATION.LOGIN') }}
            </h2>
            <div class="intro-x mt-2 text-gray-500 xl:hidden text-center">
              A few more clicks to sign in to your account. Manage all your
              e-commerce accounts in one place
            </div>
            <div class="intro-x mt-8">
              <input
                v-model="authentication.username"
                type="text"
                class="
                  intro-x
                  login__input
                  form-control
                  py-3
                  px-4
                  border-gray-300
                  block
                "
                placeholder="Username"
              />
              <input
                v-model="authentication.password"
                @keydown.enter="submitForm"
                type="password"
                :class="[
                  'intro-x',
                  'login__input',
                  'form-control',
                  'py-3',
                  'px-4',
                  'border-gray-300',
                  'block',
                  'mt-4',
                  passwordValidation ? '' : 'border-theme-21'
                ]"
                placeholder="Password"
              />
              <div class="text-theme-21 mt-2" v-if="!passwordValidation">
                {{ passwordValidationMessage }}
              </div>
            </div>
            <div
              class="
                intro-x
                flex
                text-gray-700
                dark:text-gray-600
                text-xs
                sm:text-sm
                mt-4
              "
            >
              <div class="flex items-center mr-auto">
                <input
                  id="remember-me"
                  type="checkbox"
                  class="form-check-input border mr-2"
                />
                <label class="cursor-pointer select-none" for="remember-me"
                  >Remember me</label
                >
              </div>
            </div>
            <div class="intro-x mt-5 xl:mt-8 text-center xl:text-left">
              <LoadingButton
                :title="'Login'"
                :btnClass="['btn-primary']"
                :isReady="isReady"
                :onClick="submitForm"
              ></LoadingButton>
              <button
                v-if="signUpEnabled"
                class="
                  btn btn-outline-secondary
                  py-3
                  px-4
                  w-full
                  xl:w-32
                  mt-3
                  xl:mt-0
                  align-top
                "
              >
                Sign up
              </button>
            </div>
            <div
              v-if="signUpEnabled"
              class="
                intro-x
                mt-10
                xl:mt-24
                text-gray-700
                dark:text-gray-600
                text-center
                xl:text-left
              "
            >
              By signin up, you agree to our <br />
              <a class="text-theme-30 dark:text-theme-10" href=""
                >Terms and Conditions</a
              >
              &
              <a class="text-theme-30 dark:text-theme-10" href=""
                >Privacy Policy</a
              >
            </div>
          </div>
        </div>
        <!-- END: Login Form -->
      </div>
    </div>
  </div>
</template>

<script>
import { defineComponent } from 'vue'
import DarkModeSwitcher from '@/components/dark-mode-switcher/Main.vue'
import LoadingButton from '@/components/buttons/LoadingButton.vue'

import { ValidatePassword } from '@/core/services/AuthHelper.js'
import { Login } from '@/core/services/entities/authentication.service'
import { mapGetters } from 'vuex'
import { useToast } from 'vue-toastification'

export default defineComponent({
  data() {
    return {
      isReady: true,
      signUpEnabled: false,
      authentication: {
        username: '',
        password: ''
      },
      passwordValidationMessage: ''
    }
  },
  components: {
    DarkModeSwitcher,
    LoadingButton
  },
  computed: {
    ...mapGetters('app', ['projectName']),
    ...mapGetters(['isAuthenticated']),
    passwordValidation() {
      const result = ValidatePassword(this.authentication.password)
      this.changeMessage(result.message)
      return result.isValid
    }
  },
  mounted() {
    cash('body')
      .removeClass('main')
      .removeClass('error-page')
      .addClass('login')
    if (this.isAuthenticated) {
      this.$router.push({ name: 'adminDashboard' })
    }
  },
  methods: {
    changeMessage(msg) {
      this.passwordValidationMessage = msg
    },
    async submitForm() {
      this.isReady = false

      setTimeout(() => {
        this.$router.push({ name: 'adminDashboard' })
        this.isReady = true
      }, 1000)
      // const toast = useToast()
      // const response = await Login(this.authentication)
      // if (response.isSuccess) {
      //   this.$router.push({ name: 'adminDashboard' })
      // } else {
      //   toast.error(response.message, {
      //     timeout: 2000
      //   })
      // }
    }
  }
})
</script>

<style lang="scss" src="@/assets/sass/app.scss"></style>
