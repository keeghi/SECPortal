const controllerName = 'users'

function Get(id) {
  return window.axios.get(`api/${controllerName}/${id}`)
}

function RegisterAdmin(formModel) {
  return window.axios.post('api/users', formModel)
}

function PutAdmin(id, formModel) {
  return window.axios.put(`api/users/${id}`, formModel)
}

function PutCustomer(id, formModel) {
  return window.axios.put(`api/users/customer/${id}`, formModel)
}

function PutCustomerOwnAccount(id, formModel) {
  return window.axios.put(`api/users/customer/${id}`, formModel)
}

function PutCustomerOwnPassword(id, formModel) {
  return window.axios.put(`api/users/customer/${id}/password`, formModel)
}

function ChangePasswordAdmin(formModel) {
  return window.axios.post('api/users/change-password', formModel)
}

function RegisterCustomer(formModel) {
  return window.axios.post('api/users/customer', formModel)
}

function DeleteUser(id) {
  return window.axios.delete(`api/users/${id}`)
}

function CustomerForgotPassword(formModel) {
  return window.axios.post('api/users/forgot-password', formModel)
}

function ResetPassword(formModel) {
  return window.axios.post('api/users/reset-password', formModel)
}

function ActivateAccount(formModel) {
  return window.axios.post('api/users/activate-account', formModel)
}

export {
  Get,
  RegisterAdmin,
  PutAdmin,
  PutCustomer,
  PutCustomerOwnAccount,
  PutCustomerOwnPassword,
  RegisterCustomer,
  ChangePasswordAdmin,
  DeleteUser,
  CustomerForgotPassword,
  ResetPassword,
  ActivateAccount
}
