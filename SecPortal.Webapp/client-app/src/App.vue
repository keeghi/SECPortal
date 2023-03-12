<template>
  <metainfo>
    <template v-slot:title="{ content }">{{
      content ? `${content} | SEC` : `SEC`
    }}</template>
  </metainfo>
  <router-view />
</template>

<script>
</script>

<style>
</style>
