const USER_KEY = 'user_key'

export const getToken = () => {
  const user = JSON.parse(window.localStorage.getItem(USER_KEY))
  const check = !!user
  return check ? user.accessToken : null
}

export const getUser = () => {
  return JSON.parse(window.localStorage.getItem(USER_KEY))
}

export const saveUser = user => {
  window.localStorage.setItem(USER_KEY, JSON.stringify(user))
}

export const destroyToken = () => {
  window.localStorage.removeItem(USER_KEY)
}

export default { getToken, getUser, saveUser, destroyToken }
