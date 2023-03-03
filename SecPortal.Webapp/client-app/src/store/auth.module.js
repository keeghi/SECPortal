import ApiService from '@/core/services/api.service'
import JwtService from '@/core/services/jwt.service'

// action types
export const VERIFY_AUTH = 'verifyAuth'
export const LOGIN = 'login'
export const LOGOUT = 'logout'
export const REGISTER = 'register'
export const UPDATE_PASSWORD = 'updateUser'
export const ACTIVATE_ACCOUNT = 'activateAccount'
export const FORGOT_PASSWORD = 'forgotPassword'
export const CHECK_AUTH = 'checkAuth'

// mutation types
export const PURGE_AUTH = 'logOut'
export const SET_AUTH = 'setUser'
export const SET_PASSWORD = 'setPassword'
export const SET_ERROR = 'setError'

const state = {
  errors: null,
  user: {},
  projects: [],
  // isAuthenticated: !!JwtService.getToken()
  isAuthenticated: false
}

const getters = {
  currentUser(state) {
    return state.user
  },
  currentProjects(state) {
    return state.projects
  },
  isAuthenticated(state) {
    return state.isAuthenticated
  }
}

const actions = {
  [CHECK_AUTH]({ commit }) {
    return new Promise(resolve => {
      window.axios
        .get('/api/oauth/validate')
        .then(response => {
          resolve(response)
        })
        .catch(ex => {
          commit(SET_ERROR, ex)
        })
    })
  },
  [LOGIN]({ commit }, credentials) {
    return new Promise(resolve => {
      window.axios
        .post('/api/oauth/login', credentials)
        .then(response => {
          if (response.data.isSuccess) {
            commit(SET_AUTH, response.data)
            resolve(response.data)
          } else {
            commit(SET_ERROR, response.data.message)
            resolve(response.data)
          }
        })
        .catch(ex => {
          commit(SET_ERROR, ex)
        })
    })
  },
  [ACTIVATE_ACCOUNT](context, credentials) {
    return new Promise(resolve => {
      ApiService.post('api/oauth/activate-account', credentials)
        .then(({ data }) => {
          if (data.isSuccess) {
            context.commit(SET_AUTH, data)
            resolve(data)
          } else {
            context.commit(SET_ERROR, data.message)
            resolve(null)
          }
        })
        .catch(({ response }) => {
          context.commit(SET_ERROR, response.data.errors)
        })
    })
  },
  [LOGOUT](context) {
    context.commit(PURGE_AUTH)
  },
  [REGISTER](context, credentials) {
    return new Promise(resolve => {
      ApiService.post('api/users', credentials)
        .then(({ data }) => {
          context.commit(SET_AUTH, data)
          resolve(data)
        })
        .catch(({ response }) => {
          context.commit(SET_ERROR, response.data.errors)
        })
    })
  },
  [FORGOT_PASSWORD](context, credentials) {
    return new Promise(resolve => {
      ApiService.post('api/oauth/forgot-password', credentials)
        .then(({ data }) => {
          context.commit(SET_AUTH, data)
          resolve(data)
        })
        .catch(({ response }) => {
          context.commit(SET_ERROR, response.data.errors)
        })
    })
  },
  [VERIFY_AUTH]({ commit }) {
    if (JwtService.getToken()) {
      commit(SET_AUTH, JwtService.getUser())
    } else {
      commit(PURGE_AUTH)
    }
  },
  [PURGE_AUTH]({ commit }) {
    commit(PURGE_AUTH)
  },
  [UPDATE_PASSWORD](context, payload) {
    const password = payload

    return ApiService.put('password', password).then(({ data }) => {
      context.commit(SET_PASSWORD, data)
      return data
    })
  }
}

const mutations = {
  [SET_ERROR](state, error) {
    state.errors = error
  },
  [SET_AUTH](state, user) {
    state.isAuthenticated = true
    state.user = user
    state.errors = {}
    state.projects = ['Rumah', 'lna1', 'Tangguh']

    window.axios.interceptors.request.use(function(config) {
      config.headers.Authorization = `Bearer ${state.user.accessToken}`
      return config
    })

    JwtService.saveUser(state.user)
  },
  [SET_PASSWORD](state, password) {
    state.user.password = password
  },
  [PURGE_AUTH](state) {
    state.isAuthenticated = false
    state.user = {}
    state.errors = {}
    JwtService.destroyToken()
  }
}

export default {
  state,
  actions,
  mutations,
  getters
}
