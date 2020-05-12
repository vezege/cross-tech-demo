import Vue from 'vue';
import Vuex from 'vuex';

Vue.use(Vuex);

export const mutations = {
  showModal(state, {name, props}) {
    // eslint-disable-next-line no-param-reassign
    state.modalVisible = true;
    // eslint-disable-next-line no-param-reassign
    state.modalComponent = name;
    state.modalProps = props;
  },
  hideModal(state) {
    // eslint-disable-next-line no-param-reassign
    state.modalVisible = false;
    state.modalProps = null;
  },
  addEmployee(state, newEmployee) {
    state.employees.push(newEmployee);
  },
  deleteEmployee(state, employeeId) {
    state.employees = state.employees.filter(employee => {
      return employee.Id != employeeId;
    })
  },
  setUsers(state, users) {
    state.users = users;
  }
};

export const state = {
  modalVisible: false,
  modalComponent: null,
  modalProps: null,
  employees: window.employees,
  users: []
};

export const store = new Vuex.Store({
  mutations,
  state,
});
