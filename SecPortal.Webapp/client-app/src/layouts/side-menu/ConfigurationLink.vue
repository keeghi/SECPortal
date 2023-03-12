<template>
  <li @click="openSlideoutPanel" style="cursor: pointer">
    <SideMenuTooltip tag="a" content="Configuration" class="side-menu">
      <div class="side-menu__icon">
        <component :is="'SlidersIcon'" />
      </div>
      <div class="side-menu__title">Configuration</div>
    </SideMenuTooltip>
  </li>
</template>

<script>
import SideMenuTooltip from '@/components/side-menu-tooltip/Main.vue'
import { mapActions } from 'vuex'
export default {
  components: {
    SideMenuTooltip: SideMenuTooltip
  },
  methods: {
    ...mapActions('slideover', ['ChangeShownState']),
    openSlideoutPanel() {
      this.ChangeShownState({
        isShown: true,
        title: 'Edit Configuration',
        component: 'configurationsForm'
      })
    }
  }
}
</script>

<style>
</style>
