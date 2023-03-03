<template>
  <metainfo>
    <template v-slot:title="{ content }">{{
      content ? `${content} | Hikarusa` : `Hikarusa`
    }}</template>
  </metainfo>
  <router-view />
</template>

<script>
</script>

<style>
</style>
