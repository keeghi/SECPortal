<template>
  <button
    :disabled="!isReady"
    @click.prevent="onClick"
    :class="[
      'btn',
      'py-3',
      'px-4',
      'w-full',
      'xl:w-32',
      'xl:mr-3',
      'align-top',
      btnClass,
      isReady ? '' : 'justify-between'
    ]"
  >
    <LoadingIcon v-if="!isReady" icon="puff" class="w-8 h-8" />
    {{ title }}
  </button>
</template>

<script>
export default {
  props: {
    onClick: {
      type: Function,
      required: true
    },
    btnClass: {
      type: Array,
      default: () => {
        return []
      }
    },
    title: {
      type: String,
      required: true
    },
    isReady: {
      type: Boolean,
      required: true
    }
  }
}
</script>

<style>
</style>
