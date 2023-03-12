<template>
  <div class="bpcity-form">
    <vue-form-generator
      :schema="putAdminSchema"
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
export default {
  inject: ['emitter'],
  mixins: [formMixin],
  data() {
    return {
      putAdminSchema: {
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
                disabled: true,
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
  }
}
</script>

<style>
</style>
