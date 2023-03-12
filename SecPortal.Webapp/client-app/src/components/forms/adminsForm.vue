<template>
  <div v-if="!activeId" class="bpcity-form">
    <vue-form-generator
      :schema="createAdminSchema"
      :model="formModel"
      :options="formOptions"
      ref="vfg1"
    ></vue-form-generator>
  </div>
  <teleport to="#slideover-footer" v-if="isReady">
    <button class="btn btn-primary mt-5" @click="vfgSubmit">Save</button>
  </teleport>
</template>

<script>
import formMixin from './slideout-forms.mixin'
import {
  Get,
  PutAdmin,
  RegisterAdmin
} from '@/core/services/entities/users.service'
import { VFGValidatePassword } from '@/core/services/AuthHelper'
export default {
  inject: ['emitter'],
  mixins: [formMixin],
  data() {
    const _this = this
    return {
      createAdminSchema: {
        groups: [
          {
            styleClasses: 'grid grid-cols-12 gap-4 gap-y-2 mt-5',
            fields: [
              {
                type: 'input',
                inputType: 'email',
                label: 'Email',
                model: 'email',
                styleClasses: 'intro-y col-span-12 sm:col-span-12',
                required: true,
                validator: ['email']
              },
              {
                type: 'input',
                inputType: 'text',
                label: 'First Name',
                model: 'firstName',
                styleClasses: 'intro-y col-span-6 sm:col-span-6',
                required: true,
                validator: ['required']
              },
              {
                type: 'input',
                inputType: 'text',
                label: 'Last Name',
                model: 'lastName',
                styleClasses: 'intro-y col-span-6 sm:col-span-6'
              },
              {
                type: 'input',
                inputType: 'password',
                label: 'Password',
                model: 'password',
                min: 6,
                required: true,
                visible: _this.isShowPassword,
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
                visible: _this.isShowPassword,
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
    ValidatePassword(value) {
      return value === this.formModel.password ? [] : ["Password don't match"]
    },
    isShowPassword() {
      if (this.activeId) {
        return false
      } else {
        return true
      }
    }
  }
}
</script>

<style>
</style>
