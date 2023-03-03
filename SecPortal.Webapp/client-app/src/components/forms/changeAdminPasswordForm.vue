<template>
  <div class="mt-5">
    <vue-form-generator
      :schema="changePasswordSchema"
      :model="formModel"
      :options="formOptions"
      ref="vfg2"
    ></vue-form-generator>
  </div>
  <teleport to="#slideover-footer" v-if="isReady">
    <button class="btn btn-primary mt-5" @click="ChangePassword">
      Change Password
    </button>
  </teleport>
</template>

<script>
import formMixin from './slideout-forms.mixin'
import {
  Get,
  PutAdmin,
  RegisterAdmin,
  ChangePasswordAdmin
} from '@/core/services/entities/users.service'
import { VFGValidatePassword } from '@/core/services/AuthHelper'
import { useToast } from 'vue-toastification'
export default {
  inject: ['emitter'],
  mixins: [formMixin],
  data() {
    const _this = this
    return {
      changePasswordSchema: {
        groups: [
          {
            styleClasses: 'grid grid-cols-12 gap-4 gap-y-2 mt-5',
            fields: [
              {
                type: 'input',
                inputType: 'password',
                label: 'Password',
                model: 'password',
                min: 6,
                required: true,
                styleClasses: 'intro-y col-span-12 sm:col-span-12',
                validator: VFGValidatePassword
              },
              {
                type: 'input',
                inputType: 'password',
                label: 'Confirm Password',
                model: 'confirmPassword',
                min: 6,
                required: true,
                styleClasses: 'intro-y col-span-12 sm:col-span-12',
                validator: _this.ValidatePassword
              }
            ]
          }
        ]
      }
    }
  },
  mounted() {
    this.setBaseForm(
      {
        email: '',
        firstName: '',
        lastName: '',
        password: '',
        confirmPassword: ''
      },
      Get,
      PutAdmin,
      RegisterAdmin
    )
    this.reload()
  },
  methods: {
    async adminSubmit() {
      const error = await this.$refs.vfg1.validate()
      if (error.length > 0) {
        return
      }

      this.submit()
    },
    ValidatePassword(value) {
      return value === this.formModel.password ? [] : ["Password don't match"]
    },
    isShowPassword() {
      if (this.activeId) {
        return false
      } else {
        return true
      }
    },
    async ChangePassword() {
      const error = await this.$refs.vfg2.validate()
      if (error.length > 0) {
        return
      }

      const toast = useToast()
      const response = await ChangePasswordAdmin(this.formModel)
      if (response.status === 200) {
        if (response.data.isSuccess) {
          toast.success('Password changed success!')
          this.ChangeShownState({ isShown: false, title: '' })
          this.emitter.emit('refresh_table')
        } else {
          toast.error(response.data.message, {
            timeout: 5000
          })
        }
      } else {
        toast.error(this.$t('MESSAGES.CHECK_CONNECTION'))
      }
    }
  }
}
</script>

<style>
</style>
