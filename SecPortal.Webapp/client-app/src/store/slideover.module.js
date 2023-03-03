export const CHANGE_SHOWN_STATE = 'ChangeShownState'
export const SET_ACTIVE_ID = 'SetActiveId'

const state = {
  isShown: false,
  isHidden: true,
  title: '',
  activeId: '',
  activeComponent: ''
}

const getters = {
  isShown(state) {
    return state.isShown
  },
  isHidden(state) {
    return state.isHidden
  },
  title(state) {
    return state.title
  },
  activeId(state) {
    return state.activeId
  },
  activeComponent(state) {
    return state.activeComponent
  }
}

const mutations = {
  [CHANGE_SHOWN_STATE](state, { isShown, title, component, id }) {
    if (title) {
      state.title = title
    }

    if (id) {
      state.activeId = id
    } else {
      state.activeId = null
    }

    if (component) {
      state.activeComponent = component
    } else {
      state.activeComponent = null
    }

    if (isShown) {
      if (state.isShown) {
        // it is stuck on model open state
        state.isShown = false
        state.isHidden = true
      }

      state.isHidden = false
      setTimeout(() => {
        state.isShown = isShown
      }, 100)
    } else {
      state.isShown = isShown
      setTimeout(() => {
        if (!state.isShown) {
          state.isHidden = true
        }
      }, 300)
    }
  },
  [SET_ACTIVE_ID](state, payload) {
    state.activeId = payload
  }
}

const actions = {
  [CHANGE_SHOWN_STATE]({ commit }, payload) {
    commit(CHANGE_SHOWN_STATE, payload)
  },
  [SET_ACTIVE_ID]({ commit }, payload) {
    commit(SET_ACTIVE_ID, payload)
  }
}

export default {
  namespaced: true,
  state,
  actions,
  mutations,
  getters
}
