module.exports = {
  root: true,
  env: {
    node: true
  },
  extends: ['plugin:vue/vue3-essential', '@vue/standard'],
  parserOptions: {
    parser: 'babel-eslint'
  },
  rules: {
    eqeqeq: 'off',
    indent: 'off',
    'no-new': 'off',
    'no-unused-expressions': 'off',
    'no-unused-vars': 'off',
    'no-undef': 'off',
    'space-before-function-paren': 'off',
    'no-console': process.env.NODE_ENV === 'production' ? 'warn' : 'off',
    'no-debugger': process.env.NODE_ENV === 'production' ? 'warn' : 'off',
    'vue/no-mutating-props': 'off'
  },
  globals: {
    cash: true
  }
}
