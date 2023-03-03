const controllerName = 'sizes'

function Get(id) {
  return window.axios.get(`api/${controllerName}/${id}`)
}

function Put(id, formModel) {
  return window.axios.put(`api/${controllerName}/${id}`, formModel)
}

function Post(formModel) {
  return window.axios.post(`api/${controllerName}`, formModel)
}

function KeyValue(params) {
  if (!params) {
    params = {
      'isActive.Operator': 4,
      'isActive.Value': true
    }
  }

  return window.axios.get(`api/${controllerName}/key-value`, { params })
}

export { Get, Put, Post, KeyValue }
