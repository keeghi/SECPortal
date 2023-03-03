const controllerName = 'systems'

function GetCountries() {
  return window.axios.get(`api/${controllerName}/countries`)
}

function GetActivityCodes() {
  return window.axios.get(`api/${controllerName}/activity-codes`)
}

function GetCurrencies() {
  return window.axios.get(`api/${controllerName}/currencies`)
}

export { GetCountries, GetActivityCodes, GetCurrencies }
