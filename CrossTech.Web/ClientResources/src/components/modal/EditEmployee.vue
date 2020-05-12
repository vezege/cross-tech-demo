<template>
    <form action="#" @submit.prevent="updateEmployee">
        <div class="c-modalLogin">
            <div class="form-row">
                <div class="col-md-6">
                    <div class="form-group">
                        <label for="first-name-input">Имя</label>
                        <input name="FirstName" required type="text" id="first-name-input" class="form-control" v-model="employee.FirstName" :class="{ 'is-invalid': validationErrors['FirstName'] != null }">
                        <div class="invalid-feedback" v-if="validationErrors['FirstName'] != null">
                            {{ validationErrors['FirstName'][0] }}
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <label for="last-name-input">Фамилия</label>
                        <input name="LastName" required type="text" id="last-name-input" class="form-control" v-model="employee.LastName" :class="{ 'is-invalid': validationErrors['LastName'] != null }">
                        <div class="invalid-feedback" v-if="validationErrors['LastName'] != null">
                            {{ validationErrors['LastName'][0] }}
                        </div>
                    </div>
                </div>
            </div>
            
            <div class="form-row">
                <div class="col-md-6">
                    <div class="form-group">
                        <label for="middle-name-input">Отчество</label>
                        <input name="MiddleName" type="text" id="middle-name-input" class="form-control" v-model="employee.MiddleName" :class="{ 'is-invalid': validationErrors['MiddleName'] != null }">
                        <div class="invalid-feedback" v-if="validationErrors['MiddleName'] != null">
                            {{ validationErrors['MiddleName'][0] }}
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <label for="phone-input">Телефон</label>
                        <input name="Phone" required type="text" id="phone-input" class="form-control" placeholder="+7 (xxx) xxx-xx-xx" v-model="employee.Phone" :class="{ 'is-invalid': validationErrors['Phone'] != null }">
                        <div class="invalid-feedback" v-if="validationErrors['Phone'] != null">
                            {{ validationErrors['Phone'][0] }}
                        </div>
                    </div>
                </div>
            </div>

            <div class="form-group">
                <label>Пол</label>
                <br/>
                <div class="custom-control custom-radio custom-control-inline">
                    <input type="radio" id="FemaleGender" name="Gender" class="custom-control-input" value="Female" v-model="employee.Gender">
                    <label class="custom-control-label" for="FemaleGender">Женский</label>
                </div>
                <div class="custom-control custom-radio custom-control-inline">
                    <input type="radio" id="MaleGender" name="Gender" class="custom-control-input" value="Male" v-model="employee.Gender">
                    <label class="custom-control-label" for="MaleGender">Мужской</label>
                </div>
                <div class="invalid-feedback" v-if="validationErrors['Gender'] != null">
                    {{ validationErrors['Gender'][0] }}
                </div>
            </div>

            <div class="form-group">
                <label for="name">Должность</label>
                <select class="custom-select" v-model="employee.PositionId" :class="{ 'is-invalid': validationErrors['PositionId'] != null }">
                    <option :value="position.id" v-for="position in positions" :key="position.id">{{ position.name }}</option>
                </select>
                <div class="invalid-feedback" v-if="validationErrors['PositionId'] != null">
                    {{ validationErrors['PositionId'][0] }}
                </div>
            </div>

            <div class="form-group">
                <label for ="birthdate-input">Дата рождения</label>
                <datepicker name="Birthdate" format="yyyy-MM-dd" v-model="employee.Birthdate" :input-class="validationErrors['Birthdate'] != null ? 'is-invalid' : ''" :monday-first="true" :bootstrap-styling="true" :disabled-dates="birthdateRange" :language="ruLanguage"></datepicker>
                <div class="invalid-feedback" v-if="validationErrors['Birthdate'] != null">
                    {{ validationErrors['Birthdate'][0] }}
                </div>
            </div>

            <button class="btn btn-success" type="submit">Сохранить</button>
            <button class="btn btn-danger" @click="hideModal" type="button">Отменить</button>
        </div>
    </form>
</template>

<script>
import { mapMutations } from 'vuex';
import Datepicker from 'vuejs-datepicker';
import { ru } from 'vuejs-datepicker/dist/locale';

export default {
    name: `EditEmployee`,
    components: {
        Datepicker
    },
    data: function () {
        return {
            birthdateRange: {
                from: new Date(2005, 12, 30),
                to: new Date(1940, 1, 1)
            },
            ruLanguage: ru,
            positions: [],
            validationErrors: [],
            employee: {}
        };
    },
    methods: {
        ...mapMutations([
            `hideModal`,
        ]),
        updateEmployee: function() {
            this.validationErrors = [];

            // формируем данные запроса, меняем формат некоторых полей
            let employeeData = new FormData();
            Object.keys(this.employee).forEach(key => {
                if (key === 'Birthdate' && this.employee[key] instanceof Date) {
                    employeeData.append(key, this.employee[key].toISOString());
                } else if (key === 'Gender' && this.employee[key] != null) {
                    let genderCode = this.employee[key] == 'Male' ? 1 : 0;
                    employeeData.append('Gender', genderCode);
                } else {
                    employeeData.append(key, this.employee[key]);
                }
            });

            window.axios.patch('/employees/' + this.employee.Id + '/edit', employeeData).then(data => {
                let employee = this.$store.state.employees.find((employee) => {
                    return employee.Id === this.$store.state.modalProps;
                });

                // меняем названия полей на PascalCase, чтобы сохранить единообразие, и заменяем данные сотрудника
                let updatedEmployee = data.data.payload;
                Object.keys(updatedEmployee).forEach(key => {
                    let upperCaseKey = key[0].toUpperCase() + key.slice(1);
                    employee[upperCaseKey] = updatedEmployee[key];
                });

                this.hideModal();
            }).catch(error => {
                if (error.response.status === 422) {
                    this.validationErrors = error.response.data.payload;
                    this.$notify({
                        group: 'main',
                        title: 'Отправлены некорректные данные',
                        text: 'Пожалуйста, проверьте введенные данные ещё раз',
                        type: 'error'
                    });
                } else {
                    let errorMessage = typeof error.response.data.payload === 'string' ? error.response.data.payload : 'Пожалуйста, попробуйте позднее';
                    this.$notify({
                        group: 'main',
                        title: 'Что-то пошло не так',
                        text: errorMessage,
                        type: 'error'
                    });
                }
            });
        }
    },
    created: function() {
        window.axios.get('/positions').then((data) => {
            this.positions = data.data.payload;
        });
        
        let employeeId = this.$store.state.modalProps;
        if (employeeId === null) {
            return;
        }

        let employee = this.$store.state.employees.find((employee) => {
            return employee.Id === employeeId;
        });
        if (employee == null) {
            this.$notify({
                group: 'main',
                title: 'Ошибка',
                text: 'Сотрудник не найден',
                type: 'error'
            });
        }

        // создаем локальную копию сотрудника, чтобы не менять в форме актуальную версию
        let employeeCopy = {};
        Object.assign(employeeCopy, employee);
        if (employeeCopy.Birthdate != null) {
            employeeCopy.Birthdate = new Date(employeeCopy.Birthdate);
        }
        this.employee = employeeCopy;
    }
};
</script>
