<template>
  <ContainerComponent>
    <div class="flex flex-1 min-h-0">
      <!-- Упрощенный Sidebar -->
      <div class="w-56 bg-emerald-50 flex flex-col p-4 space-y-6">
        <!-- Логотип с Кермитом -->
        <div class="flex items-center space-x-4">
          <img 
            src="../assets/KermitTheFrogAdvisor.png" 
            alt="Kermit Advisor"
            class="w-40 h-40 object-contain"
          >
        </div>

        <!-- Выбор счета -->
        <div>
          <label class="block text-xl text-center font-medium text-gray-500 mb-1">{{ t('dashboard.current_account') }}</label>
          <select 
            v-model="selectedAccount"
            class="w-full px-3 py-2 text-m border border-gray-300 rounded-lg focus:ring-emerald-500 focus:border-emerald-500"
          >
            <option v-for="account in accounts" :key="account.id" :value="account.id">
              {{ account.name }}
            </option>
            <option value="new">+ {{ t('dashboard.add_account_option') }}</option>
          </select>
        </div>

        <!-- Переключение между расходами/доходами -->
        <div class="flex border border-emerald-200 rounded-lg overflow-hidden">
          <button
            @click="currentTab = 'expenses'"
            :class="[
              'flex-1 py-2 text-m font-medium',
              currentTab === 'expenses' ? 'bg-emerald-500 text-white' : 'text-emerald-700 hover:bg-emerald-100'
            ]"
          >
            {{ t('dashboard.expenses_option') }}
          </button>
          <button
            @click="currentTab = 'income'"
            :class="[
              'flex-1 py-2 text-m font-medium',
              currentTab === 'income' ? 'bg-emerald-500 text-white' : 'text-emerald-700 hover:bg-emerald-100'
            ]"
          >
            {{ t('dashboard.income_option') }}
          </button>
        </div>

        <!-- Фильтр по периоду -->
        <div>
          <label class="block text-m font-medium text-gray-500 mb-1">{{ t('dashboard.period_label') }}</label>
          <select 
            v-model="selectedPeriod"
            class="w-full px-3 py-2 text-m border border-gray-300 rounded-lg focus:ring-emerald-500 focus:border-emerald-500"
          >
            <option v-for="period in periods" :key="period.value" :value="period.value">
              {{ period.label }}
            </option>
          </select>
        </div>

        <!-- Основные действия -->
        <div class="space-y-3">
          <button
            @click="showAddTransaction = true"
            class="w-full py-2 px-4 bg-emerald-600 text-white text-m font-medium rounded-lg hover:bg-emerald-700 transition-colors"
          >
            + {{ t('dashboard.add_transaction_button') }}
          </button>
          
          <button
            @click="showAccountsPanel = true"
            class="w-full py-2 px-4 text-m font-medium text-emerald-700 hover:bg-emerald-100 rounded-lg transition-colors"
          >
            {{ t('dashboard.manage_accounts_button') }}
          </button>
          
          <button
            @click="showCategoriesPanel = true"
            class="w-full py-2 px-4 text-m font-medium text-emerald-700 hover:bg-emerald-100 rounded-lg transition-colors"
          >
            {{ t('dashboard.manage_categories_button') }}
          </button>
          
          <RouterLink
            to="/report"
            class="block w-full py-2 px-4 text-m font-medium text-emerald-700 hover:bg-emerald-100 rounded-lg transition-colors text-center"
          >
            {{ t('dashboard.view_report_button') }}
          </RouterLink>
        </div>
      </div>

      <!-- Основной контент -->
      <div class="flex-1 flex flex-col overflow-hidden">
        <!-- Заголовок с балансом -->
        <div class="border-b border-gray-200 p-4 flex justify-between items-center">
          <h2 class="text-2xl font-bold text-gray-800">
            {{ currentTab === 'expenses' ? t('dashboard.expenses_option') : t('dashboard.income_option') }}
            <span class="text-m font-normal text-gray-500 ml-2">{{ currentPeriodLabel }}</span>
          </h2>
          <div class="text-lg font-semibold" :class="balanceClass">
            {{ formattedBalance }}
          </div>
        </div>

        <!-- Список транзакций -->
        <div class="flex-1 overflow-y-auto p-4">
          <TransactionList 
            :transactions="filteredTransactions" 
            :type="currentTab"
          />
        </div>
      </div>

      <!-- Модальные окна -->
      <AddTransactionModal 
        v-if="showAddTransaction"
        @close="showAddTransaction = false"
      />
      
      <AccountsPanel 
        v-if="showAccountsPanel"
        @close="showAccountsPanel = false"
      />
      
      <CategoriesPanel 
        v-if="showCategoriesPanel"
        @close="showCategoriesPanel = false"
      />
    </div>
  </ContainerComponent>
</template>

<script setup>
import { ref, computed } from 'vue'
import { RouterLink } from 'vue-router'
import ContainerComponent from '../components/ContainerComponent.vue'
import { useI18n } from "vue-i18n"
// import TransactionList from './TransactionList.vue'
// import AddTransactionModal from './AddTransactionModal.vue'
// import AccountsPanel from './AccountsPanel.vue'
// import CategoriesPanel from './CategoriesPanel.vue'

const { t, locale } = useI18n()

// Состояния
const currentTab = ref('expenses')
const selectedAccount = ref('1')
const selectedPeriod = ref('week')
const showAddTransaction = ref(false)
const showAccountsPanel = ref(false)
const showCategoriesPanel = ref(false)

// Данные
const accounts = ref([
  { id: '1', name: 'Main Account', balance: 12500.50 },
  { id: '2', name: 'Savings', balance: 34000.00 },
  { id: '3', name: 'Investment', balance: 8750.75 }
])

const periods = ref([
  { value: 'today', label: t('dashboard.day_period') },
  { value: 'week', label: t('dashboard.week_period') },
  { value: 'month', label: t('dashboard.month_period') },
  { value: 'year', label: t('dashboard.year_period') },
  { value: 'custom', label: t('dashboard.custom_period') }
])

const transactions = ref([
  // Пример данных транзакций
  { id: 1, type: 'expense', amount: 50.00, category: 'Food', date: '2023-06-15', account: '1' },
  { id: 2, type: 'income', amount: 2000.00, category: 'Salary', date: '2023-06-10', account: '1' }
])

// Вычисляемые свойства
const currentPeriodLabel = computed(() => {
  return periods.value.find(p => p.value === selectedPeriod.value)?.label || ''
})

const formattedBalance = computed(() => {
  const account = accounts.value.find(a => a.id === selectedAccount.value)
  return account ? new Intl.NumberFormat('en-US', { 
    style: 'currency', 
    currency: 'USD' 
  }).format(account.balance) : '$0.00'
})

const balanceClass = computed(() => {
  return currentTab.value === 'expenses' ? 'text-red-600' : 'text-green-600'
})

const filteredTransactions = computed(() => {
  return transactions.value.filter(t => 
    t.type === currentTab.value && 
    t.account === selectedAccount.value
    // + фильтрация по датам в зависимости от selectedPeriod
  )
})
</script>

<style scoped>
/* Минималистичные стили */
</style>