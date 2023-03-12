import { IsValidProp } from '../helper.service'

const controllerName = 'addresses'

function GetAll(params) {
  return window.axios.get(`api/${controllerName}`, { params })
}

function Get(id) {
  return window.axios.get(`api/${controllerName}/${id}`)
}

function Put(id, formModel) {
  return window.axios.put(`api/${controllerName}/${id}`, formModel)
}

function Post(formModel) {
  const formData = new FormData()

  for (const key in formModel) {
    if (key === 'businessPartnerImage') {
      if (formModel[key][0]) {
        formData.append(key, formModel[key][0].file)
      }
    } else if (IsValidProp(formModel[key])) {
      formData.append(key, formModel[key] == null ? '' : formModel[key])
    }
  }

  return window.axios.post(`api/${controllerName}`, formModel)
}

export { GetAll, Get, Put, Post }
