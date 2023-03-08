const controllerName = 'Organizations'

function GetAll(params) {
    return window.axios.get(`api/${controllerName}`, { params })
}

export { GetAll }