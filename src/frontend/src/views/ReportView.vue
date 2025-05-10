<template>
    <container-component>
        <div class="report-container">
            <button @click="getReport" class="load-btn">Загрузить отчет</button>

            <div v-if="error" class="error-message">{{ error }}</div>

            <div v-if="report" class="report-content">
                <!-- График динамики за месяц -->
                <div class="chart-box">
                    <h3>Динамика за месяц</h3>
                    <canvas ref="monthlyChart"></canvas>
                </div>

                <!-- Расходы по категориям -->
                <div class="chart-box">
                    <h3>Расходы по категориям</h3>
                    <canvas ref="categoryChart"></canvas>
                </div>

                <!-- Итоговая статистика -->
                <div class="summary-box">
                    <h3>Итоговая статистика</h3>
                    <div class="summary-item">
                        <span>Общий доход:</span>
                        <span class="amount income">{{ formatCurrency(report.totalIncome) }}</span>
                    </div>
                    <div class="summary-item">
                        <span>Общие расходы:</span>
                        <span class="amount expense">{{ formatCurrency(report.totalExpenses) }}</span>
                    </div>
                    <div class="summary-item">
                        <span>Итоговый баланс:</span>
                        <span class="amount balance">{{ formatCurrency(report.totalBalance) }}</span>
                    </div>
                </div>

                <!-- Список транзакций -->
                <div class="transactions-box">
                    <h3>Последние транзакции</h3>
                    <ul>
                        <li v-for="account in report.accountTransfers" :key="account.id">
                            <h4>{{ account.name || 'Без названия' }} (Баланс: {{ formatCurrency(account.balance) }})
                            </h4>
                            <ul>
                                <li v-for="transfer in account.transfers" :key="transfer.id" class="transfer-item">
                                    <span class="transfer-date">{{ formatDate(transfer.moment) }}</span>
                                    <span class="transfer-category">{{ transfer.category.name }}</span>
                                    <span class="transfer-amount" :class="transfer.bill >= 0 ? 'income' : 'expense'">
                                        {{ formatCurrency(transfer.bill) }}
                                    </span>
                                    <span class="transfer-desc">{{ transfer.description || 'Без описания' }}</span>
                                </li>
                            </ul>
                        </li>
                    </ul>
                </div>
            </div>
        </div>

    </container-component>
</template>

<script setup>
    import ContainerComponent from '../components/ContainerComponent.vue'
</script>

<script>
import Chart from 'chart.js/auto';
import api from '../api/api.js';


export default {
    name: 'FinanceReport',
    data() {
        return {
            report: null,
            error: null,
            monthlyChart: null,
            categoryChart: null
        };
    },
    methods: {
        async getReport() {
            try {
                const response = await api.get('/report');
                this.report = response.data;
                this.error = null;

                this.$nextTick(() => {
                    this.renderCharts();
                });

            } catch (err) {
                if (err.status == 401) {
                    this.$router.push("/")
                }
                this.report = null;
                this.error = err.response?.data?.message || 'Ошибка при загрузке отчета';
                console.error(err);
            }
        },

        renderCharts() {
            if (!this.report) return;

            // Уничтожаем старые графики, если они есть
            if (this.monthlyChart) this.monthlyChart.destroy();
            if (this.categoryChart) this.categoryChart.destroy();

            // 1. График динамики за месяц
            const daysInMonth = this.report.balanceDynamics.length;
            const daysLabels = Array.from({ length: daysInMonth }, (_, i) => i + 1);

            this.monthlyChart = new Chart(this.$refs.monthlyChart, {
                type: 'line',
                data: {
                    labels: daysLabels,
                    datasets: [
                        {
                            label: 'Баланс',
                            data: this.report.balanceDynamics,
                            borderColor: '#4e73df',
                            backgroundColor: 'rgba(78, 115, 223, 0.05)',
                            tension: 0.3,
                            fill: true
                        },
                        {
                            label: 'Доходы',
                            data: this.report.incomeDynamics,
                            borderColor: '#1cc88a',
                            backgroundColor: 'rgba(28, 200, 138, 0.05)',
                            tension: 0.3
                        },
                        {
                            label: 'Расходы',
                            data: this.report.expensesDynamics,
                            borderColor: '#e74a3b',
                            backgroundColor: 'rgba(231, 74, 59, 0.05)',
                            tension: 0.3
                        }
                    ]
                },
                options: {
                    responsive: true,
                    maintainAspectRatio: false,
                    plugins: {
                        tooltip: {
                            callbacks: {
                                label: (context) => {
                                    return `${context.dataset.label}: ${this.formatCurrency(context.raw)}`;
                                }
                            }
                        }
                    },
                    scales: {
                        y: {
                            ticks: {
                                callback: (value) => this.formatCurrency(value)
                            }
                        }
                    }
                }
            });

            // 2. Круговой график по категориям
            const categories = Object.keys(this.report.categoryTransfers);
            const categoryData = categories.map(category => {
                return this.report.categoryTransfers[category].reduce((sum, t) => sum + t.bill, 0);
            });

            this.categoryChart = new Chart(this.$refs.categoryChart, {
                type: 'doughnut',
                data: {
                    labels: categories,
                    datasets: [{
                        data: categoryData,
                        backgroundColor: [
                            '#4e73df',
                            '#1cc88a',
                            '#36b9cc',
                            '#f6c23e',
                            '#e74a3b',
                            '#858796'
                        ],
                        hoverBackgroundColor: [
                            '#2e59d9',
                            '#17a673',
                            '#2c9faf',
                            '#dda20a',
                            '#be2617',
                            '#6c757d'
                        ],
                        hoverBorderColor: "rgba(234, 236, 244, 1)",
                    }]
                },
                options: {
                    responsive: true,
                    maintainAspectRatio: false,
                    plugins: {
                        tooltip: {
                            callbacks: {
                                label: (context) => {
                                    const value = context.raw;
                                    const total = context.dataset.data.reduce((a, b) => a + b, 0);
                                    const percentage = Math.round((value / total) * 100);
                                    return `${context.label}: ${this.formatCurrency(value)} (${percentage}%)`;
                                }
                            }
                        }
                    },
                    cutout: '70%',
                }
            });
        },

        formatCurrency(value) {
            return new Intl.NumberFormat('ru-RU', {
                style: 'currency',
                currency: 'RUB',
                minimumFractionDigits: 0
            }).format(value);
        },

        formatDate(dateString) {
            const date = new Date(dateString);
            return date.toLocaleDateString('ru-RU') + ' ' + date.toLocaleTimeString('ru-RU', { hour: '2-digit', minute: '2-digit' });
        }
    },

    beforeUnmount() {
        // Уничтожаем графики при удалении компонента
        if (this.monthlyChart) this.monthlyChart.destroy();
        if (this.categoryChart) this.categoryChart.destroy();
    }
};
</script>

<style scoped>
.report-container {
    max-width: 1200px;
    margin: 0 auto;
    padding: 20px;
}

.load-btn {
    background-color: #4e73df;
    color: white;
    padding: 10px 20px;
    border: none;
    border-radius: 4px;
    cursor: pointer;
    font-size: 16px;
    margin-bottom: 20px;
}

.load-btn:hover {
    background-color: #2e59d9;
}

.error-message {
    color: #e74a3b;
    padding: 10px;
    background-color: #f8d7da;
    border-radius: 4px;
    margin-bottom: 20px;
}

.report-content {
    display: grid;
    grid-template-columns: 1fr 1fr;
    gap: 20px;
}

.chart-box,
.summary-box,
.transactions-box {
    background: white;
    border-radius: 8px;
    box-shadow: 0 0 10px rgba(0, 0, 0, 0.05);
    padding: 20px;
}

.chart-box {
    grid-column: span 1;
    height: 400px;
}

.summary-box {
    grid-column: span 2;
}

.transactions-box {
    grid-column: span 2;
}

h3 {
    margin-top: 0;
    color: #5a5c69;
    border-bottom: 1px solid #e3e6f0;
    padding-bottom: 10px;
}

.summary-item {
    display: flex;
    justify-content: space-between;
    margin: 10px 0;
    font-size: 16px;
}

.amount {
    font-weight: bold;
}

.income {
    color: #1cc88a;
}

.expense {
    color: #e74a3b;
}

.balance {
    color: #4e73df;
}

.transfer-item {
    display: grid;
    grid-template-columns: 120px 150px 100px 1fr;
    gap: 10px;
    padding: 8px 0;
    border-bottom: 1px solid #f8f9fc;
}

.transfer-item:hover {
    background-color: #f8f9fc;
}

.transfer-date {
    color: #858796;
}

.transfer-category {
    font-weight: 500;
}

.transfer-amount {
    text-align: right;
    font-weight: bold;
}

.transfer-desc {
    color: #858796;
    white-space: nowrap;
    overflow: hidden;
    text-overflow: ellipsis;
}

@media (max-width: 768px) {
    .report-content {
        grid-template-columns: 1fr;
    }

    .chart-box {
        grid-column: span 1;
    }

    .transfer-item {
        grid-template-columns: 1fr 1fr;
    }
}
</style>