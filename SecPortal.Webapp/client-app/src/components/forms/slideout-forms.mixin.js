import { useToast } from 'vue-toastification'
import { mapGetters, mapActions } from 'vuex'

export default {
  data() {
    return {
      isReady: false,
      formModel: {},
      baseForm: {},
      Get: () => {},
      Post: () => {},
      Put: () => {},
      OnReady: () => {},
      OnGet: () => {},
      formOptions: {
        validateAfterLoad: false,
        validateAfterChanged: true,
        validateAsync: true
      }
    }
  },
  computed: {
    ...mapGetters('slideover', ['activeId'])
  },
  methods: {
    ...mapActions('slideover', ['ChangeShownState']),
    ...mapActions('app', ['ChangeLoadingState']),
    setBaseForm(obj, Get, Put, Post, OnReady, OnGet) {
      this.baseForm = obj
      this.Get = Get
      this.Put = Put
      this.Post = Post

      if (OnReady) {
        this.OnReady = OnReady
      }

      if (OnGet) {
        this.OnGet = OnGet
      }
    },
    async vfgSubmit() {
      const error = await this.$refs.vfg1.validate()
      if (error.length > 0) {
        return
      }

      this.submit()
    },
    async reload() {
      this.isReady = false
      let response
      if (this.activeId) {
        response = await this.Get(this.activeId)
        this.OnGet(response)
        this.formModel = response.data.data
      } else {
        this.formModel = this.baseForm
      }

      this.OnReady(response)
      this.isReady = true
    },
    async submit() {
      this.ChangeLoadingState(true)
      const toast = useToast()
      try {
        if (this.activeId) {
          await this.doPut()
        } else {
          await this.doPost()
        }
      } catch (e) {
        toast.error(
          'Failed to process your request, please try again or contact your administrator!',
          {
            timeout: 2000
          }
        )
      } finally {
        this.ChangeLoadingState(false)
      }
    },
    async doPut() {
      const toast = useToast()
      const response = await this.Put(this.activeId, this.formModel)
      if (response.data.isSuccess) {
        toast.success('Edit success!')
        this.ChangeShownState({ isShown: false, title: '' })
        this.emitter.emit('refresh_table')
      } else {
        throw Object.assign(new Error(), { code: 500 })
      }
    },
    async doPost() {
      const toast = useToast()
      const response = await this.Post(this.formModel)
      if (response.status === 200) {
        if (response.data.isSuccess) {
          toast.success(response.data.message, {
            timeout: 2000
          })
        } else {
          toast.error(response.data.message, {
            timeout: 10000
          })
        }
      } else if (response.status === 201) {
        toast.success('Creation success!')
        this.ChangeShownState({ isShown: false, title: '' })
        this.emitter.emit('refresh_table')
      } else throw Object.assign(new Error(), { code: 500 })
    }
  }
}
