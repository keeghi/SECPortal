import { Types as getBpType } from '@/core/services/entities/businessPartner.service'
import { KeyValue as getCitiesKeyValue } from '@/core/services/entities/city.service'

export const SET_BP_TYPE = 'SetbpTypes'
export const SET_MASTER_COLOR = 'SetMasterColor'
export const SET_MASTER_SIZE = 'SetMasterSize'
export const SET_MASTER_VARIANT = 'SetMasterVariant'
export const SET_MASTER_CATEGORIES = 'SetMasterCategories'
export const SET_MASTER_CITIES = 'SetMasterCities'

const state = {
  bpTypes: [],
  colors: [],
  sizes: [],
  variants: [],
  categories: [],
  cities: []
}

const mutations = {
  [SET_BP_TYPE](state, types) {
    state.bpTypes = types
  },
  [SET_MASTER_COLOR](state, data) {
    state.colors = data
  },
  [SET_MASTER_SIZE](state, data) {
    state.sizes = data
  },
  [SET_MASTER_VARIANT](state, data) {
    state.variants = data
  },
  [SET_MASTER_CATEGORIES](state, data) {
    state.categories = data
  },
  [SET_MASTER_CITIES](state, data) {
    state.cities = data
  }
}

const actions = {
  async [SET_BP_TYPE]({ commit }) {
    const response = await getBpType()
    if (response.status === 200) {
      commit(SET_BP_TYPE, response.data.data)
    }
  },
  async [SET_MASTER_COLOR]({ commit }) {
    const response = await getColorKeyValue()
    if (response.status === 200) {
      commit(SET_MASTER_COLOR, response.data.data)
    }
  },
  async [SET_MASTER_SIZE]({ commit }, payload) {
    let params
    if (payload) {
      params = {
        'isActive.Operator': 4,
        'isActive.Value': true,
        'categoryId.Operator': 4,
        'categoryId.Value': payload
      }
    }

    const response = await getSizeKeyValue(params)
    if (response.status === 200) {
      commit(SET_MASTER_SIZE, response.data.data)
    }
  },
  async [SET_MASTER_VARIANT]({ commit }) {
    const response = await getVariantKeyValue()
    if (response.status === 200) {
      commit(SET_MASTER_VARIANT, response.data.data)
    }
  },
  async [SET_MASTER_CATEGORIES]({ commit }) {
    const response = await getCategoriesKeyValue()
    if (response.status === 200) {
      commit(SET_MASTER_CATEGORIES, response.data.data)
    }
  },
  async [SET_MASTER_CITIES]({ commit }) {
    const response = await getCitiesKeyValue()
    if (response.status === 200) {
      commit(SET_MASTER_CITIES, response.data.data)
    }
  }
}

const getters = {
  bpTypes(state) {
    return state.bpTypes
  },
  colors(state) {
    return state.colors
  },
  sizes(state) {
    return state.sizes
  },
  variants(state) {
    return state.variants
  },
  categories(state) {
    return state.categories
  },
  cities(state) {
    return state.cities
  }
}

export default {
  namespaced: true,
  state,
  getters,
  mutations,
  actions
}
