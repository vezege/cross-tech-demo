<template>
    <div>
        <div class="text-left mb-2">
            <button type="button" class="btn btn-primary" @click="showModal({ name: 'AddEmployee' })" v-if="userCan('create_employee')">Добавить сотрудника</button>
        </div>
        <table class="table table-borderless">
            <thead class="thead-dark">
            <tr>
                <th scope="col">#</th>
                <th scope="col">Имя</th>
                <th scope="col">Пол</th>
                <th scope="col">Должность</th>
                <th scope="col">Дата рождения</th>
                <th scope="col">Телефон</th>
                <th scope="col" v-if="userCan('update_employee') || userCan('delete_employee')">Управление</th>
            </tr>
            </thead>
            <tbody>
            <tr v-for="employee in $store.state.employees" :key="employee.Id">
                <th scope="row">{{ employee.Id }}</th>
                <td>{{ employee.FullName }}</td>
                <td>{{ employee.Gender }}</td>
                <td>{{ employee.Position }}</td>
                <td>{{ employee.Birthdate }}</td>
                <td>{{ employee.Phone }}</td>
                <td>
                <button type="button" class="btn btn-primary" v-if="userCan('update_employee')" @click="showModal({ name: 'EditEmployee', props: employee.Id })">Редактировать</button>
                <button type="button" class="btn btn-danger" v-if="userCan('delete_employee')" @click="deleteEmployee(employee.Id)">Удалить</button>
                </td>
            </tr>
            </tbody>
        </table>
    </div>
</template>

<script>
import { mapMutations } from 'vuex';

export default {
    data: function () {
        return {
            employees: this.$store.state.employees
        }
    },
    methods: {
        ...mapMutations([`showModal`]),
        userCan: (action) => {
            let permissions = window.requesterPermissions ?? ['delete_employee'];
            return permissions.includes(action)
        },
        deleteEmployee: function(employeeId) {
            window.axios.delete('/employees/' + employeeId).then(() => {
                this.$notify({
                    group: 'main',
                    title: 'Сотрудник был удален',
                    type: 'success'
                });
                this.$store.commit('deleteEmployee', employeeId);
            }).catch((error) => {
                let errorMessage = typeof error.response.payload === 'string' ? error.response.payload : 'Пожалуйста, попробуйте позднее';
                this.$notify({
                    group: 'main',
                    title: 'Что-то пошло не так',
                    text: errorMessage,
                    type: 'error'
                });
            });
        }
    }
}
</script>