<template>
  <div
    class="
      report-box-4
      w-full
      h-full
      grid grid-cols-12
      gap-6
      xl:absolute
      -mt-8
      xl:mt-0
      pb-6
      xl:pb-0
      top-0
      right-0
      z-30
      xl:z-auto
    "
  >
    <div class="col-span-12 xl:col-span-3 xl:col-start-10 xl:pb-16 z-30">
      <div class="h-full flex flex-col">
        <div class="report-box-4__content xl:min-h-0 intro-x">
          <div class="max-h-full xl:overflow-y-auto box mt-5">
            <div class="bg-white dark:bg-dark-3 xl:sticky top-0 px-5 pt-5 pb-6">
              <div class="flex items-center">
                <div class="text-lg font-medium truncate mr-5">
                  Summary Report
                </div>
                <a
                  @click="GetDashboardSummary"
                  href="javascript:;"
                  class="
                    ml-auto
                    flex
                    items-center
                    text-theme-30
                    dark:text-theme-25
                  "
                >
                  <RefreshCcwIcon class="w-4 h-4 mr-3" />
                  Refresh
                </a>
              </div>
            </div>
            <div class="tab-content px-5 pb-5">
              <div
                id="active-users"
                class="tab-pane active grid grid-cols-12 gap-y-6"
                role="tabpanel"
                aria-labelledby="active-users-tab"
              >
                <div
                  class="col-span-12 sm:col-span-6 md:col-span-4 xl:col-span-12"
                >
                  <div class="text-gray-600 dark:text-gray-500">
                    Total Unprocessed Invoice
                  </div>
                  <div class="mt-1.5 flex items-center">
                    <div class="text-lg">{{
                        numeralFormat(
                          summary.totalUnprocessedInvoice,
                          '0,0[.]00'
                        )
                      }}</div>
                  </div>
                </div>
                <div
                  class="col-span-12 sm:col-span-6 md:col-span-4 xl:col-span-12"
                >
                  <div class="text-gray-600 dark:text-gray-500">
                    Daily New Invoice
                  </div>
                  <div class="mt-1.5 flex items-center">
                    <div class="text-lg">{{
                        numeralFormat(
                          summary.dailyNewInvoice,
                          '0,0[.]00'
                        )
                      }}</div>
                  </div>
                </div>
                <div
                  class="col-span-12 sm:col-span-6 md:col-span-4 xl:col-span-12"
                >
                  <div class="text-gray-600 dark:text-gray-500">
                    Daily Expired Invoice
                  </div>
                  <div class="mt-1.5 flex items-center">
                    <div class="text-lg">{{
                        numeralFormat(
                          summary.dailyExpiredInvoice,
                          '0,0[.]00'
                        )
                      }}</div>
                  </div>
                </div>

                <div
                  class="col-span-12 sm:col-span-6 md:col-span-4 xl:col-span-12"
                >
                  <div class="text-gray-600 dark:text-gray-500">
                    Daily Customer Registration
                  </div>
                  <div class="mt-1.5 flex items-center">
                    <div class="text-lg">
                      {{
                        numeralFormat(
                          summary.dailyCustomerRegistration,
                          '0,0[.]00'
                        )
                      }}
                    </div>
                  </div>
                </div>
                <div
                  class="col-span-12 sm:col-span-6 md:col-span-4 xl:col-span-12"
                >
                  <div class="text-gray-600 dark:text-gray-500">
                    Daily Revenue
                  </div>
                  <div class="mt-1.5 flex items-center">
                    <div class="text-lg">
                      Rp.
                      {{ numeralFormat(summary.dailyRevenue, '0,0[.]00') }}
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { DashboardSummary } from '@/core/services/entities/dashboard.service'
export default {
  data() {
    return {
      summary: {
        totalUnprocessedInvoice: 0,
        dailyNewInvoice: 0,
        dailyExpiredInvoice: 0,
        dailyCustomerRegistration: 0,
        dailyRevenue: 0
      }
    }
  },
  methods: {
    async GetDashboardSummary() {
      const response = await DashboardSummary()
      if (response.status === 200 && response.data.isSuccess) {
        this.summary = response.data.data
      }
    }
  },
  mounted() {
    this.GetDashboardSummary()
  }
}
</script>

<style>
</style>
