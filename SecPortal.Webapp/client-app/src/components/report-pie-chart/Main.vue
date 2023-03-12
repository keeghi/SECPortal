<template>
  <Chart
    type="pie"
    :width="width"
    :height="height"
    :data="data"
    :options="options"
  />
</template>

<script>
import { defineComponent, computed } from 'vue'
import { useStore } from '@/store'

export default defineComponent({
  props: {
    width: {
      type: Number,
      default: 0
    },
    height: {
      type: Number,
      default: 0
    }
  },
  setup() {
    const store = useStore()
    const darkMode = computed(() => store.state.main.darkMode)
    const data = computed(() => {
      return {
        labels: ['Yellow', 'Dark'],
        datasets: [
          {
            data: [15, 10, 65],
            backgroundColor: ['#1BA8E6', '#DD4A4A', '#2F5AD8'],
            hoverBackgroundColor: ['#1BA8E6', '#DD4A4A', '#2F5AD8'],
            borderWidth: 5,
            borderColor: darkMode.value ? '#303953' : '#fff'
          }
        ]
      }
    })
    const options = computed(() => {
      return {
        legend: {
          display: false
        }
      }
    })

    return {
      data,
      options
    }
  }
})
</script>
