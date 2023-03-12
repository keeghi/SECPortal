import { BASE_URL } from '@/core/constant/env'

export default {
  data() {
    return {
      baseUrl: BASE_URL,
      productDefaultImage: require('@/assets/images/pillow-product.png')
    }
  },
  methods: {
    getDisplayedImage(product) {
      if (product.productImages.length > 0) {
        return `${BASE_URL}/attachments/${product.productImages[0].systemName}`
      }

      return this.productDefaultImage
    },
    calculateDiscount(product) {
      return product.discount > 0
        ? product.price - (product.price * product.discount) / 100
        : 0
    }
  }
}
