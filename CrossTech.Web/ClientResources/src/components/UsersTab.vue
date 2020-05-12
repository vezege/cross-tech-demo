<template>
    <table class="table table-borderless">
        <thead class="thead-dark">
        <tr>
            <th scope="col">#</th>
            <th scope="col">Имя пользователя</th>
            <th scope="col">Пароль</th>
            <th scope="col">Роль</th>
        </tr>
        </thead>
        <tbody>
        <tr v-for="user in $store.state.users" :key="user.Id">
            <th scope="row">{{ user.Id }}</th>
            <td>{{ user.Login }}</td>
            <td>{{ user.Password }}</td>
            <td>{{ user.Role }}</td>
        </tr>
        </tbody>
    </table>
</template>

<script>
export default {
    created: function () {
        window.axios.get('/users').then(data => {
            if (data.data.success) {
                let usersList = data.data.payload;

                for (let i = 0; i < usersList.length; i++) {
                    Object.keys(usersList[i]).forEach(key => {
                        let upperCaseKey = key[0].toUpperCase() + key.slice(1);
                        usersList[i][upperCaseKey] = usersList[i][key];
                        delete usersList[i][key];
                    });
                }
                
                this.$store.commit('setUsers', usersList);
            } else {
                this.$notify({
                    group: 'main',
                    title: 'Что-то пошло не так',
                    text: 'При загрузке списка пользователей произошла ошибка',
                    type: 'error'
                });
            }
      });
    }
}
</script>