export const CHANGE_LANGUAGE = 'ChangeAppLanguage'
export const CHANGE_LOADING_STATE = 'ChangeLoadingState'

const state = {
  projectName: 'ECorps',
  appLanguage: 'id_ID',
  availableLanguage: [
    { code: 'id_ID', flag: 'id', name: 'Indonesian' },
    { code: 'en_US', flag: 'us', name: 'United States' }
  ],
  isLoading: false
}

const getters = {
  projectName: state => state.projectName,
  appLanguage: state => state.appLanguage,
  availableLanguage: state => state.availableLanguage,
  isLoading: state => state.isLoading
}

const mutations = {
  [CHANGE_LANGUAGE](state, payload) {
    state.appLanguage = payload
  },
  [CHANGE_LOADING_STATE](state, payload) {
    state.isLoading = payload
  }
}

const actions = {
  [CHANGE_LANGUAGE]({ commit }, payload) {
    commit(CHANGE_LANGUAGE, payload)
  },
  [CHANGE_LOADING_STATE]({ commit }, payload) {
    commit(CHANGE_LOADING_STATE, payload)
  }
}

export default {
  namespaced: true,
  state,
  actions,
  mutations,
  getters
}
