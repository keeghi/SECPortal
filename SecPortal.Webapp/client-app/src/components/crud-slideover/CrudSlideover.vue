<template>
  <teleport to="body">
    <div
      v-if="!isHidden"
      data-keyboard="false"
      @keydown.esc="closeModal"
      @keyup.esc="closeModal"
      :class="slideoverClass"
      tabindex="-1"
      aria-hidden="false"
      :style="slideoverStyle"
    >
      <div class="slideover-overlay" v-if="isShown"></div>
      <div class="modal-dialog modal-xl slideover-modal">
        <div class="modal-content">
          <div class="modal-header p-5">
            <h2 class="font-medium text-base mr-auto">{{ title }}</h2>
            <a class="cursor-pointer" @click="closeModal">
              <XIcon class="w-4 h-4" />
            </a>
          </div>
          <div class="modal-body">
            <component ref="dynamicComponent" :is="activeComponent"></component>
          </div>
          <div class="modal-footer" id="slideover-footer"></div>
        </div>
      </div>
    </div>
  </teleport>
</template>

<script>
import { mapGetters, mapActions } from 'vuex'
import AdminsForm from '@/components/forms/adminsForm.vue'
import EditAdminsForm from '@/components/forms/editAdminsForm.vue'
import ChangeAdminPasswordForm from '@/components/forms/changeAdminPasswordForm.vue'

export default {
  components: {
    AdminsForm: AdminsForm,
    EditAdminsForm: EditAdminsForm,
    ChangeAdminPasswordForm: ChangeAdminPasswordForm
  },
  watch: {
    isShown(val) {
      if (val) {
        this.$refs.dynamicComponent.reload()
      }
    }
  },
  computed: {
    ...mapGetters('slideover', [
      'isShown',
      'isHidden',
      'title',
      'activeComponent'
    ]),
    slideoverClass() {
      if (!this.isShown && this.isHidden) {
        return ['modal modal-slide-over']
      } else if (!this.isShown && !this.isHidden) {
        return ['modal', 'modal-slide-over', 'opacity-0', 'overflow-y-auto']
      } else {
        return [
          'modal',
          'modal-slide-over',
          'opacity-0',
          'overflow-y-auto',
          'show'
        ]
      }
    },
    slideoverStyle() {
      if (!this.isShown && this.isHidden) {
        return []
      } else if (!this.isShown && !this.isHidden) {
        return [
          'margin-top: 0px;',
          'margin-left: 0px;',
          'padding-left: 0px;',
          'z-index: 9998;'
        ]
      } else {
        return [
          'margin-top: 0px;',
          'margin-left: 0px;',
          'padding-left: 0px;',
          'z-index: 9998;'
        ]
      }
    }
  },
  methods: {
    ...mapActions('slideover', ['ChangeShownState']),
    closeModal(event) {
      if (event) {
        event.preventDefault()
      }
      this.ChangeShownState({ isShown: false, title: '' })
    }
  }
}
</script>

<style lang="scss">
.slideover-overlay {
  height: 100vh;
  width: 100vw;
  height: 100vh;
  position: absolute;
  top: 0;
  left: 0;
  background: rgba(0, 0, 0, 0.2);
}</style
>>
