<template>
  <!-- BEGIN: Modal Content -->
  <div id="add-edit-modal" class="modal" tabindex="-1" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">
        <form ref="form" enctype="multipart/form-data">
          <!-- BEGIN: Modal Header -->
          <div class="modal-header">
            <h2 class="font-medium text-base mr-auto">
              {{ formTitle }}
            </h2>
          </div>
          <!-- END: Modal Header -->
          <!-- BEGIN: Modal Body -->
          <div class="modal-body grid grid-cols-12 gap-4 gap-y-3">
            <div class="col-span-12 sm:col-span-12">
              <label for="form-name" class="form-label">Organization Name</label>
              <input
                v-model="model.name"
                type="text"
                class="form-control"
                id="form-name"
              />
            </div>
            <div class="col-span-12 sm:col-span-12">
              <label for="form-description" class="form-label">Description</label>
              <textarea
                v-model="model.description"
                rows="4"
                class="form-control"
                id="form-address"
              ></textarea>
            </div>
            <div class="col-span-12 sm:col-span-6">
              <label for="form-primary-contact-email" class="form-label">Primary Contact Email</label>
              <input
                v-model="model.primaryContactEmail"
                type="text"
                class="form-control"
                id="form-city"
              />
            </div>
          </div>
          <!-- END: Modal Body -->
          <!-- BEGIN: Modal Footer -->
          <div class="modal-footer text-right">
            <button
              type="button"
              data-dismiss="modal"
              class="btn btn-outline-secondary w-20 mr-1"
            >
              Cancel
            </button>
            <button type="submit" class="btn btn-primary w-20">Save</button>
          </div>
          <!-- END: Modal Footer -->
        </form>
      </div>
    </div>
  </div>
  <!-- END: Modal Content -->
</template>

<script>
import { defineComponent } from 'vue'

export default defineComponent({
  props: {
    model: {
      type: Object
    },
    formTitle: {
      type: String
    },
    saveData: {
      type: Function,
    },
  },
  setup(props, { emit }) {
    const close = () => {
      emit('close')
    }
    const isNumberKey = () => function(evt){
      var charCode = (evt.which) ? evt.which : evt.keyCode;
      if (charCode != 46 && charCode > 31 
        && (charCode < 48 || charCode > 57))
          return false;

      return true;
    }
    return { close, isNumberKey }
  },
})
</script>