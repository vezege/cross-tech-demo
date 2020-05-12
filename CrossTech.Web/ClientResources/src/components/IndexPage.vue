<template>
  <div>
    <ul class="nav nav-pills mb-3" id="pills-tab" role="tablist">
      <li class="nav-item">
        <a class="nav-link active" id="pills-employees-tab" data-toggle="pill" href="#pills-employees" role="tab" aria-controls="pills-employees" aria-selected="true">Сотрудники</a>
      </li>
      <li class="nav-item" v-if="userCan('see_users_list')">
        <a class="nav-link" id="pills-users-tab" data-toggle="pill" href="#pills-users" role="tab" aria-controls="pills-users" aria-selected="false">Пользователи</a>
      </li>
    </ul>
    <div class="tab-content" id="pills-tabContent">
      <div class="tab-pane fade show active" id="pills-employees" role="tabpanel" aria-labelledby="pills-employees-tab">
        <employees-tab></employees-tab>
      </div>
      <div class="tab-pane fade" id="pills-users" role="tabpanel" aria-labelledby="pills-users-tab" v-if="userCan('see_users_list')">
        <users-tab></users-tab>
      </div>
    </div>
  </div>
</template>

<script>
import { mapMutations } from 'vuex';

export default {
  data: function () {
    return {
      opened: false
    }
  },
  methods: {
    ...mapMutations([`showModal`]),
    userCan: (action) => {
      let permissions = window.requesterPermissions ?? ['see_users_list'];
      return permissions.includes(action)
    }
  },
  mounted: function() {
    
  }
}
</script>

<style scoped>
.modal-mask {
  position: fixed;
  z-index: 9998;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, .5);
  display: table;
  transition: opacity .3s ease;
}

.modal-wrapper {
  display: table-cell;
  vertical-align: middle;
}
</style>
