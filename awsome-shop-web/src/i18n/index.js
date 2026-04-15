import { createI18n } from 'vue-i18n'

const messages = {
  'zh-CN': {
    common: {
      appName: 'AWSome Shop',
      loading: '加载中...',
      confirm: '确认',
      cancel: '取消',
      save: '保存',
      delete: '删除',
      edit: '编辑',
      add: '添加',
      search: '搜索',
      back: '返回',
      home: '首页',
      logout: '退出登录'
    },
    employee: {
      title: '员工福利商城',
      welcome: '欢迎来到员工福利商城',
      points: '我的积分',
      products: '商品列表',
      cart: '购物车',
      orders: '我的订单',
      profile: '个人中心',
      checkout: '结算',
      orderSuccess: '订单提交成功',
      name: '姓名',
      email: '邮箱',
      role: '角色',
      status: '状态'
    },
    admin: {
      title: '管理后台',
      dashboard: '仪表盘',
      products: '商品管理',
      orders: '订单管理',
      categories: '分类管理',
      employees: '员工管理',
      statistics: '数据统计'
    }
  },
  'en-US': {
    common: {
      appName: 'AWSome Shop',
      loading: 'Loading...',
      confirm: 'Confirm',
      cancel: 'Cancel',
      save: 'Save',
      delete: 'Delete',
      edit: 'Edit',
      search: 'Search',
      back: 'Back',
      home: 'Home',
      logout: 'Logout'
    },
    employee: {
      title: 'Employee Benefits Shop',
      welcome: 'Welcome to Employee Benefits Shop',
      points: 'My Points',
      products: 'Products',
      cart: 'Shopping Cart',
      orders: 'My Orders',
      profile: 'Profile',
      checkout: 'Checkout',
      orderSuccess: 'Order submitted successfully',
      name: 'Name',
      email: 'Email',
      role: 'Role',
      status: 'Status'
    },
    admin: {
      title: 'Admin Dashboard',
      dashboard: 'Dashboard',
      products: 'Product Management',
      orders: 'Order Management',
      categories: 'Category Management',
      employees: 'Employee Management',
      statistics: 'Statistics'
    }
  }
}

const i18n = createI18n({
  legacy: false,
  locale: 'zh-CN',
  fallbackLocale: 'en-US',
  messages
})

export default i18n