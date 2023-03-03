<template>
  <Chart
    type="line"
    :width="width"
    :height="height"
    :data="data"
    :options="options"
  />
</template>

<script>
import { defineComponent, computed } from 'vue'

export default defineComponent({
  props: {
    width: {
      type: Number,
      default: 0
    },
    height: {
      type: Number,
      default: 0
    },
    data: {
      type: Object,
      required: true
    }
  },
  setup() {
    const options = computed(() => {
      return {
        legend: {
          labels: {
            fontColor: '#FFFFFF'
          }
        },
        scales: {
          xAxes: [
            {
              ticks: {
                fontSize: '12',
                fontColor: '#FFFFFF'
              },
              gridLines: {
                display: false
              }
            }
          ],
          yAxes: [
            {
              ticks: {
                fontSize: '12',
                fontColor: '#FFFFFF',
                callback: function (value) {
                  return 'Rp' + value
                }
              },
              gridLines: {
                color: '#FFFFFF',
                zeroLineColor: '#FFFFFF',
                borderDash: [2, 2],
                zeroLineBorderDash: [2, 2],
                drawBorder: false
              }
            }
          ]
        }
      }
    })

    return {
      options
    }
  }
})
</script>
