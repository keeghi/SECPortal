import { createI18n } from 'vue-i18n'

// Localisation language list
import { locale as en } from '@/core/config/i18n/en'

let messages = {}
messages = { ...messages, en }

// get current selected language
const lang = localStorage.getItem('language') || 'en'

// Create VueI18n instance with options
const i18n = createI18n({
  warnHtmlInMessage: 'off',
  locale: lang, // set locale
  messages // set locale messages
})

export default i18n
