import enHomepage from './customer/homepage/en.js'
import enContactUs from './customer/contactus/en'
import enProductItem from './customer/commons/card/productItem/en.js'
import enFAQ from './customer/faq/en'
import enCareManual from './customer/careManual/en'
import enFooter from './customer/footer/en'
import enHeader from './customer/header/en'
import enProductDetails from './customer/productdetails/en'
import enAuthentications from './customer/authentications/en'
import enForms from './customer/commons/form/en'
import enLogin from './customer/login/en'
import enCommon from './customer/commons/en'
import enCart from './customer/cart/en'
import enCheckout from './customer/checkout/en'
import enAccountDetail from './customer/accountDetail/en'
import enForgotPassword from './customer/forgotPassword/en'
import enResetPassword from './customer/resetPassword/en'
import enActivateAccount from './customer/activateAccount/en'
import enRegisterReseller from './customer/registerReseller/en'
import enReturnProducts from './customer/returnProducts/en'
import enSizeRecommendation from './customer/sizeRecommendation/en'

export const locale = {
  ...enCareManual,
  ...enHomepage,
  ...enProductItem,
  ...enContactUs,
  ...enFAQ,
  ...enFooter,
  ...enHeader,
  ...enProductDetails,
  ...enAuthentications,
  ...enForms,
  ...enLogin,
  ...enCommon,
  ...enCart,
  ...enCheckout,
  ...enAccountDetail,
  ...enForgotPassword,
  ...enResetPassword,
  ...enActivateAccount,
  ...enRegisterReseller,
  ...enReturnProducts,
  ...enSizeRecommendation,
  COMMON: {
    BTNTEXT: {
      LEARNMORE: 'Learn more',
      ADDTOCART: 'Add to cart',
      OUTOFSTOCK: 'Out of stock',
      WISHLIST: 'Wishlist',
      CLICKHERE: 'Click Here',
      CHATWA: 'Chat via whatsapp'
    }
  },
  TRANSLATOR: {
    SELECT: 'Select your language'
  },
  MENU: {
    NEW: 'new',
    ACTIONS: 'Actions',
    CREATE_POST: 'Create New Post',
    PAGES: 'Pages',
    FEATURES: 'Features',
    APPS: 'Apps',
    DASHBOARD: 'Dashboard'
  },
  AUTH: {
    GENERAL: {
      OR: 'Or',
      SUBMIT_BUTTON: 'Submit',
      NO_ACCOUNT: "Don't have an account?",
      SIGNUP_BUTTON: 'Sign Up',
      FORGOT_BUTTON: 'Forgot Password',
      BACK_BUTTON: 'Back',
      PRIVACY: 'Privacy',
      LEGAL: 'Legal',
      CONTACT: 'Contact'
    },
    LOGIN: {
      TITLE: 'Login Account',
      BUTTON: 'Sign In'
    },
    FORGOT: {
      TITLE: 'Forgot Password?',
      DESC: 'Enter your email to reset your password',
      SUCCESS: 'Your account has been successfully reset.'
    },
    REGISTER: {
      TITLE: 'Sign Up',
      DESC: 'Enter your details to create your account',
      SUCCESS: 'Your account has been successfuly registered.'
    },
    INPUT: {
      EMAIL: 'Email',
      FULLNAME: 'Fullname',
      PASSWORD: 'Password',
      CONFIRM_PASSWORD: 'Confirm Password',
      USERNAME: 'Username'
    },
    VALIDATION: {
      INVALID: '{{name}} is not valid',
      REQUIRED: '{{name}} is required',
      MIN_LENGTH: '{{name}} minimum length is {{min}}',
      AGREEMENT_REQUIRED: 'Accepting terms & conditions are required',
      NOT_FOUND: 'The requested {{name}} is not found',
      INVALID_LOGIN: 'The login detail is incorrect',
      REQUIRED_FIELD: 'Required field',
      MIN_LENGTH_FIELD: 'Minimum field length:',
      MAX_LENGTH_FIELD: 'Maximum field length:',
      INVALID_FIELD: 'Field is not valid'
    }
  },
  ECOMMERCE: {
    COMMON: {
      SELECTED_RECORDS_COUNT: 'Selected records count: ',
      ALL: 'All',
      SUSPENDED: 'Suspended',
      ACTIVE: 'Active',
      FILTER: 'Filter',
      BY_STATUS: 'by Status',
      BY_TYPE: 'by Type',
      BUSINESS: 'Business',
      INDIVIDUAL: 'Individual',
      SEARCH: 'Search',
      IN_ALL_FIELDS: 'in all fields'
    },
    ECOMMERCE: 'eCommerce',
    CUSTOMERS: {
      CUSTOMERS: 'Customers',
      CUSTOMERS_LIST: 'Customers list',
      NEW_CUSTOMER: 'New Customer',
      DELETE_CUSTOMER_SIMPLE: {
        TITLE: 'Customer Delete',
        DESCRIPTION: 'Are you sure to permanently delete this customer?',
        WAIT_DESCRIPTION: 'Customer is deleting...',
        MESSAGE: 'Customer has been deleted'
      },
      DELETE_CUSTOMER_MULTY: {
        TITLE: 'Customers Delete',
        DESCRIPTION: 'Are you sure to permanently delete selected customers?',
        WAIT_DESCRIPTION: 'Customers are deleting...',
        MESSAGE: 'Selected customers have been deleted'
      },
      UPDATE_STATUS: {
        TITLE: 'Status has been updated for selected customers',
        MESSAGE: 'Selected customers status have successfully been updated'
      },
      EDIT: {
        UPDATE_MESSAGE: 'Customer has been updated',
        ADD_MESSAGE: 'Customer has been created'
      }
    }
  }
}
