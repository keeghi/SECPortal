function OperatorEnum(operator) {
  switch (operator) {
    case '>':
      return 0
    case '<':
      return 1
    case '>=':
      return 2
    case '<=':
      return 3
    case '=':
      return 4
    case '!=':
      return 5
    case 'BETWEEN':
      return 6
    case 'LIKE':
      return 7
    case 'STARTS WITH':
      return 8
    case 'ENDS WITH':
      return 9
    case 'NOT NULL':
      return 10
    default:
      throw new Error('Operator unrecognized')
  }
}

function IsValidProp(item) {
  return !Array.isArray(item) || (Array.isArray(item) && item.length > 0)
}

export { OperatorEnum, IsValidProp }
