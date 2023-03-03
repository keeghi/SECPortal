const state = () => {
  return {
    menu: [
      {
        icon: 'HomeIcon',
        pageName: 'adminDashboard',
        title: 'Dashboard'
      },
      'devider',
      {
        icon: 'UserIcon',
        pageName: 'adminAdmins',
        title: 'Users'
      },
      {
        icon: 'InboxIcon',
        title: 'Master',
        subMenu: [
          {
            icon: '',
            pageName: 'adminAddresses',
            title: 'Addresses'
          }
        ]
      }
    ]
  }
}

// getters
const getters = {
  menu: state => state.menu
}

// actions
const actions = {}

// mutations
const mutations = {}

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations
}

// {
//   icon: 'HomeIcon',
//   pageName: 'side-menu-dashboard',
//   title: 'Dashboard',
//   subMenu: [
//     {
//       icon: '',
//       pageName: 'side-menu-dashboard-overview-1',
//       title: 'Overview 1'
//     },
//     {
//       icon: '',
//       pageName: 'side-menu-dashboard-overview-2',
//       title: 'Overview 2'
//     },
//     {
//       icon: '',
//       pageName: 'side-menu-dashboard-overview-3',
//       title: 'Overview 3'
//     }
//   ]
// },
