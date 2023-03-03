export const INITIALIZE_SYSTEM = 'InitializeSystem'
export const SET_COUNTRIES = 'SetCountries'
export const SET_ACTIVITY_CODES = 'SetActivityCodes'
export const SET_CURRENCIES = 'SetCurrencies'

const state = {
  countries: [],
  activityCodes: [],
  currencies: []
}

const getters = {
  countries: state => state.countries,
  activityCodes: state => state.activityCodes,
  currencies: state => state.currencies
}

const mutations = {
  [SET_COUNTRIES](state, payload) {
    state.countries = payload
  },
  [SET_ACTIVITY_CODES](state, payload) {
    state.activityCodes = payload
  },
  [SET_CURRENCIES](state, payload) {
    state.currencies = payload
  }
}

const actions = {
  [INITIALIZE_SYSTEM]({ commit }, payload) {
    commit(INITIALIZE_SYSTEM, payload)
  }
}

export default {
  namespaced: true,
  state,
  actions,
  mutations,
  getters
}
