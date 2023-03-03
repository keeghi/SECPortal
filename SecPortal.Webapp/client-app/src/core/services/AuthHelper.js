const PASSWORD_SETTING = {
  minLength: 6,
  RequireCharacter: true,
  RequireUppercase: false,
  RequireNumeric: true,
  RequireSpecial: false
}

function VFGValidatePassword(password) {
  const validation = []
  if (password.length == 0) {
    // the user hasn't inputted their password;
    validation.push('Password is required')
    return validation
  }

  if (password.length < PASSWORD_SETTING.minLength) {
    validation.push(
      `Password length must be at least ${PASSWORD_SETTING.minLength} character`
    )
  }

  if (PASSWORD_SETTING.RequireCharacter && !/[a-zA-Z]/.test(password)) {
    validation.push('Your password include at least 1 letter')
  }

  if (PASSWORD_SETTING.RequireUppercase && !/[A-Z]/.test(password)) {
    validation.push('Your password include at least 1 uppercase letter')
  }

  if (PASSWORD_SETTING.RequireNumeric && !/[1-9]/.test(password)) {
    validation.push('Your password include at least 1 number')
  }

  if (PASSWORD_SETTING.RequireSpecial && !/[!@#$%^&*]/.test(password)) {
    validation.push('Your password include at least 1 special character')
  }

  return validation
}

function ValidatePassword(password) {
  if (password.length == 0) {
    // the user hasn't inputted their password;
    return {
      isValid: null,
      message: ''
    }
  }

  if (password.length < PASSWORD_SETTING.minLength) {
    return {
      isValid: false,
      message: `Password length must be at least ${PASSWORD_SETTING.minLength} character`
    }
  }

  if (PASSWORD_SETTING.RequireCharacter && !/[a-zA-Z]/.test(password)) {
    return {
      isValid: false,
      message: 'Your password include at least 1 letter'
    }
  }

  if (PASSWORD_SETTING.RequireUppercase && !/[A-Z]/.test(password)) {
    return {
      isValid: false,
      message: 'Your password include at least 1 uppercase letter'
    }
  }

  if (PASSWORD_SETTING.RequireNumeric && !/[1-9]/.test(password)) {
    return {
      isValid: false,
      message: 'Your password include at least 1 number.'
    }
  }

  if (PASSWORD_SETTING.RequireSpecial && !/[!@#$%^&*]/.test(password)) {
    return {
      isValid: false,
      message: 'Your password include at least 1 special character.'
    }
  }

  return {
    isValid: true,
    message: 'Looks good, keep this password to yourselves.'
  }
}

export { ValidatePassword, VFGValidatePassword }
