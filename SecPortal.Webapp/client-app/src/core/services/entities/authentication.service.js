import store from '@/store/index.js'
import { LOGIN, LOGOUT } from '@/store/auth.module'

function Login({ username, password }) {
  return store.dispatch(LOGIN, { username, password })
}

function Logout() {
  store.dispatch(LOGOUT)
}

function Validate() {
  return window.axios.get('api/oauth/validate')
}

export { Login, Logout, Validate }
