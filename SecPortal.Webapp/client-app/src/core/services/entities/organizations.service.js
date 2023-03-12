const controllerName = 'Organizations'

function GetAll(params) {
  return window.axios.get(`api/${controllerName}`, { params })
}

function GetData(id) {
  return axios.get(`api/${controllerName}/${id}`).then(response => {
    return response.data.data
  })
}

function CreateData(data) {
  return axios
    .post('api/Organizations', data)
    .then(response => {
      if (response.data != null) {
        if (response.data.id != null) {
          alert('Save Successfully')
        } else {
          alert(response.data.message)
        }
      } else {
        alert('Oops - Error System')
      }
    })
    .catch(ex => {
      console.log(ex)
    })
}

function UpdateData(data) {
  return axios
    .put('api/Organizations/', data)
    .then(response => {
      if (response.data.isSuccess) {
        alert('Save Successfully')
        return true
      } else {
        alert(response.data.message)
        return false
      }
    })
    .catch(ex => {
      console.log(ex)
    })
}

export { GetAll, GetData, CreateData, UpdateData }
